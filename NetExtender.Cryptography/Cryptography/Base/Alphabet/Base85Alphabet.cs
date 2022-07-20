// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="Base85Alphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;

namespace NetExtender.Cryptography.Base.Alphabet
{
    /// <summary>
    /// Base85 Alphabet.
    /// </summary>
    public sealed class Base85Alphabet : EncodingAlphabet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Base85Alphabet"/> class
        /// using custom settings.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        /// <param name="allZeroShortcut">Character to substitute for all zero.</param>
        /// <param name="allSpaceShortcut">Character to substitute for all space.</param>
        public Base85Alphabet(
            String alphabet,
            Char? allZeroShortcut = null,
            Char? allSpaceShortcut = null)
            : base(85, alphabet)
        {
            AllZeroShortcut = allZeroShortcut;
            AllSpaceShortcut = allSpaceShortcut;
        }

        /// <summary>
        /// Gets ZeroMQ Z85 Alphabet.
        /// </summary>
        public static Base85Alphabet Z85 { get; } = new Base85Alphabet("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#");

        /// <summary>
        /// Gets Adobe Ascii85 Alphabet (each character is directly produced by raw value + 33),
        /// also known as "btoa" encoding.
        /// </summary>
        public static Base85Alphabet Ascii85 { get; } = new Base85Alphabet(
            "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstu",
            'z',
            'y');

        /// <summary>
        /// Gets the character to be used for "all zeros".
        /// </summary>
        public Char? AllZeroShortcut { get; }

        /// <summary>
        /// Gets the character to be used for "all spaces".
        /// </summary>
        public Char? AllSpaceShortcut { get; }

        /// <summary>
        /// Gets a value indicating whether the alphabet uses one of shortcut characters for all spaces
        /// or all zeros.
        /// </summary>
        public Boolean HasShortcut
        {
            get
            {
                return AllSpaceShortcut.HasValue || AllZeroShortcut.HasValue;
            }
        }
    }
}
