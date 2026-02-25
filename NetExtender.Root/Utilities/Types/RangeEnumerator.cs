// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
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
                throw new ArgumentOutOfRangeException(nameof(step), step, null);
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
}