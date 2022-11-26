// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Cryptography.Hash.Common;
using NetExtender.Cryptography.Hash.Interfaces;
using NetExtender.Utilities.Cryptography;
using NetExtender.Utilities.Types;

namespace NetExtender.Cryptography.Hash
{
    public class CryptographyHash : ICryptographyHash
    {
        public const Int32 DefaultIterations = 1;

        public HashType HashType { get; }
        public Byte[] LeftSalt { get; }
        public Byte[] RightSalt { get; }
        public Byte[] LeftPepper { get; }
        public Byte[] RightPepper { get; }
        public UInt16 Iterations { get; }

        public CryptographyHash(HashType type)
            : this(type, DefaultIterations)
        {
        }

        public CryptographyHash(HashType type, UInt16 iterations)
            : this(type, null, iterations)
        {
        }

        public CryptographyHash(HashType type, Byte[]? salt)
            : this(type, salt, DefaultIterations)
        {
        }

        public CryptographyHash(HashType type, Byte[]? salt, UInt16 iterations)
            : this(type, salt, null, iterations)
        {
        }

        public CryptographyHash(HashType type, Byte[]? salt, Byte[]? pepper)
            : this(type, salt, salt, pepper, pepper, DefaultIterations)
        {
        }

        public CryptographyHash(HashType type, Byte[]? salt, Byte[]? pepper, UInt16 iterations)
            : this(type, salt, salt, pepper, pepper, iterations)
        {
        }

        public CryptographyHash(HashType type, Byte[]? lsalt, Byte[]? rsalt, Byte[]? lpepper, Byte[]? rpepper)
            : this(type, lsalt, rsalt, lpepper, rpepper, DefaultIterations)
        {
        }

        public CryptographyHash(HashType type, Byte[]? lsalt, Byte[]? rsalt, Byte[]? lpepper, Byte[]? rpepper, UInt16 iterations)
        {
            HashType = type;

            LeftSalt = lsalt ?? Array.Empty<Byte>();
            RightSalt = rsalt ?? Array.Empty<Byte>();

            LeftPepper = lpepper ?? Array.Empty<Byte>();
            RightPepper = rpepper ?? Array.Empty<Byte>();

            Iterations = iterations;
        }

        public CryptographyHash(HashType type, CryptographyHashParameters parameters)
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

        public Byte[] Hashing(String value, CryptographyHashParameters parameters)
        {
            return Hashing(value.ToBytes(), parameters);
        }

        public Byte[] Hashing(Byte[] value)
        {
            return Hashing(value, LeftSalt, RightSalt, LeftPepper, RightPepper, Iterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] salt)
        {
            return Hashing(value, salt, Iterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] salt, UInt16 iterations)
        {
            return Hashing(value, salt, salt, LeftPepper, RightPepper, iterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] salt, Byte[] pepper)
        {
            return Hashing(value, salt, pepper, Iterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] salt, Byte[] pepper, UInt16 iterations)
        {
            return Hashing(value, salt, salt, pepper, pepper, iterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper)
        {
            return Hashing(value, lsalt, rsalt, lpepper, rpepper, Iterations);
        }

        public Byte[] Hashing(Byte[] value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations)
        {
            // ReSharper disable once InvokeAsExtensionMethod
            Byte[] buffer = BufferUtilities.Combine(lpepper, lsalt, value, rsalt, rpepper).Hashing(HashType);

            while (--iterations > 0)
            {
                buffer.Hashing(buffer, HashType);
            }

            return buffer;
        }

        public Byte[] Hashing(Byte[] value, CryptographyHashParameters parameters)
        {
            return Hashing(value, parameters.FirstSalt, parameters.LastSalt, parameters.FirstPepper, parameters.LastPepper, parameters.Iterations);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, Span<Byte> destination)
        {
            return Hashing(value, destination, out _);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, LeftSalt, RightSalt, LeftPepper, RightPepper, Iterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, Span<Byte> destination)
        {
            return Hashing(value, salt, destination, out _);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, salt, Iterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, UInt16 iterations, Span<Byte> destination)
        {
            return Hashing(value, salt, iterations, destination, out _);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, UInt16 iterations, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, salt, salt, LeftPepper, RightPepper, iterations, destination, out written);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, Span<Byte> destination)
        {
            return Hashing(value, salt, pepper, destination, out _);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, salt, pepper, Iterations, destination, out written);
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
            return Hashing(value, lsalt, rsalt, lpepper, rpepper, Iterations, destination, out written);
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

            Int32 offset = 0;

            lpepper.CopyTo(buffer.Slice(offset, lpepper.Length));
            lsalt.CopyTo(buffer.Slice(offset += lpepper.Length, lsalt.Length));
            value.CopyTo(buffer.Slice(offset += lsalt.Length, value.Length));
            rsalt.CopyTo(buffer.Slice(offset += value.Length, rsalt.Length));
            // ReSharper disable once RedundantAssignment
            rpepper.CopyTo(buffer.Slice(offset += rsalt.Length, rpepper.Length));

            Span<Byte> current = stackalloc Byte[(UInt16) HashType];

            Boolean successfull = buffer.Hashing(current, HashType, out written);

            while (--iterations > 0 && successfull)
            {
                successfull = current.Hashing(current, HashType, out written);
            }

            if (successfull)
            {
                current.CopyTo(destination);
                return true;
            }

            written = 0;
            return false;
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, CryptographyHashRefParameters parameters, Span<Byte> destination)
        {
            return Hashing(value, parameters, destination, out _);
        }

        public Boolean Hashing(ReadOnlySpan<Byte> value, CryptographyHashRefParameters parameters, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, parameters.FirstSalt, parameters.LastSalt, parameters.FirstPepper, parameters.LastPepper, parameters.Iterations, destination, out written);
        }
    }
}