// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="Base58Alphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;

namespace NetExtender.Crypto.Base.Alphabet
{
    /// <summary>
    /// Base58 alphabet.
    /// </summary>
    public sealed class Base58Alphabet : EncodingAlphabet
    {
        public static Base58Alphabet Bitcoin { get; } = new Base58Alphabet("123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz");
        public static Base58Alphabet Ripple { get; } = new Base58Alphabet("rpshnaf39wBUDNEGHJKLM4PQRST7VWXYZ2bcdeCg65jkm8oFqi1tuvAxyz");
        public static Base58Alphabet Flickr { get; } = new Base58Alphabet("123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ");

        /// <summary>
        /// Initializes a new instance of the <see cref="Base58Alphabet"/> class
        /// using a custom alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        public Base58Alphabet(String alphabet)
            : base(58, alphabet)
        {
        }
    }
}