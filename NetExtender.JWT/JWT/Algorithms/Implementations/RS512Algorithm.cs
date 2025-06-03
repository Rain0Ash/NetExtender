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
        /// RSASSA-PKCS1-v1_5 using SHA-512
        /// </summary>
        public sealed class RS512Algorithm : RSAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.RS512);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA512;
                }
            }

            public RS512Algorithm()
            {
            }

            public RS512Algorithm(RSA @public)
                : base(@public)
            {
            }

            public RS512Algorithm(RSA @public, RSA @private)
                : base(@public, @private)
            {
            }

            public RS512Algorithm(X509Certificate2 certificate)
            {
                ApplyCertificate(certificate);
            }
        }
    }
}
