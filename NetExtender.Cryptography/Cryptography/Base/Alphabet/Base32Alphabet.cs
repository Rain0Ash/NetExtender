// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;

namespace NetExtender.Cryptography.Base.Alphabet
{
    /// <summary>
    /// Base32 alphabet flavors.
    /// </summary>
    public class Base32Alphabet : EncodingAlphabet
    {
        public static CrockfordBase32Alphabet Crockford { get; } = new CrockfordBase32Alphabet();
        public static Base32Alphabet Rfc4648 { get; } = new Base32Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ234567");
        public static Base32Alphabet ExtHex { get; } = new Base32Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUV");
        public static Base32Alphabet ZBase32 { get; } = new Base32Alphabet("ybndrfg8ejkmcpqxot1uwisza345h769");
        public static Base32Alphabet Geohash { get; } = new Base32Alphabet("0123456789bcdefghjkmnpqrstuvwxyz");

        /// <summary>
        /// Gets the padding character used in encoding.
        /// </summary>
        public Char Padding { get; } = '=';

        /// <summary>
        /// Initializes a new instance of the <see cref="Base32Alphabet"/> class.
        /// </summary>
        /// <param name="alphabet">Characters.</param>
        public Base32Alphabet(String alphabet)
            : base(32, alphabet)
        {
            MapLowerCaseCounterparts(alphabet);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base32Alphabet"/> class.
        /// </summary>
        /// <param name="alphabet">Encoding alphabet to use.</param>
        /// <param name="padding">Padding character.</param>
        public Base32Alphabet(String alphabet, Char padding)
            : this(alphabet)
        {
            Padding = padding;
        }

        private void MapLowerCaseCounterparts(String alphabet)
        {
            if (alphabet is null)
            {
                throw new ArgumentNullException(nameof(alphabet));
            }

            foreach (Char character in alphabet.Where(Char.IsUpper))
            {
                Map(Char.ToLowerInvariant(character), ReverseLookupTable[character] - 1);
            }
        }
    }
}