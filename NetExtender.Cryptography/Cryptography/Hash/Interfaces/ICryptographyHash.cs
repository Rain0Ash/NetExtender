// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Cryptography.Hash.Common;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Cryptography.Hash.Interfaces
{
    public interface ICryptographyHash
    {
        public HashType HashType { get; }
        public Byte[] LeftSalt { get; }
        public Byte[] RightSalt { get; }
        public Byte[] LeftPepper { get; }
        public Byte[] RightPepper { get; }
        public UInt16 Iterations { get; }

        public Byte[] Hashing(String value);
        public Byte[] Hashing(String value, Byte[] salt);
        public Byte[] Hashing(String value, Byte[] salt, UInt16 iterations);
        public Byte[] Hashing(String value, Byte[] salt, Byte[] pepper);
        public Byte[] Hashing(String value, Byte[] salt, Byte[] pepper, UInt16 iterations);
        public Byte[] Hashing(String value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper);
        public Byte[] Hashing(String value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations);
        public Byte[] Hashing(String value, CryptographyHashParameters parameters);

        public Byte[] Hashing(Byte[] value);
        public Byte[] Hashing(Byte[] value, Byte[] salt);
        public Byte[] Hashing(Byte[] value, Byte[] salt, UInt16 iterations);
        public Byte[] Hashing(Byte[] value, Byte[] salt, Byte[] pepper);
        public Byte[] Hashing(Byte[] value, Byte[] salt, Byte[] pepper, UInt16 iterations);
        public Byte[] Hashing(Byte[] value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper);
        public Byte[] Hashing(Byte[] value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations);
        public Byte[] Hashing(Byte[] value, CryptographyHashParameters parameters);

        public Boolean Hashing(ReadOnlySpan<Byte> value, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, Span<Byte> destination, out Int32 written);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, UInt16 iterations, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, UInt16 iterations, Span<Byte> destination, out Int32 written);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, Span<Byte> destination, out Int32 written);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, UInt16 iterations, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> salt, ReadOnlySpan<Byte> pepper, UInt16 iterations, Span<Byte> destination, out Int32 written);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, Span<Byte> destination, out Int32 written);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, UInt16 iterations, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, ReadOnlySpan<Byte> lsalt, ReadOnlySpan<Byte> rsalt, ReadOnlySpan<Byte> lpepper, ReadOnlySpan<Byte> rpepper, UInt16 iterations, Span<Byte> destination, out Int32 written);
        public Boolean Hashing(ReadOnlySpan<Byte> value, CryptographyHashRefParameters parameters, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, CryptographyHashRefParameters parameters, Span<Byte> destination, out Int32 written);
    }
}