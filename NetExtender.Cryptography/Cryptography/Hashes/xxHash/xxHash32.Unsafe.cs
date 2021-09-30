using System;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Crypto.Hashes.XXHash
{
    public static partial class XXHash32
    {
        private const UInt32 P1 = 2654435761U;
        private const UInt32 P2 = 2246822519U;
        private const UInt32 P3 = 3266489917U;
        private const UInt32 P4 = 668265263U;
        private const UInt32 P5 = 374761393U;

        /// <summary>
        /// Compute xxhash32 for the unsafe array of memory
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="length"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe UInt32 UnsafeComputeHash(Byte* ptr, Int32 length, UInt32 seed)
        {
            unchecked
            {
                Byte* end = ptr + length;
                UInt32 h32;

                if (length >= 16)
                {
                    Byte* limit = end - 16;

                    UInt32 v1 = seed + P1 + P2;
                    UInt32 v2 = seed + P2;
                    UInt32 v3 = seed + 0;
                    UInt32 v4 = seed - P1;

                    do
                    {
                        v1 += *(UInt32*) ptr * P2;
                        v1 = v1.BitwiseRotateLeft(13); // rotl 13
                        v1 *= P1;
                        ptr += 4;

                        v2 += *(UInt32*) ptr * P2;
                        v2 = v2.BitwiseRotateLeft(13); // rotl 13
                        v2 *= P1;
                        ptr += 4;

                        v3 += *(UInt32*) ptr * P2;
                        v3 = v3.BitwiseRotateLeft(13); // rotl 13
                        v3 *= P1;
                        ptr += 4;

                        v4 += *(UInt32*) ptr * P2;
                        v4 = v4.BitwiseRotateLeft(13); // rotl 13
                        v4 *= P1;
                        ptr += 4;
                    } while (ptr <= limit);

                    h32 = v1.BitwiseRotateLeft(1) + // rotl 1
                          v2.BitwiseRotateLeft(7) + // rotl 7
                          v3.BitwiseRotateLeft(12) + // rotl 12
                          v4.BitwiseRotateLeft(18); // rotl 18
                }
                else
                {
                    h32 = seed + P5;
                }

                h32 += (UInt32) length;

                // finalize
                while (ptr <= end - 4)
                {
                    h32 += *(UInt32*) ptr * P3;
                    h32 = h32.BitwiseRotateLeft(17) * P4; // (rotl 17) * p4
                    ptr += 4;
                }

                while (ptr < end)
                {
                    h32 += *ptr * P5;
                    h32 = h32.BitwiseRotateLeft(11) * P1; // (rotl 11) * p1
                    ptr += 1;
                }

                // avalanche
                h32 ^= h32 >> 15;
                h32 *= P2;
                h32 ^= h32 >> 13;
                h32 *= P3;
                h32 ^= h32 >> 16;

                return h32;
            }
        }

        /// <summary>
        /// Compute the first part of xxhash32 (need for the streaming api)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="l"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="v4"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void UnsafeAlign(ReadOnlySpan<Byte> data, Int32 l, ref UInt32 v1, ref UInt32 v2, ref UInt32 v3, ref UInt32 v4)
        {
            unchecked
            {
                fixed (Byte* pData = &data[0])
                {
                    Byte* ptr = pData;
                    Byte* limit = ptr + l;

                    do
                    {
                        v1 += *(UInt32*) ptr * P2;
                        v1 = v1.BitwiseRotateLeft(13); // rotl 13
                        v1 *= P1;
                        ptr += 4;

                        v2 += *(UInt32*) ptr * P2;
                        v2 = v2.BitwiseRotateLeft(13); // rotl 13
                        v2 *= P1;
                        ptr += 4;

                        v3 += *(UInt32*) ptr * P2;
                        v3 = v3.BitwiseRotateLeft(13); // rotl 13
                        v3 *= P1;
                        ptr += 4;

                        v4 += *(UInt32*) ptr * P2;
                        v4 = v4.BitwiseRotateLeft(13); // rotl 13
                        v4 *= P1;
                        ptr += 4;
                    } while (ptr < limit);
                }
            }
        }

        /// <summary>
        /// Compute the second part of xxhash32 (need for the streaming api)
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
        private static unsafe UInt32 UnsafeFinal(ReadOnlySpan<Byte> data, Int32 l, ref UInt32 v1, ref UInt32 v2, ref UInt32 v3, ref UInt32 v4, Int64 length, UInt32 seed)
        {
            unchecked
            {
                fixed (Byte* pData = &data[0])
                {
                    Byte* ptr = pData;
                    Byte* end = pData + l;
                    UInt32 h32;

                    if (length >= 16)
                    {
                        h32 = v1.BitwiseRotateLeft(1) + // rotl 1
                              v2.BitwiseRotateLeft(7) + // rotl 7
                              v3.BitwiseRotateLeft(12) + // rotl 12
                              v4.BitwiseRotateLeft(18); // rotl 18
                    }
                    else
                    {
                        h32 = seed + P5;
                    }

                    h32 += (UInt32) length;

                    // finalize
                    while (ptr <= end - 4)
                    {
                        h32 += *(UInt32*) ptr * P3;
                        h32 = h32.BitwiseRotateLeft(17) * P4; // (rotl 17) * p4
                        ptr += 4;
                    }

                    while (ptr < end)
                    {
                        h32 += *ptr * P5;
                        h32 = h32.BitwiseRotateLeft(11) * P1; // (rotl 11) * p1
                        ptr += 1;
                    }

                    // avalanche
                    h32 ^= h32 >> 15;
                    h32 *= P2;
                    h32 ^= h32 >> 13;
                    h32 *= P3;
                    h32 ^= h32 >> 16;

                    return h32;
                }
            }
        }
    }
}