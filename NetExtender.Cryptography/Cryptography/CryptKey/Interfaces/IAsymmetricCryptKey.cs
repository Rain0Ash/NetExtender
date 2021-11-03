// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Interfaces;

namespace NetExtender.Crypto.CryptKey.Interfaces
{
    public interface IAsymmetricCryptKey : ICryptKey, ICloneable<IAsymmetricCryptKey>
    {
        public ReadOnlySpan<Byte> PrivateKey { get; }
        public ReadOnlySpan<Byte> PublicKey { get; }

        public new IAsymmetricCryptKey Clone();
        public new IAsymmetricCryptKey Clone(CryptAction crypt);
    }
}