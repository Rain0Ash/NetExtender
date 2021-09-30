// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Property.Interfaces;
using NetExtender.Configuration.Property.Interfaces.Common;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration
{
    internal static class ConfigurationPropertyObserver
    {
        private static readonly IIndexDictionary<IPropertyConfigBase, IIndexMap<String, IReadOnlyConfigPropertyBase>> Properties =
            new IndexDictionary<IPropertyConfigBase, IIndexMap<String, IReadOnlyConfigPropertyBase>>();
        
        public static Boolean IsLinked(IReadOnlyConfigPropertyBase property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            lock (Properties)
            {
                return Properties.TryGetValue(property.Config, out IIndexMap<String, IReadOnlyConfigPropertyBase>? map) && map.ContainsValue(property);
            }
        }

        public static Boolean IsLinkedTo(IPropertyConfigBase config, IReadOnlyConfigPropertyBase property)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            lock (Properties)
            {
                return Properties.TryGetValue(config, out IIndexMap<String, IReadOnlyConfigPropertyBase>? map) && map.ContainsValue(property);
            }
        }
        
        public static void ThrowIfPropertyNotLinked(IReadOnlyConfigPropertyBase property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (!IsLinked(property))
            {
                throw new InvalidOperationException("Property not linked to config");
            }
        }
        
        public static void ThrowIfPropertyNotLinkedTo(IPropertyConfigBase config, IReadOnlyConfigPropertyBase property)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (!IsLinkedTo(config, property))
            {
                throw new InvalidOperationException("Property not linked to this config");
            }
        }
        
        private static IIndexMap<String, IReadOnlyConfigPropertyBase> CreatePropertyMap()
        {
            return new IndexMap<String, IReadOnlyConfigPropertyBase>();
        }
        
        public static IReadOnlyConfigPropertyBase GetOrAddProperty(IReadOnlyConfigPropertyBase property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            IPropertyConfigBase config = property.Config;

            if (config is null)
            {
                throw new ArgumentException(@"Cannot add property without config", nameof(property));
            }

            String path = property.Path;

            if (path is null)
            {
                throw new ArgumentException(@"Cannot add property without path", nameof(property));
            }
            
            lock (Properties)
            {
                return Properties.GetOrAdd(config, CreatePropertyMap).GetOrAdd(path, property);
            }
        }

        public static IReadOnlyConfigPropertyBase GetOrAddProperty(IPropertyConfigBase config, String path, Func<IReadOnlyConfigPropertyBase> factory)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            lock (Properties)
            {
                return Properties.GetOrAdd(config, CreatePropertyMap).GetOrAdd(path, factory);
            }
        }

        public static Boolean Contains(IPropertyConfigBase config, String path)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            lock (Properties)
            {
                return Properties.TryGetValue(config, out IIndexMap<String, IReadOnlyConfigPropertyBase>? properties) && properties.ContainsKey(path);
            }
        }
        
        public static IEnumerable<IReadOnlyConfigPropertyBase>? GetProperties(IPropertyConfigBase config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            lock (Properties)
            {
                return Properties.TryGetValue(config, out IIndexMap<String, IReadOnlyConfigPropertyBase>? dictionary) ? dictionary?.Values : null;
            }
        }
        
        public static Boolean RemoveProperty(IReadOnlyConfigPropertyBase property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Config is null)
            {
                throw new ArgumentException(@"Cannot remove property without config", nameof(property));
            }
            
            lock (Properties)
            {
                return Properties.TryGetValue(property.Config, out IIndexMap<String, IReadOnlyConfigPropertyBase>? dictionary) && dictionary.Remove(property.Path);
            }
        }

        public static Boolean RemoveProperty(IPropertyConfigBase config, IReadOnlyConfigPropertyBase property)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            ThrowIfPropertyNotLinkedTo(config, property);
            return RemoveProperty(property);
        }

        public static void ForEachProperty(IPropertyConfigBase config, Action<IReadOnlyConfigPropertyBase> action)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            lock (Properties)
            {
                if (!Properties.TryGetValue(config, out IIndexMap<String, IReadOnlyConfigPropertyBase>? dictionary))
                {
                    return;
                }

                foreach (IReadOnlyConfigPropertyBase value in dictionary.Values)
                {
                    action(value);
                }
            }
        }
        
        public static Boolean ClearProperties(IPropertyConfigBase config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            lock (Properties)
            {
                return Properties.Remove(config);
            }
        }
    }
}