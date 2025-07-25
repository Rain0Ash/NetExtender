// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Behavior;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Configuration.Cryptography.Interfaces;
using NetExtender.Cryptography.Keys.AES;
using NetExtender.Cryptography.Keys.Interfaces;

namespace NetExtender.Configuration.Cryptography.Utilities
{
    public static class ConfigurationCryptographyBehaviorUtilities
    {
        public static ICryptographyConfigBehavior Cryptography(this IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new CryptographyConfigBehavior(behavior, AesCryptographyKey.Default.Cryptor());
        }
        
        public static ICryptographyConfigBehavior Cryptography(this IConfigBehavior behavior, CryptographyConfigOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new CryptographyConfigBehavior(behavior, AesCryptographyKey.Default.Cryptor(options));
        }

        public static ICryptographyConfigBehavior Cryptography(this IConfigBehavior behavior, IStringCryptor cryptor)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            if (cryptor is null)
            {
                throw new ArgumentNullException(nameof(cryptor));
            }

            return new CryptographyConfigBehavior(behavior, cryptor.Cryptor());
        }

        public static ICryptographyConfigBehavior Cryptography(this IConfigBehavior behavior, IStringCryptor cryptor, CryptographyConfigOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            if (cryptor is null)
            {
                throw new ArgumentNullException(nameof(cryptor));
            }

            return new CryptographyConfigBehavior(behavior, cryptor.Cryptor(options));
        }

        public static ICryptographyConfig Create(this ICryptographyConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new CryptographyConfig(behavior);
        }
    }
}