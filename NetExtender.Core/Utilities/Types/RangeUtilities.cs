// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class RangeUtilities
    {
        public struct RangeEnumerator
        {
            public Int32 Current { get; private set; }
            private Int32 To { get; }
            private Int32 Step { get; }

            public RangeEnumerator(Int32 from, Int32 to, Int32 step)
            {
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
    }
}