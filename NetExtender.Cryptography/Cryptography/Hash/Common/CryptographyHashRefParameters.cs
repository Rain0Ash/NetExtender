// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Cryptography.Hash.Common
{
    public readonly ref struct CryptographyHashRefParameters
    {
        public static implicit operator CryptographyHashParameters(CryptographyHashRefParameters parameters)
        {
            return new CryptographyHashParameters(parameters.FirstSaltArray, parameters.LastSaltArray, parameters.FirstPepperArray, parameters.LastPepperArray, parameters.Iterations);
        }

        public ReadOnlySpan<Byte> FirstSalt { get; }

        public Byte[] FirstSaltArray
        {
            get
            {
                return FirstSalt.ToArray();
            }
        }

        public ReadOnlySpan<Byte> LastSalt { get; }

        public Byte[] LastSaltArray
        {
            get
            {
                return LastSalt.ToArray();
            }
        }

        public ReadOnlySpan<Byte> FirstPepper { get; }

        public Byte[] FirstPepperArray
        {
            get
            {
                return FirstPepper.ToArray();
            }
        }

        public ReadOnlySpan<Byte> LastPepper { get; }

        public Byte[] LastPepperArray
        {
            get
            {
                return LastPepper.ToArray();
            }
        }

        public UInt16 Iterations { get; }

        public CryptographyHashRefParameters(ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, UInt16 iterations = 1)
            : this(salt, salt, pepper, pepper, iterations)
        {
        }

        public CryptographyHashRefParameters(ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, UInt16 iterations = 1)
        {
            FirstSalt = lsalt;
            LastSalt = rsalt;
            FirstPepper = lpepper;
            LastPepper = rpepper;
            Iterations = iterations;
        }
    }
}