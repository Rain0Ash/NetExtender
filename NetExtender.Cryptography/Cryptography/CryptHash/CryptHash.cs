// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Crypto.CryptHash.Common;
using NetExtender.Crypto.CryptHash.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Crypto.CryptHash
{
    public class CryptHash : ICryptHash
    {
        public HashType HashType { get; }
        
        [NotNull]
        public Byte[] DefaultFirstSalt { get; }
        
        [NotNull]
        public Byte[] DefaultLastSalt { get; }
        
        [NotNull]
        public Byte[] DefaultFirstPepper { get; }
        
        [NotNull]
        public Byte[] DefaultLastPepper { get; }
        
        [NotNull]
        public UInt16 DefaultIterations { get; }

        public CryptHash(HashType type, UInt16 iterations = 1)
            : this(type, null, iterations)
        {
        }

        public CryptHash(HashType type, Byte[] salt, UInt16 iterations = 1)
            : this(type, salt, null, iterations)
        {
        }

        public CryptHash(HashType type, Byte[] salt, Byte[] pepper, UInt16 iterations = 1)
            : this(type, salt, salt, pepper, pepper, iterations)
        {
        }

        public CryptHash(HashType type, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations = 1)
        {
            HashType = type;
            
            DefaultFirstSalt = lsalt ?? Array.Empty<Byte>();
            DefaultLastSalt = rsalt ?? Array.Empty<Byte>();
            
            DefaultFirstPepper = lpepper ?? Array.Empty<Byte>();
            DefaultLastPepper = rpepper ?? Array.Empty<Byte>();
            
            DefaultIterations = iterations;
        }

        public CryptHash(HashType type, CryptHashParameters parameters)
            : this(type, parameters.FirstSalt, parameters.LastSalt, parameters.FirstPepper, parameters.LastPepper, parameters.Iterations)
        {
        }
        
        public Byte[] Hashing(String value)
        {
            return Hashing(value.ToBytes());
        }

        public Byte[] Hashing(String value, Byte[] salt)
        {
            return Hashing(value.ToBytes(), salt);
        }

        public Byte[] Hashing(String value, Byte[] salt, UInt16 iterations)
        {
            return Hashing(value.ToBytes(), salt, iterations);
        }

        public Byte[] Hashing(String value, Byte[] salt, Byte[] pepper)
        {
            return Hashing(value.ToBytes(), salt, pepper);
        }

        public Byte[] Hashing(String value, Byte[] salt, Byte[] pepper, UInt16 iterations)
        {
            return Hashing(value.ToBytes(), salt, pepper, iterations);
        }

        public Byte[] Hashing(String value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper)
        {
            return Hashing(value.ToBytes(), lsalt, rsalt, lpepper, rpepper);
        }

        public Byte[] Hashing(String value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations)
        {
            return Hashing(value.ToBytes(), lsalt, rsalt, lpepper, rpepper, iterations);
        }

        public Byte[] Hashing(String value, CryptHashParameters parameters)
        {
            return Hashing(value.ToBytes(), parameters);
        }

        public Byte[] Hashing(Byte[] value)
        {
            return Hashing(value, DefaultFirstSalt, DefaultLastSalt, DefaultFirstPepper, DefaultLastPepper, DefaultIterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] salt)
        {
            return Hashing(value, salt, DefaultIterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] salt, UInt16 iterations)
        {
            return Hashing(value, salt, salt, DefaultFirstPepper, DefaultLastPepper, iterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] salt, Byte[] pepper)
        {
            return Hashing(value, salt, pepper, DefaultIterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] salt, Byte[] pepper, UInt16 iterations)
        {
            return Hashing(value, salt, salt, pepper, pepper, iterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper)
        {
            return Hashing(value, lsalt, rsalt, lpepper, rpepper, DefaultIterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations)
        {
            // ReSharper disable once InvokeAsExtensionMethod
            Byte[] buffer = BufferUtilities.Combine(lpepper, lsalt, value, rsalt, rpepper).Hashing(HashType);

            while (--iterations > 0)
            {
                Cryptography.Hashing(buffer, buffer, HashType);
            }

            return buffer;
        }

        public Byte[] Hashing(Byte[] value, CryptHashParameters parameters)
        {
            return Hashing(value, parameters.FirstSalt, parameters.LastSalt, parameters.FirstPepper, parameters.LastPepper, parameters.Iterations);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, Span<Byte> destination)
        {
            return Hashing(value, destination, out _);
        }
        
        public Boolean Hashing(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, DefaultFirstSalt, DefaultLastSalt, DefaultFirstPepper, DefaultLastPepper, DefaultIterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, Span<Byte> destination)
        {
            return Hashing(value, salt, destination, out _);
        }
        
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, salt, DefaultIterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, UInt16 iterations, Span<Byte> destination)
        {
            return Hashing(value, salt, iterations, destination, out _);
        }
        
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, UInt16 iterations, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, salt, salt, DefaultFirstPepper, DefaultLastPepper, iterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, Span<Byte> destination)
        {
            return Hashing(value, salt, pepper, destination, out _);
        }
        
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, salt, pepper, DefaultIterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, UInt16 iterations, Span<Byte> destination)
        {
            return Hashing(value, salt, pepper, iterations, destination, out _);
        }
        
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, UInt16 iterations, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, salt, salt, pepper, pepper, iterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, Span<Byte> destination)
        {
            return Hashing(value, lsalt, rsalt, lpepper, rpepper, destination, out _);
        }
        
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, lsalt, rsalt, lpepper, rpepper, DefaultIterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, UInt16 iterations, Span<Byte> destination)
        {
            return Hashing(value, lsalt, rsalt, lpepper, rpepper, iterations, destination, out _);
        }
        
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, UInt16 iterations, Span<Byte> destination, out Int32 written)
        {
            if (destination.Length < (UInt16) HashType)
            {
                written = 0;
                return false;
            }
            
            Byte[] buffer = new Byte[value.Length + lsalt.Length + rsalt.Length + lpepper.Length + rpepper.Length];
            Span<Byte> bufferspan = buffer.AsSpan();
            
            Int32 offset = 0;
            
            lpepper.CopyTo(bufferspan.Slice(offset, lpepper.Length));
            lsalt.CopyTo(bufferspan.Slice(offset += lpepper.Length, lsalt.Length));
            value.CopyTo(bufferspan.Slice(offset += lsalt.Length, value.Length));
            rsalt.CopyTo(bufferspan.Slice(offset += value.Length, rsalt.Length));
            // ReSharper disable once RedundantAssignment
            rpepper.CopyTo(bufferspan.Slice(offset += rsalt.Length, rpepper.Length));
            
            Span<Byte> current = stackalloc Byte[(UInt16) HashType];
            
            Boolean successfull = Cryptography.Hashing(buffer, current, out written, HashType);
            
            while (--iterations > 0 && successfull)
            {
                successfull = Cryptography.Hashing(current, current, out written, HashType);
            }

            if (successfull)
            {
                current.CopyTo(destination);
            }
            else
            {
                written = 0;
            }

            return successfull;
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, CryptHashRefParameters parameters, Span<Byte> destination)
        {
            return Hashing(value, parameters, destination, out _);
        }
        
        public Boolean Hashing(ReadOnlySpan<Byte> value, CryptHashRefParameters parameters, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, parameters.FirstSalt, parameters.LastSalt, parameters.FirstPepper, parameters.LastPepper, parameters.Iterations, destination, out written);
        }
    }
}