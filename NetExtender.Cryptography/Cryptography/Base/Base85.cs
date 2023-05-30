// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Cryptography.Base.Alphabet;
using NetExtender.Cryptography.Base.Common;

namespace NetExtender.Cryptography.Base
{
    /// <summary>
    /// Base58 encoding/decoding class.
    /// </summary>
    public sealed class Base85 : BaseCryptographyStream
    {
        private const Int32 BaseLength = 85;
        private const Int32 ByteBlockSize = 4;
        private const Int32 StringBlockSize = 5;
        private const Int64 AllSpace = 0x20202020;
        private const Int32 DecodeBufferSize = 5120;

        public static Base85 Z85 { get; } = new Base85(Base85Alphabet.Z85);
        public static Base85 Ascii85 { get; } = new Base85(Base85Alphabet.Ascii85);

        /// <summary>
        /// Gets the encoding alphabet.
        /// </summary>
        public Base85Alphabet Alphabet { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Base85"/> class
        /// using a custom alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base85(Base85Alphabet alphabet)
        {
            Alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsWhiteSpace(Char character)
        {
            return character == ' ' || character == 0x85 || character == 0xA0 || character >= 0x09 && character <= 0x0D;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe Boolean WriteShortcut(ref Byte* pointer, Byte* end, ref Int32 block, Int64 value)
        {
            if (block != 0)
            {
                throw new ArgumentException("Unexpected shortcut character in the middle of a regular block");
            }

            block = 0;
            return WriteValue(ref pointer, end, value, ByteBlockSize);
        }

        private static Int32 SafeCharCountForEncoding(Int32 length)
        {
            return length switch
            {
                < 0 => throw new ArgumentOutOfRangeException(nameof(length), length, null),
                0 => 0,
                _ => (length + ByteBlockSize - 1) * StringBlockSize / ByteBlockSize
            };
        }

        /// <inheritdoc/>
        public override Int32 SafeCharCountForEncoding(ReadOnlySpan<Byte> value)
        {
            return SafeCharCountForEncoding(value.Length);
        }

        private static Int32 SafeByteCountForDecoding(Int32 length, Boolean shortcut)
        {
            return shortcut ? length * ByteBlockSize : ((length - 1) / StringBlockSize + 1) * ByteBlockSize;
        }

        /// <inheritdoc/>
        public override Int32 SafeByteCountForDecoding(ReadOnlySpan<Char> buffer)
        {
            return SafeByteCountForDecoding(buffer.Length, Alphabet.ZeroShortcut is not null || Alphabet.SpaceShortcut is not null);
        }

        /// <summary>
        /// Encode the given bytes into Base85.
        /// </summary>
        /// <param name="value">Bytes to encode.</param>
        /// <returns>Encoded text.</returns>
        public override unsafe String Encode(ReadOnlySpan<Byte> value)
        {
            unchecked
            {
                if (value.Length <= 0)
                {
                    return String.Empty;
                }

                Int32 count = SafeCharCountForEncoding(value);
                String output = new String('\0', count);

                fixed (Byte* pinput = value)
                fixed (Char* poutput = output)
                {
                    if (!InternalEncode(pinput, value.Length, poutput, count, out Int32 written))
                    {
                        throw new InvalidOperationException("Insufficient output buffer size while encoding Base85");
                    }

                    return output[..written];
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

        /// <inheritdoc/>
        public override unsafe Boolean TryEncode(ReadOnlySpan<Byte> value, Span<Char> output, out Int32 written)
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
                    return InternalEncode(pinput, value.Length, poutput, output.Length, out written);
                }
            }
        }

        /// <summary>
        /// Encode a given stream into a text writer.
        /// </summary>
        /// <param name="input">Input stream.</param>
        /// <param name="output">Output writer.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task EncodeAsync(Stream input, TextWriter output)
        {
            return EncodeAsync(input, output, (buffer, _) => Encode(buffer.Span));
        }

        private unsafe Boolean InternalEncode(Byte* pinput, Int32 inputlength, Char* poutput, Int32 outputlength, out Int32 written)
        {
            unchecked
            {
                Int32 total = (inputlength >> 2) << 2; // size of whole 4-byte blocks

                Char* poutputstart = poutput;
                Char* poutputend = poutputstart + outputlength;
                Byte* pinputend = pinput + total;
                while (pinput != pinputend)
                {
                    // build a 32-bit representation of input
                    Int64 input = ((UInt32) (*pinput++) << 24) | ((UInt32) (*pinput++) << 16) | ((UInt32) (*pinput++) << 8) | *pinput++;

                    if (WriteValue(input, ref poutput, poutputend, Alphabet.Value, StringBlockSize, Alphabet.ZeroShortcut.HasValue, Alphabet.SpaceShortcut.HasValue))
                    {
                        continue;
                    }

                    written = 0;
                    return false;
                }

                // check if a part is remaining
                Int32 remaining = inputlength - total;
                if (remaining > 0)
                {
                    Int64 input = 0;
                    for (Int32 i = 0; i < remaining; i++)
                    {
                        input |= (UInt32) (*pinput++) << ((3 - i) << 3);
                    }

                    if (!WriteValue(input, ref poutput, poutputend, Alphabet.Value, remaining + 1, Alphabet.ZeroShortcut.HasValue, Alphabet.SpaceShortcut.HasValue))
                    {
                        written = 0;
                        return false;
                    }
                }

                written = (Int32) (poutput - poutputstart);
                return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe Boolean WriteValue(Int64 input, ref Char* poutput, Char* poutputend, String table, Int32 length, Boolean zero, Boolean space)
        {
            unchecked
            {
                switch (input)
                {
                    case 0 when zero:
                    {
                        if (poutput >= poutputend)
                        {
                            return false;
                        }

                        *poutput++ = Alphabet.ZeroShortcut ?? '!';
                        return true;
                    }
                    case AllSpace when space:
                    {
                        if (poutput >= poutputend)
                        {
                            return false;
                        }

                        *poutput++ = Alphabet.SpaceShortcut ?? '!';
                        return true;
                    }
                }

                if (poutput >= poutputend - length)
                {
                    return false;
                }

                for (Int32 i = StringBlockSize - 1; i >= 0; i--)
                {
                    input = Math.DivRem(input, BaseLength, out Int64 result);
                    if (i < length)
                    {
                        poutput[i] = table[(Int32) result];
                    }
                }

                poutput += length;
                return true;
            }
        }

        /// <summary>
        /// Decode given characters into bytes.
        /// </summary>
        /// <param name="value">Characters to decode.</param>
        /// <returns>Decoded bytes.</returns>
        public override unsafe Span<Byte> Decode(ReadOnlySpan<Char> value)
        {
            unchecked
            {
                if (value.Length <= 0)
                {
                    return Array.Empty<Byte>();
                }

                Byte[] buffer = new Byte[SafeByteCountForDecoding(value.Length, Alphabet.HasShortcut)];
                
                fixed (Char* pointer = value)
                fixed (Byte* pbuffer = buffer)
                {
                    InternalDecode(pointer, value.Length, pbuffer, buffer.Length, out Int32 written);
                    return buffer.AsSpan()[..written];
                }
            }
        }

        /// <summary>
        /// Decode a text reader into a stream.
        /// </summary>
        /// <param name="input">Input reader.</param>
        /// <param name="output">Output stream.</param>
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

            Decode(input, output, text => Decode(text.Span).ToArray(), DecodeBufferSize);
        }

        /// <inheritdoc/>
        public override unsafe Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 written)
        {
            fixed (Char* pinput = input)
            fixed (Byte* poutput = output)
            {
                return InternalDecode(pinput, input.Length, poutput, output.Length, out written);
            }
        }

        /// <summary>
        /// Decode a text reader into a stream.
        /// </summary>
        /// <param name="input">Input reader.</param>
        /// <param name="output">Output stream.</param>
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

            return DecodeAsync(input, output, text => Decode(text.Span).ToArray(), DecodeBufferSize);
        }

        // ReSharper disable once CognitiveComplexity
        private unsafe Boolean InternalDecode(Char* pinput, Int32 inputlength, Byte* poutput, Int32 outputlength, out Int32 written)
        {
            unchecked
            {
                ReadOnlySpan<Byte> table = Alphabet.ReverseLookupTable;
                Byte* poutputstart = poutput;
                Char* pinputend = pinput + inputlength;
                Byte* poutputend = poutputstart + outputlength;

                Int32 block = 0;
                Int64 value = 0;
                while (pinput != pinputend)
                {
                    Char character = *pinput++;
                    if (IsWhiteSpace(character))
                    {
                        continue;
                    }

                    if (character == Alphabet.ZeroShortcut)
                    {
                        if (!WriteShortcut(ref poutput, poutputend, ref block, 0))
                        {
                            written = 0;
                            return false;
                        }

                        continue;
                    }

                    if (character == Alphabet.SpaceShortcut)
                    {
                        if (!WriteShortcut(ref poutput, poutputend, ref block, AllSpace))
                        {
                            written = 0;
                            return false;
                        }

                        continue;
                    }

                    Int32 x = table[character] - 1;
                    if (x < 0)
                    {
                        throw new ArgumentException($"Invalid character: {character}");
                    }

                    value = value * BaseLength + x;
                    block += 1;
                    if (block != StringBlockSize)
                    {
                        continue;
                    }

                    if (!WriteValue(ref poutput, poutputend, value, ByteBlockSize))
                    {
                        written = 0;
                        return false;
                    }

                    block = 0;
                    value = 0;
                }

                if (block > 0)
                {
                    for (Int32 i = 0; i < StringBlockSize - block; i++)
                    {
                        value = value * BaseLength + (BaseLength - 1);
                    }

                    if (!WriteValue(ref poutput, poutputend, value, block - 1))
                    {
                        written = 0;
                        return false;
                    }
                }

                written = (Int32) (poutput - poutputstart);
                return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe Boolean WriteValue(ref Byte* poutput, Byte* poutputend, Int64 value, Int32 write)
        {
            unchecked
            {
                if (poutput + write > poutputend)
                {
                    return false;
                }

                for (Int32 i = ByteBlockSize - 1; i >= 0 && write > 0; i--, write--)
                {
                    Byte b = (Byte) ((value >> (i << 3)) & Byte.MaxValue);
                    *poutput++ = b;
                }

                return true;
            }
        }
    }
}