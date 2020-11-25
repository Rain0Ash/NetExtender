// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using NetExtender.Utils.Types;
using NetExtender.Crypto.Compression;

namespace NetExtender.Crypto
{
    public enum CompressionType
    {
        GZip,
        Deflate,
        Brotli
    }
    
    public static partial class Cryptography
    {
        public static String Compress(this String input, CompressionType type)
        {
            return Compress(input, Compression.DefaultCompressionLevel, type);
        }
        
        public static String Compress(this String input, CompressionLevel level = Compression.DefaultCompressionLevel, CompressionType type = Compression.DefaultCompressionType)
        {
            return Compress(input.ToBytes(), level, type).GetStringFromBytes();
        }
        
        public static String Decompress(this String input, CompressionType type = Compression.DefaultCompressionType)
        {
            return Decompress(input.ToBytes(), type).GetStringFromBytes();
        }
        
        public static Byte[] Compress(this Byte[] data, CompressionType type)
        {
            return Compress(data, Compression.DefaultCompressionLevel, type);
        }
        
        public static Byte[] Compress(this Byte[] data, CompressionLevel level = Compression.DefaultCompressionLevel, CompressionType type = Compression.DefaultCompressionType)
        {
            using MemoryStream stream = new MemoryStream();
            Compress(data.ToStream(), stream, level, type);
            return stream.ToArray();
        }
        
        public static Task<Byte[]> CompressAsync(this Byte[] data, CompressionType type)
        {
            return CompressAsync(data, Compression.DefaultCompressionLevel, type);
        }
        
        public static async Task<Byte[]> CompressAsync(this Byte[] data, CompressionLevel level = Compression.DefaultCompressionLevel, CompressionType type = Compression.DefaultCompressionType)
        {
            await using MemoryStream stream = new MemoryStream();
            await CompressAsync(data.ToStream(), stream, level, type).ConfigureAwait(false);
            return stream.ToArray();
        }
        
        public static Byte[] Decompress(this Byte[] data, CompressionType type = Compression.DefaultCompressionType)
        {
            using MemoryStream stream = new MemoryStream();
            Decompress(data.ToStream(), stream, type);
            return stream.ToArray();
        }

        public static async Task<Byte[]> DecompressAsync(this Byte[] data, CompressionType type = Compression.DefaultCompressionType)
        {
            await using MemoryStream stream = new MemoryStream();
            await DecompressAsync(data.ToStream(), stream, type).ConfigureAwait(false);
            return stream.ToArray();
        }
        
        public static Stream Compress(this Stream stream, CompressionType type = Compression.DefaultCompressionType)
        {
            return Compress(stream, Compression.DefaultCompressionLevel, type);
        }
        
        public static Stream Compress(this Stream stream, CompressionLevel level = Compression.DefaultCompressionLevel, CompressionType type = Compression.DefaultCompressionType)
        {
            return Compress(stream, null, level, type);
        }

        public static Stream Compress(this Stream stream, Stream destination, CompressionLevel level = Compression.DefaultCompressionLevel, CompressionType type = Compression.DefaultCompressionType)
        {
            using CompressionStream compress = new CompressionStream(destination ??= new MemoryStream(), type, level);
            stream.CopyTo(compress);
            return destination;
        }
        
        public static Task<Stream> CompressAsync(this Stream stream, CompressionType type = Compression.DefaultCompressionType)
        {
            return CompressAsync(stream, Compression.DefaultCompressionLevel, type);
        }
        
        public static Task<Stream> CompressAsync(this Stream stream, CompressionLevel level = Compression.DefaultCompressionLevel, CompressionType type = Compression.DefaultCompressionType)
        {
            return CompressAsync(stream, null, level, type);
        }

        public static async Task<Stream> CompressAsync(this Stream stream, Stream destination, CompressionLevel level = Compression.DefaultCompressionLevel, CompressionType type = Compression.DefaultCompressionType)
        {
            await using CompressionStream compress = new CompressionStream(destination ??= new MemoryStream(), type, level);
            await stream.CopyToAsync(compress).ConfigureAwait(false);
            return destination;
        }
        
        public static Stream Decompress(this Stream stream, CompressionType type = Compression.DefaultCompressionType)
        {
            return Decompress(stream, null, type);
        }

        public static Stream Decompress(this Stream stream, Stream destination, CompressionType type = Compression.DefaultCompressionType)
        {
            using CompressionStream compress = new CompressionStream(stream, type, CompressionMode.Decompress);
            compress.CopyTo(destination ??= new MemoryStream());
            return destination;
        }
        
        public static Task<Stream> DecompressAsync(this Stream stream, CompressionType type = Compression.DefaultCompressionType)
        {
            return DecompressAsync(stream, null, type);
        }

        public static async Task<Stream> DecompressAsync(this Stream stream, Stream destination, CompressionType type = Compression.DefaultCompressionType)
        {
            await using CompressionStream compress = new CompressionStream(stream, type, CompressionMode.Decompress);
            await compress.CopyToAsync(destination ??= new MemoryStream()).ConfigureAwait(false);
            return destination;
        }
        
        public static class Compression
        {
            public const CompressionType DefaultCompressionType = CompressionType.GZip;
            public const CompressionLevel DefaultCompressionLevel = CompressionLevel.Optimal;
        }
    }
}