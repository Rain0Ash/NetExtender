// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Common
{
    public class LocalizationString : StringBase
    {
        protected ILocalizationBehavior Behavior { get; }
        protected Dictionary<LCID, String> Localization { get; }
        
        public override Boolean Constant
        {
            get
            {
                return false;
            }
        }

        public String Text
        {
            get
            {
                Localization.TryGetValue()
            }
        }

        public LocalizationString(ILocalizationBehavior behavior, IEnumerable<KeyValuePair<LCID, String>> localization)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Localization = localization?.ToDictionary() ?? throw new ArgumentNullException(nameof(localization));
        }
    }
}