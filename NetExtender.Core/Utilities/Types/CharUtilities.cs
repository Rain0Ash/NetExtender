// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class CharUtilities
    {
        /// <inheritdoc cref="Char.IsAscii(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAscii(this Char character)
        {
            return Char.IsAscii(character);
        }
        
        /// <inheritdoc cref="Char.IsAscii(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotAscii(this Char character)
        {
            return !Char.IsAscii(character);
        }
        
        /// <inheritdoc cref="Char.IsDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDigit(this Char character)
        {
            return Char.IsDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotDigit(this Char character)
        {
            return !Char.IsDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsLetter(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetter(this Char character)
        {
            return Char.IsLetter(character);
        }
        
        /// <inheritdoc cref="Char.IsLetter(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLetter(this Char character)
        {
            return !Char.IsLetter(character);
        }
        
        /// <inheritdoc cref="Char.IsLetterOrDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetterOrDigit(this Char character)
        {
            return Char.IsLetterOrDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsLetterOrDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLetterOrDigit(this Char character)
        {
            return !Char.IsLetterOrDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsNumber(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNumber(this Char character)
        {
            return Char.IsNumber(character);
        }
        
        /// <inheritdoc cref="Char.IsNumber(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNumber(this Char character)
        {
            return !Char.IsNumber(character);
        }
        
        /// <inheritdoc cref="Char.IsLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLower(this Char character)
        {
            return Char.IsLower(character);
        }
        
        /// <inheritdoc cref="Char.IsLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLower(this Char character)
        {
            return !Char.IsLower(character);
        }
        
        /// <inheritdoc cref="Char.IsUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUpper(this Char character)
        {
            return Char.IsUpper(character);
        }
        
        /// <inheritdoc cref="Char.IsUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotUpper(this Char character)
        {
            return !Char.IsUpper(character);
        }
        
        /// <inheritdoc cref="Char.IsSymbol(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSymbol(this Char character)
        {
            return Char.IsSymbol(character);
        }
        
        /// <inheritdoc cref="Char.IsSymbol(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSymbol(this Char character)
        {
            return !Char.IsSymbol(character);
        }
        
        /// <inheritdoc cref="Char.IsPunctuation(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPunctuation(this Char character)
        {
            return Char.IsPunctuation(character);
        }
        
        /// <inheritdoc cref="Char.IsPunctuation(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotPunctuation(this Char character)
        {
            return !Char.IsPunctuation(character);
        }
        
        /// <inheritdoc cref="Char.IsSeparator(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSeparator(this Char character)
        {
            return Char.IsSeparator(character);
        }
        
        /// <inheritdoc cref="Char.IsSeparator(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSeparator(this Char character)
        {
            return !Char.IsSeparator(character);
        }
        
        /// <inheritdoc cref="Char.IsWhiteSpace(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace(this Char character)
        {
            return Char.IsWhiteSpace(character);
        }
        
        /// <inheritdoc cref="Char.IsWhiteSpace(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace(this Char character)
        {
            return !Char.IsWhiteSpace(character);
        }

        /// <inheritdoc cref="Char.IsControl(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsControl(this Char character)
        {
            return Char.IsControl(character);
        }
        
        /// <inheritdoc cref="Char.IsControl(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotControl(this Char character)
        {
            return !Char.IsControl(character);
        }

        /// <inheritdoc cref="Char.IsSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSurrogate(this Char character)
        {
            return Char.IsSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSurrogate(this Char character)
        {
            return !Char.IsSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsLowSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLowSurrogate(this Char character)
        {
            return Char.IsLowSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsLowSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLowSurrogate(this Char character)
        {
            return !Char.IsLowSurrogate(character);
        }

        /// <inheritdoc cref="Char.IsHighSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsHighSurrogate(this Char character)
        {
            return Char.IsHighSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsHighSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotHighSurrogate(this Char character)
        {
            return !Char.IsHighSurrogate(character);
        }

        /// <inheritdoc cref="Char.IsSurrogatePair(Char,Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSurrogatePair(this Char character, Char pair)
        {
            return Char.IsSurrogatePair(character, pair);
        }
        
        /// <inheritdoc cref="Char.IsSurrogatePair(Char,Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSurrogatePair(this Char character, Char pair)
        {
            return !Char.IsSurrogatePair(character, pair);
        }

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