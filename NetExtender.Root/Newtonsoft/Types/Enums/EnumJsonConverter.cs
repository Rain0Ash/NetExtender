// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Culture;
using NetExtender.Types.Entities;
using NetExtender.Types.Enums;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Enums
{
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public sealed class EnumJsonConverter : NewtonsoftJsonConverter
    {
        public override Boolean CanConvert(Type type)
        {
            return EnumUtilities.Properties.Contains(type) || typeof(Enum<>).IsSubclassOfRawGeneric(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected internal override Object? Read(in JsonReader reader, Type type, Object? exist, ref SerializerOptions options)
        {
            JToken token = JToken.Load(reader);

            @switch:
            switch (token.Type)
            {
                case JTokenType.Null:
                {
                    return null;
                }
                case JTokenType.Integer:
                {
                    Int64 value = token.Value<Int64>();

                    if (type.IsEnum)
                    {
                        return Enum.ToObject(type, value);
                    }

                    if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                    {
                        throw new JsonSerializationException($"Unsupported type '{type.Name}' for '{GetType().Name}'.");
                    }

                    Enum id = (Enum) Enum.ToObject(properties.Underlying, value);
                    return properties.TryParse.Id(id, out Object? @enum) ? @enum : properties.Create.Id(id);
                }
                case JTokenType.String:
                {
                    if (token.Value<String>() is not { } value)
                    {
                        return null;
                    }

                    Object? result;
                    if (type.IsEnum)
                    {
                        return Enum.TryParse(type, value, true, out result) ? result : throw new JsonSerializationException($"Value '{value}' is not valid for enum type '{type.Name}'.");
                    }

                    if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                    {
                        throw new JsonSerializationException($"Unsupported type '{type.Name}' for '{GetType().Name}'.");
                    }

                    if (properties.TryParse.Title(value, out result))
                    {
                        return result;
                    }

                    if (Enum.TryParse(properties.Underlying, value, true, out result) && result is Enum id)
                    {
                        return properties.TryParse.Id(id, out Object? @enum) ? @enum : properties.Create.Id(id);
                    }

                    throw new JsonSerializationException($"Unsupported value '{value}' for type '{type.Name}' for '{GetType().Name}'.");
                }
                case JTokenType.Object when token is JObject @object:
                {
                    if (type.IsEnum)
                    {
                        token = @object.GetValue(nameof(Enum<Any.Value>.Id), options) ?? @object.GetValue(nameof(Enum<Any.Value>.Title), options) ?? throw new JsonSerializationException($"Object must have '{nameof(Enum<Any.Value>.Id)}' property.");
                        goto @switch;
                    }

                    if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                    {
                        throw new JsonSerializationException($"Unsupported type '{type.Name}' for '{GetType().Name}'.");
                    }

                    [return: NotNullIfNotNull("value")]
                    static Object? Populate(JObject @object, Object? value, EnumUtilities.Properties properties, ref SerializerOptions options)
                    {
                        if (value is null)
                        {
                            return null;
                        }

                        foreach (ReflectionProperty property in properties)
                        {
                            Object? set = @object.GetValue(property.Name, options)?.ToObject(property.Property, options);
                            property.SetValue(value, set);
                        }

                        return value;
                    }

                    Object? result;
                    String? title = @object.GetValue(nameof(Enum<Any.Value>.Title), options)?.ToString();
                    if (@object.GetValue(nameof(Enum<Any.Value>.Id), options)?.ToObject(properties.Underlying, options) is Enum id)
                    {
                        if (properties.Others.Length <= 0 && properties.TryParse.Id(id, out result))
                        {
                            return title is null ? result : properties.GetTitle(result) == title ? result : properties.Create.Title(id, title);
                        }

                        return Populate(@object, title is null ? properties.Create.Id(id) : properties.Create.Title(id, title), properties, ref options);
                    }

                    if (title is null)
                    {
                        throw new JsonSerializationException($"'{nameof(Enum<Any.Value>.Id)}' is required for conversion to enum type '{properties.Underlying.Name}'.");
                    }

                    if (properties.TryParse.Title(title, out result))
                    {
                        return result;
                    }

                    if (Enum.TryParse(properties.Underlying, title, true, out result) && (id = (result as Enum)!) is not null)
                    {
                        return properties.Others.Length <= 0 && properties.TryParse.Id(id, out result) ? result : Populate(@object, properties.Create.Id(id), properties, ref options);
                    }

                    throw new JsonSerializationException($"Unsupported value '{title}' for enum type '{type.Name}' for '{GetType().Name}'.");
                }
                default:
                {
                    throw new JsonSerializationException($"Expected '{JTokenType.Integer}', '{JTokenType.String}' or '{JTokenType.Object}' token for conversion to enum type '{type.Name}'.");
                }
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, Object? value, ref SerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNull();
                return true;
            }

            Type type = value.GetType();
            if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
            {
                throw new JsonSerializationException($"Unsupported type '{type.Name}' for '{GetType().Name}'.");
            }

            writer.WriteStartObject();

            writer.WritePropertyName(nameof(Enum<Any.Value>.Id), options);
            writer.WriteValue(properties.GetId(value));

            writer.WritePropertyName(nameof(Enum<Any.Value>.Title), options);
            writer.WriteValue(properties.GetTitle(value));

            if (properties.GetIdentifier(value) is { } identifier && identifier != LocalizationIdentifier.Invariant)
            {
                writer.WritePropertyName(nameof(Enum<Any.Value>.Identifier), options);
                writer.WriteValue(identifier);
            }

            foreach (ReflectionProperty<Object, Object> property in properties)
            {
                if (!property.Type.HasFlag(ReflectionPropertyType.Get))
                {
                    continue;
                }

                writer.WritePropertyName(property.Name, options);
                writer.WriteValue(property.GetValue(value));
            }

            writer.WriteEndObject();
            return true;
        }
    }
}

namespace NetExtender.Serialization.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class EnumJsonConverter : JsonConverterFactory
    {
        public override Boolean CanConvert(Type type)
        {
            return EnumUtilities.Properties.Contains(type) || typeof(Enum<>).IsSubclassOfRawGeneric(type);
        }

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            return (JsonConverter) Activator.CreateInstance(typeof(Converter<>).MakeGenericType(type))!;
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private sealed class Converter<T> : JsonConverter<T>
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public override T? Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.Null:
                    {
                        return default;
                    }
                    case JsonTokenType.Number:
                    {
                        Int64 value = reader.GetInt64();

                        if (type.IsEnum)
                        {
                            return (T) Enum.ToObject(type, value);
                        }

                        if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                        {
                            throw new JsonException($"Unsupported type '{type.Name}' for '{GetType().Name}'.");
                        }

                        Enum id = (Enum) Enum.ToObject(properties.Underlying, value);
                        return (T) (properties.TryParse.Id(id, out Object? @enum) ? @enum : properties.Create.Id(id));
                    }
                    case JsonTokenType.String:
                    {
                        if (reader.GetString() is not { } value)
                        {
                            return default;
                        }

                        Object? result;
                        if (type.IsEnum)
                        {
                            return Enum.TryParse(type, value, true, out result) ? (T?) result : throw new JsonException($"Value '{value}' is not valid for enum type '{type.Name}'.");
                        }

                        if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                        {
                            throw new JsonException($"Unsupported type '{type.Name}' for '{GetType().Name}'.");
                        }

                        if (properties.TryParse.Title(value, out result))
                        {
                            return (T) result;
                        }

                        if (Enum.TryParse(properties.Underlying, value, true, out result) && result is Enum id)
                        {
                            return (T) (properties.TryParse.Id(id, out Object? @enum) ? @enum : properties.Create.Id(id));
                        }

                        throw new JsonException($"Unsupported value '{value}' for type '{type.Name}' for '{GetType().Name}'.");
                    }
                    case JsonTokenType.StartObject:
                    {
                        using JsonDocument document = JsonDocument.ParseValue(ref reader);
                        JsonElement @object = document.RootElement.Clone();

                        if (type.IsEnum)
                        {
                            JsonElement token = GetValue(@object, nameof(Enum<Any.Value>.Id), options) ?? GetValue(@object, nameof(Enum<Any.Value>.Title), options) ?? throw new JsonException($"Object must have '{nameof(Enum<Any.Value>.Id)}' property.");

                            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
                            return token.ValueKind switch
                            {
                                JsonValueKind.Null => default,
                                JsonValueKind.Number => (T) Enum.ToObject(type, token.GetInt64()),
                                JsonValueKind.String => token.GetString() is { } value ? Enum.TryParse(type, value, true, out Object? @enum) ? (T?) @enum : throw new JsonException($"Value '{value}' is not valid for enum type '{type.Name}'.") : default,
                                _ => throw new JsonException($"Expected number or string for enum value in object, got '{token.ValueKind}'.")
                            };
                        }

                        if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                        {
                            throw new JsonException($"Unsupported type '{type.Name}' for '{GetType().Name}'.");
                        }

                        [return: NotNullIfNotNull("value")]
                        static Object? Populate(JsonElement @object, Object? value, EnumUtilities.Properties properties, JsonSerializerOptions options)
                        {
                            if (value is null)
                            {
                                return null;
                            }

                            foreach (ReflectionProperty property in properties)
                            {
                                if (GetValue(@object, property.Name, options) is not { } element)
                                {
                                    continue;
                                }

                                Object? set = element.Deserialize(property.Property, options);
                                property.SetValue(value, set);
                            }

                            return value;
                        }

                        Object? result;
                        String? title = GetValue(@object, nameof(Enum<Any.Value>.Title), options)?.GetString();
                        if (GetValue(@object, nameof(Enum<Any.Value>.Id), options)?.Deserialize(properties.Underlying, options) is Enum id)
                        {
                            if (properties.Others.Length <= 0 && properties.TryParse.Id(id, out result))
                            {
                                return (T) (title is null ? result : properties.GetTitle(result) == title ? result : properties.Create.Title(id, title));
                            }

                            return (T) Populate(@object, title is null ? properties.Create.Id(id) : properties.Create.Title(id, title), properties, options);
                        }

                        if (title is null)
                        {
                            throw new JsonException($"'{nameof(Enum<Any.Value>.Id)}' is required for conversion to enum type '{properties.Underlying.Name}'.");
                        }

                        if (properties.TryParse.Title(title, out result))
                        {
                            return (T) result;
                        }

                        if (Enum.TryParse(properties.Underlying, title, true, out result) && (id = (result as Enum)!) is not null)
                        {
                            return (T) (properties.Others.Length <= 0 && properties.TryParse.Id(id, out result) ? result : Populate(@object, properties.Create.Id(id), properties, options));
                        }

                        throw new JsonException($"Unsupported value '{title}' for enum type '{type.Name}' for '{GetType().Name}'.");
                    }
                    default:
                    {
                        throw new JsonException($"Expected '{JsonTokenType.Number}', '{JsonTokenType.String}' or '{JsonTokenType.StartObject}' token for conversion to enum type '{type.Name}'.");
                    }
                }
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                if (value is null)
                {
                    writer.WriteNullValue();
                    return;
                }

                Type type = value.GetType();
                if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                {
                    throw new JsonException($"Unsupported type '{type.Name}' for '{GetType().Name}'.");
                }

                writer.WriteStartObject();

                writer.WritePropertyName(ConvertName(nameof(Enum<Any.Value>.Id), options));
                JsonSerializer.Serialize(writer, properties.GetId(value), properties.Underlying, options);

                writer.WritePropertyName(ConvertName(nameof(Enum<Any.Value>.Title), options));
                JsonSerializer.Serialize(writer, properties.GetTitle(value), typeof(String), options);

                if (properties.GetIdentifier(value) is { } identifier && identifier != LocalizationIdentifier.Invariant)
                {
                    writer.WritePropertyName(ConvertName(nameof(Enum<Any.Value>.Identifier), options));
                    JsonSerializer.Serialize(writer, identifier, typeof(LocalizationIdentifier), options);
                }

                foreach (ReflectionProperty property in properties)
                {
                    if (!property.Type.HasFlag(ReflectionPropertyType.Get))
                    {
                        continue;
                    }

                    writer.WritePropertyName(ConvertName(property.Name, options));
                    JsonSerializer.Serialize(writer, property.GetValue(value), property.Property, options);
                }

                writer.WriteEndObject();
            }

            private static String ConvertName(String name, JsonSerializerOptions options)
            {
                return options.PropertyNamingPolicy?.ConvertName(name) ?? name;
            }

            [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
            private static JsonElement? GetValue(JsonElement @object, String name, JsonSerializerOptions options)
            {
                if (@object.ValueKind is not JsonValueKind.Object)
                {
                    return null;
                }

                if (@object.TryGetProperty(name, out JsonElement value) || options.PropertyNamingPolicy is not null && @object.TryGetProperty(options.PropertyNamingPolicy.ConvertName(name), out value))
                {
                    return value;
                }

                if (!options.PropertyNameCaseInsensitive)
                {
                    return null;
                }

                foreach (JsonProperty property in @object.EnumerateObject())
                {
                    if (String.Equals(property.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        return property.Value;
                    }
                }

                return null;
            }
        }
    }
}