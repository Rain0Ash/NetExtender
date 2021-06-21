using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Crypto.Hashes.XXHash
{
    internal static class UnsafeBuffer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void BlockCopy(Byte[] src, Int32 srcOffset, Byte[] dst, Int32 dstOffset, Int32 count)
        {
            fixed (Byte* pSrc = &src[srcOffset])
            fixed (Byte* pDst = &dst[dstOffset])
            {
                Byte* ptrSrc = pSrc;
                Byte* ptrDst = pDst;

                SMALLTABLE:
                switch (count)
                {
                    case 0:
                        return;
                    case 1:
                        *ptrDst = *ptrSrc;
                        return;
                    case 2:
                        *(Int16*) ptrDst = *(Int16*) ptrSrc;
                        return;
                    case 3:
                        *(Int16*) (ptrDst + 0) = *(Int16*) (ptrSrc + 0);
                        *(ptrDst + 2) = *(ptrSrc + 2);
                        return;
                    case 4:
                        *(Int32*) ptrDst = *(Int32*) ptrSrc;
                        return;
                    case 5:
                        *(Int32*) (ptrDst + 0) = *(Int32*) (ptrSrc + 0);
                        *(ptrDst + 4) = *(ptrSrc + 4);
                        return;
                    case 6:
                        *(Int32*) (ptrDst + 0) = *(Int32*) (ptrSrc + 0);
                        *(Int16*) (ptrDst + 4) = *(Int16*) (ptrSrc + 4);
                        return;
                    case 7:
                        *(Int32*) (ptrDst + 0) = *(Int32*) (ptrSrc + 0);
                        *(Int16*) (ptrDst + 4) = *(Int16*) (ptrSrc + 4);
                        *(ptrDst + 6) = *(ptrSrc + 6);
                        return;
                    case 8:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        return;
                    case 9:
                        *(Int64*) (ptrDst + 0) = *(Int64*) (ptrSrc + 0);
                        *(ptrDst + 8) = *(ptrSrc + 8);
                        return;
                    case 10:
                        *(Int64*) (ptrDst + 0) = *(Int64*) (ptrSrc + 0);
                        *(Int16*) (ptrDst + 8) = *(Int16*) (ptrSrc + 8);
                        return;
                    case 11:
                        *(Int64*) (ptrDst + 0) = *(Int64*) (ptrSrc + 0);
                        *(Int16*) (ptrDst + 8) = *(Int16*) (ptrSrc + 8);
                        *(ptrDst + 10) = *(ptrSrc + 10);
                        return;
                    case 12:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int32*) (ptrDst + 8) = *(Int32*) (ptrSrc + 8);
                        return;
                    case 13:
                        *(Int64*) (ptrDst + 0) = *(Int64*) (ptrSrc + 0);
                        *(Int32*) (ptrDst + 8) = *(Int32*) (ptrSrc + 8);
                        *(ptrDst + 12) = *(ptrSrc + 12);
                        return;
                    case 14:
                        *(Int64*) (ptrDst + 0) = *(Int64*) (ptrSrc + 0);
                        *(Int32*) (ptrDst + 8) = *(Int32*) (ptrSrc + 8);
                        *(Int16*) (ptrDst + 12) = *(Int16*) (ptrSrc + 12);
                        return;
                    case 15:
                        *(Int64*) (ptrDst + 0) = *(Int64*) (ptrSrc + 0);
                        *(Int32*) (ptrDst + 8) = *(Int32*) (ptrSrc + 8);
                        *(Int16*) (ptrDst + 12) = *(Int16*) (ptrSrc + 12);
                        *(ptrDst + 14) = *(ptrSrc + 14);
                        return;
                    case 16:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        return;
                    case 17:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(ptrDst + 16) = *(ptrSrc + 16);
                        return;
                    case 18:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int16*) (ptrDst + 16) = *(Int16*) (ptrSrc + 16);
                        return;
                    case 19:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int16*) (ptrDst + 16) = *(Int16*) (ptrSrc + 16);
                        *(ptrDst + 18) = *(ptrSrc + 18);
                        return;
                    case 20:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int32*) (ptrDst + 16) = *(Int32*) (ptrSrc + 16);
                        return;

                    case 21:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int32*) (ptrDst + 16) = *(Int32*) (ptrSrc + 16);
                        *(ptrDst + 20) = *(ptrSrc + 20);
                        return;
                    case 22:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int32*) (ptrDst + 16) = *(Int32*) (ptrSrc + 16);
                        *(Int16*) (ptrDst + 20) = *(Int16*) (ptrSrc + 20);
                        return;
                    case 23:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int32*) (ptrDst + 16) = *(Int32*) (ptrSrc + 16);
                        *(Int16*) (ptrDst + 20) = *(Int16*) (ptrSrc + 20);
                        *(ptrDst + 22) = *(ptrSrc + 22);
                        return;
                    case 24:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        return;
                    case 25:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        *(ptrDst + 24) = *(ptrSrc + 24);
                        return;
                    case 26:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        *(Int16*) (ptrDst + 24) = *(Int16*) (ptrSrc + 24);
                        return;
                    case 27:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        *(Int16*) (ptrDst + 24) = *(Int16*) (ptrSrc + 24);
                        *(ptrDst + 26) = *(ptrSrc + 26);
                        return;
                    case 28:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        *(Int32*) (ptrDst + 24) = *(Int32*) (ptrSrc + 24);
                        return;
                    case 29:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        *(Int32*) (ptrDst + 24) = *(Int32*) (ptrSrc + 24);
                        *(ptrDst + 28) = *(ptrSrc + 28);
                        return;
                    case 30:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        *(Int32*) (ptrDst + 24) = *(Int32*) (ptrSrc + 24);
                        *(Int16*) (ptrDst + 28) = *(Int16*) (ptrSrc + 28);
                        return;
                    case 31:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        *(Int32*) (ptrDst + 24) = *(Int32*) (ptrSrc + 24);
                        *(Int16*) (ptrDst + 28) = *(Int16*) (ptrSrc + 28);
                        *(ptrDst + 30) = *(ptrSrc + 30);
                        return;
                    case 32:
                        *(Int64*) ptrDst = *(Int64*) ptrSrc;
                        *(Int64*) (ptrDst + 8) = *(Int64*) (ptrSrc + 8);
                        *(Int64*) (ptrDst + 16) = *(Int64*) (ptrSrc + 16);
                        *(Int64*) (ptrDst + 24) = *(Int64*) (ptrSrc + 24);
                        return;
                    default:
                        break;
                }

                Int64* lpSrc = (Int64*) ptrSrc;
                Int64* ldSrc = (Int64*) ptrDst;
                while (count >= 64)
                {
                    *(ldSrc + 0) = *(lpSrc + 0);
                    *(ldSrc + 1) = *(lpSrc + 1);
                    *(ldSrc + 2) = *(lpSrc + 2);
                    *(ldSrc + 3) = *(lpSrc + 3);
                    *(ldSrc + 4) = *(lpSrc + 4);
                    *(ldSrc + 5) = *(lpSrc + 5);
                    *(ldSrc + 6) = *(lpSrc + 6);
                    *(ldSrc + 7) = *(lpSrc + 7);

                    if (count == 64)
                    {
                        return;
                    }

                    count -= 64;
                    lpSrc += 8;
                    ldSrc += 8;
                }

                if (count > 32)
                {
                    *(ldSrc + 0) = *(lpSrc + 0);
                    *(ldSrc + 1) = *(lpSrc + 1);
                    *(ldSrc + 2) = *(lpSrc + 2);
                    *(ldSrc + 3) = *(lpSrc + 3);
                    count -= 32;
                    lpSrc += 4;
                    ldSrc += 4;
                }

                ptrSrc = (Byte*) lpSrc;
                ptrDst = (Byte*) ldSrc;
                goto SMALLTABLE;
            }
        }
    }
}