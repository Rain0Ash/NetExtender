// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Linq;
using NetExtender.Serialization.Json;
using Newtonsoft.Json;

namespace NetExtender.Newtonsoft
{
    public sealed class PolymorphicJsonConverter : NewtonsoftJsonConverter
    {
        public override Boolean CanRead
        {
            get
            {
                return false;
            }
        }
        
        public override Boolean CanConvert(Type type)
        {
            return type.IsAssignableTo(typeof(Object));
        }

        protected internal override Object Read(in JsonReader reader, Type type, Object? value, ref SerializerOptions options)
        {
            throw new NotSupportedException();
        }

        protected internal override Boolean Write(in JsonWriter writer, Object? value, ref SerializerOptions options)
        {
            return value switch
            {
                null => options.Serialize(writer, null),
                String @string => options.Serialize(writer, @string),
                Object[] array => options.Serialize(writer, array),
                IEnumerable enumerable => options.Serialize(writer, enumerable.Cast<Object?>().ToArray()),
                _ => options.Serialize(writer, value)
            };
        }
    }
}

namespace System.Text.Json.Serialization
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PolymorphicJsonConverterAttribute : JsonConverterAttribute
    {
        public PolymorphicJsonConverterAttribute()
            : base(typeof(PolymorphicJsonConverter))
        {
        }
    }
    
    public sealed class PolymorphicJsonConverter : TextJsonConverter<Object?>
    {
        public override Boolean CanConvert(Type type)
        {
            return type.IsAssignableTo(typeof(Object));
        }

        protected internal override Object Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            throw new NotSupportedException();
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, Object? value, ref SerializerOptions options)
        {
            switch (value)
            {
                case null:
                    JsonSerializer.Serialize(writer, default(Object?), options);
                    return true;
                case String @string:
                    JsonSerializer.Serialize(writer, @string, options);
                    return true;
                case Object[] array:
                    JsonSerializer.Serialize(writer, array, options);
                    return true;
                case IEnumerable enumerable:
                    JsonSerializer.Serialize(writer, enumerable.Cast<Object?>().ToArray(), options);
                    return true;
                default:
                    JsonSerializer.Serialize<Object?>(writer, value, options);
                    return true;
            }
        }
    }
}