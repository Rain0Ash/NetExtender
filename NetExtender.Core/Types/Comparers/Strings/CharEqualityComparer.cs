// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;

namespace NetExtender.Types.Comparers
{
    public sealed class CharEqualityComparer : IEqualityComparer<Char>
    {
        private delegate Boolean CompareHandler(Char x, Char y);

        public static IComparer<Char> Default
        {
            get
            {
                return Comparer<Char>.Default;
            }
        }

        public StringComparison Comparison { get; }
        private CompareHandler Comparer { get; }

        public CharEqualityComparer()
            : this(StringComparison.Ordinal)
        {
        }

        public CharEqualityComparer(StringComparison comparison)
        {
            Comparison = comparison;
            Comparer = GetComparer();
        }

        private CompareHandler GetComparer()
        {
            return GetComparer(Comparison);
        }

        private CompareHandler GetComparer(StringComparison comparison)
        {
            if (comparison == Comparison)
            {
                return Comparer ?? GetComparerInternal(comparison);
            }

            return GetComparerInternal(comparison);
        }

        private static CompareHandler GetComparerInternal(StringComparison comparison)
        {
            return comparison switch
            {
                StringComparison.CurrentCulture => (x, y) => x == y,
                StringComparison.CurrentCultureIgnoreCase => (x, y) => Char.ToUpper(x, CultureInfo.CurrentCulture) == Char.ToUpper(y, CultureInfo.CurrentCulture),
                StringComparison.InvariantCulture => (x, y) => x == y,
                StringComparison.InvariantCultureIgnoreCase => (x, y) => Char.ToUpper(x, CultureInfo.InvariantCulture) == Char.ToUpper(y, CultureInfo.InvariantCulture),
                StringComparison.Ordinal => (x, y) => x == y,
                StringComparison.OrdinalIgnoreCase => (x, y) => Char.ToUpperInvariant(x) == Char.ToUpperInvariant(y),
                _ => throw new NotSupportedException()
            };
        }

        private static Boolean Equals(Char x, Char y, CompareHandler comparison)
        {
            return comparison(x, y);
        }

        public Boolean Equals(Char x, Char y)
        {
            return Equals(x, y, Comparer);
        }

        public Boolean Equals(Char x, Char y, StringComparison comparison)
        {
            return Equals(x, y, GetComparer(comparison));
        }

        public Int32 GetHashCode(Char obj)
        {
            return obj.GetHashCode();
        }
    }
}