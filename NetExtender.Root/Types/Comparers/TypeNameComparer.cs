// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Cecil;
using NetExtender.Exceptions;

namespace NetExtender.Types.Comparers
{
    public enum TypeComparison
    {
        NameOrdinal,
        NameOrdinalIgnoreCase,
        FullNameOrdinal,
        FullNameOrdinalIgnoreCase
    }

    public abstract class TypeComparer : IComparer<Type?>, IComparer<MonoCecilType?>
    {
        public static implicit operator TypeComparer(TypeComparison value)
        {
            return value switch
            {
                TypeComparison.NameOrdinal => NameOrdinal,
                TypeComparison.NameOrdinalIgnoreCase => NameOrdinalIgnoreCase,
                TypeComparison.FullNameOrdinal => FullNameOrdinal,
                TypeComparison.FullNameOrdinalIgnoreCase => FullNameOrdinalIgnoreCase,
                _ => throw new EnumUndefinedOrNotSupportedException<TypeComparison>(value, nameof(value), null)
            };
        }

        public static TypeComparer NameOrdinal { get; } = new NameTypeComparer(StringComparer.Ordinal);
        public static TypeComparer NameOrdinalIgnoreCase { get; } = new NameTypeComparer(StringComparer.OrdinalIgnoreCase);
        public static TypeComparer FullNameOrdinal { get; } = new FullNameTypeComparer(StringComparer.Ordinal);
        public static TypeComparer FullNameOrdinalIgnoreCase { get; } = new FullNameTypeComparer(StringComparer.OrdinalIgnoreCase);

        public abstract Int32 Compare(Type? x, Type? y);
        public abstract Int32 Compare(MonoCecilType? x, MonoCecilType? y);

        private abstract class StringTypeComparer : TypeComparer
        {
            public IComparer<String> Comparer { get; }

            protected StringTypeComparer(IComparer<String> comparer)
            {
                Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }

            public abstract String? Selector(Type? type);
            public abstract String? Selector(MonoCecilType? type);

            public override Int32 Compare(Type? x, Type? y)
            {
                return Comparer.Compare(Selector(x), Selector(y));
            }

            public override Int32 Compare(MonoCecilType? x, MonoCecilType? y)
            {
                return Comparer.Compare(Selector(x), Selector(y));
            }
        }

        private sealed class NameTypeComparer : StringTypeComparer
        {
            public NameTypeComparer(IComparer<String> comparer)
                : base(comparer)
            {
            }

            public override String? Selector(Type? type)
            {
                return type?.Name;
            }

            public override String? Selector(MonoCecilType? type)
            {
                return type?.Name;
            }
        }

        private sealed class FullNameTypeComparer : StringTypeComparer
        {
            public FullNameTypeComparer(IComparer<String> comparer)
                : base(comparer)
            {
            }

            public override String? Selector(Type? type)
            {
                return type?.FullName;
            }

            public override String? Selector(MonoCecilType? type)
            {
                return type?.FullName;
            }
        }
    }
}