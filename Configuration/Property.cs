// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Utils.Types;
using NetExtender.Configuration.Interfaces;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Maps;

namespace NetExtender.Configuration
{
    public partial class Config
    {
        private static readonly IIndexDictionary<IConfig, IIndexMap<String, IReadOnlyConfigPropertyBase>> Properties =
            new IndexDictionary<IConfig, IIndexMap<String, IReadOnlyConfigPropertyBase>>();

        private static Boolean IsLinked(IReadOnlyConfigPropertyBase property)
        {
            return Properties.TryGetValue(property.Config, out IIndexMap<String, IReadOnlyConfigPropertyBase> map) && map.ContainsValue(property);
        }

        private static void ThrowIfPropertyNotLinked(IReadOnlyConfigPropertyBase property)
        {
            if (!IsLinked(property))
            {
                throw new ArgumentException("Property not linked to config");
            }
        }
        
        public Boolean SetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            ThrowIfPropertyNotLinked(property);
            return SetValue(property.Key, value, property.CryptKey, property.Sections);
        }

        public T GetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            ThrowIfPropertyNotLinked(property);
            return GetValue(property.Key, property.DefaultValue, property.CryptKey, property.Converter, property.Sections);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            ThrowIfPropertyNotLinked(property);
            return GetOrSetValue(property, property.DefaultValue);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            ThrowIfPropertyNotLinked(property);
            return GetOrSetValue(property.Key, value, property.CryptKey, property.Converter, property.Sections);
        }

        public Boolean KeyExist(IReadOnlyConfigPropertyBase property)
        {
            ThrowIfPropertyNotLinked(property);
            return KeyExist(property.Key, property.Sections);
        }

        public Boolean RemoveValue(IReadOnlyConfigPropertyBase property)
        {
            ThrowIfPropertyNotLinked(property);
            return RemoveValue(property.Key, property.Sections);
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
            return GetProperty(key, value, validate, crypt, options, ConvertUtils.TryConvert, sections);
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

        private static IIndexMap<String, IReadOnlyConfigPropertyBase> NewPropertyMap()
        {
            return new IndexMap<String, IReadOnlyConfigPropertyBase>();
        }

        private static IReadOnlyConfigPropertyBase GetOrAddProperty(IReadOnlyConfigPropertyBase property)
        {
            return Properties.GetOrAdd(property.Config, NewPropertyMap)
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

        [CanBeNull]
        public IEnumerable<IReadOnlyConfigPropertyBase> GetProperties()
        {
            return Properties.TryGetValue(this, out IIndexMap<String, IReadOnlyConfigPropertyBase> dictionary) ? dictionary.Values : null;
        }
        
        private static void ReadProperty(IReadOnlyConfigPropertyBase property)
        {
            property.Read();
        }

        private static void SaveProperty(IReadOnlyConfigPropertyBase property)
        {
            (property as IConfigPropertyBase)?.Save();
        }

        private static void ResetProperty(IReadOnlyConfigPropertyBase property)
        {
            (property as IConfigPropertyBase)?.Reset();
        }

        private static void ClearProperty(IReadOnlyConfigPropertyBase property)
        {
            (property as IConfigPropertyBase)?.Dispose();
        }
        
        public static void RemoveProperty(IReadOnlyConfigPropertyBase property)
        {
            if (!Properties.TryGetValue(property.Config, out IIndexMap<String, IReadOnlyConfigPropertyBase> dictionary))
            {
                return;
            }

            dictionary.Remove(property.Path);
            ClearProperty(property);
        }
        
        private void ForEachProperty(Action<IReadOnlyConfigPropertyBase> action)
        {
            if (Properties.TryGetValue(this, out IIndexMap<String, IReadOnlyConfigPropertyBase> dictionary))
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