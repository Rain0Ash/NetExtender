// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using NetExtender.Types.Strings;
using NetExtender.Utils.Types;

namespace NetExtender.Localizations
{
    public class LocalizationString : StringBase
    {
        public override Boolean Immutable
        {
            get
            {
                return true;
            }
        }

        public override Boolean Constant
        {
            get
            {
                return false;
            }
        }

        protected IDictionary<CultureInfo, String> Localizations { get; }
        
        public String Default { get; }
        
        public override String Text
        {
            get
            {
                return GetLocalization();
            }
            protected set
            {
                throw new NotSupportedException();
            }
        }

        public LocalizationString(String @default)
        {
            Default = @default;
            Localizations = new Dictionary<CultureInfo, String>();
        }

        public LocalizationString(String @default, [NotNull] IEnumerable<KeyValuePair<CultureInfo, String>> localization)
        {
            if (localization is null)
            {
                throw new ArgumentNullException(nameof(localization));
            }

            Default = @default;
            Localizations = new Dictionary<CultureInfo, String>(localization);
        }

        protected String GetLocalization()
        {
            return GetLocalization(Localization.Culture);
        }
        
        protected String GetLocalization(CultureInfo info)
        {
            return Localizations.TryGetValue(info, Default);
        }
    }
}