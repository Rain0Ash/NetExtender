using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Comparers
{
    public sealed class EpsilonEqualityComparer<T> : IEqualityComparer<T>
    {
        public static IEqualityComparer<T> Default { get; } = new EpsilonEqualityComparer<T>();
        private IEqualityComparer<T> Comparer { get; }

        private EpsilonEqualityComparer()
            : this(null)
        {
        }

        public EpsilonEqualityComparer(IEqualityComparer<T>? comparer)
        {
            Comparer = comparer ?? EqualityComparer<T>.Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean Equals(T? first, T? second)
        {
            if (typeof(T) == typeof(Single))
            {
                return Math.Abs(Unsafe.As<T, Single>(ref first!) - Unsafe.As<T, Single>(ref second!)) < Single.Epsilon;
            }

            if (typeof(T) == typeof(Double))
            {
                return Math.Abs(Unsafe.As<T, Double>(ref first!) - Unsafe.As<T, Double>(ref second!)) < Double.Epsilon;
            }

            if (typeof(T) == typeof(Decimal))
            {
                return Math.Abs(Unsafe.As<T, Decimal>(ref first!) - Unsafe.As<T, Decimal>(ref second!)) < MathUtilities.Constants.Decimal.Epsilon;
            }

            return Comparer.Equals(first, second);
        }

        public Int32 GetHashCode(T? other)
        {
            return other is not null ? Comparer.GetHashCode(other) : 0;
        }
    }
}