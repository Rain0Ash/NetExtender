// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.JWT.Interfaces;

namespace NetExtender.JWT.Utilities
{
    public static class JWTEncoderUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, Byte[]? key, TPayload payload)
        {
            return Encode(encoder, key, payload, null, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, String? key, TPayload payload)
        {
            return Encode(encoder, key, payload, null, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, IJWTSecret? secret, TPayload payload)
        {
            return Encode(encoder, secret, payload, null, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, Byte[]? key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra)
        {
            return Encode(encoder, key, payload, extra, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, String? key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra)
        {
            return Encode(encoder, key, payload, extra, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, IJWTSecret? secret, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra)
        {
            return Encode(encoder, secret, payload, extra, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, Byte[]? key, TPayload payload, IDictionary<String, Object>? extra)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            return encoder.Encode((JWTKey) key, payload, extra);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, String? key, TPayload payload, IDictionary<String, Object>? extra)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            return encoder.Encode((JWTKey) key, payload, extra);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, IJWTSecret? secret, TPayload payload, IDictionary<String, Object>? extra)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            return encoder.Encode(secret?.Key ?? default, payload, extra);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, Byte[]? key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra, out Dictionary<String, Object> headers)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            return encoder.Encode((JWTKey) key, payload, extra, out headers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, String? key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra, out Dictionary<String, Object> headers)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            return encoder.Encode((JWTKey) key, payload, extra, out headers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode<TPayload>(this IJWTEncoder encoder, IJWTSecret? secret, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra, out Dictionary<String, Object> headers)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            return encoder.Encode(secret?.Key ?? default, payload, extra, out headers);
        }
    }
}