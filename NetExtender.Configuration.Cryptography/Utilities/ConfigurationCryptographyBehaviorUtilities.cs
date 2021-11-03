// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Behavior;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Crypto.CryptKey.AES;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Cryptography.Utilities
{
    public static class ConfigurationCryptographyBehaviorUtilities
    {
        public static ICryptographyConfigBehavior Cryptography(this IConfigBehavior behavior)
        {
            return new CryptographyBehavior(behavior, AESCryptKey.Default);
        }
        
        public static ICryptographyConfigBehavior Cryptography(this IConfigBehavior behavior, CryptographyConfigOptions options)
        {
            return new CryptographyBehavior(behavior, AESCryptKey.Default, options);
        }
        
        public static ICryptographyConfigBehavior Cryptography(this IConfigBehavior behavior, IStringCryptor cryptor)
        {
            return new CryptographyBehavior(behavior, cryptor);
        }
        
        public static ICryptographyConfigBehavior Cryptography(this IConfigBehavior behavior, IStringCryptor cryptor, CryptographyConfigOptions options)
        {
            return new CryptographyBehavior(behavior, cryptor, options);
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