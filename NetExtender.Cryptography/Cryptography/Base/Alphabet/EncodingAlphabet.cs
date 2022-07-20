// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="EncodingAlphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;
using NetExtender.Cryptography.Base.Interfaces;

namespace NetExtender.Cryptography.Base.Alphabet
{
    /// <summary>
    /// A single encoding algorithm can support many different alphabets.
    /// EncodingAlphabet consists of a basis for implementing different
    /// alphabets for different encodings. It's suitable if you want to
    /// implement your own encoding based on the existing base classes.
    /// </summary>
    public abstract class EncodingAlphabet : IEncodingAlphabet
    {
        /// <summary>
        /// Specifies the highest possible char value in an encoding alphabet
        /// Any char above with would raise an exception.
        /// </summary>
        private const Int32 MaxLength = 127;

        /// <summary>
        /// Holds a mapping from character to an actual byte value
        /// The values are held as "value + 1" so a zero would denote "not set"
        /// and would cause an exception.
        /// </summary>
        /// <remarks>byte[] has no discernible perf impact and saves memory.</remarks>
        private readonly Byte[] _reverseLookupTable = new Byte[MaxLength];

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodingAlphabet"/> class.
        /// </summary>
        /// <param name="length">Length of the alphabe.</param>
        /// <param name="alphabet">Alphabet character.</param>
        protected EncodingAlphabet(Int32 length, String alphabet)
        {
            if (alphabet is null)
            {
                throw new ArgumentNullException(nameof(alphabet));
            }

            if (alphabet.Length != length)
            {
                throw new ArgumentException($"Required alphabet length is {length} but provided alphabet is "
                    + $"{alphabet.Length} characters long");
            }

            Length = length;
            Value = alphabet;

            for (Int16 i = 0; i < length; i++)
            {
                Map(alphabet[i], i);
            }
        }

        /// <summary>
        /// Gets the length of the alphabet.
        /// </summary>
        public Int32 Length { get; }

        /// <summary>
        /// Gets the characters of the alphabet.
        /// </summary>
        public String Value { get; }

        internal ReadOnlySpan<Byte> ReverseLookupTable
        {
            get
            {
                return _reverseLookupTable;
            }
        }

        /// <summary>
        /// Generates a standard invalid character exception for alphabets.
        /// </summary>
        /// <remarks>
        /// The reason this is not a throwing method itself is
        /// that the compiler has no way of knowing whether the execution
        /// will end after the method call and can incorrectly assume
        /// reachable code.
        /// </remarks>
        /// <param name="c">Characters.</param>
        /// <returns>Exception to be thrown.</returns>
        public static Exception InvalidCharacter(Char c)
        {
            return new ArgumentException($"Invalid character: {c}");
        }

        /// <summary>
        /// Get the string representation of the alphabet.
        /// </summary>
        /// <returns>The characters of the encoding alphabet.</returns>
        public override String ToString()
        {
            return Value;
        }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return Value.GetHashCode(StringComparison.Ordinal);
        }

        /// <summary>
        /// Map a character to a value.
        /// </summary>
        /// <param name="c">Characters.</param>
        /// <param name="value">Corresponding value.</param>
        protected void Map(Char c, Int32 value)
        {
            _reverseLookupTable[c] = (Byte)(value + 1);
        }
    }
}