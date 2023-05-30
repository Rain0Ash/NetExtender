// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    public abstract class EncodingAlphabet : IEncodingAlphabet, IEquatable<EncodingAlphabet>
    {
        /// <summary>
        /// Specifies the highest possible char value in an encoding alphabet
        /// Any char above with would raise an exception.
        /// </summary>
        private const Int32 MaxLength = 127;

        /// <summary>
        /// Gets the length of the alphabet.
        /// </summary>
        public Int32 Length { get; }

        private Byte[] Internal { get; } = new Byte[MaxLength];

        /// <summary>
        /// Holds a mapping from character to an actual byte value
        /// The values are held as "value + 1" so a zero would denote "not set"
        /// and would cause an exception.
        /// </summary>
        internal ReadOnlySpan<Byte> ReverseLookupTable
        {
            get
            {
                return Internal;
            }
        }

        /// <summary>
        /// Gets the characters of the alphabet.
        /// </summary>
        public String Value { get; }

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
                throw new ArgumentOutOfRangeException(nameof(alphabet), alphabet.Length, $"Required alphabet length is {length}.");
            }

            Length = length;
            Value = alphabet;

            for (Int16 i = 0; i < length; i++)
            {
                Map(alphabet[i], i);
            }
        }

        /// <summary>
        /// Map a character to a value.
        /// </summary>
        /// <param name="character">Characters.</param>
        /// <param name="value">Corresponding value.</param>
        protected void Map(Char character, Int32 value)
        {
            Internal[character] = (Byte) (value + 1);
        }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return Value.GetHashCode(StringComparison.Ordinal);
        }

        /// <inheritdoc/>
        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                EncodingAlphabet alphabet => Equals(alphabet),
                IEncodingAlphabet alphabet => Equals(alphabet),
                _ => false
            };
        }

        public Boolean Equals(EncodingAlphabet? other)
        {
            return other is not null && String.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        public Boolean Equals(IEncodingAlphabet? other)
        {
            return other is not null && String.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Get the string representation of the alphabet.
        /// </summary>
        /// <returns>The characters of the encoding alphabet.</returns>
        public override String ToString()
        {
            return Value;
        }
    }
}