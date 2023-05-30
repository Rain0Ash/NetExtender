// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Cryptography.Base.Interfaces
{
    /// <summary>
    /// Efficient encoding functionality using pre-allocated memory buffers by the callers.
    /// </summary>
    public interface INonAllocatingBaseEncoder
    {
        /// <summary>
        /// Gets a safe estimation about how many bytes decoding will take without performing
        /// the actual decoding operation. The estimation can be slightly larger than the actual
        /// output size.
        /// </summary>
        /// <param name="buffer">Text to be decoded.</param>
        /// <returns>Number of estimated bytes, or zero if the input length is invalid.</returns>
        public Int32 SafeByteCountForDecoding(ReadOnlySpan<Char> buffer);

        /// <summary>
        /// Gets a safe estimation about how many characters encoding a buffer will take without
        /// performing the actual encoding operation. The estimation can be slightly larger than the
        /// actual output size.
        /// </summary>
        /// <param name="buffer">Bytes to be encoded.</param>
        /// <returns>Number of estimated characters, or zero if the input length is invalid.</returns>
        public Int32 SafeCharCountForEncoding(ReadOnlySpan<Byte> buffer);
        
        /// <summary>
        /// Encode a buffer into a base-encoded representation using pre-allocated buffers.
        /// </summary>
        /// <param name="value">Bytes to encode.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="written">Actual number of characters written to the output.</param>
        /// <returns>Whether encoding was successful or not. If false, <paramref name="written"/>
        /// will be zero and the content of <paramref name="output"/> will be undefined.</returns>
        public Boolean TryEncode(ReadOnlySpan<Byte> value, Span<Char> output, out Int32 written);

        /// <summary>
        /// Decode an encoded character buffer into a pre-allocated output buffer.
        /// </summary>
        /// <param name="input">Encoded text.</param>
        /// <param name="output">Output buffer.</param>
        /// <param name="written">Actual number of bytes written to the output.</param>
        /// <returns>Whether decoding was successful. If false, the value of <paramref name="written"/>
        /// will be zero and the content of <paramref name="output"/> will be undefined.</returns>
        public Boolean TryDecode(ReadOnlySpan<Char> input, Span<Byte> output, out Int32 written);
    }
}
