// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;

namespace NetExtender.Comparers
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
            switch (comparison)
            {
                case StringComparison.CurrentCulture:
                    return (x, y) => x == y;
                case StringComparison.CurrentCultureIgnoreCase:
                    return (x, y) => Char.ToUpper(x, CultureInfo.CurrentCulture) == Char.ToUpper(y, CultureInfo.CurrentCulture);
                case StringComparison.InvariantCulture:
                    return (x, y) => x == y;
                case StringComparison.InvariantCultureIgnoreCase:
                    return (x, y) => Char.ToUpper(x, CultureInfo.InvariantCulture) == Char.ToUpper(y, CultureInfo.InvariantCulture);
                case StringComparison.Ordinal:
                    return (x, y) => x == y;
                case StringComparison.OrdinalIgnoreCase:
                    return (x, y) => Char.ToUpperInvariant(x) == Char.ToUpperInvariant(y);
                default:
                    throw new NotSupportedException();
            }
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