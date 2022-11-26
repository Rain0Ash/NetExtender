// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Comparers.Interfaces;

namespace NetExtender.Types.Comparers
{
    public sealed class EqualityComparisonComparer<T> : IEqualityComparer<T>
    {
        public static IEqualityComparer<T?> Default
        {
            get
            {
                return EqualityComparer<T?>.Default;
            }
        }

        private EqualityComparison<T> Comparison { get; }
        private HashHandler<T> Hash { get; }
        public Boolean Nullable { get; }

        public EqualityComparisonComparer(EqualityComparison<T> comparison)
            : this(comparison, true)
        {
        }

        public EqualityComparisonComparer(EqualityComparison<T> comparison, Boolean nullable)
            : this(comparison, null, nullable)
        {
        }

        public EqualityComparisonComparer(EqualityComparison<T> comparison, HashHandler<T>? hash)
            : this(comparison, hash, true)
        {
        }

        public EqualityComparisonComparer(EqualityComparison<T> comparison, HashHandler<T>? hash, Boolean nullable)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
            Hash = hash ?? HashCode.Combine;
            Nullable = nullable;
        }

        public Boolean Equals(T? x, T? y)
        {
            if (Nullable)
            {
                return Comparison.Invoke(x!, y!);
            }

            if (x is null)
            {
                return y is null;
            }

            return y is not null && Comparison.Invoke(x, y);
        }

        public Int32 GetHashCode(T? obj)
        {
            return Nullable || obj is not null ? Hash.Invoke(obj!) : 0;
        }
    }

    public sealed class EqualityComparisonComparer<T1, T2> : IEqualityComparer<T1, T2>
    {
        private EqualityComparison<T1, T2> Comparison { get; }
        private HashHandler<T1> Hash1 { get; }
        private HashHandler<T2> Hash2 { get; }
        public Boolean Nullable { get; }

        public EqualityComparisonComparer(EqualityComparison<T1, T2> comparison)
            : this(comparison, true)
        {
        }

        public EqualityComparisonComparer(EqualityComparison<T1, T2> comparison, Boolean nullable)
            : this(comparison, null, null, nullable)
        {
        }

        public EqualityComparisonComparer(EqualityComparison<T1, T2> comparison, HashHandler<T1>? hash1, HashHandler<T2>? hash2)
            : this(comparison, hash1, hash2, true)
        {
        }

        public EqualityComparisonComparer(EqualityComparison<T1, T2> comparison, HashHandler<T1>? hash1, HashHandler<T2>? hash2, Boolean nullable)
        {
            Comparison = comparison ?? throw new ArgumentNullException(nameof(comparison));
            Hash1 = hash1 ?? HashCode.Combine;
            Hash2 = hash2 ?? HashCode.Combine;
            Nullable = nullable;
        }

        public Boolean Equals(T1? x, T2? y)
        {
            if (Nullable)
            {
                return Comparison.Invoke(x!, y!);
            }

            if (x is null)
            {
                return y is null;
            }

            return y is not null && Comparison.Invoke(x, y);
        }

        public Int32 GetHashCode(T1? obj)
        {
            return Nullable || obj is not null ? Hash1.Invoke(obj!) : 0;
        }

        public Int32 GetHashCode(T2? obj)
        {
            return Nullable || obj is not null ? Hash2.Invoke(obj!) : 0;
        }
    }
}