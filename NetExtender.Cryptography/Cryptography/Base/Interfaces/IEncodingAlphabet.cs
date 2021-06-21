// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="IEncodingAlphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

 using System;

 namespace NetExtender.Crypto.Base.Interfaces
{
    /// <summary>
    /// Defines basic encoding alphabet.
    /// </summary>
    public interface IEncodingAlphabet
    {
        /// <summary>
        /// Gets the characters in the alphabet.
        /// </summary>
        public String Value { get; }

        /// <summary>
        /// Gets the length of the alphabet.
        /// </summary>
        public Int32 Length { get; }
    }
}