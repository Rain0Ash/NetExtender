// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Utils.Types;
using NetExtender.Config.Interfaces;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Dictionaries;

namespace NetExtender.Config
{
    public partial class Config
    {
        public void SetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            SetValue(property.Key, value, property.CryptKey, property.Sections);
        }

        public T GetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            return GetValue(property.Key, property.DefaultValue, property.CryptKey, property.Converter, property.Sections);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            return GetOrSetValue(property, property.DefaultValue);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            return GetOrSetValue(property.Key, value, property.CryptKey, property.Converter, property.Sections);
        }

        public Boolean KeyExist(IConfigPropertyBase property)
        {
            return KeyExist(property.Key, property.Sections);
        }

        public void RemoveValue(IConfigPropertyBase property)
        {
            RemoveValue(property.Key, property.Sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, params String[] sections)
        {
            return GetProperty(key, default(T), sections);
        }

        public IConfigProperty<T> GetProperty<T>(String key, T value, params String[] sections)
        {
            return GetProperty(key, value, null, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptAction.Decrypt, sections);
        }

        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), sections);
        }

        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt, DefaultOptions, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), options, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt, options, Utils.Types.ConvertUtils.TryConvert, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), options, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), options, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), options, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), options, converter, sections);
        }

        IReadOnlyConfigProperty<T> IReadOnlyPropertyConfig.GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt, options, converter, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetOrAddProperty<T>(new ConfigProperty<T>(this, key, value, validate, crypt, options, converter, sections));
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), options, converter, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), options, converter, sections);
        }
        
        public IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), options, converter, sections);
        }

        private static IConfigPropertyBase GetOrAddProperty(IConfigPropertyBase property)
        {
            return Properties.GetOrAdd(property.Config, new IndexDictionary<String, IConfigPropertyBase>())
                .GetOrAdd(property.Path, property);
        }

        private static IConfigProperty<T> GetOrAddProperty<T>(IConfigPropertyBase property)
        {
            if (GetOrAddProperty(property) is IConfigProperty<T> result)
            {
                return result;
            }
            
            throw new ArgumentException(@$"Config already contains another property with same path '{property.Path}' and different generic type.", nameof(property));
        }

        public IEnumerable<IConfigPropertyBase> GetProperties()
        {
            return Properties.TryGetValue(this, out IndexDictionary<String, IConfigPropertyBase> dictionary) ? dictionary.Values : null;
        }
        
        private static void ReadProperty(IConfigPropertyBase property)
        {
            property.Read();
        }

        private static void SaveProperty(IConfigPropertyBase property)
        {
            property.Save();
        }

        private static void ResetProperty(IConfigPropertyBase property)
        {
            property.Reset();
        }

        private static IConfigPropertyBase CopyProperty(IConfigPropertyBase property)
        {
            return property.DeepCopy();
        }
        
        private static void ClearProperty(IConfigPropertyBase property)
        {
            property.Dispose(true);
        }
        
        public static void RemoveProperty(IConfigPropertyBase property)
        {
            if (!Properties.TryGetValue(property.Config, out IndexDictionary<String, IConfigPropertyBase> dictionary))
            {
                return;
            }

            dictionary.Remove(property.Path);
            ClearProperty(property);
        }
        
        private void ForEachProperty(Action<IConfigPropertyBase> action)
        {
            if (Properties.TryGetValue(this, out IndexDictionary<String, IConfigPropertyBase> dictionary))
            {
                dictionary.Values.ToList().ForEach(action);
            }
        }
        
        public void ReadProperties()
        {
            ForEachProperty(ReadProperty);
        }

        public void SaveProperties()
        {
            ForEachProperty(SaveProperty);
        }

        public void ResetProperties()
        {
            ForEachProperty(ResetProperty);
        }

        public void ClearProperties()
        {
            ForEachProperty(ClearProperty);
            Properties.TryGetValue(this)?.Clear();
        }
    }
}