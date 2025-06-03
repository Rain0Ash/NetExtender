// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.JWT.Algorithms.Interfaces;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public abstract partial class Factory
        {
            public sealed class Any<T> : Factory where T : class, IJWTAlgorithm, new()
            {
                public override IJWTAlgorithm Create()
                {
                    return new T();
                }

                public override IJWTAlgorithm Create(JWTToken jwt, JWTHeaderInfo? header, String? payload)
                {
                    return Create();
                }
            }
        }
    }
}