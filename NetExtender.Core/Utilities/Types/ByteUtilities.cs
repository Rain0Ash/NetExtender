// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public enum InformationSize : UInt64
    {
        Bit = 1,
        Byte = ByteUtilities.BitInByte,
        KiloBit = Bit * ByteUtilities.ByteMultiplier,
        KiloByte = ByteUtilities.BitInByte * KiloBit,
        MegaBit = KiloBit * KiloBit,
        MegaByte = ByteUtilities.BitInByte * MegaBit,
        GigaBit = MegaBit * KiloBit,
        GigaByte = ByteUtilities.BitInByte * GigaBit,
        TeraBit = MegaBit * MegaBit,
        TeraByte = ByteUtilities.BitInByte * TeraBit,
        PetaBit = GigaBit * MegaBit,
        PetaByte = ByteUtilities.BitInByte * PetaBit,
        ZettaBit = GigaBit * GigaBit,
        ZettaByte = ByteUtilities.BitInByte * ZettaBit,
    }

    public static class ByteUtilities
    {
        public const Int32 BitInByte = BitUtilities.BitInByte;
        public const Int32 ByteMultiplier = 1024;

        public static Boolean ToBoolean(this Byte value)
        {
            return Unsafe.As<Byte, Boolean>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual(this Byte[]? first, Byte[]? second)
        {
            return first.AsSpan().SequenceEqual(second.AsSpan());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHexString(this Byte[] array)
        {
            return ToHexString((ReadOnlySpan<Byte>) array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHexString(this Memory<Byte> memory)
        {
            return ToHexString(memory.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHexString(this Span<Byte> span)
        {
            return ToHexString((ReadOnlySpan<Byte>) span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHexString(this ReadOnlyMemory<Byte> memory)
        {
            return ToHexString(memory.Span);
        }

        public static unsafe String ToHexString(this ReadOnlySpan<Byte> span)
        {
            if (span.Length <= 0)
            {
                return String.Empty;
            }

            Int32 length = span.Length * 2;

            String result = new String('\0', length);

            fixed (Char* value = result)
            fixed (Byte* buffer = span)
            {
                Char* pointer = value;

                for (Int32 i = 0; i < span.Length; pointer += 2, i++)
                {
                    Byte @byte = buffer[i];
                    Int32 number = @byte >> 4;
                    pointer[0] = (Char) (55 + number + (((number - 10) >> 31) & -7));

                    number = @byte & 0xF;
                    pointer[1] = (Char) (55 + number + (((number - 10) >> 31) & -7));
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ConvertInformation(this Single value, InformationSize from, InformationSize to)
        {
            if (from == to)
            {
                return value;
            }

            return value * ((Single) from / (Single) to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ConvertInformation(this InformationSize from, Single value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ConvertInformation(this Double value, InformationSize from, InformationSize to)
        {
            if (from == to)
            {
                return value;
            }

            return value * ((Double) from / (Double) to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ConvertInformation(this InformationSize from, Double value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ConvertInformation(this Decimal value, InformationSize from, InformationSize to)
        {
            if (from == to)
            {
                return value;
            }

            return value * ((Decimal) from / (Decimal) to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ConvertInformation(this InformationSize from, Decimal value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this Int32 value, InformationSize from, InformationSize to)
        {
            return value > 0 ? ConvertInformation(from, (UInt64) value, to) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this InformationSize from, Int32 value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this UInt32 value, InformationSize from, InformationSize to)
        {
            return ConvertInformation((UInt64) value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this InformationSize from, UInt32 value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this Int64 value, InformationSize from, InformationSize to)
        {
            return value > 0 ? ConvertInformation(from, (UInt64) value, to) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this InformationSize from, Int64 value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this UInt64 value, InformationSize from, InformationSize to)
        {
            if (from == to)
            {
                return value;
            }

            return value * ((UInt64) from / (UInt64) to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this InformationSize from, UInt64 value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(Int32 value, InformationSize from)
        {
            return value > 0 ? ConvertToBit((UInt64) value, from) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(this InformationSize from, Int32 value)
        {
            return ConvertToBit(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(UInt32 value, InformationSize from)
        {
            return ConvertToBytes(value, from) * BitInByte;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(this InformationSize from, UInt32 value)
        {
            return ConvertToBit(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(Int64 value, InformationSize from)
        {
            return value > 0 ? ConvertToBit((UInt64) value, from) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(this InformationSize from, Int64 value)
        {
            return ConvertToBit(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(UInt64 value, InformationSize from)
        {
            return ConvertToBytes(value, from) * BitInByte;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(this InformationSize from, UInt64 value)
        {
            return ConvertToBit(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ConvertToBit(Double value, InformationSize from)
        {
            return ConvertToBytes(value, from) * BitInByte;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ConvertToBit(this InformationSize from, Double value)
        {
            return ConvertToBit(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ConvertToBit(Decimal value, InformationSize from)
        {
            return ConvertToBytes(value, from) * BitInByte;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ConvertToBit(this InformationSize from, Decimal value)
        {
            return ConvertToBit(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(Int32 value, InformationSize from)
        {
            return value > 0 ? ConvertToBytes(from, (UInt64) value) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(this InformationSize from, Int32 value)
        {
            return ConvertToBytes(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(UInt32 value, InformationSize from)
        {
            return value > 0 ? ConvertToBytes(from, (UInt64) value) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(this InformationSize from, UInt32 value)
        {
            return ConvertToBytes(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(Int64 value, InformationSize from)
        {
            return value > 0 ? ConvertToBytes(from, (UInt64) value) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(this InformationSize from, Int64 value)
        {
            return ConvertToBytes(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(UInt64 value, InformationSize from)
        {
            return from switch
            {
                InformationSize.Bit => value / BitInByte,
                InformationSize.Byte => value,
                _ => value * ((UInt64) from / BitInByte)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(this InformationSize from, UInt64 value)
        {
            return ConvertToBytes(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ConvertToBytes(Double value, InformationSize from)
        {
            return from switch
            {
                InformationSize.Bit => value / BitInByte,
                InformationSize.Byte => value,
                _ => value * ((UInt64) from / (Double) BitInByte)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ConvertToBytes(this InformationSize from, Double value)
        {
            return ConvertToBytes(value, from);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ConvertToBytes(Decimal value, InformationSize from)
        {
            return from switch
            {
                InformationSize.Bit => value / BitInByte,
                InformationSize.Byte => value,
                _ => value * ((UInt64) from / (Decimal) BitInByte)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ConvertToBytes(this InformationSize from, Decimal value)
        {
            return ConvertToBytes(value, from);
        }
    }
}