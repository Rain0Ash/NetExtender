// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

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
#if NETCOREAPP3_1_OR_GREATER
            return character.Rune.IsAscii;
#else
            return character.ToChar(out Char value) && IsAscii(value);
#endif
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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsDigit(character);
#else
            return character.ToChar(out Char value) && IsDigit(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsDigit(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDigit(this Rune character)
        {
            return Rune.IsDigit(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsDigit(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotDigit(this Rune character)
        {
            return !IsDigit(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsLetter(character);
#else
            return character.ToChar(out Char value) && IsLetter(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsLetter(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetter(this Rune character)
        {
            return Rune.IsLetter(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsLetter(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLetter(this Rune character)
        {
            return !IsLetter(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsLetterOrDigit(character);
#else
            return character.ToChar(out Char value) && IsLetterOrDigit(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsLetterOrDigit(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetterOrDigit(this Rune character)
        {
            return Rune.IsLetterOrDigit(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsLetterOrDigit(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLetterOrDigit(this Rune character)
        {
            return !IsLetterOrDigit(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsNumber(character);
#else
            return character.ToChar(out Char value) && IsNumber(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsNumber(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNumber(this Rune character)
        {
            return Rune.IsNumber(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsNumber(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNumber(this Rune character)
        {
            return !IsNumber(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsLower(character);
#else
            return character.ToChar(out Char value) && IsLower(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsLower(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLower(this Rune character)
        {
            return Rune.IsLower(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsLower(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotLower(this Rune character)
        {
            return !IsLower(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsUpper(character);
#else
            return character.ToChar(out Char value) && IsUpper(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsUpper(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUpper(this Rune character)
        {
            return Rune.IsUpper(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsUpper(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotUpper(this Rune character)
        {
            return !IsUpper(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsSymbol(character);
#else
            return character.ToChar(out Char value) && IsSymbol(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsSymbol(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSymbol(this Rune character)
        {
            return Rune.IsSymbol(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsSymbol(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSymbol(this Rune character)
        {
            return !IsSymbol(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsPunctuation(character);
#else
            return character.ToChar(out Char value) && IsPunctuation(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsPunctuation(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPunctuation(this Rune character)
        {
            return Rune.IsPunctuation(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsPunctuation(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotPunctuation(this Rune character)
        {
            return !IsPunctuation(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsSeparator(character);
#else
            return character.ToChar(out Char value) && IsSeparator(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsSeparator(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSeparator(this Rune character)
        {
            return Rune.IsSeparator(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsSeparator(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotSeparator(this Rune character)
        {
            return !IsSeparator(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsWhiteSpace(character);
#else
            return character.ToChar(out Char value) && IsWhiteSpace(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsWhiteSpace(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace(this Rune character)
        {
            return Rune.IsWhiteSpace(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsWhiteSpace(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace(this Rune character)
        {
            return !IsWhiteSpace(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.IsControl(character);
#else
            return character.ToChar(out Char value) && IsControl(value);
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsControl(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsControl(this Rune character)
        {
            return Rune.IsControl(character);
        }
#endif

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

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.IsControl(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotControl(this Rune character)
        {
            return !IsControl(character);
        }
#endif

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Repeat(this Rune character, Int32 count)
        {
            return count > 0 ? unchecked((UInt32) character.Value) > Char.MaxValue ? character.ToString().Repeat(count) : new String(unchecked((Char) character.Value), count) : String.Empty;
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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.ToLowerInvariant(character);
#else
            return character.ToChar(out Char value) ? ToLower(value) : character;
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.ToLowerInvariant(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rune ToLower(this Rune character)
        {
            return ToLowerInvariant(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return info is null ? ToLowerInvariant(character) : Rune.ToLower(character, info);
#else
            return character.ToChar(out Char value) ? info is null ? ToLower(value) : ToLower(value, info) : character;
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.ToLower(Rune,CultureInfo)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rune ToLower(this Rune character, CultureInfo? info)
        {
            return info is null ? ToLower(character) : Rune.ToLower(character, info);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.ToLowerInvariant(character);
#else
            return character.ToChar(out Char value) ? ToLowerInvariant(value) : character;
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.ToLowerInvariant(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rune ToLowerInvariant(this Rune character)
        {
            return Rune.ToLowerInvariant(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.ToUpperInvariant(character);
#else
            return character.ToChar(out Char value) ? ToUpper(value) : character;
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.ToUpperInvariant(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rune ToUpper(this Rune character)
        {
            return ToUpperInvariant(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return info is null ? ToUpperInvariant(character) : Rune.ToUpper(character, info);
#else
            return character.ToChar(out Char value) ? info is null ? ToUpper(value) : ToUpper(value, info) : character;
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.ToUpper(Rune,CultureInfo)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rune ToUpper(this Rune character, CultureInfo? info)
        {
            return info is null ? ToUpper(character) : Rune.ToUpper(character, info);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.ToUpperInvariant(character);
#else
            return character.ToChar(out Char value) ? ToUpperInvariant(value) : character;
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.ToUpperInvariant(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rune ToUpperInvariant(this Rune character)
        {
            return Rune.ToUpperInvariant(character);
        }
#endif

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
#if NETCOREAPP3_1_OR_GREATER
            return Rune.GetUnicodeCategory(character);
#else
            return character.ToChar(out Char value) ? GetUnicodeCategory(value) : UnicodeCategory.OtherNotAssigned;
#endif
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <inheritdoc cref="Rune.GetUnicodeCategory(Rune)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCategory GetUnicodeCategory(this Rune character)
        {
            return Rune.GetUnicodeCategory(character);
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToChar(this Boolean value)
        {
            return value ? 'T' : 'F';
        }

#if NETCOREAPP3_1_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char32 GetChar32At(this String value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Rune.GetRuneAt(value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetChar32At(this String value, Int32 index, out Char32 result)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (Rune.TryGetRuneAt(value, index, out Rune rune))
            {
                result = rune;
                return true;
            }

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rune GetRuneAt(this String value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Rune.GetRuneAt(value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetRuneAt(this String value, Int32 index, out Rune result)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Rune.TryGetRuneAt(value, index, out result);
        }
#endif
    }
}