// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Utilities.Core;

namespace NetExtender.Localization.Property.Localization.Initializers
{
    public abstract class LocalizationAutoInitializer : LocalizationInitializerAbstraction
    {
        public sealed override event PropertyChangedEventHandler? PropertyChanged;
        protected IndexDictionary<ILocalizationPropertyInfo, PropertyInfo> Store { get; } = new IndexDictionary<ILocalizationPropertyInfo, PropertyInfo>();
        
        protected LocalizationAutoInitializer()
        {
            foreach (PropertyInfo property in GetType().GetProperties().Where(IsProperty))
            {
                if (property.GetValue(this) is not ILocalizationPropertyInfo info)
                {
                    continue;
                }

                Store.Add(info, property);
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

            if (sender is not ILocalizationPropertyInfo info || !Store.TryGetValue(info, out PropertyInfo? property))
            {
                return;
            }
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property.Name));
        }
    }

    public abstract class LocalizationInitializer : LocalizationInitializerAbstraction
    {
        public sealed override event PropertyChangedEventHandler? PropertyChanged;
        protected IndexDictionary<ILocalizationPropertyInfo, String> Store { get; } = new IndexDictionary<ILocalizationPropertyInfo, String>();

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

            Store.Add(value, name);
            value.PropertyChanged += OnPropertyChanged;
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ILocalizationPropertyInfo.Current))
            {
                return;
            }

            if (sender is not ILocalizationPropertyInfo info || !Store.TryGetValue(info, out String? name))
            {
                return;
            }
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public abstract class LocalizationInitializerAbstraction : INotifyPropertyChanged
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }

    public abstract class LocalizationInitializerSingleton<T> : LocalizationInitializerAbstraction where T : LocalizationInitializerAbstraction, new()
    {
        private static Lazy<T> Internal { get; } = new Lazy<T>(() => new T(), true);

        public static T Instance
        {
            get
            {
                return Internal.Value;
            }
        }
    }

    public abstract class LocalizationManualInitializerSingleton<T> : LocalizationInitializer where T : LocalizationInitializer, new()
    {
        private static Lazy<T> Internal { get; } = new Lazy<T>(() => new T(), true);

        public static T Instance
        {
            get
            {
                return Internal.Value;
            }
        }
    }

    public abstract class LocalizationAutoInitializerSingleton<T> : LocalizationAutoInitializer where T : LocalizationAutoInitializer, new()
    {
        private static Lazy<T> Internal { get; } = new Lazy<T>(() => new T(), true);

        public static T Instance
        {
            get
            {
                return Internal.Value;
            }
        }
    }
}