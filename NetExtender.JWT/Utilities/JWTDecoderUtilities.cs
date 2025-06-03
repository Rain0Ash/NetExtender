// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.JWT.Interfaces;

namespace NetExtender.JWT.Utilities
{
    public static class JWTDecoderUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Validate(this IJWTDecoder decoder, JWTToken jwt, ReadOnlySpan<Byte> key)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            if (decoder.TryValidate(jwt, key) is { } exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Validate(this IJWTDecoder decoder, JWTToken jwt, JWTKey key)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            if (decoder.TryValidate(jwt, key) is { } exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Validate(this IJWTDecoder decoder, JWTToken jwt, Byte[]? key)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            if (decoder.TryValidate(jwt, key) is { } exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Validate(this IJWTDecoder decoder, JWTToken jwt, String? key)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            if (decoder.TryValidate(jwt, key) is { } exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Validate(this IJWTDecoder decoder, JWTToken jwt, JWTKeys keys)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            if (decoder.TryValidate(jwt, keys) is { } exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Validate(this IJWTDecoder decoder, JWTToken jwt, params JWTKey[]? keys)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            if (decoder.TryValidate(jwt, keys) is { } exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Validate(this IJWTDecoder decoder, JWTToken jwt, IEnumerable<JWTKey>? keys)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            if (decoder.TryValidate(jwt, keys) is { } exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Validate(this IJWTDecoder decoder, JWTToken jwt, IJWTSecret? secret)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            if (decoder.TryValidate(jwt, secret) is { } exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? TryValidate(this IJWTDecoder decoder, JWTToken jwt, Byte[]? key)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            return decoder.TryValidate(jwt, (JWTKey) key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? TryValidate(this IJWTDecoder decoder, JWTToken jwt, String? key)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            return decoder.TryValidate(jwt, (JWTKey) key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? TryValidate(this IJWTDecoder decoder, JWTToken jwt, params JWTKey[]? keys)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            return decoder.TryValidate(jwt, new JWTKeys(keys));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? TryValidate(this IJWTDecoder decoder, JWTToken jwt, IEnumerable<JWTKey>? keys)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            return decoder.TryValidate(jwt, new JWTKeys(keys));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception? TryValidate(this IJWTDecoder decoder, JWTToken jwt, IJWTSecret? secret)
        {
            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            return decoder.TryValidate(jwt, secret?.Keys ?? default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, Byte[]? key)
        {
            return decoder is not null ? decoder.Decode(jwt, (JWTKey) key) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, Byte[]? key, Boolean verify)
        {
            return decoder is not null ? decoder.Decode(jwt, (JWTKey) key, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, String? key)
        {
            return decoder is not null ? decoder.Decode(jwt, (JWTKey) key) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, String? key, Boolean verify)
        {
            return decoder is not null ? decoder.Decode(jwt, (JWTKey) key, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, params JWTKey[]? keys)
        {
            return decoder is not null ? decoder.Decode(jwt, new JWTKeys(keys)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, Boolean verify, params JWTKey[]? keys)
        {
            return decoder is not null ? decoder.Decode(jwt, new JWTKeys(keys), verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, IEnumerable<JWTKey>? keys)
        {
            return decoder is not null ? decoder.Decode(jwt, new JWTKeys(keys)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, IEnumerable<JWTKey>? keys, Boolean verify)
        {
            return decoder is not null ? decoder.Decode(jwt, new JWTKeys(keys), verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, IJWTSecret? secret)
        {
            return decoder is not null ? decoder.Decode(jwt, secret?.Keys ?? default) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Decode(this IJWTDecoder decoder, JWTToken jwt, IJWTSecret? secret, Boolean verify)
        {
            return decoder is not null ? decoder.Decode(jwt, secret?.Keys ?? default, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, Byte[]? key)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, (JWTKey) key) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, Byte[]? key, Boolean verify)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, (JWTKey) key, verify) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, String? key)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, (JWTKey) key) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, String? key, Boolean verify)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, (JWTKey) key, verify) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, params JWTKey[]? keys)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, new JWTKeys(keys)) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, Boolean verify, params JWTKey[]? keys)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, new JWTKeys(keys), verify) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, IEnumerable<JWTKey>? keys)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, new JWTKeys(keys)) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, IEnumerable<JWTKey>? keys, Boolean verify)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, new JWTKeys(keys), verify) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, IJWTSecret? secret)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, secret?.Keys ?? default) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object Decode(this IJWTDecoder decoder, Type type, JWTToken jwt, IJWTSecret? secret, Boolean verify)
        {
            return decoder is not null ? type is not null ? decoder.Decode(type, jwt, secret?.Keys ?? default, verify) : throw new ArgumentNullException(nameof(type)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, Byte[]? key)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, (JWTKey) key) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, Byte[]? key, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, (JWTKey) key, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, String? key)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, (JWTKey) key) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, String? key, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, (JWTKey) key, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, params JWTKey[]? keys)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, new JWTKeys(keys)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, Boolean verify, params JWTKey[]? keys)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, new JWTKeys(keys), verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, IEnumerable<JWTKey>? keys)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, new JWTKeys(keys)) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, IEnumerable<JWTKey>? keys, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, new JWTKeys(keys), verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, IJWTSecret? secret)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, secret?.Keys ?? default) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(this IJWTDecoder decoder, JWTToken jwt, IJWTSecret? secret, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<T>(jwt, secret?.Keys ?? default, verify) : throw new ArgumentNullException(nameof(decoder));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeHeaderToDictionary(this IJWTDecoder decoder, JWTToken jwt)
        {
            return decoder is not null ? decoder.DecodeHeader<Dictionary<String, Object>>(jwt) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, ReadOnlySpan<Byte> key)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, key) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, ReadOnlySpan<Byte> key, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, key, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, JWTKey key)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, key) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, JWTKey key, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, key, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, Byte[]? key)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, key) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, Byte[]? key, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, key, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, String? key)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, key) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, String? key, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, key, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, JWTKeys keys)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, keys) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, JWTKeys keys, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, keys, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, params JWTKey[]? keys)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, keys) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, Boolean verify, params JWTKey[]? keys)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, keys, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, IEnumerable<JWTKey>? keys)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, keys) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, IEnumerable<JWTKey>? keys, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, keys, verify) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, IJWTSecret? secret)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, secret) : throw new ArgumentNullException(nameof(decoder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<String, Object> DecodeToDictionary(this IJWTDecoder decoder, JWTToken jwt, IJWTSecret? secret, Boolean verify)
        {
            return decoder is not null ? decoder.Decode<Dictionary<String, Object>>(jwt, secret, verify) : throw new ArgumentNullException(nameof(decoder));
        }
    }
}