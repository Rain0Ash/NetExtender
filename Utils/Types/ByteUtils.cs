// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    public enum InformationSize : UInt64
    {
        Bit = 1,
        Byte = ByteUtils.BitInBytes,
        KiloBit = Bit * ByteUtils.ByteMultiplier,
        KiloByte = ByteUtils.BitInBytes * KiloBit,
        MegaBit = KiloBit * KiloBit,
        MegaByte = ByteUtils.BitInBytes * MegaBit,
        GigaBit = MegaBit * KiloBit,
        GigaByte = ByteUtils.BitInBytes * GigaBit,
        TeraBit = MegaBit * MegaBit,
        TeraByte = ByteUtils.BitInBytes * TeraBit,
        PetaBit = GigaBit * MegaBit,
        PetaByte = ByteUtils.BitInBytes * PetaBit,
        ZettaBit = GigaBit * GigaBit,
        ZettaByte = ByteUtils.BitInBytes * ZettaBit
    }

    public static class ByteUtils
    {
        public const Int32 BitInBytes = 8;
        public const Int32 ByteMultiplier = 1024;

        [DllImport("msvcrt.dll")]
        private static extern Int32 memcmp(Byte[] first, Byte[] second, Int64 count);

        public static Boolean ByteArrayCompare(this Byte[] first, Byte[] second)
        {
            return first.Length == second.Length && memcmp(first, second, first.Length) == 0;
        }

        public static Boolean ByteArrayCompare(this ReadOnlySpan<Byte> first, ReadOnlySpan<Byte> second)
        {
            return first.SequenceEqual(second);
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
        [NotNull]
        [Pure]
        public static unsafe String ToHexString([NotNull] this Byte[] data, [CanBeNull] String separator)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length == 0)
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

        public static Double ConvertInformation(this InformationSize from, Double value, InformationSize to = InformationSize.Byte)
        {
            if (from == to)
            {
                return value;
            }

            return value * ((Double)from / (Double)to);
        }

        public static UInt64 ConvertInformation(this InformationSize from, Int64 value, InformationSize to = InformationSize.Byte)
        {
            return value > 0 ? ConvertInformation(from, (UInt64) value, to) : 0;
        }

        public static UInt64 ConvertInformation(this InformationSize from, UInt64 value, InformationSize to = InformationSize.Byte)
        {
            if (from == to)
            {
                return value;
            }

            return value * ((UInt64) from / (UInt64) to);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBit(this InformationSize from, UInt64 value)
        {
            return ConvertToBytes(from, value) * BitInBytes;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(this InformationSize from, Int32 value)
        {
            return value > 0 ? ConvertToBytes(from, (UInt64) value) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToBytes(this InformationSize from, UInt64 value)
        {
            return from switch
            {
                InformationSize.Bit => value / BitInBytes,
                InformationSize.Byte => value,
                _ => value * ((UInt64) from / BitInBytes)
            };
        }
    }
}