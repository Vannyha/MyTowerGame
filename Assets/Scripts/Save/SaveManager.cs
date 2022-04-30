using System;
using System.Collections.Generic;
using Context;
using GameScripts;
using Helpers.SlowUpdate;
using JsonFs;
using UnityEngine;

namespace Save
{
    public class SaveManager : MonoBehaviour, ISaveManager
    {
        private IJsonFsManager jsonFsManager;
        
        private const float SerializationCountdown = 30f;
        private const float SerializationCallBias = 1f;

        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
        private readonly Dictionary<string, object> _changes = new Dictionary<string, object>();
        private List<string> _cachedKeys = new List<string>();
        private SlowUpdateProc _serializationKeysFixedTimeProcess;
        private SlowUpdateProc _serializationPrefsFixedTimeProcess;

        private bool _forceSave;
        private string _pullKey = "save";
        
        public void SetupBeans(GameContext context)
        {
            jsonFsManager = context.JsonFsManagerInstance;
            _forceSave = false;
            PullKeys();
            PullPrefs();
            _serializationKeysFixedTimeProcess = new SlowUpdateProc(PushDataParallelKeys, SerializationCountdown);
            _serializationPrefsFixedTimeProcess = new SlowUpdateProc(PushDataParallelPrefs, SerializationCountdown + SerializationCallBias);
        }

        public void SaveValue<T>(string key, T value)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key] = value;
            }
            else
            {
                _cache.Add(key, value);
            }

            if (!_changes.ContainsKey(key))
            {
                _changes.Add(key, value);
            }
            else
            {
                _changes[key] = value;
            }
        }

        public T LoadValue<T>(string key, T defaultValue)
        {
            object value = null;

            if (_cache.ContainsKey(key))
            {
                value = _cache[key];

                if (defaultValue != null && value != null && value.GetType() != defaultValue.GetType())
                {
                    return defaultValue;
                }
                
                if (value is IConvertible)
                {
                    IConvertible convertible = value as IConvertible;
                    return ConvertTo<T>(convertible);
                }
            }

            if (value == null)
            {
                SaveValue(key, defaultValue);
                return defaultValue;
            }

            return (T) value;
        }

        public void EmergencySave()
        {
            SerializeKeysInstantly();
            SerializePrefsInstantly();
            _forceSave = true;
        }

        private void Update()
        {
            if (_forceSave)
            {
                _forceSave = false;
                InnerSave();
            }
            else
            {
                _serializationKeysFixedTimeProcess.ProceedOnFixedUpdate();
                _serializationPrefsFixedTimeProcess.ProceedOnFixedUpdate();
            }
        }

        private void InnerSave()
        {
            PlayerPrefs.Save();
        }
        
        private void PushDataParallelKeys()
        {
            _cachedKeys = DuplicateKeys(_cache.Keys);
            
            SerializeKeysParallel(_cachedKeys);
        }
        
        private void PushDataParallelPrefs()
        {
            Dictionary<string, object> cashedChanges = DuplicateChanges(_changes);

            SerializePrefsParallel(cashedChanges);
        }

        private void SerializeKeysParallel(List<string> keysList)
        {
            string serializedKeysList = jsonFsManager.Serialize(keysList);
            
            PlayerPrefs.SetString(_pullKey, serializedKeysList);
            PlayerPrefs.Save();
        }

        private void SerializeKeysInstantly()
        {
            List<string> keysList = DuplicateKeys(_cache.Keys);

            string serializedKeysList = jsonFsManager.Serialize(keysList);

            PlayerPrefs.SetString(_pullKey, serializedKeysList);
            PlayerPrefs.Save();
        }

        private void SerializePrefsParallel(object incObject)
        {
            Dictionary<string, object> incomingValues = (Dictionary<string, object>) incObject;
            Dictionary<string, string> serializedValues = SerializePrefs(incomingValues);
            
            foreach (KeyValuePair<string, string> pair in serializedValues)
            {
                PlayerPrefs.SetString(pair.Key, pair.Value);
            }

            PlayerPrefs.Save();

            Dictionary<string, object> duplicatedChanges = DuplicateChanges(_changes);
            foreach (KeyValuePair<string, object> pair in incomingValues)
            {
                if (duplicatedChanges.ContainsKey(pair.Key) && (null == pair.Value &&
                                                                null == duplicatedChanges[pair.Key] ||
                                                                duplicatedChanges[pair.Key].Equals(pair.Value)
                    ))
                {
                    _changes.Remove(pair.Key);
                }
            }
        }

        private void SerializePrefsInstantly()
        {
            Dictionary<string, string> serializedValues = SerializePrefs(_changes);

            foreach (KeyValuePair<string, string> pair in serializedValues)
            {
                PlayerPrefs.SetString(pair.Key, pair.Value);
            }

            PlayerPrefs.Save();

            _changes.Clear();
        }

        private Dictionary<string, string> SerializePrefs(Dictionary<string, object> changes)
        {
            Dictionary<string, string> serializedValues = new Dictionary<string, string>();

            foreach (KeyValuePair<string, object> pair in changes)
            {
                serializedValues.Add(pair.Key, jsonFsManager.Serialize(pair.Value));
            }

            return serializedValues;
        }

        private Dictionary<string, object> DuplicateChanges(Dictionary<string, object> original)
        {
            Dictionary<string, object> duplicate = new Dictionary<string, object>();

            if (original == null || original.Count == 0)
            {
                return duplicate;
            }

            foreach (KeyValuePair<string, object> pair in original)
            {
                duplicate.Add(pair.Key, pair.Value);
            }

            return duplicate;
        }

        private List<string> DuplicateKeys(Dictionary<string, object>.KeyCollection incKeys)
        {
            List<string> duplicate = new List<string>();

            foreach (string key in incKeys)
            {
                duplicate.Add(key);
            }

            return duplicate;
        }

        private void PullKeys()
        {
            string serializedVariable = PlayerPrefs.GetString(_pullKey);

            List<string> deserializedVariable = null;
            if (!serializedVariable.Equals(String.Empty))
            {
                deserializedVariable = jsonFsManager.Deserialize<List<string>>(serializedVariable);
            }

            if (deserializedVariable == null)
            {
                return;
            }

            for (int i = 0; i < deserializedVariable.Count; i++)
            {
                string key = deserializedVariable[i];
                if (!_cache.ContainsKey(key))
                    _cache.Add(key, null);
            }
        }

        private void PullPrefs()
        {
            List<string> keys = new List<string>();

            foreach (string key in _cache.Keys)
            {
                keys.Add(key);
            }

            foreach (string key in keys)
            {
                object value;
                value = PullObject(key);
                _cache[key] = value;
            }
        }

        private object PullObject(string key)
        {
            string serializedVariable = PlayerPrefs.GetString(key);
            object deserializedVariable = null;

            if (serializedVariable.Equals(String.Empty))
            {
                return null;
            }

            try
            {
                deserializedVariable = jsonFsManager.Deserialize<object>(serializedVariable);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return deserializedVariable;
        }

        private T ConvertTo<T>(IConvertible obj)
        {
            Type t = typeof(T);
            Type u = Nullable.GetUnderlyingType(t);

            if (u != null)
            {
                if (obj == null)
                {
                    return default;
                }

                return (T) Convert.ChangeType(obj, u);
            }

            return (T) Convert.ChangeType(obj, t);
        }
        
        private void OnApplicationQuit()
        {
            SilentForceSave();
        }

        private void SilentForceSave()
        {
            try
            {
                ForceSaving();
            }
            catch (Exception e)
            {
                //Shouldn't be break
            }
        }

        private void ForceSaving()
        {
            EmergencySave();
            _forceSave = false;
            InnerSave();
        }
    }
}