// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Globalization;
using NetExtender.Types.Culture;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Newtonsoft.Types.Culture
{
    public enum LocalizationIdentifierJsonConvertType
    {
        Default,
        Number,
        TwoLetter,
        ThreeLetter,
        English,
        Display,
        Native,
        Language
    }

    public sealed class LocalizationIdentifierJsonConverter : NewtonsoftJsonConverter
    {
        private static ImmutableHashSet<Type> Types { get; } = ImmutableHashSet.Create
        (
            typeof(LocalizationIdentifier),
            typeof(LocalizationIdentifier?),
            typeof(CultureIdentifier),
            typeof(CultureIdentifier?),
            typeof(CultureInfo),
            typeof(String),
            typeof(UInt16),
            typeof(UInt16?),
            typeof(Int32),
            typeof(Int32?)
        );

        public LocalizationIdentifierJsonConvertType Convert { get; init; }

        public override Boolean CanRead
        {
            get
            {
                return Convert is not LocalizationIdentifierJsonConvertType.Native and not LocalizationIdentifierJsonConvertType.Language;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return true;
            }
        }

        public override Boolean CanConvert(Type type)
        {
            return Types.Contains(type);
        }

        // ReSharper disable once CognitiveComplexity
        protected internal override Object? Read(in JsonReader reader, Type type, Object? exist, ref SerializerOptions options)
        {
            if (!reader.ReadFirstToken(out JsonTokenEntry token))
            {
                throw new JsonSerializationException("Expected a value but found end of input.");
            }

            switch (token.Token)
            {
                case JsonToken.Null:
                {
                    return null;
                }
                case JsonToken.None:
                case JsonToken.PropertyName:
                {
                    if (!reader.ReadToken(out token))
                    {
                        throw new JsonSerializationException("Expected a value but found end of input.");
                    }

                    if (token.Token is JsonToken.Null)
                    {
                        goto case JsonToken.Null;
                    }

                    if (token.Token is JsonToken.Integer)
                    {
                        goto case JsonToken.Integer;
                    }

                    if (token.Token is JsonToken.String)
                    {
                        goto case JsonToken.String;
                    }

                    goto default;
                }
                case JsonToken.Integer:
                {
                    return ConvertUtilities.TryChangeType(token.Value, out Int32 code) ? new LocalizationIdentifier(code) : throw new JsonSerializationException();
                }
                case JsonToken.String:
                {
                    if (token.Current is not { } value)
                    {
                        return null;
                    }

                    if (Int32.TryParse(value, out Int32 code))
                    {
                        return new LocalizationIdentifier(code);
                    }

                    if (!CultureUtilities.TryGetIdentifier(value, out LocalizationIdentifier identifier))
                    {
                        throw new JsonSerializationException();
                    }

                    return identifier;
                }
                default:
                {
                    throw new JsonSerializationException($"Expected a value but found {token.Token}.");
                }
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private Boolean Write(in JsonWriter writer, LocalizationIdentifier identifier, ref SerializerOptions options)
        {
            switch (Convert)
            {
                case LocalizationIdentifierJsonConvertType.Default:
                {
                    goto case LocalizationIdentifierJsonConvertType.TwoLetter;
                }
                case LocalizationIdentifierJsonConvertType.Number:
                {
                    writer.WriteValue(identifier.Code);
                    return true;
                }
                case LocalizationIdentifierJsonConvertType.TwoLetter when identifier.TwoLetterISOLanguageName is { } code:
                {
                    writer.WriteValue(code);
                    return true;
                }
                case LocalizationIdentifierJsonConvertType.TwoLetter:
                {
                    goto case LocalizationIdentifierJsonConvertType.ThreeLetter;
                }
                case LocalizationIdentifierJsonConvertType.ThreeLetter when identifier.ThreeLetterISOLanguageName is { } code:
                {
                    writer.WriteValue(code);
                    return true;
                }
                case LocalizationIdentifierJsonConvertType.ThreeLetter:
                {
                    goto case LocalizationIdentifierJsonConvertType.Number;
                }
                case LocalizationIdentifierJsonConvertType.English when identifier.TryGetCultureInfo(out CultureInfo info):
                {
                    writer.WriteValue(info.EnglishName);
                    return true;
                }
                case LocalizationIdentifierJsonConvertType.English:
                {
                    goto case LocalizationIdentifierJsonConvertType.Number;
                }
                case LocalizationIdentifierJsonConvertType.Display when identifier.TryGetCultureInfo(out CultureInfo info):
                {
                    writer.WriteValue(info.DisplayName);
                    return true;
                }
                case LocalizationIdentifierJsonConvertType.Display:
                {
                    goto case LocalizationIdentifierJsonConvertType.Number;
                }
                case LocalizationIdentifierJsonConvertType.Native when identifier.TryGetCultureInfo(out CultureInfo info):
                {
                    writer.WriteValue(info.NativeName);
                    return true;
                }
                case LocalizationIdentifierJsonConvertType.Native:
                {
                    goto case LocalizationIdentifierJsonConvertType.Number;
                }
                case LocalizationIdentifierJsonConvertType.Language when identifier.TryGetCultureInfo(out CultureInfo info):
                {
                    writer.WriteValue(info.GetNativeLanguageName());
                    return true;
                }
                case LocalizationIdentifierJsonConvertType.Language:
                {
                    goto case LocalizationIdentifierJsonConvertType.Number;
                }
                default:
                {
                    throw new EnumUndefinedOrNotSupportedException<LocalizationIdentifierJsonConvertType>(Convert, nameof(Convert), null);
                }
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, Object? value, ref SerializerOptions options)
        {
            switch (value)
            {
                case null:
                {
                    writer.WriteNull();
                    return true;
                }
                case LocalizationIdentifier identifier:
                {
                    return Write(in writer, identifier, ref options);
                }
                case CultureIdentifier identifier:
                {
                    return Write(in writer, identifier, ref options);
                }
                case CultureInfo culture:
                {
                    return Write(in writer, culture, ref options);
                }
                case String identifier when CultureUtilities.TryGetIdentifier(identifier, out LocalizationIdentifier localization):
                {
                    return Write(in writer, localization, ref options);
                }
                case String identifier:
                {
                    writer.WriteValue(identifier);
                    return true;
                }
                case UInt16 identifier:
                {
                    return Write(in writer, identifier, ref options);
                }
                case Int32 identifier:
                {
                    return Write(in writer, identifier, ref options);
                }
                default:
                {
                    throw new JsonSerializationException();
                }
            }
        }
    }
}