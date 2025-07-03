using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace NetExtender.Newtonsoft
{
    public abstract class NewtonsoftJsonConverter<T> : JsonConverter<T>
    {
        private Boolean? _read;
        public override Boolean CanRead
        {
            get
            {
                if (_read is { } result)
                {
                    return result;
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;
                Type type = GetType();
                MethodInfo method = type.GetMethod(nameof(Read), binding, new[] { typeof(JsonReader), typeof(Type), typeof(Maybe<T?>), typeof(SerializerOptions) }) ?? throw new MissingMethodException(type.Name, nameof(Read));
                return (_read = method.HasImplementation()) is true;
            }
        }

        private Boolean? _write;
        public override Boolean CanWrite
        {
            get
            {
                if (_write is { } result)
                {
                    return result;
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;
                Type type = GetType();
                MethodInfo method = type.GetMethod(nameof(Write), binding, new[] { typeof(JsonWriter), typeof(T), typeof(SerializerOptions) }) ?? throw new MissingMethodException(type.Name, nameof(Write));
                return (_write = method.HasImplementation()) is true;
            }
        }

        public sealed override T? ReadJson(JsonReader reader, Type type, T? exist, Boolean exists, JsonSerializer serializer)
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

            SerializerOptions options = serializer;
            return Read(reader, type, exists ? (Maybe<T>) exist! : default, ref options);
        }

        protected internal virtual T? Read(in JsonReader reader, Type type, Maybe<T> exist, ref SerializerOptions options)
        {
            return default;
        }

        public sealed override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }
            
            SerializerOptions options = serializer;
            Write(writer, value, ref options);
        }

        protected internal virtual Boolean Write(in JsonWriter writer, T? value, ref SerializerOptions options)
        {
            return false;
        }

        protected internal struct SerializerOptions : IEquatableStruct<SerializerOptions>, IEquatable<NamingStrategy>, IEquatable<JsonSerializer>
        {
            public static implicit operator SerializerOptions(JsonSerializer? value)
            {
                return new SerializerOptions(value);
            }

            public static implicit operator NamingStrategy(SerializerOptions value)
            {
                return value.Strategy;
            }

            public static implicit operator JsonSerializer(SerializerOptions value)
            {
                return value.Serializer;
            }
            
            public static Boolean operator ==(SerializerOptions first, SerializerOptions second)
            {
                return first._options == second._options;
            }

            public static Boolean operator !=(SerializerOptions first, SerializerOptions second)
            {
                return !(first == second);
            }

            private NewtonsoftJsonConverter.SerializerOptions _options;
            
            public JsonSerializer Serializer
            {
                get
                {
                    return _options.Serializer;
                }
            }

            public NamingStrategy Strategy
            {
                get
                {
                    return _options.Strategy;
                }
            }

            public readonly Boolean IsEmpty
            {
                get
                {
                    return _options.IsEmpty;
                }
            }

            public SerializerOptions(JsonSerializer? serializer)
            {
                _options = new NewtonsoftJsonConverter.SerializerOptions(serializer);
            }

            public Boolean Serialize(JsonWriter writer, Object? value)
            {
                return _options.Serialize(writer, value);
            }

            public Boolean Serialize(JsonWriter writer, Object? value, Type? type)
            {
                return _options.Serialize(writer, value, type);
            }

            public Boolean Serialize(JsonWriter writer, T? value)
            {
                return _options.Serialize(writer, value);
            }

            public Boolean Serialize<TValue>(JsonWriter writer, TValue? value)
            {
                return _options.Serialize(writer, value);
            }

            public T? Deserialize(JsonReader reader)
            {
                return _options.Deserialize<T>(reader);
            }

            public Object? Deserialize(JsonReader reader, Type? type)
            {
                return _options.Deserialize(reader, type);
            }

            public TResult? Deserialize<TResult>(JsonReader reader)
            {
                return _options.Deserialize<TResult>(reader);
            }

            public override readonly Int32 GetHashCode()
            {
                return _options.GetHashCode();
            }

            public override readonly Boolean Equals([NotNullWhen(true)] Object? other)
            {
                return _options.Equals(other);
            }

            public readonly Boolean Equals(SerializerOptions other)
            {
                return _options.Equals(other._options);
            }

            public readonly Boolean Equals(NamingStrategy? other)
            {
                return _options.Equals(other);
            }

            public readonly Boolean Equals(JsonSerializer? other)
            {
                return _options.Equals(other);
            }

            public override readonly String? ToString()
            {
                return _options.ToString();
            }
        }
    }

    public abstract class NewtonsoftJsonConverter : JsonConverter
    {
        private Boolean? _read;
        public override Boolean CanRead
        {
            get
            {
                if (_read is { } result)
                {
                    return result;
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;
                Type type = GetType();
                MethodInfo method = type.GetMethod(nameof(Read), binding, new[] { typeof(JsonReader), typeof(Type), typeof(Object), typeof(SerializerOptions) }) ?? throw new MissingMethodException(type.Name, nameof(Read));
                return (_read = method.HasImplementation()) is true;
            }
        }

        private Boolean? _write;
        public override Boolean CanWrite
        {
            get
            {
                if (_write is { } result)
                {
                    return result;
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;
                Type type = GetType();
                MethodInfo method = type.GetMethod(nameof(Write), binding, new[] { typeof(JsonWriter), typeof(Object), typeof(SerializerOptions) }) ?? throw new MissingMethodException(type.Name, nameof(Write));
                return (_write = method.HasImplementation()) is true;
            }
        }

        public abstract override Boolean CanConvert(Type type);

        public sealed override Object? ReadJson(JsonReader reader, Type type, Object? exist, JsonSerializer serializer)
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

            SerializerOptions options = serializer;
            return Read(reader, type, exist, ref options);
        }

        protected internal virtual Object? Read(in JsonReader reader, Type type, Object? exist, ref SerializerOptions options)
        {
            return null;
        }

        public sealed override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            SerializerOptions options = serializer;
            Write(writer, value, ref options);
        }

        protected internal virtual Boolean Write(in JsonWriter writer, Object? value, ref SerializerOptions options)
        {
            return false;
        }
        
        protected internal struct SerializerOptions : IEquatableStruct<SerializerOptions>, IEquatable<NamingStrategy>, IEquatable<JsonSerializer>
        {
            public static implicit operator SerializerOptions(JsonSerializer? value)
            {
                return new SerializerOptions(value);
            }

            public static implicit operator NamingStrategy(SerializerOptions value)
            {
                return value.Strategy;
            }

            public static implicit operator JsonSerializer(SerializerOptions value)
            {
                return value.Serializer;
            }
            
            public static Boolean operator ==(SerializerOptions first, SerializerOptions second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(SerializerOptions first, SerializerOptions second)
            {
                return !(first == second);
            }
            
            private JsonSerializer? _serializer;
            public JsonSerializer Serializer
            {
                get
                {
                    return _serializer ??= GetSerializer();
                }
            }

            private NamingStrategy? _strategy = null;
            public NamingStrategy Strategy
            {
                get
                {
                    return _strategy ??= GetStrategy()!;
                }
            }

            public readonly Boolean IsEmpty
            {
                get
                {
                    return _serializer is null;
                }
            }

            public SerializerOptions(JsonSerializer? serializer)
            {
                _serializer = serializer;
            }

            private readonly JsonSerializer GetSerializer()
            {
                return _serializer ?? DefaultJsonSerializerSettings.Serializer;
            }

            private readonly NamingStrategy? GetStrategy()
            {
                return _strategy ?? GetSerializer().GetNamingStrategy();
            }

            public Boolean Serialize(JsonWriter? writer, Object? value)
            {
                if (writer is null)
                {
                    return false;
                }
                
                Serializer.Serialize(writer, value);
                return true;
            }

            public Boolean Serialize(JsonWriter? writer, Object? value, Type? type)
            {
                if (writer is null)
                {
                    return false;
                }

                Serializer.Serialize(writer, value, type);
                return true;
            }

            public Boolean Serialize<T>(JsonWriter? writer, T? value)
            {
                if (writer is null)
                {
                    return false;
                }

                Serializer.Serialize(writer, value);
                return true;
            }

            public Object? Deserialize(JsonReader reader)
            {
                return Serializer.Deserialize(reader);
            }

            public Object? Deserialize(JsonReader reader, Type? type)
            {
                return Serializer.Deserialize(reader, type);
            }

            public T? Deserialize<T>(JsonReader reader)
            {
                return Serializer.Deserialize<T>(reader);
            }

            public override readonly Int32 GetHashCode()
            {
                return GetStrategy()?.GetHashCode() ?? 0;
            }

            public override readonly Boolean Equals([NotNullWhen(true)] Object? other)
            {
                return other switch
                {
                    SerializerOptions value => Equals(value),
                    NamingStrategy value => Equals(value),
                    JsonSerializer value => Equals(value),
                    _ => false
                };
            }

            public readonly Boolean Equals(SerializerOptions other)
            {
                return Equals(other._serializer);
            }

            public readonly Boolean Equals(NamingStrategy? other)
            {
                NamingStrategy? strategy = GetStrategy();
                return ReferenceEquals(strategy, other) || strategy is not null && strategy.Equals(other);
            }

            public readonly Boolean Equals(JsonSerializer? other)
            {
                JsonSerializer options = GetSerializer();
                return ReferenceEquals(options, other) || options.Equals(other);
            }

            public override readonly String? ToString()
            {
                return GetStrategy()?.GetType().Name;
            }
        }
    }
}

namespace NetExtender.Serialization.Json
{
    using System.Text.Json;
    
    public abstract class TextJsonConverter<T> : System.Text.Json.Serialization.JsonConverter<T>
    {
        public override Boolean HandleNull
        {
            get
            {
                return true;
            }
        }

        public override Boolean CanConvert(Type type)
        {
            return base.CanConvert(type);
        }

        public sealed override T? Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions serializer)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            SerializerOptions options = serializer;
            return Read(ref reader, type, ref options);
        }

        protected internal abstract T? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options);

        public sealed override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            SerializerOptions options = serializer;
            Write(writer, value, ref options);
        }

        protected internal abstract Boolean Write(in Utf8JsonWriter writer, T value, ref SerializerOptions options);

        public sealed override T ReadAsPropertyName(ref Utf8JsonReader reader, Type type, JsonSerializerOptions serializer)
        {
            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            SerializerOptions options = serializer;
            return ReadAsPropertyName(ref reader, type, ref options);
        }

        protected internal virtual T ReadAsPropertyName(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            return base.ReadAsPropertyName(ref reader, type, options);
        }

        public sealed override void WriteAsPropertyName(Utf8JsonWriter writer, T value, JsonSerializerOptions serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            SerializerOptions options = serializer;
            WriteAsPropertyName(in writer, value, ref options);
        }

        protected internal virtual Boolean WriteAsPropertyName(in Utf8JsonWriter writer, T value, ref SerializerOptions options)
        {
            base.WriteAsPropertyName(writer, value, options);
            return true;
        }

        protected internal struct SerializerOptions : IEquatableStruct<SerializerOptions>, IEquatable<JsonNamingPolicy>, IEquatable<JsonSerializerOptions>
        {
            public static implicit operator SerializerOptions(JsonSerializerOptions? value)
            {
                return new SerializerOptions(value);
            }

            public static implicit operator JsonNamingPolicy(SerializerOptions value)
            {
                return value.Policy;
            }

            public static implicit operator JsonSerializerOptions(SerializerOptions value)
            {
                return value.Options;
            }
            
            public static Boolean operator ==(SerializerOptions first, SerializerOptions second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(SerializerOptions first, SerializerOptions second)
            {
                return !(first == second);
            }
            
            private JsonSerializerOptions? _options;
            public JsonSerializerOptions Options
            {
                get
                {
                    return _options ??= GetOptions();
                }
            }

            private JsonNamingPolicy? _policy = null;
            public JsonNamingPolicy Policy
            {
                get
                {
                    return _policy ??= GetPolicy()!;
                }
            }

            public readonly Boolean IsEmpty
            {
                get
                {
                    return _options is null;
                }
            }

            public SerializerOptions(JsonSerializerOptions? options)
            {
                _options = options;
            }

            private readonly JsonSerializerOptions GetOptions()
            {
                return _options ?? DefaultJsonSerializerOptions.Options;
            }

            private readonly JsonNamingPolicy? GetPolicy()
            {
                return _policy ?? _options?.GetNamingPolicy() ?? DefaultJsonSerializerOptions.Options.GetNamingPolicy();
            }

            public override readonly Int32 GetHashCode()
            {
                return GetPolicy()?.GetHashCode() ?? 0;
            }

            public override readonly Boolean Equals([NotNullWhen(true)] Object? other)
            {
                return other switch
                {
                    SerializerOptions value => Equals(value),
                    JsonNamingPolicy value => Equals(value),
                    JsonSerializerOptions value => Equals(value),
                    _ => false
                };
            }

            public readonly Boolean Equals(SerializerOptions other)
            {
                return Equals(other._options);
            }

            public readonly Boolean Equals(JsonNamingPolicy? other)
            {
                JsonNamingPolicy? policy = GetPolicy();
                return ReferenceEquals(policy, other) || policy is not null && policy.Equals(other);
            }

            public readonly Boolean Equals(JsonSerializerOptions? other)
            {
                JsonSerializerOptions options = GetOptions();
                return ReferenceEquals(options, other) || options.Equals(other);
            }

            public override readonly String? ToString()
            {
                return GetPolicy()?.GetType().Name;
            }
        }
    }
}