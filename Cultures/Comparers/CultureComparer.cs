// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Utils.Numerics;
using NetExtender.Comparers;
using NetExtender.Localizations;
using NetExtender.Utils.Types;

namespace NetExtender.Cultures.Comparers
{
    public class CultureComparer : OrderedComparer<String>
    {
        public CultureComparer(IEnumerable<Culture> order)
            : this(order?.Select(culture => culture.Code) ?? EnumerableUtils.GetEnumerableFrom(Localization.CurrentCulture.Code))
        {
        }

        public CultureComparer(IEnumerable<Int32> order)
            : this(order?.TrySelect(Localization.CodeByLCID.TryGetValue))
        {
        }

        public CultureComparer(IEnumerable<String> order = null)
            : base((order ?? LocaleMultiString.DefaultLocalization).Select(code => code.ToLower()))
        {
        }

        public Int32 GetLanguageOrderID(Int32 lcid)
        {
            return GetLanguageOrderID(Localization.CodeByLCID.TryGetValue(lcid, Localization.DefaultCulture.Code));
        }

        public Int32 GetLanguageOrderID(String code)
        {
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            return Order.IndexOf(code.ToLower()).ToRange();
        }
    }
}