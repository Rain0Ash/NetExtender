// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace NetExtender.Utilities.Crypto
{
    public static class RSAUtilities
    {
        /// <inheritdoc cref="RSA.ImportRSAPrivateKey"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImportRSAPrivateKey(this RSA rsa, ReadOnlySpan<Byte> key)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            rsa.ImportRSAPrivateKey(key, out _);
        }
        
        /// <inheritdoc cref="RSA.ImportPkcs8PrivateKey"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImportPkcs8PrivateKey(this RSA rsa, ReadOnlySpan<Byte> key)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            rsa.ImportPkcs8PrivateKey(key, out _);
        }
        
        /// <inheritdoc cref="RSA.ImportEncryptedPkcs8PrivateKey(System.ReadOnlySpan{Byte},System.ReadOnlySpan{Byte},out Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImportEncryptedPkcs8PrivateKey(this RSA rsa, ReadOnlySpan<Byte> password, ReadOnlySpan<Byte> key)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            rsa.ImportEncryptedPkcs8PrivateKey(password, key, out _);
        }
        
        /// <inheritdoc cref="RSA.ImportEncryptedPkcs8PrivateKey(System.ReadOnlySpan{Char},System.ReadOnlySpan{Byte},out Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImportEncryptedPkcs8PrivateKey(this RSA rsa, ReadOnlySpan<Char> password, ReadOnlySpan<Byte> key)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            rsa.ImportEncryptedPkcs8PrivateKey(password, key, out _);
        }
        
        /// <inheritdoc cref="RSA.ImportRSAPublicKey"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImportRSAPublicKey(this RSA rsa, ReadOnlySpan<Byte> key)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            rsa.ImportRSAPublicKey(key, out _);
        }
        
        /// <inheritdoc cref="RSA.ImportSubjectPublicKeyInfo"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImportSubjectPublicKeyInfo(this RSA rsa, ReadOnlySpan<Byte> key)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            rsa.ImportSubjectPublicKeyInfo(key, out _);
        }

        /// <inheritdoc cref="RSA.Encrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Encrypt(this RSA rsa, Byte[] data)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
        }
        
        /// <inheritdoc cref="RSA.Decrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Decrypt(this RSA rsa, Byte[] data)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
        }
        
        /// <inheritdoc cref="RSA.TryEncrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryEncrypt(this RSA rsa, ReadOnlySpan<Byte> data, Span<Byte> destination)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return TryEncrypt(rsa, data, destination, RSAEncryptionPadding.OaepSHA256);
        }
        
        /// <inheritdoc cref="RSA.TryEncrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryEncrypt(this RSA rsa, ReadOnlySpan<Byte> data, Span<Byte> destination, RSAEncryptionPadding padding)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return rsa.TryEncrypt(data, destination, padding, out _);
        }

        /// <inheritdoc cref="RSA.TryEncrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryEncrypt(this RSA rsa, ReadOnlySpan<Byte> data, out Byte[] buffer)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return TryEncrypt(rsa, data, RSAEncryptionPadding.OaepSHA256, out buffer);
        }
        
        /// <inheritdoc cref="RSA.TryEncrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryEncrypt(this RSA rsa, ReadOnlySpan<Byte> data, RSAEncryptionPadding padding, out Byte[] buffer)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            buffer = new Byte[data.Length];
            return rsa.TryEncrypt(data, buffer, padding, out _);
        }
        
        /// <inheritdoc cref="RSA.TryEncrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryDecrypt(this RSA rsa, ReadOnlySpan<Byte> data, Span<Byte> destination)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return TryDecrypt(rsa, data, destination, RSAEncryptionPadding.OaepSHA256);
        }
        
        /// <inheritdoc cref="RSA.TryEncrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryDecrypt(this RSA rsa, ReadOnlySpan<Byte> data, Span<Byte> destination, RSAEncryptionPadding padding)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return rsa.TryDecrypt(data, destination, padding, out _);
        }
        
        /// <inheritdoc cref="RSA.TryDecrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryDecrypt(this RSA rsa, ReadOnlySpan<Byte> data, out Byte[] buffer)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return TryDecrypt(rsa, data, RSAEncryptionPadding.OaepSHA256, out buffer);
        }
        
        /// <inheritdoc cref="RSA.TryDecrypt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryDecrypt(this RSA rsa, ReadOnlySpan<Byte> data, RSAEncryptionPadding padding, out Byte[] buffer)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            buffer = new Byte[data.Length];
            return rsa.TryDecrypt(data, buffer, padding, out _);
        }
        
        /// <inheritdoc cref="RSA.ExportParameters"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RSAParameters ExportParameters(this RSA rsa)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return rsa.ExportParameters(false);
        }
        
        /// <inheritdoc cref="RSA.ExportParameters"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RSAParameters ExportPrivateParameters(this RSA rsa)
        {
            if (rsa is null)
            {
                throw new ArgumentNullException(nameof(rsa));
            }

            return rsa.ExportParameters(true);
        }
    }
}