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
        public static Boolean IsAscii(this Char32 character)
        {
            return character.ToChar(out Char value) && IsAscii(value);
        }
        
        /// <inheritdoc cref="Char.IsAscii(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotAscii(this Char character)
        {
            return !IsAscii(character);
        }
        
        /// <inheritdoc cref="Char.IsAscii(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotAscii(this Char32 character)
        {
            return !IsAscii(character);
        }
        
        /// <inheritdoc cref="Char.IsDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDigit(this Char character)
        {
            return Char.IsDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDigit(this Char32 character)
        {
            return character.ToChar(out Char value) && IsDigit(value);
        }
        
        /// <inheritdoc cref="Char.IsDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotDigit(this Char character)
        {
            return !IsDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotDigit(this Char32 character)
        {
            return !IsDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsLetter(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetter(this Char character)
        {
            return Char.IsLetter(character);
        }
        
        /// <inheritdoc cref="Char.IsLetter(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetter(this Char32 character)
        {
            return character.ToChar(out Char value) && IsLetter(value);
        }
        
        /// <inheritdoc cref="Char.IsLetter(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLetter(this Char character)
        {
            return !IsLetter(character);
        }
        
        /// <inheritdoc cref="Char.IsLetter(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLetter(this Char32 character)
        {
            return !IsLetter(character);
        }
        
        /// <inheritdoc cref="Char.IsLetterOrDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetterOrDigit(this Char character)
        {
            return Char.IsLetterOrDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsLetterOrDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetterOrDigit(this Char32 character)
        {
            return character.ToChar(out Char value) && IsLetterOrDigit(value);
        }
        
        /// <inheritdoc cref="Char.IsLetterOrDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLetterOrDigit(this Char character)
        {
            return !IsLetterOrDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsLetterOrDigit(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLetterOrDigit(this Char32 character)
        {
            return !IsLetterOrDigit(character);
        }
        
        /// <inheritdoc cref="Char.IsNumber(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNumber(this Char character)
        {
            return Char.IsNumber(character);
        }
        
        /// <inheritdoc cref="Char.IsNumber(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNumber(this Char32 character)
        {
            return character.ToChar(out Char value) && IsNumber(value);
        }
        
        /// <inheritdoc cref="Char.IsNumber(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNumber(this Char character)
        {
            return !IsNumber(character);
        }
        
        /// <inheritdoc cref="Char.IsNumber(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNumber(this Char32 character)
        {
            return !IsNumber(character);
        }
        
        /// <inheritdoc cref="Char.IsLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLower(this Char character)
        {
            return Char.IsLower(character);
        }
        
        /// <inheritdoc cref="Char.IsLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLower(this Char32 character)
        {
            return character.ToChar(out Char value) && IsLower(value);
        }
        
        /// <inheritdoc cref="Char.IsLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLower(this Char character)
        {
            return !IsLower(character);
        }
        
        /// <inheritdoc cref="Char.IsLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLower(this Char32 character)
        {
            return !IsLower(character);
        }
        
        /// <inheritdoc cref="Char.IsUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUpper(this Char character)
        {
            return Char.IsUpper(character);
        }
        
        /// <inheritdoc cref="Char.IsUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUpper(this Char32 character)
        {
            return character.ToChar(out Char value) && IsUpper(value);
        }
        
        /// <inheritdoc cref="Char.IsUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotUpper(this Char character)
        {
            return !IsUpper(character);
        }
        
        /// <inheritdoc cref="Char.IsUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotUpper(this Char32 character)
        {
            return !IsUpper(character);
        }
        
        /// <inheritdoc cref="Char.IsSymbol(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSymbol(this Char character)
        {
            return Char.IsSymbol(character);
        }
        
        /// <inheritdoc cref="Char.IsSymbol(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSymbol(this Char32 character)
        {
            return character.ToChar(out Char value) && IsSymbol(value);
        }
        
        /// <inheritdoc cref="Char.IsSymbol(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSymbol(this Char character)
        {
            return !IsSymbol(character);
        }
        
        /// <inheritdoc cref="Char.IsSymbol(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSymbol(this Char32 character)
        {
            return !IsSymbol(character);
        }
        
        /// <inheritdoc cref="Char.IsPunctuation(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPunctuation(this Char character)
        {
            return Char.IsPunctuation(character);
        }
        
        /// <inheritdoc cref="Char.IsPunctuation(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPunctuation(this Char32 character)
        {
            return character.ToChar(out Char value) && IsPunctuation(value);
        }
        
        /// <inheritdoc cref="Char.IsPunctuation(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotPunctuation(this Char character)
        {
            return !IsPunctuation(character);
        }
        
        /// <inheritdoc cref="Char.IsPunctuation(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotPunctuation(this Char32 character)
        {
            return !IsPunctuation(character);
        }
        
        /// <inheritdoc cref="Char.IsSeparator(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSeparator(this Char character)
        {
            return Char.IsSeparator(character);
        }
        
        /// <inheritdoc cref="Char.IsSeparator(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSeparator(this Char32 character)
        {
            return character.ToChar(out Char value) && IsSeparator(value);
        }
        
        /// <inheritdoc cref="Char.IsSeparator(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSeparator(this Char character)
        {
            return !IsSeparator(character);
        }
        
        /// <inheritdoc cref="Char.IsSeparator(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSeparator(this Char32 character)
        {
            return !IsSeparator(character);
        }
        
        /// <inheritdoc cref="Char.IsWhiteSpace(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace(this Char character)
        {
            return Char.IsWhiteSpace(character);
        }
        
        /// <inheritdoc cref="Char.IsWhiteSpace(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace(this Char32 character)
        {
            return character.ToChar(out Char value) && IsWhiteSpace(value);
        }
        
        /// <inheritdoc cref="Char.IsWhiteSpace(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace(this Char character)
        {
            return !IsWhiteSpace(character);
        }
        
        /// <inheritdoc cref="Char.IsWhiteSpace(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace(this Char32 character)
        {
            return !IsWhiteSpace(character);
        }

        /// <inheritdoc cref="Char.IsControl(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsControl(this Char character)
        {
            return Char.IsControl(character);
        }

        /// <inheritdoc cref="Char.IsControl(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsControl(this Char32 character)
        {
            return character.ToChar(out Char value) && IsControl(value);
        }
        
        /// <inheritdoc cref="Char.IsControl(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotControl(this Char character)
        {
            return !IsControl(character);
        }
        
        /// <inheritdoc cref="Char.IsControl(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotControl(this Char32 character)
        {
            return !IsControl(character);
        }

        /// <inheritdoc cref="Char.IsSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSurrogate(this Char character)
        {
            return Char.IsSurrogate(character);
        }

        /// <inheritdoc cref="Char.IsSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSurrogate(this Char32 character)
        {
            return character.ToChar(out Char value) && IsSurrogate(value);
        }
        
        /// <inheritdoc cref="Char.IsSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSurrogate(this Char character)
        {
            return !IsSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSurrogate(this Char32 character)
        {
            return !IsSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsHighSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsHighSurrogate(this Char character)
        {
            return Char.IsHighSurrogate(character);
        }

        /// <inheritdoc cref="Char.IsHighSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsHighSurrogate(this Char32 character)
        {
            return character.ToChar(out Char value) && IsHighSurrogate(value);
        }
        
        /// <inheritdoc cref="Char.IsHighSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotHighSurrogate(this Char character)
        {
            return !IsHighSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsHighSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotHighSurrogate(this Char32 character)
        {
            return !IsHighSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsLowSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLowSurrogate(this Char character)
        {
            return Char.IsLowSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsLowSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLowSurrogate(this Char32 character)
        {
            return character.ToChar(out Char value) && IsLowSurrogate(value);
        }
        
        /// <inheritdoc cref="Char.IsLowSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLowSurrogate(this Char character)
        {
            return !IsLowSurrogate(character);
        }
        
        /// <inheritdoc cref="Char.IsLowSurrogate(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLowSurrogate(this Char32 character)
        {
            return !IsLowSurrogate(character);
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
            return !IsSurrogatePair(character, pair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Repeat(this Char character, Int32 count)
        {
            return count <= 0 ? String.Empty : new String(character, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Repeat(this Char32 character, Int32 count)
        {
            return count > 0 ? character > Char.MaxValue ? character.ToString().Repeat(count) : new String((Char) character, count) : String.Empty;
        }
        
        /// <inheritdoc cref="Char.ToLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToLower(this Char character)
        {
            return Char.ToLower(character);
        }
        
        /// <inheritdoc cref="Char.ToLower(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char32 ToLower(this Char32 character)
        {
            return character.ToChar(out Char value) ? ToLower(value) : character;
        }

        /// <inheritdoc cref="Char.ToLower(Char,CultureInfo)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToLower(this Char character, CultureInfo? info)
        {
            return info is null ? ToLower(character) : Char.ToLower(character, info);
        }

        /// <inheritdoc cref="Char.ToLower(Char,CultureInfo)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char32 ToLower(this Char32 character, CultureInfo? info)
        {
            return character.ToChar(out Char value) ? info is null ? ToLower(value) : ToLower(value, info) : character;
        }

        /// <inheritdoc cref="Char.ToLowerInvariant"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToLowerInvariant(this Char character)
        {
            return Char.ToLowerInvariant(character);
        }

        /// <inheritdoc cref="Char.ToLowerInvariant"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char32 ToLowerInvariant(this Char32 character)
        {
            return character.ToChar(out Char value) ? ToLowerInvariant(value) : character;
        }

        /// <inheritdoc cref="Char.ToUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToUpper(this Char character)
        {
            return Char.ToUpper(character);
        }

        /// <inheritdoc cref="Char.ToUpper(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char32 ToUpper(this Char32 character)
        {
            return character.ToChar(out Char value) ? ToUpper(value) : character;
        }

        /// <inheritdoc cref="Char.ToUpper(Char,CultureInfo)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToUpper(this Char character, CultureInfo? info)
        {
            return info is null ? ToUpper(character) : Char.ToUpper(character, info);
        }

        /// <inheritdoc cref="Char.ToUpper(Char,CultureInfo)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char32 ToUpper(this Char32 character, CultureInfo? info)
        {
            return character.ToChar(out Char value) ? info is null ? ToUpper(value) : ToUpper(value, info) : character;
        }

        /// <inheritdoc cref="Char.ToUpperInvariant"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToUpperInvariant(this Char character)
        {
            return Char.ToUpperInvariant(character);
        }

        /// <inheritdoc cref="Char.ToUpperInvariant"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char32 ToUpperInvariant(this Char32 character)
        {
            return character.ToChar(out Char value) ? ToUpperInvariant(value) : character;
        }
        
        /// <inheritdoc cref="Char.GetUnicodeCategory(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCategory GetUnicodeCategory(this Char character)
        {
            return Char.GetUnicodeCategory(character);
        }
        
        /// <inheritdoc cref="Char.GetUnicodeCategory(Char)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCategory GetUnicodeCategory(this Char32 character)
        {
            return character.ToChar(out Char value) ? GetUnicodeCategory(value) : UnicodeCategory.OtherNotAssigned;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToChar(this Boolean value)
        {
            return value ? '1' : '0';
        }
    }
}