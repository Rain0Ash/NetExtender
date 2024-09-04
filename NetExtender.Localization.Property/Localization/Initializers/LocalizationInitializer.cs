// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Singletons;
using NetExtender.Types.Singletons.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Property.Localization.Initializers
{
    public abstract class LocalizationAutoInitializer : LocalizationInitializerAbstraction
    {
        public sealed override event PropertyChangedEventHandler? PropertyChanged;
        protected IndexDictionary<ILocalizationPropertyInfo, PropertyInfo> Storage { get; } = new IndexDictionary<ILocalizationPropertyInfo, PropertyInfo>();
        
        protected LocalizationAutoInitializer()
        {
            foreach (PropertyInfo property in GetType().GetProperties().Where(IsProperty))
            {
                if (property.GetValue(this) is not ILocalizationPropertyInfo info)
                {
                    continue;
                }

                if (!Storage.TryAdd(info, property))
                {
                    continue;
                }

                info.PropertyChanged += OnPropertyChanged;
            }
        }
        
        private static Boolean IsProperty(PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.PropertyType.HasInterface(typeof(ILocalizationPropertyInfo));
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ILocalizationPropertyInfo.Current))
            {
                return;
            }

            if (sender is not ILocalizationPropertyInfo info || !Storage.TryGetValue(info, out PropertyInfo? property))
            {
                return;
            }
            
            PropertyChanged?.Invoke(this, new PropertyChanged(property.Name));
        }
    }

    public abstract class LocalizationInitializer : LocalizationInitializerAbstraction
    {
        public sealed override event PropertyChangedEventHandler? PropertyChanged;
        protected IndexDictionary<ILocalizationPropertyInfo, String> Storage { get; } = new IndexDictionary<ILocalizationPropertyInfo, String>();

        protected internal void Subscribe(ILocalizationPropertyInfo value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Subscribe(value, value.Key ?? value.Path);
        }

        protected internal void Subscribe(ILocalizationPropertyInfo value, String name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Storage.Add(value, name);
            value.PropertyChanged += OnPropertyChanged;
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ILocalizationPropertyInfo.Current))
            {
                return;
            }

            if (sender is not ILocalizationPropertyInfo info || !Storage.TryGetValue(info, out String? name))
            {
                return;
            }
            
            PropertyChanged?.Invoke(this, new PropertyChanged(name));
        }
    }

    public abstract class LocalizationInitializerAbstraction : INotifyPropertyChanged
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }

    public abstract class LocalizationInitializerSingleton<T> : LocalizationInitializerAbstraction where T : LocalizationInitializerAbstraction, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }
    }

    public abstract class LocalizationManualInitializerSingleton<T> : LocalizationInitializer where T : LocalizationInitializer, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }
    }

    public abstract class LocalizationAutoInitializerSingleton<T> : LocalizationAutoInitializer where T : LocalizationAutoInitializer, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }
    }
}