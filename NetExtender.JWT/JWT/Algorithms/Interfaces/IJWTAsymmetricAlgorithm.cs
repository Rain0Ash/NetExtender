// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.JWT.Algorithms.Interfaces
{
    public interface IJWTAsymmetricAlgorithm : IJWTAlgorithm
    {
        public Boolean Verify(ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature);
        public Boolean Verify(Byte[] source, Byte[] signature);
    }
}