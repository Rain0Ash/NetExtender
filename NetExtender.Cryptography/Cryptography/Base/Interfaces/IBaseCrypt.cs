// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="IBaseEncoder.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

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
        /// <param name="plain">String to encode.</param>
        /// <returns>Base16 string.</returns>
        public String Encode(String plain);

        /// <summary>
        /// Encode a buffer to base-encoded representation.
        /// </summary>
        /// <param name="bytes">Bytes to encode.</param>
        /// <returns>Base16 string.</returns>
        public String Encode(ReadOnlySpan<Byte> bytes);

        /// <summary>
        /// Decode base-encoded text into bytes.
        /// </summary>
        /// <param name="encoded">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        public Span<Byte> Decode(String encoded);

        /// <summary>
        /// Decode base-encoded text into bytes.
        /// </summary>
        /// <param name="bytes">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        public Span<Byte> Decode(ReadOnlySpan<Byte> bytes);

        /// <summary>
        /// Decode base-encoded text into bytes.
        /// </summary>
        /// <param name="text">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        public Span<Byte> Decode(ReadOnlySpan<Char> text);
    }
}
