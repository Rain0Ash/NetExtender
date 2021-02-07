// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Interfaces.Property.Common;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Configuration.Interfaces.Property
{
    public interface IReadOnlyPropertyConfig : IReadOnlyConfig, IReadOnlyPropertyConfigBase
    {
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, params String[] sections)
        {
            return GetProperty(key, default(T), sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, params String[] sections)
        {
            return GetProperty(key, value, null, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptAction.Decrypt, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt, DefaultOptions, sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), options, sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt, options, ConvertUtils.TryConvert, sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), options, sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), options, sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, ConfigPropertyOptions options, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), options, sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, CryptAction crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), options, converter, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections);
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), options, converter, sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), options, converter, sections);
        }
        
        public IReadOnlyConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, Func<String, String[], ICryptKey> crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key, sections), options, converter, sections);
        }

        public IEnumerable<IReadOnlyConfigPropertyBase> GetProperties();
    }
}