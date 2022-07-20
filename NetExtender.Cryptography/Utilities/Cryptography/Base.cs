// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;
using NetExtender.Cryptography.Base;
using NetExtender.Cryptography.Base.Alphabet;
using NetExtender.Cryptography.Base.Interfaces;

namespace NetExtender.Utilities.Cryptography
{
    public enum BaseCryptType
    {
        Base16,
        Base32,
        Base58,
        Base64,
        Base85
    }

    public static partial class CryptographyUtilities
    {
        public static String GetBase64StringFromBytes(this ReadOnlySpan<Byte> value)
        {
            return GetBase64StringFromBytes(value, Base64FormattingOptions.None);
        }

        public static String GetBase64StringFromBytes(this ReadOnlySpan<Byte> value, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(value, options);
        }
        
        public static String? TryGetBase64StringFromBytes(this ReadOnlySpan<Byte> value)
        {
            return TryGetBase64StringFromBytes(value, Base64FormattingOptions.None);
        }

        public static String? TryGetBase64StringFromBytes(this ReadOnlySpan<Byte> value, Base64FormattingOptions options)
        {
            try
            {
                return GetBase64StringFromBytes(value, options);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static String GetBase64StringFromBytes(this Byte[] value)
        {
            return GetBase64StringFromBytes(value, Base64FormattingOptions.None);
        }

        public static String GetBase64StringFromBytes(this Byte[] value, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(value, options);
        }

        public static String? TryGetBase64StringFromBytes(this Byte[] value)
        {
            return TryGetBase64StringFromBytes(value, Base64FormattingOptions.None);
        }

        public static String? TryGetBase64StringFromBytes(this Byte[] value, Base64FormattingOptions options)
        {
            try
            {
                return GetBase64StringFromBytes(value, options);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static String EncodeBase(this String value)
        {
            return Base.Encode(value);
        }

        public static String? TryEncodeBase(this String value)
        {
            return Base.TryEncode(value);
        }

        public static String EncodeBase(this String value, BaseCryptType type)
        {
            return Base.Encode(value, type);
        }

        public static String? TryEncodeBase(this String value, BaseCryptType type)
        {
            return Base.TryEncode(value, type);
        }

        public static String EncodeBase(this Byte[] value)
        {
            return Base.Encode(value);
        }

        public static String? TryEncodeBase(this Byte[] value)
        {
            return Base.TryEncode(value);
        }

        public static String EncodeBase(this Byte[] value, BaseCryptType type)
        {
            return Base.Encode(value, type);
        }

        public static String? TryEncodeBase(this Byte[] value, BaseCryptType type)
        {
            return Base.TryEncode(value, type);
        }

        public static String DecodeBase(this String value)
        {
            return Base.Decode(value);
        }

        public static String? TryDecodeBase(this String value)
        {
            return Base.TryDecode(value);
        }

        public static String DecodeBase(this String value, BaseCryptType type)
        {
            return Base.Decode(value, type);
        }

        public static String? TryDecodeBase(this String value, BaseCryptType type)
        {
            return Base.TryDecode(value, type);
        }

        public static String DecodeBase(this Byte[] value)
        {
            return Base.Decode(value);
        }

        public static String? TryDecodeBase(this Byte[] value)
        {
            return Base.TryDecode(value);
        }

        public static String DecodeBase(this Byte[] value, BaseCryptType type)
        {
            return Base.Decode(value, type);
        }

        public static String? TryDecodeBase(this Byte[] value, BaseCryptType type)
        {
            return Base.TryDecode(value, type);
        }

        public static class Base
        {
            public const BaseCryptType DefaultBaseCryptType = BaseCryptType.Base64;
            
            private static IBaseCrypt Base16 { get; } = new Base16(Base16Alphabet.UpperCase);
            private static IBaseCrypt Base32 { get; } = new Base32(Base32Alphabet.ZBase32);
            private static IBaseCrypt Base58 { get; } = new Base58(Base58Alphabet.Bitcoin);
            private static IBaseCrypt Base85 { get; } = new Base85(Base85Alphabet.Ascii85);

            public static String Encode(String value)
            {
                return Encode(value, DefaultBaseCryptType);
            }

            public static String Encode(String value, BaseCryptType type)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return type switch
                {
                    BaseCryptType.Base16 => Encode(Base16, value),
                    BaseCryptType.Base32 => Encode(Base32, value),
                    BaseCryptType.Base58 => Encode(Base58, value),
                    BaseCryptType.Base64 => Base64Encode(value),
                    BaseCryptType.Base85 => Encode(Base85, value),
                    _ => throw new NotSupportedException()
                };
            }
            
            public static String? TryEncode(String value)
            {
                return TryEncode(value, DefaultBaseCryptType);
            }

            public static String? TryEncode(String value, BaseCryptType type)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return type switch
                {
                    BaseCryptType.Base16 => TryEncode(Base16, value),
                    BaseCryptType.Base32 => TryEncode(Base32, value),
                    BaseCryptType.Base58 => TryEncode(Base58, value),
                    BaseCryptType.Base64 => TryBase64Encode(value),
                    BaseCryptType.Base85 => TryEncode(Base85, value),
                    _ => throw new NotSupportedException()
                };
            }

            public static String Encode(Byte[] value)
            {
                return Encode(value, DefaultBaseCryptType);
            }

            public static String Encode(Byte[] value, BaseCryptType type)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return type switch
                {
                    BaseCryptType.Base16 => Encode(Base16, value),
                    BaseCryptType.Base32 => Encode(Base32, value),
                    BaseCryptType.Base58 => Encode(Base58, value),
                    BaseCryptType.Base64 => Base64Encode(value),
                    BaseCryptType.Base85 => Encode(Base85, value),
                    _ => throw new NotSupportedException()
                };
            }

            public static String? TryEncode(Byte[] value)
            {
                return TryEncode(value, DefaultBaseCryptType);
            }

            public static String? TryEncode(Byte[] value, BaseCryptType type)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return type switch
                {
                    BaseCryptType.Base16 => TryEncode(Base16, value),
                    BaseCryptType.Base32 => TryEncode(Base32, value),
                    BaseCryptType.Base58 => TryEncode(Base58, value),
                    BaseCryptType.Base64 => TryBase64Encode(value),
                    BaseCryptType.Base85 => TryEncode(Base85, value),
                    _ => throw new NotSupportedException()
                };
            }

            public static String Decode(String value)
            {
                return Decode(value, DefaultBaseCryptType);
            }

            public static String Decode(String value, BaseCryptType type)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return type switch
                {
                    BaseCryptType.Base16 => Decode(Base16, value),
                    BaseCryptType.Base32 => Decode(Base32, value),
                    BaseCryptType.Base58 => Decode(Base58, value),
                    BaseCryptType.Base64 => Base64Decode(value),
                    BaseCryptType.Base85 => Decode(Base85, value),
                    _ => throw new NotSupportedException()
                };
            }

            public static String? TryDecode(String value)
            {
                return TryDecode(value, DefaultBaseCryptType);
            }

            public static String? TryDecode(String value, BaseCryptType type)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return type switch
                {
                    BaseCryptType.Base16 => TryDecode(Base16, value),
                    BaseCryptType.Base32 => TryDecode(Base32, value),
                    BaseCryptType.Base58 => TryDecode(Base58, value),
                    BaseCryptType.Base64 => TryBase64Decode(value),
                    BaseCryptType.Base85 => TryDecode(Base85, value),
                    _ => throw new NotSupportedException()
                };
            }

            public static String Decode(Byte[] value)
            {
                return Decode(value, DefaultBaseCryptType);
            }

            public static String Decode(Byte[] value, BaseCryptType type)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return type switch
                {
                    BaseCryptType.Base16 => Decode(Base16, value),
                    BaseCryptType.Base32 => Decode(Base32, value),
                    BaseCryptType.Base58 => Decode(Base58, value),
                    BaseCryptType.Base64 => Base64Decode(value),
                    BaseCryptType.Base85 => Decode(Base85, value),
                    _ => throw new NotSupportedException()
                };
            }

            public static String? TryDecode(Byte[] value)
            {
                return TryDecode(value, DefaultBaseCryptType);
            }

            public static String? TryDecode(Byte[] value, BaseCryptType type)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return type switch
                {
                    BaseCryptType.Base16 => TryDecode(Base16, value),
                    BaseCryptType.Base32 => TryDecode(Base32, value),
                    BaseCryptType.Base58 => TryDecode(Base58, value),
                    BaseCryptType.Base64 => TryBase64Decode(value),
                    BaseCryptType.Base85 => TryDecode(Base85, value),
                    _ => throw new NotSupportedException()
                };
            }

            public static String Encode(IBaseCrypt crypt, String value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return crypt.Encode(Encoding.UTF8.GetBytes(value));
            }

            public static String? TryEncode(IBaseCrypt crypt, String value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Encode(crypt, value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Encode(IBaseCrypt crypt, Byte[] value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return crypt.Encode(value);
            }

            public static String? TryEncode(IBaseCrypt crypt, Byte[] value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Encode(crypt, value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Decode(IBaseCrypt crypt, String value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return Decode(crypt, value.ToCharArray());
            }

            public static String? TryDecode(IBaseCrypt crypt, String value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Decode(crypt, value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Decode(IBaseCrypt crypt, Byte[] value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return Decode(crypt, Encoding.UTF8.GetString(value).ToCharArray());
            }

            public static String? TryDecode(IBaseCrypt crypt, Byte[] value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Decode(crypt, value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Decode(IBaseCrypt crypt, Char[] value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return Encoding.UTF8.GetString(crypt.Decode(value));
            }

            public static String? TryDecode(IBaseCrypt crypt, Char[] value)
            {
                if (crypt is null)
                {
                    throw new ArgumentNullException(nameof(crypt));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Decode(crypt, value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Base64Encode(String value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return Base64Encode(Encoding.UTF8.GetBytes(value));
            }

            public static String? TryBase64Encode(String value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Base64Encode(value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Base64Encode(ReadOnlySpan<Byte> value)
            {
                return Convert.ToBase64String(value);
            }

            public static String? TryBase64Encode(ReadOnlySpan<Byte> value)
            {
                try
                {
                    return Base64Encode(value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Base64Encode(Byte[] value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return Convert.ToBase64String(value);
            }

            public static String? TryBase64Encode(Byte[] value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Base64Encode(value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Base64Decode(String value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }

            public static String? TryBase64Decode(String value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Base64Decode(value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Base64Decode(ReadOnlySpan<Byte> value)
            {
                return Base64Decode(Encoding.UTF8.GetString(value));
            }

            public static String? TryBase64Decode(ReadOnlySpan<Byte> value)
            {
                try
                {
                    return Base64Decode(value);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static String Base64Decode(Byte[] value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return Base64Decode(Encoding.UTF8.GetString(value));
            }

            public static String? TryBase64Decode(Byte[] value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                try
                {
                    return Base64Decode(value);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}