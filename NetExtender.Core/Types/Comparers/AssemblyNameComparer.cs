using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Comparers
{
    public enum AssemblyComparison
    {
        FullNameOrdinal,
        FullNameOrdinalIgnoreCase
    }
    
    public abstract class AssemblyComparer : IComparer<Assembly?>
    {
        public static implicit operator AssemblyComparer(AssemblyComparison value)
        {
            return value switch
            {
                AssemblyComparison.FullNameOrdinal => FullNameOrdinal,
                AssemblyComparison.FullNameOrdinalIgnoreCase => FullNameOrdinalIgnoreCase,
                _ => throw new EnumUndefinedOrNotSupportedException<AssemblyComparison>(value, nameof(value), null)
            };
        }
        
        public static AssemblyComparer FullNameOrdinal { get; } = new FullNameAssemblyComparer(StringComparer.Ordinal);
        public static AssemblyComparer FullNameOrdinalIgnoreCase { get; } = new FullNameAssemblyComparer(StringComparer.OrdinalIgnoreCase);
        
        public abstract Int32 Compare(Assembly? x, Assembly? y);
        
        private abstract class StringAssemblyComparer : AssemblyComparer
        {
            public IComparer<String> Comparer { get; }
            
            protected StringAssemblyComparer(IComparer<String> comparer)
            {
                Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }
            
            public abstract String? Selector(Assembly? type);
            
            public override Int32 Compare(Assembly? x, Assembly? y)
            {
                return Comparer.Compare(Selector(x), Selector(y));
            }
        }
        
        private sealed class FullNameAssemblyComparer : StringAssemblyComparer
        {
            public FullNameAssemblyComparer(IComparer<String> comparer)
                : base(comparer)
            {
            }
            
            public override String? Selector(Assembly? type)
            {
                return type?.FullName;
            }
        }
    }
}