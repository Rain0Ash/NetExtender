// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Comparers.Interfaces;

namespace NetExtender.Types.Comparers.Common
{
    public sealed class ComparisonComparer<T> : IComparer<T>
    {
        public static IComparer<T> Default
        {
            get
            {
                return Comparer<T>.Default;
            }
        }

        private Comparison<T> Comparison { get; }
        public Boolean Nullable { get; }

        public ComparisonComparer(Comparison<T> comparison)
            : this(comparison, true)
        {
        }

        public ComparisonComparer(Comparison<T> comparison, Boolean nullable)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
            Nullable = nullable;
        }
        
        public Int32 Compare(T? x, T? y)
        {
            if (Nullable)
            {
                return Comparison.Invoke(x!, y!);
            }
            
            if (x is null)
            {
                return y is null ? 0 : -1;
            }

            return y is not null ? Comparison.Invoke(x, y) : 1;
        }
    }
    
    public sealed class ComparisonComparer<T1, T2> : IComparer<T1, T2>
    {
        private Comparison<T1, T2> Comparison { get; }
        public Boolean Nullable { get; }

        public ComparisonComparer(Comparison<T1, T2> comparison)
            : this(comparison, true)
        {
        }

        public ComparisonComparer(Comparison<T1, T2> comparison, Boolean nullable)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
            Nullable = nullable;
        }
        
        public Int32 Compare(T1? x, T2? y)
        {
            if (Nullable)
            {
                return Comparison.Invoke(x!, y!);
            }
            
            if (x is null)
            {
                return y is null ? 0 : -1;
            }

            return y is not null ? Comparison.Invoke(x, y) : 1;
        }
    }
}