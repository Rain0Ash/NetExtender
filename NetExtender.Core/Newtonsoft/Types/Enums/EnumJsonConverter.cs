// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
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
    public class EnumJsonConverter : NewtonsoftJsonConverter
    {
        public override Boolean CanConvert(Type type)
        {
            return EnumUtilities.Properties.Contains(type) || typeof(Enum<>).IsSubclassOfRawGeneric(type);
        }

        // ReSharper disable once CognitiveComplexity
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