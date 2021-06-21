// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Crypto.CryptKey.Interfaces
{
    public interface IAsymmetricCryptKey : ICryptKey
    {
        public ReadOnlySpan<Byte> PrivateKey { get; }
        public ReadOnlySpan<Byte> PublicKey { get; }
    }
}