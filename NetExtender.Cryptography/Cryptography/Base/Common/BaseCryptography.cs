// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;
using NetExtender.Cryptography.Base.Interfaces;

namespace NetExtender.Cryptography.Base.Common
{
    public abstract class BaseCryptography : IBaseCrypt, INonAllocatingBaseEncoder
    {
        /// <inheritdoc/>
        public String Encode(String plain)
        {
            return Encode(Encoding.UTF8.GetBytes(plain));
        }
        
        /// <inheritdoc/>
        public abstract String Encode(ReadOnlySpan<Byte> bytes);
        
        /// <inheritdoc/>
        public Span<Byte> Decode(String encoded)
        {
            return Decode(encoded.ToCharArray());
        }

        /// <inheritdoc/>
        public Span<Byte> Decode(ReadOnlySpan<Byte> bytes)
        {
            return Decode(Encoding.UTF8.GetString(bytes).ToCharArray());
        }

        /// <inheritdoc/>
        public abstract Span<Byte> Decode(ReadOnlySpan<Char> text);

        /// <inheritdoc/>
        public abstract Boolean TryEncode(ReadOnlySpan<Byte> input, Span<Char> output, out Int32 numCharsWritten);

        /// <inheritdoc/>
        public abstract Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 numBytesWritten);

        /// <inheritdoc/>
        public abstract Int32 GetSafeByteCountForDecoding(ReadOnlySpan<Char> text);

        /// <inheritdoc/>
        public abstract Int32 GetSafeCharCountForEncoding(ReadOnlySpan<Byte> buffer);
    }
}