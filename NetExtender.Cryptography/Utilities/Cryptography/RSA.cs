// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using System.Text;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;
using Rsa = System.Security.Cryptography.RSA;

namespace NetExtender.Utilities.Cryptography
{
    public enum RSAKeyType
    {
        RSA,
        RSAPublic,
        Pkcs8,
        Pkcs8Encrypted,
        /// <summary>X.509</summary>
        SubjectPublic
    }

    public static partial class CryptographyUtilities
    {
        public static class RSA
        {
            public const RSAKeyType DefaultRSAKeyType = RSAKeyType.RSA;

            public static Rsa Create(ReadOnlySpan<Byte> key, RSAKeyType type = DefaultRSAKeyType, RSAParameters? parameters = null)
            {
                Rsa rsa = parameters is null ? Rsa.Create() : Rsa.Create(parameters.Value);

                if (rsa is null)
                {
                    throw new FactoryException();
                }

                Int32 size = key.Length * 8;
                try
                {
                    rsa.KeySize = size;
                }
                catch (CryptographicException)
                {
                    throw new ArgumentException($"Invalid key size: {size}", nameof(key));
                }

                switch (type)
                {
                    case RSAKeyType.RSA:
                        rsa.ImportRSAPrivateKey(key);
                        return rsa;
                    case RSAKeyType.Pkcs8:
                        rsa.ImportPkcs8PrivateKey(key);
                        return rsa;
                    case RSAKeyType.RSAPublic:
                        rsa.ImportRSAPublicKey(key);
                        return rsa;
                    case RSAKeyType.SubjectPublic:
                        rsa.ImportSubjectPublicKeyInfo(key);
                        return rsa;
                    default:
                        throw new EnumUndefinedOrNotSupportedException<RSAKeyType>(type, nameof(type), null);
                }
            }

            public static String? Encrypt(String? text, ReadOnlySpan<Byte> key, RSAKeyType type = DefaultRSAKeyType, RSAParameters? parameters = null)
            {
                using Rsa rsa = Create(key, type, parameters);

                return Encrypt(text, rsa);
            }

            public static String? Encrypt(String? text, Rsa rsa)
            {
                if (text is null)
                {
                    return null;
                }

                try
                {
                    return Convert.ToBase64String(Encrypt(text.ToBytes(), rsa));
                }
                catch (CryptographicException)
                {
                    throw;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String? Decrypt(String? cipher, ReadOnlySpan<Byte> key, RSAKeyType type = DefaultRSAKeyType, RSAParameters? parameters = null)
            {
                using Rsa rsa = Create(key, type, parameters);

                return Decrypt(cipher, rsa);
            }

            public static String? Decrypt(String? cipher, Rsa rsa)
            {
                if (cipher is null)
                {
                    return null;
                }

                try
                {
                    return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(cipher), rsa));
                }
                catch (CryptographicException)
                {
                    throw;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static Byte[] Encrypt(Byte[] data, ReadOnlySpan<Byte> key, RSAKeyType type = DefaultRSAKeyType, RSAParameters? parameters = null)
            {
                if (data is null)
                {
                    throw new ArgumentNullException(nameof(data));
                }

                using Rsa rsa = Create(key, type, parameters);

                return Encrypt(data, rsa);
            }

            public static Byte[] Encrypt(Byte[] data, Rsa rsa)
            {
                if (data is null)
                {
                    throw new ArgumentNullException(nameof(data));
                }

                if (rsa is null)
                {
                    throw new ArgumentNullException(nameof(rsa));
                }

                return rsa.Encrypt(data);
            }

            public static Byte[] Decrypt(Byte[] data, ReadOnlySpan<Byte> key, RSAKeyType type = DefaultRSAKeyType, RSAParameters? parameters = null)
            {
                if (data is null)
                {
                    throw new ArgumentNullException(nameof(data));
                }

                using Rsa rsa = Create(key, type, parameters);

                return Decrypt(data, rsa);
            }

            public static Byte[] Decrypt(Byte[] data, Rsa rsa)
            {
                if (data is null)
                {
                    throw new ArgumentNullException(nameof(data));
                }

                if (rsa is null)
                {
                    throw new ArgumentNullException(nameof(rsa));
                }

                return rsa.Decrypt(data);
            }

            public static Rsa Clone(Rsa rsa)
            {
                if (rsa is null)
                {
                    throw new ArgumentNullException(nameof(rsa));
                }

                return Rsa.Create(rsa.ExportParameters(true)) ?? throw new FactoryException();
            }
        }
    }
}