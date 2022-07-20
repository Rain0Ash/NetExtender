// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="Base58.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;
using NetExtender.Cryptography.Base.Alphabet;
using NetExtender.Cryptography.Base.Common;

namespace NetExtender.Cryptography.Base
{
    /// <summary>
    /// Base58 Encoding/Decoding implementation.
    /// </summary>
    /// <remarks>
    /// Base58 doesn't implement a Stream-based interface because it's not feasible to use
    /// on large buffers.
    /// </remarks>
    public sealed class Base58 : BaseCryptography
    {
        public static Base58 Bitcoin { get; } = new Base58(Base58Alphabet.Bitcoin);
        public static Base58 Ripple { get; } = new Base58(Base58Alphabet.Ripple);
        public static Base58 Flickr { get; } = new Base58(Base58Alphabet.Flickr);

        /// <summary>
        /// Initializes a new instance of the <see cref="Base58"/> class
        /// using a custom alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base58(Base58Alphabet alphabet)
        {
            Alphabet = alphabet;
        }

        /// <summary>
        /// Gets the encoding alphabet.
        /// </summary>
        public Base58Alphabet Alphabet { get; }

        /// <inheritdoc/>
        public override Int32 GetSafeByteCountForDecoding(ReadOnlySpan<Char> text)
        {
            const Int32 reductionFactor = 733;

            return (text.Length + 1) * reductionFactor / 1000 + 1;
        }

        /// <inheritdoc/>
        public override Int32 GetSafeCharCountForEncoding(ReadOnlySpan<Byte> bytes)
        {
            Int32 bytesLen = bytes.Length;
            Int32 numZeroes = GetZeroCount(bytes, bytesLen);

            return GetSafeCharCountForEncoding(bytesLen, numZeroes);
        }

        /// <summary>
        /// Encode to Base58 representation.
        /// </summary>
        /// <param name="bytes">Bytes to encode.</param>
        /// <returns>Encoded string.</returns>
        public override unsafe String Encode(ReadOnlySpan<Byte> bytes)
        {
            unchecked
            {
                Int32 bytesLen = bytes.Length;
                if (bytesLen == 0)
                {
                    return String.Empty;
                }

                Int32 numZeroes = GetZeroCount(bytes, bytesLen);
                Int32 outputLen = GetSafeCharCountForEncoding(bytesLen, numZeroes);
                String output = new String('\0', outputLen);

                // 29.70µs (64.9x slower)   | 31.63µs (40.8x slower)
                // 30.93µs (first tryencode impl)
                // 29.36µs (single pass translation/copy + shift over multiply)
                // 31.04µs (70x slower)     | 24.71µs (34.3x slower)
                fixed (Byte* inputPtr = bytes)
                fixed (Char* outputPtr = output)
                {
                    if (!InternalEncode(inputPtr, bytesLen, outputPtr, outputLen, numZeroes, out Int32 length))
                    {
                        throw new InvalidOperationException("Output buffer with insufficient size generated");
                    }

                    return output[..length];
                }
            }
        }

        /// <summary>
        /// Decode a Base58 representation.
        /// </summary>
        /// <param name="text">Base58 encoded text.</param>
        /// <returns>Array of decoded bytes.</returns>
        public override unsafe Span<Byte> Decode(ReadOnlySpan<Char> text)
        {
            unchecked
            {
                Int32 textLen = text.Length;
                if (textLen == 0)
                {
                    return Array.Empty<Byte>();
                }

                Int32 outputLen = GetSafeByteCountForDecoding(text);
                Char zeroChar = Alphabet.Value[0];
                Int32 numZeroes = GetPrefixCount(text, textLen, zeroChar);
                Byte[] output = new Byte[outputLen];
                fixed (Char* inputPtr = text)
                fixed (Byte* outputPtr = output)
                {
                    if (!InternalDecode(
                        inputPtr,
                        textLen,
                        outputPtr,
                        outputLen,
                        numZeroes,
                        out Int32 numBytesWritten))
                    {
                        throw new InvalidOperationException("Output buffer was too small while decoding Base58");
                    }

                    return output[..numBytesWritten];
                }
            }
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryEncode(ReadOnlySpan<Byte> input, Span<Char> output, out Int32 numCharsWritten)
        {
            fixed (Byte* inputPtr = input)
            fixed (Char* outputPtr = output)
            {
                Int32 inputLen = input.Length;
                Int32 numZeroes = GetZeroCount(input, inputLen);
                return InternalEncode(inputPtr, inputLen, outputPtr, output.Length, numZeroes, out numCharsWritten);
            }
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 numBytesWritten)
        {
            unchecked
            {
                Int32 inputLen = input.Length;
                if (inputLen == 0)
                {
                    numBytesWritten = 0;
                    return true;
                }

                Int32 zeroCount = GetPrefixCount(input, inputLen, Alphabet.Value[0]);
                fixed (Char* inputPtr = input)
                fixed (Byte* outputPtr = output)
                {
                    return InternalDecode(
                        inputPtr,
                        input.Length,
                        outputPtr,
                        output.Length,
                        zeroCount,
                        out numBytesWritten);
                }
            }
        }

        // ReSharper disable once CognitiveComplexity
        private unsafe Boolean InternalDecode(Char* inputPtr, Int32 inputLen, Byte* outputPtr, Int32 outputLen, Int32 numZeroes, out Int32 numBytesWritten)
        {
            unchecked
            {
                Char* pInputEnd = inputPtr + inputLen;
                Char* pInput = inputPtr + numZeroes;
                if (pInput == pInputEnd)
                {
                    if (numZeroes > outputLen)
                    {
                        numBytesWritten = 0;
                        return false;
                    }

                    Byte* pOutput = outputPtr;
                    for (Int32 i = 0; i < numZeroes; i++)
                    {
                        *pOutput++ = 0;
                    }

                    numBytesWritten = numZeroes;
                    return true;
                }

                ReadOnlySpan<Byte> table = Alphabet.ReverseLookupTable;
                Byte* pOutputEnd = outputPtr + outputLen - 1;
                Byte* pMinOutput = pOutputEnd;
                while (pInput != pInputEnd)
                {
                    Char c = *pInput;
                    Int32 carry = table[c] - 1;
                    if (carry < 0)
                    {
                        throw EncodingAlphabet.InvalidCharacter(c);
                    }

                    for (Byte* pOutput = pOutputEnd; pOutput >= outputPtr; pOutput--)
                    {
                        carry += 58 * *pOutput;
                        *pOutput = (Byte) carry;
                        if (pMinOutput > pOutput && carry != 0)
                        {
                            pMinOutput = pOutput;
                        }

                        carry >>= 8;
                    }

                    pInput++;
                }

                Int32 startIndex = (Int32) (pMinOutput - numZeroes - outputPtr);
                numBytesWritten = outputLen - startIndex;
                Buffer.MemoryCopy(outputPtr + startIndex, outputPtr, numBytesWritten, numBytesWritten);
                return true;
            }
        }

        // ReSharper disable once CognitiveComplexity
        private unsafe Boolean InternalEncode(Byte* inputPtr, Int32 inputLen, Char* outputPtr, Int32 outputLen, Int32 numZeroes, out Int32 numCharsWritten)
        {
            unchecked
            {
                if (inputLen == 0)
                {
                    numCharsWritten = 0;
                    return true;
                }

                fixed (Char* alphabetPtr = Alphabet.Value)
                {
                    Byte* pInput = inputPtr + numZeroes;
                    Byte* pInputEnd = inputPtr + inputLen;
                    Char zeroChar = alphabetPtr[0];

                    // optimized path for an all zero buffer
                    if (pInput == pInputEnd)
                    {
                        if (outputLen < numZeroes)
                        {
                            numCharsWritten = 0;
                            return false;
                        }

                        for (Int32 i = 0; i < numZeroes; i++)
                        {
                            *outputPtr++ = zeroChar;
                        }

                        numCharsWritten = numZeroes;
                        return true;
                    }

                    Int32 length = 0;
                    Char* pOutput = outputPtr;
                    Char* pLastChar = pOutput + outputLen - 1;
                    while (pInput != pInputEnd)
                    {
                        Int32 carry = *pInput;
                        Int32 i = 0;
                        for (Char* pDigit = pLastChar;
                            (carry != 0 || i < length)
                            && pDigit >= outputPtr;
                            pDigit--, i++)
                        {
                            carry += *pDigit << 8;
                            carry = Math.DivRem(carry, 58, out Int32 remainder);
                            *pDigit = (Char) remainder;
                        }

                        length = i;
                        pInput++;
                    }

                    Char* pOutputEnd = pOutput + outputLen;

                    // copy the characters to the beginning of the buffer
                    // and translate them at the same time. if no copying
                    // is needed, this only acts as the translation phase.
                    for (Char* a = outputPtr + numZeroes, b = pOutputEnd - length;
                        b != pOutputEnd;
                        a++, b++)
                    {
                        *a = alphabetPtr[*b];
                    }

                    // translate the zeroes at the start
                    while (pOutput != pOutputEnd)
                    {
                        Char c = *pOutput;
                        if (c != '\0')
                        {
                            break;
                        }

                        *pOutput = alphabetPtr[c];
                        pOutput++;
                    }

                    Int32 actualLen = numZeroes + length;

                    numCharsWritten = actualLen;
                    return true;
                }
            }
        }

        private static unsafe Int32 GetZeroCount(ReadOnlySpan<Byte> bytes, Int32 bytesLen)
        {
            unchecked
            {
                if (bytesLen == 0)
                {
                    return 0;
                }

                Int32 numZeroes = 0;
                fixed (Byte* inputPtr = bytes)
                {
                    Byte* pInput = inputPtr;
                    while (*pInput == 0 && numZeroes < bytesLen)
                    {
                        numZeroes++;
                        pInput++;
                    }
                }

                return numZeroes;
            }
        }

        private static unsafe Int32 GetPrefixCount(ReadOnlySpan<Char> input, Int32 length, Char value)
        {
            if (length == 0)
            {
                return 0;
            }

            Int32 numZeroes = 0;
            fixed (Char* inputPtr = input)
            {
                Char* pInput = inputPtr;
                while (*pInput == value && numZeroes < length)
                {
                    numZeroes++;
                    pInput++;
                }
            }

            return numZeroes;
        }

        private static Int32 GetSafeCharCountForEncoding(Int32 bytesLen, Int32 numZeroes)
        {
            const Int32 growthPercentage = 138;

            return numZeroes + (bytesLen - numZeroes) * growthPercentage / 100 + 1;
        }
    }
}