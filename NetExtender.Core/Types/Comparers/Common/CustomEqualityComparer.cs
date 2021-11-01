// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Comparers.Interfaces;

namespace NetExtender.Types.Comparers.Common
{
    public static class CustomEqualityComparer
    {
        public static IEqualityComparer<T> Create<T>(Func<T?, T?, Boolean> comparison)
        {
            return new CustomEqualityComparer<T>(comparison);
        }
        
        public static IEqualityComparer<T> Create<T>(Func<T?, T?, Boolean> comparison, Func<T?, Int32>? hash)
        {
            return new CustomEqualityComparer<T>(comparison, hash);
        }
        
        public static IEqualityComparer<T1, T2> Create<T1, T2>(Func<T1?, T2?, Boolean> comparison)
        {
            return new CustomEqualityComparer<T1, T2>(comparison);
        }
        
        public static IEqualityComparer<T1, T2> Create<T1, T2>(Func<T1?, T2?, Boolean> comparison, Func<T1?, Int32>? hash1, Func<T2?, Int32>? hash2)
        {
            return new CustomEqualityComparer<T1, T2>(comparison, hash1, hash2);
        }
    }
    
    public sealed class CustomEqualityComparer<T> : IEqualityComparer<T>
    {
        public static IEqualityComparer<T?> Default
        {
            get
            {
                return EqualityComparer<T?>.Default;
            }
        }
        
        private Func<T?, T?, Boolean> Comparison { get; }
        private Func<T?, Int32> Hash { get; }

        public CustomEqualityComparer(Func<T?, T?, Boolean> comparison)
            : this(comparison, null)
        {
        }
        
        public CustomEqualityComparer(Func<T?, T?, Boolean> comparison, Func<T?, Int32>? hash)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
            Hash = hash ?? HashCode.Combine;
        }
        
        public Boolean Equals(T? x, T? y)
        {
            return Comparison.Invoke(x, y);
        }

        public Int32 GetHashCode(T? obj)
        {
            return Hash.Invoke(obj);
        }
    }
    
    public sealed class CustomEqualityComparer<T1, T2> : IEqualityComparer<T1, T2>
    {
        private Func<T1?, T2?, Boolean> Comparison { get; }
        private Func<T1?, Int32> Hash1 { get; }
        private Func<T2?, Int32> Hash2 { get; }

        public CustomEqualityComparer(Func<T1?, T2?, Boolean> comparison)
            : this(comparison, null, null)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
        }
        
        public CustomEqualityComparer(Func<T1?, T2?, Boolean> comparison, Func<T1?, Int32>? hash1, Func<T2?, Int32>? hash2)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
            Hash1 = hash1 ?? HashCode.Combine;
            Hash2 = hash2 ?? HashCode.Combine;
        }
        
        public Boolean Equals(T1? x, T2? y)
        {
            return Comparison.Invoke(x, y);
        }

        public Int32 GetHashCode(T1? obj)
        {
            return Hash1.Invoke(obj);
        }
        
        public Int32 GetHashCode(T2? obj)
        {
            return Hash2.Invoke(obj);
        }
    }
}