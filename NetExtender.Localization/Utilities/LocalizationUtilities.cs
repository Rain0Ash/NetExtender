// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Localization.Behavior;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Interfaces;
using NetExtender.Localization.Wrappers;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Utilities
{
    public static class LocalizationUtilities
    {
        public static IReadOnlyLocalizationConfig AsReadOnly(this ILocalizationConfig config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            return new ReadOnlyLocalizationConfigWrapper(config);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, options);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, ILocalizationConverter? converter)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, converter);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, ILocalizationConverter? converter, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, converter, options);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, options);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, ILocalizationConverter? converter)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, converter);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, ILocalizationConverter? converter, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, converter, options);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, system);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, system, options);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, ILocalizationConverter? converter)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, system, converter);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, ILocalizationConverter? converter, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, system, converter, options);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, comparer);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, IComparer<LocalizationIdentifier>? comparer, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, comparer, options);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, ILocalizationConverter? converter, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, converter, comparer);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, ILocalizationConverter? converter, IComparer<LocalizationIdentifier>? comparer, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, converter, comparer, options);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, comparer);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationOptions options, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, options, comparer);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, ILocalizationConverter? converter, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, converter, comparer);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, ILocalizationConverter? converter, LocalizationOptions options, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, converter, options, comparer);
        }
        
        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, system, comparer);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, LocalizationOptions options, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, system, options, comparer);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, ILocalizationConverter? converter, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, system, converter, comparer);
        }

        public static ILocalizationBehavior Localization(this IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, ILocalizationConverter? converter, LocalizationOptions options, IComparer<LocalizationIdentifier>? comparer)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }
            
            return new LocalizationBehavior(behavior, localization, system, converter, options, comparer);
        }

        public static ILocalizationConfig Create(this ILocalizationBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new LocalizationConfig(behavior);
        }
    }
}