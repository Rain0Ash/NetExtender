// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Cryptography.Keys.Interfaces
{
    public interface IStringDecryptor : IDecryptor
    {
        public String? Decrypt(String value);
        
        public String? DecryptString(String value);
        
        public IEnumerable<String?> Decrypt(IEnumerable<String> source);

        public IEnumerable<String?> DecryptString(IEnumerable<String> source);
    }
}