// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Streams;

namespace NetExtender.Utilities.Types
{
    public static class StreamUtilities
    {
        private static Type SynchronizedStreamType { get; } = Stream.Synchronized(Stream.Null).GetType();

        public static Boolean IsSynchronized(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return stream.GetType() == SynchronizedStreamType;
        }

        public static Stream Synchronize(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return stream.IsSynchronized() ? stream : Stream.Synchronized(stream);
        }

        public static Boolean Read<T>(this Stream stream, out T value) where T : struct
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Int32 size = Unsafe.SizeOf<T>();
            Span<Byte> buffer = stackalloc Byte[size];
            Int32 length = stream.Read(buffer);

            if (length < size)
            {
                value = default;
                return false;
            }

            value = MemoryMarshal.Read<T>(buffer);
            return true;
        }

        public static void Write<T>(this Stream stream, T value) where T : struct
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Int32 size = Unsafe.SizeOf<T>();
            Span<Byte> buffer = stackalloc Byte[size];
            MemoryMarshal.Write(buffer, ref value);
            stream.Write(buffer);
        }

        public static Char ReadChar(this Stream stream)
        {
            return ReadChar(stream, Encoding.UTF8);
        }

        public static Char ReadChar(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new StreamArgumentNotSupportReadException(stream, nameof(stream));
            }

            encoding ??= Encoding.UTF8;

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
            return TryReadChar(stream, Encoding.UTF8);
        }

        public static Char? TryReadChar(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new StreamArgumentNotSupportReadException(stream, nameof(stream));
            }

            encoding ??= Encoding.UTF8;

            Span<Byte> first = stackalloc Byte[1];
            if (stream.Read(first) != 1)
            {
                return null;
            }

            Span<Char> symbol = stackalloc Char[1];

            if (first[0] <= 0x7F)
            {
                if (encoding.GetChars(first, symbol) != 1)
                {
                    return null;
                }

                return symbol[0];
            }

            Int32 remaining = (first[0] & 240) == 240 ? 3 : (first[0] & 224) == 224 ? 2 : (first[0] & 192) == 192 ? 1 : -1;

            if (remaining <= 0)
            {
                return null;
            }

            Span<Byte> buffer = stackalloc Byte[remaining + 1];
            buffer[0] = first[0];

            if (stream.Read(buffer.Slice(1)) != remaining)
            {
                return null;
            }

            if (encoding.GetChars(buffer, symbol) != 1)
            {
                return null;
            }

            return symbol[0];
        }

        public static IEnumerable<Char> ReadCharSequence(this Stream stream)
        {
            return ReadCharSequence(stream, Encoding.UTF8);
        }

        public static IEnumerable<Char> ReadCharSequence(this Stream stream, Encoding? encoding)
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

        public static Char32 ReadChar32(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new StreamArgumentNotSupportReadException(stream, nameof(stream));
            }

            Span<Byte> first = stackalloc Byte[1];
            if (stream.Read(first) != 1)
            {
                throw new EndOfStreamException();
            }

            if (first[0] <= 0x7F)
            {
                return first[0];
            }

            Int32 remaining = (first[0] & 240) == 240 ? 3 : (first[0] & 224) == 224 ? 2 : (first[0] & 192) == 192 ? 1 : -1;

            if (remaining <= 0)
            {
                throw new InvalidOperationException("Invalid sequence.");
            }

            Span<Byte> buffer = stackalloc Byte[remaining + 1];
            buffer[0] = first[0];

            if (stream.Read(buffer.Slice(1)) != remaining)
            {
                throw new EndOfStreamException();
            }

            return BitConverter.ToInt32(buffer);
        }

        public static Char32? TryReadChar32(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new StreamArgumentNotSupportReadException(stream, nameof(stream));
            }

            Span<Byte> first = stackalloc Byte[1];
            if (stream.Read(first) != 1)
            {
                return null;
            }

            if (first[0] <= 0x7F)
            {
                return first[0];
            }

            Int32 remaining = (first[0] & 240) == 240 ? 3 : (first[0] & 224) == 224 ? 2 : (first[0] & 192) == 192 ? 1 : -1;

            if (remaining <= 0)
            {
                return null;
            }

            Span<Byte> buffer = stackalloc Byte[remaining + 1];
            buffer[0] = first[0];

            if (stream.Read(buffer.Slice(1)) != remaining)
            {
                return null;
            }

            return BitConverter.ToUInt32(buffer);
        }

        public static IEnumerable<Char32> ReadChar32Sequence(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            while (stream.TryReadChar32() is Char32 symbol)
            {
                yield return symbol;
            }
        }

#if NETCOREAPP3_1_OR_GREATER
        public static Rune? TryReadRune(this Stream stream)
        {
            return TryReadChar32(stream);
        }

        public static IEnumerable<Rune> ReadRuneSequence(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            while (stream.TryReadRune() is Rune symbol)
            {
                yield return symbol;
            }
        }
#endif

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

            Byte[] buffer = new Byte[BufferUtilities.DefaultBuffer];

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

            Byte[] buffer = new Byte[BufferUtilities.DefaultBuffer];

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

        public static void CopyTo(this Stream input, Stream output, Int32 position, Int32 bufferSize)
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

        public static Task CopyToAsync(this Stream input, Stream output, Int32 position, Int32 bufferSize)
        {
            return CopyToAsync(input, output, bufferSize, position, CancellationToken.None);
        }

        public static Task CopyToAsync(this Stream input, Stream output, Int32 position, Int32 bufferSize, CancellationToken token)
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

        public static BandwidthStream Bandwidth(this Stream stream)
        {
            return Bandwidth(stream, UInt64.MaxValue);
        }

        public static BandwidthStream Bandwidth(this Stream stream, Int32 speed)
        {
            return Bandwidth(stream, speed, InformationSize.Byte);
        }

        public static BandwidthStream Bandwidth(this Stream stream, Int32 speed, InformationSize size)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BandwidthStream(stream, speed, size);
        }

        public static BandwidthStream Bandwidth(this Stream stream, UInt64 speed)
        {
            return Bandwidth(stream, speed, InformationSize.Byte);
        }

        public static BandwidthStream Bandwidth(this Stream stream, UInt64 speed, InformationSize size)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BandwidthStream(stream, speed, size);
        }

        public static BandwidthStream<T> Bandwidth<T>(this T stream) where T : Stream
        {
            return Bandwidth(stream, UInt64.MaxValue);
        }

        public static BandwidthStream<T> Bandwidth<T>(this T stream, Int32 speed) where T : Stream
        {
            return Bandwidth(stream, speed, InformationSize.Byte);
        }

        public static BandwidthStream<T> Bandwidth<T>(this T stream, Int32 speed, InformationSize size) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BandwidthStream<T>(stream, speed, size);
        }

        public static BandwidthStream<T> Bandwidth<T>(this T stream, UInt64 speed) where T : Stream
        {
            return Bandwidth(stream, speed, InformationSize.Byte);
        }

        public static BandwidthStream<T> Bandwidth<T>(this T stream, UInt64 speed, InformationSize size) where T : Stream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BandwidthStream<T>(stream, speed, size);
        }

        public static MemoryStream ToStream(this String value)
        {
            return ToStream(value, Encoding.UTF8);
        }

        public static MemoryStream ToStream(this String value, Encoding? encoding)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return ToStream((encoding ?? Encoding.UTF8).GetBytes(value));
        }

        public static Stream ToStream(this String value, Stream output)
        {
            return ToStream(value, output, Encoding.UTF8);
        }

        public static Stream ToStream(this String value, Stream output, Encoding? encoding)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return ToStream((encoding ?? Encoding.UTF8).GetBytes(value), output);
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

        public static Boolean TryPosition(this Stream stream, out Int64 position)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                if (!stream.CanSeek)
                {
                    position = 0;
                    return false;
                }

                position = stream.Position;
                return true;
            }
            catch (Exception)
            {
                position = 0;
                return false;
            }
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
        public static StreamReader ToStreamReader(this Stream stream)
        {
            return ToStreamReader(stream, null, true);
        }

        /// <summary>
        /// Wraps <paramref name="stream" /> with <see cref="StreamReader" />.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static StreamReader ToStreamReader(this Stream stream, Encoding? encoding)
        {
            return ToStreamReader(stream, encoding, true);
        }

        /// <summary>
        /// Wraps <paramref name="stream" /> with <see cref="StreamReader" />.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="StreamReader" /> object is disposed; otherwise, false.</param>
        public static StreamReader ToStreamReader(this Stream stream, Boolean leaveOpen)
        {
            return ToStreamReader(stream, null, leaveOpen);
        }

        /// <summary>
        /// Wraps <paramref name="stream" /> with <see cref="StreamReader" />.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="StreamReader" /> object is disposed; otherwise, false.</param>
        public static StreamReader ToStreamReader(this Stream stream, Encoding? encoding, Boolean leaveOpen)
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
        public static BinaryReader ToBinaryReader(this Stream stream)
        {
            return ToBinaryReader(stream, null, true);
        }

        /// <summary>
        /// Wraps <paramref name="stream"/> with <see cref="BinaryReader"/>.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static BinaryReader ToBinaryReader(this Stream stream, Encoding? encoding)
        {
            return ToBinaryReader(stream, encoding, true);
        }

        /// <summary>
        /// Wraps <paramref name="stream"/> with <see cref="BinaryReader"/>.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="BinaryReader" /> object is disposed; otherwise, false.</param>
        public static BinaryReader ToBinaryReader(this Stream stream, Boolean leaveOpen)
        {
            return ToBinaryReader(stream, null, leaveOpen);
        }

        /// <summary>
        /// Wraps <paramref name="stream"/> with <see cref="BinaryReader"/>.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="BinaryReader" /> object is disposed; otherwise, false.</param>
        public static BinaryReader ToBinaryReader(this Stream stream, Encoding? encoding, Boolean leaveOpen)
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
        public static StreamWriter ToStreamWriter(this Stream stream)
        {
            return ToStreamWriter(stream, null, true);
        }

        /// <summary>
        /// Wraps <paramrefref name="stream"/> with <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static StreamWriter ToStreamWriter(this Stream stream, Encoding? encoding)
        {
            return ToStreamWriter(stream, encoding, true);
        }

        /// <summary>
        /// Wraps <paramrefref name="stream"/> with <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="StreamWriter" /> object is disposed; otherwise, false.</param>
        public static StreamWriter ToStreamWriter(this Stream stream, Boolean leaveOpen)
        {
            return ToStreamWriter(stream, null, leaveOpen);
        }

        /// <summary>
        /// Wraps <paramrefref name="stream"/> with <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="StreamWriter" /> object is disposed; otherwise, false.</param>
        public static StreamWriter ToStreamWriter(this Stream stream, Encoding? encoding, Boolean leaveOpen)
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
        public static BinaryWriter ToBinaryWriter(this Stream stream)
        {
            return ToBinaryWriter(stream, null, true);
        }

        /// <summary>
        /// Wraps <paramref name="stream"/> with <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static BinaryWriter ToBinaryWriter(this Stream stream, Encoding? encoding)
        {
            return ToBinaryWriter(stream, encoding, true);
        }

        /// <summary>
        /// Wraps <paramref name="stream"/> with <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="BinaryWriter" /> object is disposed; otherwise, false.</param>
        public static BinaryWriter ToBinaryWriter(this Stream stream, Boolean leaveOpen)
        {
            return ToBinaryWriter(stream, null, leaveOpen);
        }

        /// <summary>
        /// Wraps <paramref name="stream"/> with <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="BinaryWriter" /> object is disposed; otherwise, false.</param>
        public static BinaryWriter ToBinaryWriter(this Stream stream, Encoding? encoding, Boolean leaveOpen)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new BinaryWriter(stream, encoding ?? Encoding.UTF8, leaveOpen);
        }

        public static ProgressStream Progress(this Stream stream, IProgress<ProgressStreamInfo> progress)
        {
            return new ProgressStream(stream, progress);
        }

        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public static String ReadAsString(this Stream stream)
        {
            return ReadAsString(stream, null);
        }

        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static String ReadAsString(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamReader reader = stream.ToStreamReader(encoding, true);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Returns content of the stream as a string.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public static Task<String> ReadAsStringAsync(this Stream stream)
        {
            return ReadAsStringAsync(stream, null);
        }

        /// <summary>
        /// Returns content of the stream as a string.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static async Task<String> ReadAsStringAsync(this Stream stream, Encoding? encoding)
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
        public static String[] ReadAsLines(this Stream stream)
        {
            return ReadAsLines(stream, null);
        }

        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static String[] ReadAsLines(this Stream stream, Encoding? encoding)
        {
            return ReadAsSequential(stream, encoding).ToArray();
        }

        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public static IEnumerable<String> ReadAsSequential(this Stream stream)
        {
            return ReadAsSequential(stream, null);
        }

        /// <summary>
        /// Returns content of the stream as a byte array.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public static IEnumerable<String> ReadAsSequential(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamReader reader = stream.ToStreamReader(encoding, true);

            while(reader.ReadLine() is { } line)
            {
                yield return line;
            }
        }

        public static void Write(this Stream stream, ReadOnlySpan<Char> buffer)
        {
            Write(stream, buffer, null);
        }

        public static void Write(this Stream stream, ReadOnlySpan<Char> buffer, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            writer.Write(buffer);
        }

        public static void Write(this Stream stream, String? value)
        {
            Write(stream, value, null);
        }

        public static void Write(this Stream stream, String? value, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            writer.Write(value);
        }

        public static void Write(this Stream stream, StringBuilder? value)
        {
            Write(stream, value, null);
        }

        public static void Write(this Stream stream, StringBuilder? value, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            writer.Write(value);
        }

        public static void WriteLine(this Stream stream)
        {
            WriteLine(stream, (Encoding?) null);
        }

        public static void WriteLine(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            writer.WriteLine();
        }

        public static void WriteLine(this Stream stream, ReadOnlySpan<Char> buffer)
        {
            WriteLine(stream, buffer, null);
        }

        public static void WriteLine(this Stream stream, ReadOnlySpan<Char> buffer, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            writer.WriteLine(buffer);
        }

        public static void WriteLine(this Stream stream, String? value)
        {
            WriteLine(stream, value, null);
        }

        public static void WriteLine(this Stream stream, String? value, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            writer.WriteLine(value);
        }

        public static void WriteLine(this Stream stream, StringBuilder? value)
        {
            WriteLine(stream, value, null);
        }

        public static void WriteLine(this Stream stream, StringBuilder? value, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            writer.WriteLine(value);
        }

        public static Task WriteAsync(this Stream stream, ReadOnlyMemory<Char> buffer)
        {
            return WriteAsync(stream, buffer, null, CancellationToken.None);
        }

        public static Task WriteAsync(this Stream stream, ReadOnlyMemory<Char> buffer, Encoding? encoding)
        {
            return WriteAsync(stream, buffer, encoding, CancellationToken.None);
        }

        public static Task WriteAsync(this Stream stream, ReadOnlyMemory<Char> buffer, CancellationToken token)
        {
            return WriteAsync(stream, buffer, null, token);
        }

        public static async Task WriteAsync(this Stream stream, ReadOnlyMemory<Char> buffer, Encoding? encoding, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            await writer.WriteAsync(buffer, token).ConfigureAwait(false);
        }

        public static Task WriteAsync(this Stream stream, String? value)
        {
            return WriteAsync(stream, value, null);
        }

        public static async Task WriteAsync(this Stream stream, String? value, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            await writer.WriteAsync(value).ConfigureAwait(false);
        }

        public static Task WriteAsync(this Stream stream, StringBuilder? value)
        {
            return WriteAsync(stream, value, null, CancellationToken.None);
        }

        public static Task WriteAsync(this Stream stream, StringBuilder? value, Encoding? encoding)
        {
            return WriteAsync(stream, value, encoding, CancellationToken.None);
        }

        public static Task WriteAsync(this Stream stream, StringBuilder? value, CancellationToken token)
        {
            return WriteAsync(stream, value, null, token);
        }

        public static async Task WriteAsync(this Stream stream, StringBuilder? value, Encoding? encoding, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            await writer.WriteAsync(value, token).ConfigureAwait(false);
        }

        public static Task WriteLineAsync(this Stream stream)
        {
            return WriteLineAsync(stream, (Encoding?) null);
        }

        public static async Task WriteLineAsync(this Stream stream, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            await writer.WriteLineAsync().ConfigureAwait(false);
        }

        public static Task WriteLineAsync(this Stream stream, ReadOnlyMemory<Char> buffer)
        {
            return WriteLineAsync(stream, buffer, null, CancellationToken.None);
        }

        public static Task WriteLineAsync(this Stream stream, ReadOnlyMemory<Char> buffer, Encoding? encoding)
        {
            return WriteLineAsync(stream, buffer, encoding, CancellationToken.None);
        }

        public static Task WriteLineAsync(this Stream stream, ReadOnlyMemory<Char> buffer, CancellationToken token)
        {
            return WriteLineAsync(stream, buffer, null, token);
        }

        public static async Task WriteLineAsync(this Stream stream, ReadOnlyMemory<Char> buffer, Encoding? encoding, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            await writer.WriteLineAsync(buffer, token).ConfigureAwait(false);
        }

        public static Task WriteLineAsync(this Stream stream, String? value)
        {
            return WriteLineAsync(stream, value, null);
        }

        public static async Task WriteLineAsync(this Stream stream, String? value, Encoding? encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            await writer.WriteLineAsync(value).ConfigureAwait(false);
        }

        public static Task WriteLineAsync(this Stream stream, StringBuilder? value)
        {
            return WriteLineAsync(stream, value, null, CancellationToken.None);
        }

        public static Task WriteLineAsync(this Stream stream, StringBuilder? value, Encoding? encoding)
        {
            return WriteLineAsync(stream, value, encoding, CancellationToken.None);
        }

        public static Task WriteLineAsync(this Stream stream, StringBuilder? value, CancellationToken token)
        {
            return WriteLineAsync(stream, value, null, token);
        }

        public static async Task WriteLineAsync(this Stream stream, StringBuilder? value, Encoding? encoding, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await using StreamWriter writer = stream.ToStreamWriter(encoding, true);
            await writer.WriteLineAsync(value, token).ConfigureAwait(false);
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

        public static Task<Byte[]> ReadAsByteArrayAsync(this Stream stream)
        {
            return ReadAsByteArrayAsync(stream, CancellationToken.None);
        }

        public static async Task<Byte[]> ReadAsByteArrayAsync(this Stream stream, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using BinaryReader reader = stream.ToBinaryReader(null, true);
            Int32 count = checked((Int32) (stream.Length - stream.Position));
            return await reader.ReadBytesAsync(count, token).ConfigureAwait(false);
        }

        public static Stream AsSeekableStream(this Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return stream.CanSeek ? stream : new SeekableStream(stream);
        }

        private sealed class StreamPositionFreeze : IDisposable
        {
            private Stream Stream { get; }
            private Int64 Position { get; }
            private Boolean Safe { get; }

            public StreamPositionFreeze(Stream stream, Int64 position, Boolean safe)
            {
                Stream = stream ?? throw new ArgumentNullException(nameof(stream));
                Position = position;
                Safe = safe;
            }

            public void Dispose()
            {
                if (Safe)
                {
                    Stream.TrySetPosition(Position);
                }
                else
                {
                    Stream.Position = Position;
                }
            }
        }

        public static IDisposable Freeze(this Stream stream)
        {
            return Freeze(stream, true);
        }

        public static IDisposable Freeze(this Stream stream, Boolean safe)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new StreamPositionFreeze(stream, stream.Position, safe);
        }
    }
}