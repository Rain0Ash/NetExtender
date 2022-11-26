// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography.X509Certificates;

namespace NetExtender.Cryptography.Certificate
{
    public sealed record X509Certificate2RootContainer : IDisposable
    {
        public X509Certificate2 Root { get; }
        public X509Certificate2 Certificate { get; }

        public X509Certificate2RootContainer(X509Certificate2 root, X509Certificate2 certificate)
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