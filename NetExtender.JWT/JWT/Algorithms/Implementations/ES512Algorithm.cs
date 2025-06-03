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
        /// ECDSA using SHA-512 hash algorithm
        /// </summary>
        public sealed class ES512Algorithm : ECDSAAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.ES512);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA512;
                }
            }

            public ES512Algorithm()
            {
            }

            public ES512Algorithm(ECDsa @public)
                : base(@public)
            {
            }

            public ES512Algorithm(ECDsa @public, ECDsa @private)
                : base(@public, @private)
            {
            }

            public ES512Algorithm(X509Certificate2 certificate)
            {
                ApplyCertificate(certificate);
            }
        }
    }
}
