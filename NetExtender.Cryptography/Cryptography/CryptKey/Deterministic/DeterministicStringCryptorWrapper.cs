// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Crypto.CryptKey.Deterministic
{
    public class DeterministicStringCryptorWrapper : DeterministicStringEncryptorWrapper, IStringCryptor
    {
        private IStringCryptor Cryptor { get; }
        
        public CryptAction Crypt
        {
            get
            {
                return Cryptor.Crypt;
            }
        }

        public Boolean IsDecrypt
        {
            get
            {
                return Cryptor.IsDecrypt;
            }
        }

        public DeterministicStringCryptorWrapper(IStringCryptor cryptor)
            : base(cryptor)
        {
            Cryptor = cryptor ?? throw new ArgumentNullException(nameof(cryptor));
        }

        public DeterministicStringCryptorWrapper(IStringCryptor cryptor, IEnumerable<KeyValuePair<String, String?>>? source)
            : base(cryptor, source)
        {
            Cryptor = cryptor ?? throw new ArgumentNullException(nameof(cryptor));
        }
        
        public String? Decrypt(String value)
        {
            return Cryptor.Decrypt(value);
        }

        public String? DecryptString(String value)
        {
            return Cryptor.DecryptString(value);
        }

        public IEnumerable<String?> Decrypt(IEnumerable<String> source)
        {
            return Cryptor.Decrypt(source);
        }

        public IEnumerable<String?> DecryptString(IEnumerable<String> source)
        {
            return Cryptor.DecryptString(source);
        }
    }
    
    public class DeterministicStringCryptorWrapper<T> : DeterministicStringEncryptorWrapper where T : IStringCryptor
    {
        public T Cryptor { get; }
        
        public DeterministicStringCryptorWrapper(T cryptor)
            : base(cryptor)
        {
            Cryptor = cryptor ?? throw new ArgumentNullException(nameof(cryptor));
        }
        
        public DeterministicStringCryptorWrapper(T cryptor, IEnumerable<KeyValuePair<String, String?>>? source)
            : base(cryptor, source)
        {
            Cryptor = cryptor ?? throw new ArgumentNullException(nameof(cryptor));
        }
    }
}