// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Cryptography.Base.Interfaces
{
    /// <summary>
    /// Basic encoding functionality.
    /// </summary>
    public interface IBaseCrypt
    {
        /// <summary>
        /// Encode a buffer to base-encoded representation.
        /// </summary>
        /// <param name="value">String to encode.</param>
        /// <returns>Base16 string.</returns>
        public String Encode(String value);

        /// <summary>
        /// Encode a buffer to base-encoded representation.
        /// </summary>
        /// <param name="value">Bytes to encode.</param>
        /// <returns>Base16 string.</returns>
        public String Encode(ReadOnlySpan<Byte> value);

        /// <summary>
        /// Decode base-encoded text into bytes.
        /// </summary>
        /// <param name="value">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        public Span<Byte> Decode(String value);

        /// <summary>
        /// Decode base-encoded text into bytes.
        /// </summary>
        /// <param name="value">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        public Span<Byte> Decode(ReadOnlySpan<Byte> value);

        /// <summary>
        /// Decode base-encoded text into bytes.
        /// </summary>
        /// <param name="value">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        public Span<Byte> Decode(ReadOnlySpan<Char> value);
    }
}
