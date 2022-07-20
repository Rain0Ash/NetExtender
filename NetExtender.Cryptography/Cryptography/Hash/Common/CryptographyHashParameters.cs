// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Cryptography.Hash.Common
{
    public class CryptographyHashParameters
    {
        public static implicit operator CryptographyHashRefParameters(CryptographyHashParameters parameters)
        {
            return new CryptographyHashRefParameters(parameters.FirstSalt, parameters.LastSalt, parameters.FirstPepper, parameters.LastPepper, parameters.Iterations);
        }
        
        public Byte[] FirstSalt { get; }
        
        public Byte[] LastSalt { get; }

        public Byte[] FirstPepper { get; }

        public Byte[] LastPepper { get; }

        public UInt16 Iterations { get; }

        public CryptographyHashParameters()
            : this(CryptographyHash.DefaultIterations)
        {
        }
        
        public CryptographyHashParameters(UInt16 iterations)
            : this(null, iterations)
        {
        }
        
        public CryptographyHashParameters(Byte[]? salt)
            : this(salt, CryptographyHash.DefaultIterations)
        {
        }
        
        public CryptographyHashParameters(Byte[]? salt, UInt16 iterations)
            : this(salt, null, iterations)
        {
        }
        
        public CryptographyHashParameters(Byte[]? salt, Byte[]? pepper)
            : this(salt, pepper, CryptographyHash.DefaultIterations)
        {
        }
        
        public CryptographyHashParameters(Byte[]? salt, Byte[]? pepper, UInt16 iterations)
            : this(salt, salt, pepper, pepper, iterations)
        {
        }

        public CryptographyHashParameters(Byte[]? lsalt, Byte[]? rsalt, Byte[]? lpepper, Byte[]? rpepper)
            : this(lsalt, rsalt, lpepper, rpepper, CryptographyHash.DefaultIterations)
        {
        }

        public CryptographyHashParameters(Byte[]? lsalt, Byte[]? rsalt, Byte[]? lpepper, Byte[]? rpepper, UInt16 iterations)
        {
            FirstSalt = lsalt ?? Array.Empty<Byte>();
            LastSalt = rsalt ?? Array.Empty<Byte>();
            FirstPepper = lpepper ?? Array.Empty<Byte>();
            LastPepper = rpepper ?? Array.Empty<Byte>();
            Iterations = iterations;
        }
    }
}