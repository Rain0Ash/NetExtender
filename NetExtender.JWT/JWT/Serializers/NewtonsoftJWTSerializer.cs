// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Text;
using NetExtender.Types.Exceptions;
using Newtonsoft.Json;

namespace NetExtender.JWT
{
    public class NewtonsoftJWTSerializer : JWTSerializer
    {
        protected JsonSerializer Json { get; }

        public sealed override JWTSerializerType Type
        {
            get
            {
                return JWTSerializerType.Newtonsoft;
            }
        }

        public NewtonsoftJWTSerializer()
            : this(JsonSerializer.CreateDefault())
        {
        }

        public NewtonsoftJWTSerializer(JsonSerializer serializer)
        {
            Json = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public NewtonsoftJWTSerializer(JsonSerializerSettings settings)
            : this(settings is not null ? JsonSerializer.Create(settings) : throw new ArgumentNullException(nameof(settings)))
        {
        }

        public override String Serialize<T>(T value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            StringBuilder builder = new StringBuilder(256);
            using StringWriter @string = new StringWriter(builder);
            using JsonTextWriter writer = new JsonTextWriter(@string);
            
            Json.Serialize(writer, value);
            return builder.ToString();
        }

        public override Object Deserialize(Type type, String json)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (String.IsNullOrEmpty(json))
            {
                throw new ArgumentNullOrEmptyStringException(json, nameof(json));
            }

            using StringReader reader = new StringReader(json);
            using JsonTextReader text = new JsonTextReader(reader);
            return Json.Deserialize(text, type) ?? throw new ArgumentException($"Can't deserialize json to '{type}'.");
        }

        public override T Deserialize<T>(String json)
        {
            if (String.IsNullOrEmpty(json))
            {
                throw new ArgumentNullOrEmptyStringException(json, nameof(json));
            }

            using StringReader reader = new StringReader(json);
            using JsonTextReader text = new JsonTextReader(reader);
            return Json.Deserialize<T>(text) ?? throw new ArgumentException($"Can't deserialize json to '{typeof(T).Name}'.");
        }

        public class Serializer : NewtonsoftJWTSerializer
        {
            public Serializer()
            {
            }

            public Serializer(JsonSerializer serializer)
                : base(serializer)
            {
            }

            public Serializer(JsonSerializerSettings settings)
                : base(settings)
            {
            }

            public sealed override String Serialize<T>(T value)
            {
                return base.Serialize(value);
            }

            public sealed override Object Deserialize(Type type, String json)
            {
                return base.Deserialize(type, json);
            }

            public sealed override T Deserialize<T>(String json)
            {
                return base.Deserialize<T>(json);
            }
        }
    }
}
