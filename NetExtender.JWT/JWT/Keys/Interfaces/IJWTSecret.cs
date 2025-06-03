// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.JWT
{
    public interface IJWTSecret : IReadOnlyCollection<JWTKey>, IGetter<JWTKey>, IGetter<JWTKeys>, IEquatable<JWTKey>, IEquatable<JWTKeys>, IEquatable<IJWTSecret>
    {
        public JWTKey Key { get; }
        public JWTKeys Keys { get; }
        public Boolean IsEmpty { get; }

        public new JWTKeys Get();
    }
}