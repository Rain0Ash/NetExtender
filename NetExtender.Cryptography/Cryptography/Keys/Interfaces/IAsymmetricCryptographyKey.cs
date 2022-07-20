// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Interfaces;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Cryptography.Keys.Interfaces
{
    public interface IAsymmetricCryptographyKey : ICryptographyKey, ICloneable<IAsymmetricCryptographyKey>
    {
        public ReadOnlySpan<Byte> PrivateKey { get; }
        public ReadOnlySpan<Byte> PublicKey { get; }

        public new IAsymmetricCryptographyKey Clone();
        public new IAsymmetricCryptographyKey Clone(CryptAction crypt);
    }
}