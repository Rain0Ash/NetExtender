// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Cryptography.Base.Interfaces
{
    /// <summary>
    /// Defines basic encoding alphabet.
    /// </summary>
    public interface IEncodingAlphabet : IEquatable<IEncodingAlphabet>
    {
        /// <summary>
        /// Gets the length of the alphabet.
        /// </summary>
        public Int32 Length { get; }

        /// <summary>
        /// Gets the characters in the alphabet.
        /// </summary>
        public String Value { get; }
    }
}