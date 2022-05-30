// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.NewtonSoft.Types.Culture
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
    
    public sealed class LocalizationIdentifierJsonConverter : JsonConverter
    {
        private static ImmutableHashSet<Type> Types { get; } = new HashSet<Type>
        {
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
        }.ToImmutableHashSet();

        public override Boolean CanRead
        {
            get
            {
                return true;
            }
        }
        
        public override Boolean CanWrite
        {
            get
            {
                return true;
            }
        }
        
        public LocalizationIdentifierJsonConvertType Convert { get; init; }
        
        public override Boolean CanConvert(Type objectType)
        {
            return Types.Contains(objectType);
        }
        
        // ReSharper disable once CognitiveComplexity
        public override Object? ReadJson(JsonReader reader, Type objectType, Object? existingValue, JsonSerializer serializer)
        {
            if (!reader.ReadFirstToken(out JsonTokenEntry token))
            {
                throw new JsonException("Expected a value but found end of input.");
            }

            switch (token.Token)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.None:
                case JsonToken.PropertyName:
                    if (!reader.ReadToken(out token))
                    {
                        throw new JsonException("Expected a value but found end of input.");
                    }

                    if (token.Token == JsonToken.Integer)
                    {
                        goto case JsonToken.Integer;
                    }
                    
                    if (token.Token == JsonToken.String)
                    {
                        goto case JsonToken.String;
                    }
                    
                    if (token.Token == JsonToken.Null)
                    {
                        goto case JsonToken.Null;
                    }

                    goto default;
                case JsonToken.Integer:
                {
                    if (!ConvertUtilities.TryChangeType(token.Value, out Int32 code))
                    {
                        throw new JsonException();
                    }
                        
                    return new LocalizationIdentifier(code);
                }
                case JsonToken.String:
                {
                    String? value = token.Current;

                    if (value is null)
                    {
                        return null;
                    }

                    if (Int32.TryParse(value, out Int32 code))
                    {
                        return new LocalizationIdentifier(code);
                    }

                    if (!CultureUtilities.TryGetIdentifier(value, out LocalizationIdentifier identifier))
                    {
                        throw new JsonException();
                    }
                        
                    return identifier;
                }
                default:
                    throw new JsonException($"Expected a value but found {token.Token}.");
            }
        }

        // ReSharper disable once CognitiveComplexity
        // ReSharper disable once UnusedParameter.Local
        private void WriteJson(JsonWriter writer, LocalizationIdentifier identifier, JsonSerializer serializer)
        {
            switch (Convert)
            {
                case LocalizationIdentifierJsonConvertType.Default:
                    goto case LocalizationIdentifierJsonConvertType.TwoLetter;
                case LocalizationIdentifierJsonConvertType.Number:
                    writer.WriteValue(identifier.Code);
                    break;
                case LocalizationIdentifierJsonConvertType.TwoLetter:
                {
                    String? code = identifier.TwoLetterISOLanguageName;

                    if (code is null)
                    {
                        goto case LocalizationIdentifierJsonConvertType.ThreeLetter;
                    }
                    
                    writer.WriteValue(code);
                    break;
                }
                case LocalizationIdentifierJsonConvertType.ThreeLetter:
                {
                    String? code = identifier.ThreeLetterISOLanguageName;

                    if (code is null)
                    {
                        goto case LocalizationIdentifierJsonConvertType.Number;
                    }

                    writer.WriteValue(code);
                    break;
                }
                case LocalizationIdentifierJsonConvertType.English:
                {
                    if (!identifier.TryGetCultureInfo(out CultureInfo info))
                    {
                        goto case LocalizationIdentifierJsonConvertType.Number;
                    }

                    writer.WriteValue(info.EnglishName);
                    break;
                }
                case LocalizationIdentifierJsonConvertType.Display:
                {
                    if (!identifier.TryGetCultureInfo(out CultureInfo info))
                    {
                        goto case LocalizationIdentifierJsonConvertType.Number;
                    }

                    writer.WriteValue(info.DisplayName);
                    break;
                }
                case LocalizationIdentifierJsonConvertType.Native:
                {
                    if (!identifier.TryGetCultureInfo(out CultureInfo info))
                    {
                        goto case LocalizationIdentifierJsonConvertType.Number;
                    }

                    writer.WriteValue(info.NativeName);
                    break;
                }
                case LocalizationIdentifierJsonConvertType.Language:
                {
                    if (!identifier.TryGetCultureInfo(out CultureInfo info))
                    {
                        goto case LocalizationIdentifierJsonConvertType.Number;
                    }

                    writer.WriteValue(info.GetNativeLanguageName());
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(Convert), Convert, null);
            }
        }

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            switch (value)
            {
                case null:
                    writer.WriteNull();
                    return;
                case LocalizationIdentifier identifier:
                    WriteJson(writer, identifier, serializer);
                    break;
                case CultureIdentifier identifier:
                    WriteJson(writer, identifier, serializer);
                    break;
                case CultureInfo culture:
                    WriteJson(writer, culture, serializer);
                    break;
                case String identifier:
                    if (CultureUtilities.TryGetIdentifier(identifier, out LocalizationIdentifier localization))
                    {
                        WriteJson(writer, localization, serializer);
                        break;
                    }

                    writer.WriteValue(identifier);
                    break;
                case UInt16 identifier:
                    WriteJson(writer, identifier, serializer);
                    break;
                case Int32 identifier:
                    WriteJson(writer, identifier, serializer);
                    break;
            }
        }
    }
}