// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace NetExtender.Types.Numerics
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct LargeInteger
    {
        public static implicit operator Int64(LargeInteger value)
        {
            return value.ToInt64();
        }

        public Int32 High { get; }
        public Int32 Low { get; }
            
        public LargeInteger(Int32 high, Int32 low)
        {
            High = high;
            Low = low;
        }

        public Int64 ToInt64()
        {
            return High * 0x100000000 + Low;
        }
    }
}