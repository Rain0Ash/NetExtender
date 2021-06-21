// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;
using NetExtender.Crypto.Base;
using NetExtender.Crypto.Base.Alphabet;
using NetExtender.Crypto.Base.Interfaces;

namespace NetExtender.Crypto
{
    public enum BaseCryptType
    {
        Base16,
        Base32,
        Base58,
        Base64,
        Base85
    }

    public static partial class Cryptography
    {
        public static String GetBase64StringFromBytes(this ReadOnlySpan<Byte> data, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(data, options);
        }

        public static String GetBase64StringFromBytes(this Byte[] data, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(data, options);
        }

        public static String EncodeBase(this String plain, BaseCryptType type = Base.DefaultBaseCryptType)
        {
            return Base.Encode(plain, type);
        }

        public static String EncodeBase(this Byte[] data, BaseCryptType type = Base.DefaultBaseCryptType)
        {
            return Base.Encode(data, type);
        }

        public static String DecodeBase(this String encoded, BaseCryptType type = Base.DefaultBaseCryptType)
        {
            return Base.Decode(encoded, type);
        }

        public static String DecodeBase(this Byte[] data, BaseCryptType type = Base.DefaultBaseCryptType)
        {
            return Base.Decode(data, type);
        }

        public static class Base
        {
            public const BaseCryptType DefaultBaseCryptType = BaseCryptType.Base64;
            
            private static IBaseCrypt Base16 { get; } = new Base16(Base16Alphabet.UpperCase);
            private static IBaseCrypt Base32 { get; } = new Base32(Base32Alphabet.ZBase32);
            private static IBaseCrypt Base58 { get; } = new Base58(Base58Alphabet.Bitcoin);
            private static IBaseCrypt Base85 { get; } = new Base85(Base85Alphabet.Ascii85);

            public static String Encode(String plain, BaseCryptType type = DefaultBaseCryptType)
            {
                return type switch
                {
                    BaseCryptType.Base16 => Encode(Base16, plain),
                    BaseCryptType.Base32 => Encode(Base32, plain),
                    BaseCryptType.Base58 => Encode(Base58, plain),
                    BaseCryptType.Base64 => Base64Encode(plain),
                    BaseCryptType.Base85 => Encode(Base85, plain),
                    _ => throw new NotSupportedException()
                };
            }

            public static String Encode(Byte[] data, BaseCryptType type = DefaultBaseCryptType)
            {
                return type switch
                {
                    BaseCryptType.Base16 => Encode(Base16, data),
                    BaseCryptType.Base32 => Encode(Base32, data),
                    BaseCryptType.Base58 => Encode(Base58, data),
                    BaseCryptType.Base64 => Base64Encode(data),
                    BaseCryptType.Base85 => Encode(Base85, data),
                    _ => throw new NotSupportedException()
                };
            }

            public static String Decode(String encoded, BaseCryptType type = DefaultBaseCryptType)
            {
                return type switch
                {
                    BaseCryptType.Base16 => Decode(Base16, encoded),
                    BaseCryptType.Base32 => Decode(Base32, encoded),
                    BaseCryptType.Base58 => Decode(Base58, encoded),
                    BaseCryptType.Base64 => Base64Decode(encoded),
                    BaseCryptType.Base85 => Decode(Base85, encoded),
                    _ => throw new NotSupportedException()
                };
            }

            public static String Decode(Byte[] data, BaseCryptType type = DefaultBaseCryptType)
            {
                return type switch
                {
                    BaseCryptType.Base16 => Decode(Base16, data),
                    BaseCryptType.Base32 => Decode(Base32, data),
                    BaseCryptType.Base58 => Decode(Base58, data),
                    BaseCryptType.Base64 => Base64Decode(data),
                    BaseCryptType.Base85 => Decode(Base85, data),
                    _ => throw new NotSupportedException()
                };
            }

            public static String Encode(IBaseCrypt crypt, String plain)
            {
                return crypt.Encode(Encoding.UTF8.GetBytes(plain));
            }

            public static String Encode(IBaseCrypt crypt, Byte[] data)
            {
                return crypt.Encode(data);
            }

            public static String Decode(IBaseCrypt crypt, String encoded)
            {
                return Decode(crypt, encoded.ToCharArray());
            }

            public static String Decode(IBaseCrypt crypt, Byte[] data)
            {
                return Decode(crypt, Encoding.UTF8.GetString(data).ToCharArray());
            }

            public static String Decode(IBaseCrypt crypt, Char[] data)
            {
                return Encoding.UTF8.GetString(crypt.Decode(data));
            }

            public static String Base64Encode(String plain)
            {
                return Base64Encode(Encoding.UTF8.GetBytes(plain));
            }

            public static String Base64Encode(ReadOnlySpan<Byte> bytes)
            {
                return Convert.ToBase64String(bytes);
            }

            public static String Base64Encode(Byte[] bytes)
            {
                return Convert.ToBase64String(bytes);
            }

            public static String Base64Decode(String encoded)
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            }

            public static String Base64Decode(ReadOnlySpan<Byte> bytes)
            {
                return Encoding.UTF8.GetString(bytes);
            }

            public static String Base64Decode(Byte[] bytes)
            {
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}