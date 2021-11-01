// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Property;
using NetExtender.Configuration.Property.Interfaces;
using NetExtender.Configuration.Property.Interfaces.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration
{
    public class PropertyConfig : Config, IPropertyConfig, IReadOnlyPropertyConfig
    {
        public new static IPropertyConfig Create(IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new PropertyConfig(behavior);
        }
        
        public PropertyConfig(IConfigBehavior behavior)
            : base(behavior)
        {
        }

        public Boolean SetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            ConfigurationPropertyObserver.ThrowIfPropertyNotLinked(property);
            return SetValue(property.Key, value, property.CryptKey, property.Sections);
        }

        public T? GetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            ConfigurationPropertyObserver.ThrowIfPropertyNotLinked(property);
            return GetValue(property.Key, property.Alternate, property.CryptKey, property.Converter, property.Sections);
        }

        public T? GetOrSetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            ConfigurationPropertyObserver.ThrowIfPropertyNotLinked(property);
            return GetOrSetValue(property, property.Alternate);
        }

        public T? GetOrSetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            ConfigurationPropertyObserver.ThrowIfPropertyNotLinked(property);
            return GetOrSetValue(property.Key, value, property.CryptKey, property.Converter, property.Sections);
        }

        public Boolean KeyExist(IReadOnlyConfigPropertyBase property)
        {
            ConfigurationPropertyObserver.ThrowIfPropertyNotLinked(property);
            return KeyExist(property.Key, property.Sections);
        }

        public Boolean RemoveValue(IReadOnlyConfigPropertyBase property)
        {
            ConfigurationPropertyObserver.ThrowIfPropertyNotLinked(property);
            return RemoveValue(property.Key, property.Sections);
        }

        IReadOnlyConfigProperty<T> IReadOnlyPropertyConfig.GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, ICryptKey? crypt, ConfigPropertyOptions options, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt, options, converter, sections);
        }
        
        public virtual IConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, ICryptKey? crypt, ConfigPropertyOptions options, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            IImmutableList<String> materialized = sections.AsIImmutableList();
            return GetOrAddProperty<T>(this, key, materialized, () => new ConfigProperty<T>(this, key, value, validate, crypt, options, converter, materialized));
        }

        private static IConfigProperty<T> GetOrAddProperty<T>(IConfigPropertyBase property)
        {
            if (ConfigurationPropertyObserver.GetOrAddProperty(property) is IConfigProperty<T> result)
            {
                return result;
            }
            
            throw new ArgumentException(@$"Config already contains another property with same path '{property.Path}' and different generic type.", nameof(property));
        }
        
        private static IConfigProperty<T> GetOrAddProperty<T>(IPropertyConfigBase config, String? key, IEnumerable<String> sections, Func<IConfigPropertyBase> factory)
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
            
            if (ConfigurationPropertyObserver.GetOrAddProperty(config, path, factory) is IConfigProperty<T> result)
            {
                return result;
            }
            
            throw new ArgumentException(@$"Config already contains another property with same path '{path}' and different generic type.", nameof(sections));
        }

        public IEnumerable<IReadOnlyConfigPropertyBase>? GetProperties()
        {
            return ConfigurationPropertyObserver.GetProperties(this);
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
            ConfigurationPropertyObserver.RemoveProperty(this, property);
            ClearProperty(property);
        }
        
        private void ForEachProperty(Action<IReadOnlyConfigPropertyBase> action)
        {
            ConfigurationPropertyObserver.ForEachProperty(this, action);
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
            ConfigurationPropertyObserver.ClearProperties(this);
        }

        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);
            
            ClearProperties();
        }
    }
}