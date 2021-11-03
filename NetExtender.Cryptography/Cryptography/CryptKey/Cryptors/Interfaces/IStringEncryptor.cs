// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Crypto.CryptKey.Interfaces
{
    public interface IStringEncryptor : IEncryptor
    {
        public String? Encrypt(String value);

        public String? EncryptString(String value);
        
        public IEnumerable<String?> Encrypt(IEnumerable<String> source);

        public IEnumerable<String?> EncryptString(IEnumerable<String> source);
    }
}