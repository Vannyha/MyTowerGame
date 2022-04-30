using Context;
using FullSerializer;
using UnityEngine;

namespace JsonFs
{
    public class JsonFsManager : MonoBehaviour, IJsonFsManager
    {
        public void SetupBeans(GameContext context)
        {
        }
        
        public string Serialize(object obj)
        {
            fsSerializer serializer = new fsSerializer();
            fsData fsData;
            serializer.TrySerialize(obj, out fsData);
            return fsData.ToString();
        }

        public object Deserialize(string str)
        {
            return Deserialize<object>(str);
        }

        public T Deserialize<T>(string str)
        {
            fsData parsedData = fsJsonParser.Parse(str);
            fsSerializer serializer = new fsSerializer();
            T result = default(T);
            serializer.TryDeserialize(parsedData, ref result);
            return result; 
        }
    }
}