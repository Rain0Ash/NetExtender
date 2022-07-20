// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using NetExtender.BouncyCastle.Types.Numerics;
using NetExtender.BouncyCastle.Utilities;
using NetExtender.Cryptography.Certificate;
using NetExtender.Utilities.Types;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace NetExtender.Utilities.Cryptography
{
    public static class BouncyCertificateUtilities
    {
        private const CertificateKeyStrength DefaultCertificateKeyStrength = CertificateKeyStrength.Rsa2048;
        private const String DefaultSignatureAlgorithm = "SHA256WithRSA";

        private static TimeSpan DefaultExpirationTime
        {
            get
            {
                return Types.Time.Year.One;
            }
        }

        public static X509Certificate2RootContainer RegisterTrustedSelfSignedCertificate(String subject, String issuer)
        {
            return RegisterTrustedSelfSignedCertificate(subject, issuer, DefaultCertificateKeyStrength);
        }

        public static X509Certificate2RootContainer RegisterTrustedSelfSignedCertificate(String subject, String issuer, CertificateKeyStrength strength)
        {
            return RegisterTrustedSelfSignedCertificate(subject, issuer, DefaultExpirationTime, strength);
        }

        public static X509Certificate2RootContainer RegisterTrustedSelfSignedCertificate(String subject, String issuer, TimeSpan expiration)
        {
            return RegisterTrustedSelfSignedCertificate(subject, issuer, expiration, DefaultCertificateKeyStrength);
        }

        public static X509Certificate2RootContainer RegisterTrustedSelfSignedCertificate(String subject, String issuer, TimeSpan expiration, CertificateKeyStrength strength)
        {
            X509Certificate2 root = GenerateAuthorityCertificate(issuer, expiration, strength, out AsymmetricKeyParameter key);
            root.AddCertificateToStore(StoreName.Root, StoreLocation.LocalMachine);
            X509Certificate2 certificate = GenerateSelfSignedCertificate(subject, issuer, key, expiration, strength);
            certificate.AddCertificateToStore(StoreName.My, StoreLocation.LocalMachine);
            return new X509Certificate2RootContainer(root, certificate);
        }

        public static X509Certificate2 GenerateSelfSignedCertificate(String subject, String issuer, AsymmetricKeyParameter key)
        {
            return GenerateSelfSignedCertificate(subject, issuer, key, DefaultCertificateKeyStrength);
        }
        
        public static X509Certificate2 GenerateSelfSignedCertificate(String subject, String issuer, AsymmetricKeyParameter key, CertificateKeyStrength strength)
        {
            return GenerateSelfSignedCertificate(subject, issuer, key, DefaultExpirationTime, strength);
        }

        public static X509Certificate2 GenerateSelfSignedCertificate(String subject, String issuer, AsymmetricKeyParameter key, TimeSpan expiration)
        {
            return GenerateSelfSignedCertificate(subject, issuer, key, expiration, DefaultCertificateKeyStrength);
        }

        public static X509Certificate2 GenerateSelfSignedCertificate(String subject, String issuer, AsymmetricKeyParameter key, TimeSpan expiration, CertificateKeyStrength strength)
        {
            if (String.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(subject));
            }

            if (String.IsNullOrEmpty(issuer))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(issuer));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (expiration.TotalDays > Types.Time.Year.Millennium.TotalDays)
            {
                throw new ArgumentOutOfRangeException(nameof(expiration), expiration, "Expiration must be less than a millennium.");
            }
            
            if (!EnumUtilities.ContainsValue(strength))
            {
                throw new ArgumentOutOfRangeException(nameof(strength), strength, "Invalid key strength");
            }
            
            if (!subject.StartsWith("CN="))
            {
                subject = "CN=" + subject;
            }
            
            if (!issuer.StartsWith("CN="))
            {
                issuer = "CN=" + issuer;
            }

            SecureRandom random = BouncyCastleUtilities.SecureRandom;
            X509V3CertificateGenerator generator = BouncyCastleUtilities.CertificateGenerator;

            BouncyBigIntegerWrapper serial = BouncyBigIntegerWrapper.CreateRandomInRange(BigInteger.One, Int64.MaxValue, random);
            generator.SetSerialNumber(serial);

            generator.SetIssuerDN(new X509Name(issuer));
            generator.SetSubjectDN(new X509Name(subject));

            DateTime current = DateTime.UtcNow.Date;
            generator.SetNotBefore(current);
            generator.SetNotAfter(current.Add(expiration));

            RsaKeyPairGenerator rsa = BouncyCastleUtilities.CreateRsaKeyPairGenerator(random, strength);
            AsymmetricCipherKeyPair cipher = rsa.GenerateKeyPair();

            generator.SetPublicKey(cipher.Public);
            
            Asn1SignatureFactory factory = new Asn1SignatureFactory(DefaultSignatureAlgorithm, key, random);
            Org.BouncyCastle.X509.X509Certificate x509 = generator.Generate(factory);
            PrivateKeyInfo info = PrivateKeyInfoFactory.CreatePrivateKeyInfo(cipher.Private);
            
            using X509Certificate2 certificate = new X509Certificate2(x509.GetEncoded());
            Asn1Sequence sequence = (Asn1Sequence) Asn1Object.FromByteArray(info.PrivateKeyData.GetDerEncoded());
            RsaPrivateCrtKeyParameters parameters = new RsaPrivateCrtKeyParameters(RsaPrivateKeyStructure.GetInstance(sequence));
            
            using RSA instance = DotNetUtilities.ToRSA(parameters);
            return certificate.CopyWithPrivateKey(instance);
        }

        public static X509Certificate2 GenerateAuthorityCertificate(String subject, out AsymmetricKeyParameter key)
        {
            return GenerateAuthorityCertificate(subject, DefaultCertificateKeyStrength, out key);
        }

        public static X509Certificate2 GenerateAuthorityCertificate(String subject, CertificateKeyStrength strength, out AsymmetricKeyParameter key)
        {
            return GenerateAuthorityCertificate(subject, DefaultExpirationTime, strength, out key);
        }

        public static X509Certificate2 GenerateAuthorityCertificate(String subject, TimeSpan expiration, out AsymmetricKeyParameter key)
        {
            return GenerateAuthorityCertificate(subject, expiration, DefaultCertificateKeyStrength, out key);
        }

        public static X509Certificate2 GenerateAuthorityCertificate(String subject, TimeSpan expiration, CertificateKeyStrength strength, out AsymmetricKeyParameter key)
        {
            if (String.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(subject));
            }

            if (expiration.TotalDays > Types.Time.Year.Millennium.TotalDays)
            {
                throw new ArgumentOutOfRangeException(nameof(expiration), expiration, "Expiration must be less than a millennium.");
            }

            if (!EnumUtilities.ContainsValue(strength))
            {
                throw new ArgumentOutOfRangeException(nameof(strength), strength, "Invalid key strength");
            }
            
            if (!subject.StartsWith("CN="))
            {
                subject = "CN=" + subject;
            }

            SecureRandom random = new SecureRandom(new CryptoApiRandomGenerator());
            X509V3CertificateGenerator generator = new X509V3CertificateGenerator();

            BouncyBigIntegerWrapper serial = BouncyBigIntegerWrapper.CreateRandomInRange(BigInteger.One, Int64.MaxValue, random);
            generator.SetSerialNumber(serial);

            X509Name name = new X509Name(subject);
            generator.SetIssuerDN(name);
            generator.SetSubjectDN(name);

            DateTime current = DateTime.UtcNow.Date;
            generator.SetNotBefore(current);
            generator.SetNotAfter(current.Add(expiration));

            RsaKeyPairGenerator rsa = BouncyCastleUtilities.CreateRsaKeyPairGenerator(random, strength);
            AsymmetricCipherKeyPair cipher = rsa.GenerateKeyPair();
            
            generator.SetPublicKey(cipher.Public);
            
            Asn1SignatureFactory factory = new Asn1SignatureFactory(DefaultSignatureAlgorithm, cipher.Private, random);
            Org.BouncyCastle.X509.X509Certificate x509 = generator.Generate(factory);
            X509Certificate2 certificate = new X509Certificate2(x509.GetEncoded());
            key = cipher.Private;
            return certificate;
        }
    }
}