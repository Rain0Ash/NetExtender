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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual(this Byte[]? first, Byte[]? second)
        {
            return first.AsSpan().SequenceEqual(second.AsSpan());
        }

        /// <summary>
        /// Creates hex representation of byte array.
        /// </summary>
        /// <param name="data">Byte array.</param>
        /// <param name="separator">Separator between bytes. If null - no separator used.</param>
        /// <returns>
        /// <paramref name="data"/> represented as a series of hexadecimal representations divided by separator.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is null.</exception>
        public static unsafe String ToHexString(this ReadOnlySpan<Byte> data, String? separator)
        {
            if (data.Length <= 0)
            {
                return String.Empty;
            }

            Boolean hasSeparator = !String.IsNullOrEmpty(separator);
            Int32 length = data.Length * 2;
            if (hasSeparator)
            {
                length += (data.Length - 1) * separator.Length;
            }

            String result = new String('\0', length);

            fixed (Char* res = result, sep = separator)
            fixed (Byte* buf = &data[0])
            {
                Char* pres = res;

                for (Int32 i = 0; i < data.Length; pres += 2, i++)
                {
                    if (hasSeparator & (i != 0))
                    {
                        for (Int32 j = 0; j < separator.Length; pres++, j++)
                        {
                            pres[0] = sep[j];
                        }
                    }

                    Byte b = buf[i];
                    Int32 n = b >> 4;
                    pres[0] = (Char) (55 + n + (((n - 10) >> 31) & -7));

                    n = b & 0xF;
                    pres[1] = (Char) (55 + n + (((n - 10) >> 31) & -7));
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ConvertInformation(Double value, InformationSize from, InformationSize to)
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
        public static Decimal ConvertInformation(Decimal value, InformationSize from, InformationSize to)
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
        public static UInt64 ConvertInformation(Int32 value, InformationSize from, InformationSize to)
        {
            return value > 0 ? ConvertInformation(from, (UInt64) value, to) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this InformationSize from, Int32 value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(UInt32 value, InformationSize from, InformationSize to)
        {
            return ConvertInformation((UInt64) value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this InformationSize from, UInt32 value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(Int64 value, InformationSize from, InformationSize to)
        {
            return value > 0 ? ConvertInformation(from, (UInt64) value, to) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(this InformationSize from, Int64 value, InformationSize to)
        {
            return ConvertInformation(value, from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertInformation(UInt64 value, InformationSize from, InformationSize to)
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