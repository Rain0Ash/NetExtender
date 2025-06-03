// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.JWT.Interfaces;

namespace NetExtender.JWT.Utilities
{
    public static class JWTUrlEncoderUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Encode(this IJWTUrlEncoder encoder, Byte[] source)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return encoder.Encode((ReadOnlySpan<Byte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryDecode(this IJWTUrlEncoder encoder, ReadOnlySpan<Char> source, ref Span<Byte> destination)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            if (!encoder.TryDecode(source, destination, out Int32 written))
            {
                return false;
            }

            destination = destination.Slice(written);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryDecode(this IJWTUrlEncoder encoder, String source, ref Span<Byte> destination)
        {
            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            if (!encoder.TryDecode(source, destination, out Int32 written))
            {
                return false;
            }

            destination = destination.Slice(written);
            return true;
        }
    }
}