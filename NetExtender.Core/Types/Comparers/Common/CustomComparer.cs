// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Comparers.Interfaces;

namespace NetExtender.Types.Comparers.Common
{
    public static class CustomComparer
    {
        public static IComparer<T> Create<T>(Func<T?, T?, Int32> comparison)
        {
            return new CustomComparer<T>(comparison);
        }
        
        public static IComparer<T1, T2> Create<T1, T2>(Func<T1?, T2?, Int32> comparison)
        {
            return new CustomComparer<T1, T2>(comparison);
        }
    }
    
    public sealed class CustomComparer<T> : IComparer<T>
    {
        public static IComparer<T> Default
        {
            get
            {
                return Comparer<T>.Default;
            }
        }

        private Func<T?, T?, Int32> Comparison { get; }

        public CustomComparer(Func<T?, T?, Int32> comparison)
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
        private Func<T1?, T2?, Int32> Comparison { get; }

        public CustomComparer(Func<T1?, T2?, Int32> comparison)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
        }
        
        public Int32 Compare(T1? x, T2? y)
        {
            return Comparison.Invoke(x, y);
        }
    }
}