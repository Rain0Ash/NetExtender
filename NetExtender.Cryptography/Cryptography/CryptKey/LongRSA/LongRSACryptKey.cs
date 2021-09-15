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
        public static LongRSACryptKey GetLongRSACryptKeyFromEncryptedAES(ReadOnlySpan<Byte> encryptedkey, ReadOnlySpan<Byte> encryptediv, ReadOnlySpan<Byte> privatekey)
        {
            return GetLongRSACryptKeyFromEncryptedAES(encryptedkey, encryptediv, privatekey, null);
        }
        
        public static LongRSACryptKey GetLongRSACryptKeyFromEncryptedAES(ReadOnlySpan<Byte> encryptedkey, ReadOnlySpan<Byte> encryptediv, ReadOnlySpan<Byte> privatekey, RSAParameters? parameters)
        {
            return GetLongRSACryptKeyFromEncryptedAES(encryptedkey, encryptediv, privatekey, Cryptography.RSA.DefaultRSAKeyType, parameters);
        }
        
        public static LongRSACryptKey GetLongRSACryptKeyFromEncryptedAES(ReadOnlySpan<Byte> encryptedkey, ReadOnlySpan<Byte> encryptediv, ReadOnlySpan<Byte> privatekey, RSAKeyType type)
        {
            return GetLongRSACryptKeyFromEncryptedAES(encryptedkey, encryptediv, privatekey, type, null);
        }

        public static LongRSACryptKey GetLongRSACryptKeyFromEncryptedAES(ReadOnlySpan<Byte> encryptedkey, ReadOnlySpan<Byte> encryptediv,
            ReadOnlySpan<Byte> privatekey, RSAKeyType type, RSAParameters? parameters)
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
        
        public LongRSACryptKey()
            : this(2048)
        {
        }
        
        public LongRSACryptKey(Int32 length)
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

        public LongRSACryptKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv, ReadOnlySpan<Byte> rsakey, RSAKeyType type)
            : this(key, iv, rsakey, type, null)
        {
        }
        
        public LongRSACryptKey(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv, ReadOnlySpan<Byte> rsakey, RSAKeyType type, RSAParameters? parameters)
            : base(key, iv)
        {
            Rsa = Cryptography.RSA.Create(rsakey, type, parameters);
        }

        public LongRSACryptKey(Aes aes)
            : this(aes, false)
        {
        }

        public LongRSACryptKey(Aes aes, Boolean disposable)
            : base(aes, disposable)
        {
            Rsa = Rsa.Create();
        }
        
        public LongRSACryptKey(Aes aes, RSAParameters parameters)
            : this(aes, parameters, false)
        {
        }
        
        public LongRSACryptKey(Aes aes, RSAParameters parameters, Boolean disposable)
            : base(aes, disposable)
        {
            Rsa = Rsa.Create(parameters);
        }
        
        public LongRSACryptKey(Aes aes, Rsa rsa)
            : this(aes, rsa, false)
        {
        }
        
        public LongRSACryptKey(Aes aes, Rsa rsa, Boolean disposable)
            : base(aes, disposable)
        {
            Rsa = rsa;
        }
    }
}