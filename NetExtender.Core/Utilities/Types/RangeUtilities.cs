// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Types.Comparers.Common;

namespace NetExtender.Utilities.Types
{
    public static class RangeUtilities
    {
        public static IComparer<Range> Comparer { get; } = new ComparisonComparer<Range>(CompareTo);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 GetOffset(this Range range)
        {
            if (range.Start.IsFromEnd)
            {
                throw new InvalidOperationException($"Range {nameof(Index)}.{nameof(Index.Start)} can't use {nameof(Index.IsFromEnd)}.");
            }
            
            return range.Start.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 GetLength(this Range range)
        {
            if (range.Start.IsFromEnd || range.End.IsFromEnd)
            {
                throw new InvalidOperationException($"Range {nameof(Index)} can't use {nameof(Index.IsFromEnd)}.");
            }

            return Math.Abs(range.End.Value - range.Start.Value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 GetOffset(this Range range, Int32 length)
        {
            return range.GetOffsetAndLength(length).Offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 GetLength(this Range range, Int32 length)
        {
            return range.GetOffsetAndLength(length).Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 CompareTo(this Range first, Range second)
        {
            return GetLength(first).CompareTo(GetLength(second));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 CompareTo(this Range first, Range second, Int32 length)
        {
            return GetLength(first, length).CompareTo(GetLength(second, length));
        }
        
        public struct RangeEnumerator
        {
            public Int32 Current { get; private set; }
            private Int32 To { get; }
            private Int32 Step { get; }

            public RangeEnumerator(Int32 from, Int32 to)
                : this(from, to, 1)
            {
            }

            public RangeEnumerator(Int32 from, Int32 to, Int32 step)
            {
                if (step == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(step));
                }

                Step = step;
                To = to + step;
                Current = from - step;
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public Boolean MoveNext()
            {
                Current += Step;
                return Current < To;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RangeEnumerator GetEnumerator(this Range range)
        {
            return (range.Start, range.End) switch
            {
                ({ IsFromEnd: true, Value: 0 }, { IsFromEnd: true, Value: 0 }) => new RangeEnumerator(0, Int32.MaxValue, 1),
                ({ IsFromEnd: true, Value: 0 }, { IsFromEnd: false, Value: var to }) => new RangeEnumerator(0, to + 1, 1),
                ({ IsFromEnd: false, Value: var from }, { IsFromEnd: true, Value: 0 }) => new RangeEnumerator(from, Int32.MaxValue, 1),
                ({ IsFromEnd: false, Value: var from }, { IsFromEnd: false, Value: var to })
                    => (from < to) switch
                    {
                        true => new RangeEnumerator(from, to, 1),
                        false => new RangeEnumerator(from, to, -1),
                    },
                _ => throw new InvalidOperationException("Invalid range")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> AsEnumerable(this Range range)
        {
            foreach (Int32 i in range)
            {
                yield return i;
            }
        }
    }
}