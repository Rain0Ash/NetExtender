// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Properties;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Property.Localization.Properties
{
    public abstract class LocalizationPropertyInfo : ConfigPropertyInfo<ILocalizationString?>, ILocalizationPropertyInfo
    {
        public abstract LocalizationIdentifier Identifier { get; }
        
        public abstract String? Current { get; }

        public abstract event LocalizationChangedEventHandler? LocalizationChanged;

        protected LocalizationPropertyInfo(String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options, sections)
        {
        }
    }
    
    public abstract class LocalizationIdentifierPropertyInfo : ConfigPropertyInfo<String?>, ILocalizationIdentifierPropertyInfo
    {
        public LocalizationIdentifier Identifier { get; }
        
        public abstract event LocalizationChangedEventHandler? LocalizationChanged;

        protected LocalizationIdentifierPropertyInfo(String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options, sections)
        {
            Identifier = identifier;
        }
    }
}