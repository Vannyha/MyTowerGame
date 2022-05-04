using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = System.Object;

namespace Context
{
    public class Injector : MonoBehaviour, IInjector
    {
        private MultiMap<Type, IBean> singletonMultiMap = new MultiMap<Type, IBean>();
        private HashSet<IBean> singletonBeans = new HashSet<IBean>();
        private static readonly HashSet<Type> IgnoredTypes = new HashSet<Type>(new[]
        {
            typeof(MonoBehaviour),
            typeof(Component),            
            typeof(IInitResolve),            
            typeof(IBean),            
        });
        private static readonly BindingFlags FieldsBindingFlags =
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        private void Awake()
        {
            InitSingletons();
            DontDestroyOnLoad(gameObject);
        }

        private void InitSingletons()
        {
            AddToSingletonCollections(this);
            
            List<GameObject> gameObjectsToScan = new List<GameObject>(FindObjectsOfType<GameObject>(true));
            gameObjectsToScan.Remove(gameObject);

            List<GameObject> beanGameObjects = new List<GameObject>();

            IEnumerable<GameObject> foundedBeanGameObjectsAfterScan = ScanForSingletons(gameObjectsToScan);
            beanGameObjects.AddRange(foundedBeanGameObjectsAfterScan);

            foreach (IBean bean in singletonBeans)
            {
                InjectBeanFields(bean);
            }

            ResolveInit(singletonBeans);
            
            foreach (GameObject beanGameObject in beanGameObjects)
            {
                beanGameObject.SetActive(true);
            }
        }
        
        private void AddToSingletonCollections(IBean bean)
        {
            foreach (Type beanType in GetBeanTypes(bean))
            {
                singletonMultiMap.Add(beanType, bean);
                singletonBeans.Add(bean);
            }
        }
        
        private ICollection<Type> GetBeanTypes(IBean bean)
        {
            Type currentType = bean.GetType();
            List<Type> baseTypes = new List<Type>(currentType.GetInterfaces());
            for (int i = baseTypes.Count - 1; i >= 0; i--)
            {
                Type type = baseTypes[i];
                if (IgnoredTypes.Contains(type) || !typeof(IBean).IsAssignableFrom(type))
                {
                    baseTypes.RemoveAt(i);
                }
            }

            while (currentType != null && !IgnoredTypes.Contains(currentType) &&
                   typeof(IBean).IsAssignableFrom(currentType))
            {
                baseTypes.Add(currentType);
                currentType = currentType.BaseType;
            }

            return baseTypes;
        }
        
        private List<GameObject> ScanForSingletons(List<GameObject> gameObjectsToScan)
        {
            List<GameObject> foundBeanGameObjects = new List<GameObject>();
            foreach (GameObject scanObject in gameObjectsToScan)
            {
                Component[] topComponents = scanObject.GetComponents<Component>();
                bool isSingleton = false;
                foreach (Component beanComponent in topComponents)
                {
                    if (beanComponent == null)
                    {
                        throw new MissingComponentException(
                            String.Format("{0} have some missing components", scanObject));
                    }
                    
                    SingletonAttribute customAttribute = (SingletonAttribute)Attribute.GetCustomAttribute(
                        beanComponent.GetType(), 
                        typeof(SingletonAttribute), false);
                    
                    if (customAttribute != null)
                    {
                        isSingleton = true;
                        break;                        
                    }
                }
                
                if (isSingleton)
                {
                    foundBeanGameObjects.Add(scanObject);
                    Component[] components = scanObject.GetComponentsInChildren<Component>();
                    foreach (Component beanComponent in components)
                    {
                        if (typeof(IBean).IsAssignableFrom(beanComponent.GetType()) &&
                            !singletonBeans.Contains((IBean) beanComponent))
                        {
                            AddToSingletonCollections((IBean) beanComponent);
                        }
                    }
                }
            }
            return foundBeanGameObjects;
        }

        private void InjectBeanFields(object bean)
        {
            Type currentType = bean.GetType();
            ICollection<FieldInfo> fieldInfos = GetFieldsWithAttribute(currentType, FieldsBindingFlags, typeof(InjectAttribute));
            
            while (currentType != null 
                   && (typeof(IBean).IsAssignableFrom(currentType)))
            {
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    InjectAttribute customAttribute = (InjectAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(InjectAttribute), false);

                    if (customAttribute != null)
                    {
                        Type injectType = fieldInfo.FieldType;
                        object fieldValue = null;
                        
                        if (injectType.IsGenericType && (injectType.GetGenericTypeDefinition() == typeof(ICollection<>) || injectType.GetGenericTypeDefinition() == typeof(IList<>)))
                        {
                            Type genericArgument = injectType.GetGenericArguments()[0];  
                            
                            MethodInfo method = typeof(Injector).GetMethod("GetBeans", new[] {typeof(IBean)});
                            MethodInfo generic = method.MakeGenericMethod(genericArgument);
                            fieldValue = generic.Invoke(this, new[] {(object)bean});                        
                        }
                        else if (injectType.IsArray)
                        {
                            ICollection<IBean> collection = GetBeans<IBean>(injectType.GetElementType());
                            Array array = Array.CreateInstance(injectType.GetElementType(), collection.Count);
                            int i = 0;
                            foreach (IBean bean1 in collection)
                            {
                                array.SetValue(bean1, i++);                            
                            }
                            fieldValue = array;
                        }
                        else
                        {
                            fieldValue = GetBean(injectType);
                        }
                        
                        fieldInfo.SetValue(bean, fieldValue);                        
                    }
                }                
                
                currentType = currentType.BaseType;
                if (currentType != null)
                {
                    fieldInfos = currentType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                }
            }
        }
        
        private ICollection<FieldInfo> GetFieldsWithAttribute(Type type, BindingFlags flags, Type attribute)
        {
            HashSet<FieldInfo> fields = new HashSet<FieldInfo>();
            FieldInfo[] fieldInfos = type.GetFields(flags);
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                if (attribute.IsInstanceOfType(Attribute.GetCustomAttribute(fieldInfo, attribute, false)))
                {
                    fields.Add(fieldInfo);
                }
            }
            return fields;
        }

        private Object GetBean(Type type)
        {
            if (GetBeanLocalContext().TryGetValue(type, out IBean bean))
            {
                return bean;
            }
            
            return new Exception("No bean found");
        }
        
        private ICollection<T> GetBeans<T>(Type type) where T:IBean
        {
            List<T> resultBeans = new List<T>();
            
            ICollection<T> valuesByKey = GetBeanLocalContext().ValuesByKey<T>(type);
            resultBeans.AddRange(valuesByKey);

            return resultBeans;                        
        }
        
        private void ResolveInit(ICollection<IBean> beans)
        {
            Dictionary<Type, ICollection<Type>> beansTypesMap = new Dictionary<Type, ICollection<Type>>();
            HashSet<Type> beanTypes = new HashSet<Type>();
            
            foreach (IBean bean in beans)
            {
                ICollection<Type> bt = GetBeanTypes(bean);
                
                try
                {
                    beansTypesMap.Add(bean.GetType(), bt);
                }
                catch (ArgumentException)
                {
                    //
                }
                
                foreach (Type t in bt)
                {
                    beanTypes.Add(t);
                }
            }
            ICollection<IBean> resolvedBeans = new HashSet<IBean>();
            Dictionary<IBean, HashSet<Type>> beansDependencies = new Dictionary<IBean, HashSet<Type>>();
            Dictionary<IBean, HashSet<Type>> beansInjects = new Dictionary<IBean, HashSet<Type>>();

            foreach (IBean bean in beans)
            {
                if (!(bean is IInitResolve))
                {
                    resolvedBeans.Add(bean);
                    continue;
                }
                IInitResolve postResolvingBean = bean as IInitResolve;
                HashSet<Type> dependencies = GetDependencies(postResolvingBean);
                if (null == dependencies)
                {
                    ICollection<FieldInfo> fieldsWithAttribute = GetFieldsWithAttribute(postResolvingBean.GetType(), FieldsBindingFlags, typeof(InjectAttribute));
                    HashSet<Type> injects = new HashSet<Type>();
                    foreach (FieldInfo fieldInfo in fieldsWithAttribute)
                    {
                        injects.Add(fieldInfo.FieldType);
                    }
                    
                    if (0 == injects.Count)
                    {
                        resolvedBeans.Add(postResolvingBean);
                    }
                    else
                    {
                        HashSet<Type> intersects = new HashSet<Type>();
                        foreach (Type inject in injects)
                        {
                            foreach (Type beanType in beanTypes)
                            {
                                if (inject == beanType)
                                {
                                    intersects.Add(beanType);
                                    break;
                                }
                            }
                        }
                        beansInjects.Add(postResolvingBean, intersects);
                    }
                }
                else
                {
                    HashSet<Type> intersects = new HashSet<Type>();
                    foreach (Type dependency in dependencies)
                    {
                        foreach (Type beanType in beanTypes)
                        {
                            if (dependency == beanType)
                            {
                                intersects.Add(dependency);
                                break;
                            }
                        }
                    }
                    beansDependencies.Add(postResolvingBean, intersects);
                }
            }
            
            foreach (IBean resolvedBean in resolvedBeans)
            {
                IInitResolve prBean = resolvedBean as IInitResolve;
                if (prBean != null)
                {
                    prBean.Init();
                }
            }

            while (0 != resolvedBeans.Count)
            {
                foreach (IBean bean in resolvedBeans)
                {
                    ICollection<Type> types = null;
                    if (!beansTypesMap.TryGetValue(bean.GetType(), out types))
                    {
                        //
                    }
                    beansDependencies.Remove(bean);
                    foreach (KeyValuePair<IBean,HashSet<Type>> item in beansDependencies)
                    {
                        item.Value.RemoveWhere(itemValue => types.Contains(itemValue));
                    }
                    
                    beansInjects.Remove(bean);
                    foreach (KeyValuePair<IBean,HashSet<Type>> item in beansInjects)
                    {
                        item.Value.RemoveWhere(itemValue => types.Contains(itemValue));
                    }
                }
                resolvedBeans.Clear();
                
                HashSet<IBean> beansWd = new HashSet<IBean>();
                foreach (KeyValuePair<IBean,HashSet<Type>> beansDependency in beansDependencies)
                {
                    beansWd.Add(beansDependency.Key);
                }
                foreach (KeyValuePair<IBean,HashSet<Type>> pair in beansInjects)
                {
                    beansWd.Add(pair.Key);
                }
                foreach (IBean bean in beansWd)
                {
                    HashSet<Type> dependencies = null;
                    HashSet<Type> injects = null;
                    if (beansDependencies.TryGetValue(bean, out dependencies))
                    {
                        if (null == dependencies || 0 == dependencies.Count)
                        {
                            resolvedBeans.Add(bean);
                        }
                    }
                    else if (beansInjects.TryGetValue(bean, out injects) && 0 == injects.Count)
                    {
                        resolvedBeans.Add(bean);
                    }
                }
                
                foreach (IBean bean in resolvedBeans)
                {
                    if (bean is IInitResolve prBean)
                    {
                        prBean.Init();
                    }
                }
            }
            if (0 != beansDependencies.Count)
            {
                string dependenciesMessage = "";
                foreach (KeyValuePair<IBean,HashSet<Type>> dependency in beansDependencies)
                {
                    dependenciesMessage += dependency.Key + ", ";
                }
            }
            if (0 != beansInjects.Count)
            {
                string injectsMessage = "";
                foreach (KeyValuePair<IBean,HashSet<Type>> inject in beansInjects)
                {
                    injectsMessage += inject.Key + ", ";
                }
            }
        }
        
        private HashSet<Type> GetDependencies(IBean bean)
        {
            if (null == (bean as IInitResolve))
            {
                return null;
            }
            HashSet<Type> types = null;
            object[] customAttributes = bean.GetType().GetCustomAttributes(typeof(ResolveAfterAttribute), true);
            foreach (object dependency in customAttributes)
            {
                if (dependency is ResolveAfterAttribute postResolveDependencyAttribute && null != postResolveDependencyAttribute.Dependencies)
                {
                    if (null == types)
                    {
                        types = new HashSet<Type>();
                    }
                    for (int i = 0; i < postResolveDependencyAttribute.Dependencies.Length; i++)
                    {
                        Type type = postResolveDependencyAttribute.Dependencies[i];
                        types.Add(type);
                    }
                }
            }
            return types;
        }
        
        private  MultiMap<Type, IBean> GetBeanLocalContext()
        {
            return singletonMultiMap;
        }
    }
}