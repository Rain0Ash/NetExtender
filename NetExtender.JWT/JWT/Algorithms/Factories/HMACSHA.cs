// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public abstract partial class Factory
        {
            public sealed class HMACSHA : JWT
            {
                protected override IJWTAlgorithm Create(JWTAlgorithmType algorithm)
                {
                    return algorithm switch
                    {
                        JWTAlgorithmType.HS256 => new HMACSHA256Algorithm(),
                        JWTAlgorithmType.HS384 => new HMACSHA384Algorithm(),
                        JWTAlgorithmType.HS512 => new HMACSHA512Algorithm(),
                        JWTAlgorithmType.RS256 or JWTAlgorithmType.RS384 or JWTAlgorithmType.RS512 => throw new NotSupportedException($"For algorithm '{algorithm}' please use an instance of '{nameof(JWT)}.{nameof(Factory)}.{nameof(RSA)}'."),
                        JWTAlgorithmType.ES256 or JWTAlgorithmType.ES384 or JWTAlgorithmType.ES512 => throw new NotSupportedException($"For algorithm '{algorithm}' please use an instance of '{nameof(JWT)}.{nameof(Factory)}.{nameof(ECDSA)}'."),
                        _ => throw new EnumUndefinedOrNotSupportedException<JWTAlgorithmType>(algorithm, nameof(algorithm), $"For algorithm '{algorithm}' please use the appropriate factory by implementing '{nameof(IJWTAlgorithmFactory)}'.")
                    };
                }
            }
        }
    }
}