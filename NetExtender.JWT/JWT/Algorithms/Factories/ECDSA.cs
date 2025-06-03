// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public abstract partial class Factory
        {
            public sealed class ECDSA : JWT
            {
                private Func<X509Certificate2>? Certificate { get; }
                private ECDsa? Public { get; }
                private ECDsa? Private { get; }

                public ECDSA(ECDsa @public)
                {
                    Public = @public ?? throw new ArgumentNullException(nameof(@public));
                }

                public ECDSA(ECDsa @public, ECDsa @private)
                    : this(@public)
                {
                    Private = @private ?? throw new ArgumentNullException(nameof(@private));
                }

                public ECDSA(Func<X509Certificate2> factory)
                {
                    Certificate = factory ?? throw new ArgumentNullException(nameof(factory));
                }

                protected override IJWTAlgorithm Create(JWTAlgorithmType algorithm)
                {
                    return algorithm switch
                    {
                        JWTAlgorithmType.ES256 => CreateES256Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.ES384 => CreateES384Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.ES512 => CreateES512Algorithm(Certificate, Public, Private),
                        JWTAlgorithmType.HS256 or JWTAlgorithmType.HS384 or JWTAlgorithmType.HS512 => throw new NotSupportedException($"For algorithm '{algorithm}' please use an instance of '{nameof(JWT)}.{nameof(Factory)}.{nameof(HMACSHA)}'."),
                        JWTAlgorithmType.RS256 or JWTAlgorithmType.RS384 or JWTAlgorithmType.RS512 => throw new NotSupportedException($"For algorithm '{algorithm}' please use an instance of '{nameof(JWT)}.{nameof(Factory)}.{nameof(RSA)}'."),
                        _ => throw new EnumUndefinedOrNotSupportedException<JWTAlgorithmType>(algorithm, nameof(algorithm), $"For algorithm '{algorithm}' please use the appropriate factory by implementing '{nameof(IJWTAlgorithmFactory)}'.")
                    };
                }

                private static IJWTAlgorithm CreateES256Algorithm(Func<X509Certificate2>? factory, ECDsa? @public, ECDsa? @private)
                {
                    if (factory is not null)
                    {
                        return new ES256Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new ES256Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new ES256Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }

                private static IJWTAlgorithm CreateES384Algorithm(Func<X509Certificate2>? factory, ECDsa? @public, ECDsa? @private)
                {
                    if (factory is not null)
                    {
                        return new ES384Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new ES384Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new ES384Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }

                private static IJWTAlgorithm CreateES512Algorithm(Func<X509Certificate2>? factory, ECDsa? @public, ECDsa? @private)
                {
                    if (factory is not null)
                    {
                        return new ES512Algorithm(factory.Invoke());
                    }

                    if (@public is not null && @private is not null)
                    {
                        return new ES512Algorithm(@public, @private);
                    }

                    if (@public is not null)
                    {
                        return new ES512Algorithm(@public);
                    }

                    throw new JWTFactoryException();
                }
            }
        }
    }
}