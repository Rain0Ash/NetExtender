// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public abstract class CertificateAlgorithm<T> : JWTAlgorithm, IJWTCertificateAlgorithm where T : class
        {
            private Boolean? _lateinit;
            
            private T? _public;
            protected T Public
            {
                get
                {
                    return _public ?? _lateinit switch
                    {
                        null => _public!,
                        true => throw new NotInitializedException("Algorithm is not initialized."),
                        false => throw new InvalidOperationException("Can't verify data without private key.")
                    };
                }
            }

            protected Boolean HasPrivateKey
            {
                get
                {
                    return _private is not null;
                }
            }

            private T? _private;
            protected T? Private
            {
                get
                {
                    return _private ?? _lateinit switch
                    {
                        null => _private,
                        true => throw new NotInitializedException("Algorithm is not initialized."),
                        false => throw new InvalidOperationException("Can't sign data without private key.")
                    };
                }
            }

            protected T VerifyPrivate
            {
                get
                {
                    if (Private is null || _private is null)
                    {
                        throw new InvalidOperationException("Can't sign data without private key.");
                    }

                    return _private!;
                }
            }

            protected CertificateAlgorithm()
            {
                _lateinit = true;
            }

            protected CertificateAlgorithm(T @public)
            {
                _public = @public ?? throw new ArgumentNullException(nameof(@public));
                _private = null;
                _lateinit = false;
            }

            protected CertificateAlgorithm(T @public, T @private)
                : this(@public)
            {
                _private = @private ?? throw new ArgumentNullException(nameof(@private));
            }

            public virtual void ApplyCertificate(X509Certificate2 certificate)
            {
                if (certificate is null)
                {
                    throw new ArgumentNullException(nameof(certificate));
                }

                if (_lateinit is not true)
                {
                    throw new AlreadyInitializedException("Algorithm is already initialized.");
                }

                _public = GetPublicKey(certificate) ?? throw new ArgumentException("Certificate must have public key.");
                _private = GetPrivateKey(certificate);
                _lateinit = null;
            }

            protected abstract T? GetPublicKey(X509Certificate2 certificate);
            protected abstract T? GetPrivateKey(X509Certificate2 certificate);

            public abstract Boolean Verify(ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature);
            public abstract Boolean Verify(Byte[] source, Byte[] signature);
            public abstract Byte[] Sign(Byte[] source);

            public ValueTask<Byte[]?> SignAsync(Byte[] source)
            {
                return SignAsync(source, CancellationToken.None);
            }
            
            public abstract ValueTask<Byte[]?> SignAsync(Byte[] source, CancellationToken token);

            public Boolean TrySign(ReadOnlySpan<Byte> source, ref Span<Byte> destination)
            {
                if (!TrySign(source, destination, out Int32 written))
                {
                    return false;
                }

                destination = destination.Slice(written);
                return true;
            }
            
            public abstract Boolean TrySign(ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written);

            public override Byte[] Sign(JWTKey key, Byte[] source)
            {
                return Sign(source);
            }

            public override Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
            {
                return TrySign(source, destination, out written);
            }

            public override Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
            {
                return TrySign(source, destination, out written);
            }
        }
    }
}