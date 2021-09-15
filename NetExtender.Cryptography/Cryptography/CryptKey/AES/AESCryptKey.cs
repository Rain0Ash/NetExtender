// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Exceptions;

namespace NetExtender.Crypto.CryptKey.AES
{
    public class AESCryptKey : CryptKey, ISymmetricCryptKey
    {
        public static ICryptKey Default { get; } = new AESCryptKey(Cryptography.DefaultHash.AsSpan(), Cryptography.AES.DefaultIV.AsSpan(), false);

        protected Aes Aes { get; }

        public override Int32 KeySize
        {
            get
            {
                return Key.Length;
            }
        }
        
        public ReadOnlySpan<Byte> Key
        {
            get
            {
                return Aes.Key;
            }
        }

        public ReadOnlySpan<Byte> IV
        {
            get
            {
                return Aes.IV;
            }
        }

        public CipherMode Mode
        {
            get
            {
                return Aes.Mode;
            }
        }
        
        public PaddingMode Padding
        {
            get
            {
                return Aes.Padding;
            }
        }

        public Int32 BlockSize
        {
            get
            {
                return Aes.BlockSize;
            }
        }
        
        public Int32 FeedbackSize
        {
            get
            {
                return Aes.FeedbackSize;
            }
        }

        public AESCryptKey()
            : this(true)
        {
        }

        protected AESCryptKey(Boolean disposable)
            : this(Aes.Create() ?? throw new FactoryException(), disposable)
        {
        }
        
        public AESCryptKey(ReadOnlySpan<Byte> key)
            : this(key, ReadOnlySpan<Byte>.Empty)
        {
        }

        public AESCryptKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv)
            : this(key, iv, true)
        {
        }

        protected AESCryptKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv, Boolean disposable)
            : this(Cryptography.AES.Create(key, iv), disposable)
        {
        }

        public AESCryptKey(Aes aes)
            : this(aes, false)
        {
        }

        public AESCryptKey(Aes aes, Boolean disposable)
            : base(disposable)
        {
            Aes = aes ?? throw new ArgumentNullException(nameof(aes));
        }

        public override String? EncryptString(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cryptography.AES.Encrypt(value, Aes);
        }

        public override Byte[]? EncryptBytes(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cryptography.AES.Encrypt(value, Aes);
        }

        public override String? DecryptString(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cryptography.AES.Decrypt(value, Aes);
        }

        public override Byte[]? DecryptBytes(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cryptography.AES.Decrypt(value, Aes);
        }

        public override ICryptKey Clone(CryptAction crypt)
        {
            return new AESCryptKey(Cryptography.AES.Clone(Aes))
            {
                Crypt = crypt
            };
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Aes.Dispose();
            }
        }
    }
}