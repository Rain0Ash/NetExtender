// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    public static class CharUtils
    {
        public static Boolean IsControl(Char chr)
        {
            return chr != '	' && Char.IsControl(chr);
        }

        public static String Repeat(this Char chr, Int32 count)
        {
            return count < 1 ? String.Empty : new String(chr, count);
        }
        
        /// <inheritdoc cref="Char.ToLower(Char)"/>
        [Pure]
        public static Char ToLower(this Char chr)
        {
            return Char.ToLower(chr);
        }

        /// <inheritdoc cref="Char.ToLower(Char,CultureInfo)"/>
        [Pure]
        public static Char ToLower(this Char chr, [NotNull] CultureInfo culture)
        {
            return Char.ToLower(chr, culture);
        }

        /// <inheritdoc cref="Char.ToLowerInvariant"/>
        [Pure]
        public static Char ToLowerInvariant(this Char chr)
        {
            return Char.ToLowerInvariant(chr);
        }

        /// <inheritdoc cref="Char.ToUpper(Char)"/>
        [Pure]
        public static Char ToUpper(this Char chr)
        {
            return Char.ToUpper(chr);
        }

        /// <inheritdoc cref="Char.ToUpper(Char,CultureInfo)"/>
        [Pure]
        public static Char ToUpper(this Char chr, [NotNull] CultureInfo culture)
        {
            return Char.ToUpper(chr, culture);
        }

        /// <inheritdoc cref="Char.ToUpperInvariant"/>
        [Pure]
        public static Char ToUpperInvariant(this Char chr)
        {
            return Char.ToUpperInvariant(chr);
        }
        
        /// <inheritdoc cref="Char.GetUnicodeCategory(Char)"/>
        [Pure]
        public static UnicodeCategory GetUnicodeCategory(this Char chr)
        {
            return Char.GetUnicodeCategory(chr);
        }

        [Pure]
        public static Char ToChar(this Boolean value)
        {
            return value ? '1' : '0';
        }
    }
}