// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.JWT.Algorithms.Interfaces
{
    public interface IJWTAlgorithmFactory
    {
        public IJWTAlgorithm? Create();
        public IJWTAlgorithm Create(JWTToken jwt, JWTHeaderInfo header, String payload);
        public ValueTask<IJWTAlgorithm?> CreateAsync();
        public ValueTask<IJWTAlgorithm?> CreateAsync(CancellationToken token);
        public ValueTask<IJWTAlgorithm> CreateAsync(JWTToken jwt, JWTHeaderInfo header, String payload);
        public ValueTask<IJWTAlgorithm> CreateAsync(JWTToken jwt, JWTHeaderInfo header, String payload, CancellationToken token);
    }
}