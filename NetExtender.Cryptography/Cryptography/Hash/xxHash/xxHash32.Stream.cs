// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;
using System.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Cryptography.Hash.XXHash
{
    public static partial class XXHash32
    {
        /// <summary>
        /// Compute xxHash for the stream
        /// </summary>
        /// <param name="stream">The stream of data</param>
        /// <param name="buffer">The buffer size</param>
        /// <param name="seed">The seed number</param>
        /// <returns>The hash</returns>
        public static UInt32 ComputeHash(Stream stream, Int32 buffer = BufferUtilities.DefaultBuffer, UInt32 seed = 0)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (buffer < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer), buffer, null);
            }

            // Optimizing memory allocation
            Byte[] array = ArrayPool<Byte>.Shared.Rent(buffer + 16);

            Int32 offset = 0;
            Int64 length = 0;

            // Prepare the seed vector
            UInt32 v1 = seed + P1 + P2;
            UInt32 v2 = seed + P2;
            UInt32 v3 = seed + 0;
            UInt32 v4 = seed - P1;

            try
            {
                // Read flow of bytes
                Int32 read;
                while ((read = stream.Read(array, offset, buffer)) > 0)
                {
                    length += read;
                    offset += read;

                    if (offset < 16)
                    {
                        continue;
                    }

                    Int32 r = offset % 16; // remain
                    Int32 l = offset - r;  // length

                    // Process the next chunk 
                    UnsafeAlign(array, l, ref v1, ref v2, ref v3, ref v4);

                    // Put remaining bytes to buffer
                    BufferUtilities.BlockCopyUnsafe(array, l, array, 0, r);
                    offset = r;
                }

                // Process the last chunk
                UInt32 h32 = UnsafeFinal(array, offset, ref v1, ref v2, ref v3, ref v4, length, seed);

                return h32;
            }
            finally
            {
                // Free memory
                ArrayPool<Byte>.Shared.Return(array, true);
            }
        }
    }
}