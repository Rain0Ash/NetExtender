// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Cryptography.Keys.Interfaces;

namespace NetExtender.Configuration.Cryptography.Utilities
{
    public static class ConfigurationCryptorUtilities
    {
        public static IConfigurationCryptor Cryptor(this IStringCryptor cryptor)
        {
            if (cryptor is null)
            {
                throw new ArgumentNullException(nameof(cryptor));
            }

            return new ConfigurationCryptor(cryptor);
        }

        public static IConfigurationCryptor Cryptor(this IStringCryptor cryptor, CryptographyConfigOptions options)
        {
            if (cryptor is null)
            {
                throw new ArgumentNullException(nameof(cryptor));
            }

            return new ConfigurationCryptor(cryptor, options);
        }

        public static IConfigurationCryptor AsCryptor(this IStringCryptor cryptor)
        {
            if (cryptor is null)
            {
                throw new ArgumentNullException(nameof(cryptor));
            }

            return cryptor as IConfigurationCryptor ?? cryptor.Cryptor();
        }

        public static IConfigurationCryptor AsCryptor(this IStringCryptor cryptor, CryptographyConfigOptions options)
        {
            if (cryptor is null)
            {
                throw new ArgumentNullException(nameof(cryptor));
            }

            return cryptor as IConfigurationCryptor ?? cryptor.Cryptor(options);
        }
    }
}