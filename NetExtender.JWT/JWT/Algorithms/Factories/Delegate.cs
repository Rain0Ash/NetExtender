// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.JWT.Algorithms.Interfaces;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public abstract partial class Factory
        {
            public delegate IJWTAlgorithm? Handler(JWTToken jwt, JWTHeaderInfo? header, String? payload);

            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            public sealed class Delegate : Factory
            {
                private Handler Factory { get; }

                public Delegate(IJWTAlgorithm algorithm) :
                    this(algorithm is not null ? () => algorithm : throw new ArgumentNullException(nameof(algorithm)))
                {
                }

                public Delegate(IJWTAlgorithmFactory factory) :
                    this(factory is not null ? new Handler(factory.Create!) : throw new ArgumentNullException(nameof(factory)))
                {
                }

                public Delegate(Func<IJWTAlgorithm> factory)
                    : this(factory is not null ? (_, _, _) => factory() : throw new ArgumentNullException(nameof(factory)))
                {
                }

                public Delegate(Handler factory)
                {
                    Factory = factory ?? throw new ArgumentNullException(nameof(factory));
                }

                public override IJWTAlgorithm? Create()
                {
                    return Factory(default, default, default);
                }

                public override IJWTAlgorithm Create(JWTToken jwt, JWTHeaderInfo header, String payload)
                {
                    return Factory(jwt, header, payload) ?? throw new InvalidOperationException("Unable to create algorithm.");
                }
            }

            public sealed class Instance : Factory
            {
                private IJWTAlgorithm Algorithm { get; }
                
                public Instance(IJWTAlgorithm algorithm)
                {
                    Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
                }
                
                public override IJWTAlgorithm Create()
                {
                    return Algorithm;
                }

                public override IJWTAlgorithm Create(JWTToken jwt, JWTHeaderInfo header, String payload)
                {
                    return Algorithm;
                }
            }
        }
    }
}
