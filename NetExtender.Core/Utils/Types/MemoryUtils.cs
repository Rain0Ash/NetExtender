// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utils.Types
{
    public static class MemoryUtils
    {
        public static unsafe Boolean Compare<T>(T first, T second) where T : unmanaged
        {
            return Compare(&first, &second);
        }
        
        public static unsafe Boolean Compare<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return Compare(pf, ps);
            }
        }
        
        public static unsafe Boolean Compare<T>(T* first, T* second) where T : unmanaged
        {
            return Compare((Byte*) first, (Byte*) second, sizeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean CompareInline<T>(T* first, T* second) where T : unmanaged
        {
            return CompareInline((Byte*) first, (Byte*) second, sizeof(T));
        }
        
        /// <summary>
        /// Determines whether the first count of bytes of the <paramref name="first"/> is equal to the <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first buffer to compare.</param>
        /// <param name="second">The second buffer to compare.</param>
        /// <param name="count">The number of bytes to compare.</param>
        /// <returns>
        /// true if all count bytes of the <paramref name="first"/> and <paramref name="second"/> are equal; otherwise, false.
        /// </returns>
        public static unsafe Boolean Compare(Byte* first, Byte* second, Int32 count)
        {
            return CompareInline(first, second, count);
        }

        /// <summary>
        /// Determines whether the first count of bytes of the <paramref name="first"/> is equal to the <paramref name="second"/>.
        /// </summary>
        /// <remarks>
        /// This is a forced inline version, use with care.
        /// </remarks>
        /// <param name="first">The first buffer to compare.</param>
        /// <param name="second">The second buffer to compare.</param>
        /// <param name="count">The number of bytes to compare.</param>
        /// <returns>
        /// true if all count bytes of the <paramref name="first"/> and <paramref name="second"/> are equal; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean CompareInline(Byte* first, Byte* second, Int32 count)
        {
            Byte* bp1 = first;
            Byte* bp2 = second;

            if (count >= 32)
            {
                Int32 len = count;
                do
                {
                    if (*(Int64*) bp1 != *(Int64*) bp2
                        || *(Int64*) (bp1 + 1 * 8) != *(Int64*) (bp2 + 1 * 8)
                        || *(Int64*) (bp1 + 2 * 8) != *(Int64*) (bp2 + 2 * 8)
                        || *(Int64*) (bp1 + 3 * 8) != *(Int64*) (bp2 + 3 * 8))
                    {
                        return false;
                    }

                    bp1 += 32;
                    bp2 += 32;
                    len -= 32;
                } while (len >= 32);
            }

            if ((count & 16) != 0)
            {
                if (*(Int64*) bp1 != *(Int64*) bp2
                    || *(Int64*) (bp1 + 8) != *(Int64*) (bp2 + 8))
                {
                    return false;
                }

                bp1 += 16;
                bp2 += 16;
            }

            if ((count & 8) != 0)
            {
                if (*(Int64*) bp1 != *(Int64*) bp2)
                {
                    return false;
                }

                bp1 += 8;
                bp2 += 8;
            }

            if ((count & 4) != 0)
            {
                if (*(Int32*) bp1 != *(Int32*) bp2)
                {
                    return false;
                }

                bp1 += 4;
                bp2 += 4;
            }

            // ReSharper disable once InvertIf
            if ((count & 2) != 0)
            {
                if (*(Int16*) bp1 != *(Int16*) bp2)
                {
                    return false;
                }

                bp1 += 2;
                bp2 += 2;
            }

            return (count & 1) == 0 || *bp1 == *bp2;
        }
    }
}