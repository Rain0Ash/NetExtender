// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using NetExtender.Utilities.Types;
using Md5 = System.Security.Cryptography.MD5;

namespace NetExtender.Crypto
{
    public enum HashType : UInt16
    {
        CRC8 = 1,
        CRC16 = 2,
        CRC32 = 4,
        CRC64 = 8,
        MD5 = 16,
        SHA1 = 20,
        SHA224 = 28,
        SHA256 = 32,
        SHA384 = 48,
        SHA512 = 64
    }

    public static partial class Cryptography
    {
        public static Byte[] Hashing(this String data, HashType type = HashType.SHA256)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return Hashing(data.ToBytes(), type);
        }

        public static Byte[] Hashing(this Byte[] data, HashType type = HashType.SHA256)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return type switch
            {
                HashType.CRC8 => new[] {Hash.Crc8(data)},
                HashType.MD5 => Hash.MD5(data),
                HashType.SHA1 => Hash.Sha1(data),
                HashType.SHA256 => Hash.Sha256(data),
                HashType.SHA384 => Hash.Sha384(data),
                HashType.SHA512 => Hash.Sha512(data),
                _ => throw new NotSupportedException()
            };
        }

        public static Boolean Hashing(this ReadOnlySpan<Byte> data, Span<Byte> destination, HashType type = HashType.SHA256)
        {
            return Hashing(data, destination, out _, type);
        }

        public static Boolean Hashing(this ReadOnlySpan<Byte> data, Span<Byte> destination, out Int32 written, HashType type = HashType.SHA256)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return type switch
            {
                HashType.CRC8 => Hash.Crc8(data, destination, out written),
                HashType.MD5 => Hash.MD5(data, destination, out written),
                HashType.SHA1 => Hash.Sha1(data, destination, out written),
                HashType.SHA256 => Hash.Sha256(data, destination, out written),
                HashType.SHA384 => Hash.Sha384(data, destination, out written),
                HashType.SHA512 => Hash.Sha512(data, destination, out written),
                _ => throw new NotSupportedException()
            };
        }

        public static class Hash
        {
            public static Byte[] Sha1(String data)
            {
                return Sha1(data.ToBytes());
            }

            public static Byte[] Sha1(Byte[] data)
            {
                using SHA1 sha1 = new SHA1Managed();
                return sha1.ComputeHash(data);
            }

            public static Boolean Sha1(ReadOnlySpan<Byte> data, Span<Byte> destination)
            {
                return Sha1(data, destination, out _);
            }

            public static Boolean Sha1(ReadOnlySpan<Byte> data, Span<Byte> destination, out Int32 written)
            {
                using SHA1 sha1 = new SHA1Managed();
                return sha1.TryComputeHash(data, destination, out written);
            }

            public static String Sha1String(String data)
            {
                return Sha1(data).GetStringFromBytes();
            }

            public static String Sha1String(Byte[] data)
            {
                return Sha1(data).GetStringFromBytes();
            }

            public static Byte[] Sha256(String data)
            {
                return Sha256(data.ToBytes());
            }

            public static Byte[] Sha256(Byte[] data)
            {
                using SHA256 sha256 = new SHA256Managed();
                return sha256.ComputeHash(data);
            }

            public static Boolean Sha256(ReadOnlySpan<Byte> data, Span<Byte> destination)
            {
                return Sha256(data, destination, out _);
            }

            public static Boolean Sha256(ReadOnlySpan<Byte> data, Span<Byte> destination, out Int32 written)
            {
                using SHA256 sha256 = new SHA256Managed();
                return sha256.TryComputeHash(data, destination, out written);
            }

            public static String Sha256String(String data)
            {
                return Sha256(data).GetStringFromBytes();
            }

            public static String Sha256String(Byte[] data)
            {
                return Sha256(data).GetStringFromBytes();
            }

            public static Byte[] Sha384(String data)
            {
                return Sha384(data.ToBytes());
            }

            public static Byte[] Sha384(Byte[] data)
            {
                using SHA384 sha384 = new SHA384Managed();
                return sha384.ComputeHash(data);
            }

            public static Boolean Sha384(ReadOnlySpan<Byte> data, Span<Byte> destination)
            {
                return Sha384(data, destination, out _);
            }

            public static Boolean Sha384(ReadOnlySpan<Byte> data, Span<Byte> destination, out Int32 written)
            {
                using SHA384 sha384 = new SHA384Managed();
                return sha384.TryComputeHash(data, destination, out written);
            }

            public static String Sha384String(String data)
            {
                return Sha384(data).GetStringFromBytes();
            }

            public static String Sha384String(Byte[] data)
            {
                return Sha384(data).GetStringFromBytes();
            }

            public static Byte[] Sha512(String data)
            {
                return Sha512(data.ToBytes());
            }

            public static Byte[] Sha512(Byte[] data)
            {
                using SHA512 sha512 = new SHA512Managed();
                return sha512.ComputeHash(data);
            }

            public static Boolean Sha512(ReadOnlySpan<Byte> data, Span<Byte> destination)
            {
                return Sha512(data, destination, out _);
            }

            public static Boolean Sha512(ReadOnlySpan<Byte> data, Span<Byte> destination, out Int32 written)
            {
                using SHA512 sha512 = new SHA512Managed();
                return sha512.TryComputeHash(data, destination, out written);
            }

            public static String Sha512String(String data)
            {
                return Sha512(data).GetStringFromBytes();
            }

            public static String Sha512String(Byte[] data)
            {
                return Sha512(data).GetStringFromBytes();
            }

            public static Byte[] MD5(String data)
            {
                return MD5(data.ToBytes());
            }

            public static Byte[] MD5(Byte[] data)
            {
                using Md5 md5 = Md5.Create();
                return md5.ComputeHash(data);
            }

            public static Boolean MD5(ReadOnlySpan<Byte> data, Span<Byte> destination)
            {
                return MD5(data, destination, out _);
            }

            public static Boolean MD5(ReadOnlySpan<Byte> data, Span<Byte> destination, out Int32 written)
            {
                using Md5 md5 = Md5.Create();
                return md5.TryComputeHash(data, destination, out written);
            }

            public static String MD5String(String data)
            {
                return MD5(data).GetStringFromBytes();
            }

            public static String MD5String(Byte[] data)
            {
                return MD5(data).GetStringFromBytes();
            }

            public static Byte Crc8(String data)
            {
                return Crc8(data.ToBytes());
            }

            public static Byte Crc8(ReadOnlySpan<Byte> data)
            {
                return Crc8(data, data.Length);
            }

            public static Boolean Crc8(ReadOnlySpan<Byte> data, Span<Byte> destination)
            {
                return Crc8(data, destination, out _);
            }

            public static Boolean Crc8(ReadOnlySpan<Byte> data, Span<Byte> destination, out Int32 written)
            {
                if (destination.Length >= 1)
                {
                    Byte crc8 = Crc8(data);
                    destination[0] = crc8;
                    written = 1;
                    return true;
                }

                written = 0;
                return false;
            }

            public static Byte Crc8(ReadOnlySpan<Byte> data, Int32 size)
            {
                UInt32 checksum = 0;

                unchecked
                {
                    for (Int32 i = 0; i <= size - 1; i++)
                    {
                        checksum *= 0x13;
                        checksum += data[i];
                    }

                    return (Byte) checksum;
                }
            }
        }
    }
}