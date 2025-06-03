// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading.Tasks;
using NetExtender.Cryptography.Base.Alphabet;
using NetExtender.Cryptography.Base.Common;

namespace NetExtender.Cryptography.Base
{
    /// <summary>
    /// Base32 encoding/decoding functions.
    /// </summary>
    public sealed class Base32 : BaseCryptographyStream
    {
        private const Int32 BitsPerByte = 8;
        private const Int32 BitsPerChar = 5;

        public static Base32 Crockford { get; } = new Base32(Base32Alphabet.Crockford);
        public static Base32 RFC4648 { get; } = new Base32(Base32Alphabet.Rfc4648);
        public static Base32 ExtendedHex { get; } = new Base32(Base32Alphabet.ExtHex);
        public static Base32 ZBase32 { get; } = new Base32(Base32Alphabet.ZBase32);
        public static Base32 Geohash { get; } = new Base32(Base32Alphabet.Geohash);

        /// <summary>
        /// Gets the encoding alphabet.
        /// </summary>
        public Base32Alphabet Alphabet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base32"/> class with a
        /// custom alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base32(Base32Alphabet alphabet)
        {
            Alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet));
        }

        private static Int32 GetAllocationByteCountForDecoding(Int32 length)
        {
            return length * BitsPerChar / BitsPerByte;
        }

        private Int32 GetPaddingCharCount(ReadOnlySpan<Char> value)
        {
            unchecked
            {
                Int32 result = 0;
                Int32 length = value.Length;
                while (length > 0 && value[--length] == Alphabet.Padding)
                {
                    result++;
                }

                return result;
            }
        }

        /// <inheritdoc/>
        public override Int32 SafeCharCountForEncoding(ReadOnlySpan<Byte> buffer)
        {
            return ((buffer.Length - 1) / BitsPerChar + 1) * BitsPerByte;
        }

        /// <inheritdoc/>
        public override Int32 SafeByteCountForDecoding(ReadOnlySpan<Char> buffer)
        {
            return GetAllocationByteCountForDecoding(buffer.Length - GetPaddingCharCount(buffer));
        }

        /// <summary>
        /// Encode a byte array into a Base32 string without padding.
        /// </summary>
        /// <param name="value">Buffer to be encoded.</param>
        /// <returns>Encoded string.</returns>
        public override String Encode(ReadOnlySpan<Byte> value)
        {
            return Encode(value, false);
        }

        /// <summary>
        /// Encode a byte array into a Base32 string.
        /// </summary>
        /// <param name="value">Buffer to be encoded.</param>
        /// <param name="padding">Append padding characters in the output.</param>
        /// <returns>Encoded string.</returns>
        public unsafe String Encode(ReadOnlySpan<Byte> value, Boolean padding)
        {
            unchecked
            {
                if (value.Length <= 0)
                {
                    return String.Empty;
                }

                // we are ok with slightly larger buffer since the output string will always
                // have the exact length of the output produced.
                Int32 length = SafeCharCountForEncoding(value);
                String output = new String('\0', length);
                fixed (Byte* pinput = value)
                fixed (Char* poutput = output)
                {
                    if (!EncodeCore(pinput, value.Length, poutput, length, padding, out Int32 written))
                    {
                        throw new InvalidOperationException("Internal error: couldn't calculate proper output buffer size for input");
                    }

                    return output[..written];
                }
            }
        }

        /// <summary>
        /// Encode a binary stream to a Base32 text stream without padding.
        /// </summary>
        /// <param name="input">Input bytes.</param>
        /// <param name="output">The writer the output is written to.</param>
        public override void Encode(Stream input, TextWriter output)
        {
            Encode(input, output, false);
        }

        /// <summary>
        /// Encode a binary stream to a Base32 text stream.
        /// </summary>
        /// <param name="input">Input bytes.</param>
        /// <param name="output">The writer the output is written to.</param>
        /// <param name="padding">Whether to use padding at the end of the output.</param>
        public void Encode(Stream input, TextWriter output, Boolean padding)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            Encode(input, output, (buffer, last) => Encode(buffer.Span, last && padding));
        }

        /// <summary>
        /// Encode a binary stream to a Base32 text stream without padding.
        /// </summary>
        /// <param name="input">Input bytes.</param>
        /// <param name="output">The writer the output is written to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task EncodeAsync(Stream input, TextWriter output)
        {
            return EncodeAsync(input, output, false);
        }

        /// <summary>
        /// Encode a binary stream to a Base32 text stream.
        /// </summary>
        /// <param name="input">Input bytes.</param>
        /// <param name="output">The writer the output is written to.</param>
        /// <param name="padding">Whether to use padding at the end of the output.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task EncodeAsync(Stream input, TextWriter output, Boolean padding)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            return EncodeAsync(input, output, (buffer, last) => Encode(buffer.Span, last && padding));
        }

        /// <inheritdoc/>
        public override Boolean TryEncode(ReadOnlySpan<Byte> value, Span<Char> output, out Int32 written)
        {
            return TryEncode(value, output, false, out written);
        }

        /// <summary>
        /// Encode to the given preallocated buffer.
        /// </summary>
        /// <param name="value">Input bytes.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="padding">Whether to use padding characters at the end.</param>
        /// <param name="written">Number of characters written to the output.</param>
        /// <returns>True if encoding is successful, false if the output is invalid.</returns>
        public unsafe Boolean TryEncode(ReadOnlySpan<Byte> value, Span<Char> output, Boolean padding, out Int32 written)
        {
            unchecked
            {
                if (value.Length <= 0)
                {
                    written = 0;
                    return true;
                }

                fixed (Byte* pinput = value)
                fixed (Char* poutput = output)
                {
                    return EncodeCore(pinput, value.Length, poutput, output.Length, padding, out written);
                }
            }
        }

        // ReSharper disable once CognitiveComplexity
        private unsafe Boolean EncodeCore(Byte* pinput, Int32 length, Char* poutput, Int32 outputlength, Boolean padding, out Int32 written)
        {
            unchecked
            {
                Char* poutputend = poutput + outputlength;
                Byte* pinputend = pinput + length;

                for (Int32 left = BitsPerByte, current = *pinput; pinput != pinputend;)
                {
                    Int32 outputpad;
                    if (left > BitsPerChar)
                    {
                        left -= BitsPerChar;
                        outputpad = current >> left;
                        *poutput++ = Alphabet.Value[outputpad];
                        if (poutput > poutputend)
                        {
                            goto overflow;
                        }

                        current &= (1 << left) - 1;
                    }

                    Int32 next = BitsPerChar - left;
                    left = BitsPerByte - next;
                    outputpad = current << next;
                    if (++pinput != pinputend)
                    {
                        current = *pinput;
                        outputpad |= current >> left;
                        current &= (1 << left) - 1;
                    }

                    *poutput++ = Alphabet.Value[outputpad];
                    if (poutput > poutputend)
                    {
                        goto overflow;
                    }
                }

                if (padding)
                {
                    while (poutput != poutputend)
                    {
                        *poutput++ = Alphabet.Padding;
                        if (poutput > poutputend)
                        {
                            goto overflow;
                        }
                    }
                }

                written = (Int32) (poutput - poutput);
                return true;
                
                overflow:
                written = (Int32) (poutput - poutput);
                return false;
            }
        }

        /// <summary>
        /// Decode a Base32 encoded string into a byte array.
        /// </summary>
        /// <param name="value">Encoded Base32 string.</param>
        /// <returns>Decoded byte array.</returns>
        public override unsafe Span<Byte> Decode(ReadOnlySpan<Char> value)
        {
            unchecked
            {
                Int32 length = value.Length - GetPaddingCharCount(value);
                Int32 count = GetAllocationByteCountForDecoding(length);
                if (count <= 0)
                {
                    return Array.Empty<Byte>();
                }

                Byte[] buffer = new Byte[count];

                fixed (Byte* poutput = buffer)
                fixed (Char* pinput = value)
                {
                    if (!DecodeCore(pinput, length, poutput, out _))
                    {
                        throw new ArgumentException(@"Invalid input or output", nameof(value));
                    }
                }

                return buffer;
            }
        }

        /// <summary>
        /// Decode a text stream into a binary stream.
        /// </summary>
        /// <param name="input">TextReader open on the stream.</param>
        /// <param name="output">Binary output stream.</param>
        public override void Decode(TextReader input, Stream output)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            Decode(input, output, buffer => Decode(buffer.Span).ToArray());
        }

        /// <summary>
        /// Decode a text stream into a binary stream.
        /// </summary>
        /// <param name="input">TextReader open on the stream.</param>
        /// <param name="output">Binary output stream.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task DecodeAsync(TextReader input, Stream output)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            return DecodeAsync(input, output, buffer => Decode(buffer.Span).ToArray());
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 written)
        {
            unchecked
            {
                Int32 length = input.Length - GetPaddingCharCount(input);
                if (length <= 0 || output.Length <= 0)
                {
                    written = 0;
                    return true;
                }

                fixed (Char* pinput = input)
                fixed (Byte* poutput = output)
                {
                    return DecodeCore(pinput, length, poutput, out written);
                }
            }
        }

        private unsafe Boolean DecodeCore(Char* pinput, Int32 length, Byte* poutput, out Int32 written)
        {
            unchecked
            {
                ReadOnlySpan<Byte> table = Alphabet.ReverseLookupTable;
                Int32 outputpad = 0;
                Int32 left = BitsPerByte;

                Byte* poutputstart = poutput;
                Char* pend = pinput + length;
                while (pinput != pend)
                {
                    Char character = *pinput++;
                    Int32 b = table[character] - 1;
                    if (b < 0)
                    {
                        written = (Int32) (poutput - poutputstart);
                        return false;
                    }

                    if (left > BitsPerChar)
                    {
                        left -= BitsPerChar;
                        outputpad |= b << left;
                        continue;
                    }

                    Int32 shift = BitsPerChar - left;
                    outputpad |= b >> shift;
                    *poutput++ = (Byte) outputpad;
                    b &= (1 << shift) - 1;
                    left = BitsPerByte - shift;
                    outputpad = b << left;
                }

                written = (Int32) (poutput - poutputstart);
                return true;
            }
        }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return Alphabet.GetHashCode();
        }

        /// <inheritdoc/>
        public override Boolean Equals(Object? other)
        {
            return other is Base32 value && Alphabet.Equals(value.Alphabet);
        }

        /// <inheritdoc/>
        public override String ToString()
        {
            return $"{nameof(Base32)}_{Alphabet}";
        }
    }
}