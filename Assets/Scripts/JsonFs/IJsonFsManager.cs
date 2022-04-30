using Context;

namespace JsonFs
{
    public interface IJsonFsManager : IBean
    {
        string Serialize(object obj);
        object Deserialize(string str);
        T Deserialize<T>(string str);
    }
}