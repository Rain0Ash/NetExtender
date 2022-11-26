// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography.X509Certificates;

namespace NetExtender.Cryptography.Certificate
{
    public sealed record X509CertificateRootContainer : IDisposable
    {
        public X509Certificate Root { get; }
        public X509Certificate Certificate { get; }

        public X509CertificateRootContainer(X509Certificate root, X509Certificate certificate)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
            Certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
        }

        public void Dispose()
        {
            Root.Dispose();
            Certificate.Dispose();
        }
    }
}