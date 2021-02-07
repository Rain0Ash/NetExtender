// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    public static class CharUtils
    {
        public static Boolean IsControl(Char character)
        {
            return character != '	' && Char.IsControl(character);
        }

        public static String Repeat(this Char character, Int32 count)
        {
            return count < 1 ? String.Empty : new String(character, count);
        }
        
        /// <inheritdoc cref="Char.ToLower(Char)"/>
        [Pure]
        public static Char ToLower(this Char character)
        {
            return Char.ToLower(character);
        }

        /// <inheritdoc cref="Char.ToLower(Char,CultureInfo)"/>
        [Pure]
        public static Char ToLower(this Char character, [CanBeNull] CultureInfo info)
        {
            return info is null ? ToLower(character) : Char.ToLower(character, info);
        }

        /// <inheritdoc cref="Char.ToLowerInvariant"/>
        [Pure]
        public static Char ToLowerInvariant(this Char character)
        {
            return Char.ToLowerInvariant(character);
        }

        /// <inheritdoc cref="Char.ToUpper(Char)"/>
        [Pure]
        public static Char ToUpper(this Char character)
        {
            return Char.ToUpper(character);
        }

        /// <inheritdoc cref="Char.ToUpper(Char,CultureInfo)"/>
        [Pure]
        public static Char ToUpper(this Char character, [CanBeNull] CultureInfo info)
        {
            return info is null ? ToUpper(character) : Char.ToUpper(character, info);
        }

        /// <inheritdoc cref="Char.ToUpperInvariant"/>
        [Pure]
        public static Char ToUpperInvariant(this Char character)
        {
            return Char.ToUpperInvariant(character);
        }
        
        /// <inheritdoc cref="Char.GetUnicodeCategory(Char)"/>
        [Pure]
        public static UnicodeCategory GetUnicodeCategory(this Char character)
        {
            return Char.GetUnicodeCategory(character);
        }

        [Pure]
        public static Char ToChar(this Boolean value)
        {
            return value ? '1' : '0';
        }
    }
}