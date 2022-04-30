using Context;

namespace GameScripts
{
    public interface ISaveManager : IBean
    {
        void SaveValue<T>(string key, T value);
        T LoadValue<T>(string key, T defaultValue);
        void EmergencySave();
    }
}