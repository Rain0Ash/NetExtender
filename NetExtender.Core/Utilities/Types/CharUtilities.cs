// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class CharUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Repeat(this Char character, Int32 count)
        {
            return count < 1 ? String.Empty : new String(character, count);
        }
        
        /// <inheritdoc cref="Char.ToLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToLower(this Char character)
        {
            return Char.ToLower(character);
        }

        /// <inheritdoc cref="Char.ToLower(Char,CultureInfo)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToLower(this Char character, CultureInfo? info)
        {
            return info is null ? ToLower(character) : Char.ToLower(character, info);
        }

        /// <inheritdoc cref="Char.ToLowerInvariant"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToLowerInvariant(this Char character)
        {
            return Char.ToLowerInvariant(character);
        }

        /// <inheritdoc cref="Char.ToUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToUpper(this Char character)
        {
            return Char.ToUpper(character);
        }

        /// <inheritdoc cref="Char.ToUpper(Char,CultureInfo)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToUpper(this Char character, CultureInfo? info)
        {
            return info is null ? ToUpper(character) : Char.ToUpper(character, info);
        }

        /// <inheritdoc cref="Char.ToUpperInvariant"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToUpperInvariant(this Char character)
        {
            return Char.ToUpperInvariant(character);
        }
        
        /// <inheritdoc cref="Char.GetUnicodeCategory(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCategory GetUnicodeCategory(this Char character)
        {
            return Char.GetUnicodeCategory(character);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToChar(this Boolean value)
        {
            return value ? '1' : '0';
        }
    }
}