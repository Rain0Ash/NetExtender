// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
    public sealed class SHA224Managed : SHA224
    {
        private const Int32 BlockSizeBytes = 64;

        private static UInt32[] K1 { get; } =
        {
            0x428A2F98, 0x71374491, 0xB5C0FBCF, 0xE9B5DBA5,
            0x3956C25B, 0x59F111F1, 0x923F82A4, 0xAB1C5ED5,
            0xD807AA98, 0x12835B01, 0x243185BE, 0x550C7DC3,
            0x72BE5D74, 0x80DEB1FE, 0x9BDC06A7, 0xC19BF174,
            0xE49B69C1, 0xEFBE4786, 0x0FC19DC6, 0x240CA1CC,
            0x2DE92C6F, 0x4A7484AA, 0x5CB0A9DC, 0x76F988DA,
            0x983E5152, 0xA831C66D, 0xB00327C8, 0xBF597FC7,
            0xC6E00BF3, 0xD5A79147, 0x06CA6351, 0x14292967,
            0x27B70A85, 0x2E1B2138, 0x4D2C6DFC, 0x53380D13,
            0x650A7354, 0x766A0ABB, 0x81C2C92E, 0x92722C85,
            0xA2BFE8A1, 0xA81A664B, 0xC24B8B70, 0xC76C51A3,
            0xD192E819, 0xD6990624, 0xF40E3585, 0x106AA070,
            0x19A4C116, 0x1E376C08, 0x2748774C, 0x34B0BCB5,
            0x391C0CB3, 0x4ED8AA4A, 0x5B9CCA4F, 0x682E6FF3,
            0x748F82EE, 0x78A5636F, 0x84C87814, 0x8CC70208,
            0x90BEFFFA, 0xA4506CEB, 0xBEF9A3F7, 0xC67178F2
        };

        private UInt32[] H { get; }
        private UInt64 Count { get; set; }
        private UInt32[] Buffer { get; }
        private Byte[] ProcessingBuffer { get; }
        private Int32 ProcessingBufferCount { get; set; }

        public SHA224Managed()
        {
            H = new UInt32[8];
            Buffer = new UInt32[64];
            ProcessingBuffer = new Byte [BlockSizeBytes];

            Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopy(Array src, Int32 srcOffset, Array dst, Int32 dstOffset, Int32 count)
        {
            System.Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
        }

        private static UInt32 Ch(UInt32 u, UInt32 v, UInt32 w)
        {
            return (u & v) ^ (~u & w);
        }

        private static UInt32 Maj(UInt32 u, UInt32 v, UInt32 w)
        {
            return (u & v) ^ (u & w) ^ (v & w);
        }

        private static UInt32 Ro0(UInt32 x)
        {
            return ((x >> 7) | (x << 25)) ^ ((x >> 18) | (x << 14)) ^ (x >> 3);
        }

        private static UInt32 Ro1(UInt32 x)
        {
            return ((x >> 17) | (x << 15)) ^ ((x >> 19) | (x << 13)) ^ (x >> 10);
        }

        private static UInt32 Sig0(UInt32 x)
        {
            return ((x >> 2) | (x << 30)) ^ ((x >> 13) | (x << 19)) ^ ((x >> 22) | (x << 10));
        }

        private static UInt32 Sig1(UInt32 x)
        {
            return ((x >> 6) | (x << 26)) ^ ((x >> 11) | (x << 21)) ^ ((x >> 25) | (x << 7));
        }

        protected override void HashCore(Byte[] rgb, Int32 start, Int32 size)
        {
            unchecked
            {
                Int32 i;
                State = 1;

                if (ProcessingBufferCount != 0)
                {
                    if (size < BlockSizeBytes - ProcessingBufferCount)
                    {
                        BlockCopy(rgb, start, ProcessingBuffer, ProcessingBufferCount, size);
                        ProcessingBufferCount += size;
                        return;
                    }

                    i = BlockSizeBytes - ProcessingBufferCount;
                    BlockCopy(rgb, start, ProcessingBuffer, ProcessingBufferCount, i);
                    ProcessBlock(ProcessingBuffer, 0);
                    ProcessingBufferCount = 0;
                    start += i;
                    size -= i;
                }

                for (i = 0; i < size - size % BlockSizeBytes; i += BlockSizeBytes)
                {
                    ProcessBlock(rgb, start + i);
                }

                if (size % BlockSizeBytes == 0)
                {
                    return;
                }

                BlockCopy(rgb, size - size % BlockSizeBytes + start, ProcessingBuffer, 0, size % BlockSizeBytes);
                ProcessingBufferCount = size % BlockSizeBytes;
            }
        }

        protected override Byte[] HashFinal()
        {
            Byte[] hash = new Byte[28];
            ProcessFinalBlock(ProcessingBuffer, 0, ProcessingBufferCount);

            unchecked
            {
                for (Int32 i = 0; i < 7; i++)
                {
                    Int32 j;
                    for (j = 0; j < 4; j++)
                    {
                        hash[i * 4 + j] = (Byte) (H[i] >> (24 - j * 8));
                    }
                }
            }

            State = 0;
            return hash;
        }

        public override void Initialize()
        {
            Count = 0;
            ProcessingBufferCount = 0;

            H[0] = 0xC1059ED8;
            H[1] = 0x367CD507;
            H[2] = 0x3070DD17;
            H[3] = 0xF70E5939;
            H[4] = 0xFFC00B31;
            H[5] = 0x68581511;
            H[6] = 0x64F98FA7;
            H[7] = 0xBEFA4FA4;
        }

        private void ProcessBlock(Span<Byte> inputBuffer, Int32 inputOffset)
        {
            UInt32[] buff = Buffer;

            unchecked
            {
                Count += BlockSizeBytes;

                Int32 i;
                for (i = 0; i < 16; i++)
                {
                    buff[i] = (UInt32) ((inputBuffer[inputOffset + 4 * i] << 24)
                                        | (inputBuffer[inputOffset + 4 * i + 1] << 16)
                                        | (inputBuffer[inputOffset + 4 * i + 2] << 8)
                                        | inputBuffer[inputOffset + 4 * i + 3]);
                }

                UInt32 t1, t2;

                for (i = 16; i < 64; i++)
                {
                    t1 = buff[i - 15];
                    t1 = ((t1 >> 7) | (t1 << 25)) ^ ((t1 >> 18) | (t1 << 14)) ^ (t1 >> 3);

                    t2 = buff[i - 2];
                    t2 = ((t2 >> 17) | (t2 << 15)) ^ ((t2 >> 19) | (t2 << 13)) ^ (t2 >> 10);
                    buff[i] = t2 + buff[i - 7] + t1 + buff[i - 16];
                }

                UInt32 a = H[0];
                UInt32 b = H[1];
                UInt32 c = H[2];
                UInt32 d = H[3];
                UInt32 e = H[4];
                UInt32 f = H[5];
                UInt32 g = H[6];
                UInt32 h = H[7];

                for (i = 0; i < 64; i++)
                {
                    t1 = h + (((e >> 6) | (e << 26)) ^ ((e >> 11) | (e << 21)) ^ ((e >> 25) | (e << 7))) + ((e & f) ^ (~e & g)) + K1[i] + buff[i];

                    t2 = ((a >> 2) | (a << 30)) ^ ((a >> 13) | (a << 19)) ^ ((a >> 22) | (a << 10));
                    t2 += (a & b) ^ (a & c) ^ (b & c);
                    h = g;
                    g = f;
                    f = e;
                    e = d + t1;
                    d = c;
                    c = b;
                    b = a;
                    a = t1 + t2;
                }

                H[0] += a;
                H[1] += b;
                H[2] += c;
                H[3] += d;
                H[4] += e;
                H[5] += f;
                H[6] += g;
                H[7] += h;
            }
        }

        private void ProcessFinalBlock(Span<Byte> inputBuffer, Int32 inputOffset, Int32 inputCount)
        {
            unchecked
            {
                UInt64 total = Count + (UInt64) inputCount;
                Int32 paddingSize = 56 - (Int32) (total % BlockSizeBytes);

                if (paddingSize < 1)
                {
                    paddingSize += BlockSizeBytes;
                }

                Byte[] fooBuffer = new Byte[inputCount + paddingSize + 8];

                for (Int32 i = 0; i < inputCount; i++)
                {
                    fooBuffer[i] = inputBuffer[i + inputOffset];
                }

                fooBuffer[inputCount] = 0x80;
                for (Int32 i = inputCount + 1; i < inputCount + paddingSize; i++)
                {
                    fooBuffer[i] = 0x00;
                }

                // I deal in bytes. The algorithm deals in bits.
                UInt64 size = total << 3;
                AddLength(size, fooBuffer, inputCount + paddingSize);
                ProcessBlock(fooBuffer, 0);

                if (inputCount + paddingSize + 8 == 128)
                {
                    ProcessBlock(fooBuffer, 64);
                }
            }
        }

        private static void AddLength(UInt64 length, Span<Byte> buffer, Int32 position)
        {
            unchecked
            {
                buffer[position++] = (Byte) (length >> 56);
                buffer[position++] = (Byte) (length >> 48);
                buffer[position++] = (Byte) (length >> 40);
                buffer[position++] = (Byte) (length >> 32);
                buffer[position++] = (Byte) (length >> 24);
                buffer[position++] = (Byte) (length >> 16);
                buffer[position++] = (Byte) (length >> 8);
                buffer[position] = (Byte) length;
            }
        }
    }
}