// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Interfaces;

namespace NetExtender.Crypto.CryptKey.Interfaces
{
    public interface ICryptKey : ICloneable<ICryptKey>, IStringCryptor, IByteCryptor, IDisposable
    {
        public ICryptKey Clone(CryptAction crypt);
    }
}