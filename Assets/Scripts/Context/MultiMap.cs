using System;
using System.Collections;
using System.Collections.Generic;

namespace Context
{
    public class MultiMap<Key, Value> : IDictionary<Key, Value>
    {
        private IDictionary<Key, Value> internalMap = new Dictionary<Key, Value>();
        private IDictionary<Key, LinkedList<Value>> listMap = new Dictionary<Key, LinkedList<Value>>();
        
        public bool IsReadOnly { get { return internalMap.IsReadOnly; } }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return internalMap.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<Key, Value> item)
        {            
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            internalMap.Clear();
            listMap.Clear();
        }

        public bool Contains(KeyValuePair<Key, Value> item)
        {
            bool containsInMap = internalMap.Contains(item);
            if (containsInMap)
            {
                return true;
            }
            
            LinkedList<Value> list;
            if (listMap.TryGetValue(item.Key, out list))
            {
                foreach (Value value in list)
                {
                    if (value.Equals(item.Value))
                        return true;
                }
            }
            return false;                        
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            return Remove(item.Key);
        }

        public int Count 
        {
            get { return internalMap.Count; }  
        }
        
        public bool ContainsKey(Key key)
        {
            return internalMap.ContainsKey(key);
        }

        public void Add(Key key, Value value)
        {
            if (internalMap.ContainsKey(key))
            {
                LinkedList<Value> list;
                if (!listMap.TryGetValue(key, out list))
                {
                    list = new LinkedList<Value>();
                    listMap.Add(key, list);
                }
                list.AddLast(value);
            }
            else
            {
                internalMap.Add(key, value);
            }
        }

        public bool Remove(Key key)
        {
            LinkedList<Value> list;
            if (listMap.TryGetValue(key, out list) && list.Count > 0)
            {
                list.RemoveLast();
                return true;
            }
            return internalMap.Remove(key);
        }

        public bool TryGetValue(Key key, out Value value)
        {
            return internalMap.TryGetValue(key, out value);
        }

        public int ValuesCount(Key key)
        {
            LinkedList<Value> list;
            if (listMap.TryGetValue(key, out list))
            {
                return list.Count + 1;                
            }
            return internalMap.ContainsKey(key) ? 1 : 0;            
        }

        public ICollection<Value> ValuesByKey(Key key)
        {
            return ValuesByKey<Value>(key); 
        }
        
        public ICollection<T> ValuesByKey<T>(Key key) where T : Value
        {
            ICollection<T> list = new LinkedList<T>();
            Value firstValue;
            if (internalMap.TryGetValue(key, out firstValue))
            {
                list.Add((T)firstValue);                
            }
            LinkedList<Value> internalList;
            if (listMap.TryGetValue(key, out internalList) && internalList.Count > 0)
            {
                foreach (Value value in internalList)
                {
                    list.Add((T)value);
                }
            }
            return list;
        }
        

        public Value this[Key key]
        {
            get { return internalMap[key]; }
            set { internalMap[key] = value; }
        }

        public ICollection<Key> Keys 
        {
            get { return internalMap.Keys; }
        }
        
        public ICollection<Value> Values 
        {
            get { return internalMap.Values; }
        }
    }
}