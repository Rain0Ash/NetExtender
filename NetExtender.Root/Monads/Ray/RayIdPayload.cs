using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using NetExtender.Exceptions;
using NetExtender.Interfaces;
using NetExtender.Types.Enums;
using NetExtender.Types.Enums.Interfaces;

namespace NetExtender.Monads
{
    [Serializable]
    public abstract partial class RayIdPayload : IReadOnlyDictionary<String, RayIdPayload.Element>, ISerializable, ISpanFormattable, ICloneable, ICloneable<RayIdPayload>
    {
        internal const Int32 Buffer = 2048;

        public abstract Int32 Count { get; }
        public abstract IEnumerable<String> Keys { get; }
        public abstract IEnumerable<Element> Values { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdPayload New()
        {
            return New(Many.Comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdPayload New(StringComparer? comparer)
        {
            return new Zero(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdPayload New(Int32 capacity)
        {
            return New(capacity, Many.Comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdPayload New(Int32 capacity, StringComparer? comparer)
        {
            return new Many(capacity, comparer);
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdPayload New(SerializationInfo info, StreamingContext context)
        {
            return new Many(info, context);
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);

        public static Boolean Allow<T>() where T : notnull
        {
            return Element.Allow<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        protected static Boolean ValidateKey(ReadOnlySpan<Char> key)
        {
            if (key.Length is <= 0 or > 256)
            {
                return false;
            }

            if (Char.IsWhiteSpace(key[0]) || Char.IsWhiteSpace(key[^1]))
            {
                return false;
            }

            Int32 at = key.IndexOf('@');
            if (at < 0)
            {
                if (!IsAlpha(key[0]))
                {
                    return false;
                }

                for (Int32 i = 1; i < key.Length; i++)
                {
                    if (!IsKeyChar(key[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            if (key[(at + 1)..].IndexOf('@') >= 0)
            {
                return false;
            }

            ReadOnlySpan<Char> tenant = key[..at];
            ReadOnlySpan<Char> system = key[(at + 1)..];

            if (tenant.Length is < 1 or > 241)
            {
                return false;
            }

            if (system.Length is < 1 or > 14)
            {
                return false;
            }

            if (!IsAlphaOrDigit(tenant[0]))
            {
                return false;
            }

            for (Int32 i = 1; i < tenant.Length; i++)
            {
                if (!IsKeyChar(tenant[i]))
                {
                    return false;
                }
            }

            if (!IsAlpha(system[0]))
            {
                return false;
            }

            for (Int32 i = 1; i < system.Length; i++)
            {
                if (!IsKeyChar(system[i]))
                {
                    return false;
                }
            }

            return true;

            static Boolean IsAlpha(Char character)
            {
                return character is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
            }

            static Boolean IsAlphaOrDigit(Char character)
            {
                return IsAlpha(character) || (character is >= '0' and <= '9');
            }

            static Boolean IsKeyChar(Char character)
            {
                return IsAlpha(character) || (character is >= '0' and <= '9') || character is '_' or '-' or '*' or '/';
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected static Boolean ValidateValue(ReadOnlySpan<Char> value)
        {
            if (value.Length is <= 0 or > 256 || value[^1] == ' ')
            {
                return false;
            }

            foreach (Char character in value)
            {
                if (character < 0x20 || character > 0x7E)
                {
                    return false;
                }

                if (character is ',' or '=')
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, [MaybeNullWhen(false)] out RayIdPayload result)
        {
            return TryParse(value, CultureInfo.InvariantCulture, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static Boolean TryParse(ReadOnlySpan<Char> value, IFormatProvider? provider, [MaybeNullWhen(false)] out RayIdPayload result)
        {
            result = null;

            Int32 index = 0;
            while (index < value.Length)
            {
                while (index < value.Length && Char.IsWhiteSpace(value[index]))
                {
                    index++;
                }

                if (index >= value.Length)
                {
                    break;
                }

                Int32 start = index;
                while (index < value.Length && !Char.IsWhiteSpace(value[index]) && value[index] != '=')
                {
                    index++;
                }

                if (start == index)
                {
                    return false;
                }

                ReadOnlySpan<Char> name = value.Slice(start, index - start);

                while (index < value.Length && Char.IsWhiteSpace(value[index]))
                {
                    index++;
                }

                if (index >= value.Length || value[index] != '=')
                {
                    return false;
                }

                index++;

                while (index < value.Length && Char.IsWhiteSpace(value[index]))
                {
                    index++;
                }

                start = index;

                while (index < value.Length && value[index] != ',')
                {
                    index++;
                }

                Int32 end = index;

                while (end > start && Char.IsWhiteSpace(value[end - 1]))
                {
                    end--;
                }

                if (ValidateKey(name))
                {
                    result ??= New();
                    String key = name.ToString();
                    switch (Element.Selector(value.Slice(start, end - start), out ReadOnlySpan<Char> element, out Type? complex))
                    {
                        case Element.Type.Unknown:
                        {
                            break;
                        }
                        case Element.Type.String when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Enum when Element.Enum(complex) is { } handler && handler.Invoke(result, key, element, provider):
                        {
                            break;
                        }
                        case Element.Type.Enum when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Boolean when Element<Boolean>.DefaultParser.Invoke(element, provider, out Boolean convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Boolean when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Char when Element<Char>.DefaultParser.Invoke(element, provider, out Char convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Char when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
#if NETCOREAPP3_1_OR_GREATER
                        case Element.Type.Rune when Element<Rune>.DefaultParser.Invoke(element, provider, out Rune convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Rune when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
#endif
                        case Element.Type.Char32 when Element<Char32>.DefaultParser.Invoke(element, provider, out Char32 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Char32 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.IntPtr when Element<IntPtr>.DefaultParser.Invoke(element, provider, out IntPtr convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.IntPtr when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.UIntPtr when Element<UIntPtr>.DefaultParser.Invoke(element, provider, out UIntPtr convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.UIntPtr when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.SByte when Element<SByte>.DefaultParser.Invoke(element, provider, out SByte convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.SByte when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Byte when Element<Byte>.DefaultParser.Invoke(element, provider, out Byte convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Byte when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Int16 when Element<Int16>.DefaultParser.Invoke(element, provider, out Int16 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Int16 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.UInt16 when Element<UInt16>.DefaultParser.Invoke(element, provider, out UInt16 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.UInt16 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Int32 when Element<Int32>.DefaultParser.Invoke(element, provider, out Int32 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Int32 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.UInt32 when Element<UInt32>.DefaultParser.Invoke(element, provider, out UInt32 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.UInt32 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Int64 when Element<Int64>.DefaultParser.Invoke(element, provider, out Int64 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Int64 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.UInt64 when Element<UInt64>.DefaultParser.Invoke(element, provider, out UInt64 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.UInt64 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
#if NET7_0_OR_GREATER
                        case Element.Type.Int128 when Element<Int128>.DefaultParser.Invoke(element, provider, out Int128 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Int128 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.UInt128 when Element<UInt128>.DefaultParser.Invoke(element, provider, out UInt128 convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.UInt128 when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
#endif
                        case Element.Type.Half when Element<Half>.DefaultParser.Invoke(element, provider, out Half convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Half when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Single when Element<Single>.DefaultParser.Invoke(element, provider, out Single convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Single when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Double when Element<Double>.DefaultParser.Invoke(element, provider, out Double convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Double when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Decimal when Element<Decimal>.DefaultParser.Invoke(element, provider, out Decimal convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Decimal when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.BigInteger when Element<BigInteger>.DefaultParser.Invoke(element, provider, out BigInteger convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.BigInteger when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.Guid when Element<Guid>.DefaultParser.Invoke(element, provider, out Guid convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.Guid when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.TimeSpan when Element<TimeSpan>.DefaultParser.Invoke(element, provider, out TimeSpan convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.TimeSpan when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
#if NET6_0_OR_GREATER
                        case Element.Type.TimeOnly when Element<TimeOnly>.DefaultParser.Invoke(element, provider, out TimeOnly convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.TimeOnly when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.DateOnly when Element<DateOnly>.DefaultParser.Invoke(element, provider, out DateOnly convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.DateOnly when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
#endif
                        case Element.Type.DateTime when Element<DateTime>.DefaultParser.Invoke(element, provider, out DateTime convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.DateTime when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        case Element.Type.DateTimeOffset when Element<DateTimeOffset>.DefaultParser.Invoke(element, provider, out DateTimeOffset convert) is not null:
                        {
                            result.AddCore(key, convert);
                            break;
                        }
                        case Element.Type.DateTimeOffset when ValidateValue(element) && element.ToString() is { } convert:
                        {
                            result.AddCore(key, (Element) new StringElement(convert));
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }
                }

                if (index < value.Length && value[index] == ',')
                {
                    index++;
                }
                else if (index >= value.Length)
                {
                    break;
                }
                else
                {
                    return false;
                }
            }

            if (result is not null && result.Count > 0)
            {
                result.Reverse();
                return true;
            }

            result = default;
            return false;
        }

        public abstract Boolean ContainsKey([NotNullWhen(true)] String? key);
        public abstract Boolean TryGetValue([NotNullWhen(true)] String? key, [MaybeNullWhen(false)] out Element value);
        public abstract Boolean Unsafe([NotNullWhen(true)] String? key, Element element);
        protected abstract Boolean AddCore(String key, Element element);

        protected Boolean AddCore<T>(String key, T value) where T : notnull
        {
            return AddCore(key, (Element) new Element<T>(value));
        }

        public Boolean Add([NotNullWhen(true)] String? key, String value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && ValidateValue(value) && AddCore(key, (Element) new StringElement(value));
        }

        public Boolean Add([NotNullWhen(true)] String? key, Boolean value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Char value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

#if NETCOREAPP3_1_OR_GREATER
        public Boolean Add([NotNullWhen(true)] String? key, Rune value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }
#endif
        public Boolean Add([NotNullWhen(true)] String? key, Char32 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, IntPtr value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, UIntPtr value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, SByte value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Byte value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Int16 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, UInt16 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Int32 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, UInt32 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Int64 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, UInt64 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

#if NET7_0_OR_GREATER
        public Boolean Add([NotNullWhen(true)] String? key, Int128 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, UInt128 value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }
#endif
        public Boolean Add([NotNullWhen(true)] String? key, Half value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Single value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Double value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Decimal value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, BigInteger value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, Guid value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add<T>([NotNullWhen(true)] String? key, T value) where T : unmanaged, Enum
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add<T>([NotNullWhen(true)] String? key, Enum<T> value) where T : unmanaged, Enum
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add<T, TEnum>([NotNullWhen(true)] String? key, Enum<T, TEnum> value) where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, IEnum value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add<T>([NotNullWhen(true)] String? key, IEnum<T> value) where T : unmanaged, Enum
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add<T, TEnum>([NotNullWhen(true)] String? key, IEnum<T, TEnum> value) where T : unmanaged, Enum where TEnum : class, IEnum<T, TEnum>, new()
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, TimeSpan value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

#if NET6_0_OR_GREATER
        public Boolean Add([NotNullWhen(true)] String? key, TimeOnly value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, DateOnly value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }
#endif
        public Boolean Add([NotNullWhen(true)] String? key, DateTime value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        public Boolean Add([NotNullWhen(true)] String? key, DateTimeOffset value)
        {
            return key is not null && !ContainsKey(key) && ValidateKey(key) && AddCore(key, value);
        }

        protected abstract Boolean SetCore(String key, Element element);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected Boolean SetCore<T>(String key, T value) where T : notnull
        {
            return SetCore(key, (Element) new Element<T>(value));
        }

        public Boolean Set([NotNullWhen(true)] String? key, String value)
        {
            return key is not null && ValidateKey(key) && ValidateValue(value) && SetCore(key, (Element) new StringElement(value));
        }

        public Boolean Set([NotNullWhen(true)] String? key, Boolean value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Char value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

#if NETCOREAPP3_1_OR_GREATER
        public Boolean Set([NotNullWhen(true)] String? key, Rune value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }
#endif
        public Boolean Set([NotNullWhen(true)] String? key, Char32 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, IntPtr value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, UIntPtr value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, SByte value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Byte value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Int16 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, UInt16 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Int32 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, UInt32 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Int64 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, UInt64 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

#if NET7_0_OR_GREATER
        public Boolean Set([NotNullWhen(true)] String? key, Int128 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, UInt128 value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }
#endif
        public Boolean Set([NotNullWhen(true)] String? key, Half value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Single value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Double value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Decimal value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, BigInteger value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, Guid value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set<T>([NotNullWhen(true)] String? key, T value) where T : unmanaged, Enum
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set<T>([NotNullWhen(true)] String? key, Enum<T> value) where T : unmanaged, Enum
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set<T, TEnum>([NotNullWhen(true)] String? key, Enum<T, TEnum> value) where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, IEnum value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set<T>([NotNullWhen(true)] String? key, IEnum<T> value) where T : unmanaged, Enum
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set<T, TEnum>([NotNullWhen(true)] String? key, IEnum<T, TEnum> value) where T : unmanaged, Enum where TEnum : class, IEnum<T, TEnum>, new()
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, TimeSpan value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

#if NET6_0_OR_GREATER
        public Boolean Set([NotNullWhen(true)] String? key, TimeOnly value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, DateOnly value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }
#endif
        public Boolean Set([NotNullWhen(true)] String? key, DateTime value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public Boolean Set([NotNullWhen(true)] String? key, DateTimeOffset value)
        {
            return key is not null && ValidateKey(key) && SetCore(key, value);
        }

        public abstract Boolean Remove([NotNullWhen(true)] String? key);
        public abstract Boolean Remove([NotNullWhen(true)] String? key, [MaybeNullWhen(false)] out Element value);

        protected abstract Boolean Reverse();
        public abstract RayIdPayload Clone();

        Object ICloneable.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 written)
        {
            return TryFormat(destination, out written, default(ReadOnlySpan<Char>), null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 written, ReadOnlySpan<Char> format)
        {
            return TryFormat(destination, out written, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 written, IFormatProvider? provider)
        {
            return TryFormat(destination, out written, default(ReadOnlySpan<Char>), provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 written, ReadOnlySpan<Char> format, IFormatProvider? provider)
        {
            return TryFormat(destination, out written, 32, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 written, Int32 count)
        {
            return TryFormat(destination, out written, count, default, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 written, Int32 count, ReadOnlySpan<Char> format)
        {
            return TryFormat(destination, out written, count, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 written, Int32 count, IFormatProvider? provider)
        {
            return TryFormat(destination, out written, count, default, provider);
        }

        public abstract Boolean TryFormat(Span<Char> destination, out Int32 written, Int32 count, ReadOnlySpan<Char> format, IFormatProvider? provider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String ToString()
        {
            return ToString(null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return ToString(32, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(Int32 count)
        {
            return ToString(count, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(Int32 count, String? format)
        {
            return ToString(count, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(Int32 count, IFormatProvider? provider)
        {
            return ToString(count, null, provider);
        }

        public abstract String ToString(Int32 count, String? format, IFormatProvider? provider);
        public abstract IEnumerator<KeyValuePair<String, Element>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual Element? this[String key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return TryGetValue(key, out Element? value) ? value : null;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentNullOrWhiteSpaceStringException(key, nameof(key));
                }

                if (!ValidateKey(key))
                {
                    throw new ArgumentException($"Key '{key}' is not valid.", nameof(key));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                Remove(key);
                Unsafe(key, value);
            }
        }

        Element IReadOnlyDictionary<String, Element>.this[String key]
        {
            get
            {
                return this[key] ?? throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
            }
        }
    }
}