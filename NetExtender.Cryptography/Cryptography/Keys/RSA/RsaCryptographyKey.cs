// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using NetExtender.Cryptography.Keys.Interfaces;
using NetExtender.Utilities.Cryptography;
using Rsa = System.Security.Cryptography.RSA;

namespace NetExtender.Cryptography.Keys.RSA
{
    public class RsaCryptographyKey : CryptographyKey, IAsymmetricCryptographyKey
    {
        public static IAsymmetricCryptographyKey Default { get; } = new RsaCryptographyKey(2048, false);

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

        public RsaCryptographyKey()
            : this(2048)
        {
        }

        public RsaCryptographyKey(Int32 length)
            : this(length, true)
        {
        }

        protected RsaCryptographyKey(Int32 length, Boolean disposable)
            : base(disposable)
        {
            if (length < 384 || length > 16384)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            if (length % 8 != 0)
            {
                throw new ArgumentException("Length must be a multiple of 8");
            }

            Rsa = Rsa.Create(length);
        }

        public RsaCryptographyKey(RSAParameters parameters)
            : base(true)
        {
            Rsa = Rsa.Create(parameters);
        }

        public RsaCryptographyKey(ReadOnlySpan<Byte> key)
            : this(key, null)
        {
        }

        public RsaCryptographyKey(ReadOnlySpan<Byte> key, RSAKeyType type)
            : this(key, type, null)
        {
        }

        public RsaCryptographyKey(ReadOnlySpan<Byte> key, RSAParameters? parameters)
            : this(key, RSAKeyType.RSA, parameters)
        {
        }

        public RsaCryptographyKey(ReadOnlySpan<Byte> key, RSAKeyType type, RSAParameters? parameters)
            : this(key, type, parameters, true)
        {
        }

        protected RsaCryptographyKey(ReadOnlySpan<Byte> key, RSAKeyType type, RSAParameters? parameters, Boolean disposable)
            : this(CryptographyUtilities.RSA.Create(key, type, parameters), disposable)
        {
        }

        public RsaCryptographyKey(Rsa rsa)
            : this(rsa, false)
        {
        }

        public RsaCryptographyKey(Rsa rsa, Boolean disposable)
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

            return CryptographyUtilities.RSA.Encrypt(value, Rsa);
        }

        public override Byte[] EncryptBytes(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return CryptographyUtilities.RSA.Encrypt(value, Rsa);
        }

        public override String? DecryptString(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return CryptographyUtilities.RSA.Decrypt(value, Rsa);
        }

        public override Byte[] DecryptBytes(Byte[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return CryptographyUtilities.RSA.Decrypt(value, Rsa);
        }

        public override IAsymmetricCryptographyKey Clone()
        {
            return Clone(Crypt);
        }

        public override IAsymmetricCryptographyKey Clone(CryptAction crypt)
        {
            return new RsaCryptographyKey(CryptographyUtilities.RSA.Clone(Rsa))
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