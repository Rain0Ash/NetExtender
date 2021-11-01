// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using NetExtender.Types.Exceptions;

namespace NetExtender.Crypto
{
    public static partial class Cryptography
    {
        public static class AES
        {
            public const Int32 IVSize = 16;
            public static ImmutableArray<Byte> DefaultIV { get; } = new Byte[IVSize].ToImmutableArray();

            public static Aes Create(ReadOnlySpan<Byte> key)
            {
                return Create(key, ReadOnlySpan<Byte>.Empty);
            }
            
            public static Aes Create(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv)
            {
                Aes aes = Aes.Create();

                if (aes is null)
                {
                    throw new FactoryException();
                }

                Int32 size = key.Length * 8;
                if (!aes.ValidKeySize(size))
                {
                    throw new ArgumentException($@"Invalid key size: {size}", nameof(key));
                }
                
                if (iv.IsEmpty)
                {
                    iv = DefaultIV.ToArray();
                }
                else if (iv.Length != IVSize)
                {
                    throw new ArgumentException($@"Invalid IV size: {iv.Length * 8}", nameof(key));
                }

                aes.Mode = CipherMode.CBC;
                aes.KeySize = size;
                aes.BlockSize = aes.KeySize;
                aes.FeedbackSize = aes.KeySize;
                aes.Padding = PaddingMode.Zeros;

                aes.Key = key.ToArray();
                aes.IV = iv.ToArray();

                return aes;
            }
            
            public static String? Encrypt(String? text, String key, HashType hash)
            {
                return Encrypt(text, Hashing(key, hash));
            }

            public static String? Encrypt(String? text, ReadOnlySpan<Byte> key)
            {
                return Encrypt(text, key, ReadOnlySpan<Byte>.Empty);
            }

            public static String? Encrypt(String? text, ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv)
            {
                using Aes aes = Create(key, iv);

                return Encrypt(text, aes);
            }
            
            public static String? Encrypt(String? text, Aes aes)
            {
                if (text is null)
                {
                    return null;
                }
                
                try
                {
                    using ICryptoTransform encryptor = aes.CreateEncryptor();

                    using MemoryStream memory = new MemoryStream();
                    using CryptoStream crypto = new CryptoStream(memory, encryptor, CryptoStreamMode.Write);
                    using (StreamWriter writer = new StreamWriter(crypto))
                    {
                        writer.Write(text);
                    }

                    Byte[] array = memory.ToArray();

                    return Convert.ToBase64String(array);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String? Decrypt(String? cipher, String key, HashType hash)
            {
                return Decrypt(cipher, Hashing(key, hash));
            }

            public static String? Decrypt(String? cipher, ReadOnlySpan<Byte> key)
            {
                return Decrypt(cipher, key, ReadOnlySpan<Byte>.Empty);
            }

            public static String? Decrypt(String? cipher, ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv)
            {
                using Aes aes = Create(key, iv);

                return Decrypt(cipher, aes);
            }

            public static String? Decrypt(String? cipher, Aes aes)
            {
                if (cipher is null)
                {
                    return null;
                }

                try
                {
                    Byte[] buffer = Convert.FromBase64String(cipher);
                    
                    using ICryptoTransform decryptor = aes.CreateDecryptor();

                    using MemoryStream memory = new MemoryStream(buffer);
                    using CryptoStream crypto = new CryptoStream(memory, decryptor, CryptoStreamMode.Read);
                    using StreamReader reader = new StreamReader(crypto);
                    return reader.ReadToEnd();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            
            public static Byte[]? Encrypt(Byte[]? data, ReadOnlySpan<Byte> key)
            {
                return Encrypt(data, key, ReadOnlySpan<Byte>.Empty);
            }
            
            public static Byte[]? Encrypt(Byte[]? data, ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv)
            {
                using Aes aes = Create(key, iv);

                return Encrypt(data, aes);
            }

            public static Byte[]? Encrypt(Byte[]? data, Aes aes)
            {
                if (aes is null)
                {
                    throw new ArgumentNullException(nameof(aes));
                }

                using ICryptoTransform encryptor = aes.CreateEncryptor();
                return PerformCryptography(data, encryptor);
            }

            public static Byte[]? Decrypt(Byte[]? data, ReadOnlySpan<Byte> key)
            {
                return Decrypt(data, key, ReadOnlySpan<Byte>.Empty);
            }

            public static Byte[]? Decrypt(Byte[]? data, ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> iv)
            {
                using Aes aes = Create(key, iv);

                return Decrypt(data, aes);
            }

            public static Byte[]? Decrypt(Byte[]? data, Aes aes)
            {
                using ICryptoTransform decryptor = aes.CreateDecryptor();
                return PerformCryptography(data, decryptor);
            }

            private static Byte[]? PerformCryptography(Byte[]? data, ICryptoTransform transform)
            {
                if (data is null)
                {
                    return null;
                }
                
                using MemoryStream memory = new MemoryStream();
                using CryptoStream crypto = new CryptoStream(memory, transform, CryptoStreamMode.Write);

                try
                {
                    crypto.Write(data, 0, data.Length);
                    crypto.FlushFinalBlock();
                }
                catch(Exception)
                {
                    return null;
                }
                
                return memory.ToArray();
            }

            public static Aes Clone(Aes aes)
            {
                if (aes is null)
                {
                    throw new ArgumentNullException(nameof(aes));
                }

                Aes copy = Aes.Create() ?? throw new FactoryException();
                
                copy.Mode = aes.Mode;
                copy.KeySize = aes.KeySize;
                copy.BlockSize = aes.BlockSize;
                copy.FeedbackSize = aes.FeedbackSize;
                copy.Padding = aes.Padding;

                copy.Key = aes.Key;
                copy.IV = aes.IV;

                return copy;
            }
        }
    }
}