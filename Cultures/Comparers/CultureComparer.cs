// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NetExtender.Comparers;
using NetExtender.Comparers.Interfaces;
using NetExtender.Localizations;
using NetExtender.Types.Culture;
using NetExtender.Utils.Types;

namespace NetExtender.Cultures.Comparers
{
    public class CultureComparer : OrderedComparer<CultureInfo>
    {
        public static IReadOnlyOrderedComparer<CultureInfo> Default { get; } = new CultureComparer {Localization.Default};

        public CultureComparer(params CultureLCID[] order)
            : this((IEnumerable<CultureLCID>) order)
        {
        }
        
        public CultureComparer(params CultureInfo[] order)
            : this((IEnumerable<CultureInfo>) order)
        {
        }
        
        public CultureComparer(IEnumerable<CultureLCID> order)
            : this(order?.Select(CultureUtils.GetCultureInfo))
        {
        }
        
        public CultureComparer(IEnumerable<CultureInfo> order = null)
            : base(order)
        {
            Comparer = Comparer<CultureInfo>.Create(CompareInternal);
        }

        protected virtual Int32 CompareInternal(CultureInfo first, CultureInfo second)
        {
            return Comparer<Int32?>.Default.Compare(first?.LCID, second?.LCID);
        }
    }
}