// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography.X509Certificates;

namespace NetExtender.Utilities.Cryptography
{
    public enum CertificateKeyStrength
    {
        Rsa1024 = 1024,
        Rsa2048 = 2048,
        Rsa4096 = 4096,
        Rsa8192 = 8192,
        Rsa16384 = 16384
    }
    
    public static class CertificateUtilities
    {
        public static void AddCertificateToStore(this X509Certificate2 certificate, X509Store store)
        {
            if (certificate is null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            if (store is null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            store.Open(OpenFlags.ReadWrite);
            store.Add(certificate);
        }
        
        public static void AddCertificateToStore(this X509Certificate2 certificate, StoreName name, StoreLocation location)
        {
            if (certificate is null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            using X509Store store = new X509Store(name, location);
            AddCertificateToStore(certificate, store);
        }

        public static Boolean TryAddCertificateToStore(this X509Certificate2 certificate, X509Store store)
        {
            if (certificate is null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            if (store is null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            
            try
            {
                AddCertificateToStore(certificate, store);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TryAddCertificateToStore(this X509Certificate2 certificate, StoreName name, StoreLocation location)
        {
            if (certificate is null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            try
            {
                AddCertificateToStore(certificate, name, location);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}