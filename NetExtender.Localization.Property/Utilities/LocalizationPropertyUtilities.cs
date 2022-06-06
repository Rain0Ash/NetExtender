// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Interfaces;
using NetExtender.Localization.Properties;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Localization.Property.Localization.Properties;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Utilities
{
    public static class LocalizationPropertyUtilities
    {
        public static IReadOnlyLocalizationProperty AsReadOnly(this ILocalizationProperty property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return new ReadOnlyLocalizationPropertyWrapper(property);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty AsReadOnly(this ILocalizationIdentifierProperty property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return new ReadOnlyLocalizationIdentifierPropertyWrapper(property);
        }
        
        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, (ILocalizationString?) null, sections);
        }
        
        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, (ILocalizationString?) null, options, sections);
        }

        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, String? alternate, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyLocalizationProperty(config, key, alternate, options, sections);
        }

        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, ILocalizationString? alternate, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, ILocalizationString? alternate, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyLocalizationProperty(config, key, alternate, options, sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, identifier, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, identifier, null, sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, identifier, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, identifier, null, options, sections);
        }

        public static IReadOnlyLocalizationIdentifierProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, identifier, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyLocalizationIdentifierProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, identifier, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static IReadOnlyLocalizationIdentifierProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, identifier, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetLocalizationProperty(this IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyLocalizationIdentifierProperty(config, key, identifier, alternate, options, sections);
        }
        
        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, (IEnumerable<String>?) sections);
        }
        
        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, (ILocalizationString?) null, sections);
        }
        
        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, options, (IEnumerable<String>?) sections);
        }
        
        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, (ILocalizationString?) null, options, sections);
        }

        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, String? alternate, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new LocalizationProperty(config, key, alternate, options, sections);
        }

        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, ILocalizationString? alternate, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, ILocalizationString? alternate, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static ILocalizationProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new LocalizationProperty(config, key, alternate, options, sections);
        }
        
        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, (ILocalizationString?) null, sections);
        }
        
        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, (ILocalizationString?) null, options, sections);
        }

        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, String? alternate, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyLocalizationPropertyWrapper(config, key, alternate, options, sections);
        }

        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, ILocalizationString? alternate, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, ILocalizationString? alternate, IEnumerable<String>? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyLocalizationPropertyWrapper(config, key, alternate, options, sections);
        }
        
        public static ILocalizationIdentifierProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, identifier, (IEnumerable<String>?) sections);
        }
        
        public static ILocalizationIdentifierProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, identifier, null, sections);
        }
        
        public static ILocalizationIdentifierProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, identifier, options, (IEnumerable<String>?) sections);
        }
        
        public static ILocalizationIdentifierProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, identifier, null, options, sections);
        }

        public static ILocalizationIdentifierProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, identifier, alternate, (IEnumerable<String>?) sections);
        }

        public static ILocalizationIdentifierProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections)
        {
            return GetLocalizationProperty(config, key, identifier, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static ILocalizationIdentifierProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetLocalizationProperty(config, key, identifier, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static ILocalizationIdentifierProperty GetLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new LocalizationIdentifierProperty(config, key, identifier, alternate, options, sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, identifier, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, identifier, null, sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, identifier, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, identifier, null, options, sections);
        }

        public static IReadOnlyLocalizationIdentifierProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, identifier, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyLocalizationIdentifierProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, identifier, alternate, ConfigPropertyOptions.Caching, sections);
        }

        public static IReadOnlyLocalizationIdentifierProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyLocalizationProperty(config, key, identifier, alternate, options, (IEnumerable<String>?) sections);
        }
        
        public static IReadOnlyLocalizationIdentifierProperty GetReadOnlyLocalizationProperty(this ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyLocalizationIdentifierPropertyWrapper(config, key, identifier, alternate, options, sections);
        }
    }
}