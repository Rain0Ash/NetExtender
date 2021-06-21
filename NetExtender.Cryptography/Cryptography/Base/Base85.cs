// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="Base85.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Crypto.Base.Alphabet;
using NetExtender.Crypto.Base.Common;

namespace NetExtender.Crypto.Base
{
    /// <summary>
    /// Base58 encoding/decoding class.
    /// </summary>
    public sealed class Base85 : BaseCryptStream
    {
        private const Int32 BaseLength = 85;
        private const Int32 ByteBlockSize = 4;
        private const Int32 StringBlockSize = 5;
        private const Int64 AllSpace = 0x20202020;
        private const Int32 DecodeBufferSize = 5120; // don't remember what was special with this number

        /// <summary>
        /// Initializes a new instance of the <see cref="Base85"/> class
        /// using a custom alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base85(Base85Alphabet alphabet)
        {
            Alphabet = alphabet;
        }

        /// <summary>
        /// Gets Z85 flavor of Base85.
        /// </summary>
        public static Base85 Z85 { get; } = new Base85(Base85Alphabet.Z85);

        /// <summary>
        /// Gets Ascii85 flavor of Base85.
        /// </summary>
        public static Base85 Ascii85 { get; } = new Base85(Base85Alphabet.Ascii85);

        /// <summary>
        /// Gets the encoding alphabet.
        /// </summary>
        public Base85Alphabet Alphabet { get; }

        /// <inheritdoc/>
        public override Int32 GetSafeByteCountForDecoding(ReadOnlySpan<Char> text)
        {
            Boolean usingShortcuts = Alphabet.AllZeroShortcut is not null || Alphabet.AllSpaceShortcut is not null;
            return GetSafeByteCountForDecoding(text.Length, usingShortcuts);
        }

        /// <inheritdoc/>
        public override Int32 GetSafeCharCountForEncoding(ReadOnlySpan<Byte> bytes)
        {
            return GetSafeCharCountForEncoding(bytes.Length);
        }

        /// <summary>
        /// Encode the given bytes into Base85.
        /// </summary>
        /// <param name="bytes">Bytes to encode.</param>
        /// <returns>Encoded text.</returns>
        public override unsafe String Encode(ReadOnlySpan<Byte> bytes)
        {
            unchecked
            {
                Int32 inputLen = bytes.Length;
                if (inputLen == 0)
                {
                    return String.Empty;
                }

                Int32 outputLen = GetSafeCharCountForEncoding(bytes);
                String output = new String('\0', outputLen);

                fixed (Byte* inputPtr = bytes)
                fixed (Char* outputPtr = output)
                {
                    if (!InternalEncode(inputPtr, inputLen, outputPtr, outputLen, out Int32 numCharsWritten))
                    {
                        throw new InvalidOperationException("Insufficient output buffer size while encoding Base85");
                    }

                    return output[..numCharsWritten];
                }
            }
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryEncode(ReadOnlySpan<Byte> input, Span<Char> output, out Int32 numCharsWritten)
        {
            unchecked
            {
                Int32 inputLen = input.Length;
                if (inputLen == 0)
                {
                    numCharsWritten = 0;
                    return true;
                }

                fixed (Byte* inputPtr = input)
                fixed (Char* outputPtr = output)
                {
                    return InternalEncode(inputPtr, inputLen, outputPtr, output.Length, out numCharsWritten);
                }
            }
        }

        /// <summary>
        /// Encode a given stream into a text writer.
        /// </summary>
        /// <param name="input">Input stream.</param>
        /// <param name="output">Output writer.</param>
        public override void Encode(Stream input, TextWriter output)
        {
            StreamHelper.Encode(input, output, (buffer, lastBlock) => Encode(buffer.Span));
        }

        /// <summary>
        /// Encode a given stream into a text writer.
        /// </summary>
        /// <param name="input">Input stream.</param>
        /// <param name="output">Output writer.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task EncodeAsync(Stream input, TextWriter output)
        {
            return StreamHelper.EncodeAsync(input, output, (buffer, lastBlock) => Encode(buffer.Span));
        }

        /// <summary>
        /// Decode a text reader into a stream.
        /// </summary>
        /// <param name="input">Input reader.</param>
        /// <param name="output">Output stream.</param>
        public override void Decode(TextReader input, Stream output)
        {
            StreamHelper.Decode(input, output, (text) => Decode(text.Span).ToArray(), DecodeBufferSize);
        }

        /// <summary>
        /// Decode a text reader into a stream.
        /// </summary>
        /// <param name="input">Input reader.</param>
        /// <param name="output">Output stream.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task DecodeAsync(TextReader input, Stream output)
        {
            return StreamHelper.DecodeAsync(input, output, (text) => Decode(text.Span).ToArray(), DecodeBufferSize);
        }

        /// <summary>
        /// Decode given characters into bytes.
        /// </summary>
        /// <param name="text">Characters to decode.</param>
        /// <returns>Decoded bytes.</returns>
        public override unsafe Span<Byte> Decode(ReadOnlySpan<Char> text)
        {
            unchecked
            {
                Int32 textLen = text.Length;
                if (textLen == 0)
                {
                    return Array.Empty<Byte>();
                }

                Boolean usingShortcuts = Alphabet.HasShortcut;

                // allocate a larger buffer if we're using shortcuts
                Int32 decodeBufferLen = GetSafeByteCountForDecoding(textLen, usingShortcuts);
                Byte[] decodeBuffer = new Byte[decodeBufferLen];
                fixed (Char* inputPtr = text)
                fixed (Byte* decodeBufferPtr = decodeBuffer)
                {
                    InternalDecode(inputPtr, textLen, decodeBufferPtr, decodeBufferLen, out Int32 numBytesWritten);
                    return decodeBuffer[..numBytesWritten];
                }
            }
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 numBytesWritten)
        {
            fixed (Char* inputPtr = input)
            fixed (Byte* outputPtr = output)
            {
                return InternalDecode(inputPtr, input.Length, outputPtr, output.Length, out numBytesWritten);
            }
        }

        private unsafe Boolean InternalEncode(
            Byte* inputPtr,
            Int32 inputLen,
            Char* outputPtr,
            Int32 outputLen,
            out Int32 numCharsWritten)
        {
            unchecked
            {
                Boolean usesZeroShortcut = Alphabet.AllZeroShortcut is not null;
                Boolean usesSpaceShortcut = Alphabet.AllSpaceShortcut is not null;
                String table = Alphabet.Value;
                Int32 fullLen = (inputLen >> 2) << 2; // size of whole 4-byte blocks

                Char* pOutput = outputPtr;
                Char* pOutputEnd = pOutput + outputLen;
                Byte* pInput = inputPtr;
                Byte* pInputEnd = pInput + fullLen;
                while (pInput != pInputEnd)
                {
                    // build a 32-bit representation of input
                    Int64 input = ((UInt32)(*pInput++) << 24)
                                  | ((UInt32)(*pInput++) << 16)
                                  | ((UInt32)(*pInput++) << 8)
                                  | *pInput++;

                    if (WriteEncodedValue(
                        input,
                        ref pOutput,
                        pOutputEnd,
                        table,
                        StringBlockSize,
                        usesZeroShortcut,
                        usesSpaceShortcut))
                    {
                        continue;
                    }

                    numCharsWritten = 0;
                    return false;
                }

                // check if a part is remaining
                Int32 remainingBytes = inputLen - fullLen;
                if (remainingBytes > 0)
                {
                    Int64 input = 0;
                    for (Int32 n = 0; n < remainingBytes; n++)
                    {
                        input |= (UInt32)(*pInput++) << ((3 - n) << 3);
                    }

                    if (!WriteEncodedValue(
                        input,
                        ref pOutput,
                        pOutputEnd,
                        table,
                        remainingBytes + 1,
                        usesZeroShortcut,
                        usesSpaceShortcut))
                    {
                        numCharsWritten = 0;
                        return false;
                    }
                }

                numCharsWritten = (Int32) (pOutput - outputPtr);
                return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe Boolean WriteEncodedValue(
            Int64 input,
            ref Char* pOutput,
            Char* pOutputEnd,
            String table,
            Int32 stringLength,
            Boolean usesZeroShortcut,
            Boolean usesSpaceShortcut)
        {
            unchecked
            {
                switch (input)
                {
                    // handle shortcuts
                    case 0 when usesZeroShortcut:
                    {
                        if (pOutput >= pOutputEnd)
                        {
                            return false;
                        }

                        *pOutput++ = Alphabet.AllZeroShortcut ?? '!'; // guaranteed to be non-null
                        return true;
                    }
                    case AllSpace when usesSpaceShortcut:
                    {
                        if (pOutput >= pOutputEnd)
                        {
                            return false;
                        }

                        *pOutput++ = Alphabet.AllSpaceShortcut ?? '!'; // guaranteed to be non-null
                        return true;
                    }
                }

                if (pOutput >= pOutputEnd - stringLength)
                {
                    return false;
                }

                // map the 4-byte packet to to 5-byte octets
                for (Int32 i = StringBlockSize - 1; i >= 0; i--)
                {
                    input = Math.DivRem(input, BaseLength, out Int64 result);
                    if (i < stringLength)
                    {
                        pOutput[i] = table[(Int32) result];
                    }
                }

                pOutput += stringLength;
                return true;
            }
        }

        private unsafe Boolean InternalDecode(
            Char* inputPtr,
            Int32 inputLen,
            Byte* outputPtr,
            Int32 outputLen,
            out Int32 numBytesWritten)
        {
            unchecked
            {
                Char? allZeroChar = Alphabet.AllZeroShortcut;
                Char? allSpaceChar = Alphabet.AllSpaceShortcut;
                Boolean checkZero = allZeroChar is not null;
                Boolean checkSpace = allSpaceChar is not null;

                ReadOnlySpan<Byte> table = Alphabet.ReverseLookupTable;
                Byte* pOutput = outputPtr;
                Char* pInput = inputPtr;
                Char* pInputEnd = pInput + inputLen;
                Byte* pOutputEnd = pOutput + outputLen;

                Int32 blockIndex = 0;
                Int64 value = 0;
                while (pInput != pInputEnd)
                {
                    Char c = *pInput++;
                    if (IsWhiteSpace(c))
                    {
                        continue;
                    }

                    // handle shortcut characters
                    if (checkZero && c == allZeroChar)
                    {
                        if (!WriteShortcut(ref pOutput, pOutputEnd, ref blockIndex, 0))
                        {
                            goto Error;
                        }

                        continue;
                    }

                    if (checkSpace && c == allSpaceChar)
                    {
                        if (!WriteShortcut(ref pOutput, pOutputEnd, ref blockIndex, AllSpace))
                        {
                            goto Error;
                        }

                        continue;
                    }

                    // handle regular blocks
                    Int32 x = table[c] - 1; // map character to byte value
                    if (x < 0)
                    {
                        throw EncodingAlphabet.InvalidCharacter(c);
                    }

                    value = value * BaseLength + x;
                    blockIndex += 1;
                    if (blockIndex != StringBlockSize)
                    {
                        continue;
                    }

                    if (!WriteDecodedValue(ref pOutput, pOutputEnd, value, ByteBlockSize))
                    {
                        goto Error;
                    }

                    blockIndex = 0;
                    value = 0;
                }

                if (blockIndex > 0)
                {
                    // handle padding by treating the rest of the characters
                    // as "u"s. so both big endianness and bit weirdness work out okay.
                    for (Int32 i = 0; i < StringBlockSize - blockIndex; i++)
                    {
                        value = value * BaseLength + (BaseLength - 1);
                    }

                    if (!WriteDecodedValue(ref pOutput, pOutputEnd, value, blockIndex - 1))
                    {
                        goto Error;
                    }
                }

                numBytesWritten = (Int32) (pOutput - outputPtr);
                return true;
                Error:
                numBytesWritten = 0;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe Boolean WriteDecodedValue(
            ref Byte* pOutput,
            Byte* pOutputEnd,
            Int64 value,
            Int32 numBytesToWrite)
        {
            unchecked
            {
                if (pOutput + numBytesToWrite > pOutputEnd)
                {
                    return false;
                }

                for (Int32 i = ByteBlockSize - 1; i >= 0 && numBytesToWrite > 0; i--, numBytesToWrite--)
                {
                    Byte b = (Byte) ((value >> (i << 3)) & 0xFF);
                    *pOutput++ = b;
                }

                return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsWhiteSpace(Char c)
        {
            return c == ' ' || c == 0x85 || c == 0xA0 || c >= 0x09 && c <= 0x0D;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe Boolean WriteShortcut(
            ref Byte* pOutput,
            Byte* pOutputEnd,
            ref Int32 blockIndex,
            Int64 value)
        {
            if (blockIndex != 0)
            {
                throw new ArgumentException(
                    "Unexpected shortcut character in the middle of a regular block");
            }

            blockIndex = 0; // restart block after the shortcut character
            return WriteDecodedValue(ref pOutput, pOutputEnd, value, ByteBlockSize);
        }

        private static Int32 GetSafeCharCountForEncoding(Int32 bytesLength)
        {
            if (bytesLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bytesLength));
            }

            if (bytesLength == 0)
            {
                return 0;
            }

            return (bytesLength + ByteBlockSize - 1) * StringBlockSize / ByteBlockSize;
        }

        private static Int32 GetSafeByteCountForDecoding(Int32 textLength, Boolean usingShortcuts)
        {
            if (usingShortcuts)
            {
                return textLength * ByteBlockSize; // max possible size using shortcuts
            }

            // max possible size without shortcuts
            return ((textLength - 1) / StringBlockSize + 1) * ByteBlockSize;
        }
    }
}