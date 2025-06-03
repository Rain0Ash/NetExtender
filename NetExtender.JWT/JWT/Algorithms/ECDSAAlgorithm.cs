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
        public abstract class ECDSAAlgorithm : CertificateAlgorithm<ECDsa>
        {
            protected ECDSAAlgorithm()
            {
            }

            protected ECDSAAlgorithm(ECDsa @public)
                : base(@public)
            {
            }

            protected ECDSAAlgorithm(ECDsa @public, ECDsa @private)
                : base(@public, @private)
            {
            }

            protected override ECDsa? GetPublicKey(X509Certificate2 certificate)
            {
                return certificate.GetECDsaPublicKey();
            }

            protected override ECDsa? GetPrivateKey(X509Certificate2 certificate)
            {
                return certificate.GetECDsaPrivateKey();
            }

            public override Boolean Verify(ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature)
            {
                return Public.VerifyData(source, signature, Algorithm);
            }

            public override Boolean Verify(Byte[] source, Byte[] signature)
            {
                return Public.VerifyData(source, signature, Algorithm);
            }

            public override Byte[] Sign(Byte[] source)
            {
                return VerifyPrivate.SignData(source, Algorithm);
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
                return VerifyPrivate.TrySignData(source, destination, Algorithm, out written);
            }
        }
    }
}
