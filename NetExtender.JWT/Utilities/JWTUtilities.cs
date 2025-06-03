// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using NetExtender.JWT;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.JWT.Interfaces;

namespace NetExtender.Utilities.JWT
{
    public static class JWTUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Create<T>(X509Certificate2 certificate) where T : class, IJWTCertificateAlgorithm, new()
        {
            if (certificate is null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            T algorithm = new T();
            algorithm.ApplyCertificate(certificate);
            return algorithm;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAsymmetric(this IJWTAlgorithm algorithm)
        {
            if (algorithm is null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            return algorithm is IJWTAsymmetricAlgorithm;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Deserialize<T>(this IJWTSerializer serializer, String json)
        {
            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (json is null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            return (T) serializer.Deserialize(typeof(T), json);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWTValidationInfo With(this JWTValidationInfo value, Action<JWTValidationInfo>? action)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            action?.Invoke(value);
            return value;
        }
    }
}