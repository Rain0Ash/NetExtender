// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.NewtonSoft.Utilities;
using NetExtender.Types.Culture;
using NetExtender.Types.Entities;
using NetExtender.Types.Enums;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace NetExtender.NewtonSoft.Types.Enums
{
    public class EnumJsonConverter : JsonConverter
    {
        public override Boolean CanConvert(Type objectType)
        {
            return EnumUtilities.Properties.Contains(objectType) || typeof(Enum<>).IsSubclassOfRawGeneric(objectType);
        }

        // ReSharper disable once CognitiveComplexity
        public override Object? ReadJson(JsonReader reader, Type type, Object? existingValue, JsonSerializer serializer)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            JToken token = JToken.Load(reader);

            @switch:
            switch (token.Type)
            {
                case JTokenType.Null:
                    return null;
                case JTokenType.Integer:
                {
                    Int64 value = token.Value<Int64>();

                    if (type.IsEnum)
                    {
                        return Enum.ToObject(type, value);
                    }

                    if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                    {
                        throw new JsonException($"Unsupported type '{type}' for {GetType()}.");
                    }

                    Enum id = (Enum) Enum.ToObject(properties.Underlying, value);
                    return properties.TryParse.Id(id, out Object? @enum) ? @enum : properties.Create.Id(id);
                }
                case JTokenType.String:
                {
                    String? value = token.Value<String>();

                    if (value is null)
                    {
                        return null;
                    }

                    Object? result;
                    if (type.IsEnum)
                    {
                        return Enum.TryParse(type, value, true, out result) ? result : throw new JsonException($"Value '{value}' is not valid for enum type '{type}'.");
                    }

                    if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                    {
                        throw new JsonException($"Unsupported type '{type}' for {GetType()}.");
                    }

                    if (properties.TryParse.Title(value, out result))
                    {
                        return result;
                    }

                    if (Enum.TryParse(properties.Underlying, value, true, out result) && result is Enum id)
                    {
                        return properties.TryParse.Id(id, out Object? @enum) ? @enum : properties.Create.Id(id);
                    }

                    throw new JsonException($"Unsupported value '{value}' for type '{type}' for {GetType()}.");
                }
                case JTokenType.Object when token is JObject jobject:
                {
                    //TODO:
                    NamingStrategy? strategy = serializer.GetNamingStrategy();

                    if (type.IsEnum)
                    {
                        token = jobject[strategy.NamingStrategy(nameof(Enum<Any.Value>.Id), false)] ?? jobject[strategy.NamingStrategy(nameof(Enum<Any.Value>.Title), false)] ??
                            throw new JsonException($"Object must have '{nameof(Enum<Any.Value>.Id)}' property.");
                        goto @switch;
                    }

                    if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
                    {
                        throw new JsonException($"Unsupported type '{type}' for {GetType()}.");
                    }

                    [return: NotNullIfNotNull("value")]
                    Object? Populate(Object? value)
                    {
                        if (value is null)
                        {
                            return null;
                        }

                        foreach (ReflectionProperty property in properties)
                        {
                            Object? set = jobject[strategy.NamingStrategy(property.Name, false)]?.ToObject(property.Property, serializer);
                            property.SetValue(value, set);
                        }

                        return value;
                    }

                    Object? result;
                    String? title = jobject[strategy.NamingStrategy(nameof(Enum<Any.Value>.Title), false)]?.ToString();
                    if (jobject[strategy.NamingStrategy(nameof(Enum<Any.Value>.Id), false)]?.ToObject(properties.Underlying, serializer) is Enum id)
                    {
                        if (properties.Others.Length <= 0 && properties.TryParse.Id(id, out result))
                        {
                            return title is null ? result : properties.GetTitle(result) == title ? result : properties.Create.Title(id, title);
                        }

                        return Populate(title is null ? properties.Create.Id(id) : properties.Create.Title(id, title));
                    }

                    if (title is null)
                    {
                        throw new JsonException($"'{nameof(Enum<Any.Value>.Id)}' is required for conversion to enum type '{properties.Underlying}'.");
                    }

                    if (properties.TryParse.Title(title, out result))
                    {
                        return result;
                    }

                    if (Enum.TryParse(properties.Underlying, title, true, out result) && (id = (result as Enum)!) is not null)
                    {
                        return properties.Others.Length <= 0 && properties.TryParse.Id(id, out result) ? result : Populate(properties.Create.Id(id));
                    }

                    throw new JsonException($"Unsupported value '{title}' for enum type '{type}' for {GetType()}.");
                }
                default:
                {
                    throw new JsonException($"Expected {JTokenType.Integer}, {JTokenType.String} or {JTokenType.Object} token for conversion to enum type '{type}'.");
                }
            }
        }

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            Type type = value.GetType();
            if (!EnumUtilities.Properties.TryFactory(type, out EnumUtilities.Properties? properties))
            {
                throw new JsonException($"Unsupported type '{type}' for {GetType()}.");
            }

            NamingStrategy? strategy = serializer.GetNamingStrategy();

            writer.WriteStartObject();
            writer.WritePropertyName(strategy.NamingStrategy(nameof(Enum<Any.Value>.Id), false));
            writer.WriteValue(properties.GetId(value));
            writer.WritePropertyName(strategy.NamingStrategy(nameof(Enum<Any.Value>.Title), false));
            writer.WriteValue(properties.GetTitle(value));

            if (properties.GetIdentifier(value) is { } identifier && identifier != LocalizationIdentifier.Invariant)
            {
                writer.WritePropertyName(strategy.NamingStrategy(nameof(Enum<Any.Value>.Identifier), false));
                writer.WriteValue(identifier);
            }

            foreach (ReflectionProperty<Object, Object> property in properties)
            {
                if (!property.Type.HasFlag(ReflectionPropertyType.Get))
                {
                    continue;
                }
                
                writer.WritePropertyName(strategy.NamingStrategy(property.Name, false));
                writer.WriteValue(property.GetValue(value));
            }

            writer.WriteEndObject();
        }
    }
}