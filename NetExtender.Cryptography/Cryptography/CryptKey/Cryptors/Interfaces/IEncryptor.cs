// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Crypto.CryptKey.Interfaces
{
    public interface IEncryptor
    {
        public Int32 KeySize { get; }
        public Boolean IsEncrypt { get; }
        public Boolean IsDeterministic { get; }
    }
}