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
        /// RSASSA-PKCS1-v1_5 using SHA-1024
        /// </summary>
        public sealed class RS1024Algorithm : RSAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.RS1024);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA512;
                }
            }

            public RS1024Algorithm()
            {
            }

            public RS1024Algorithm(RSA @public)
                : base(@public)
            {
            }

            public RS1024Algorithm(RSA @public, RSA @private)
                : base(@public, @private)
            {
            }

            public RS1024Algorithm(X509Certificate2 certificate)
            {
                ApplyCertificate(certificate);
            }
        }
    }
}