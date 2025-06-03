// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public abstract partial class Factory : IJWTAlgorithmFactory
        {
            public abstract IJWTAlgorithm? Create();
            public abstract IJWTAlgorithm Create(JWTToken jwt, JWTHeaderInfo header, String payload);

            public ValueTask<IJWTAlgorithm?> CreateAsync()
            {
                return CreateAsync(CancellationToken.None);
            }

            public virtual ValueTask<IJWTAlgorithm?> CreateAsync(CancellationToken token)
            {
                if (token.IsCancellationRequested)
                {
                    return ValueTask.FromCanceled<IJWTAlgorithm?>(token);
                }

                try
                {
                    return ValueTask.FromResult(Create());
                }
                catch (Exception exception)
                {
                    return ValueTask.FromException<IJWTAlgorithm?>(exception);
                }
            }

            public ValueTask<IJWTAlgorithm> CreateAsync(JWTToken jwt, JWTHeaderInfo header, String payload)
            {
                return CreateAsync(jwt, header, payload, CancellationToken.None);
            }

            public virtual ValueTask<IJWTAlgorithm> CreateAsync(JWTToken jwt, JWTHeaderInfo header, String payload, CancellationToken token)
            {
                if (token.IsCancellationRequested)
                {
                    return ValueTask.FromCanceled<IJWTAlgorithm>(token);
                }

                try
                {
                    return ValueTask.FromResult(Create(jwt, header, payload));
                }
                catch (Exception exception)
                {
                    return ValueTask.FromException<IJWTAlgorithm>(exception);
                }
            }

            public abstract class JWT : Factory
            {
                protected abstract IJWTAlgorithm Create(JWTAlgorithmType algorithm);

                public override IJWTAlgorithm? Create()
                {
                    return null;
                }

                public override IJWTAlgorithm Create(JWTToken jwt, JWTHeaderInfo header, String payload)
                {
                    if (header is null)
                    {
                        throw new ArgumentNullException(nameof(header));
                    }

                    String algorithm = header.Algorithm ?? throw new ArgumentNullException(nameof(header));
                    return Create(EnumUtilities.Parse<JWTAlgorithmType>(algorithm));
                }
            }
        }
    }
}