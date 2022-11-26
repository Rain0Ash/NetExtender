// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Spans
{
    public readonly ref struct BitSpan
    {
        public static implicit operator ReadOnlyBitSpan(BitSpan span)
        {
            return new ReadOnlyBitSpan(span.Span);
        }

        private const Int32 Size = sizeof(Int32) * 8;
        private Span<Int32> Span { get; }

        public BitSpan(Span<Int32> span)
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
                Int32 index;
                if (position < 0 || (index = position / Size) > Span.Length) 
                {
                    throw new ArgumentOutOfRangeException(nameof(position));
                }

                return (Span[index] & (1 << (position % Size))) != 0;
            }
            set
            {
                Int32 index;
                if (position < 0 || (index = position / Size) > Span.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(position));
                }

                Int32 bit = 1 << (position % Size);
                if (value)
                {
                    Span[index] |= bit;
                    return;
                }

                Span[index] &= ~bit;
            }
        }
    }
}