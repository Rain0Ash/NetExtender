// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="IBaseStreamEncoder.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System.IO;
using System.Threading.Tasks;

namespace NetExtender.Crypto.Base.Interfaces
{
    /// <summary>
    /// Stream-based encoding functionality.
    /// </summary>
    public interface IBaseStreamEncoder
    {
        /// <summary>
        /// Encodes stream of bytes into base-encoded text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        public void Encode(Stream input, TextWriter output);

        /// <summary>
        /// Encodes stream of bytes into base-encoded text.
        /// </summary>
        /// <param name="input">Stream that provides bytes to be encoded.</param>
        /// <param name="output">Stream that the encoded text is written to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task EncodeAsync(Stream input, TextWriter output);

        /// <summary>
        /// Decode base-encoded text through streams. Stream based variant tries to consume
        /// as little memory as possible, and relies of .NET's own underlying buffering mechanisms,
        /// contrary to their buffer-based versions.
        /// </summary>
        /// <param name="input">Stream that the encoded bytes would be read from.</param>
        /// <param name="output">Stream where decoded bytes will be written to.</param>
        public void Decode(TextReader input, Stream output);

        /// <summary>
        /// Decode base-encoded text through streams. Stream based variant tries to consume
        /// as little memory as possible, and relies of .NET's own underlying buffering mechanisms,
        /// contrary to their buffer-based versions.
        /// </summary>
        /// <param name="input">Stream that the encoded bytes would be read from.</param>
        /// <param name="output">Stream where decoded bytes will be written to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task DecodeAsync(TextReader input, Stream output);
    }
}
