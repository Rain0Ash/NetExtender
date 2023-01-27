// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Numerics
{
    [Serializable]
    public readonly struct Range<T> : IComparable<Range<T>>, IEquatable<Range<T>> where T : IComparable<T>
    {
        public static implicit operator (T Min, T Max)(Range<T> value)
        {
            return (value.Minimum, value.Maximum);
        }

        [SuppressMessage("ReSharper", "UseDeconstructionOnParameter")]
        public static implicit operator Range<T>((T Min, T Max) value)
        {
            return new Range<T>(value.Min, value.Max);
        }

        public static Boolean operator ==(Range<T> first, Range<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Range<T> first, Range<T> second)
        {
            return !first.Equals(second);
        }

        public static Boolean operator <(Range<T> first, Range<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Range<T> first, Range<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Range<T> first, Range<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Range<T> first, Range<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public T Minimum { get; }
        public T Maximum { get; }

        public Range(T minimum, T maximum)
        {
            (Minimum, Maximum) = ComparerUtilities.Sort(minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range<T> Expand(Range<T> other)
        {
            return new Range<T>(Minimum.Min(other.Minimum), Maximum.Max(other.Maximum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range<T> Truncate(Range<T> other)
        {
            return new Range<T>(Minimum.Max(other.Minimum), Maximum.Min(other.Maximum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsSubRange(Range<T> other)
        {
            return Minimum.MoreEquals(other.Minimum) && Maximum.LessEquals(other.Maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsSuperRange(Range<T> other)
        {
            return Minimum.LessEquals(other.Minimum) && Maximum.MoreEquals(other.Maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Intersect(Range<T> other)
        {
            return Minimum.LessEquals(other.Maximum) && other.Minimum.LessEquals(Maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Contains(T value)
        {
            return Minimum.LessEquals(value) && Maximum.MoreEquals(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Contains(Range<T> other)
        {
            return IsSubRange(other);
        }

        public Int32 CompareTo(Range<T> other)
        {
            if (Equals(other))
            {
                return 0;
            }

            if (Minimum.More(other.Maximum))
            {
                return Int32.MaxValue;
            }

            if (Maximum.Less(other.Minimum))
            {
                return Int32.MinValue;
            }

            return Minimum.CompareTo(other.Minimum) + Maximum.CompareTo(other.Maximum);
        }

        public Boolean Equals(Range<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Minimum, other.Minimum) && EqualityComparer<T>.Default.Equals(Maximum, other.Maximum);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is Range<T> other && Equals(other);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Minimum, Maximum);
        }
    }
}