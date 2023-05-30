// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Cryptography.Base.Alphabet
{
    /// <summary>
    /// Alphabet representation for Base16 encodings.
    /// </summary>
    public class Base16Alphabet : EncodingAlphabet
    {
        /// <summary>
        /// Gets lower case Base16 alphabet.
        /// </summary>
        public static Base16Alphabet LowerCase { get; } =  new Base16Alphabet("0123456789abcdef");

        /// <summary>
        /// Gets upper case Base16 alphabet.
        /// </summary>
        public static Base16Alphabet UpperCase { get; } = new Base16Alphabet("0123456789ABCDEF");

        /// <summary>
        /// Gets ModHex Base16 alphabet, used by Yubico apps.
        /// </summary>
        public static Base16Alphabet ModHex { get; } = new Base16Alphabet("cbdefghijklnrtuv");

        /// <summary>
        /// Gets a value indicating whether the decoding should be performed in a case sensitive fashion.
        /// The default is false.
        /// </summary>
        public Boolean Insensitive { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base16Alphabet"/> class.
        /// </summary>
        /// <param name="alphabet">Encoding alphabet.</param>
        public Base16Alphabet(String alphabet)
            : this(alphabet, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base16Alphabet"/> class.
        /// </summary>
        /// <param name="alphabet">Encoding alphabet.</param>
        /// <param name="insensitive">If the decoding should be performed case sensitive.</param>
        public Base16Alphabet(String alphabet, Boolean insensitive)
            : base(16, alphabet)
        {
            Insensitive = insensitive;
            
            if (Insensitive)
            {
                MapCounterparts();
            }
        }

        private void MapCounterparts()
        {
            for (Int32 i = 0; i < Value.Length; i++)
            {
                Char character = Value[i];

                if (!Char.IsLetter(character))
                {
                    continue;
                }

                if (Char.IsUpper(character))
                {
                    Map(Char.ToLowerInvariant(character), i);
                }

                if (Char.IsLower(character))
                {
                    Map(Char.ToUpperInvariant(character), i);
                }
            }
        }
    }
}