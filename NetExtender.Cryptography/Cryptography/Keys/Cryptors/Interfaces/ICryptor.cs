// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Cryptography.Keys.Interfaces
{
    public interface ICryptor : IEncryptor, IDecryptor
    {
        public CryptAction Crypt { get; }
        public new Int32 KeySize { get; }
    }
}