// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Utils.Numerics;
using NetExtender.Exceptions;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Utils.Types
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class ConvertUtils
    {
        public static T CastConvert<T>(this Object obj)
        {
            if (TryConvert(obj, out T value))
            {
                return value;
            }

            throw new InvalidCastException();
        }

        public static T CastConvert<T>(this String input)
        {
            return CastConvert<T>(input, CultureInfo.InvariantCulture);
        }
        
        public static T CastConvert<T>(this String input, CultureInfo info)
        {
            if (TryConvert(input, out T value))
            {
                return value;
            }

            throw new InvalidCastException();
        }
        
        public static T Convert<T>(this Object obj)
        {
            TryConvert(obj, out T value);
            return value;
        }

        public static T Convert<T>(this String input)
        {
            return Convert<T>(input, CultureInfo.InvariantCulture);
        }
        
        public static T Convert<T>(this String input, CultureInfo info)
        {
            TryConvert(input, info, out T value);
            return value;
        }
        
        public static IEnumerable<T> CastConvert<T>(this String input, String separator)
        {
            return CastConvert<T>(input, separator, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> CastConvert<T>(this String input, String separator, CultureInfo info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : CastConvert<T>(input.Split(separator, StringSplitOptions.RemoveEmptyEntries), info);
        }

        public static IEnumerable<T> CastConvert<T>(this String input, String[] separators, CultureInfo info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : CastConvert<T>(input.Split(separators, StringSplitOptions.RemoveEmptyEntries), info);
        }
        
        public static IEnumerable<T> Convert<T>(this String input, String separator)
        {
            return Convert<T>(input, separator, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> Convert<T>(this String input, String separator, CultureInfo info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : Convert<T>(input.Split(separator, StringSplitOptions.RemoveEmptyEntries), info);
        }

        public static IEnumerable<T> Convert<T>(this String input, String[] separators, CultureInfo info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : Convert<T>(input.Split(separators, StringSplitOptions.RemoveEmptyEntries), info);
        }
        
        public static IEnumerable<T> TryConvert<T>(this String input, String separator)
        {
            return TryConvert<T>(input, separator, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> TryConvert<T>(this String input, String separator, CultureInfo info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : TryConvert<T>(input.Split(separator, StringSplitOptions.RemoveEmptyEntries), info);
        }
        
        public static IEnumerable<T> TryConvert<T>(this String input, String[] separators, CultureInfo info)
        {
            return String.IsNullOrEmpty(input) ? Enumerable.Empty<T>() : TryConvert<T>(input.Split(separators, StringSplitOptions.RemoveEmptyEntries), info);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<T> Convert<T>(this IEnumerable source)
        {
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
        
        public static IEnumerable<T> CastConvert<T>(this IEnumerable<String> source, CultureInfo info)
        {
            return source.Select(value => value.CastConvert<T>(info));
        }

        public static IEnumerable<T> Convert<T>(this IEnumerable<String> source)
        {
            return Convert<T>(source, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> Convert<T>(this IEnumerable<String> source, CultureInfo info)
        {
            return source.Select(value => value.Convert<T>(info));
        }

        public static IEnumerable<T> TryConvert<T>(this IEnumerable<String> source)
        {
            return TryConvert<T>(source, CultureInfo.InvariantCulture);
        }
        
        public static IEnumerable<T> TryConvert<T>(this IEnumerable<String> source, CultureInfo info)
        {
            Boolean TryConvertInner(String input, out T result)
            {
                return TryConvert(input, info, out result);
            }
            
            return source.TryParse<String, T>(TryConvertInner);
        }

        #region DecimalConvert

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToSByte(this Decimal value)
        {
            return System.Convert.ToSByte(value.ToRange(SByte.MinValue, SByte.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToByte(this Decimal value)
        {
            return value >= 0
                ? System.Convert.ToByte(value.ToRange(Byte.MinValue, Byte.MaxValue))
                : ConvertToUnsigned(ToSByte(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToInt16(this Decimal value)
        {
            return System.Convert.ToInt16(value.ToRange(Int16.MinValue, Int16.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToUInt16(this Decimal value)
        {
            return value >= 0
                ? System.Convert.ToUInt16(value.ToRange(UInt16.MinValue, UInt16.MaxValue))
                : ConvertToUnsigned(ToInt16(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToInt32(this Decimal value)
        {
            return System.Convert.ToInt32(value.ToRange(Int32.MinValue, Int32.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToUInt32(this Decimal value)
        {
            return value >= 0
                ? System.Convert.ToUInt32(value.ToRange(UInt32.MinValue, UInt32.MaxValue))
                : ConvertToUnsigned(ToInt32(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToInt64(this Decimal value)
        {
            return System.Convert.ToInt64(value.ToRange(Int64.MinValue, Int64.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToUInt64(this Decimal value)
        {
            return value >= 0
                ? System.Convert.ToUInt64(value.ToRange(UInt64.MinValue, UInt64.MaxValue))
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

        public static Byte[] ToBytes(this String input)
        {
            return ToBytes(input, Encoding.UTF8);
        }

        public static Byte[] ToBytes(this String input, Encoding encoding)
        {
            return input is null ? null : encoding.GetBytes(input);
        }

        #region ToString

        public static String GetString<T>(this T value)
        {
            return GetString(value, CultureInfo.InvariantCulture);
        }

        public static String GetString<T>(this T value, IFormatProvider provider, Boolean escape = false)
        {
            return value switch
            {
                null => null,
                Char chr => escape ? $"\'{chr.GetString(provider)}\'" : chr.GetString(provider),
                String str => escape ? $"\"{str.GetString(provider)}\"" : str.GetString(provider),
                IEnumerable enumerable => enumerable.GetString(provider),
                IFormattable formattable => formattable.ToString(null, provider),
                IConvertible convertible => convertible.ToString(provider),
                _ => value.ToString()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Boolean value)
        {
            return value.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Boolean value, IFormatProvider provider)
        {
            return value.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char value)
        {
            return Char.ToString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char value, IFormatProvider provider)
        {
            return Char.ToString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this SByte value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this SByte value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Byte value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Byte value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int16 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int16 value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt16 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt16 value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int32 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int32 value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt32 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt32 value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int64 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int64 value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt64 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt64 value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Single value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Single value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Double value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Double value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Decimal value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Decimal value, IFormatProvider provider)
        {
            return value.Normalize().ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this DateTime value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this DateTime value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this String value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this String value, IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this IEnumerable<T> source)
        {
            return GetString(source, CultureInfo.InvariantCulture);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static String GetString(this IEnumerable source, IFormatProvider provider)
        {
            return source switch
            {
                null => null,
                String str => str.GetString(provider),
                IDictionary dictionary => dictionary.GetString(provider),
                ISet set => set.GetString(provider),
                IEnumerable<IEnumerable> jagged => jagged.GetString(provider),

                _ => $"[{String.Join(", ", source.Cast<Object>().Select(item => item.GetString(provider, true)))}]"
            };
        }

        private static String GetString(this IDictionary dictionary, IFormatProvider provider)
        {
            return $"{{{String.Join(", ", dictionary.Keys.Cast<Object>().Select(key => $"{key.GetString(provider, true)} : {dictionary[key].GetString(provider, true)}"))}}}";
        }

        private static String GetString(this ISet set, IFormatProvider provider)
        {
            return $"{{{String.Join(", ", set.Cast<Object>().Select(item => item.GetString(provider, true)))}}}";
        }

        private static String GetString(this IEnumerable<IEnumerable> jagged, IFormatProvider provider)
        {
            return $"[{String.Join(", ", jagged.Select(e => e.GetString(provider)))}]";
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvert<T>(this String input, CultureInfo info, out T result)
        {
            return TryConvert(input, info, null, out result);
        }

        public static Boolean TryConvert<T>(this String input, CultureInfo info, ITypeDescriptorContext context, out T result)
        {
            if (typeof(T) == typeof(Boolean))
            {
                Boolean convert = ToBoolean(input);
                result = Unsafe.As<Boolean, T>(ref convert);
                return true;
            }
            
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

                result = (T) converter.ConvertFromString(context, info, input);

                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryConvert<TInput, TOutput>(this TInput input, out TOutput result)
        {
            if (typeof(TOutput) == typeof(Boolean))
            {
                Boolean convert = ToBoolean(input);
                result = Unsafe.As<Boolean, TOutput>(ref convert);
                return true;
            }
            
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(TOutput));

                result = converter.ConvertFrom(input) is TOutput output ? output : default;

                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean ToBoolean<T>(this T obj)
        {
            if (typeof(T).IsValueType)
            {
                return !EqualityComparer<T>.Default.Equals(obj, default);
            }
            
            return obj switch
            {
                null => false,
                String str => ToBoolean(str),
                ICollection collection => ToBoolean(collection),
                _ => !obj.Equals(default(T))
            };
        }

        public static Boolean ToBoolean(this String str)
        {
            return str?.ToUpper() switch
            {
                "TRUE" => true,
                "T" => true,
                "+" => true,
                "1" => true,
                _ => false
            };
        }

        public static Boolean ToBoolean(this ICollection collection)
        {
            return collection?.Count > 0;
        }
        
        public static String GetStringFromBytes(this ReadOnlySpan<Byte> data)
        {
            return GetStringFromBytes(data, Encoding.UTF8);
        }

        public static String GetStringFromBytes(this ReadOnlySpan<Byte> data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        public static String GetStringFromBytes(this Byte[] data)
        {
            return GetStringFromBytes(data, Encoding.UTF8);
        }
        
        public static String GetStringFromBytes(this Byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        public static T Clone<T>(this T value)
        {
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