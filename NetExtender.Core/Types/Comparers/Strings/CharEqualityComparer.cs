// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Types.Exceptions;

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
        
        public static IEqualityComparer<Char> CurrentCulture { get; } = new CharEqualityComparer(StringComparison.CurrentCulture);
        public static IEqualityComparer<Char> CurrentCultureIgnoreCase { get; } = new CharEqualityComparer(StringComparison.CurrentCultureIgnoreCase);
        public static IEqualityComparer<Char> InvariantCulture { get; } = new CharEqualityComparer(StringComparison.InvariantCulture);
        public static IEqualityComparer<Char> InvariantCultureIgnoreCase { get; } = new CharEqualityComparer(StringComparison.InvariantCultureIgnoreCase);
        public static IEqualityComparer<Char> Ordinal { get; } = new CharEqualityComparer(StringComparison.Ordinal);
        public static IEqualityComparer<Char> OrdinalIgnoreCase { get; } = new CharEqualityComparer(StringComparison.OrdinalIgnoreCase);

        public StringComparison Comparison { get; }
        private CompareHandler? Comparer { get; }

        public CharEqualityComparer()
            : this(StringComparison.Ordinal)
        {
        }

        public CharEqualityComparer(StringComparison comparison)
        {
            Comparison = comparison;
            Comparer = GetComparer();
        }

        public static IEqualityComparer<Char> Create(StringComparison comparison)
        {
            return comparison switch
            {
                StringComparison.CurrentCulture => CurrentCulture,
                StringComparison.CurrentCultureIgnoreCase => CurrentCultureIgnoreCase,
                StringComparison.InvariantCulture => InvariantCulture,
                StringComparison.InvariantCultureIgnoreCase => InvariantCultureIgnoreCase,
                StringComparison.Ordinal => Ordinal,
                StringComparison.OrdinalIgnoreCase => OrdinalIgnoreCase,
                _ => throw new EnumUndefinedOrNotSupportedException<StringComparison>(comparison, nameof(comparison), null)
            };
        }

        private CompareHandler GetComparer()
        {
            return GetComparer(Comparison);
        }

        private CompareHandler GetComparer(StringComparison comparison)
        {
            if (comparison == Comparison)
            {
                return Comparer ?? GetComparerCore(comparison);
            }

            return GetComparerCore(comparison);
        }

        private static CompareHandler GetComparerCore(StringComparison comparison)
        {
            return comparison switch
            {
                StringComparison.CurrentCulture => (x, y) => x == y,
                StringComparison.CurrentCultureIgnoreCase => (x, y) => Char.ToUpper(x, CultureInfo.CurrentCulture) == Char.ToUpper(y, CultureInfo.CurrentCulture),
                StringComparison.InvariantCulture => (x, y) => x == y,
                StringComparison.InvariantCultureIgnoreCase => (x, y) => Char.ToUpper(x, CultureInfo.InvariantCulture) == Char.ToUpper(y, CultureInfo.InvariantCulture),
                StringComparison.Ordinal => (x, y) => x == y,
                StringComparison.OrdinalIgnoreCase => (x, y) => Char.ToUpperInvariant(x) == Char.ToUpperInvariant(y),
                _ => throw new EnumUndefinedOrNotSupportedException<StringComparison>(comparison, nameof(comparison), null)
            };
        }

        private Boolean Equals(Char x, Char y, CompareHandler? comparison)
        {
            comparison ??= GetComparer();
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

        public Int32 GetHashCode(Char value)
        {
            return value.GetHashCode();
        }
    }
}