// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Common
{
    public class MutableLocalizationString : LocalizationString, IMutableLocalizationString
    {
        public MutableLocalizationString(ILocalizationInfo info)
            : base(info)
        {
        }

        public MutableLocalizationString(ILocalizationInfo info, IComparer<LocalizationIdentifier>? comparer)
            : base(info, comparer)
        {
        }
        
        public MutableLocalizationString(ILocalizationInfo info, IEnumerable<KeyValuePair<LocalizationIdentifier, String>> localization)
            : base(info, localization)
        {
        }

        public MutableLocalizationString(ILocalizationInfo info, IEnumerable<KeyValuePair<LocalizationIdentifier, String>> localization, IComparer<LocalizationIdentifier>? comparer)
            : base(info, localization, comparer)
        {
        }

        public MutableLocalizationString(ILocalizationInfo info, IEnumerable<LocalizationValueEntry> localization)
            : base(info, localization)
        {
        }

        public MutableLocalizationString(ILocalizationInfo info, IEnumerable<LocalizationValueEntry> localization, IComparer<LocalizationIdentifier>? comparer)
            : base(info, localization, comparer)
        {
        }

        public void Add(LocalizationIdentifier identifier, String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            Localization.Add(identifier, value);
        }

        public Boolean Set(LocalizationIdentifier identifier, String? value)
        {
            if (value is null)
            {
                return Localization.Remove(identifier);
            }

            Localization[identifier] = value;
            return true;
        }

        public new String this[LocalizationIdentifier identifier]
        {
            get
            {
                return base[identifier];
            }
            set
            {
                Localization[identifier] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public override IMutableLocalizationString Clone()
        {
            return new MutableLocalizationString(Info, Localization, Comparer);
        }

        public sealed override IMutableLocalizationString ToMutable()
        {
            return Clone();
        }
    }
}