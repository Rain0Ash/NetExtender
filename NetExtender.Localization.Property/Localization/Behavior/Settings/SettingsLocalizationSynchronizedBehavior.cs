// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration;
using NetExtender.Configuration.Behavior.Settings;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Behavior.Settings
{
    public abstract class SettingsLocalizationSynchronizedBehavior<T> : SettingsLocalizationSynchronizedBehavior where T : SettingsLocalizationSynchronizedBehavior, new()
    {
        private static T Create()
        {
            T instance = new T();
            instance.Subscribe();
            return instance;
        }
        
        private static Lazy<T> Internal { get; } = new Lazy<T>(Create, true);

        public static T Instance
        {
            get
            {
                return Internal.Value;
            }
        }
        
        protected SettingsLocalizationSynchronizedBehavior()
        {
        }

        protected SettingsLocalizationSynchronizedBehavior(IComparer<LocalizationIdentifier>? comparer)
            : base(comparer)
        {
        }

        protected SettingsLocalizationSynchronizedBehavior(params LocalizationIdentifier[]? order)
            : base(order)
        {
        }

        protected SettingsLocalizationSynchronizedBehavior(params LocalizationIdentifiers[]? order)
            : base(order)
        {
        }

        protected SettingsLocalizationSynchronizedBehavior(IEnumerable<LocalizationIdentifier>? order)
            : base(order)
        {
        }

        protected SettingsLocalizationSynchronizedBehavior(IEnumerable<LocalizationIdentifiers>? order)
            : base(order)
        {
        }

        protected SettingsLocalizationSynchronizedBehavior(IComparer<LocalizationIdentifier>? comparer, params LocalizationIdentifier[]? order)
            : base(comparer, order)
        {
        }

        protected SettingsLocalizationSynchronizedBehavior(IEnumerable<LocalizationIdentifier>? order, IComparer<LocalizationIdentifier>? comparer)
            : base(order, comparer)
        {
        }

        protected SettingsLocalizationSynchronizedBehavior(IEnumerable<LocalizationIdentifiers>? order, IComparer<LocalizationIdentifier>? comparer)
            : base(order, comparer)
        {
        }
    }
    
    public abstract class SettingsLocalizationSynchronizedBehavior : SettingsSynchronizedBehavior
    {
        public abstract ILocalizationConfigInfo Localization { get; }

        public LocalizationComparer Comparer { get; }
        protected SortedSet<LocalizationIdentifier> Support { get; }

        public IReadOnlySet<LocalizationIdentifier> Identifiers
        {
            get
            {
                return Support;
            }
        }

        public abstract IConfigProperty<Int32> Identifier { get; }

        protected SettingsLocalizationSynchronizedBehavior()
        {
            Comparer = new LocalizationComparer();
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected SettingsLocalizationSynchronizedBehavior(IComparer<LocalizationIdentifier>? comparer)
        {
            Comparer = new LocalizationComparer(comparer);
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected SettingsLocalizationSynchronizedBehavior(params LocalizationIdentifier[]? order)
        {
            Comparer = new LocalizationComparer(order);
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected SettingsLocalizationSynchronizedBehavior(params LocalizationIdentifiers[]? order)
        {
            Comparer = new LocalizationComparer(order);
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected SettingsLocalizationSynchronizedBehavior(IEnumerable<LocalizationIdentifier>? order)
        {
            Comparer = new LocalizationComparer(order);
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected SettingsLocalizationSynchronizedBehavior(IEnumerable<LocalizationIdentifiers>? order)
        {
            Comparer = new LocalizationComparer(order);
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected SettingsLocalizationSynchronizedBehavior(IComparer<LocalizationIdentifier>? comparer, params LocalizationIdentifier[]? order)
        {
            Comparer = new LocalizationComparer(comparer, order);
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected SettingsLocalizationSynchronizedBehavior(IEnumerable<LocalizationIdentifier>? order, IComparer<LocalizationIdentifier>? comparer)
        {
            Comparer = new LocalizationComparer(order, comparer);
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected SettingsLocalizationSynchronizedBehavior(IEnumerable<LocalizationIdentifiers>? order, IComparer<LocalizationIdentifier>? comparer)
        {
            Comparer = new LocalizationComparer(order, comparer);
            Support = new SortedSet<LocalizationIdentifier>(Comparer, Comparer);
        }

        protected internal virtual void Subscribe()
        {
            Localization.Changed += LocalizationChanged;
            Identifier.Changed += IdentifierChanged;
        }

        protected virtual void IdentifierChanged(Object? sender, ConfigurationChangedEventArgs<Int32> args)
        {
            Localization.Localization = args.Value.Value;
        }

        protected virtual void LocalizationChanged(Object? sender, LocalizationChangedEventArgs args)
        {
            Identifier.Value = args.Value;
        }
    }
}