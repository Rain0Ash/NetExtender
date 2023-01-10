using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using NetExtender.Localization.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Localization.Types.Flags;

namespace NetExtender.WindowsPresentation.ReactiveUI.Types.Models
{
    public class SettingsReactiveViewModelInitializer
    {
        public virtual LocalizationIdentifier InitializeIdentifier(ILocalizationConfigInfo config, IReadOnlyCollection<LocalizationFlagBitmapSourceEntry> identifiers)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (identifiers is null)
            {
                throw new ArgumentNullException(nameof(identifiers));
            }

            LocalizationIdentifier? Current()
            {
                return identifiers.FirstOrDefault(item => item.Identifier == config.Localization)?.Identifier;
            }

            LocalizationIdentifier? System()
            {
                return !config.WithoutSystem ? identifiers.FirstOrDefault(item => item.Identifier == config.System)?.Identifier : null;
            }

            return Current() ?? System() ?? config.Default;
        }

        public virtual ObservableCollection<LocalizationFlagBitmapSourceEntry> InitializeIdentifiers(IEnumerable<LocalizationIdentifier> identifiers, Handler handler)
        {
            if (identifiers is null)
            {
                throw new ArgumentNullException(nameof(identifiers));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            ObservableCollection<LocalizationFlagBitmapSourceEntry> languages = identifiers.Normalize().Select(LocalizationFlagBitmapSourceEntry.Convert).ToObservableCollection();
            return Subscribe(languages, handler);
        }
        
        protected virtual T Subscribe<T>(T value, Handler handler) where T : INotifyPropertyChanged, INotifyCollectionChanged
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            value.PropertyChanged += handler.Property;
            value.CollectionChanged += handler.Collection;
            return value;
        }
        
        public class Handler
        {
            public static implicit operator Handler((PropertyChangedEventHandler Property, NotifyCollectionChangedEventHandler Collection) value)
            {
                return new Handler(value.Property, value.Collection);
            }
            
            public PropertyChangedEventHandler Property { get; }
            public NotifyCollectionChangedEventHandler Collection { get; }

            private Handler(PropertyChangedEventHandler property, NotifyCollectionChangedEventHandler collection)
            {
                Property = property ?? throw new ArgumentNullException(nameof(property));
                Collection = collection ?? throw new ArgumentNullException(nameof(collection));
            }
        }
    }
}