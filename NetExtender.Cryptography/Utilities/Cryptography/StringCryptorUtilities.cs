// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using NetExtender.Crypto.CryptKey.Deterministic;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Types;

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
        
        public static Boolean TryEncrypt(this IStringEncryptor encryptor, String? value, out String? result)
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            if (value is null)
            {
                result = value;
                return true;
            }

            try
            {
                if (!encryptor.IsEncrypt)
                {
                    result = default;
                    return false;
                }

                result = encryptor.Encrypt(value);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static Boolean TryDecrypt(this IStringDecryptor decryptor, String? value, out String? result)
        {
            if (decryptor is null)
            {
                throw new ArgumentNullException(nameof(decryptor));
            }

            if (value is null)
            {
                result = value;
                return true;
            }
            
            try
            {
                if (!decryptor.IsDecrypt)
                {
                    result = default;
                    return false;
                }
                        
                result = decryptor.Decrypt(value);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static Boolean TryEncrypt(this IStringEncryptor encryptor, IEnumerable<String>? source, out IEnumerable<String>? result)
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            if (source is null)
            {
                result = source;
                return true;
            }

            try
            {
                if (!encryptor.IsEncrypt)
                {
                    result = default;
                    return false;
                }
                        
                result = encryptor.Encrypt(source).ThrowIfNull<String, CryptographicException>().ToArray();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryDecrypt(this IStringDecryptor decryptor, IEnumerable<String>? source, out IEnumerable<String>? result)
        {
            if (decryptor is null)
            {
                throw new ArgumentNullException(nameof(decryptor));
            }

            if (source is null)
            {
                result = source;
                return true;
            }

            try
            {
                if (!decryptor.IsDecrypt)
                {
                    result = default;
                    return false;
                }
                        
                result = decryptor.Decrypt(source).ThrowIfNull<String, CryptographicException>().ToArray();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
    }
}