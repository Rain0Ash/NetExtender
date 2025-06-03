// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-2048
        /// </summary>
        public sealed class RS2048Algorithm : RSAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.RS2048);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA512;
                }
            }

            public RS2048Algorithm()
            {
            }

            public RS2048Algorithm(RSA @public)
                : base(@public)
            {
            }

            public RS2048Algorithm(RSA @public, RSA @private)
                : base(@public, @private)
            {
            }

            public RS2048Algorithm(X509Certificate2 certificate)
            {
                ApplyCertificate(certificate);
            }
        }
    }
}