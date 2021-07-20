// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Streams;

namespace NetExtender.Utils.Types
{
    public static class StreamUtils
    {
        public static Char ReadChar(this Stream stream)
        {
            return ReadChar(stream, Encoding.UTF32);
        }

        internal static Char ReadChar(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Stream not support reading");
            }
            
            encoding ??= Encoding.UTF32;
            
            Span<Byte> first = stackalloc Byte[1];
            if (stream.Read(first) != 1)
            {
                throw new EndOfStreamException();
            }

            Span<Char> symbol = stackalloc Char[1];
            
            if (first[0] <= 0x7F)
            {
                if (encoding.GetChars(first, symbol) != 1)
                {
                    throw new InvalidOperationException();
                }

                return symbol[0];
            }

            Int32 remaining = (first[0] & 240) == 240 ? 3 : (first[0] & 224) == 224 ? 2 : (first[0] & 192) == 192 ? 1 : -1;
            
            if (remaining <= 0)
            {
                throw new InvalidOperationException($"Invalid {encoding.EncodingName} char sequence.");
            }

            Span<Byte> buffer = stackalloc Byte[remaining + 1];
            buffer[0] = first[0];

            if (stream.Read(buffer.Slice(1)) != remaining)
            {
                throw new EndOfStreamException();
            }

            if (encoding.GetChars(buffer, symbol) != 1)
            {
                throw new InvalidOperationException();
            }

            return symbol[0];
        }

        public static Char? TryReadChar(this Stream stream)
        {
            return TryReadChar(stream, Encoding.UTF32);
        }

        internal static Char? TryReadChar(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Stream not support reading");
            }

            try
            {
                return ReadChar(stream, encoding);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<Char> ReadCharSequence(this Stream stream)
        {
            return ReadCharSequence(stream, Encoding.UTF32);
        }

        internal static IEnumerable<Char> ReadCharSequence(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            while (stream.TryReadChar(encoding) is Char symbol)
            {
                yield return symbol;
            }
        }

        public static void CopyStream(this Stream input, Stream output)
        {
            CopyStream(input, output, 0);
        }

        public static void CopyStream(this Stream input, Stream output, Int32? position)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            Byte[] buffer = new Byte[BufferUtils.DefaultBuffer];

            if (position.HasValue && input.CanSeek)
            {
                input.Position = position.Value;
            }

            Int32 read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static Task CopyStreamAsync(this Stream input, Stream output)
        {
            return CopyStreamAsync(input, output, CancellationToken.None);
        }
        
        public static Task CopyStreamAsync(this Stream input, Stream output, CancellationToken token)
        {
            return CopyStreamAsync(input, output, 0, token);
        }

        public static Task CopyStreamAsync(this Stream input, Stream output, IProgress<Int64>? progress, CancellationToken token)
        {
            return CopyStreamAsync(input, output, 0, progress, token);
        }
        
        public static Task CopyStreamAsync(this Stream input, Stream output, Int32? position)
        {
            return CopyStreamAsync(input, output, position, CancellationToken.None);
        }

        public static Task CopyStreamAsync(this Stream input, Stream output, Int32? position, CancellationToken token)
        {
            return CopyStreamAsync(input, output, position, null, token);
        }
        
        public static Task CopyStreamAsync(this Stream input, Stream output, Int32? position, IProgress<Int64>? progress)
        {
            return CopyStreamAsync(input, output, position, progress, CancellationToken.None);
        }

        public static async Task CopyStreamAsync(this Stream input, Stream output, Int32? position, IProgress<Int64>? progress, CancellationToken token)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            Byte[] buffer = new Byte[BufferUtils.DefaultBuffer];

            if (position.HasValue && input.CanSeek)
            {
                input.Position = position.Value;
            }

            Int32 read;
            Int64 already = 0;
            while ((read = await input.ReadAsync(buffer.AsMemory(0, buffer.Length), token).ConfigureAwait(false)) > 0)
            {
                await output.WriteAsync(buffer.AsMemory(0, read), token).ConfigureAwait(false);

                already += read;
                progress?.Report(already);
            }
        }

        public static void CopyTo(this Stream input, Stream output, Int32 bufferSize = BufferUtils.DefaultBuffer, Int32 position = 0)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (input.CanSeek)
            {
                input.Position = position;
            }

            input.CopyTo(output, bufferSize);
        }

        public static void StartCopyTo(this Stream input, Stream output, Int32 bufferSize = BufferUtils.DefaultBuffer)
        {
            CopyTo(input, output, bufferSize);
        }

        public static Task CopyToAsync(this Stream input, Stream output, Int32 bufferSize, Int32 position)
        {
            return CopyToAsync(input, output, bufferSize, position, CancellationToken.None);
        }

        public static Task CopyToAsync(this Stream input, Stream output, Int32 bufferSize, Int32 position, CancellationToken token)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (input.CanSeek)
            {
                input.Position = position;
            }

            return input.CopyToAsync(output, bufferSize, token);
        }

        public static Task StartCopyToAsync(this Stream input, Stream output)
        {
            return StartCopyToAsync(input, output, CancellationToken.None);
        }

        public static Task StartCopyToAsync(this Stream input, Stream output, CancellationToken token)
        {
            return StartCopyToAsync(input, output, BufferUtils.DefaultBuffer, token);
        }

        public static Task StartCopyToAsync(this Stream input, Stream output, Int32 bufferSize)
        {
            return StartCopyToAsync(input, output, bufferSize, CancellationToken.None);
        }

        public static Task StartCopyToAsync(this Stream input, Stream output, Int32 bufferSize, CancellationToken token)
        {
            return CopyToAsync(input, output, bufferSize, 0, token);
        }
        
        public static BandwidthStream Bandwidth(this Stream stream, Int32 speed, InformationSize size)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BandwidthStream(stream, speed, size);
        }
        
        public static BandwidthStream<T> Bandwidth<T>(this T stream, Int32 speed, InformationSize size) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BandwidthStream<T>(stream, speed, size);
        }
        
        public static BandwidthStream Bandwidth(this Stream stream, UInt64 speed = UInt64.MaxValue, InformationSize size = InformationSize.Byte)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            
            return new BandwidthStream(stream, speed, size);
        }
        
        public static BandwidthStream<T> Bandwidth<T>(this T stream, UInt64 speed = UInt64.MaxValue, InformationSize size = InformationSize.Byte) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            
            return new BandwidthStream<T>(stream, speed, size);
        }

        public static MemoryStream ToStream(this String str)
        {
            return ToStream(str, Encoding.UTF8);
        }

        public static MemoryStream ToStream(this String str, Encoding? encoding)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return ToStream((encoding ?? Encoding.UTF8).GetBytes(str));
        }

        public static Stream ToStream(this String str, Stream output)
        {
            return ToStream(str, output, Encoding.UTF8);
        }

        public static Stream ToStream(this String str, Stream output, Encoding? encoding)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return ToStream((encoding ?? Encoding.UTF8).GetBytes(str), output);
        }

        public static MemoryStream ToStream(this Byte[] bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return new MemoryStream(bytes);
        }

        public static Stream ToStream(this Byte[] bytes, Stream? output)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            using MemoryStream input = new MemoryStream(bytes);
            output ??= new MemoryStream();

            CopyStream(input, output);
            return output;
        }
        
        public static Task<MemoryStream> ToStreamAsync(this Byte[] bytes)
        {
            return Task.FromResult(new MemoryStream(bytes));
        }

        public static Task<Stream> ToStreamAsync(this Byte[] bytes, Stream? output)
        {
            return ToStreamAsync(bytes, output, CancellationToken.None);
        }

        public static async Task<Stream> ToStreamAsync(this Byte[] bytes, Stream? output, CancellationToken token)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            await using MemoryStream input = new MemoryStream(bytes);
            output ??= new MemoryStream();

            await CopyStreamAsync(input, output, token).ConfigureAwait(false);
            return output;
        }

        public static Task<Int32> ReadAsync(this Stream stream, Byte[] buffer, CancellationToken token)
        {
            return ReadAsync(stream, buffer, 0, token);
        }

        public static Task<Int32> ReadAsync(this Stream stream, Byte[] buffer, Int32 offset = 0)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            return stream.ReadAsync(buffer, offset, buffer.Length);
        }

        public static Task<Int32> ReadAsync(this Stream stream, Byte[] buffer, Int32 offset, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            return stream.ReadAsync(buffer, offset, buffer.Length, token);
        }

        public static String ConvertToString(this Stream stream)
        {
            return ConvertToString(stream, Encoding.UTF8);
        }

        public static String ConvertToString(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public static Task<String> ConvertToStringAsync(this Stream stream)
        {
            return ConvertToStringAsync(stream, Encoding.UTF8);
        }

        public static Task<String> ConvertToStringAsync(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8);
            return reader.ReadToEndAsync();
        }

        public static T SetPosition<T>(this T stream) where T : Stream
        {
            return ResetPosition(stream);
        }
        
        public static T SetPosition<T>(this T stream, Int64 position) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            stream.Position = position;
            return stream;
        }

        public static Boolean TrySetPosition(this Stream stream, Int64 position)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                if (!stream.CanSeek)
                {
                    return false;
                }
                
                stream.Position = position;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static T ResetPosition<T>(this T stream) where T : Stream
        {
            return SetPosition(stream, 0);
        }
        
        public static T TryResetPosition<T>(this T stream) where T : Stream
        {
            return TryResetPosition(stream, out _);
        }
        
        public static T TryResetPosition<T>(this T stream, out Boolean successful) where T : Stream
        {
            successful = TrySetPosition(stream, 0);
            return stream;
        }

        public static T SeekPosition<T>(this T stream, Int64 offset) where T : Stream
        {
            return SeekPosition(stream, offset, SeekOrigin.Begin);
        }

        public static T SeekPosition<T>(this T stream, Int64 offset, SeekOrigin origin) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            stream.Seek(offset, origin);
            return stream;
        }

        public static Boolean TrySeek(this Stream stream, Int64 offset)
        {
            return TrySeek(stream, offset, SeekOrigin.Begin);
        }

        public static Boolean TrySeek(this Stream stream, Int64 offset, SeekOrigin origin)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                if (!stream.CanSeek)
                {
                    return false;
                }
                
                stream.Seek(offset, origin);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private const Int32 DefaultBufferSize = 1024;
        
        /// <summary>
        /// Wraps <paramref name="stream" /> with <see cref="StreamReader" />.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="StreamReader" /> object is disposed; otherwise, false.</param>
        public static StreamReader ToStreamReader(this Stream stream, Encoding? encoding = null, Boolean leaveOpen = false)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new StreamReader(stream, encoding ?? Encoding.UTF8, true, DefaultBufferSize, leaveOpen);
        }

        /// <summary>
        /// Wraps <paramref name="stream"/> with <see cref="BinaryReader"/>.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="BinaryReader" /> object is disposed; otherwise, false.</param>
        public static BinaryReader ToBinaryReader(this Stream stream, Encoding? encoding = null, Boolean leaveOpen = false)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BinaryReader(stream, encoding ?? Encoding.UTF8, leaveOpen);
        }

        /// <summary>
        /// Wraps <paramrefref name="stream"/> with <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="StreamWriter" /> object is disposed; otherwise, false.</param>
        public static StreamWriter ToStreamWriter(this Stream stream, Encoding? encoding = null, Boolean leaveOpen = false)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new StreamWriter(stream, encoding ?? Encoding.UTF8, DefaultBufferSize, leaveOpen);
        }

        /// <summary>
        /// Wraps <paramref name="stream"/> with <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="BinaryWriter" /> object is disposed; otherwise, false.</param>
        public static BinaryWriter ToBinaryWriter(this Stream stream, Encoding? encoding = null, Boolean leaveOpen = false)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BinaryWriter(stream, encoding ?? Encoding.UTF8, leaveOpen);
        }

        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static String ReadAsString(this Stream stream, Encoding? encoding = null)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamReader reader = stream.ToStreamReader(encoding, true);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static String[] ReadAsLines(this Stream stream, Encoding? encoding = null)
        {
            return ReadAsSequential(stream, encoding).ToArray();
        }
        
        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static IEnumerable<String> ReadAsSequential(this Stream stream, Encoding? encoding = null)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamReader reader = stream.ToStreamReader(encoding, true);

            String? line;
            while((line = reader.ReadLine()) is not null)
            {
                yield return line;
            }
        }

        /// <summary>
        /// Returns content of the stream as a string.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static async Task<String> ReadAsStringAsync(this Stream stream, Encoding? encoding = null)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamReader reader = stream.ToStreamReader(encoding, true);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public static Byte[] ReadAsByteArray(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using BinaryReader reader = stream.ToBinaryReader(null, true);
            
            Int32 count = checked((Int32) (stream.Length - stream.Position));
            
            return reader.ReadBytes(count);
        }

        public static Stream AsSeekableStream(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return stream.CanSeek ? stream : new SeekableStream(stream);
        }
    }
}