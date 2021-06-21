// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="Base32Alphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;
using System.Linq;

 namespace NetExtender.Crypto.Base.Alphabet
{
    /// <summary>
    /// Base32 alphabet flavors.
    /// </summary>
    public class Base32Alphabet : EncodingAlphabet
    {
        internal static readonly CrockfordBase32Alphabet Crockford = new CrockfordBase32Alphabet();

        public static readonly Base32Alphabet RFC4648 = new Base32Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ234567");

        public static readonly Base32Alphabet ExtendedHex = new Base32Alphabet("0123456789ABCDEFGHIJKLMNOPQRSTUV");

        public static readonly Base32Alphabet ZBase32 = new Base32Alphabet("ybndrfg8ejkmcpqxot1uwisza345h769");

        public static readonly Base32Alphabet Geohash = new Base32Alphabet("0123456789bcdefghjkmnpqrstuvwxyz");

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
        /// <param name="paddingChar">Padding character.</param>
        public Base32Alphabet(String alphabet, Char paddingChar)
            : this(alphabet)
        {
            PaddingChar = paddingChar;
        }

        /// <summary>
        /// Gets the padding character used in encoding.
        /// </summary>
        public Char PaddingChar { get; } = '=';

        private void MapLowerCaseCounterparts(String alphabet)
        {
            foreach (Char c in alphabet.Where(Char.IsUpper))
            {
                Map(Char.ToLowerInvariant(c), ReverseLookupTable[c] - 1);
            }
        }
    }
}