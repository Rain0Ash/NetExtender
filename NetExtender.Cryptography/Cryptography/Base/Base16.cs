// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="Base16.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;
using System.IO;
using System.Threading.Tasks;
using NetExtender.Crypto.Base.Alphabet;
using NetExtender.Crypto.Base.Common;

namespace NetExtender.Crypto.Base
{
    /// <summary>
    /// Base16 encoding/decoding.
    /// </summary>
    public sealed class Base16 : BaseCryptStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Base16"/> class.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base16(Base16Alphabet alphabet)
        {
            Alphabet = alphabet;
        }

        /// <summary>
        /// Gets upper case Base16 encoder. Decoding is case-insensitive.
        /// </summary>
        public static Base16 UpperCase { get; } = new Base16(Base16Alphabet.UpperCase);

        /// <summary>
        /// Gets lower case Base16 encoder. Decoding is case-insensitive.
        /// </summary>
        public static Base16 LowerCase { get; } = new Base16(Base16Alphabet.LowerCase);

        /// <summary>
        /// Gets lower case Base16 encoder. Decoding is case-insensitive.
        /// </summary>
        public static Base16 ModHex { get; } = new Base16(Base16Alphabet.ModHex);

        /// <summary>
        /// Gets the alphabet used by the encoder.
        /// </summary>
        public Base16Alphabet Alphabet { get; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return Alphabet.GetHashCode();
        }

        /// <inheritdoc/>
        public override String ToString()
        {
            return $"{nameof(Base16)}_{Alphabet}";
        }

        /// <inheritdoc/>
        public override Int32 GetSafeByteCountForDecoding(ReadOnlySpan<Char> text)
        {
            Int32 textLen = text.Length;
            if ((textLen & 1) != 0)
            {
                return 0;
            }

            return textLen / 2;
        }

        /// <inheritdoc/>
        public override Int32 GetSafeCharCountForEncoding(ReadOnlySpan<Byte> buffer)
        {
            return buffer.Length * 2;
        }

        /// <summary>
        /// Encode to Base16 representation using uppercase lettering.
        /// </summary>
        /// <param name="bytes">Bytes to encode.</param>
        /// <returns>Base16 string.</returns>
        public static String EncodeUpper(ReadOnlySpan<Byte> bytes)
        {
            return UpperCase.Encode(bytes);
        }

        /// <summary>
        /// Encode to Base16 representation using lowercase lettering.
        /// </summary>
        /// <param name="bytes">Bytes to encode.</param>
        /// <returns>Base16 string.</returns>
        public static String EncodeLower(ReadOnlySpan<Byte> bytes)
        {
            return LowerCase.Encode(bytes);
        }

        /// <summary>
        /// Encodes stream of bytes into a Base16 text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        public static void EncodeUpper(Stream input, TextWriter output)
        {
            UpperCase.Encode(input, output);
        }

        /// <summary>
        /// Encodes stream of bytes into a Base16 text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static Task EncodeUpperAsync(Stream input, TextWriter output)
        {
            return UpperCase.EncodeAsync(input, output);
        }

        /// <summary>
        /// Encodes stream of bytes into a Base16 text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        public static void EncodeLower(Stream input, TextWriter output)
        {
            LowerCase.Encode(input, output);
        }

        /// <summary>
        /// Encodes stream of bytes into a Base16 text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static Task EncodeLowerAsync(Stream input, TextWriter output)
        {
            return LowerCase.EncodeAsync(input, output);
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
            return StreamHelper.DecodeAsync(input, output, buffer => Decode(buffer.Span).ToArray());
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
            StreamHelper.Decode(input, output, buffer => Decode(buffer.Span).ToArray());
        }

        /// <summary>
        /// Encodes stream of bytes into a Base16 text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        public override void Encode(Stream input, TextWriter output)
        {
            StreamHelper.Encode(input, output, (buffer, _) => Encode(buffer.Span));
        }

        /// <summary>
        /// Encodes stream of bytes into a Base16 text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task EncodeAsync(Stream input, TextWriter output)
        {
            return StreamHelper.EncodeAsync(input, output, (buffer, _) => Encode(buffer.Span));
        }

        /// <summary>
        /// Decode Base16 text into bytes.
        /// </summary>
        /// <param name="text">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        public override Span<Byte> Decode(ReadOnlySpan<Char> text)
        {
            Int32 textLen = text.Length;
            if (textLen == 0)
            {
                return Array.Empty<Byte>();
            }

            Byte[] output = new Byte[GetSafeByteCountForDecoding(text)];
            if (!TryDecode(text, output, out _))
            {
                throw new ArgumentException(@"Invalid text", nameof(text));
            }

            return output;
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryDecode(ReadOnlySpan<Char> text, Span<Byte> output, out Int32 numBytesWritten)
        {
            unchecked
            {
                Int32 textLen = text.Length;
                if (textLen == 0)
                {
                    numBytesWritten = 0;
                    return true;
                }

                if ((textLen & 1) != 0)
                {
                    numBytesWritten = 0;
                    return false;
                }

                Int32 outputLen = textLen / 2;
                if (output.Length < outputLen)
                {
                    numBytesWritten = 0;
                    return false;
                }

                ReadOnlySpan<Byte> table = Alphabet.ReverseLookupTable;

                fixed (Byte* outputPtr = output)
                fixed (Char* textPtr = text)
                {
                    Byte* pOutput = outputPtr;
                    Char* pInput = textPtr;
                    Char* pEnd = pInput + textLen;
                    while (pInput != pEnd)
                    {
                        Int32 b1 = table[pInput[0]] - 1;
                        if (b1 < 0)
                        {
                            throw new ArgumentException($"Invalid hex character: {pInput[0]}");
                        }

                        Int32 b2 = table[pInput[1]] - 1;
                        if (b2 < 0)
                        {
                            throw new ArgumentException($"Invalid hex character: {pInput[1]}");
                        }

                        *pOutput++ = (Byte)(b1 << 4 | b2);
                        pInput += 2;
                    }
                }

                numBytesWritten = outputLen;
                return true;
            }
        }

        /// <summary>
        /// Encode to Base16 representation.
        /// </summary>
        /// <param name="bytes">Bytes to encode.</param>
        /// <returns>Base16 string.</returns>
        public override unsafe String Encode(ReadOnlySpan<Byte> bytes)
        {
            Int32 bytesLen = bytes.Length;
            if (bytesLen == 0)
            {
                return String.Empty;
            }

            String output = new String('\0', GetSafeCharCountForEncoding(bytes));
            fixed (Char* outputPtr = output)
            {
                InternalEncode(bytes, bytesLen, Alphabet.Value, outputPtr);
            }

            return output;
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryEncode(ReadOnlySpan<Byte> bytes, Span<Char> output, out Int32 numCharsWritten)
        {
            unchecked
            {
                Int32 bytesLen = bytes.Length;
                String alphabet = Alphabet.Value;

                Int32 outputLen = bytesLen * 2;
                if (output.Length < outputLen)
                {
                    numCharsWritten = 0;
                    return false;
                }

                if (outputLen == 0)
                {
                    numCharsWritten = 0;
                    return true;
                }

                fixed (Char* outputPtr = output)
                {
                    InternalEncode(bytes, bytesLen, alphabet, outputPtr);
                }

                numCharsWritten = outputLen;
                return true;
            }
        }

        private static unsafe void InternalEncode(ReadOnlySpan<Byte> bytes, Int32 bytesLen, String alphabet, Char* outputPtr)
        {
            unchecked
            {
                fixed (Byte* bytesPtr = bytes)
                {
                    Char* pOutput = outputPtr;
                    Byte* pInput = bytesPtr;

                    Int32 octets = bytesLen / sizeof(UInt64);
                    for (Int32 i = 0; i < octets; i++, pInput += sizeof(UInt64))
                    {
                        // read bigger chunks
                        UInt64 input = *(UInt64*)pInput;
                        for (Int32 j = 0; j < sizeof(UInt64) / 2; j++, input >>= 16)
                        {
                            UInt16 pair = (UInt16)input;

                            // use cpu pipeline to parallelize writes
                            pOutput[0] = alphabet[(pair >> 4) & 0x0F];
                            pOutput[1] = alphabet[pair & 0x0F];
                            pOutput[2] = alphabet[pair >> 12];
                            pOutput[3] = alphabet[(pair >> 8) & 0x0F];
                            pOutput += 4;
                        }
                    }

                    for (Int32 remaining = bytesLen % sizeof(UInt64); remaining > 0; remaining--)
                    {
                        Byte b = *pInput++;
                        pOutput[0] = alphabet[b >> 4];
                        pOutput[1] = alphabet[b & 0x0F];
                        pOutput += 2;
                    }
                }
            }
        }
    }
}