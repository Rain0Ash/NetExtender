// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using NetExtender.Cryptography.Keys.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Cryptography.Keys.AES
{
    public class AesCryptographyKey : CryptographyKey, ISymmetricCryptographyKey
    {
        public static ICryptographyKey Default { get; } = new AesCryptographyKey(CryptographyUtilities.DefaultHash.AsSpan(), CryptographyUtilities.AES.DefaultIV.AsSpan(), false);

        protected Aes Aes { get; }

        public override Int32 KeySize
        {
            get
            {
                return Key.Length;
            }
        }
        
        public override Boolean IsDeterministic
        {
            get
            {
                return true;
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

        public AesCryptographyKey()
            : this(true)
        {
        }

        protected AesCryptographyKey(Boolean disposable)
            : this(Aes.Create() ?? throw new FactoryException(), disposable)
        {
        }
        
        public AesCryptographyKey(ReadOnlySpan<Byte> key)
            : this(key, ReadOnlySpan<Byte>.Empty)
        {
        }

        public AesCryptographyKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv)
            : this(key, iv, true)
        {
        }

        protected AesCryptographyKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv, Boolean disposable)
            : this(CryptographyUtilities.AES.Create(key, iv), disposable)
        {
        }

        public AesCryptographyKey(Aes aes)
            : this(aes, false)
        {
        }

        public AesCryptographyKey(Aes aes, Boolean disposable)
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

            return CryptographyUtilities.AES.Encrypt(value, Aes);
        }

        public override Byte[]? EncryptBytes(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return CryptographyUtilities.AES.Encrypt(value, Aes);
        }

        public override String? DecryptString(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return CryptographyUtilities.AES.Decrypt(value, Aes)?.TrimEnd('\0');
        }

        public override Byte[]? DecryptBytes(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return CryptographyUtilities.AES.Decrypt(value, Aes);
        }

        public override ICryptographyKey Clone(CryptAction crypt)
        {
            return new AesCryptographyKey(CryptographyUtilities.AES.Clone(Aes))
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