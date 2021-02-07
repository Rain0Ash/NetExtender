// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NetExtender.Configuration.Common;
using NetExtender.Utils.Types;
using NetExtender.Configuration.Interfaces.Property;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration
{
    public class PropertyConfig : Config, IPropertyConfig
    {
        public new static IPropertyConfig Create([NotNull] IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new PropertyConfig(behavior);
        }
        
        public new static IPropertyConfig Create(String path)
        {
            return new PropertyConfig(ConfigBehavior.Create(path));
        }
        
        public new static IPropertyConfig Create(String path, ConfigType type)
        {
            return new PropertyConfig(ConfigBehavior.Create(path, type));
        }
        
        public new static IPropertyConfig Create(String path, ConfigOptions options)
        {
            return new PropertyConfig(ConfigBehavior.Create(path, options));
        }

        public new static IPropertyConfig Create(String path, ConfigType type, ConfigOptions options)
        {
            return new PropertyConfig(ConfigBehavior.Create(path, type, options));
        }
        
        public PropertyConfig([NotNull] IConfigBehavior behavior)
            : base(behavior)
        {
        }

        public PropertyConfig(ConfigType type = DefaultConfigType, ConfigOptions options = DefaultConfigOptions)
            : base(type, options)
        {
        }

        public PropertyConfig(String path, ConfigType type = DefaultConfigType, ConfigOptions options = DefaultConfigOptions)
            : base(path, type, options)
        {
        }

        public Boolean SetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
            return SetValue(property.Key, value, property.CryptKey, property.Sections);
        }

        public T GetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
            return GetValue(property.Key, property.DefaultValue, property.CryptKey, property.Converter, property.Sections);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
            return GetOrSetValue(property, property.DefaultValue);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
            return GetOrSetValue(property.Key, value, property.CryptKey, property.Converter, property.Sections);
        }

        public Boolean KeyExist(IReadOnlyConfigPropertyBase property)
        {
            ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
            return KeyExist(property.Key, property.Sections);
        }

        public Boolean RemoveValue(IReadOnlyConfigPropertyBase property)
        {
            ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
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
        
        public virtual IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
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

        private static IConfigProperty<T> GetOrAddProperty<T>(IConfigPropertyBase property)
        {
            if (ConfigPropertyObserver.GetOrAddProperty(property) is IConfigProperty<T> result)
            {
                return result;
            }
            
            throw new ArgumentException(@$"Config already contains another property with same path '{property.Path}' and different generic type.", nameof(property));
        }

        [CanBeNull]
        public IEnumerable<IReadOnlyConfigPropertyBase> GetProperties()
        {
            return ConfigPropertyObserver.GetProperties(this);
        }
        
        private static void ReadProperty(IReadOnlyConfigPropertyBase property)
        {
            property?.Read();
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
        
        public void RemoveProperty(IReadOnlyConfigPropertyBase property)
        {
            ConfigPropertyObserver.RemoveProperty(this, property);
            ClearProperty(property);
        }
        
        private void ForEachProperty(Action<IReadOnlyConfigPropertyBase> action)
        {
            ConfigPropertyObserver.ForEachProperty(this, action);
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
            ConfigPropertyObserver.ClearProperties(this);
        }

        protected override void DisposeInternal(Boolean disposing)
        {
            base.DisposeInternal(disposing);
            
            ClearProperties();
        }
    }
}