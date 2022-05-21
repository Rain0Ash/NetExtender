// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;
using Md5 = System.Security.Cryptography.MD5;

namespace NetExtender.Crypto
{
    public enum HashType : Byte
    {
        Default,
        CRC8,
        CRC16,
        CRC32,
        CRC64,
        MD5,
        SHA1,
        SHA224,
        SHA256,
        SHA384,
        SHA512
    }

    public static partial class Cryptography
    {
        public const HashType DefaultHashType = HashType.SHA256;

        public static Int32 Size(this HashType type)
        {
            return type switch
            {
                HashType.Default => Size(DefaultHashType),
                HashType.CRC8 => 1,
                HashType.CRC16 => 2,
                HashType.CRC32 => 4,
                HashType.CRC64 => 8,
                HashType.MD5 => 16,
                HashType.SHA1 => 20,
                HashType.SHA224 => 28,
                HashType.SHA256 => 32,
                HashType.SHA384 => 48,
                HashType.SHA512 => 64,
                _ => (Int32) type
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BitSize(this HashType type)
        {
            return Size(type) * BitUtilities.BitInByte;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Hashing(this String value)
        {
            return Hashing(value, DefaultHashType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Hashing(this String value, HashType type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Hashing(value.ToBytes(), type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Hashing(this Byte[] value)
        {
            return Hashing(value, DefaultHashType);
        }

        public static Byte[] Hashing(this Byte[] value, HashType type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            switch (type)
            {
                case HashType.Default:
                    goto case DefaultHashType;
                case HashType.CRC8:
                    return new[] { Hash.Crc8(value) };
                case HashType.CRC16:
                {
                    UInt16[] array = { Hash.Crc16(value) };
                    return Unsafe.As<UInt16[], Byte[]>(ref array);
                }
                case HashType.CRC32:
                {
                    UInt32[] array = { Hash.Crc32(value) };
                    return Unsafe.As<UInt32[], Byte[]>(ref array);
                }
                case HashType.CRC64:
                {
                    UInt64[] array = { Hash.Crc64(value) };
                    return Unsafe.As<UInt64[], Byte[]>(ref array);
                }
                case HashType.MD5:
                    return Hash.MD5(value);
                case HashType.SHA1:
                    return Hash.Sha1(value);
                case HashType.SHA224:
                    return Hash.Sha224(value);
                case HashType.SHA256:
                    return Hash.Sha256(value);
                case HashType.SHA384:
                    return Hash.Sha384(value);
                case HashType.SHA512:
                    return Hash.Sha512(value);
                default:
                    throw new NotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Byte[] value, Span<Byte> destination)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Hashing((ReadOnlySpan<Byte>) value, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Memory<Byte> value, Span<Byte> destination)
        {
            return Hashing(value.Span, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Span<Byte> value, Span<Byte> destination)
        {
            return Hashing((ReadOnlySpan<Byte>) value, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlyMemory<Byte> value, Span<Byte> destination)
        {
            return Hashing(value.Span, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlySpan<Byte> value, Span<Byte> destination)
        {
            return Hashing(value, destination, DefaultHashType);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Byte[] value, Span<Byte> destination, HashType type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Hashing((ReadOnlySpan<Byte>) value, destination, type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Memory<Byte> value, Span<Byte> destination, HashType type)
        {
            return Hashing(value.Span, destination, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Span<Byte> value, Span<Byte> destination, HashType type)
        {
            return Hashing((ReadOnlySpan<Byte>) value, destination, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlyMemory<Byte> value, Span<Byte> destination, HashType type)
        {
            return Hashing(value.Span, destination, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlySpan<Byte> value, Span<Byte> destination, HashType type)
        {
            return Hashing(value, destination, type, out _);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Byte[] value, Span<Byte> destination, out Int32 written)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Hashing((ReadOnlySpan<Byte>) value, destination, out written);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Memory<Byte> value, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value.Span, destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Span<Byte> value, Span<Byte> destination, out Int32 written)
        {
            return Hashing((ReadOnlySpan<Byte>) value, destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlyMemory<Byte> value, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value.Span, destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, destination, DefaultHashType, out written);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Byte[] value, Span<Byte> destination, HashType type, out Int32 written)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Hashing((ReadOnlySpan<Byte>) value, destination, type, out written);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Memory<Byte> value, Span<Byte> destination, HashType type, out Int32 written)
        {
            return Hashing(value.Span, destination, type, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Span<Byte> value, Span<Byte> destination, HashType type, out Int32 written)
        {
            return Hashing((ReadOnlySpan<Byte>) value, destination, type, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlyMemory<Byte> value, Span<Byte> destination, HashType type, out Int32 written)
        {
            return Hashing(value.Span, destination, type, out written);
        }

        public static Boolean Hashing(this ReadOnlySpan<Byte> value, Span<Byte> destination, HashType type, out Int32 written)
        {
            return type switch
            {
                HashType.Default => Hashing(value, destination, DefaultHashType, out written),
                HashType.CRC8 => Hash.Crc8(value, destination, out written),
                HashType.CRC16 => Hash.Crc16(value, destination, out written),
                HashType.CRC32 => Hash.Crc32(value, destination, out written),
                HashType.CRC64 => Hash.Crc64(value, destination, out written),
                HashType.MD5 => Hash.MD5(value, destination, out written),
                HashType.SHA1 => Hash.Sha1(value, destination, out written),
                HashType.SHA224 => Hash.Sha224(value, destination, out written),
                HashType.SHA256 => Hash.Sha256(value, destination, out written),
                HashType.SHA384 => Hash.Sha384(value, destination, out written),
                HashType.SHA512 => Hash.Sha512(value, destination, out written),
                _ => throw new NotSupportedException()
            };
        }

        public static class Hash
        {
            public static Byte[] Sha1(String value)
            {
                return Sha1(value.ToBytes());
            }

            public static Byte[] Sha1(Byte[] value)
            {
                using SHA1 sha1 = SHA1.Create();
                return sha1.ComputeHash(value);
            }

            public static Boolean Sha1(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha1(value, destination, out _);
            }

            public static Boolean Sha1(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA1 sha1 = SHA1.Create();
                return sha1.TryComputeHash(value, destination, out written);
            }

            public static String Sha1String(String value)
            {
                return Sha1(value).GetStringFromBytes();
            }

            public static String Sha1String(Byte[] value)
            {
                return Sha1(value).GetStringFromBytes();
            }

            public static Byte[] Sha224(String value)
            {
                return Sha224(value.ToBytes());
            }

            public static Byte[] Sha224(Byte[] value)
            {
                using SHA224 sha224 = SHA224.Create();
                return sha224.ComputeHash(value);
            }

            public static Boolean Sha224(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha224(value, destination, out _);
            }

            public static Boolean Sha224(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA224 sha224 = SHA224.Create();
                return sha224.TryComputeHash(value, destination, out written);
            }

            public static String Sha224String(String value)
            {
                return Sha224(value).GetStringFromBytes();
            }

            public static String Sha224String(Byte[] value)
            {
                return Sha224(value).GetStringFromBytes();
            }

            public static Byte[] Sha256(String value)
            {
                return Sha256(value.ToBytes());
            }

            public static Byte[] Sha256(Byte[] value)
            {
                using SHA256 sha256 = SHA256.Create();
                return sha256.ComputeHash(value);
            }

            public static Boolean Sha256(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha256(value, destination, out _);
            }

            public static Boolean Sha256(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA256 sha256 = SHA256.Create();
                return sha256.TryComputeHash(value, destination, out written);
            }

            public static String Sha256String(String value)
            {
                return Sha256(value).GetStringFromBytes();
            }

            public static String Sha256String(Byte[] value)
            {
                return Sha256(value).GetStringFromBytes();
            }

            public static Byte[] Sha384(String value)
            {
                return Sha384(value.ToBytes());
            }

            public static Byte[] Sha384(Byte[] value)
            {
                using SHA384 sha384 = SHA384.Create();
                return sha384.ComputeHash(value);
            }

            public static Boolean Sha384(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha384(value, destination, out _);
            }

            public static Boolean Sha384(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA384 sha384 = SHA384.Create();
                return sha384.TryComputeHash(value, destination, out written);
            }

            public static String Sha384String(String value)
            {
                return Sha384(value).GetStringFromBytes();
            }

            public static String Sha384String(Byte[] value)
            {
                return Sha384(value).GetStringFromBytes();
            }

            public static Byte[] Sha512(String value)
            {
                return Sha512(value.ToBytes());
            }

            public static Byte[] Sha512(Byte[] value)
            {
                using SHA512 sha512 = SHA512.Create();
                return sha512.ComputeHash(value);
            }

            public static Boolean Sha512(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha512(value, destination, out _);
            }

            public static Boolean Sha512(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA512 sha512 = SHA512.Create();
                return sha512.TryComputeHash(value, destination, out written);
            }

            public static String Sha512String(String value)
            {
                return Sha512(value).GetStringFromBytes();
            }

            public static String Sha512String(Byte[] value)
            {
                return Sha512(value).GetStringFromBytes();
            }

            public static Byte[] MD5(String value)
            {
                return MD5(value.ToBytes());
            }

            public static Byte[] MD5(Byte[] value)
            {
                using Md5 md5 = Md5.Create();
                return md5.ComputeHash(value);
            }

            public static Boolean MD5(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return MD5(value, destination, out _);
            }

            public static Boolean MD5(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using Md5 md5 = Md5.Create();
                return md5.TryComputeHash(value, destination, out written);
            }

            public static String MD5String(String value)
            {
                return MD5(value).GetStringFromBytes();
            }

            public static String MD5String(Byte[] value)
            {
                return MD5(value).GetStringFromBytes();
            }

            public static Byte Crc8(String value)
            {
                return Crc8(value.ToBytes());
            }

            public static Byte Crc8(ReadOnlySpan<Byte> value)
            {
                Byte checksum = 0;

                foreach (Byte item in value)
                {
                    checksum = Crc8Table.Table[checksum ^ item];
                }

                return checksum;
            }

            public static Boolean Crc8(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Crc8(value, destination, out _);
            }

            public static Boolean Crc8(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                if (destination.Length < sizeof(Byte))
                {
                    written = 0;
                    return false;
                }

                Byte checksum = Crc8(value);
                destination[0] = checksum;
                written = sizeof(Byte);
                return true;
            }

            public static UInt16 Crc16(String value)
            {
                return Crc16(value.ToBytes());
            }

            public static UInt16 Crc16(ReadOnlySpan<Byte> value)
            {
                UInt16 checksum = 0xFFFF;

                foreach (Byte item in value)
                {
                    checksum = (UInt16) ((checksum << 8) ^ Crc16Table.Table[(checksum >> 8) ^ item]);
                }

                return (UInt16) ~checksum;
            }

            public static Boolean Crc16(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Crc16(value, destination, out _);
            }

            public static Boolean Crc16(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                if (destination.Length < sizeof(UInt16))
                {
                    written = 0;
                    return false;
                }

                UInt16 checksum = Crc16(value);
                destination[0] = (Byte) checksum;
                destination[1] = (Byte) (checksum >> BitUtilities.BitInByte);
                written = sizeof(UInt16);
                return true;
            }

            public static UInt32 Crc32(String value)
            {
                return Crc32(value.ToBytes());
            }

            public static UInt32 Crc32(ReadOnlySpan<Byte> value)
            {
                UInt32 checksum = 0xFFFFFFFF;

                foreach (Byte item in value)
                {
                    checksum = Crc32Table.Table[(checksum ^ item) & 0xFF] ^ (checksum >> 8);
                }

                return ~checksum;
            }

            public static Boolean Crc32(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Crc32(value, destination, out _);
            }

            public static Boolean Crc32(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                if (destination.Length < sizeof(UInt32))
                {
                    written = 0;
                    return false;
                }

                UInt32 checksum = Crc32(value);
                    
                for (written = 0; written < sizeof(UInt32); written++)
                {
                    destination[written] = (Byte) checksum;
                    checksum >>= BitUtilities.BitInByte;
                }
                    
                return true;
            }

            public static UInt64 Crc64(String value)
            {
                return Crc32(value.ToBytes());
            }

            public static UInt64 Crc64(ReadOnlySpan<Byte> value)
            {
                UInt64 checksum = 0;
                
                foreach (Byte item in value)
                {
                    checksum = Crc64Table.Table[(Byte) (checksum ^ item)] ^ (checksum >> 8);
                }

                return checksum;
            }

            public static Boolean Crc64(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Crc64(value, destination, out _);
            }

            public static Boolean Crc64(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                if (destination.Length < sizeof(UInt64))
                {
                    written = 0;
                    return false;
                }

                UInt64 checksum = Crc64(value);
                    
                for (written = 0; written < sizeof(UInt64); written++)
                {
                    destination[written] = (Byte) checksum;
                    checksum >>= BitUtilities.BitInByte;
                }

                return true;
            }

            private static class Crc8Table
            {
                public static Byte[] Table { get; } =
                {
                    0x00, 0x5E, 0xBC, 0xE2, 0x61, 0x3F, 0xDD, 0x83,
                    0xC2, 0x9C, 0x7E, 0x20, 0xA3, 0xFD, 0x1F, 0x41,
                    0x9D, 0xC3, 0x21, 0x7F, 0xFC, 0xA2, 0x40, 0x1E,
                    0x5F, 0x01, 0xE3, 0xBD, 0x3E, 0x60, 0x82, 0xDC,
                    0x23, 0x7D, 0x9F, 0xC1, 0x42, 0x1C, 0xFE, 0xA0,
                    0xE1, 0xBF, 0x5D, 0x03, 0x80, 0xDE, 0x3C, 0x62,
                    0xBE, 0xE0, 0x02, 0x5C, 0xDF, 0x81, 0x63, 0x3D,
                    0x7C, 0x22, 0xC0, 0x9E, 0x1D, 0x43, 0xA1, 0xFF,
                    0x46, 0x18, 0xFA, 0xA4, 0x27, 0x79, 0x9B, 0xC5,
                    0x84, 0xDA, 0x38, 0x66, 0xE5, 0xBB, 0x59, 0x07,
                    0xDB, 0x85, 0x67, 0x39, 0xBA, 0xE4, 0x06, 0x58,
                    0x19, 0x47, 0xA5, 0xFB, 0x78, 0x26, 0xC4, 0x9A,
                    0x65, 0x3B, 0xD9, 0x87, 0x04, 0x5A, 0xB8, 0xE6,
                    0xA7, 0xF9, 0x1B, 0x45, 0xC6, 0x98, 0x7A, 0x24,
                    0xF8, 0xA6, 0x44, 0x1A, 0x99, 0xC7, 0x25, 0x7B,
                    0x3A, 0x64, 0x86, 0xD8, 0x5B, 0x05, 0xE7, 0xB9,
                    0x8C, 0xD2, 0x30, 0x6E, 0xED, 0xB3, 0x51, 0x0F,
                    0x4E, 0x10, 0xF2, 0xAC, 0x2F, 0x71, 0x93, 0xCD,
                    0x11, 0x4F, 0xAD, 0xF3, 0x70, 0x2E, 0xCC, 0x92,
                    0xD3, 0x8D, 0x6F, 0x31, 0xB2, 0xEC, 0x0E, 0x50,
                    0xAF, 0xF1, 0x13, 0x4D, 0xCE, 0x90, 0x72, 0x2C,
                    0x6D, 0x33, 0xD1, 0x8F, 0x0C, 0x52, 0xB0, 0xEE,
                    0x32, 0x6C, 0x8E, 0xD0, 0x53, 0x0D, 0xEF, 0xB1,
                    0xF0, 0xAE, 0x4C, 0x12, 0x91, 0xCF, 0x2D, 0x73,
                    0xCA, 0x94, 0x76, 0x28, 0xAB, 0xF5, 0x17, 0x49,
                    0x08, 0x56, 0xB4, 0xEA, 0x69, 0x37, 0xD5, 0x8B,
                    0x57, 0x09, 0xEB, 0xB5, 0x36, 0x68, 0x8A, 0xD4,
                    0x95, 0xCB, 0x29, 0x77, 0xF4, 0xAA, 0x48, 0x16,
                    0xE9, 0xB7, 0x55, 0x0B, 0x88, 0xD6, 0x34, 0x6A,
                    0x2B, 0x75, 0x97, 0xC9, 0x4A, 0x14, 0xF6, 0xA8,
                    0x74, 0x2A, 0xC8, 0x96, 0x15, 0x4B, 0xA9, 0xF7,
                    0xB6, 0xE8, 0x0A, 0x54, 0xD7, 0x89, 0x6B, 0x35
                };
            }

            private static class Crc16Table
            {
                public static UInt16[] Table { get; } =
                {
                    0x0000, 0xC1C0, 0x81C1, 0x4001, 0x01C3, 0xC003, 0x8002, 0x41C2,
                    0x01C6, 0xC006, 0x8007, 0x41C7, 0x0005, 0xC1C5, 0x81C4, 0x4004,
                    0x01CC, 0xC00C, 0x800D, 0x41CD, 0x000F, 0xC1CF, 0x81CE, 0x400E,
                    0x000A, 0xC1CA, 0x81CB, 0x400B, 0x01C9, 0xC009, 0x8008, 0x41C8,
                    0x01D8, 0xC018, 0x8019, 0x41D9, 0x001B, 0xC1DB, 0x81DA, 0x401A,
                    0x001E, 0xC1DE, 0x81DF, 0x401F, 0x01DD, 0xC01D, 0x801C, 0x41DC,
                    0x0014, 0xC1D4, 0x81D5, 0x4015, 0x01D7, 0xC017, 0x8016, 0x41D6,
                    0x01D2, 0xC012, 0x8013, 0x41D3, 0x0011, 0xC1D1, 0x81D0, 0x4010,
                    0x01F0, 0xC030, 0x8031, 0x41F1, 0x0033, 0xC1F3, 0x81F2, 0x4032,
                    0x0036, 0xC1F6, 0x81F7, 0x4037, 0x01F5, 0xC035, 0x8034, 0x41F4,
                    0x003C, 0xC1FC, 0x81FD, 0x403D, 0x01FF, 0xC03F, 0x803E, 0x41FE,
                    0x01FA, 0xC03A, 0x803B, 0x41FB, 0x0039, 0xC1F9, 0x81F8, 0x4038,
                    0x0028, 0xC1E8, 0x81E9, 0x4029, 0x01EB, 0xC02B, 0x802A, 0x41EA,
                    0x01EE, 0xC02E, 0x802F, 0x41EF, 0x002D, 0xC1ED, 0x81EC, 0x402C,
                    0x01E4, 0xC024, 0x8025, 0x41E5, 0x0027, 0xC1E7, 0x81E6, 0x4026,
                    0x0022, 0xC1E2, 0x81E3, 0x4023, 0x01E1, 0xC021, 0x8020, 0x41E0,
                    0x01A0, 0xC060, 0x8061, 0x41A1, 0x0063, 0xC1A3, 0x81A2, 0x4062,
                    0x0066, 0xC1A6, 0x81A7, 0x4067, 0x01A5, 0xC065, 0x8064, 0x41A4,
                    0x006C, 0xC1AC, 0x81AD, 0x406D, 0x01AF, 0xC06F, 0x806E, 0x41AE,
                    0x01AA, 0xC06A, 0x806B, 0x41AB, 0x0069, 0xC1A9, 0x81A8, 0x4068,
                    0x0078, 0xC1B8, 0x81B9, 0x4079, 0x01BB, 0xC07B, 0x807A, 0x41BA,
                    0x01BE, 0xC07E, 0x807F, 0x41BF, 0x007D, 0xC1BD, 0x81BC, 0x407C,
                    0x01B4, 0xC074, 0x8075, 0x41B5, 0x0077, 0xC1B7, 0x81B6, 0x4076,
                    0x0072, 0xC1B2, 0x81B3, 0x4073, 0x01B1, 0xC071, 0x8070, 0x41B0,
                    0x0050, 0xC190, 0x8191, 0x4051, 0x0193, 0xC053, 0x8052, 0x4192,
                    0x0196, 0xC056, 0x8057, 0x4197, 0x0055, 0xC195, 0x8194, 0x4054,
                    0x019C, 0xC05C, 0x805D, 0x419D, 0x005F, 0xC19F, 0x819E, 0x405E,
                    0x005A, 0xC19A, 0x819B, 0x405B, 0x0199, 0xC059, 0x8058, 0x4198,
                    0x0188, 0xC048, 0x8049, 0x4189, 0x004B, 0xC18B, 0x818A, 0x404A,
                    0x004E, 0xC18E, 0x818F, 0x404F, 0x018D, 0xC04D, 0x804C, 0x418C,
                    0x0044, 0xC184, 0x8185, 0x4045, 0x0187, 0xC047, 0x8046, 0x4186,
                    0x0182, 0xC042, 0x8043, 0x4183, 0x0041, 0xC181, 0x8180, 0x4040
                };
            }

            public static class Crc32Table
            {
                public static UInt32[] Table { get; } =
                {
                    0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419, 0x706AF48F,
                    0xE963A535, 0x9E6495A3, 0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988,
                    0x09B64C2B, 0x7EB17CBD, 0xE7B82D07, 0x90BF1D91, 0x1DB71064, 0x6AB020F2,
                    0xF3B97148, 0x84BE41DE, 0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7,
                    0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC, 0x14015C4F, 0x63066CD9,
                    0xFA0F3D63, 0x8D080DF5, 0x3B6E20C8, 0x4C69105E, 0xD56041E4, 0xA2677172,
                    0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B, 0x35B5A8FA, 0x42B2986C,
                    0xDBBBC9D6, 0xACBCF940, 0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59,
                    0x26D930AC, 0x51DE003A, 0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423,
                    0xCFBA9599, 0xB8BDA50F, 0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
                    0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D, 0x76DC4190, 0x01DB7106,
                    0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F, 0x9FBFE4A5, 0xE8B8D433,
                    0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818, 0x7F6A0DBB, 0x086D3D2D,
                    0x91646C97, 0xE6635C01, 0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E,
                    0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457, 0x65B0D9C6, 0x12B7E950,
                    0x8BBEB8EA, 0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65,
                    0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2, 0x4ADFA541, 0x3DD895D7,
                    0xA4D1C46D, 0xD3D6F4FB, 0x4369E96A, 0x346ED9FC, 0xAD678846, 0xDA60B8D0,
                    0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9, 0x5005713C, 0x270241AA,
                    0xBE0B1010, 0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
                    0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17, 0x2EB40D81,
                    0xB7BD5C3B, 0xC0BA6CAD, 0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A,
                    0xEAD54739, 0x9DD277AF, 0x04DB2615, 0x73DC1683, 0xE3630B12, 0x94643B84,
                    0x0D6D6A3E, 0x7A6A5AA8, 0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1,
                    0xF00F9344, 0x8708A3D2, 0x1E01F268, 0x6906C2FE, 0xF762575D, 0x806567CB,
                    0x196C3671, 0x6E6B06E7, 0xFED41B76, 0x89D32BE0, 0x10DA7A5A, 0x67DD4ACC,
                    0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5, 0xD6D6A3E8, 0xA1D1937E,
                    0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,
                    0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55,
                    0x316E8EEF, 0x4669BE79, 0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
                    0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F, 0xC5BA3BBE, 0xB2BD0B28,
                    0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7, 0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D,
                    0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A, 0x9C0906A9, 0xEB0E363F,
                    0x72076785, 0x05005713, 0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38,
                    0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21, 0x86D3D2D4, 0xF1D4E242,
                    0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777,
                    0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C, 0x8F659EFF, 0xF862AE69,
                    0x616BFFD3, 0x166CCF45, 0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2,
                    0xA7672661, 0xD06016F7, 0x4969474D, 0x3E6E77DB, 0xAED16A4A, 0xD9D65ADC,
                    0x40DF0B66, 0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
                    0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605, 0xCDD70693,
                    0x54DE5729, 0x23D967BF, 0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94,
                    0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D
                };
            }

            private static class Crc64Table
            {
                public static UInt64[] Table { get; } =
                {
                    0x0000000000000000, 0x7AD870C830358979,
                    0xF5B0E190606B12F2, 0x8F689158505E9B8B,
                    0xC038E5739841B68F, 0xBAE095BBA8743FF6,
                    0x358804E3F82AA47D, 0x4F50742BC81F2D04,
                    0xAB28ECB46814FE75, 0xD1F09C7C5821770C,
                    0x5E980D24087FEC87, 0x24407DEC384A65FE,
                    0x6B1009C7F05548FA, 0x11C8790FC060C183,
                    0x9EA0E857903E5A08, 0xE478989FA00BD371,
                    0x7D08FF3B88BE6F81, 0x07D08FF3B88BE6F8,
                    0x88B81EABE8D57D73, 0xF2606E63D8E0F40A,
                    0xBD301A4810FFD90E, 0xC7E86A8020CA5077,
                    0x4880FBD87094CBFC, 0x32588B1040A14285,
                    0xD620138FE0AA91F4, 0xACF86347D09F188D,
                    0x2390F21F80C18306, 0x594882D7B0F40A7F,
                    0x1618F6FC78EB277B, 0x6CC0863448DEAE02,
                    0xE3A8176C18803589, 0x997067A428B5BCF0,
                    0xFA11FE77117CDF02, 0x80C98EBF2149567B,
                    0x0FA11FE77117CDF0, 0x75796F2F41224489,
                    0x3A291B04893D698D, 0x40F16BCCB908E0F4,
                    0xCF99FA94E9567B7F, 0xB5418A5CD963F206,
                    0x513912C379682177, 0x2BE1620B495DA80E,
                    0xA489F35319033385, 0xDE51839B2936BAFC,
                    0x9101F7B0E12997F8, 0xEBD98778D11C1E81,
                    0x64B116208142850A, 0x1E6966E8B1770C73,
                    0x8719014C99C2B083, 0xFDC17184A9F739FA,
                    0x72A9E0DCF9A9A271, 0x08719014C99C2B08,
                    0x4721E43F0183060C, 0x3DF994F731B68F75,
                    0xB29105AF61E814FE, 0xC849756751DD9D87,
                    0x2C31EDF8F1D64EF6, 0x56E99D30C1E3C78F,
                    0xD9810C6891BD5C04, 0xA3597CA0A188D57D,
                    0xEC09088B6997F879, 0x96D1784359A27100,
                    0x19B9E91B09FCEA8B, 0x636199D339C963F2,
                    0xDF7ADABD7A6E2D6F, 0xA5A2AA754A5BA416,
                    0x2ACA3B2D1A053F9D, 0x50124BE52A30B6E4,
                    0x1F423FCEE22F9BE0, 0x659A4F06D21A1299,
                    0xEAF2DE5E82448912, 0x902AAE96B271006B,
                    0x74523609127AD31A, 0x0E8A46C1224F5A63,
                    0x81E2D7997211C1E8, 0xFB3AA75142244891,
                    0xB46AD37A8A3B6595, 0xCEB2A3B2BA0EECEC,
                    0x41DA32EAEA507767, 0x3B024222DA65FE1E,
                    0xA2722586F2D042EE, 0xD8AA554EC2E5CB97,
                    0x57C2C41692BB501C, 0x2D1AB4DEA28ED965,
                    0x624AC0F56A91F461, 0x1892B03D5AA47D18,
                    0x97FA21650AFAE693, 0xED2251AD3ACF6FEA,
                    0x095AC9329AC4BC9B, 0x7382B9FAAAF135E2,
                    0xFCEA28A2FAAFAE69, 0x8632586ACA9A2710,
                    0xC9622C4102850A14, 0xB3BA5C8932B0836D,
                    0x3CD2CDD162EE18E6, 0x460ABD1952DB919F,
                    0x256B24CA6B12F26D, 0x5FB354025B277B14,
                    0xD0DBC55A0B79E09F, 0xAA03B5923B4C69E6,
                    0xE553C1B9F35344E2, 0x9F8BB171C366CD9B,
                    0x10E3202993385610, 0x6A3B50E1A30DDF69,
                    0x8E43C87E03060C18, 0xF49BB8B633338561,
                    0x7BF329EE636D1EEA, 0x012B592653589793,
                    0x4E7B2D0D9B47BA97, 0x34A35DC5AB7233EE,
                    0xBBCBCC9DFB2CA865, 0xC113BC55CB19211C,
                    0x5863DBF1E3AC9DEC, 0x22BBAB39D3991495,
                    0xADD33A6183C78F1E, 0xD70B4AA9B3F20667,
                    0x985B3E827BED2B63, 0xE2834E4A4BD8A21A,
                    0x6DEBDF121B863991, 0x1733AFDA2BB3B0E8,
                    0xF34B37458BB86399, 0x8993478DBB8DEAE0,
                    0x06FBD6D5EBD3716B, 0x7C23A61DDBE6F812,
                    0x3373D23613F9D516, 0x49ABA2FE23CC5C6F,
                    0xC6C333A67392C7E4, 0xBC1B436E43A74E9D,
                    0x95AC9329AC4BC9B5, 0xEF74E3E19C7E40CC,
                    0x601C72B9CC20DB47, 0x1AC40271FC15523E,
                    0x5594765A340A7F3A, 0x2F4C0692043FF643,
                    0xA02497CA54616DC8, 0xDAFCE7026454E4B1,
                    0x3E847F9DC45F37C0, 0x445C0F55F46ABEB9,
                    0xCB349E0DA4342532, 0xB1ECEEC59401AC4B,
                    0xFEBC9AEE5C1E814F, 0x8464EA266C2B0836,
                    0x0B0C7B7E3C7593BD, 0x71D40BB60C401AC4,
                    0xE8A46C1224F5A634, 0x927C1CDA14C02F4D,
                    0x1D148D82449EB4C6, 0x67CCFD4A74AB3DBF,
                    0x289C8961BCB410BB, 0x5244F9A98C8199C2,
                    0xDD2C68F1DCDF0249, 0xA7F41839ECEA8B30,
                    0x438C80A64CE15841, 0x3954F06E7CD4D138,
                    0xB63C61362C8A4AB3, 0xCCE411FE1CBFC3CA,
                    0x83B465D5D4A0EECE, 0xF96C151DE49567B7,
                    0x76048445B4CBFC3C, 0x0CDCF48D84FE7545,
                    0x6FBD6D5EBD3716B7, 0x15651D968D029FCE,
                    0x9A0D8CCEDD5C0445, 0xE0D5FC06ED698D3C,
                    0xAF85882D2576A038, 0xD55DF8E515432941,
                    0x5A3569BD451DB2CA, 0x20ED197575283BB3,
                    0xC49581EAD523E8C2, 0xBE4DF122E51661BB,
                    0x3125607AB548FA30, 0x4BFD10B2857D7349,
                    0x04AD64994D625E4D, 0x7E7514517D57D734,
                    0xF11D85092D094CBF, 0x8BC5F5C11D3CC5C6,
                    0x12B5926535897936, 0x686DE2AD05BCF04F,
                    0xE70573F555E26BC4, 0x9DDD033D65D7E2BD,
                    0xD28D7716ADC8CFB9, 0xA85507DE9DFD46C0,
                    0x273D9686CDA3DD4B, 0x5DE5E64EFD965432,
                    0xB99D7ED15D9D8743, 0xC3450E196DA80E3A,
                    0x4C2D9F413DF695B1, 0x36F5EF890DC31CC8,
                    0x79A59BA2C5DC31CC, 0x037DEB6AF5E9B8B5,
                    0x8C157A32A5B7233E, 0xF6CD0AFA9582AA47,
                    0x4AD64994D625E4DA, 0x300E395CE6106DA3,
                    0xBF66A804B64EF628, 0xC5BED8CC867B7F51,
                    0x8AEEACE74E645255, 0xF036DC2F7E51DB2C,
                    0x7F5E4D772E0F40A7, 0x05863DBF1E3AC9DE,
                    0xE1FEA520BE311AAF, 0x9B26D5E88E0493D6,
                    0x144E44B0DE5A085D, 0x6E963478EE6F8124,
                    0x21C640532670AC20, 0x5B1E309B16452559,
                    0xD476A1C3461BBED2, 0xAEAED10B762E37AB,
                    0x37DEB6AF5E9B8B5B, 0x4D06C6676EAE0222,
                    0xC26E573F3EF099A9, 0xB8B627F70EC510D0,
                    0xF7E653DCC6DA3DD4, 0x8D3E2314F6EFB4AD,
                    0x0256B24CA6B12F26, 0x788EC2849684A65F,
                    0x9CF65A1B368F752E, 0xE62E2AD306BAFC57,
                    0x6946BB8B56E467DC, 0x139ECB4366D1EEA5,
                    0x5CCEBF68AECEC3A1, 0x2616CFA09EFB4AD8,
                    0xA97E5EF8CEA5D153, 0xD3A62E30FE90582A,
                    0xB0C7B7E3C7593BD8, 0xCA1FC72BF76CB2A1,
                    0x45775673A732292A, 0x3FAF26BB9707A053,
                    0x70FF52905F188D57, 0x0A2722586F2D042E,
                    0x854FB3003F739FA5, 0xFF97C3C80F4616DC,
                    0x1BEF5B57AF4DC5AD, 0x61372B9F9F784CD4,
                    0xEE5FBAC7CF26D75F, 0x9487CA0FFF135E26,
                    0xDBD7BE24370C7322, 0xA10FCEEC0739FA5B,
                    0x2E675FB4576761D0, 0x54BF2F7C6752E8A9,
                    0xCDCF48D84FE75459, 0xB71738107FD2DD20,
                    0x387FA9482F8C46AB, 0x42A7D9801FB9CFD2,
                    0x0DF7ADABD7A6E2D6, 0x772FDD63E7936BAF,
                    0xF8474C3BB7CDF024, 0x829F3CF387F8795D,
                    0x66E7A46C27F3AA2C, 0x1C3FD4A417C62355,
                    0x935745FC4798B8DE, 0xE98F353477AD31A7,
                    0xA6DF411FBFB21CA3, 0xDC0731D78F8795DA,
                    0x536FA08FDFD90E51, 0x29B7D047EFEC8728
                };
            }
        }
    }
}