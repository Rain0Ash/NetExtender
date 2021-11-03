// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Crypto;
using Rsa = System.Security.Cryptography.RSA;

namespace NetExtender.Crypto.CryptKey.RSA
{
    public class RSACryptKey : CryptKey, IAsymmetricCryptKey
    {
        public static IAsymmetricCryptKey Default { get; } = new RSACryptKey(2048, false);
        
        protected Rsa Rsa { get; }

        public override Int32 KeySize
        {
            get
            {
                return Rsa.KeySize;
            }
        }

        public ReadOnlySpan<Byte> PrivateKey
        {
            get
            {
                return Rsa.ExportRSAPrivateKey();
            }
        }
        
        public ReadOnlySpan<Byte> PublicKey
        {
            get
            {
                return Rsa.ExportRSAPublicKey();
            }
        }

        public RSAParameters Parameters
        {
            get
            {
                return Rsa.ExportParameters();
            }
        }
        
        public RSAParameters PrivateParameters
        {
            get
            {
                return Rsa.ExportPrivateParameters();
            }
        }

        public RSACryptKey()
            : this(2048)
        {
        }

        public RSACryptKey(Int32 length)
            : this(length, true)
        {
        }

        protected RSACryptKey(Int32 length, Boolean disposable)
            : base(disposable)
        {
            if (length < 384 || length > 16384)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (length % 8 != 0)
            {
                throw new ArgumentException("Length must be a multiple of 8");
            }
            
            Rsa = Rsa.Create(length);
        }

        public RSACryptKey(RSAParameters parameters)
            : base(true)
        {
            Rsa = Rsa.Create(parameters);
        }

        public RSACryptKey(ReadOnlySpan<Byte> key)
            : this(key, null)
        {
        }

        public RSACryptKey(ReadOnlySpan<Byte> key, RSAKeyType type)
            : this(key, type, null)
        {
        }

        public RSACryptKey(ReadOnlySpan<Byte> key, RSAParameters? parameters)
            : this(key, RSAKeyType.RSA, parameters)
        {
        }

        public RSACryptKey(ReadOnlySpan<Byte> key, RSAKeyType type, RSAParameters? parameters)
            : this(key, type, parameters, true)
        {
        }
        
        protected RSACryptKey(ReadOnlySpan<Byte> key, RSAKeyType type, RSAParameters? parameters, Boolean disposable)
            : this(Cryptography.RSA.Create(key, type, parameters), disposable)
        {
        }

        public RSACryptKey(Rsa rsa)
            : this(rsa, false)
        {
        }

        public RSACryptKey(Rsa rsa, Boolean disposable)
            : base(disposable)
        {
            Rsa = rsa ?? throw new ArgumentNullException(nameof(rsa));
        }

        public override Boolean IsDeterministic
        {
            get
            {
                return false;
            }
        }

        public override String? EncryptString(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cryptography.RSA.Encrypt(value, Rsa);
        }

        public override Byte[] EncryptBytes(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cryptography.RSA.Encrypt(value, Rsa);
        }

        public override String? DecryptString(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cryptography.RSA.Decrypt(value, Rsa);
        }

        public override Byte[] DecryptBytes(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cryptography.RSA.Decrypt(value, Rsa);
        }
        
        public override IAsymmetricCryptKey Clone()
        {
            return Clone(Crypt);
        }

        public override IAsymmetricCryptKey Clone(CryptAction crypt)
        {
            return new RSACryptKey(Cryptography.RSA.Clone(Rsa))
            {
                Crypt = crypt
            };
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Rsa.Dispose();
            }
        }
    }
}