// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Config.Interfaces
{
    public interface IPropertyConfig : IConfig, IReadOnlyPropertyConfig
    {
        public void SetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            SetValue(property.Key, value, property.CryptKey, property.Sections);
        }
        
        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            return GetOrSetValue(property, property.DefaultValue);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            return GetOrSetValue(property.Key, value, property.CryptKey, property.Converter, property.Sections);
        }
        
        public void RemoveValue(IConfigPropertyBase property)
        {
            RemoveValue(property.Key, property.Sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, params String[] sections)
        {
            return GetProperty(key, default(T), sections);
        }

        public new IConfigProperty<T> GetProperty<T>(String key, T value, params String[] sections)
        {
            return GetProperty(key, value, null, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptAction.Decrypt, sections);
        }

        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), sections);
        }

        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt, DefaultOptions, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), options, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt, options, ConvertUtils.TryConvert, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), options, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), options, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), options, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), options, converter, sections);
        }

        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections);
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), options, converter, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), options, converter, sections);
        }
        
        public new IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), options, converter, sections);
        }

        public void ReadProperties();
        public void SaveProperties();
        public void ResetProperties();
        public void ClearProperties();
    }
}