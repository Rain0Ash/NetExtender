// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Exceptions;
using NetExtender.Types.Collections;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    [Flags]
    public enum EscapeType
    {
        None,
        Null = 1,
        Full = 2,
        FullWithNull = Null | Full
    }
    
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class ConvertUtilities
    {
        public const EscapeType DefaultEscapeType = EscapeType.Null;

        public static T CastConvert<T>(this Object? value)
        {
            if (!TryConvert(value, out T? result))
            {
                throw new InvalidCastException();
            }

            return result!;
        }

        public static T CastConvert<T>(this String? input)
        {
            return CastConvert<T>(input, CultureInfo.InvariantCulture);
        }
        
        public static T CastConvert<T>(this String? input, CultureInfo? info)
        {
            if (!TryConvert(input, out T? result))
            {
                throw new InvalidCastException();
            }

            return result!;
        }
        
        public static T? Convert<T>(this Object? obj)
        {
            TryConvert(obj, out T? value);
            return value;
        }

        public static T? Convert<T>(this String? input)
        {
            return Convert<T>(input, CultureInfo.InvariantCulture);
        }
        
        public static T? Convert<T>(this String? input, CultureInfo? info)
        {
            TryConvert(input, info, out T? value);
            return value;
        }
        
        public static IEnumerable<T> CastConvert<T>(this String? input, String? separator)
        {
            return CastConvert<T>(input, separator, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> CastConvert<T>(this String? input, String? separator, CultureInfo? info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : CastConvert<T>(input.Split(separator, StringSplitOptions.RemoveEmptyEntries), info);
        }

        public static IEnumerable<T> CastConvert<T>(this String? input, String[]? separators, CultureInfo? info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : CastConvert<T>(input.Split(separators, StringSplitOptions.RemoveEmptyEntries), info);
        }
        
        public static IEnumerable<T?> Convert<T>(this String? input, String? separator)
        {
            return Convert<T>(input, separator, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T?> Convert<T>(this String? input, String? separator, CultureInfo? info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : Convert<T>(input.Split(separator, StringSplitOptions.RemoveEmptyEntries), info);
        }

        public static IEnumerable<T?> Convert<T>(this String? input, String[]? separators, CultureInfo? info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : Convert<T>(input.Split(separators, StringSplitOptions.RemoveEmptyEntries), info);
        }
        
        public static IEnumerable<T> TryConvert<T>(this String? input, String? separator)
        {
            return TryConvert<T>(input, separator, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> TryConvert<T>(this String? input, String? separator, CultureInfo? info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : TryConvert<T>(input.Split(separator, StringSplitOptions.RemoveEmptyEntries), info);
        }
        
        public static IEnumerable<T> TryConvert<T>(this String? input, String[]? separators, CultureInfo? info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : TryConvert<T>(input.Split(separators, StringSplitOptions.RemoveEmptyEntries), info);
        }

        public static IEnumerable<T?> Convert<T>(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IEnumerable<T> enumerable => enumerable,
                IEnumerable<String> enumerable => Convert<T>(enumerable),
                _ => source.Cast<T>()
            };
        }
        
        public static IEnumerable<T> CastConvert<T>(this IEnumerable<String> source)
        {
            return CastConvert<T>(source, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> CastConvert<T>(this IEnumerable<String> source, CultureInfo? info)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(value => value.CastConvert<T>(info));
        }

        public static IEnumerable<T?> Convert<T>(this IEnumerable<String> source)
        {
            return Convert<T>(source, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T?> Convert<T>(this IEnumerable<String> source, CultureInfo? info)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(value => value.Convert<T>(info));
        }

        public static IEnumerable<T> TryConvert<T>(this IEnumerable<String> source)
        {
            return TryConvert<T>(source, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> TryConvert<T>(this IEnumerable<String> source, CultureInfo? info)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Boolean TryConvertInner(String? input, [MaybeNullWhen(false)] out T result)
            {
                return TryConvert(input, info, out result);
            }
            
            return source.TryParse<String?, T>(TryConvertInner);
        }

        #region DecimalConvert

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToSByte(this Decimal value)
        {
            return System.Convert.ToSByte(value.Clamp(SByte.MinValue, SByte.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToByte(this Decimal value)
        {
            return value >= 0
                ? System.Convert.ToByte(value.Clamp(Byte.MinValue, Byte.MaxValue))
                : ConvertToUnsigned(ToSByte(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToInt16(this Decimal value)
        {
            return System.Convert.ToInt16(value.Clamp(Int16.MinValue, Int16.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToUInt16(this Decimal value)
        {
            return value >= 0
                ? System.Convert.ToUInt16(value.Clamp(UInt16.MinValue, UInt16.MaxValue))
                : ConvertToUnsigned(ToInt16(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToInt32(this Decimal value)
        {
            return System.Convert.ToInt32(value.Clamp(Int32.MinValue, Int32.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToUInt32(this Decimal value)
        {
            return value >= 0
                ? System.Convert.ToUInt32(value.Clamp(UInt32.MinValue, UInt32.MaxValue))
                : ConvertToUnsigned(ToInt32(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToInt64(this Decimal value)
        {
            return System.Convert.ToInt64(value.Clamp(Int64.MinValue, Int64.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToUInt64(this Decimal value)
        {
            return value >= 0
                ? System.Convert.ToUInt64(value.Clamp(UInt64.MinValue, UInt64.MaxValue))
                : ConvertToUnsigned(ToInt64(value));
        }

        #endregion

        #region UnsignedTypeConvert

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ConvertToUnsigned(this SByte value)
        {
            unchecked
            {
                if (value >= 0)
                {
                    return (Byte) value;
                }

                return (Byte) (value + SByte.MaxValue);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ConvertToUnsigned(this Int16 value)
        {
            unchecked
            {
                if (value >= 0)
                {
                    return (UInt16) value;
                }

                return (UInt16) (value + Int16.MaxValue);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ConvertToUnsigned(this Int32 value)
        {
            unchecked
            {
                if (value >= 0)
                {
                    return (UInt32) value;
                }

                return (UInt32) (value + Int32.MaxValue);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ConvertToUnsigned(this Int64 value)
        {
            unchecked
            {
                if (value >= 0)
                {
                    return (UInt64) value;
                }

                return (UInt64) (value + Int64.MaxValue);
            }
        }

        #endregion

        public static Byte[] ToBytes(this String? input)
        {
            return ToBytes(input, Encoding.UTF8);
        }

        public static Byte[] ToBytes(this String? input, Encoding encoding)
        {
            return input is not null ? encoding.GetBytes(input) : Array.Empty<Byte>();
        }

        #region ToString

        public static IEnumerable<String?> ToStringEnumerable<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(StringUtilities.ToString);
        }
        
        public static IEnumerable<String?> ToStringEnumerable<T>(this IEnumerable<T> source, IFormatProvider? provider)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => StringUtilities.ToString(item, provider));
        }
        
        public static IEnumerable<String?> ToStringEnumerable<T>(this IEnumerable<T> source, String? format, IFormatProvider? provider)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => StringUtilities.ToString(item, format, provider));
        }
        
        public static IEnumerable<String?> GetStringEnumerable<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(GetString);
        }
        
        public static IEnumerable<String?> GetStringEnumerable<T>(this IEnumerable<T> source, IFormatProvider? provider)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => GetString(item, provider));
        }

        public static String? GetString<T>(this T value)
        {
            return GetString(value, EscapeType.None);
        }
        
        public static String? GetStringEscape<T>(T value)
        {
            return GetString(value, EscapeType.FullWithNull);
        }
        
        public static String? GetString<T>(this T value, EscapeType escape)
        {
            return GetString(value, escape, CultureInfo.InvariantCulture);
        }

        public static String? GetString<T>(this T value, IFormatProvider? provider)
        {
            return GetString(value, EscapeType.None, provider);
        }

        public static String? GetStringEscape<T>(T value, IFormatProvider? provider)
        {
            return GetString(value, EscapeType.FullWithNull, provider);
        }

        public static String? GetString<T>(this T value, EscapeType escape, IFormatProvider? provider)
        {
            return value switch
            {
                null => escape.HasFlag(EscapeType.Null) ? StringUtilities.NullString : null,
                Char character => escape.HasFlag(EscapeType.Full) ? $"\'{character.GetString(provider)}\'" : character.GetString(provider),
                String str => escape.HasFlag(EscapeType.Full) ? $"\"{str.GetString(provider)}\"" : str.GetString(provider),
                Boolean number => number.GetString(provider),
                SByte number => number.GetString(provider),
                Byte number => number.GetString(provider),
                Int16 number => number.GetString(provider),
                UInt16 number => number.GetString(provider),
                Int32 number => number.GetString(provider),
                UInt32 number => number.GetString(provider),
                Int64 number => number.GetString(provider),
                UInt64 number => number.GetString(provider),
                Single number => number.GetString(provider),
                Double number => number.GetString(provider),
                Decimal number => number.GetString(provider),
                BigInteger number => number.GetString(provider),
                DateTime number => number.GetString(provider),
                TimeSpan number => number.GetString(provider),
                Enum number => number.GetString(escape),
                IString str => escape.HasFlag(EscapeType.Full) ? $"\"{str.ToString(provider)}\"" : str.ToString(provider),
                IEnumerable enumerable => enumerable.GetString(escape, provider),
                IFormattable formattable => formattable.ToString(null, provider),
                IConvertible convertible => convertible.ToString(provider),
                _ => ToStringUnknown(value, escape, provider)
            };
        }

        private static String? GetStringUnknown(Object? value, EscapeType escape, IFormatProvider? provider)
        {
            return GetStringUnknownInternal(value, escape, provider, out String? result) ? result : value.GetString(escape, provider);
        }
        
        private static String? ToStringUnknown(Object? value, EscapeType escape, IFormatProvider? provider)
        {
            return GetStringUnknownInternal(value, escape, provider, out String? result) ? result : value?.ToString();
        }
        
        private static Boolean GetStringUnknownInternal(Object? value, EscapeType escape, IFormatProvider? provider, [MaybeNullWhen(false)] out String result)
        {
            if (value is null)
            {
                result = default;
                return false;
            }
            
            if (value.GetType().TryGetGenericTypeDefinition() == KeyValuePairType)
            {
                dynamic item = value;
                result = $"{{{((Object) item.Key).GetString(escape, provider)} : {((Object) item.Value).GetString(escape, provider)}}}";
                return true;
            }

            if (value is DictionaryEntry entry)
            {
                result = $"{{{entry.Key.GetString(escape, provider)} : {entry.Value.GetString(escape, provider)}}}";
                return true;
            }

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Boolean value)
        {
            return value.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Boolean value, IFormatProvider? provider)
        {
            return value.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char value)
        {
            return Char.ToString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char value, IFormatProvider? provider)
        {
            return Char.ToString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this SByte value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this SByte value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Byte value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Byte value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int16 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int16 value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt16 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt16 value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int32 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int32 value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt32 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt32 value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int64 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int64 value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt64 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt64 value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Single value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Single value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Double value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Double value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Decimal value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Decimal value, IFormatProvider? provider)
        {
            return value.Normalize().ToString(provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this BigInteger value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this BigInteger value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this DateTime value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this DateTime value, IFormatProvider? provider)
        {
            return value.ToString(provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this TimeSpan value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this TimeSpan value, IFormatProvider? provider)
        {
            return value.ToString();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value)
        {
            return GetStringEscape(value?.ToString());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value, EscapeType escape)
        {
            return value is not null ? GetStringEscape(escape.HasFlag(EscapeType.Full) ? $"{value.GetType().Name}.{value.ToString()}" : value.ToString()) : StringUtilities.NullString;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum value, IFormatProvider? provider)
        {
            return GetString(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum value, EscapeType escape, IFormatProvider? provider)
        {
            return GetString(value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this String? value)
        {
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetStringEscape(this String? value)
        {
            return value ?? StringUtilities.NullString;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this String value, EscapeType escape)
        {
            return escape.HasFlag(EscapeType.Null) ? GetStringEscape(value) : GetString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this String? value, IFormatProvider? provider)
        {
            return value?.ToString(provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetStringEscape(String? value, IFormatProvider? provider)
        {
            return value?.ToString(provider) ?? StringUtilities.NullString;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this String? value, IFormatProvider? provider, EscapeType escape)
        {
            return escape.HasFlag(EscapeType.Null) ? GetStringEscape(value, provider) : GetString(value, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this IEnumerable source)
        {
            return GetString(source, EscapeType.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape(IEnumerable source)
        {
            return GetString(source, EscapeType.FullWithNull);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this IEnumerable source, EscapeType escape)
        {
            return GetString(source, escape, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this IEnumerable source, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape(IEnumerable source, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.FullWithNull, provider);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static String? GetString(this IEnumerable? source, EscapeType escape, IFormatProvider? provider)
        {
            switch (source)
            {
                case null:
                    return escape.HasFlag(EscapeType.Null) ? StringUtilities.NullString : null;
                case String result:
                    return escape.HasFlag(EscapeType.Full) ? $"\"{result.GetString(provider)}\"" : result.GetString(provider);
                default:
                    switch (source.GetCollectionType())
                    {
                        case CollectionType.Set:
                        case CollectionType.GenericSet:
                            return source.SetGetString(escape, provider);
                        case CollectionType.Dictionary:
                        case CollectionType.GenericDictionary:
                            return source.DictionaryGetString(escape, provider);
                        default:
                            if (source is IEnumerable<IEnumerable> jagged)
                            {
                                return jagged.JaggedGetString(escape, provider);
                            }
                    
                            return source.EnumerableGetString(escape, provider);
                    }
            }
        }

        private static Type KeyValuePairType { get; } = typeof(KeyValuePair<,>);

        private static IEnumerable<String?> PairGetString(this IEnumerable dictionary, EscapeType escape, IFormatProvider? provider)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            foreach (Object? obj in dictionary)
            {
                yield return GetStringUnknown(obj, escape, provider);
            }
        }

        private static String DictionaryGetString(this IEnumerable dictionary, EscapeType escape, IFormatProvider? provider)
        {
            return $"{{{String.Join(", ", dictionary.PairGetString(escape, provider))}}}";
        }

        private static String SetGetString(this IEnumerable set, EscapeType escape, IFormatProvider? provider)
        {
            return $"{{{String.Join(", ", set.Cast<Object>().Select(item => item.GetString(escape, provider)))}}}";
        }

        private static String JaggedGetString(this IEnumerable<IEnumerable> jagged, EscapeType escape, IFormatProvider? provider)
        {
            return $"[{String.Join(", ", jagged.Select(e => e.GetString(escape, provider)))}]";
        }

        private static String EnumerableGetString(this IEnumerable source, EscapeType escape, IFormatProvider? provider)
        {
            return $"[{String.Join(", ", source.Cast<Object>().Select(item => item.GetString(escape, provider)))}]";
        }

        #endregion
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this ParseHandler<T, TOutput> converter, T input)
        {
            return Convert(input, converter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this T input, ParseHandler<T, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter.Invoke(input);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput? Convert<T, TOutput>(this TryParseHandler<T, TOutput> converter, T input)
        {
            return Convert(input, converter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput? Convert<T, TOutput>(this T input, TryParseHandler<T, TOutput> converter)
        {
            return Convert(input, converter!, default(TOutput));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this TryParseHandler<T, TOutput> converter, T input, TOutput @default)
        {
            return Convert(input, converter, @default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this T input, TryParseHandler<T, TOutput> converter, TOutput @default)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter.Invoke(input, out TOutput? result) ? result! : @default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this TryParseHandler<T, TOutput> converter, T input, Func<TOutput> generator)
        {
            return Convert(input, converter, generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this T input, TryParseHandler<T, TOutput> converter, Func<TOutput> generator)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            return converter.Invoke(input, out TOutput? result) ? result! : generator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this TryParseHandler<T, TOutput> converter, T input, Func<T, TOutput> generator)
        {
            return Convert(input, converter, generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this T input, TryParseHandler<T, TOutput> converter, Func<T, TOutput> generator)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            return converter.Invoke(input, out TOutput? result) ? result! : generator(input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Convert<T, TOutput>(this TryParseHandler<T, TOutput> converter, T input, out TOutput result)
        {
            return Convert(input, converter, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Convert<T, TOutput>(this T input, TryParseHandler<T, TOutput> converter, out TOutput result)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter.Invoke(input, out result!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvert<T>(this String? input, CultureInfo? info, [MaybeNullWhen(false)] out T result)
        {
            return TryConvert(input, info, null, out result);
        }

        // ReSharper disable once CognitiveComplexity
        public static Boolean TryConvert<T>(this String? input, CultureInfo? info, ITypeDescriptorContext? context, [MaybeNullWhen(false)] out T result)
        {
            if (input is null)
            {
                result = default;
                return false;
            }
            
            if (typeof(T) == typeof(String))
            {
                result = Unsafe.As<String, T>(ref input);
                return true;
            }
            
            if (typeof(T) == typeof(Boolean))
            {
                Boolean convert = ToBoolean(input);
                result = Unsafe.As<Boolean, T>(ref convert);
                return true;
            }
            
            try
            {
                try
                {
                    if (System.Convert.ChangeType(input, typeof(T)) is T value)
                    {
                        result = value;
                        return true;
                    }

                    result = default;
                    return false;
                }
                catch (Exception)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

                    if (context is null)
                    {
                        if (converter.ConvertFromInvariantString(input) is T invariantresult)
                        {
                            result = invariantresult;
                            return true;
                        }

                        result = default;
                        return false;
                    }

                    if (info is not null)
                    {
                        if (converter.ConvertFromString(context, info, input) is T fullresult)
                        {
                            result = fullresult;
                            return true;
                        }
                    }
                        
                    if (converter.ConvertFromString(context, input) is T contextresult)
                    {
                        result = contextresult;
                        return true;
                    }
                        
                    result = default;
                    return false;
                }
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryConvert<TInput, TOutput>(this TInput input, [MaybeNullWhen(false)] out TOutput result)
        {
            if (typeof(TInput) == typeof(TOutput))
            {
                result = Unsafe.As<TInput, TOutput>(ref input);
                return true;
            }
            
            if (typeof(TOutput) == typeof(Boolean))
            {
                Boolean convert = ToBoolean(input);
                result = Unsafe.As<Boolean, TOutput>(ref convert);
                return true;
            }

            if (input is null)
            {
                result = default;
                return false;
            }

            try
            {
                try
                {
                    result = (TOutput) System.Convert.ChangeType(input, typeof(TOutput));
                    return true;
                }
                catch (Exception)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(TOutput));

                    if (converter.ConvertFrom(input) is TOutput output)
                    {
                        result = output;
                        return true;
                    }

                    result = default;
                    return false;
                }
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean ToBoolean<T>(this T value)
        {
            if (typeof(T).IsValueType)
            {
                return !EqualityComparer<T>.Default.Equals(value, default);
            }
            
            return value switch
            {
                null => false,
                String str => ToBoolean(str),
                ICollection collection => ToBoolean(collection),
                _ => !value.Equals(default(T))
            };
        }

        public static Boolean ToBoolean(this String? value)
        {
            if (value is null)
            {
                return false;
            }

            if (value.Length != 1 || value.Length != 4)
            {
                return false;
            }
            
            return value?.ToUpper() switch
            {
                "TRUE" => true,
                "T" => true,
                "+" => true,
                "1" => true,
                _ => false
            };
        }

        public static Boolean ToBoolean(this ICollection? collection)
        {
            return collection?.Count > 0;
        }
        
        public static String GetStringFromBytes(this ReadOnlySpan<Byte> data)
        {
            return GetStringFromBytes(data, Encoding.UTF8);
        }

        public static String GetStringFromBytes(this ReadOnlySpan<Byte> data, Encoding? encoding)
        {
            encoding ??= Encoding.UTF8;
            return encoding.GetString(data);
        }

        public static String GetStringFromBytes(this Byte[] data)
        {
            return GetStringFromBytes(data, Encoding.UTF8);
        }
        
        public static String GetStringFromBytes(this Byte[] data, Encoding? encoding)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            encoding ??= Encoding.UTF8;
            return encoding.GetString(data);
        }

        public static T? Clone<T>(this T? value)
        {
            if (value is null)
            {
                return default;
            }
            
            if (value.Equals(default))
            {
                return default;
            }

            return value switch
            {
                ICloneable cloneable => cloneable.Clone<T>(),
                _ => value.DeepCopy()
            };
        }

        public static T Clone<T>(this ICloneable cloneable)
        {
            if (cloneable.Clone() is T clone)
            {
                return clone;
            }

            throw new CloneException($"{cloneable.GetType()} is not {typeof(T)}");
        }
    }
}