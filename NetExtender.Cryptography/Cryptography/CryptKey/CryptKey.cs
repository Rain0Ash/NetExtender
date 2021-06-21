// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Crypto.CryptKey.AES;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Crypto.CryptKey.RSA;

namespace NetExtender.Crypto.CryptKey
{
    public enum CryptType
    {
        AES,
        RSA
    }
    
    public abstract class CryptKey : ICryptKey
    {
        private static class AES
        {
            public static ICryptKey Default { get; } = AESCryptKey.Default;
            public static ICryptKey KeyCrypt { get; } = Default.Clone(CryptAction.Crypt);
            public static ICryptKey KeyDecrypt { get; } = Default.Clone(CryptAction.Decrypt);
            public static ICryptKey KeyEncrypt { get; } = Default.Clone(CryptAction.Encrypt);
            public static ICryptKey KeyNone { get; } = Default.Clone(CryptAction.None);
        }
        
        private static class RSA
        {
            public static ICryptKey Default { get; } = RSACryptKey.Default;
            public static ICryptKey KeyCrypt { get; } = Default.Clone(CryptAction.Crypt);
            public static ICryptKey KeyDecrypt { get; } = Default.Clone(CryptAction.Decrypt);
            public static ICryptKey KeyEncrypt { get; } = Default.Clone(CryptAction.Encrypt);
            public static ICryptKey KeyNone { get; } = Default.Clone(CryptAction.None);
        }

        public static ICryptKey Create(CryptAction crypt, CryptType type = CryptType.AES)
        {
            return type switch
            {
                CryptType.AES => crypt switch
                {
                    CryptAction.None => AES.KeyNone,
                    CryptAction.Decrypt => AES.KeyDecrypt,
                    CryptAction.Encrypt => AES.KeyEncrypt,
                    CryptAction.Crypt => AES.KeyCrypt,
                    _ => AES.Default
                },
                CryptType.RSA => crypt switch
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
        
        public CryptAction Crypt { get; set; } = CryptAction.Crypt;

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

        protected CryptKey(Boolean disposable)
        {
            Disposable = disposable;
        }

        public String Encrypt(String value)
        {
            return Crypt.HasFlag(CryptAction.Encrypt) ? EncryptString(value) : value;
        }
        
        public IEnumerable<String> Encrypt(IEnumerable<String> source)
        {
            return Crypt.HasFlag(CryptAction.Encrypt) ? EncryptString(source) : source;
        }

        public abstract String EncryptString(String value);

        public IEnumerable<String> EncryptString(IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(EncryptString);
        }

        public Byte[] Encrypt(Byte[] value)
        {
            return Crypt.HasFlag(CryptAction.Encrypt) ? EncryptBytes(value) : value;
        }
        
        public abstract Byte[] EncryptBytes(Byte[] value);

        public String Decrypt(String value)
        {
            return Crypt.HasFlag(CryptAction.Decrypt) ? DecryptString(value) : value;
        }
        
        public IEnumerable<String> Decrypt(IEnumerable<String> source)
        {
            return Crypt.HasFlag(CryptAction.Decrypt) ? DecryptString(source) : source;
        }
        
        public abstract String DecryptString(String value);
        
        public IEnumerable<String> DecryptString(IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(DecryptString);
        }

        public Byte[] Decrypt(Byte[] value)
        {
            return Crypt.HasFlag(CryptAction.Decrypt) ? DecryptBytes(value) : value;
        }
        
        public abstract Byte[] DecryptBytes(Byte[] value);

        public ICryptKey Clone()
        {
            return Clone(Crypt);
        }
        
        public abstract ICryptKey Clone(CryptAction crypt);

        public void Dispose()
        {
            Dispose(Disposable);
        }

        protected abstract void Dispose(Boolean disposing);
    }
}