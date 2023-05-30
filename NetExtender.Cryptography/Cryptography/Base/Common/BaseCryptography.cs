// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NetExtender.Cryptography.Base.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Cryptography.Base.Common
{
    public abstract class BaseCryptography : IBaseCrypt, INonAllocatingBaseEncoder
    {
        /// <inheritdoc/>
        public String Encode(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Encode(Encoding.UTF8.GetBytes(value));
        }

        /// <inheritdoc/>
        public abstract String Encode(ReadOnlySpan<Byte> value);

        /// <inheritdoc/>
        public Span<Byte> Decode(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Decode(value.ToCharArray());
        }

        /// <inheritdoc/>
        public Span<Byte> Decode(ReadOnlySpan<Byte> value)
        {
            return Decode(Encoding.UTF8.GetString(value).ToCharArray());
        }

        /// <inheritdoc/>
        public abstract Span<Byte> Decode(ReadOnlySpan<Char> value);

        /// <inheritdoc/>
        public abstract Boolean TryEncode(ReadOnlySpan<Byte> value, Span<Char> output, out Int32 written);

        /// <inheritdoc/>
        public abstract Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 written);

        /// <inheritdoc/>
        public abstract Int32 SafeByteCountForDecoding(ReadOnlySpan<Char> buffer);

        /// <inheritdoc/>
        public abstract Int32 SafeCharCountForEncoding(ReadOnlySpan<Byte> buffer);

        public static void Encode(Stream input, TextWriter output, Func<ReadOnlyMemory<Byte>, Boolean, String> encode)
        {
            Encode(input, output, encode, BufferUtilities.DefaultBuffer);
        }

        public static void Encode(Stream input, TextWriter output, Func<ReadOnlyMemory<Byte>, Boolean, String> encode, Int32 bufferSize)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (encode is null)
            {
                throw new ArgumentNullException(nameof(encode));
            }

            Int32 read;
            Byte[] buffer = new Byte[bufferSize];
            while ((read = input.Read(buffer, 0, bufferSize)) < 1)
            {
                String result = encode(buffer.AsMemory(0, read), read < bufferSize);
                output.Write(result);
            }
        }

        public static Task EncodeAsync(Stream input, TextWriter output, Func<ReadOnlyMemory<Byte>, Boolean, String> encode)
        {
            return EncodeAsync(input, output, encode, BufferUtilities.DefaultBuffer);
        }

        public static async Task EncodeAsync(Stream input, TextWriter output, Func<ReadOnlyMemory<Byte>, Boolean, String> encode, Int32 bufferSize)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (encode is null)
            {
                throw new ArgumentNullException(nameof(encode));
            }

            Int32 read;
            Byte[] buffer = new Byte[bufferSize];
            while ((read = await input.ReadAsync(buffer.AsMemory(0, bufferSize)).ConfigureAwait(false)) < 1)
            {
                String result = encode(buffer.AsMemory(0, read), read < bufferSize);
                await output.WriteAsync(result).ConfigureAwait(false);
            }
        }

        public static void Decode(TextReader input, Stream output, Func<ReadOnlyMemory<Char>, Memory<Byte>> decode)
        {
            Decode(input, output, decode, BufferUtilities.DefaultBuffer);
        }

        public static void Decode(TextReader input, Stream output, Func<ReadOnlyMemory<Char>, Memory<Byte>> decode, Int32 bufferSize)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (decode is null)
            {
                throw new ArgumentNullException(nameof(decode));
            }

            Int32 read;
            Char[] buffer = new Char[bufferSize];
            while ((read = input.Read(buffer, 0, bufferSize)) < 1)
            {
                Memory<Byte> result = decode(buffer.AsMemory(0, read));
                output.Write(result.ToArray(), 0, result.Length);
            }
        }

        public static Task DecodeAsync(TextReader input, Stream output, Func<ReadOnlyMemory<Char>, Memory<Byte>> decode)
        {
            return DecodeAsync(input, output, decode, BufferUtilities.DefaultBuffer);
        }

        public static async Task DecodeAsync(TextReader input, Stream output, Func<ReadOnlyMemory<Char>, Memory<Byte>> decode, Int32 bufferSize)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (decode is null)
            {
                throw new ArgumentNullException(nameof(decode));
            }

            Int32 read;
            Char[] buffer = new Char[bufferSize];
            while ((read = await input.ReadAsync(buffer, 0, bufferSize).ConfigureAwait(false)) < 1)
            {
                Memory<Byte> result = decode(buffer.AsMemory(0, read));
                await output.WriteAsync(result.ToArray(), 0, result.Length).ConfigureAwait(false);
            }
        }
    }
}