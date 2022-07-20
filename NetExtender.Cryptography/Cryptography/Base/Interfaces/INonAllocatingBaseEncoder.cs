// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="INonAllocatingBaseEncoder.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;

namespace NetExtender.Cryptography.Base.Interfaces
{
    /// <summary>
    /// Efficient encoding functionality using pre-allocated memory buffers by the callers.
    /// </summary>
    public interface INonAllocatingBaseEncoder
    {
        /// <summary>
        /// Encode a buffer into a base-encoded representation using pre-allocated buffers.
        /// </summary>
        /// <param name="input">Bytes to encode.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="numCharsWritten">Actual number of characters written to the output.</param>
        /// <returns>Whether encoding was successful or not. If false, <paramref name="numCharsWritten"/>
        /// will be zero and the content of <paramref name="output"/> will be undefined.</returns>
        public Boolean TryEncode(ReadOnlySpan<Byte> input, Span<Char> output, out Int32 numCharsWritten);

        /// <summary>
        /// Decode an encoded character buffer into a pre-allocated output buffer.
        /// </summary>
        /// <param name="input">Encoded text.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="numBytesWritten">Actual number of bytes written to the output.</param>
        /// <returns>Whether decoding was successful. If false, the value of <paramref name="numBytesWritten"/>
        /// will be zero and the content of <paramref name="output"/> will be undefined.</returns>
        public Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 numBytesWritten);

        /// <summary>
        /// Gets a safe estimation about how many bytes decoding will take without performing
        /// the actual decoding operation. The estimation can be slightly larger than the actual
        /// output size.
        /// </summary>
        /// <param name="text">Text to be decoded.</param>
        /// <returns>Number of estimated bytes, or zero if the input length is invalid.</returns>
        public Int32 GetSafeByteCountForDecoding(ReadOnlySpan<Char> text);

        /// <summary>
        /// Gets a safe estimation about how many characters encoding a buffer will take without
        /// performing the actual encoding operation. The estimation can be slightly larger than the
        /// actual output size.
        /// </summary>
        /// <param name="buffer">Bytes to be encoded.</param>
        /// <returns>Number of estimated characters, or zero if the input length is invalid.</returns>
        public Int32 GetSafeCharCountForEncoding(ReadOnlySpan<Byte> buffer);
    }
}
