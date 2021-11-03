// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Crypto.CryptKey.Deterministic;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Utilities.Crypto
{
    public static class StringCryptorUtilities
    {
        public static DeterministicStringEncryptorWrapper CreateDeterministic(this IStringEncryptor encryptor)
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            return new DeterministicStringEncryptorWrapper(encryptor);
        }
        
        public static DeterministicStringEncryptorWrapper CreateDeterministic(this IStringEncryptor encryptor, IEnumerable<KeyValuePair<String, String?>> source)
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            return new DeterministicStringEncryptorWrapper(encryptor, source);
        }
        
        public static DeterministicStringCryptorWrapper CreateDeterministic(this IStringCryptor encryptor)
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            return new DeterministicStringCryptorWrapper(encryptor);
        }
        
        public static DeterministicStringCryptorWrapper CreateDeterministic(this IStringCryptor encryptor, IEnumerable<KeyValuePair<String, String?>> source)
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            return new DeterministicStringCryptorWrapper(encryptor, source);
        }
        
        public static DeterministicStringEncryptorWrapper<T> CreateDeterministic<T>(this T encryptor) where T : IStringEncryptor
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            return new DeterministicStringEncryptorWrapper<T>(encryptor);
        }
        
        public static DeterministicStringEncryptorWrapper<T> CreateDeterministic<T>(this T encryptor, IEnumerable<KeyValuePair<String, String?>> source) where T : IStringEncryptor
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            return new DeterministicStringEncryptorWrapper<T>(encryptor, source);
        }

        public static DeterministicStringEncryptorWrapper RegisterDeterministic<T>(this T encryptor) where T : IStringEncryptor
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            return DeterministicStringEncryptorWrapper.Register(encryptor);
        }
    }
}