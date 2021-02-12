// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Comparers.Interfaces;
using NetExtender.Configuration;
using NetExtender.Configuration.Common;
using NetExtender.Cultures.Comparers;
using NetExtender.Exceptions;
using NetExtender.Localizations.Interfaces;
using NetExtender.Localizations.Sub.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Localizations
{
    public delegate void LanguageChangedHandler(CultureInfo info);

    public static partial class Localization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class CurrentLocalization
        {
            private static ILocalization current;

            public static ILocalization Current
            {
                get
                {
                    return current ?? throw new NotInitializedException("Localization is not initialized", nameof(Current));
                }
                set
                {
                    ThrowIfAlreadyInitialized();
                    current = value ?? throw new ArgumentNullException(nameof(value));
                    current.LanguageCultureChanged += OnLanguageChanged;
                }
            }

            public static Boolean Initialized
            {
                get
                {
                    return current is not null;
                }
            }

            public static void ThrowIfAlreadyInitialized()
            {
                if (Initialized)
                {
                    throw new AlreadyInitializedException("Localization already initialized", nameof(Current));
                }
            }
        }

        public static Boolean IsInitialized
        {
            get
            {
                return CurrentLocalization.Initialized;
            }
        }

        public static ILocalization Current
        {
            get
            {
                return CurrentLocalization.Current;
            }
        }

        public static event LanguageChangedHandler Changed
        {
            add
            {
                Current.LanguageCultureChanged += value;
            }
            remove
            {
                Current.LanguageCultureChanged -= value;
            }
        }

        private static void OnLanguageChanged(CultureInfo info)
        {
            LanguageChanged?.Invoke();
        }

        public static event EmptyHandler LanguageChanged;

        public static IReadOnlyOrderedComparer<CultureInfo> Comparer
        {
            get
            {
                return IsInitialized ? Current.Comparer : CultureComparer.Default;
            }
        }

        public static Boolean ChangeUIThreadLanguage
        {
            get
            {
                return Current.ChangeUIThreadLanguage;
            }
            set
            {
                Current.ChangeUIThreadLanguage = value;
            }
        }

        public static Boolean UseSystemCulture
        {
            get
            {
                return Current.UseSystemCulture;
            }
            set
            {
                Current.UseSystemCulture = value;
            }
        }

        public static CultureInfo Culture
        {
            get
            {
                return IsInitialized ? Current.Culture : System;
            }
        }

        public static CultureInfo Basic
        {
            get
            {
                return IsInitialized ? Current.Standart : Culture;
            }
        }

        public static CultureInfo System
        {
            get
            {
                return CultureUtils.System;
            }
        }

        public static CultureInfo Default
        {
            get
            {
                return CultureUtils.English;
            }
        }

        public static IOrderedEnumerable<CultureInfo> Supported
        {
            get
            {
                return IsInitialized ? Current.Languages : EnumerableUtils.GetEnumerableFrom(Default).OrderBy(Comparer);
            }
        }

        public const ConfigType DefaultConfigType = ConfigType.JSON;

        public static ILocalization Create([NotNull] ILocalization localization)
        {
            if (localization is null)
            {
                throw new ArgumentNullException(nameof(localization));
            }
            
            CurrentLocalization.ThrowIfAlreadyInitialized();
            CurrentLocalization.Current = localization;
            return Current;
        }
        
        public static ILocalization Create([NotNull] IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            CurrentLocalization.ThrowIfAlreadyInitialized();
            CurrentLocalization.Current = new InternalLocalization(behavior);
            return Current;
        }

        public static ILocalization Create()
        {
            return Create(DefaultConfigType, Config.DefaultConfigOptions);
        }

        public static ILocalization Create(ConfigType type)
        {
            return Create(type, Config.DefaultConfigOptions);
        }

        public static ILocalization Create(ConfigOptions options)
        {
            return Create(DefaultConfigType, options);
        }

        public static ILocalization Create(ConfigType type, ConfigOptions options)
        {
            return Create(null, type, options);
        }

        public static ILocalization Create(String path)
        {
            return Create(path, DefaultConfigType);
        }

        public static ILocalization Create(String path, ConfigType type)
        {
            return Create(path, type, Config.DefaultConfigOptions);
        }

        public static ILocalization Create(String path, ConfigOptions options)
        {
            return Create(path, DefaultConfigType, options);
        }

        public static ILocalization Create(String path, ConfigType type, ConfigOptions options)
        {
            CurrentLocalization.ThrowIfAlreadyInitialized();
            CurrentLocalization.Current = new InternalLocalization(path, type, options);
            return Current;
        }

        public static Int32 GetLanguageOrderID()
        {
            return IsInitialized ? Current.GetLanguageOrderID() : 0;
        }

        public static Int32 GetLanguageOrderID([NotNull] CultureInfo info)
        {
            return Current.GetLanguageOrderID(info);
        }

        public static Boolean AddSupportedCulture([NotNull] CultureInfo info)
        {
            return Current.AddSupportedCulture(info);
        }

        public static Boolean AddSupportedCulture([NotNull] CultureInfo info, [CanBeNull] ISubLocalization localization)
        {
            return Current.AddSupportedCulture(info, localization);
        }

        public static Boolean RemoveSupportedCulture([NotNull] CultureInfo info)
        {
            return Current.RemoveSupportedCulture(info);
        }

        public static void UpdateLocalization([CanBeNull] CultureInfo info)
        {
            Current.UpdateLocalization(info);
        }
        
        public static Boolean TryUpdateLocalization([CanBeNull] CultureInfo info)
        {
            return Current.TryUpdateLocalization(info);
        }

        public static Boolean SetUILanguage()
        {
            return Current.SetUILanguage();
        }
    }
}