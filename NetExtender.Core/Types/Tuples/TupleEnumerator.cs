// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Tuples
{
    public struct TupleEnumerator<T> : IEnumerator<Object?> where T : ITuple
    {
        private T Value { get; }

        public readonly Object? Current
        {
            get
            {
                return Index >= 0 ? this[Index] : throw new InvalidOperationException();
            }
        }

        public readonly Int32 Count
        {
            get
            {
                return Value.Length;
            }
        }

        private Int32 Index { get; set; }

        public TupleEnumerator(T value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Index = -1;
        }

        public Boolean MoveNext()
        {
            if (Index >= Count - 1)
            {
                return false;
            }

            ++Index;
            return true;
        }

        public void Reset()
        {
            Index = -1;
        }

        public void Dispose()
        {
            Reset();
        }

        public readonly Object? this[Int32 index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                
                return Value[index];
            }
        }
    }
}