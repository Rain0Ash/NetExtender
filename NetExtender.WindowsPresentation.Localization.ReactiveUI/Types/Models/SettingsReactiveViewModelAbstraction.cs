using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using DynamicData.Binding;
using NetExtender.Localization.Behavior.Settings;
using NetExtender.Localization.Events;
using NetExtender.Localization.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Types.Singletons;
using NetExtender.Types.Singletons.Interfaces;
using NetExtender.WindowsPresentation.Localization.Types.Collections;
using NetExtender.WindowsPresentation.Localization.Types.Flags;
using NetExtender.WindowsPresentation.Utilities;
using ReactiveUI;

namespace NetExtender.WindowsPresentation.ReactiveUI.Types.Models
{
    public abstract class SettingsReactiveViewModelAbstraction<TWindow, T> : SettingsReactiveViewModelAbstraction<T> where TWindow : Window where T : SettingsReactiveViewModelInitializer, new()
    {
        public TWindow Window { get; }

        protected SettingsReactiveViewModelAbstraction(SettingsLocalizationSynchronizedBehavior settings)
            : base(settings)
        {
            Window = WindowStoreUtilities<TWindow>.Require();
        }
    }
    
    public abstract class SettingsReactiveViewModelAbstraction<T> : SettingsReactiveViewModelAbstraction where T : SettingsReactiveViewModelInitializer, new()
    {
        public LocalizationCollection Languages { get; }
        
        public sealed override Int32 Count
        {
            get
            {
                return Languages.Count;
            }
        }

        private LocalizationIdentifier _identifier;
        public sealed override LocalizationIdentifier Identifier
        {
            get
            {
                return _identifier;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _identifier, value);
            }
        }

        protected SettingsReactiveViewModelAbstraction(SettingsLocalizationSynchronizedBehavior settings)
            : base(settings)
        {
            T initializer = new T();

            Languages = initializer.InitializeIdentifiers(Identifiers, Handler);
            Identifier = initializer.InitializeIdentifier(Config, Languages);

            Config.Changed += LocalizationChanged;
            this.WhenPropertyChanged(model => model.Identifier).Subscribe(model => Config.Localization = model.Value);
        }
        
        private void LocalizationChanged(Object? sender, LocalizationChangedEventArgs args)
        {
            LocalizationIdentifier? identifier = Languages.FirstOrDefault(item => item.Identifier == args.Value)?.Identifier;
            
            if (identifier is not null)
            {
                Identifier = identifier.Value;
            }
        }

        public sealed override IEnumerator<LocalizationFlagBitmapSourceEntry> GetEnumerator()
        {
            return Languages.GetEnumerator();
        }
        
        public sealed override LocalizationFlagBitmapSourceEntry this[Int32 index]
        {
            get
            {
                return Languages[index];
            }
        }
    }

    public abstract class SettingsReactiveViewModelAbstraction : ReactiveViewModel, IReadOnlyList<LocalizationFlagBitmapSourceEntry>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public SettingsLocalizationSynchronizedBehavior Settings { get; }

        protected ILocalizationConfigInfo Config
        {
            get
            {
                return Settings.Localization;
            }
        }

        protected IReadOnlySet<LocalizationIdentifier> Identifiers
        {
            get
            {
                return Settings.Identifiers;
            }
        }

        protected SettingsReactiveViewModelInitializer.Handler Handler
        {
            get
            {
                return (Property, Collection);
            }
        }

        public abstract Int32 Count { get; }
        public abstract LocalizationIdentifier Identifier { get; set; }
        
        protected SettingsReactiveViewModelAbstraction(SettingsLocalizationSynchronizedBehavior settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        private void Property(Object? sender, PropertyChangedEventArgs args)
        {
            this.RaisePropertyChanged(args.PropertyName);
        }

        private void Collection(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }
        
        public abstract IEnumerator<LocalizationFlagBitmapSourceEntry> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public abstract LocalizationFlagBitmapSourceEntry this[Int32 index] { get; }
    }
    
    public abstract class SettingsReactiveViewModelAbstractionSingleton<T, TInitializer> : SettingsReactiveViewModelAbstraction<TInitializer> where T : SettingsReactiveViewModelAbstraction<TInitializer>, new() where TInitializer : SettingsReactiveViewModelInitializer, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }

        protected SettingsReactiveViewModelAbstractionSingleton(SettingsLocalizationSynchronizedBehavior settings)
            : base(settings)
        {
        }
    }
    
    public abstract class SettingsReactiveViewModelAbstractionSingleton<T, TWindow, TInitializer> : SettingsReactiveViewModelAbstraction<TWindow, TInitializer> where T : SettingsReactiveViewModelAbstraction<TWindow, TInitializer>, new() where TWindow : Window where TInitializer : SettingsReactiveViewModelInitializer, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }

        protected SettingsReactiveViewModelAbstractionSingleton(SettingsLocalizationSynchronizedBehavior settings)
            : base(settings)
        {
        }
    }
}