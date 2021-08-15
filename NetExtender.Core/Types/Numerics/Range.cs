// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Numerics
{
    [Serializable]
    public readonly struct Range<T> : IComparable<Range<T>>, IEquatable<Range<T>> where T : IComparable<T>
    {
        public static implicit operator (T Min, T Max)(Range<T> value)
        {
            return (value.Min, value.Max);
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UseDeconstructionOnParameter")]
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
        
        public T Min { get; }
        public T Max { get; }

        public Range(T min, T max)
        {
            (Min, Max) = CompareUtilities.Sort(min, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range<T> Expand(Range<T> other)
        {
            return new Range<T>(Min.Min(other.Min), Max.Max(other.Max));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range<T> Truncate(Range<T> other)
        {
            return new Range<T>(Min.Max(other.Min), Max.Min(other.Max));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsSubRange(Range<T> other)
        {
            return Min.MoreEquals(other.Min) && Max.LessEquals(other.Max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsSuperRange(Range<T> other)
        {
            return Min.LessEquals(other.Min) && Max.MoreEquals(other.Max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Intersect(Range<T> other)
        {
            return Min.LessEquals(other.Max) && other.Min.LessEquals(Max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Contains(T value)
        {
            return Min.LessEquals(value) && Max.MoreEquals(value);
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
            
            if (Min.More(other.Max))
            {
                return Int32.MaxValue;
            }

            if (Max.Less(other.Min))
            {
                return Int32.MinValue;
            }

            return Min.CompareTo(other.Min) + Max.CompareTo(other.Max);
        }
        
        public Boolean Equals(Range<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Min, other.Min) && EqualityComparer<T>.Default.Equals(Max, other.Max);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is Range<T> other && Equals(other);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}