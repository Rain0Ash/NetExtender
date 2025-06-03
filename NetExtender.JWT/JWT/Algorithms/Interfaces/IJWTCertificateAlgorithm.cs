// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Security.Cryptography.X509Certificates;

namespace NetExtender.JWT.Algorithms.Interfaces
{
    public interface IJWTCertificateAlgorithm : IJWTAsymmetricAlgorithm
    {
        public void ApplyCertificate(X509Certificate2 certificate);
    }
}