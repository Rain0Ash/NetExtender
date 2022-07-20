// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Cryptography.Compression;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Cryptography
{
    public enum CompressionType
    {
        GZip,
        Deflate,
        Brotli
    }
    
    public static partial class CryptographyUtilities
    {
        public static class Compression
        {
            public const CompressionType DefaultCompressionType = CompressionType.GZip;
            public const CompressionLevel DefaultCompressionLevel = CompressionLevel.Optimal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Compress(this String value)
        {
            return Compress(value, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Compress(this String value, CompressionType type)
        {
            return Compress(value, Compression.DefaultCompressionLevel, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Compress(this String value, CompressionLevel level)
        {
            return Compress(value, level, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Compress(this String value, CompressionType type, CompressionLevel level)
        {
            return Compress(value, level, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Compress(this String value, CompressionLevel level, CompressionType type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Compress(value.ToBytes(), level, type).GetStringFromBytes();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decompress(this String value)
        {
            return Decompress(value, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decompress(this String value, CompressionType type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Decompress(value.ToBytes(), type).GetStringFromBytes();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Compress(this Byte[] value)
        {
            return Compress(value, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Compress(this Byte[] value, CompressionType type)
        {
            return Compress(value, Compression.DefaultCompressionLevel, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Compress(this Byte[] value, CompressionLevel level)
        {
            return Compress(value, level, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Compress(this Byte[] value, CompressionType type, CompressionLevel level)
        {
            return Compress(value, level, type);
        }

        public static Byte[] Compress(this Byte[] value, CompressionLevel level, CompressionType type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using MemoryStream stream = new MemoryStream();
            Compress(value.ToStream(), stream, level, type);
            return stream.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Decompress(this Byte[] value)
        {
            return Decompress(value, Compression.DefaultCompressionType);
        }

        public static Byte[] Decompress(this Byte[] value, CompressionType type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using MemoryStream stream = new MemoryStream();
            Decompress(value.ToStream(), stream, type);
            return stream.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> CompressAsync(this Byte[] value, CompressionType type)
        {
            return CompressAsync(value, type, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> CompressAsync(this Byte[] value, CompressionType type, CancellationToken token)
        {
            return CompressAsync(value, Compression.DefaultCompressionLevel, type, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> CompressAsync(this Byte[] value, CompressionLevel level)
        {
            return CompressAsync(value, level, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> CompressAsync(this Byte[] value, CompressionLevel level, CancellationToken token)
        {
            return CompressAsync(value, level, Compression.DefaultCompressionType, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> CompressAsync(this Byte[] value, CompressionType type, CompressionLevel level)
        {
            return CompressAsync(value, type, level, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> CompressAsync(this Byte[] value, CompressionType type, CompressionLevel level, CancellationToken token)
        {
            return CompressAsync(value, level, type, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> CompressAsync(this Byte[] value, CompressionLevel level, CompressionType type)
        {
            return CompressAsync(value, level, type, CancellationToken.None);
        }

        public static async Task<Byte[]> CompressAsync(this Byte[] value, CompressionLevel level, CompressionType type, CancellationToken token)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            await using MemoryStream stream = new MemoryStream();
            await CompressAsync(await value.ToStreamAsync().ConfigureAwait(false), stream, level, type, token).ConfigureAwait(false);
            return stream.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> DecompressAsync(this Byte[] value)
        {
            return DecompressAsync(value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> DecompressAsync(this Byte[] value, CancellationToken token)
        {
            return DecompressAsync(value, Compression.DefaultCompressionType, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Byte[]> DecompressAsync(this Byte[] value, CompressionType type)
        {
            return DecompressAsync(value, type, CancellationToken.None);
        }

        public static async Task<Byte[]> DecompressAsync(this Byte[] value, CompressionType type, CancellationToken token)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            await using MemoryStream stream = new MemoryStream();
            await DecompressAsync(await value.ToStreamAsync().ConfigureAwait(false), stream, type, token).ConfigureAwait(false);
            return stream.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemoryStream Compress(this Stream stream)
        {
            return Compress(stream, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemoryStream Compress(this Stream stream, CompressionType type)
        {
            return Compress(stream, Compression.DefaultCompressionLevel, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemoryStream Compress(this Stream stream, CompressionLevel level)
        {
            return Compress(stream, level, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemoryStream Compress(this Stream stream, CompressionType type, CompressionLevel level)
        {
            return Compress(stream, level, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemoryStream Compress(this Stream stream, CompressionLevel level, CompressionType type)
        {
            return Compress<MemoryStream>(stream, null, level, type).ResetPosition();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Compress<T>(this Stream stream, T? destination) where T : Stream
        {
            return Compress(stream, destination, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Compress<T>(this Stream stream, T? destination, CompressionType type) where T : Stream
        {
            return Compress(stream, destination, Compression.DefaultCompressionLevel, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Compress<T>(this Stream stream, T? destination, CompressionLevel level) where T : Stream
        {
            return Compress(stream, destination, level, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Compress<T>(this Stream stream, T? destination, CompressionType type, CompressionLevel level) where T : Stream
        {
            return Compress(stream, destination, level, type);
        }

        public static T Compress<T>(this Stream stream, T? destination, CompressionLevel level, CompressionType type) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Boolean empty = destination is null;
            
            using CompressionStream compress = new CompressionStream(destination ??= (T)(Object) new MemoryStream(), type, level, true);
            stream.CopyTo(compress);
            
            return empty ? destination.ResetPosition() : destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemoryStream Decompress(this Stream stream)
        {
            return Decompress(stream, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemoryStream Decompress(this Stream stream, CompressionType type)
        {
            return Decompress<MemoryStream>(stream, null, type).ResetPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decompress<T>(this Stream stream, T? destination) where T : Stream
        {
            return Decompress(stream, destination, Compression.DefaultCompressionType);
        }

        public static T Decompress<T>(this Stream stream, T? destination, CompressionType type) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Boolean empty = destination is null;
            
            using CompressionStream compress = new CompressionStream(stream, type, CompressionMode.Decompress);
            compress.CopyTo(destination ??= (T)(Object) new MemoryStream());
            
            return empty ? destination.ResetPosition() : destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream)
        {
            return CompressAsync(stream, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream, CancellationToken token)
        {
            return CompressAsync(stream, Compression.DefaultCompressionType, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream, CompressionType type)
        {
            return CompressAsync(stream, type, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream, CompressionType type, CancellationToken token)
        {
            return CompressAsync(stream, Compression.DefaultCompressionLevel, type, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream, CompressionLevel level)
        {
            return CompressAsync(stream, level, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream, CompressionLevel level, CancellationToken token)
        {
            return CompressAsync(stream, level, Compression.DefaultCompressionType, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream, CompressionType type, CompressionLevel level)
        {
            return CompressAsync(stream, type, level, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream, CompressionType type, CompressionLevel level, CancellationToken token)
        {
            return CompressAsync(stream, level, type, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> CompressAsync(this Stream stream, CompressionLevel level, CompressionType type)
        {
            return CompressAsync(stream, level, type, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<MemoryStream> CompressAsync(this Stream stream, CompressionLevel level, CompressionType type, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return (await CompressAsync<MemoryStream>(stream, null, level, type, token).ConfigureAwait(false)).ResetPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination) where T : Stream
        {
            return CompressAsync(stream, destination, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination, CancellationToken token) where T : Stream
        {
            return CompressAsync(stream, destination, Compression.DefaultCompressionType, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination, CompressionType type) where T : Stream
        {
            return CompressAsync(stream, destination, type, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination, CompressionType type, CancellationToken token) where T : Stream
        {
            return CompressAsync(stream, destination, Compression.DefaultCompressionLevel, type, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination, CompressionLevel level) where T : Stream
        {
            return CompressAsync(stream, destination, level, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination, CompressionLevel level, CancellationToken token) where T : Stream
        {
            return CompressAsync(stream, destination, level, Compression.DefaultCompressionType, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination, CompressionType type, CompressionLevel level) where T : Stream
        {
            return CompressAsync(stream, destination, type, level, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination, CompressionType type, CompressionLevel level, CancellationToken token) where T : Stream
        {
            return CompressAsync(stream, destination, level, type, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> CompressAsync<T>(this Stream stream, T? destination, CompressionLevel level, CompressionType type) where T : Stream
        {
            return CompressAsync(stream, destination, level, type, CancellationToken.None);
        }

        public static async Task<T> CompressAsync<T>(this Stream stream, T? destination, CompressionLevel level, CompressionType type, CancellationToken token) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Boolean empty = destination is null;
            
            await using CompressionStream compress = new CompressionStream(destination ??= (T)(Object) new MemoryStream(), type, level, true);
            await stream.CopyToAsync(compress, token).ConfigureAwait(false);

            return empty ? destination.ResetPosition() : destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> DecompressAsync(this Stream stream)
        {
            return DecompressAsync(stream, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> DecompressAsync(this Stream stream, CompressionType type)
        {
            return DecompressAsync(stream, type, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MemoryStream> DecompressAsync(this Stream stream, CancellationToken token)
        {
            return DecompressAsync(stream, Compression.DefaultCompressionType, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<MemoryStream> DecompressAsync(this Stream stream, CompressionType type, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return (await DecompressAsync<MemoryStream>(stream, null, type, token).ConfigureAwait(false)).ResetPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> DecompressAsync<T>(this Stream stream, T? destination) where T : Stream
        {
            return DecompressAsync(stream, destination, Compression.DefaultCompressionType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> DecompressAsync<T>(this Stream stream, T? destination, CompressionType type) where T : Stream
        {
            return DecompressAsync(stream, destination, type, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> DecompressAsync<T>(this Stream stream, T? destination, CancellationToken token) where T : Stream
        {
            return DecompressAsync(stream, destination, Compression.DefaultCompressionType, token);
        }

        public static async Task<T> DecompressAsync<T>(this Stream stream, T? destination, CompressionType type, CancellationToken token) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Boolean empty = destination is null;
            
            await using CompressionStream compress = new CompressionStream(stream, type, CompressionMode.Decompress, true);
            await compress.CopyToAsync(destination ??= (T)(Object) new MemoryStream(), token).ConfigureAwait(false);
            
            return empty ? destination.ResetPosition() : destination;
        }
    }
}