// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using NetExtender.NewtonSoft;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace NetExtender.Utilities.Serialization
{
    public static class JsonUtilities
    {
        public static Boolean IsValidJson(String? json)
        {
            if (String.IsNullOrEmpty(json))
            {
                return false;
            }

            json = json.Trim();

            if (json.Length <= 0 || (!json.StartsWith("{") || !json.EndsWith("}")) && (!json.StartsWith("[") || !json.EndsWith("]")))
            {
                return false;
            }

            try
            {
                _ = JToken.Parse(json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static String JsonSerializeObject(this Object? value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static String JsonSerializeObject(this Object? value, Newtonsoft.Json.Formatting formatting)
        {
            return JsonConvert.SerializeObject(value, formatting);
        }

        public static String JsonSerializeObject(this Object? value, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(value, converters);
        }

        public static String JsonSerializeObject(this Object? value, Newtonsoft.Json.Formatting formatting, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(value, formatting, converters);
        }

        public static String JsonSerializeObject(this Object? value, JsonSerializerSettings? settings)
        {
            return JsonConvert.SerializeObject(value, settings);
        }

        public static String JsonSerializeObject(this Object? value, Type? type, JsonSerializerSettings? settings)
        {
            return JsonConvert.SerializeObject(value, type, settings);
        }

        public static String JsonSerializeObject(this Object? value, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings? settings)
        {
            return JsonConvert.SerializeObject(value, formatting, settings);
        }

        public static String JsonSerializeObject(this Object? value, Type? type, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings? settings)
        {
            return JsonConvert.SerializeObject(value, type, formatting, settings);
        }

        public static T? JsonDeserializeObject<T>(this String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public static T? JsonDeserializeObject<T>(this String json, JsonSerializerSettings? settings)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static Boolean TryJsonDeserializeObject<T>(this String? json, out T? result)
        {
            return TryJsonDeserializeObject(json, null, out result);
        }

        public static Boolean TryJsonDeserializeObject<T>(this String? json, JsonSerializerSettings? settings, out T? result)
        {
            if (String.IsNullOrWhiteSpace(json))
            {
                result = default;
                return false;
            }

            try
            {
                result = JsonConvert.DeserializeObject<T>(json, settings);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static String ToJson(String xml)
        {
            return XmlUtilities.Parse(xml).ToJson();
        }

        public static String ToJson(this XmlDocument document)
        {
            return JsonConvert.SerializeXmlNode(document, Newtonsoft.Json.Formatting.Indented, true);
        }

        private static PropertyContractResolver InitializePropertyResolver(JsonSerializerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            switch (settings.ContractResolver)
            {
                case null:
                {
                    PropertyContractResolver resolver = new PropertyContractResolver();
                    settings.ContractResolver = resolver;
                    return resolver;
                }
                case PropertyContractResolver resolver:
                {
                    return resolver;
                }
                case SingletonCompositeContractResolver composite:
                {
                    return composite.GetOrAdd<PropertyContractResolver>();
                }
                case DefaultContractResolver other:
                {
                    SingletonCompositeContractResolver singleton = new SingletonCompositeContractResolver
                    {
                        other,
                        new PropertyContractResolver(other)
                    };

                    settings.ContractResolver = singleton;
                    return singleton.GetOrAdd<PropertyContractResolver>();
                }
                default:
                {
                    SingletonCompositeContractResolver singleton = new SingletonCompositeContractResolver
                    {
                        settings.ContractResolver,
                        new PropertyContractResolver()
                    };

                    settings.ContractResolver = singleton;
                    return singleton.GetOrAdd<PropertyContractResolver>();
                }
            }
        }

        public static TSettings RenameProperty<TSettings>(this TSettings settings, Type type, String property, String name) where TSettings : JsonSerializerSettings
        {
            InitializePropertyResolver(settings).RenameProperty(type, property, name);
            return settings;
        }

        public static TSettings IgnoreProperty<TSettings>(this TSettings settings, Type type, String property) where TSettings : JsonSerializerSettings
        {
            InitializePropertyResolver(settings).IgnoreProperty(type, property);
            return settings;
        }

        public static TSettings IgnoreProperty<TSettings>(this TSettings settings, Type type, params String[] properties) where TSettings : JsonSerializerSettings
        {
            InitializePropertyResolver(settings).IgnoreProperty(type, properties);
            return settings;
        }

        public static TSettings OrderProperty<TSettings>(this TSettings settings, Type type, String property, Int32 order) where TSettings : JsonSerializerSettings
        {
            InitializePropertyResolver(settings).OrderProperty(type, property, order);
            return settings;
        }
        
        public static OverrideConvertContractResolver InitializeOverrideContractResolver(JsonSerializerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            switch (settings.ContractResolver)
            {
                case null:
                {
                    OverrideConvertContractResolver resolver = new OverrideConvertContractResolver();
                    settings.ContractResolver = resolver;
                    return resolver;
                }
                case OverrideConvertContractResolver resolver:
                {
                    return resolver;
                }
                case SingletonCompositeContractResolver composite:
                {
                    return composite.GetOrAdd<OverrideConvertContractResolver>();
                }
                case DefaultContractResolver other:
                {
                    SingletonCompositeContractResolver singleton = new SingletonCompositeContractResolver
                    {
                        other,
                        new OverrideConvertContractResolver()
                    };

                    settings.ContractResolver = singleton;
                    return singleton.GetOrAdd<OverrideConvertContractResolver>();
                }
                default:
                {
                    SingletonCompositeContractResolver singleton = new SingletonCompositeContractResolver
                    {
                        settings.ContractResolver,
                        new OverrideConvertContractResolver()
                    };

                    settings.ContractResolver = singleton;
                    return singleton.GetOrAdd<OverrideConvertContractResolver>();
                }
            }
        }
        
        public static TSettings ConverterOverride<TSettings>(this TSettings settings, Type type, JsonConverter? converter) where TSettings : JsonSerializerSettings
        {
            InitializeOverrideContractResolver(settings).Add(type, converter);
            return settings;
        }
        
        public static TSettings ConverterOverride<T, TSettings>(this TSettings settings, JsonConverter? converter) where TSettings : JsonSerializerSettings
        {
            InitializeOverrideContractResolver(settings).Add<T>(converter);
            return settings;
        }
        
        public static TSettings ConverterOverrideWithout<TSettings>(this TSettings settings, Type type) where TSettings : JsonSerializerSettings
        {
            InitializeOverrideContractResolver(settings).Without(type);
            return settings;
        }
        
        public static TSettings ConverterOverrideWithout<T, TSettings>(this TSettings settings) where TSettings : JsonSerializerSettings
        {
            InitializeOverrideContractResolver(settings).Without<T>();
            return settings;
        }

        public static TSettings ConverterOverrideRemove<TSettings>(this TSettings settings, Type type) where TSettings : JsonSerializerSettings
        {
            return ConverterOverrideRemove(settings, type, out _);
        }

        public static TSettings ConverterOverrideRemove<TSettings>(this TSettings settings, Type type, out JsonConverter? converter) where TSettings : JsonSerializerSettings
        {
            InitializeOverrideContractResolver(settings).Remove(type, out converter);
            return settings;
        }

        public static TSettings ConverterOverrideRemove<T, TSettings>(this TSettings settings) where TSettings : JsonSerializerSettings
        {
            return ConverterOverrideRemove<T, TSettings>(settings, out _);
        }

        public static TSettings ConverterOverrideRemove<T, TSettings>(this TSettings settings, out JsonConverter? converter) where TSettings : JsonSerializerSettings
        {
            InitializeOverrideContractResolver(settings).Remove<T>(out converter);
            return settings;
        }

        public static Boolean IsValue(this JsonToken token)
        {
            return token switch
            {
                JsonToken.None => false,
                JsonToken.StartObject => false,
                JsonToken.StartArray => false,
                JsonToken.StartConstructor => false,
                JsonToken.PropertyName => false,
                JsonToken.Comment => false,
                JsonToken.Raw => true,
                JsonToken.Integer => true,
                JsonToken.Float => true,
                JsonToken.String => true,
                JsonToken.Boolean => true,
                JsonToken.Null => true,
                JsonToken.Undefined => true,
                JsonToken.EndObject => false,
                JsonToken.EndArray => false,
                JsonToken.EndConstructor => false,
                JsonToken.Date => true,
                JsonToken.Bytes => true,
                _ => throw new ArgumentOutOfRangeException(nameof(token), token, null)
            };
        }

        public static JsonTokenEntry GetToken(this JsonReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            
            return new JsonTokenEntry(reader.TokenType, reader.ValueType, reader.Value, reader.Depth);
        }

        public static Boolean GetToken(this JsonReader reader, out JsonTokenEntry result)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            result = new JsonTokenEntry(reader.TokenType, reader.ValueType, reader.Value, reader.Depth);
            return result.Token != JsonToken.None;
        }

        public static Boolean ReadFirstToken(this JsonReader reader, out JsonTokenEntry result)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (reader.GetToken(out result))
            {
                return true;
            }

            reader.Read();
            return reader.GetToken(out result);
        }

        public static Boolean ReadToken(this JsonReader reader, out JsonTokenEntry result)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            Boolean successful = reader.Read();
            result = reader.GetToken();
            return successful;
        }
        
        public static IEnumerable<JsonTokenEntry> AsEnumerable(this JsonReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            
            if (reader.TokenType == JsonToken.None && !reader.Read())
            {
                yield break;
            }

            do
            {
                yield return new JsonTokenEntry(reader.TokenType, reader.ValueType, reader.Value, reader.Depth);
                
            } while (reader.Read() && reader.TokenType != JsonToken.None);
        }

        public static IEnumerator<JsonTokenEntry> GetEnumerator(this JsonReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            
            return reader.AsEnumerable().GetEnumerator();
        }

        public static IEnumerable<JsonTokenEntry>? CanBeNullable(this IEnumerable<JsonTokenEntry> source)
        {
            return CanBeNullable(source, out _);
        }

        public static IEnumerable<JsonTokenEntry>? CanBeNullable(this IEnumerable<JsonTokenEntry> source, out Boolean nullable)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<JsonTokenEntry> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                nullable = false;
                return Array.Empty<JsonTokenEntry>();
            }

            if (enumerator.Current.Token != JsonToken.Null)
            {
                nullable = false;
                return enumerator.AsEnumerable().Prepend(enumerator.Current);
            }

            nullable = true;
            return null;
        }

        public static IEnumerable<JsonTokenEntry> MustStartWith(this IEnumerable<JsonTokenEntry> source)
        {
            return MustStartWith(source, JsonToken.StartObject);
        }

        public static IEnumerable<JsonTokenEntry> MustStartWith(this IEnumerable<JsonTokenEntry> source, JsonToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<JsonTokenEntry> enumerator = source.GetEnumerator();
            
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException("The source sequence is empty.");
            }
            
            if (enumerator.Current.Token != token)
            {
                throw new InvalidOperationException($"The source sequence does not start with {token}.");
            }

            do
            {
                yield return enumerator.Current;
                
            } while (enumerator.MoveNext());
        }

        public static IEnumerable<JsonTokenEntry> With(this IEnumerable<JsonTokenEntry> source, JsonToken token, Action<JsonTokenEntry> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            foreach (JsonTokenEntry value in source)
            {
                if (value.Token == token)
                {
                    action(value);
                }
                
                yield return value;
            }
        }
        
        public static IEnumerable<JsonTokenEntry> WithBoolean(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry, Boolean> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return With(source, JsonToken.Boolean, item => action(item, item.TryConvert(out Boolean value) ? value : throw new InvalidOperationException($"The value of {item} is not a boolean.")));
        }
        
        public static IEnumerable<JsonTokenEntry> WithInteger(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry, Int64> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return With(source, JsonToken.Integer, item => action(item, item.TryConvert(out Int64 value) ? value : throw new InvalidOperationException($"The value of {item} is not an integer.")));
        }
        
        public static IEnumerable<JsonTokenEntry> WithFloat(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry, Double> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return With(source, JsonToken.Float, item => action(item, item.TryConvert(out Double value) ? value : throw new InvalidOperationException($"The value of {item} is not a float.")));
        }
        
        public static IEnumerable<JsonTokenEntry> WithDateTime(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry, DateTime> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return With(source, JsonToken.Date, item => action(item, item.TryConvert(out DateTime value) ? value : throw new InvalidOperationException($"The value of {item} is not a date.")));
        }
        
        public static IEnumerable<JsonTokenEntry> WithBytes(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry, Byte[]?> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return With(source, JsonToken.Bytes, item => action(item, item.TryConvert(out Byte[]? value) ? value : throw new InvalidOperationException($"The value of {item} is not a byte array.")));
        }

        public static IEnumerable<JsonTokenEntry> WithString(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry, String?> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return With(source, JsonToken.String, item => action(item, item.TryConvert(out String? value) ? value : throw new InvalidOperationException($"The value of {item} is not a string.")));
        }
        
        public static IEnumerable<JsonTokenEntry> WithValue(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (JsonTokenEntry value in source)
            {
                if (value.IsValue)
                {
                    action(value);
                }
                
                yield return value;
            }
        }

        public static IEnumerable<JsonTokenEntry> WithPropertyName(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry, String?> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return With(source, JsonToken.PropertyName, item => action(item, item.Current));
        }
        
        public static IEnumerable<JsonTokenEntry> WithStartObject(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry> action)
        {
            return With(source, JsonToken.StartObject, action);
        }
        
        public static IEnumerable<JsonTokenEntry> WithEndObject(this IEnumerable<JsonTokenEntry> source, Action<JsonTokenEntry> action)
        {
            return With(source, JsonToken.EndObject, action);
        }

        public static IEnumerable<JsonTokenEntry> MustEndWith(this IEnumerable<JsonTokenEntry> source)
        {
            return MustEndWith(source, JsonToken.EndObject);
        }
        
        public static IEnumerable<JsonTokenEntry> MustEndWith(this IEnumerable<JsonTokenEntry> source, JsonToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<JsonTokenEntry> enumerator = source.GetEnumerator();
            
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException("The source sequence is empty.");
            }
            
            do
            {
                yield return enumerator.Current;
                
            } while (enumerator.MoveNext());
            
            if (enumerator.Current.Token != token)
            {
                throw new InvalidOperationException($"The source sequence does not end with {token}.");
            }
        }

        public static Boolean Write(this JsonWriter writer, JsonTokenEntry value)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            
            if (value.Token == JsonToken.None)
            {
                return false;
            }
            
            writer.WriteToken(value.Token, value.Value);
            return true;
        }
        
        public static Boolean Write(this JsonWriter writer, IEnumerable<JsonTokenEntry> values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.All(writer.Write);
        }
    }
    
    public readonly struct JsonTokenEntry
    {
        public static implicit operator JsonToken(JsonTokenEntry value)
        {
            return value.Token;
        }
        
        public static implicit operator String?(JsonTokenEntry value)
        {
            return value.Current;
        }
        
        public JsonToken Token { get; }
        public Type? Type { get; }
        public Object? Value { get; }
        public Int32 Depth { get; }

        public String? Current
        {
            get
            {
                return Value as String;
            }
        }

        public Boolean IsValue
        {
            get
            {
                return Token.IsValue();
            }
        }

        public JsonTokenEntry(JsonToken token, Type? type, Object? value, Int32 depth)
        {
            Token = token;
            Value = value;
            Depth = depth;
            Type = type;
        }

        public void Deconstruct(out JsonToken token, out Object? value)
        {
            Deconstruct(out token, out _, out value);
        }
        
        public void Deconstruct(out JsonToken token, out String? value)
        {
            Deconstruct(out token, out _, out value);
        }
        
        public void Deconstruct(out JsonToken token, out Type? type, out Object? value)
        {
            token = Token;
            type = Type;
            value = Value;
        }
        
        public void Deconstruct(out JsonToken token, out Type? type, out String? value)
        {
            token = Token;
            type = Type;
            value = Current;
        }

        public Boolean TryConvert<T>(out T? value)
        {
            if (typeof(T) != typeof(String))
            {
                return Current.TryJsonDeserializeObject(out value);
            }

            value = (T?) (Object?) Current;
            return true;
        }

        public override String ToString()
        {
            String? value = Current;
            return value is not null ? $"{Depth}: {Token} : \"{value}\"" : $"{Depth}: {Token}";
        }
    }
}