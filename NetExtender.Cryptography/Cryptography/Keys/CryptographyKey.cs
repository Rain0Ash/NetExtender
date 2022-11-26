// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Cryptography.Keys.AES;
using NetExtender.Cryptography.Keys.Interfaces;
using NetExtender.Cryptography.Keys.RSA;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Cryptography.Keys
{
    public enum CryptographyKeyType
    {
        AES,
        RSA
    }

    public abstract class CryptographyKey : ICryptographyKey
    {
        private static class AES
        {
            public static ICryptographyKey Default
            {
                get
                {
                    return AesCryptographyKey.Default;
                }
            }

            public static ICryptographyKey KeyCrypt
            {
                get
                {
                    return Default.Clone(CryptAction.Crypt);
                }
            }

            public static ICryptographyKey KeyDecrypt
            {
                get
                {
                    return Default.Clone(CryptAction.Decrypt);
                }
            }
            public static ICryptographyKey KeyEncrypt
            {
                get
                {
                    return Default.Clone(CryptAction.Encrypt);
                }
            }
            public static ICryptographyKey KeyNone
            {
                get
                {
                    return Default.Clone(CryptAction.None);
                }
            }
        }

        private static class RSA
        {
            public static IAsymmetricCryptographyKey Default
            {
                get
                {
                    return RsaCryptographyKey.Default;
                }
            }

            public static IAsymmetricCryptographyKey KeyCrypt
            {
                get
                {
                    return Default.Clone(CryptAction.Crypt);
                }
            }
            public static IAsymmetricCryptographyKey KeyDecrypt
            {
                get
                {
                    return Default.Clone(CryptAction.Decrypt);
                }
            }
            public static IAsymmetricCryptographyKey KeyEncrypt
            {
                get
                {
                    return Default.Clone(CryptAction.Encrypt);
                }
            }
            public static IAsymmetricCryptographyKey KeyNone
            {
                get
                {
                    return Default.Clone(CryptAction.None);
                }
            }
        }

        public static ICryptographyKey Create(CryptAction crypt)
        {
            return Create(crypt, CryptographyKeyType.AES);
        }

        public static ICryptographyKey Create(CryptAction crypt, CryptographyKeyType type)
        {
            return type switch
            {
                CryptographyKeyType.AES => crypt switch
                {
                    CryptAction.None => AES.KeyNone,
                    CryptAction.Decrypt => AES.KeyDecrypt,
                    CryptAction.Encrypt => AES.KeyEncrypt,
                    CryptAction.Crypt => AES.KeyCrypt,
                    _ => AES.Default
                },
                CryptographyKeyType.RSA => crypt switch
                {
                    CryptAction.None => RSA.KeyNone,
                    CryptAction.Decrypt => RSA.KeyDecrypt,
                    CryptAction.Encrypt => RSA.KeyEncrypt,
                    CryptAction.Crypt => RSA.KeyCrypt,
                    _ => RSA.Default
                },
                _ => throw new NotSupportedException()
            };
        }

        public abstract Int32 KeySize { get; }

        protected Boolean Disposable { get; }

        public CryptAction Crypt { get; init; } = CryptAction.Crypt;

        public Boolean IsEncrypt
        {
            get
            {
                return Crypt.HasFlag(CryptAction.Encrypt);
            }
        }

        public Boolean IsDecrypt
        {
            get
            {
                return Crypt.HasFlag(CryptAction.Decrypt);
            }
        }

        public abstract Boolean IsDeterministic { get; }

        protected CryptographyKey(Boolean disposable)
        {
            Disposable = disposable;
        }

        public String? Encrypt(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Crypt.HasFlag(CryptAction.Encrypt) ? EncryptString(value) : value;
        }

        public IEnumerable<String?> Encrypt(IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Crypt.HasFlag(CryptAction.Encrypt) ? EncryptString(source) : source;
        }

        public abstract String? EncryptString(String value);

        public IEnumerable<String?> EncryptString(IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(EncryptString);
        }

        public Byte[]? Encrypt(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Crypt.HasFlag(CryptAction.Encrypt) ? EncryptBytes(value) : value;
        }

        public abstract Byte[]? EncryptBytes(Byte[] value);

        public String? Decrypt(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Crypt.HasFlag(CryptAction.Decrypt) ? DecryptString(value) : value;
        }

        public IEnumerable<String?> Decrypt(IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Crypt.HasFlag(CryptAction.Decrypt) ? DecryptString(source) : source;
        }

        public abstract String? DecryptString(String value);

        public IEnumerable<String?> DecryptString(IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(DecryptString);
        }

        public Byte[]? Decrypt(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Crypt.HasFlag(CryptAction.Decrypt) ? DecryptBytes(value) : value;
        }

        public abstract Byte[]? DecryptBytes(Byte[] value);

        public virtual ICryptographyKey Clone()
        {
            return Clone(Crypt);
        }

        public abstract ICryptographyKey Clone(CryptAction crypt);

        public void Dispose()
        {
            Dispose(Disposable);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }
    }
}