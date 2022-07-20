// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Interfaces;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Cryptography.Keys.Interfaces
{
    public interface ICryptographyKey : ICloneable<ICryptographyKey>, IStringCryptor, IByteCryptor, IDisposable
    {
        public ICryptographyKey Clone(CryptAction crypt);
    }
}