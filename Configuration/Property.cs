// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces.Property;
using NetExtender.Configuration.Interfaces.Property.Common;
using NetExtender.Converters;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utils.Types;

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

        IReadOnlyConfigProperty<T> IReadOnlyPropertyConfig.GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return GetProperty(key, value, validate, crypt, options, converter, sections);
        }
        
        public virtual IConfigProperty<T> GetProperty<T>(String key, T value, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            IImmutableList<String> materialized = sections.AsIImmutableList();
            return GetOrAddProperty<T>(this, key, materialized, () => new ConfigProperty<T>(this, key, value, validate, crypt, options, converter, materialized));
        }

        private static IConfigProperty<T> GetOrAddProperty<T>([NotNull] IConfigPropertyBase property)
        {
            if (ConfigPropertyObserver.GetOrAddProperty(property) is IConfigProperty<T> result)
            {
                return result;
            }
            
            throw new ArgumentException(@$"Config already contains another property with same path '{property.Path}' and different generic type.", nameof(property));
        }
        
        private static IConfigProperty<T> GetOrAddProperty<T>([NotNull] IPropertyConfigBase config, [NotNull] String key, [NotNull] IEnumerable<String> sections, [NotNull] Func<IConfigPropertyBase> factory)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (sections is null)
            {
                throw new ArgumentNullException(nameof(sections));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            String path = ConfigPropertyBase.GetPath(key, sections);
            
            if (ConfigPropertyObserver.GetOrAddProperty(config, path, factory) is IConfigProperty<T> result)
            {
                return result;
            }
            
            throw new ArgumentException(@$"Config already contains another property with same path '{path}' and different generic type.", nameof(sections));
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