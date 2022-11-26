// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Properties;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Property.Localization.Properties
{
    public abstract class LocalizationPropertyInfo : LocalizationPropertyInfoAbstraction<ILocalizationString?>
    {
        protected LocalizationPropertyInfo(String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options, sections)
        {
        }
    }

    public abstract class LocalizationIdentifierPropertyInfo : LocalizationPropertyInfoAbstraction<String?>, ILocalizationIdentifierPropertyInfo
    {
        public abstract event LocalizationValueChangedEventHandler? Changed;
        public sealed override LocalizationIdentifier Identifier { get; }

        protected LocalizationIdentifierPropertyInfo(String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options, sections)
        {
            Identifier = identifier;
        }
    }

    public abstract class LocalizationPropertyInfoAbstraction<T> : ConfigPropertyInfo<T>, ILocalizationPropertyInfo where T : class?
    {
        public abstract event LocalizationChangedEventHandler? LocalizationChanged;
        public abstract LocalizationIdentifier Identifier { get; }

        public virtual String Current
        {
            get
            {
                String? result = null;
                if (HasValue)
                {
                    result = Internal.Value?.ToString();
                }

                if (result is not null)
                {
                    return result;
                }

                result = Alternate?.ToString();
                return result ?? AlternateKeyValueIdentifier;
            }
        }

        private String? _alternate;
        protected String AlternateKeyValueIdentifier
        {
            get
            {
                return _alternate ??= CreateAlternateKeyValueIdentifier();
            }
        }

        protected LocalizationPropertyInfoAbstraction(String? key, T alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options, sections)
        {
        }

        protected virtual String CreateAlternateKeyValueIdentifier()
        {
            String? key = Key;
            ImmutableArray<String> sections = key is not null ? Sections.Add(key) : Sections;
            return String.Join(".", sections.Select(section => section.ToUpperInvariant()));
        }
    }
}