using System;
using System.Collections.Generic;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Comparers
{
    public enum TypeComparison
    {
        NameOrdinal,
        NameOrdinalIgnoreCase,
        FullNameOrdinal,
        FullNameOrdinalIgnoreCase
    }
    
    public abstract class TypeComparer : IComparer<Type>
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
        
        private abstract class StringTypeComparer : TypeComparer
        {
            public IComparer<String> Comparer { get; }
            
            protected StringTypeComparer(IComparer<String> comparer)
            {
                Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }
            
            public abstract String? Selector(Type? type);
            
            public override Int32 Compare(Type? x, Type? y)
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
        }
    }
}