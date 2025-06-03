// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public sealed class ES384Algorithm : ECDSAAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.ES384);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA384;
                }
            }

            public ES384Algorithm()
            {
            }

            public ES384Algorithm(ECDsa @public)
                : base(@public)
            {
            }

            public ES384Algorithm(ECDsa @public, ECDsa @private)
                : base(@public, @private)
            {
            }

            public ES384Algorithm(X509Certificate2 certificate)
            {
                ApplyCertificate(certificate);
            }
        }
    }
}
