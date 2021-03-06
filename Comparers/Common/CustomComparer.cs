// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NetExtender.Comparers.Interfaces;

namespace NetExtender.Comparers.Common
{
    public sealed class CustomComparer<T> : IComparer<T>
    {
        public static IComparer<T> Default
        {
            get
            {
                return Comparer<T>.Default;
            }
        }

        public static IComparer<T> Create([NotNull] Func<T?, T?, Int32> comparison)
        {
            return new CustomComparer<T>(comparison);
        }
        
        private Func<T?, T?, Int32> Comparison { get; }

        public CustomComparer([NotNull] Func<T?, T?, Int32> comparison)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
        }
        
        public Int32 Compare(T? x, T? y)
        {
            return Comparison.Invoke(x, y);
        }
    }
    
    public sealed class CustomComparer<T1, T2> : IComparer<T1, T2>
    {
        public static IComparer<T1, T2> Create([NotNull] Func<T1?, T2?, Int32> comparison)
        {
            return new CustomComparer<T1, T2>(comparison);
        }
        
        private Func<T1?, T2?, Int32> Comparison { get; }

        public CustomComparer([NotNull] Func<T1?, T2?, Int32> comparison)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
        }
        
        public Int32 Compare(T1? x, T2? y)
        {
            return Comparison.Invoke(x, y);
        }
    }
}