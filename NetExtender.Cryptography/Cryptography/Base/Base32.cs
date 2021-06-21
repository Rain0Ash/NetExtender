// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="Base32.cs" company="Sedat Kapanoglu">
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
    /// Base32 encoding/decoding functions.
    /// </summary>
    public sealed class Base32 : BaseCryptStream
    {
        private const Int32 BitsPerByte = 8;
        private const Int32 BitsPerChar = 5;

        public static Base32 Crockford { get; } = new Base32(Base32Alphabet.Crockford);
        public static Base32 RFC4648 { get; } = new Base32(Base32Alphabet.RFC4648);
        public static Base32 ExtendedHex { get; } = new Base32(Base32Alphabet.ExtendedHex);
        public static Base32 ZBase32 { get; } = new Base32(Base32Alphabet.ZBase32);
        public static Base32 Geohash { get; } = new Base32(Base32Alphabet.Geohash);

        /// <summary>
        /// Initializes a new instance of the <see cref="Base32"/> class with a
        /// custom alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base32(Base32Alphabet alphabet)
        {
            Alphabet = alphabet;
        }

        /// <summary>
        /// Gets the encoding alphabet.
        /// </summary>
        public Base32Alphabet Alphabet { get; }

        /// <inheritdoc/>
        public override Int32 GetSafeByteCountForDecoding(ReadOnlySpan<Char> text)
        {
            return GetAllocationByteCountForDecoding(text.Length - GetPaddingCharCount(text));
        }

        /// <inheritdoc/>
        public override Int32 GetSafeCharCountForEncoding(ReadOnlySpan<Byte> buffer)
        {
            return ((buffer.Length - 1) / BitsPerChar + 1) * BitsPerByte;
        }

        /// <summary>
        /// Encode a byte array into a Base32 string without padding.
        /// </summary>
        /// <param name="bytes">Buffer to be encoded.</param>
        /// <returns>Encoded string.</returns>
        public override String Encode(ReadOnlySpan<Byte> bytes)
        {
            return Encode(bytes, false);
        }

        /// <summary>
        /// Encode a byte array into a Base32 string.
        /// </summary>
        /// <param name="bytes">Buffer to be encoded.</param>
        /// <param name="padding">Append padding characters in the output.</param>
        /// <returns>Encoded string.</returns>
        public unsafe String Encode(ReadOnlySpan<Byte> bytes, Boolean padding)
        {
            unchecked
            {
                Int32 bytesLen = bytes.Length;
                if (bytesLen == 0)
                {
                    return String.Empty;
                }

                // we are ok with slightly larger buffer since the output string will always
                // have the exact length of the output produced.
                Int32 outputLen = GetSafeCharCountForEncoding(bytes);
                String output = new String('\0', outputLen);
                fixed (Byte* inputPtr = bytes)
                fixed (Char* outputPtr = output)
                {
                    if (!InternalEncode(
                        inputPtr,
                        bytesLen,
                        outputPtr,
                        outputLen,
                        padding,
                        out Int32 numCharsWritten))
                    {
                        throw new InvalidOperationException("Internal error: couldn't calculate proper output buffer size for input");
                    }

                    return output[..numCharsWritten];
                }
            }
        }

        /// <summary>
        /// Decode a Base32 encoded string into a byte array.
        /// </summary>
        /// <param name="text">Encoded Base32 string.</param>
        /// <returns>Decoded byte array.</returns>
        public override unsafe Span<Byte> Decode(ReadOnlySpan<Char> text)
        {
            unchecked
            {
                Int32 textLen = text.Length - GetPaddingCharCount(text);
                Int32 outputLen = GetAllocationByteCountForDecoding(textLen);
                if (outputLen == 0)
                {
                    return Array.Empty<Byte>();
                }

                Byte[] outputBuffer = new Byte[outputLen];

                fixed (Byte* outputPtr = outputBuffer)
                fixed (Char* inputPtr = text)
                {
                    if (!InternalDecode(inputPtr, textLen, outputPtr, out _))
                    {
                        throw new ArgumentException(@"Invalid input or output", nameof(text));
                    }
                }

                return outputBuffer;
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
            StreamHelper.Encode(input, output, (buffer, lastBlock) =>
            {
                Boolean usePadding = lastBlock && padding;
                return Encode(buffer.Span, usePadding);
            });
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
            return StreamHelper.EncodeAsync(input, output, (buffer, lastBlock) =>
            {
                Boolean usePadding = lastBlock && padding;
                return Encode(buffer.Span, usePadding);
            });
        }

        /// <summary>
        /// Decode a text stream into a binary stream.
        /// </summary>
        /// <param name="input">TextReader open on the stream.</param>
        /// <param name="output">Binary output stream.</param>
        public override void Decode(TextReader input, Stream output)
        {
            StreamHelper.Decode(input, output, buffer => Decode(buffer.Span).ToArray());
        }

        /// <summary>
        /// Decode a text stream into a binary stream.
        /// </summary>
        /// <param name="input">TextReader open on the stream.</param>
        /// <param name="output">Binary output stream.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task DecodeAsync(TextReader input, Stream output)
        {
            return StreamHelper.DecodeAsync(input, output, buffer => Decode(buffer.Span).ToArray());
        }

        /// <inheritdoc/>
        public override Boolean TryEncode(ReadOnlySpan<Byte> bytes, Span<Char> output, out Int32 numCharsWritten)
        {
            return TryEncode(bytes, output, false, out numCharsWritten);
        }

        /// <summary>
        /// Encode to the given preallocated buffer.
        /// </summary>
        /// <param name="bytes">Input bytes.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="padding">Whether to use padding characters at the end.</param>
        /// <param name="numCharsWritten">Number of characters written to the output.</param>
        /// <returns>True if encoding is successful, false if the output is invalid.</returns>
        public unsafe Boolean TryEncode(
            ReadOnlySpan<Byte> bytes,
            Span<Char> output,
            Boolean padding,
            out Int32 numCharsWritten)
        {
            unchecked
            {
                Int32 bytesLen = bytes.Length;
                if (bytesLen == 0)
                {
                    numCharsWritten = 0;
                    return true;
                }

                Int32 outputLen = output.Length;

                fixed (Byte* inputPtr = bytes)
                fixed (Char* outputPtr = output)
                {
                    return InternalEncode(inputPtr, bytesLen, outputPtr, outputLen, padding, out numCharsWritten);
                }
            }
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 numBytesWritten)
        {
            unchecked
            {
                Int32 inputLen = input.Length - GetPaddingCharCount(input);
                if (inputLen == 0)
                {
                    numBytesWritten = 0;
                    return true;
                }

                Int32 outputLen = output.Length;
                if (outputLen == 0)
                {
                    numBytesWritten = 0;
                    return false;
                }

                fixed (Char* inputPtr = input)
                fixed (Byte* outputPtr = output)
                {
                    return InternalDecode(inputPtr, inputLen, outputPtr, out numBytesWritten);
                }
            }
        }

        private unsafe Boolean InternalEncode(
           Byte* inputPtr,
           Int32 bytesLen,
           Char* outputPtr,
           Int32 outputLen,
           Boolean padding,
           out Int32 numCharsWritten)
        {
            unchecked
            {
                String table = Alphabet.Value;
                Char* pOutput = outputPtr;
                Char* pOutputEnd = outputPtr + outputLen;
                Byte* pInput = inputPtr;
                Byte* pInputEnd = pInput + bytesLen;

                for (Int32 bitsLeft = BitsPerByte, currentByte = *pInput; pInput != pInputEnd;)
                {
                    Int32 outputPad;
                    if (bitsLeft > BitsPerChar)
                    {
                        bitsLeft -= BitsPerChar;
                        outputPad = currentByte >> bitsLeft;
                        *pOutput++ = table[outputPad];
                        if (pOutput > pOutputEnd)
                        {
                            goto Overflow;
                        }

                        currentByte &= (1 << bitsLeft) - 1;
                    }

                    Int32 nextBits = BitsPerChar - bitsLeft;
                    bitsLeft = BitsPerByte - nextBits;
                    outputPad = currentByte << nextBits;
                    if (++pInput != pInputEnd)
                    {
                        currentByte = *pInput;
                        outputPad |= currentByte >> bitsLeft;
                        currentByte &= (1 << bitsLeft) - 1;
                    }

                    *pOutput++ = table[outputPad];
                    if (pOutput > pOutputEnd)
                    {
                        goto Overflow;
                    }
                }

                if (padding)
                {
                    Char paddingChar = Alphabet.PaddingChar;
                    while (pOutput != pOutputEnd)
                    {
                        *pOutput++ = paddingChar;
                        if (pOutput > pOutputEnd)
                        {
                            goto Overflow;
                        }
                    }
                }

                numCharsWritten = (Int32)(pOutput - outputPtr);
                return true;
                Overflow:
                numCharsWritten = (Int32)(pOutput - outputPtr);
                return false;
            }
        }

        private static Int32 GetAllocationByteCountForDecoding(Int32 textLenWithoutPadding)
        {
            return textLenWithoutPadding * BitsPerChar / BitsPerByte;
        }

        private Int32 GetPaddingCharCount(ReadOnlySpan<Char> text)
        {
            unchecked
            {
                Char paddingChar = Alphabet.PaddingChar;
                Int32 result = 0;
                Int32 textLen = text.Length;
                while (textLen > 0 && text[--textLen] == paddingChar)
                {
                    result++;
                }

                return result;
            }
        }

        private unsafe Boolean InternalDecode(
            Char* inputPtr,
            Int32 textLen,
            Byte* outputPtr,
            out Int32 numBytesWritten)
        {
            unchecked
            {
                ReadOnlySpan<Byte> table = Alphabet.ReverseLookupTable;
                Int32 outputPad = 0;
                Int32 bitsLeft = BitsPerByte;

                Byte* pOutput = outputPtr;
                Char* pInput = inputPtr;
                Char* pEnd = inputPtr + textLen;
                while (pInput != pEnd)
                {
                    Char c = *pInput++;
                    Int32 b = table[c] - 1;
                    if (b < 0)
                    {
                        numBytesWritten = (Int32)(pOutput - outputPtr);
                        return false;
                    }

                    if (bitsLeft > BitsPerChar)
                    {
                        bitsLeft -= BitsPerChar;
                        outputPad |= b << bitsLeft;
                        continue;
                    }

                    Int32 shiftBits = BitsPerChar - bitsLeft;
                    outputPad |= b >> shiftBits;
                    *pOutput++ = (Byte)outputPad;
                    b &= (1 << shiftBits) - 1;
                    bitsLeft = BitsPerByte - shiftBits;
                    outputPad = b << bitsLeft;
                }

                numBytesWritten = (Int32)(pOutput - outputPtr);
                return true;
            }
        }
    }
}