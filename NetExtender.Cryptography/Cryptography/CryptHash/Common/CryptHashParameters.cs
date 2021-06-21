// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Crypto.CryptHash.Common
{
    public class CryptHashParameters
    {
        public static implicit operator CryptHashRefParameters(CryptHashParameters parameters)
        {
            return new CryptHashRefParameters(parameters.FirstSalt, parameters.LastSalt, parameters.FirstPepper, parameters.LastPepper, parameters.Iterations);
        }
        
        [NotNull]
        public Byte[] FirstSalt { get; }
        
        [NotNull]
        public Byte[] LastSalt { get; }

        [NotNull]
        public Byte[] FirstPepper { get; }

        [NotNull]
        public Byte[] LastPepper { get; }

        public UInt16 Iterations { get; }

        public CryptHashParameters(UInt16 iterations = 1)
            : this(null, iterations)
        {
        }
        
        public CryptHashParameters(Byte[] salt, UInt16 iterations = 1)
            : this(salt, null, iterations)
        {
        }
        
        public CryptHashParameters(Byte[] salt, Byte[] pepper, UInt16 iterations = 1)
            : this(salt, salt, pepper, pepper, iterations)
        {
        }
        
        public CryptHashParameters(Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations = 1)
        {
            FirstSalt = lsalt ?? Array.Empty<Byte>();
            LastSalt = rsalt ?? Array.Empty<Byte>();
            FirstPepper = lpepper ?? Array.Empty<Byte>();
            LastPepper = rpepper ?? Array.Empty<Byte>();
            Iterations = iterations;
        }
    }
}