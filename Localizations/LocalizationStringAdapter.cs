// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Localizations
{
    public abstract class LocalizationStringAdapter : FormatStringBase
    {
        public override Boolean Constant
        {
            get
            {
                return false;
            }
        }
        
        public override Boolean Immutable
        {
            get
            {
                return true;
            }
        }

        public override Int32 Length
        {
            get
            {
                return GetString(Localization.Culture)?.Length ?? 0;
            }
        }

        public sealed override Int32 Arguments { get; }
        protected IDictionary<CultureInfo, IFormatString> Dictionary { get; }

        protected LocalizationStringAdapter([NotNull] IEnumerable<KeyValuePair<CultureLCID, IFormatString>> source)
            : this(source?.Select(item => new KeyValuePair<CultureInfo, IFormatString>(item.Key.GetCultureInfo(), item.Value))
                   ?? throw new ArgumentNullException(nameof(source)))
        {
        }

        protected LocalizationStringAdapter([NotNull] IEnumerable<KeyValuePair<CultureInfo, IFormatString>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<KeyValuePair<CultureInfo, IFormatString>> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return;
            }

            Dictionary = new Dictionary<CultureInfo, IFormatString>();
            
            Arguments = enumerator.Current.Value.Arguments;

            do
            {
                if (Arguments != enumerator.Current.Value.Arguments)
                {
                    throw new FormatException("Inner items expect different format arguments count");
                }
                
                Dictionary.Add(enumerator.Current);

            } while (enumerator.MoveNext());
        }

        protected virtual IFormatString GetString([NotNull] CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            return Dictionary.TryGetValue(info, out IFormatString str) ? str : Dictionary.TryGetValue(Localization.Default, out str) ? str : null;
        }

        public override String ToString()
        {
            return ToString(Localization.Culture);
        }
        
        public String ToString([NotNull] CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return GetString(info)?.ToString();
        }

        public override String ToString(IFormatProvider? provider)
        {
            return ToString(Localization.Culture, provider);
        }
        
        public String ToString([NotNull] CultureInfo info, IFormatProvider? provider)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return GetString(info)?.ToString(provider);
        }
    }
}