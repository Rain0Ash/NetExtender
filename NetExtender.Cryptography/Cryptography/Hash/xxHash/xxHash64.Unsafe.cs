// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Cryptography.Hash.XXHash
{
    public static partial class XXHash64
    {
        private const UInt64 P1 = 11400714785074694791UL;
        private const UInt64 P2 = 14029467366897019727UL;
        private const UInt64 P3 = 1609587929392839161UL;
        private const UInt64 P4 = 9650029242287828579UL;
        private const UInt64 P5 = 2870177450012600261UL;

        /// <summary>
        /// Compute xxhash64 for the unsafe array of memory
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="length"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe UInt64 UnsafeComputeHash(Byte* ptr, Int32 length, UInt64 seed)
        {
            unchecked
            {
                Byte* end = ptr + length;
                UInt64 h64;

                if (length >= 32)
                {
                    Byte* limit = end - 32;

                    UInt64 v1 = seed + P1 + P2;
                    UInt64 v2 = seed + P2;
                    UInt64 v3 = seed + 0;
                    UInt64 v4 = seed - P1;

                    do
                    {
                        v1 += *(UInt64*) ptr * P2;
                        v1 = v1.BitwiseRotateLeft(31); // rotl 31
                        v1 *= P1;
                        ptr += 8;

                        v2 += *(UInt64*) ptr * P2;
                        v2 = v2.BitwiseRotateLeft(31); // rotl 31
                        v2 *= P1;
                        ptr += 8;

                        v3 += *(UInt64*) ptr * P2;
                        v3 = v3.BitwiseRotateLeft(31); // rotl 31
                        v3 *= P1;
                        ptr += 8;

                        v4 += *(UInt64*) ptr * P2;
                        v4 = v4.BitwiseRotateLeft(31); // rotl 31
                        v4 *= P1;
                        ptr += 8;
                    } while (ptr <= limit);

                    h64 = v1.BitwiseRotateLeft(1) + // rotl 1
                          v2.BitwiseRotateLeft(7) + // rotl 7
                          v3.BitwiseRotateLeft(12) + // rotl 12
                          v4.BitwiseRotateLeft(18); // rotl 18

                    // merge round
                    v1 *= P2;
                    v1 = v1.BitwiseRotateLeft(31); // rotl 31
                    v1 *= P1;
                    h64 ^= v1;
                    h64 = h64 * P1 + P4;

                    // merge round
                    v2 *= P2;
                    v2 = v2.BitwiseRotateLeft(31); // rotl 31
                    v2 *= P1;
                    h64 ^= v2;
                    h64 = h64 * P1 + P4;

                    // merge round
                    v3 *= P2;
                    v3 = v3.BitwiseRotateLeft(31); // rotl 31
                    v3 *= P1;
                    h64 ^= v3;
                    h64 = h64 * P1 + P4;

                    // merge round
                    v4 *= P2;
                    v4 = v4.BitwiseRotateLeft(31); // rotl 31
                    v4 *= P1;
                    h64 ^= v4;
                    h64 = h64 * P1 + P4;
                }
                else
                {
                    h64 = seed + P5;
                }

                h64 += (UInt64) length;

                // finalize
                while (ptr <= end - 8)
                {
                    UInt64 t1 = *(UInt64*) ptr * P2;
                    t1 = t1.BitwiseRotateLeft(31); // rotl 31
                    t1 *= P1;
                    h64 ^= t1;
                    h64 = h64.BitwiseRotateLeft(27) * P1 + P4; // (rotl 27) * p1 + p4
                    ptr += 8;
                }

                if (ptr <= end - 4)
                {
                    h64 ^= *(UInt32*) ptr * P1;
                    h64 = h64.BitwiseRotateLeft(23) * P2 + P3; // (rotl 23) * p2 + p3
                    ptr += 4;
                }

                while (ptr < end)
                {
                    h64 ^= *ptr * P5;
                    h64 = h64.BitwiseRotateLeft(11) * P1; // (rotl 11) * p1
                    ptr += 1;
                }

                // avalanche
                h64 ^= h64 >> 33;
                h64 *= P2;
                h64 ^= h64 >> 29;
                h64 *= P3;
                h64 ^= h64 >> 32;

                return h64;
            }
        }

        /// <summary>
        /// Compute the first part of xxhash64 (need for the streaming api)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="l"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="v4"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void UnsafeAlign(ReadOnlySpan<Byte> data, Int32 l, ref UInt64 v1, ref UInt64 v2, ref UInt64 v3, ref UInt64 v4)
        {
            unchecked
            {
                fixed (Byte* pData = &data[0])
                {
                    Byte* ptr = pData;
                    Byte* limit = ptr + l;

                    do
                    {
                        v1 += *(UInt64*) ptr * P2;
                        v1 = v1.BitwiseRotateLeft(31); // rotl 31
                        v1 *= P1;
                        ptr += 8;

                        v2 += *(UInt64*) ptr * P2;
                        v2 = v2.BitwiseRotateLeft(31); // rotl 31
                        v2 *= P1;
                        ptr += 8;

                        v3 += *(UInt64*) ptr * P2;
                        v3 = v3.BitwiseRotateLeft(31); // rotl 31
                        v3 *= P1;
                        ptr += 8;

                        v4 += *(UInt64*) ptr * P2;
                        v4 = v4.BitwiseRotateLeft(31); // rotl 31
                        v4 *= P1;
                        ptr += 8;
                    } while (ptr < limit);
                }
            }
        }

        /// <summary>
        /// Compute the second part of xxhash64 (need for the streaming api)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="l"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="v4"></param>
        /// <param name="length"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe UInt64 UnsafeFinal(ReadOnlySpan<Byte> data, Int32 l, ref UInt64 v1, ref UInt64 v2, ref UInt64 v3, ref UInt64 v4, Int64 length, UInt64 seed)
        {
            unchecked
            {
                fixed (Byte* pData = &data[0])
                {
                    Byte* ptr = pData;
                    Byte* end = pData + l;
                    UInt64 h64;

                    if (length >= 32)
                    {
                        h64 = v1.BitwiseRotateLeft(1) + // rotl 1
                              v2.BitwiseRotateLeft(7) + // rotl 7
                              v3.BitwiseRotateLeft(12) + // rotl 12
                              v4.BitwiseRotateLeft(18); // rotl 18

                        // merge round
                        v1 *= P2;
                        v1 = v1.BitwiseRotateLeft(31); // rotl 31
                        v1 *= P1;
                        h64 ^= v1;
                        h64 = h64 * P1 + P4;

                        // merge round
                        v2 *= P2;
                        v2 = v2.BitwiseRotateLeft(31); // rotl 31
                        v2 *= P1;
                        h64 ^= v2;
                        h64 = h64 * P1 + P4;

                        // merge round
                        v3 *= P2;
                        v3 = v3.BitwiseRotateLeft(31); // rotl 31
                        v3 *= P1;
                        h64 ^= v3;
                        h64 = h64 * P1 + P4;

                        // merge round
                        v4 *= P2;
                        v4 = v4.BitwiseRotateLeft(31); // rotl 31
                        v4 *= P1;
                        h64 ^= v4;
                        h64 = h64 * P1 + P4;
                    }
                    else
                    {
                        h64 = seed + P5;
                    }

                    h64 += (UInt64) length;

                    // finalize
                    while (ptr <= end - 8)
                    {
                        UInt64 t1 = *(UInt64*) ptr * P2;
                        t1 = t1.BitwiseRotateLeft(31); // rotl 31
                        t1 *= P1;
                        h64 ^= t1;
                        h64 = h64.BitwiseRotateLeft(27) * P1 + P4; // (rotl 27) * p1 + p4
                        ptr += 8;
                    }

                    if (ptr <= end - 4)
                    {
                        h64 ^= *(UInt32*) ptr * P1;
                        h64 = h64.BitwiseRotateLeft(23) * P2 + P3; // (rotl 27) * p2 + p3
                        ptr += 4;
                    }

                    while (ptr < end)
                    {
                        h64 ^= *ptr * P5;
                        h64 = h64.BitwiseRotateLeft(11) * P1; // (rotl 11) * p1
                        ptr += 1;
                    }

                    // avalanche
                    h64 ^= h64 >> 33;
                    h64 *= P2;
                    h64 ^= h64 >> 29;
                    h64 *= P3;
                    h64 ^= h64 >> 32;

                    return h64;
                }
            }
        }
    }
}