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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Hashing(this String value)
        {
            return Hashing(value, HashType.SHA256);
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
            return Hashing(value, HashType.SHA256);
        }

        public static Byte[] Hashing(this Byte[] value, HashType type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            switch (type)
            {
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
        public static Boolean Hashing(this ReadOnlySpan<Byte> value, Span<Byte> destination)
        {
            return Hashing(value, destination, HashType.SHA256);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlySpan<Byte> value, Span<Byte> destination, HashType type)
        {
            return Hashing(value, destination, out _, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
        {
            return Hashing(value, destination, out written, HashType.SHA256);
        }

        public static Boolean Hashing(this ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written, HashType type)
        {
            return type switch
            {
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
                using SHA1 sha1 = new SHA1Managed();
                return sha1.ComputeHash(value);
            }

            public static Boolean Sha1(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha1(value, destination, out _);
            }

            public static Boolean Sha1(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA1 sha1 = new SHA1Managed();
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
                using SHA224 sha224 = new SHA224Managed();
                return sha224.ComputeHash(value);
            }

            public static Boolean Sha224(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha224(value, destination, out _);
            }

            public static Boolean Sha224(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA224 sha224 = new SHA224Managed();
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
                using SHA256 sha256 = new SHA256Managed();
                return sha256.ComputeHash(value);
            }

            public static Boolean Sha256(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha256(value, destination, out _);
            }

            public static Boolean Sha256(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA256 sha256 = new SHA256Managed();
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
                using SHA384 sha384 = new SHA384Managed();
                return sha384.ComputeHash(value);
            }

            public static Boolean Sha384(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha384(value, destination, out _);
            }

            public static Boolean Sha384(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA384 sha384 = new SHA384Managed();
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
                using SHA512 sha512 = new SHA512Managed();
                return sha512.ComputeHash(value);
            }

            public static Boolean Sha512(ReadOnlySpan<Byte> value, Span<Byte> destination)
            {
                return Sha512(value, destination, out _);
            }

            public static Boolean Sha512(ReadOnlySpan<Byte> value, Span<Byte> destination, out Int32 written)
            {
                using SHA512 sha512 = new SHA512Managed();
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
                if (destination.Length >= sizeof(Byte))
                {
                    Byte checksum = Crc8(value);
                    destination[0] = checksum;
                    written = sizeof(Byte);
                    return true;
                }

                written = 0;
                return false;
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
                if (destination.Length >= sizeof(UInt16))
                {
                    UInt16 checksum = Crc16(value);
                    destination[0] = (Byte) checksum;
                    destination[1] = (Byte) (checksum >> BitUtilities.BitInByte);
                    written = sizeof(UInt16);
                    return true;
                }

                written = 0;
                return false;
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
                if (destination.Length >= sizeof(UInt32))
                {
                    UInt32 checksum = Crc32(value);
                    
                    for (written = 0; written < sizeof(UInt32); written++)
                    {
                        destination[written] = (Byte) checksum;
                        checksum >>= BitUtilities.BitInByte;
                    }
                    
                    return true;
                }

                written = 0;
                return false;
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
                if (destination.Length >= sizeof(UInt64))
                {
                    UInt64 checksum = Crc64(value);
                    
                    for (written = 0; written < sizeof(UInt64); written++)
                    {
                        destination[written] = (Byte) checksum;
                        checksum >>= BitUtilities.BitInByte;
                    }

                    return true;
                }

                written = 0;
                return false;
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
                    0x00000000, 0x77073096, 0xee0e612c, 0x990951ba, 0x076dc419, 0x706af48f,
                    0xe963a535, 0x9e6495a3, 0x0edb8832, 0x79dcb8a4, 0xe0d5e91e, 0x97d2d988,
                    0x09b64c2b, 0x7eb17cbd, 0xe7b82d07, 0x90bf1d91, 0x1db71064, 0x6ab020f2,
                    0xf3b97148, 0x84be41de, 0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7,
                    0x136c9856, 0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9,
                    0xfa0f3d63, 0x8d080df5, 0x3b6e20c8, 0x4c69105e, 0xd56041e4, 0xa2677172,
                    0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b, 0x35b5a8fa, 0x42b2986c,
                    0xdbbbc9d6, 0xacbcf940, 0x32d86ce3, 0x45df5c75, 0xdcd60dcf, 0xabd13d59,
                    0x26d930ac, 0x51de003a, 0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423,
                    0xcfba9599, 0xb8bda50f, 0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924,
                    0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d, 0x76dc4190, 0x01db7106,
                    0x98d220bc, 0xefd5102a, 0x71b18589, 0x06b6b51f, 0x9fbfe4a5, 0xe8b8d433,
                    0x7807c9a2, 0x0f00f934, 0x9609a88e, 0xe10e9818, 0x7f6a0dbb, 0x086d3d2d,
                    0x91646c97, 0xe6635c01, 0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e,
                    0x6c0695ed, 0x1b01a57b, 0x8208f4c1, 0xf50fc457, 0x65b0d9c6, 0x12b7e950,
                    0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3, 0xfbd44c65,
                    0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2, 0x4adfa541, 0x3dd895d7,
                    0xa4d1c46d, 0xd3d6f4fb, 0x4369e96a, 0x346ed9fc, 0xad678846, 0xda60b8d0,
                    0x44042d73, 0x33031de5, 0xaa0a4c5f, 0xdd0d7cc9, 0x5005713c, 0x270241aa,
                    0xbe0b1010, 0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f,
                    0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17, 0x2eb40d81,
                    0xb7bd5c3b, 0xc0ba6cad, 0xedb88320, 0x9abfb3b6, 0x03b6e20c, 0x74b1d29a,
                    0xead54739, 0x9dd277af, 0x04db2615, 0x73dc1683, 0xe3630b12, 0x94643b84,
                    0x0d6d6a3e, 0x7a6a5aa8, 0xe40ecf0b, 0x9309ff9d, 0x0a00ae27, 0x7d079eb1,
                    0xf00f9344, 0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb,
                    0x196c3671, 0x6e6b06e7, 0xfed41b76, 0x89d32be0, 0x10da7a5a, 0x67dd4acc,
                    0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5, 0xd6d6a3e8, 0xa1d1937e,
                    0x38d8c2c4, 0x4fdff252, 0xd1bb67f1, 0xa6bc5767, 0x3fb506dd, 0x48b2364b,
                    0xd80d2bda, 0xaf0a1b4c, 0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55,
                    0x316e8eef, 0x4669be79, 0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236,
                    0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f, 0xc5ba3bbe, 0xb2bd0b28,
                    0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31, 0x2cd99e8b, 0x5bdeae1d,
                    0x9b64c2b0, 0xec63f226, 0x756aa39c, 0x026d930a, 0x9c0906a9, 0xeb0e363f,
                    0x72076785, 0x05005713, 0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0x0cb61b38,
                    0x92d28e9b, 0xe5d5be0d, 0x7cdcefb7, 0x0bdbdf21, 0x86d3d2d4, 0xf1d4e242,
                    0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1, 0x18b74777,
                    0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c, 0x8f659eff, 0xf862ae69,
                    0x616bffd3, 0x166ccf45, 0xa00ae278, 0xd70dd2ee, 0x4e048354, 0x3903b3c2,
                    0xa7672661, 0xd06016f7, 0x4969474d, 0x3e6e77db, 0xaed16a4a, 0xd9d65adc,
                    0x40df0b66, 0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9,
                    0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605, 0xcdd70693,
                    0x54de5729, 0x23d967bf, 0xb3667a2e, 0xc4614ab8, 0x5d681b02, 0x2a6f2b94,
                    0xb40bbe37, 0xc30c8ea1, 0x5a05df1b, 0x2d02ef8d
                };
            }

            private static class Crc64Table
            {
                public static UInt64[] Table { get; } =
                {
                    0x0000000000000000, 0x7ad870c830358979,
                    0xf5b0e190606b12f2, 0x8f689158505e9b8b,
                    0xc038e5739841b68f, 0xbae095bba8743ff6,
                    0x358804e3f82aa47d, 0x4f50742bc81f2d04,
                    0xab28ecb46814fe75, 0xd1f09c7c5821770c,
                    0x5e980d24087fec87, 0x24407dec384a65fe,
                    0x6b1009c7f05548fa, 0x11c8790fc060c183,
                    0x9ea0e857903e5a08, 0xe478989fa00bd371,
                    0x7d08ff3b88be6f81, 0x07d08ff3b88be6f8,
                    0x88b81eabe8d57d73, 0xf2606e63d8e0f40a,
                    0xbd301a4810ffd90e, 0xc7e86a8020ca5077,
                    0x4880fbd87094cbfc, 0x32588b1040a14285,
                    0xd620138fe0aa91f4, 0xacf86347d09f188d,
                    0x2390f21f80c18306, 0x594882d7b0f40a7f,
                    0x1618f6fc78eb277b, 0x6cc0863448deae02,
                    0xe3a8176c18803589, 0x997067a428b5bcf0,
                    0xfa11fe77117cdf02, 0x80c98ebf2149567b,
                    0x0fa11fe77117cdf0, 0x75796f2f41224489,
                    0x3a291b04893d698d, 0x40f16bccb908e0f4,
                    0xcf99fa94e9567b7f, 0xb5418a5cd963f206,
                    0x513912c379682177, 0x2be1620b495da80e,
                    0xa489f35319033385, 0xde51839b2936bafc,
                    0x9101f7b0e12997f8, 0xebd98778d11c1e81,
                    0x64b116208142850a, 0x1e6966e8b1770c73,
                    0x8719014c99c2b083, 0xfdc17184a9f739fa,
                    0x72a9e0dcf9a9a271, 0x08719014c99c2b08,
                    0x4721e43f0183060c, 0x3df994f731b68f75,
                    0xb29105af61e814fe, 0xc849756751dd9d87,
                    0x2c31edf8f1d64ef6, 0x56e99d30c1e3c78f,
                    0xd9810c6891bd5c04, 0xa3597ca0a188d57d,
                    0xec09088b6997f879, 0x96d1784359a27100,
                    0x19b9e91b09fcea8b, 0x636199d339c963f2,
                    0xdf7adabd7a6e2d6f, 0xa5a2aa754a5ba416,
                    0x2aca3b2d1a053f9d, 0x50124be52a30b6e4,
                    0x1f423fcee22f9be0, 0x659a4f06d21a1299,
                    0xeaf2de5e82448912, 0x902aae96b271006b,
                    0x74523609127ad31a, 0x0e8a46c1224f5a63,
                    0x81e2d7997211c1e8, 0xfb3aa75142244891,
                    0xb46ad37a8a3b6595, 0xceb2a3b2ba0eecec,
                    0x41da32eaea507767, 0x3b024222da65fe1e,
                    0xa2722586f2d042ee, 0xd8aa554ec2e5cb97,
                    0x57c2c41692bb501c, 0x2d1ab4dea28ed965,
                    0x624ac0f56a91f461, 0x1892b03d5aa47d18,
                    0x97fa21650afae693, 0xed2251ad3acf6fea,
                    0x095ac9329ac4bc9b, 0x7382b9faaaf135e2,
                    0xfcea28a2faafae69, 0x8632586aca9a2710,
                    0xc9622c4102850a14, 0xb3ba5c8932b0836d,
                    0x3cd2cdd162ee18e6, 0x460abd1952db919f,
                    0x256b24ca6b12f26d, 0x5fb354025b277b14,
                    0xd0dbc55a0b79e09f, 0xaa03b5923b4c69e6,
                    0xe553c1b9f35344e2, 0x9f8bb171c366cd9b,
                    0x10e3202993385610, 0x6a3b50e1a30ddf69,
                    0x8e43c87e03060c18, 0xf49bb8b633338561,
                    0x7bf329ee636d1eea, 0x012b592653589793,
                    0x4e7b2d0d9b47ba97, 0x34a35dc5ab7233ee,
                    0xbbcbcc9dfb2ca865, 0xc113bc55cb19211c,
                    0x5863dbf1e3ac9dec, 0x22bbab39d3991495,
                    0xadd33a6183c78f1e, 0xd70b4aa9b3f20667,
                    0x985b3e827bed2b63, 0xe2834e4a4bd8a21a,
                    0x6debdf121b863991, 0x1733afda2bb3b0e8,
                    0xf34b37458bb86399, 0x8993478dbb8deae0,
                    0x06fbd6d5ebd3716b, 0x7c23a61ddbe6f812,
                    0x3373d23613f9d516, 0x49aba2fe23cc5c6f,
                    0xc6c333a67392c7e4, 0xbc1b436e43a74e9d,
                    0x95ac9329ac4bc9b5, 0xef74e3e19c7e40cc,
                    0x601c72b9cc20db47, 0x1ac40271fc15523e,
                    0x5594765a340a7f3a, 0x2f4c0692043ff643,
                    0xa02497ca54616dc8, 0xdafce7026454e4b1,
                    0x3e847f9dc45f37c0, 0x445c0f55f46abeb9,
                    0xcb349e0da4342532, 0xb1eceec59401ac4b,
                    0xfebc9aee5c1e814f, 0x8464ea266c2b0836,
                    0x0b0c7b7e3c7593bd, 0x71d40bb60c401ac4,
                    0xe8a46c1224f5a634, 0x927c1cda14c02f4d,
                    0x1d148d82449eb4c6, 0x67ccfd4a74ab3dbf,
                    0x289c8961bcb410bb, 0x5244f9a98c8199c2,
                    0xdd2c68f1dcdf0249, 0xa7f41839ecea8b30,
                    0x438c80a64ce15841, 0x3954f06e7cd4d138,
                    0xb63c61362c8a4ab3, 0xcce411fe1cbfc3ca,
                    0x83b465d5d4a0eece, 0xf96c151de49567b7,
                    0x76048445b4cbfc3c, 0x0cdcf48d84fe7545,
                    0x6fbd6d5ebd3716b7, 0x15651d968d029fce,
                    0x9a0d8ccedd5c0445, 0xe0d5fc06ed698d3c,
                    0xaf85882d2576a038, 0xd55df8e515432941,
                    0x5a3569bd451db2ca, 0x20ed197575283bb3,
                    0xc49581ead523e8c2, 0xbe4df122e51661bb,
                    0x3125607ab548fa30, 0x4bfd10b2857d7349,
                    0x04ad64994d625e4d, 0x7e7514517d57d734,
                    0xf11d85092d094cbf, 0x8bc5f5c11d3cc5c6,
                    0x12b5926535897936, 0x686de2ad05bcf04f,
                    0xe70573f555e26bc4, 0x9ddd033d65d7e2bd,
                    0xd28d7716adc8cfb9, 0xa85507de9dfd46c0,
                    0x273d9686cda3dd4b, 0x5de5e64efd965432,
                    0xb99d7ed15d9d8743, 0xc3450e196da80e3a,
                    0x4c2d9f413df695b1, 0x36f5ef890dc31cc8,
                    0x79a59ba2c5dc31cc, 0x037deb6af5e9b8b5,
                    0x8c157a32a5b7233e, 0xf6cd0afa9582aa47,
                    0x4ad64994d625e4da, 0x300e395ce6106da3,
                    0xbf66a804b64ef628, 0xc5bed8cc867b7f51,
                    0x8aeeace74e645255, 0xf036dc2f7e51db2c,
                    0x7f5e4d772e0f40a7, 0x05863dbf1e3ac9de,
                    0xe1fea520be311aaf, 0x9b26d5e88e0493d6,
                    0x144e44b0de5a085d, 0x6e963478ee6f8124,
                    0x21c640532670ac20, 0x5b1e309b16452559,
                    0xd476a1c3461bbed2, 0xaeaed10b762e37ab,
                    0x37deb6af5e9b8b5b, 0x4d06c6676eae0222,
                    0xc26e573f3ef099a9, 0xb8b627f70ec510d0,
                    0xf7e653dcc6da3dd4, 0x8d3e2314f6efb4ad,
                    0x0256b24ca6b12f26, 0x788ec2849684a65f,
                    0x9cf65a1b368f752e, 0xe62e2ad306bafc57,
                    0x6946bb8b56e467dc, 0x139ecb4366d1eea5,
                    0x5ccebf68aecec3a1, 0x2616cfa09efb4ad8,
                    0xa97e5ef8cea5d153, 0xd3a62e30fe90582a,
                    0xb0c7b7e3c7593bd8, 0xca1fc72bf76cb2a1,
                    0x45775673a732292a, 0x3faf26bb9707a053,
                    0x70ff52905f188d57, 0x0a2722586f2d042e,
                    0x854fb3003f739fa5, 0xff97c3c80f4616dc,
                    0x1bef5b57af4dc5ad, 0x61372b9f9f784cd4,
                    0xee5fbac7cf26d75f, 0x9487ca0fff135e26,
                    0xdbd7be24370c7322, 0xa10fceec0739fa5b,
                    0x2e675fb4576761d0, 0x54bf2f7c6752e8a9,
                    0xcdcf48d84fe75459, 0xb71738107fd2dd20,
                    0x387fa9482f8c46ab, 0x42a7d9801fb9cfd2,
                    0x0df7adabd7a6e2d6, 0x772fdd63e7936baf,
                    0xf8474c3bb7cdf024, 0x829f3cf387f8795d,
                    0x66e7a46c27f3aa2c, 0x1c3fd4a417c62355,
                    0x935745fc4798b8de, 0xe98f353477ad31a7,
                    0xa6df411fbfb21ca3, 0xdc0731d78f8795da,
                    0x536fa08fdfd90e51, 0x29b7d047efec8728,
                };
            }
        }
    }
}