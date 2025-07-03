using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Serialization.Json.Monads;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Monads
{
    public sealed class BoxJsonConverter<T> : NewtonsoftJsonConverter<Box<T>>
    {
        private static NewtonsoftJsonConverter<Box<T>> Converter
        {
            get
            {
                return Id ? IdBoxJsonConverter.Instance : NoIdBoxJsonConverter.Instance;
            }
        }

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        private static Boolean? id;
        public static Boolean Id
        {
            get
            {
                return id ?? BoxJsonConverter.Id;
            }
            set
            {
                id = value;
            }
        }

        public override Boolean CanRead
        {
            get
            {
                return Converter.CanRead;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return Converter.CanWrite;
            }
        }

        protected internal override Box<T>? Read(in JsonReader reader, Type type, Maybe<Box<T>> exist, ref SerializerOptions options)
        {
            return Converter.Read(in reader, type, exist, ref options);
        }

        protected internal override Boolean Write(in JsonWriter writer, Box<T>? value, ref SerializerOptions options)
        {
            return Converter.Write(in writer, value, ref options);
        }

        public override Int32 GetHashCode()
        {
            return IdBoxJsonConverter.Instance.GetHashCode() ^ NoIdBoxJsonConverter.Instance.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Converter.Equals(other);
        }

        public override String? ToString()
        {
            return Converter.ToString();
        }

        public sealed class IdBoxJsonConverter : NewtonsoftJsonConverter<Box<T>>
        {
            public static IdBoxJsonConverter Instance { get; } = new IdBoxJsonConverter();

            protected internal override Box<T>? Read(in JsonReader reader, Type type, Maybe<Box<T>> exist, ref SerializerOptions options)
            {
                if (reader.TokenType is JsonToken.Null)
                {
                    return null;
                }

                JToken token = JToken.Load(reader);
                switch (token.Type)
                {
                    case JTokenType.Object when token is JObject @object:
                    {
                        Guid id = !@object.TryGetValue(nameof(Box<T>.Id), options, out JToken? jid) ? Box<T>.NewGuid() : jid.Type is JTokenType.Guid or JTokenType.String ? jid.ToObject<Guid>(options) : throw new JsonSerializationException($"{nameof(Box<T>.Id)} must be a {nameof(Guid)}.");
                        T? value = @object.TryGetValue(nameof(Box<T>.Value), options, out JToken? jvalue) ? jvalue.ToObject<T>(options) : default;
                        Boolean mutable = @object.TryGetValue(nameof(Box<T>.IsReadOnly), options, out JToken? jmutable) && !jmutable.Value<Boolean>();

                        return new Box<T>(id, value!, mutable);
                    }
                    default:
                    {
                        return new Box<T>(token.ToObject<T>(options)!);
                    }
                }
            }

            protected internal override Boolean Write(in JsonWriter writer, Box<T>? value, ref SerializerOptions options)
            {
                switch (value)
                {
                    case null:
                    {
                        writer.WriteNull();
                        return true;
                    }
                    case { Value: not null, IsReadOnly: true } when value.Id == Guid.Empty:
                    {
                        options.Serialize(writer, value.Value);
                        return true;
                    }
                    default:
                    {
                        writer.WriteStartObject();

                        if (value.Id != Guid.Empty)
                        {
                            writer.WritePropertyName(nameof(Box<T>.Id), options);
                            writer.WriteValue(value.Id);
                        }

                        writer.WritePropertyName(nameof(Box<T>.Value), options);
                        options.Serialize(writer, value.Value);

                        if (!value.IsReadOnly)
                        {
                            writer.WritePropertyName(nameof(Box<T>.IsReadOnly), options);
                            writer.WriteValue(false);
                        }

                        writer.WriteEndObject();
                        return true;
                    }
                }
            }
        }

        public sealed class NoIdBoxJsonConverter : NewtonsoftJsonConverter<Box<T>>
        {
            public static NoIdBoxJsonConverter Instance { get; } = new NoIdBoxJsonConverter();

            protected internal override Box<T>? Read(in JsonReader reader, Type type, Maybe<Box<T>> exist, ref SerializerOptions options)
            {
                if (reader.TokenType is JsonToken.Null)
                {
                    return null;
                }

                JToken token = JToken.Load(reader);
                switch (token.Type)
                {
                    case JTokenType.Object when token is JObject @object:
                    {
                        Guid id = @object.TryGetValue(nameof(Box<T>.Id), options, out JToken? jid) && jid.Type is JTokenType.Guid or JTokenType.String ? jid.ToObject<Guid>(options) : Guid.Empty;
                        T? value = @object.TryGetValue(nameof(Box<T>.Value), options, out JToken? jvalue) ? jvalue.ToObject<T>(options) : default;
                        Boolean mutable = @object.TryGetValue(nameof(Box<T>.IsReadOnly), options, out JToken? jmutable) && !jmutable.Value<Boolean>();
                        
                        return new Box<T>(id, value!, mutable);
                    }
                    default:
                    {
                        return new Box<T>(token.ToObject<T>(options)!);
                    }
                }
            }

            protected internal override Boolean Write(in JsonWriter writer, Box<T>? value, ref SerializerOptions options)
            {
                switch (value)
                {
                    case null:
                    {
                        writer.WriteNull();
                        return true;
                    }
                    case { Value: not null, IsReadOnly: true }:
                    {
                        options.Serialize(writer, value.Value);
                        return true;
                    }
                    default:
                    {
                        writer.WriteStartObject();
                
                        writer.WritePropertyName(nameof(Box<T>.Value), options);
                        options.Serialize(writer, value.Value);
                
                        if (!value.IsReadOnly)
                        {
                            writer.WritePropertyName(nameof(Box<T>.IsReadOnly), options);
                            writer.WriteValue(false);
                        }
                
                        writer.WriteEndObject();
                        return true;
                    }
                }
            }
        }
    }
}

namespace NetExtender.Serialization.Json.Monads
{
    using System.Text.Json;
    
    public static class BoxJsonConverter
    {
        public static Boolean Id { get; set; } = true;
    }
    
    public sealed class BoxJsonConverter<T> : TextJsonConverter<Box<T>>
    {
        private static TextJsonConverter<Box<T>> Converter
        {
            get
            {
                return Id ? IdBoxJsonConverter.Instance : NoIdBoxJsonConverter.Instance;
            }
        }
        
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        private static Boolean? id;
        public static Boolean Id
        {
            get
            {
                return id ?? BoxJsonConverter.Id;
            }
            set
            {
                id = value;
            }
        }

        public override Boolean HandleNull
        {
            get
            {
                return Converter.HandleNull;
            }
        }

        public override Boolean CanConvert(Type type)
        {
            return Converter.CanConvert(type);
        }

        protected internal override Box<T>? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            return Converter.Read(ref reader, type, ref options);
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, Box<T> value, ref SerializerOptions options)
        {
            return Converter.Write(in writer, value, ref options);
        }

        protected internal override Box<T> ReadAsPropertyName(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            return Converter.ReadAsPropertyName(ref reader, type, ref options);
        }

        protected internal override Boolean WriteAsPropertyName(in Utf8JsonWriter writer, Box<T> value, ref SerializerOptions options)
        {
            return Converter.WriteAsPropertyName(writer, value, ref options);
        }

        public override Int32 GetHashCode()
        {
            return IdBoxJsonConverter.Instance.GetHashCode() ^ NoIdBoxJsonConverter.Instance.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Converter.Equals(other);
        }

        public override String? ToString()
        {
            return Converter.ToString();
        }

        public sealed class IdBoxJsonConverter : TextJsonConverter<Box<T>>
        {
            public static IdBoxJsonConverter Instance { get; } = new IdBoxJsonConverter();

            protected internal override Box<T>? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
            {
                if (reader.TokenType is JsonTokenType.Null)
                {
                    return null;
                }

                using JsonDocument document = JsonDocument.ParseValue(ref reader);
                JsonElement root = document.RootElement;

                switch (root.ValueKind)
                {
                    case JsonValueKind.Object:
                    {
                        Guid id = root.TryGetProperty(nameof(Box<T>.Id), options, out JsonElement jid) && Guid.TryParse(jid.ToString(), out Guid guid) ? guid : Guid.Empty;
                        T? value = root.TryGetProperty(nameof(Box<T>.Value), options, out JsonElement jvalue) ? jvalue.Deserialize<T>(options) : default;
                        Boolean mutable = root.TryGetProperty(nameof(Box<T>.IsReadOnly), options, out JsonElement jmutable) && !jmutable.GetBoolean();

                        return id == Guid.Empty ? new Box<T>(value!, mutable) : new Box<T>(id, value!, mutable);
                    }
                    default:
                    {
                        return new Box<T>(root.Deserialize<T>(options)!);
                    }
                }
            }

            protected internal override Boolean Write(in Utf8JsonWriter writer, Box<T>? value, ref SerializerOptions options)
            {
                switch (value)
                {
                    case null:
                    {
                        writer.WriteNullValue();
                        return true;
                    }
                    case { Value: not null, IsReadOnly: true } when value.Id == Guid.Empty:
                    {
                        JsonSerializer.Serialize(writer, value.Value, options);
                        return true;
                    }
                    default:
                    {
                        writer.WriteStartObject();

                        if (value.Id != Guid.Empty)
                        {
                            writer.WritePropertyName(nameof(Box<T>.Id), options);
                            writer.WriteStringValue(value.Id);
                        }

                        writer.WritePropertyName(nameof(Box<T>.Value), options);
                        JsonSerializer.Serialize(writer, value.Value, options);

                        if (!value.IsReadOnly)
                        {
                            writer.WritePropertyName(nameof(Box<T>.IsReadOnly), options);
                            writer.WriteBooleanValue(false);
                        }

                        writer.WriteEndObject();
                        return true;
                    }
                }
            }
        }

        public sealed class NoIdBoxJsonConverter : TextJsonConverter<Box<T>>
        {
            public static NoIdBoxJsonConverter Instance { get; } = new NoIdBoxJsonConverter();

            protected internal override Box<T>? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
            {
                if (reader.TokenType is JsonTokenType.Null)
                {
                    return null;
                }

                using JsonDocument document = JsonDocument.ParseValue(ref reader);
                JsonElement root = document.RootElement;

                switch (root.ValueKind)
                {
                    case JsonValueKind.Object:
                    {
                        Guid id = root.TryGetProperty(nameof(Box<T>.Id), options, out JsonElement jid) && Guid.TryParse(jid.ToString(), out Guid guid) ? guid : Guid.Empty;
                        T? value = root.TryGetProperty(nameof(Box<T>.Value), options, out JsonElement jvalue) ? jvalue.Deserialize<T>(options) : default;
                        Boolean mutable = root.TryGetProperty(nameof(Box<T>.IsReadOnly), options, out JsonElement jmutable) && !jmutable.GetBoolean();

                        return id == Guid.Empty ? new Box<T>(value!, mutable) : new Box<T>(id, value!, mutable);
                    }
                    default:
                    {
                        return new Box<T>(root.Deserialize<T>(options)!);
                    }
                }
            }

            protected internal override Boolean Write(in Utf8JsonWriter writer, Box<T>? value, ref SerializerOptions options)
            {
                switch (value)
                {
                    case null:
                    {
                        writer.WriteNullValue();
                        return true;
                    }
                    case { Value: not null, IsReadOnly: true }:
                    {
                        JsonSerializer.Serialize(writer, value.Value, options);
                        return true;
                    }
                    default:
                    {
                        writer.WriteStartObject();

                        writer.WritePropertyName(nameof(Box<T>.Value), options);
                        JsonSerializer.Serialize(writer, value.Value, options);

                        if (!value.IsReadOnly)
                        {
                            writer.WritePropertyName(nameof(Box<T>.IsReadOnly), options);
                            writer.WriteBooleanValue(false);
                        }

                        writer.WriteEndObject();
                        return true;
                    }
                }
            }
        }
    }
}