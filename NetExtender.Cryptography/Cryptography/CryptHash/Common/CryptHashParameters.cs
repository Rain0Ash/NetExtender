// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Crypto.CryptHash.Common
{
    public class CryptHashParameters
    {
        public static implicit operator CryptHashRefParameters(CryptHashParameters parameters)
        {
            return new CryptHashRefParameters(parameters.FirstSalt, parameters.LastSalt, parameters.FirstPepper, parameters.LastPepper, parameters.Iterations);
        }
        
        public Byte[] FirstSalt { get; }
        
        public Byte[] LastSalt { get; }

        public Byte[] FirstPepper { get; }

        public Byte[] LastPepper { get; }

        public UInt16 Iterations { get; }

        public CryptHashParameters()
            : this(CryptHash.DefaultIterations)
        {
        }
        
        public CryptHashParameters(UInt16 iterations)
            : this(null, iterations)
        {
        }
        
        public CryptHashParameters(Byte[]? salt)
            : this(salt, CryptHash.DefaultIterations)
        {
        }
        
        public CryptHashParameters(Byte[]? salt, UInt16 iterations)
            : this(salt, null, iterations)
        {
        }
        
        public CryptHashParameters(Byte[]? salt, Byte[]? pepper)
            : this(salt, pepper, CryptHash.DefaultIterations)
        {
        }
        
        public CryptHashParameters(Byte[]? salt, Byte[]? pepper, UInt16 iterations)
            : this(salt, salt, pepper, pepper, iterations)
        {
        }

        public CryptHashParameters(Byte[]? lsalt, Byte[]? rsalt, Byte[]? lpepper, Byte[]? rpepper)
            : this(lsalt, rsalt, lpepper, rpepper, CryptHash.DefaultIterations)
        {
        }

        public CryptHashParameters(Byte[]? lsalt, Byte[]? rsalt, Byte[]? lpepper, Byte[]? rpepper, UInt16 iterations)
        {
            FirstSalt = lsalt ?? Array.Empty<Byte>();
            LastSalt = rsalt ?? Array.Empty<Byte>();
            FirstPepper = lpepper ?? Array.Empty<Byte>();
            LastPepper = rpepper ?? Array.Empty<Byte>();
            Iterations = iterations;
        }
    }
}