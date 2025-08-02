// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Cryptography.Hash.XXHash
{
    public static partial class XXHash64
    {
        /// <summary>
        /// Compute xxHash for the async stream
        /// </summary>
        /// <param name="stream">The stream of data</param>
        /// <param name="buffer">The buffer size</param>
        /// <param name="seed">The seed number</param>
        /// <returns>The hash</returns>
        public static ValueTask<UInt64> ComputeHashAsync(Stream stream, Int32 buffer = BufferUtilities.DefaultBuffer * 2, UInt64 seed = 0)
        {
            return ComputeHashAsync(stream, buffer, seed, CancellationToken.None);
        }

        /// <summary>
        /// Compute xxHash for the async stream
        /// </summary>
        /// <param name="stream">The stream of data</param>
        /// <param name="buffer">The buffer size</param>
        /// <param name="seed">The seed number</param>
        /// <param name="token">The cancelation token</param>
        /// <returns>The hash</returns>
        public static async ValueTask<UInt64> ComputeHashAsync(Stream stream, Int32 buffer, UInt64 seed, CancellationToken token)
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
            Byte[] array = ArrayPool<Byte>.Shared.Rent(buffer + 32);

            Int32 offset = 0;
            Int64 length = 0;

            // Prepare the seed vector
            UInt64 v1 = seed + P1 + P2;
            UInt64 v2 = seed + P2;
            UInt64 v3 = seed + 0;
            UInt64 v4 = seed - P1;

            try
            {
                // Read flow of bytes
                Int32 read;
                while ((read = await stream.ReadAsync(array, offset, buffer, token).ConfigureAwait(false)) > 0)
                {   
                    // Exit if the operation is canceled
                    if (token.IsCancellationRequested)
                    {
                        return await Task.FromCanceled<UInt64>(token).ConfigureAwait(false);
                    }

                    length += read;
                    offset += read;

                    if (offset < 32)
                    {
                        continue;
                    }

                    Int32 r = offset % 32; // remain
                    Int32 l = offset - r;  // length

                    // Process the next chunk 
                    UnsafeAlign(array, l, ref v1, ref v2, ref v3, ref v4);

                    // Put remaining bytes to buffer
                    BufferUtilities.BlockCopyUnsafe(array, l, array, 0, r);
                    offset = r;
                }

                // Process the final chunk
                UInt64 h64 = UnsafeFinal(array, offset, ref v1, ref v2, ref v3, ref v4, length, seed);

                return h64;
            }
            finally
            {
                // Free memory
                ArrayPool<Byte>.Shared.Return(array, true);
            }
        }
    }
}
