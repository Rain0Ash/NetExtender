// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Cryptography.Keys.Interfaces;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Configuration.Cryptography.Common
{
    public sealed class ConfigurationCryptor : IConfigurationCryptor
    {
        public IStringCryptor Cryptor { get; }

        public IStringEncryptor Encryptor { get; }

        public IStringDecryptor Decryptor { get; }

        public CryptographyConfigOptions CryptographyOptions { get; }

        public CryptAction Crypt
        {
            get
            {
                return Cryptor.Crypt;
            }
        }

        public Int32 KeySize
        {
            get
            {
                return Cryptor.KeySize;
            }
        }

        public Boolean IsDecrypt
        {
            get
            {
                return Cryptor.IsDecrypt;
            }
        }

        public Boolean IsEncrypt
        {
            get
            {
                return Cryptor.IsEncrypt;
            }
        }

        public Boolean IsDeterministic
        {
            get
            {
                return Encryptor.IsDeterministic;
            }
        }

        public Boolean IsCryptDefault
        {
            get
            {
                return CryptographyOptions.HasFlag(CryptographyConfigOptions.CryptDefault);
            }
        }

        public Boolean IsCryptKey
        {
            get
            {
                return CryptographyOptions.HasFlag(CryptographyConfigOptions.CryptKey);
            }
        }

        public Boolean IsCryptValue
        {
            get
            {
                return CryptographyOptions.HasFlag(CryptographyConfigOptions.CryptValue);
            }
        }

        public Boolean IsCryptSections
        {
            get
            {
                return CryptographyOptions.HasFlag(CryptographyConfigOptions.CryptSections);
            }
        }

        public Boolean IsCryptConfig
        {
            get
            {
                return CryptographyOptions.HasFlag(CryptographyConfigOptions.CryptConfig);
            }
        }

        public Boolean IsCryptAll
        {
            get
            {
                return CryptographyOptions.HasFlag(CryptographyConfigOptions.CryptAll);
            }
        }

        public ConfigurationCryptor(IStringCryptor cryptor)
            : this(cryptor, CryptographyConfigOptions.All)
        {
        }

        public ConfigurationCryptor(IStringCryptor cryptor, CryptographyConfigOptions options)
        {
            Cryptor = cryptor ?? throw new ArgumentNullException(nameof(cryptor));
            CryptographyOptions = options;
            Encryptor = Cryptor.IsDeterministic || !IsCryptKey && !IsCryptSections ? Cryptor : Cryptor.CreateDeterministic();
            Decryptor = Cryptor;
        }

        public String? Encrypt(String value)
        {
            return Cryptor.Encrypt(value);
        }

        public String? EncryptString(String value)
        {
            return Cryptor.EncryptString(value);
        }

        public IEnumerable<String?> Encrypt(IEnumerable<String> source)
        {
            return Cryptor.Encrypt(source);
        }

        public IEnumerable<String?> EncryptString(IEnumerable<String> source)
        {
            return Cryptor.EncryptString(source);
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
}