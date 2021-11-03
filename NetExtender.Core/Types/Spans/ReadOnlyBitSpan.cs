// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Spans
{
    public readonly ref struct ReadOnlyBitSpan
    {
        private const Int32 Size = sizeof(Int32) * 8;

        private ReadOnlySpan<Int32> Span { get; }

        public ReadOnlyBitSpan(ReadOnlySpan<Int32> span)
        {
            Span = span;
        }
        
        public static Int32 GetLength(Int32 bits)
        {
            return bits > 0 ? (bits - 1) / Size + 1 : 0;
        }

        public Boolean this[Int32 position]
        {
            get
            {
                unchecked
                {
                    Int32 index = position / Size;
                    return (UInt32) index < (UInt32) Span.Length && (Span[index] & (1 << (position % Size))) != 0;
                }
            }
        }
    }
}