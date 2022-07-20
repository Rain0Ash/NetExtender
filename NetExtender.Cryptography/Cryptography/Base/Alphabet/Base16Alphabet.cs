// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="Base16Alphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;

namespace NetExtender.Cryptography.Base.Alphabet
{
    /// <summary>
    /// Alphabet representation for Base16 encodings.
    /// </summary>
    public class Base16Alphabet : EncodingAlphabet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Base16Alphabet"/> class.
        /// </summary>
        /// <param name="alphabet">Encoding alphabet.</param>
        /// <param name="caseSensitive">If the decoding should be performed case sensitive.</param>
        public Base16Alphabet(String alphabet, Boolean caseSensitive = false)
            : base(16, alphabet)
        {
            if (!caseSensitive)
            {
                MapCounterparts();
            }
        }

        /// <summary>
        /// Gets upper case Base16 alphabet.
        /// </summary>
        public static Base16Alphabet UpperCase { get; } = new Base16Alphabet("0123456789ABCDEF");

        /// <summary>
        /// Gets lower case Base16 alphabet.
        /// </summary>
        public static Base16Alphabet LowerCase { get; } =  new Base16Alphabet("0123456789abcdef");

        /// <summary>
        /// Gets ModHex Base16 alphabet, used by Yubico apps.
        /// </summary>
        public static Base16Alphabet ModHex { get; } = new Base16Alphabet("cbdefghijklnrtuv");

        /// <summary>
        /// Gets a value indicating whether the decoding should be performed in a case sensitive fashion.
        /// The default is false.
        /// </summary>
        public Boolean CaseSensitive { get; } = false;

        private void MapCounterparts()
        {
            Int32 alphaLen = Value.Length;
            for (Int32 i = 0; i < alphaLen; i++)
            {
                Char c = Value[i];
                
                if (!Char.IsLetter(c))
                {
                    continue;
                }

                if (Char.IsUpper(c))
                {
                    Map(Char.ToLowerInvariant(c), i);
                }

                if (Char.IsLower(c))
                {
                    Map(Char.ToUpperInvariant(c), i);
                }
            }
        }
    }
}