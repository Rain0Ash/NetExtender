// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Utilities.Core;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public abstract class LocalizationWindow : CenterWindow
    {
        public abstract WindowLocalizationAbstraction Localization { get; }
    }

    public abstract class WindowLocalizationAutoInitializer : WindowLocalizationAbstraction, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected IndexDictionary<ILocalizationPropertyInfo, PropertyInfo> Internal { get; } = new IndexDictionary<ILocalizationPropertyInfo, PropertyInfo>();
        
        protected WindowLocalizationAutoInitializer()
        {
            Initialize();
        }
        
        private static Boolean IsProperty(PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.PropertyType.HasInterface(typeof(ILocalizationPropertyInfo));
        }

        private void Initialize()
        {
            foreach (PropertyInfo property in GetType().GetProperties().Where(IsProperty))
            {
                if (property.GetValue(this) is not ILocalizationPropertyInfo info)
                {
                    continue;
                }

                Internal.Add(info, property);
                info.PropertyChanged += OnPropertyChanged;
            }
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ILocalizationPropertyInfo.Current))
            {
                return;
            }

            if (sender is not ILocalizationPropertyInfo info || !Internal.TryGetValue(info, out PropertyInfo? property))
            {
                return;
            }
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property.Name));
        }
    }

    public abstract class WindowLocalizationInitializer : WindowLocalizationAbstraction, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected IndexDictionary<ILocalizationPropertyInfo, String> Internal { get; } = new IndexDictionary<ILocalizationPropertyInfo, String>();

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

            Internal.Add(value, name);
            value.PropertyChanged += OnPropertyChanged;
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ILocalizationPropertyInfo.Current))
            {
                return;
            }

            if (sender is not ILocalizationPropertyInfo info || !Internal.TryGetValue(info, out String? name))
            {
                return;
            }
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public abstract class WindowLocalizationAbstraction
    {
    }

    public abstract class WindowLocalizationSingleton<TWindowLocalization> : WindowLocalizationAbstraction where TWindowLocalization : WindowLocalizationAbstraction, new()
    {
        public static TWindowLocalization Instance { get; } = new TWindowLocalization();
    }

    public abstract class WindowLocalizationInitializerSingleton<TWindowLocalization> : WindowLocalizationInitializer where TWindowLocalization : WindowLocalizationInitializer, new()
    {
        public static TWindowLocalization Instance { get; } = new TWindowLocalization();
    }

    public abstract class WindowLocalizationAutoInitializerSingleton<TWindowLocalization> : WindowLocalizationAutoInitializer where TWindowLocalization : WindowLocalizationAutoInitializer, new()
    {
        public static TWindowLocalization Instance { get; } = new TWindowLocalization();
    }
}