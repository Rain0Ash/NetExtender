// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using NetExtender.Utils.Numerics;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Utils.Crypto;
using Rsa = System.Security.Cryptography.RSA;

namespace NetExtender.Crypto.CryptKey.RSA
{
    public class RSACryptKey : CryptKey, IAsymmetricCryptKey
    {
        public static ICryptKey Default { get; } = new RSACryptKey(2048, false);
        
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

        public RSACryptKey(Int32 length = 2048)
            : this(length, true)
        {
        }

        private RSACryptKey(Int32 length, Boolean disposable)
            : this(Rsa.Create(length.InRange(384, 16384) ? 
                       length % 8 == 0 ? length : throw new ArgumentException("Length must be a multiple of 8") : throw new ArgumentOutOfRangeException(nameof(length)))
                   ?? throw new FactoryException("Unknown exception"), disposable)
        {
        }

        public RSACryptKey(RSAParameters parameters)
            : base(true)
        {
            Rsa = Rsa.Create(parameters);
        }

        public RSACryptKey(ReadOnlySpan<Byte> key, RSAKeyType type = RSAKeyType.RSA, RSAParameters? parameters = null)
            : this(key, type, parameters, true)
        {
        }
        
        private RSACryptKey(ReadOnlySpan<Byte> key, RSAKeyType type, RSAParameters? parameters, Boolean disposable)
            : this(Cryptography.RSA.Create(key, type, parameters), disposable)
        {
        }

        public RSACryptKey(Rsa rsa, Boolean disposable = false)
            : base(disposable)
        {
            Rsa = rsa ?? throw new ArgumentNullException(nameof(rsa));
        }

        public override String EncryptString(String value)
        {
            return Cryptography.RSA.Encrypt(value, Rsa);
        }

        public override Byte[] EncryptBytes(Byte[] value)
        {
            return Cryptography.RSA.Encrypt(value, Rsa);
        }

        public override String DecryptString(String value)
        {
            return Cryptography.RSA.Decrypt(value, Rsa);
        }

        public override Byte[] DecryptBytes(Byte[] value)
        {
            return Cryptography.RSA.Decrypt(value, Rsa);
        }
        
        public override ICryptKey Clone(CryptAction crypt)
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