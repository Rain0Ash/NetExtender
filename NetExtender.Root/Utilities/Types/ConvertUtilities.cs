using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class ConvertUtilities
    {
        public const EscapeType DefaultEscapeType = EscapeType.Null;

        [return: NotNullIfNotNull("value")]
        public static Object? ChangeType(Object? value, Type type)
        {
            return System.Convert.ChangeType(value, type);
        }

        [return: NotNullIfNotNull("value")]
        public static Object? ChangeType(Object? value, Type type, IFormatProvider? provider)
        {
            return System.Convert.ChangeType(value, type, provider);
        }

        [return: NotNullIfNotNull("value")]
        public static Object? ChangeType(Object? value, TypeCode type)
        {
            return System.Convert.ChangeType(value, type);
        }

        [return: NotNullIfNotNull("value")]
        public static Object? ChangeType(Object? value, TypeCode type, IFormatProvider? provider)
        {
            return System.Convert.ChangeType(value, type, provider);
        }

        [return: NotNullIfNotNull("value")]
        public static T? ChangeType<T>(Object? value)
        {
            return (T?) System.Convert.ChangeType(value, typeof(T));
        }

        [return: NotNullIfNotNull("value")]
        public static T? ChangeType<T>(Object? value, IFormatProvider? provider)
        {
            return (T?) System.Convert.ChangeType(value, typeof(T), provider);
        }

        // ReSharper disable once RedundantNullableFlowAttribute
        public static Boolean TryChangeType(Object? value, Type type, [MaybeNullWhen(false)] [NotNullIfNotNull("value")] out Object? result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                result = System.Convert.ChangeType(value, type);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        // ReSharper disable once RedundantNullableFlowAttribute
        public static Boolean TryChangeType(Object? value, Type type, IFormatProvider? provider, [MaybeNullWhen(false)] [NotNullIfNotNull("value")] out Object? result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                result = System.Convert.ChangeType(value, type, provider);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        // ReSharper disable once RedundantNullableFlowAttribute
        public static Boolean TryChangeType(Object? value, TypeCode type, [MaybeNullWhen(false)] [NotNullIfNotNull("value")] out Object? result)
        {
            try
            {
                result = System.Convert.ChangeType(value, type);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        // ReSharper disable once RedundantNullableFlowAttribute
        public static Boolean TryChangeType(Object? value, TypeCode type, IFormatProvider? provider, [MaybeNullWhen(false)] [NotNullIfNotNull("value")] out Object? result)
        {
            try
            {
                result = System.Convert.ChangeType(value, type, provider);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public static Boolean TryChangeType<T>(Object? value, [NotNullIfNotNull("value")] out T? result)
        {
            if (TryChangeType(value, typeof(T), out Object? convert))
            {
                result = convert is T cast ? cast : default;
                return true;
            }

            result = default;
            return false;
        }

        public static Boolean TryChangeType<T>(Object? value, IFormatProvider? provider, [NotNullIfNotNull("value")] out T? result)
        {
            if (TryChangeType(value, typeof(T), provider, out Object? convert))
            {
                result = convert is T cast ? cast : default;
                return true;
            }

            result = default;
            return false;
        }

        public static Boolean IsChangeType(Object? value, Type type)
        {
            return TryChangeType(value, type, out _);
        }

        public static Boolean IsChangeType(Object? value, Type type, IFormatProvider? provider)
        {
            return TryChangeType(value, type, provider, out _);
        }

        public static Boolean IsChangeType(Object? value, TypeCode type)
        {
            return TryChangeType(value, type, out _);
        }

        public static Boolean IsChangeType(Object? value, TypeCode type, IFormatProvider? provider)
        {
            return TryChangeType(value, type, provider, out _);
        }

        public static Boolean IsChangeType<T>(Object? value)
        {
            return TryChangeType<T>(value, out _);
        }

        public static Boolean IsChangeType<T>(Object? value, IFormatProvider? provider)
        {
            return TryChangeType<T>(value, provider, out _);
        }

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

        public static T? Convert<T>(this Object? value)
        {
            TryConvert(value, out T? result);
            return result;
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

            foreach (String item in source)
            {
                if (TryConvert(item, info, out T? value))
                {
                    yield return value!;
                }
            }
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
            return value >= 0 ? System.Convert.ToByte(value.Clamp(Byte.MinValue, Byte.MaxValue)) : ConvertToUnsigned(ToSByte(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToInt16(this Decimal value)
        {
            return System.Convert.ToInt16(value.Clamp(Int16.MinValue, Int16.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToUInt16(this Decimal value)
        {
            return value >= 0 ? System.Convert.ToUInt16(value.Clamp(UInt16.MinValue, UInt16.MaxValue)) : ConvertToUnsigned(ToInt16(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToInt32(this Decimal value)
        {
            return System.Convert.ToInt32(value.Clamp(Int32.MinValue, Int32.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToUInt32(this Decimal value)
        {
            return value >= 0 ? System.Convert.ToUInt32(value.Clamp(UInt32.MinValue, UInt32.MaxValue)) : ConvertToUnsigned(ToInt32(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToInt64(this Decimal value)
        {
            return System.Convert.ToInt64(value.Clamp(Int64.MinValue, Int64.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToUInt64(this Decimal value)
        {
            return value >= 0 ? System.Convert.ToUInt64(value.Clamp(UInt64.MinValue, UInt64.MaxValue)) : ConvertToUnsigned(ToInt64(value));
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
        public static TOutput Convert<T, TOutput>(this TryParseHandler<T, TOutput> converter, T input, TOutput alternate)
        {
            return Convert(input, converter, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput Convert<T, TOutput>(this T input, TryParseHandler<T, TOutput> converter, TOutput alternate)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter.Invoke(input, out TOutput? result) ? result! : alternate;
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
        public static Boolean TryConvert<T>(this String? input, [MaybeNullWhen(false)] out T result)
        {
            return TryConvert(input, null, out result);
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
                Boolean convert = input.ToBoolean();
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
                Boolean convert = input.ToBoolean();
                result = Unsafe.As<Boolean, TOutput>(ref convert);
                return true;
            }

            if (input is null)
            {
                result = default;
                return false;
            }

            if (input is String value)
            {
                return value.TryConvert(out result);
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

        public static Byte[] ToBytes(this String? input)
        {
            return ToBytes(input, Encoding.UTF8);
        }

        public static Byte[] ToBytes(this String? input, Encoding encoding)
        {
            return input is not null ? encoding.GetBytes(input) : Array.Empty<Byte>();
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
    }
}