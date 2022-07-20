// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// <copyright file="CrockfordBase32Alphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;

namespace NetExtender.Cryptography.Base.Alphabet
{
    internal sealed class CrockfordBase32Alphabet : Base32Alphabet
    {
        public CrockfordBase32Alphabet()
            : base("0123456789ABCDEFGHJKMNPQRSTVWXYZ")
        {
            MapAlternate('O', '0');
            MapAlternate('I', '1');
            MapAlternate('L', '1');
        }

        private void MapAlternate(Char source, Char destination)
        {
            Int32 result = ReverseLookupTable[destination] - 1;
            Map(source, result);
            Map(Char.ToLowerInvariant(source), result);
        }
    }
}