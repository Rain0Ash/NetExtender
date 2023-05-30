// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        /// Gets the encoding alphabet.
        /// </summary>
        public Base58Alphabet Alphabet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base58"/> class
        /// using a custom alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base58(Base58Alphabet alphabet)
        {
            Alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet));
        }
        
        private static unsafe Int32 ZeroCount(ReadOnlySpan<Byte> value)
        {
            unchecked
            {
                if (value.Length <= 0)
                {
                    return 0;
                }

                Int32 zeroes = 0;
                fixed (Byte* pointer = value)
                {
                    Byte* pinput = pointer;
                    while (*pinput == 0 && zeroes < value.Length)
                    {
                        zeroes++;
                        pinput++;
                    }
                }

                return zeroes;
            }
        }

        private static unsafe Int32 GetPrefixCount(ReadOnlySpan<Char> input, Int32 length, Char value)
        {
            if (length <= 0)
            {
                return 0;
            }

            Int32 zeroes = 0;
            fixed (Char* pointer = input)
            {
                Char* pinput = pointer;
                while (*pinput == value && zeroes < length)
                {
                    zeroes++;
                    pinput++;
                }
            }

            return zeroes;
        }

        private static Int32 SafeCharCountForEncoding(Int32 length, Int32 zeroes)
        {
            const Int32 growth = 138;
            return zeroes + (length - zeroes) * growth / 100 + 1;
        }

        /// <inheritdoc/>
        public override Int32 SafeCharCountForEncoding(ReadOnlySpan<Byte> buffer)
        {
            Int32 zeroes = ZeroCount(buffer);
            return SafeCharCountForEncoding(buffer.Length, zeroes);
        }

        /// <inheritdoc/>
        public override Int32 SafeByteCountForDecoding(ReadOnlySpan<Char> buffer)
        {
            const Int32 factor = 733;
            return (buffer.Length + 1) * factor / 1000 + 1;
        }

        /// <summary>
        /// Encode to Base58 representation.
        /// </summary>
        /// <param name="value">Bytes to encode.</param>
        /// <returns>Encoded string.</returns>
        public override unsafe String Encode(ReadOnlySpan<Byte> value)
        {
            unchecked
            {
                if (value.Length <= 0)
                {
                    return String.Empty;
                }

                Int32 zeroes = ZeroCount(value);
                Int32 length = SafeCharCountForEncoding(value.Length, zeroes);
                String output = new String('\0', length);
                
                fixed (Byte* pinput = value)
                fixed (Char* poutput = output)
                {
                    if (!InternalEncode(pinput, value.Length, poutput, length, zeroes, out Int32 written))
                    {
                        throw new InvalidOperationException("Output buffer with insufficient size generated");
                    }

                    return output[..written];
                }
            }
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryEncode(ReadOnlySpan<Byte> value, Span<Char> output, out Int32 written)
        {
            fixed (Byte* pinput = value)
            fixed (Char* poutput = output)
            {
                return InternalEncode(pinput, value.Length, poutput, output.Length, ZeroCount(value), out written);
            }
        }

        // ReSharper disable once CognitiveComplexity
        private unsafe Boolean InternalEncode(Byte* pinput, Int32 inputlength, Char* poutput, Int32 outputlength, Int32 zeroes, out Int32 written)
        {
            unchecked
            {
                if (inputlength == 0)
                {
                    written = 0;
                    return true;
                }

                fixed (Char* palphabet = Alphabet.Value)
                {
                    Byte* pinputend = pinput + inputlength;
                    pinput += zeroes;
                    Char zerochar = palphabet[0];

                    // optimized path for an all zero buffer
                    if (pinput == pinputend)
                    {
                        if (outputlength < zeroes)
                        {
                            written = 0;
                            return false;
                        }

                        for (Int32 i = 0; i < zeroes; i++)
                        {
                            *poutput++ = zerochar;
                        }

                        written = zeroes;
                        return true;
                    }

                    Int32 length = 0;
                    Char* poutputstart = poutput;
                    Char* plastchar = poutput + outputlength - 1;
                    while (pinput != pinputend)
                    {
                        Int32 carry = *pinput;
                        Int32 i = 0;
                        for (Char* pdigit = plastchar; (carry != 0 || i < length) && pdigit >= poutputstart; pdigit--, i++)
                        {
                            carry += *pdigit << 8;
                            carry = Math.DivRem(carry, 58, out Int32 remainder);
                            *pdigit = (Char) remainder;
                        }

                        length = i;
                        pinput++;
                    }

                    Char* poutputend = poutput + outputlength;

                    // copy the characters to the beginning of the buffer
                    // and translate them at the same time. if no copying
                    // is needed, this only acts as the translation phase.
                    for (Char* i = poutputstart + zeroes, j = poutputend - length; j != poutputend; i++, j++)
                    {
                        *i = palphabet[*j];
                    }

                    // translate the zeroes at the start
                    while (poutput != poutputend)
                    {
                        Char character = *poutput;
                        if (character != '\0')
                        {
                            break;
                        }

                        *poutput = palphabet[character];
                        poutput++;
                    }

                    written = zeroes + length;
                    return true;
                }
            }
        }

        /// <summary>
        /// Decode a Base58 representation.
        /// </summary>
        /// <param name="value">Base58 encoded text.</param>
        /// <returns>Array of decoded bytes.</returns>
        public override unsafe Span<Byte> Decode(ReadOnlySpan<Char> value)
        {
            unchecked
            {
                if (value.Length <= 0)
                {
                    return Array.Empty<Byte>();
                }

                Int32 count = SafeByteCountForDecoding(value);
                Int32 zeroes = GetPrefixCount(value, value.Length, Alphabet.Value[0]);
                
                Byte[] output = new Byte[count];
                fixed (Char* pinput = value)
                fixed (Byte* poutput = output)
                {
                    if (!InternalDecode(pinput, value.Length, poutput, count, zeroes, out Int32 written))
                    {
                        throw new InvalidOperationException("Output buffer was too small while decoding Base58");
                    }

                    return output.AsSpan()[..written];
                }
            }
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 written)
        {
            unchecked
            {
                if (input.Length <= 0)
                {
                    written = 0;
                    return true;
                }

                Int32 zeroes = GetPrefixCount(input, input.Length, Alphabet.Value[0]);
                
                fixed (Char* pinput = input)
                fixed (Byte* poutput = output)
                {
                    return InternalDecode(pinput, input.Length, poutput, output.Length, zeroes, out written);
                }
            }
        }

        // ReSharper disable once CognitiveComplexity

        private unsafe Boolean InternalDecode(Char* pinput, Int32 inputlength, Byte* poutput, Int32 outputlength, Int32 zeroes, out Int32 written)
        {
            unchecked
            {
                Byte* poutputstart = poutput;
                Char* pinputend = pinput + inputlength;
                pinput += zeroes;
                
                if (pinput == pinputend)
                {
                    if (zeroes > outputlength)
                    {
                        written = 0;
                        return false;
                    }

                    poutput = poutputstart;
                    for (Int32 i = 0; i < zeroes; i++)
                    {
                        *poutput++ = 0;
                    }

                    written = zeroes;
                    return true;
                }

                ReadOnlySpan<Byte> table = Alphabet.ReverseLookupTable;
                Byte* poutputend = poutputstart + outputlength - 1;
                Byte* pminimumoutput = poutputend;
                while (pinput != pinputend)
                {
                    Char character = *pinput;
                    Int32 carry = table[character] - 1;
                    if (carry < 0)
                    {
                        throw new ArgumentException($"Invalid character: {character}");
                    }

                    for (poutput = poutputend; poutput >= poutputstart; poutput--)
                    {
                        carry += 58 * *poutput;
                        *poutput = (Byte) carry;
                        if (pminimumoutput > poutput && carry != 0)
                        {
                            pminimumoutput = poutput;
                        }

                        carry >>= 8;
                    }

                    pinput++;
                }

                Int32 start = (Int32) (pminimumoutput - zeroes - poutputstart);
                written = outputlength - start;
                Buffer.MemoryCopy(poutputstart + start, poutputstart, written, written);
                return true;
            }
        }
    }
}