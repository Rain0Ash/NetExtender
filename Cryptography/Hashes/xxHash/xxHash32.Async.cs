﻿using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
 using NetExtender.Utils.Types;

 namespace NetExtender.Crypto.Hashes.XXHash
{
    public static partial class XXHash32
    {
        /// <summary>
        /// Compute xxHash for the async stream
        /// </summary>
        /// <param name="stream">The stream of data</param>
        /// <param name="bufferSize">The buffer size</param>
        /// <param name="seed">The seed number</param>
        /// <returns>The hash</returns>
        public static ValueTask<UInt32> ComputeHashAsync(Stream stream, Int32 bufferSize = BufferUtils.DefaultBuffer, UInt32 seed = 0)
        {
            return ComputeHashAsync(stream, bufferSize, seed, CancellationToken.None);
        }
        
        
        /// <summary>
        /// Compute xxHash for the async stream
        /// </summary>
        /// <param name="stream">The stream of data</param>
        /// <param name="bufferSize">The buffer size</param>
        /// <param name="seed">The seed number</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The hash</returns>
        public static async ValueTask<UInt32> ComputeHashAsync(Stream stream, Int32 bufferSize, UInt32 seed, CancellationToken token)
        {
            // Optimizing memory allocation
            Byte[] buffer = ArrayPool<Byte>.Shared.Rent(bufferSize + 16);

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
                Int32 readBytes;
                while ((readBytes = await stream.ReadAsync(buffer, offset, bufferSize, token).ConfigureAwait(false)) > 0)
                {
                    // Exit if the operation is canceled
                    if (token.IsCancellationRequested)
                    {
                        return await Task.FromCanceled<UInt32>(token).ConfigureAwait(false);
                    }
                    
                    length += readBytes;
                    offset += readBytes;

                    if (offset < 16)
                    {
                        continue;
                    }

                    Int32 r = offset % 16; // remain
                    Int32 l = offset - r;  // length

                    // Process the next chunk 
                    UnsafeAlign(buffer, l, ref v1, ref v2, ref v3, ref v4);

                    // Put remaining bytes to buffer
                    UnsafeBuffer.BlockCopy(buffer, l, buffer, 0, r);
                    offset = r;
                }

                // Process the final chunk
                UInt32 h32 = UnsafeFinal(buffer, offset, ref v1, ref v2, ref v3, ref v4, length, seed);

                return h32;
            }
            finally
            {
                // Free memory
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }
    }
}
