// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Comparers;
using NetExtender.Comparers.Interfaces;
using NetExtender.Localizations;

namespace NetExtender.Cultures.Comparers
{
    public class CultureComparer : OrderedComparer<CultureInfo>
    {
        public static IReadOnlyOrderedComparer<CultureInfo> Default { get; } = new CultureComparer {Localization.Default};

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