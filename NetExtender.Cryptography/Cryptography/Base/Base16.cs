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
    /// Base16 encoding/decoding.
    /// </summary>
    public sealed class Base16 : BaseCryptographyStream
    {
        /// <summary>
        /// Gets lower case Base16 encoder. Decoding is case-insensitive.
        /// </summary>
        public static Base16 LowerCase { get; } = new Base16(Base16Alphabet.LowerCase);

        /// <summary>
        /// Gets upper case Base16 encoder. Decoding is case-insensitive.
        /// </summary>
        public static Base16 UpperCase { get; } = new Base16(Base16Alphabet.UpperCase);

        /// <summary>
        /// Gets lower case Base16 encoder. Decoding is case-insensitive.
        /// </summary>
        public static Base16 ModHex { get; } = new Base16(Base16Alphabet.ModHex);

        /// <summary>
        /// Gets the alphabet used by the encoder.
        /// </summary>
        public Base16Alphabet Alphabet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base16"/> class.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base16(Base16Alphabet alphabet)
        {
            Alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet));
        }

        /// <inheritdoc/>
        public override Int32 SafeCharCountForEncoding(ReadOnlySpan<Byte> buffer)
        {
            return buffer.Length * 2;
        }

        /// <inheritdoc/>
        public override Int32 SafeByteCountForDecoding(ReadOnlySpan<Char> buffer)
        {
            return (buffer.Length & 1) == 0 ? buffer.Length / 2 : 0;
        }

        /// <summary>
        /// Encode to Base16 representation.
        /// </summary>
        /// <param name="value">Bytes to encode.</param>
        /// <returns>Base16 string.</returns>
        public override unsafe String Encode(ReadOnlySpan<Byte> value)
        {
            if (value.Length <= 0)
            {
                return String.Empty;
            }

            String output = new String('\0', SafeCharCountForEncoding(value));
            fixed (Char* outputPtr = output)
            {
                InternalEncode(value, value.Length, Alphabet.Value, outputPtr);
            }

            return output;
        }

        /// <summary>
        /// Encodes stream of bytes into a Base16 text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        public override void Encode(Stream input, TextWriter output)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            Encode(input, output, (buffer, _) => Encode(buffer.Span));
        }

        /// <summary>
        /// Encodes stream of bytes into a Base16 text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task EncodeAsync(Stream input, TextWriter output)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            return EncodeAsync(input, output, (buffer, _) => Encode(buffer.Span));
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryEncode(ReadOnlySpan<Byte> value, Span<Char> output, out Int32 written)
        {
            unchecked
            {
                String alphabet = Alphabet.Value;

                Int32 length = value.Length * 2;
                if (length <= 0 || output.Length < length)
                {
                    written = 0;
                    return true;
                }

                fixed (Char* poutput = output)
                {
                    InternalEncode(value, value.Length, alphabet, poutput);
                }

                written = length;
                return true;
            }
        }

        private static unsafe void InternalEncode(ReadOnlySpan<Byte> value, Int32 length, String alphabet, Char* poutput)
        {
            unchecked
            {
                fixed (Byte* pointer = value)
                {
                    Byte* pinput = pointer;
                    Int32 octets = length / sizeof(UInt64);
                    for (Int32 i = 0; i < octets; i++, pinput += sizeof(UInt64))
                    {
                        // read bigger chunks
                        UInt64 input = *(UInt64*) pinput;
                        for (Int32 j = 0; j < sizeof(UInt64) / 2; j++, input >>= 16)
                        {
                            UInt16 pair = (UInt16) input;

                            // use cpu pipeline to parallelize writes
                            poutput[0] = alphabet[(pair >> 4) & 0x0F];
                            poutput[1] = alphabet[pair & 0x0F];
                            poutput[2] = alphabet[pair >> 12];
                            poutput[3] = alphabet[(pair >> 8) & 0x0F];
                            poutput += 4;
                        }
                    }

                    for (Int32 remaining = length % sizeof(UInt64); remaining > 0; remaining--)
                    {
                        Byte b = *pinput++;
                        poutput[0] = alphabet[b >> 4];
                        poutput[1] = alphabet[b & 0x0F];
                        poutput += 2;
                    }
                }
            }
        }

        /// <summary>
        /// Decode Base16 text into bytes.
        /// </summary>
        /// <param name="value">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        public override Span<Byte> Decode(ReadOnlySpan<Char> value)
        {
            if (value.Length <= 0)
            {
                return Array.Empty<Byte>();
            }

            Byte[] output = new Byte[SafeByteCountForDecoding(value)];
            if (!TryDecode(value, output, out _))
            {
                throw new InvalidOperationException($"Can't decode {nameof(Base16)}");
            }

            return output;
        }

        /// <summary>
        /// Decode Base16 text through streams for generic use. Stream based variant tries to consume
        /// as little memory as possible, and relies of .NET's own underlying buffering mechanisms,
        /// contrary to their buffer-based versions.
        /// </summary>
        /// <param name="input">Stream that the encoded bytes would be read from.</param>
        /// <param name="output">Stream where decoded bytes will be written to.</param>
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
        /// Decode Base16 text through streams for generic use. Stream based variant tries to consume
        /// as little memory as possible, and relies of .NET's own underlying buffering mechanisms,
        /// contrary to their buffer-based versions.
        /// </summary>
        /// <param name="input">Stream that the encoded bytes would be read from.</param>
        /// <param name="output">Stream where decoded bytes will be written to.</param>
        /// <returns>Task that represents the async operation.</returns>
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
        public override unsafe Boolean TryDecode(ReadOnlySpan<Char> text, Span<Byte> output, out Int32 written)
        {
            unchecked
            {
                if (text.Length <= 0 || (text.Length & 1) != 0)
                {
                    written = 0;
                    return true;
                }

                Int32 length = text.Length / 2;
                if (output.Length < length)
                {
                    written = 0;
                    return false;
                }

                ReadOnlySpan<Byte> table = Alphabet.ReverseLookupTable;

                fixed (Byte* pointer = output)
                fixed (Char* ptext = text)
                {
                    Char* pinput = ptext;
                    Byte* poutput = pointer;
                    Char* pend = pinput + text.Length;
                    
                    while (pinput != pend)
                    {
                        Int32 first = table[pinput[0]] - 1;
                        if (first < 0)
                        {
                            throw new ArgumentException($"Invalid hex character: {pinput[0]}");
                        }

                        Int32 second = table[pinput[1]] - 1;
                        if (second < 0)
                        {
                            throw new ArgumentException($"Invalid hex character: {pinput[1]}");
                        }

                        *poutput++ = (Byte) (first << 4 | second);
                        pinput += 2;
                    }
                }

                written = length;
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
            return other is Base16 value && Alphabet.Equals(value.Alphabet);
        }

        /// <inheritdoc/>
        public override String ToString()
        {
            return $"{nameof(Base16)}_{Alphabet}";
        }
    }
}