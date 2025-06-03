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
        /// RSASSA-PKCS1-v1_5 using SHA-256
        /// </summary>
        public sealed class RS256Algorithm : RSAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.RS256);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA256;
                }
            }

            public RS256Algorithm()
            {
            }

            public RS256Algorithm(RSA @public)
                : base(@public)
            {
            }

            public RS256Algorithm(RSA @public, RSA @private)
                : base(@public, @private)
            {
            }

            public RS256Algorithm(X509Certificate2 certificate)
            {
                ApplyCertificate(certificate);
            }
        }
    }
}