// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NetExtender.Types.Streams;

namespace NetExtender.Utils.Types
{
    public static class StreamUtils
    {
        private const Int32 DefaultPosition = 0;

        public static void CopyStream(this Stream input, Stream output, Int32? position = DefaultPosition)
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

        public static Task CopyStreamAsync(this Stream input, Stream output, Int32? position = DefaultPosition, IProgress<Int64> progress = null)
        {
            return CopyStreamAsync(input, output, position, progress, CancellationToken.None);
        }

        public static Task CopyStreamAsync(this Stream input, Stream output, CancellationToken token)
        {
            return CopyStreamAsync(input, output, DefaultPosition, token);
        }

        public static Task CopyStreamAsync(this Stream input, Stream output, IProgress<Int64> progress, CancellationToken token)
        {
            return CopyStreamAsync(input, output, DefaultPosition, progress, token);
        }

        public static Task CopyStreamAsync(this Stream input, Stream output, Int32? position, CancellationToken token)
        {
            return CopyStreamAsync(input, output, position, null, token);
        }

        public static async Task CopyStreamAsync(this Stream input, Stream output, Int32? position, IProgress<Int64> progress, CancellationToken token)
        {
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

        public static void CopyTo([NotNull] this Stream input, Stream output, Int32 bufferSize = BufferUtils.DefaultBuffer, Int32 position = 0)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.CanSeek)
            {
                input.Position = position;
            }

            input.CopyTo(output, bufferSize);
        }

        public static void StartCopyTo([NotNull] this Stream input, Stream output, Int32 bufferSize = BufferUtils.DefaultBuffer)
        {
            CopyTo(input, output, bufferSize);
        }

        public static Task CopyToAsync([NotNull] this Stream input, Stream output, Int32 bufferSize, Int32 position)
        {
            return CopyToAsync(input, output, bufferSize, position, CancellationToken.None);
        }

        public static Task CopyToAsync([NotNull] this Stream input, Stream output, Int32 bufferSize, Int32 position, CancellationToken token)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.CanSeek)
            {
                input.Position = position;
            }

            return input.CopyToAsync(output, bufferSize, token);
        }

        public static Task StartCopyToAsync([NotNull] this Stream input, Stream output)
        {
            return StartCopyToAsync(input, output, CancellationToken.None);
        }

        public static Task StartCopyToAsync([NotNull] this Stream input, Stream output, CancellationToken token)
        {
            return StartCopyToAsync(input, output, BufferUtils.DefaultBuffer, token);
        }

        public static Task StartCopyToAsync([NotNull] this Stream input, Stream output, Int32 bufferSize)
        {
            return StartCopyToAsync(input, output, bufferSize, CancellationToken.None);
        }

        public static Task StartCopyToAsync([NotNull] this Stream input, Stream output, Int32 bufferSize, CancellationToken token)
        {
            return CopyToAsync(input, output, bufferSize, 0, token);
        }
        
        public static BandwidthStream Bandwidth([NotNull] this Stream stream, Int32 speed, InformationSize size)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BandwidthStream(stream, speed, size);
        }
        
        public static BandwidthStream<T> Bandwidth<T>([NotNull] this T stream, Int32 speed, InformationSize size) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BandwidthStream<T>(stream, speed, size);
        }
        
        public static BandwidthStream Bandwidth([NotNull] this Stream stream, UInt64 speed = UInt64.MaxValue, InformationSize size = InformationSize.Byte)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            
            return new BandwidthStream(stream, speed, size);
        }
        
        public static BandwidthStream<T> Bandwidth<T>([NotNull] this T stream, UInt64 speed = UInt64.MaxValue, InformationSize size = InformationSize.Byte) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            
            return new BandwidthStream<T>(stream, speed, size);
        }

        public static MemoryStream ToStream([NotNull] this String str)
        {
            return ToStream(str, Encoding.UTF8);
        }

        public static MemoryStream ToStream([NotNull] this String str, Encoding encoding)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return ToStream((encoding ?? Encoding.UTF8).GetBytes(str));
        }

        public static Stream ToStream([NotNull] this String str, Stream output)
        {
            return ToStream(str, output, Encoding.UTF8);
        }

        public static Stream ToStream([NotNull] this String str, Stream output, Encoding encoding)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return ToStream((encoding ?? Encoding.UTF8).GetBytes(str), output);
        }

        public static MemoryStream ToStream([NotNull] this Byte[] bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return new MemoryStream(bytes);
        }

        public static Stream ToStream([NotNull] this Byte[] bytes, Stream output)
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
        
        public static async Task<MemoryStream> ToStreamAsync([NotNull] this Byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static Task<Stream> ToStreamAsync([NotNull] this Byte[] bytes, Stream? output = null)
        {
            return ToStreamAsync(bytes, output, CancellationToken.None);
        }

        public static async Task<Stream> ToStreamAsync([NotNull] this Byte[] bytes, Stream? output, CancellationToken token)
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

        public static Task<Int32> ReadAsync([NotNull] this Stream stream, Byte[] buffer, CancellationToken token)
        {
            return ReadAsync(stream, buffer, 0, token);
        }

        public static Task<Int32> ReadAsync([NotNull] this Stream stream, Byte[] buffer, Int32 offset = 0)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return stream.ReadAsync(buffer, offset, buffer.Length);
        }

        public static Task<Int32> ReadAsync([NotNull] this Stream stream, Byte[] buffer, Int32 offset, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return stream.ReadAsync(buffer, offset, buffer.Length, token);
        }

        public static String ConvertToString([NotNull] this Stream stream, Encoding encoding = null)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8);
            return reader.ReadToEnd();
        }
        
        public static Task<String> ConvertToStringAsync([NotNull] this Stream stream, Encoding encoding = null)
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
        
        public static T SetPosition<T>([NotNull] this T stream, Int64 position) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            stream.Position = position;
            return stream;
        }

        public static Boolean TrySetPosition([NotNull] this Stream stream, Int64 position)
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
        
        public static T SeekPosition<T>([NotNull] this T stream, Int64 offset, SeekOrigin origin = SeekOrigin.Begin) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            stream.Seek(offset, origin);
            return stream;
        }
        
        public static Boolean TrySeek([NotNull] this Stream stream, Int64 offset, SeekOrigin origin = SeekOrigin.Begin)
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
        [NotNull]
        public static StreamReader ToStreamReader([NotNull] this Stream stream, Encoding encoding, Boolean leaveOpen = false)
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
        [NotNull]
        public static BinaryReader ToBinaryReader([NotNull] this Stream stream, Encoding encoding = null, Boolean leaveOpen = false)
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
        [NotNull]
        public static StreamWriter ToStreamWriter([NotNull] this Stream stream, Encoding? encoding = null, Boolean leaveOpen = false)
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
        [NotNull]
        public static BinaryWriter ToBinaryWriter([NotNull] this Stream stream, Encoding? encoding = null, Boolean leaveOpen = false)
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
        [NotNull]
        public static String ReadAsString([NotNull] this Stream stream, [CanBeNull] Encoding encoding = null)
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
        [NotNull]
        public static String[] ReadAsLines([NotNull] this Stream stream, [CanBeNull] Encoding encoding = null)
        {
            return ReadAsSequential(stream, encoding).ToArray();
        }
        
        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        [NotNull]
        public static IEnumerable<String> ReadAsSequential([NotNull] this Stream stream, [CanBeNull] Encoding encoding = null)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamReader reader = stream.ToStreamReader(encoding, true);

            String line;
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
        [NotNull]
        public static async Task<String> ReadAsStringAsync([NotNull] this Stream stream, [CanBeNull] Encoding encoding = null)
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
        [NotNull]
        public static Byte[] ReadAsByteArray([NotNull] this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using BinaryReader reader = stream.ToBinaryReader(null, true);
            
            Int32 count = checked((Int32) (stream.Length - stream.Position));
            
            return reader.ReadBytes(count);
        }
    }
}