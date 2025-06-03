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
        /// RSASSA-PKCS1-v1_5 using SHA-4096
        /// </summary>
        public sealed class RS4096Algorithm : RSAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.RS4096);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA512;
                }
            }

            public RS4096Algorithm()
            {
            }

            public RS4096Algorithm(RSA @public)
                : base(@public)
            {
            }

            public RS4096Algorithm(RSA @public, RSA @private)
                : base(@public, @private)
            {
            }

            public RS4096Algorithm(X509Certificate2 certificate)
            {
                ApplyCertificate(certificate);
            }
        }
    }
}