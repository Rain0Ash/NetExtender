// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-256
        /// </summary>
        public abstract class RSAlgorithm : CertificateAlgorithm<RSA>
        {
            protected RSAlgorithm()
            {
            }

            protected RSAlgorithm(RSA @public)
                : base(@public)
            {
            }

            protected RSAlgorithm(RSA @public, RSA @private)
                : base(@public, @private)
            {
            }

            protected override RSA? GetPublicKey(X509Certificate2 certificate)
            {
                return certificate.GetRSAPublicKey();
            }

            protected override RSA? GetPrivateKey(X509Certificate2 certificate)
            {
                return certificate.GetRSAPrivateKey();
            }

            public override Boolean Verify(ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature)
            {
                return Public.VerifyData(source, signature, Algorithm, RSASignaturePadding.Pkcs1);
            }

            public override Boolean Verify(Byte[] source, Byte[] signature)
            {
                return Public.VerifyData(source, signature, Algorithm, RSASignaturePadding.Pkcs1);
            }

            public override Byte[] Sign(Byte[] source)
            {
                return VerifyPrivate.SignData(source, Algorithm, RSASignaturePadding.Pkcs1);
            }

            public override ValueTask<Byte[]?> SignAsync(Byte[] source, CancellationToken token)
            {
                if (token.IsCancellationRequested)
                {
                    return ValueTask.FromCanceled<Byte[]?>(token);
                }
                
                try
                {
                    return ValueTask.FromResult<Byte[]?>(Sign(source));
                }
                catch (Exception exception)
                {
                    return ValueTask.FromException<Byte[]?>(exception);
                }
            }

            public override Boolean TrySign(ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
            {
                return VerifyPrivate.TrySignData(source, destination, Algorithm, RSASignaturePadding.Pkcs1, out written);
            }
        }
    }
}
