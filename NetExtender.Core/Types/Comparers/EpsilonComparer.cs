using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Comparers
{
    public sealed class EpsilonComparer<T> : IComparer<T>
    {
        public static IComparer<T> Default { get; } = new EpsilonComparer<T>();
        private IComparer<T> Comparer { get; }

        private EpsilonComparer()
            : this(null)
        {
        }

        public EpsilonComparer(IComparer<T>? comparer)
        {
            Comparer = comparer ?? Comparer<T>.Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Int32 Compare(T? first, T? second)
        {
            if (typeof(T) == typeof(Single))
            {
                return Math.Abs(Unsafe.As<T, Single>(ref first!) - Unsafe.As<T, Single>(ref second!)) < Single.Epsilon ? 0 : Comparer.Compare(first, second);
            }

            if (typeof(T) == typeof(Double))
            {
                return Math.Abs(Unsafe.As<T, Double>(ref first!) - Unsafe.As<T, Double>(ref second!)) < Double.Epsilon ? 0 : Comparer.Compare(first, second);
            }

            if (typeof(T) == typeof(Decimal))
            {
                return Math.Abs(Unsafe.As<T, Decimal>(ref first!) - Unsafe.As<T, Decimal>(ref second!)) < MathUtilities.Constants.Decimal.Epsilon ? 0 : Comparer.Compare(first, second);
            }

            return Comparer.Compare(first, second);
        }
    }
}