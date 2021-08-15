// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using NetExtender.Crypto.CryptKey.AES;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Crypto;
using Rsa = System.Security.Cryptography.RSA;

namespace NetExtender.Crypto.CryptKey.RSA
{
    public class LongRSACryptKey : AESCryptKey, IAsymmetricCryptKey
    {
        public static LongRSACryptKey GetLongRSACryptKeyFromEncryptedAES(ReadOnlySpan<Byte> encryptedkey, ReadOnlySpan<Byte> encryptediv,
            ReadOnlySpan<Byte> privatekey, RSAKeyType type = Cryptography.RSA.DefaultRSAKeyType, RSAParameters? parameters = null)
        {
            Rsa rsa = Cryptography.RSA.Create(privatekey, type, parameters);
            ReadOnlySpan<Byte> key = rsa.Decrypt(encryptedkey.ToArray());
            ReadOnlySpan<Byte> iv = rsa.Decrypt(encryptediv.ToArray());
            return new LongRSACryptKey(key, iv);
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

        public ReadOnlySpan<Byte> EncryptedKey
        {
            get
            {
                return Cryptography.RSA.Encrypt(Aes.Key, Rsa);
            }
        }
        
        public ReadOnlySpan<Byte> EncryptedIV
        {
            get
            {
                return Cryptography.RSA.Encrypt(Aes.IV, Rsa);
            }
        }
        
        protected Rsa Rsa { get; }
        
        public LongRSACryptKey(Int32 length = 2048)
        {
            Rsa = Rsa.Create(length);
        }
        
        public LongRSACryptKey(RSAParameters parameters)
        {
            Rsa = Rsa.Create(parameters);
        }

        public LongRSACryptKey(ReadOnlySpan<Byte> key)
            : base(key)
        {
            Rsa = Rsa.Create();
        }

        public LongRSACryptKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv)
            : base(key, iv)
        {
            Rsa = Rsa.Create();
        }
        
        public LongRSACryptKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv, ReadOnlySpan<Byte> rsakey, RSAParameters parameters)
            : this(key, iv, rsakey, Cryptography.RSA.DefaultRSAKeyType, parameters)
        {
        }
        
        public LongRSACryptKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv, ReadOnlySpan<Byte> rsakey, RSAKeyType type = Cryptography.RSA.DefaultRSAKeyType, RSAParameters? parameters = null)
            : base(key, iv)
        {
            Rsa = Cryptography.RSA.Create(rsakey, type, parameters);
        }

        public LongRSACryptKey(Aes aes, Boolean disposable = false)
            : base(aes, disposable)
        {
            Rsa = Rsa.Create();
        }
        
        public LongRSACryptKey(Aes aes, RSAParameters parameters, Boolean disposable = false)
            : base(aes, disposable)
        {
            Rsa = Rsa.Create(parameters);
        }
        
        public LongRSACryptKey(Aes aes, Rsa rsa, Boolean disposable = false)
            : base(aes, disposable)
        {
            Rsa = rsa;
        }
    }
}