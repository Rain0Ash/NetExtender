// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using NetExtender.BouncyCastle.Types.Numerics;
using NetExtender.Utilities.Cryptography;
using NetExtender.Utilities.Types;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace NetExtender.BouncyCastle.Utilities
{
    public static class BouncyCastleUtilities
    {
        public static CryptoApiRandomGenerator RandomGenerator
        {
            get
            {
                return new CryptoApiRandomGenerator();
            }
        }

        public static SecureRandom SecureRandom
        {
            get
            {
                return new SecureRandom(RandomGenerator);
            }
        }

        public static X509V3CertificateGenerator CertificateGenerator
        {
            get
            {
                return new X509V3CertificateGenerator();
            }
        }

        public static CertificateKeyStrength ToCertificateKeyStrength(Int32 value)
        {
            return value switch
            {
                1024 => CertificateKeyStrength.Rsa1024,
                2048 => CertificateKeyStrength.Rsa2048,
                4096 => CertificateKeyStrength.Rsa4096,
                8192 => CertificateKeyStrength.Rsa8192,
                16384 => CertificateKeyStrength.Rsa16384,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid key strength")
            };
        }

        public static RsaKeyPairGenerator CreateRsaKeyPairGenerator(SecureRandom random, CertificateKeyStrength strength)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (!EnumUtilities.ContainsValue(strength))
            {
                throw new ArgumentOutOfRangeException(nameof(strength), strength, "Invalid key strength");
            }

            return CreateRsaKeyPairGenerator(new KeyGenerationParameters(random, (Int32) strength));
        }

        public static RsaKeyPairGenerator CreateRsaKeyPairGenerator(KeyGenerationParameters parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            RsaKeyPairGenerator generator = new RsaKeyPairGenerator();
            generator.Init(parameters);
            return generator;
        }

        public static BigInteger ToBigInteger(this Org.BouncyCastle.Math.BigInteger value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new BouncyBigIntegerWrapper(value);
        }

        public static Org.BouncyCastle.Math.BigInteger ToBounceBigInteger(this BigInteger value)
        {
            return new BouncyBigIntegerWrapper(value);
        }
    }
}