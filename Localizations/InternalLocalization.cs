// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using NetExtender.Configuration;
using NetExtender.Configuration.Common;
using NetExtender.Cultures.Comparers;
using NetExtender.Exceptions;
using NetExtender.Localizations.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Localizations
{
    public delegate void LanguageChangedHandler(CultureInfo info);
    
    public static partial class InternalLocalization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class CurrentLocalization
        {
            private static ILocalization current;
            public static ILocalization Current
            {
                get
                {
                    return current ?? throw new NotInitializedException();
                }
                set
                {
                    ThrowIfAlreadyInitialized();
                    current = value ?? throw new ArgumentNullException(nameof(value));
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
                    throw new AlreadyInitializedException();
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
        
        public static CultureComparer Comparer
        {
            get
            {
                return Current.Comparer;
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
                return Current.Culture;
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
        
        public static IReadOnlySet<CultureInfo> Supported
        {
            get
            {
                return Current.Languages;
            }
        }

        public const ConfigType DefaultConfigType = ConfigType.JSON;
        
        public static ILocalization Create([NotNull] IConfigBehavior behavior)
        {
            CurrentLocalization.ThrowIfAlreadyInitialized();
            CurrentLocalization.Current = new InternalLocalization2(behavior);
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
            CurrentLocalization.Current = new InternalLocalization2(path, type, options);
            return Current;
        }
    }
}