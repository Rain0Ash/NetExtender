// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography.X509Certificates;
using NetExtender.JWT.Algorithms.Interfaces;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public abstract partial class Factory
        {
            public sealed class RSA : JWT
            {
                private Func<X509Certificate2>? Certificate { get; }
                private System.Security.Cryptography.RSA? Public { get; }
                private System.Security.Cryptography.RSA? Private { get; }

                public RSA(System.Security.Cryptography.RSA @public)
                {
                    Public = @public ?? throw new ArgumentNullException(nameof(@public));
                }

                public RSA(System.Security.Cryptography.RSA @public, System.Security.Cryptography.RSA @private)
                    : this(@public)
                {
                    Private = @private ?? throw new ArgumentNullException(nameof(@private));
                }

                public RSA(Func<X509Certificate2> factory)
                {
                    Certificate = factory ?? throw new ArgumentNullException(nameof(factory));
                }

                protected override IJWTAlgorithm Create(JWTAlgorithmType algorithm)
                {
                    return algorithm switch
                    {
                        JWTAlgorithmType.RS256 => CreateRS256Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.RS384 => CreateRS384Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.RS512 => CreateRS512Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.RS1024 => CreateRS1024Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.RS2048 => CreateRS2048Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.RS4096 => CreateRS4096Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.HS256 or JWTAlgorithmType.HS384 or JWTAlgorithmType.HS512 => throw new NotSupportedException($"For algorithm '{algorithm}' please use an instance of '{nameof(JWT)}.{nameof(Factory)}.{nameof(HMACSHA)}'."),
                        JWTAlgorithmType.ES256 or JWTAlgorithmType.ES384 or JWTAlgorithmType.ES512 => throw new NotSupportedException($"For algorithm '{algorithm}' please use an instance of '{nameof(JWT)}.{nameof(Factory)}.{nameof(ECDSA)}'."),
                        _ => throw new NotSupportedException($"For algorithm '{algorithm}' please use the appropriate factory by implementing '{nameof(IJWTAlgorithmFactory)}'.")
                    };
                }

                private static RS256Algorithm CreateRS256Algorithm(Func<X509Certificate2>? factory, System.Security.Cryptography.RSA? @public, System.Security.Cryptography.RSA? @private)
                {
                    if (factory is not null)
                    {
                        return new RS256Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new RS256Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new RS256Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }

                private static RS384Algorithm CreateRS384Algorithm(Func<X509Certificate2>? factory, System.Security.Cryptography.RSA? @public, System.Security.Cryptography.RSA? @private)
                {
                    if (factory is not null)
                    {
                        return new RS384Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new RS384Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new RS384Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }

                private static RS512Algorithm CreateRS512Algorithm(Func<X509Certificate2>? factory, System.Security.Cryptography.RSA? @public, System.Security.Cryptography.RSA? @private)
                {
                    if (factory is not null)
                    {
                        return new RS512Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new RS512Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new RS512Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }

                private static RS1024Algorithm CreateRS1024Algorithm(Func<X509Certificate2>? factory, System.Security.Cryptography.RSA? @public, System.Security.Cryptography.RSA? @private)
                {
                    if (factory is not null)
                    {
                        return new RS1024Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new RS1024Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new RS1024Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }

                private static RS2048Algorithm CreateRS2048Algorithm(Func<X509Certificate2>? factory, System.Security.Cryptography.RSA? @public, System.Security.Cryptography.RSA? @private)
                {
                    if (factory is not null)
                    {
                        return new RS2048Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new RS2048Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new RS2048Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }

                private static RS4096Algorithm CreateRS4096Algorithm(Func<X509Certificate2>? factory, System.Security.Cryptography.RSA? @public, System.Security.Cryptography.RSA? @private)
                {
                    if (factory is not null)
                    {
                        return new RS4096Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new RS4096Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new RS4096Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }
            }
        }
    }
}