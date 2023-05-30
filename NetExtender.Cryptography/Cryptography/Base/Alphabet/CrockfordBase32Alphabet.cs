// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Cryptography.Base.Alphabet
{
    public sealed class CrockfordBase32Alphabet : Base32Alphabet
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