// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Crypto.CryptHash.Common;

namespace NetExtender.Crypto.CryptHash.Interfaces
{
    public interface ICryptHash
    {
        public HashType HashType { get; }
        
        [NotNull]
        public Byte[] LeftSalt { get; }
        
        [NotNull]
        public Byte[] RightSalt { get; }
        
        [NotNull]
        public Byte[] LeftPepper { get; }
        
        [NotNull]
        public Byte[] RightPepper { get; }
        
        public UInt16 Iterations { get; }

        public Byte[] Hashing(String value);
        public Byte[] Hashing(String value, Byte[] salt);
        public Byte[] Hashing(String value, Byte[] salt, UInt16 iterations);
        public Byte[] Hashing(String value, Byte[] salt, Byte[] pepper);
        public Byte[] Hashing(String value, Byte[] salt, Byte[] pepper, UInt16 iterations);
        public Byte[] Hashing(String value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper);
        public Byte[] Hashing(String value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations);
        public Byte[] Hashing(String value, CryptHashParameters parameters);
        
        public Byte[] Hashing(Byte[] value);
        public Byte[] Hashing(Byte[] value, Byte[] salt);
        public Byte[] Hashing(Byte[] value, Byte[] salt, UInt16 iterations);
        public Byte[] Hashing(Byte[] value, Byte[] salt, Byte[] pepper);
        public Byte[] Hashing(Byte[] value, Byte[] salt, Byte[] pepper, UInt16 iterations);
        public Byte[] Hashing(Byte[] value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper);
        public Byte[] Hashing(Byte[] value, Byte[] lsalt, Byte[] rsalt, Byte[] lpepper, Byte[] rpepper, UInt16 iterations);
        public Byte[] Hashing(Byte[] value, CryptHashParameters parameters);

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
        public Boolean Hashing(ReadOnlySpan<Byte> value, CryptHashRefParameters parameters, Span<Byte> destination);
        public Boolean Hashing(ReadOnlySpan<Byte> value, CryptHashRefParameters parameters, Span<Byte> destination, out Int32 written);
    }
}