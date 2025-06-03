// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Linq;

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
    
    public sealed class PolymorphicJsonConverter : JsonConverter<Object>
    {
        public override Boolean CanConvert(Type type)
        {
            return type.IsAssignableTo(typeof(Object));
        }

        public override Object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
        {
            throw new NotSupportedException();
        }

        public override void Write(Utf8JsonWriter writer, Object? value, JsonSerializerOptions? options)
        {
            switch (value)
            {
                case null:
                    JsonSerializer.Serialize(writer, default(Object?), options);
                    return;
                case String @string:
                    JsonSerializer.Serialize(writer, @string, options);
                    return;
                case Collections.IEnumerable enumerable:
                    JsonSerializer.Serialize(writer, enumerable.Cast<Object?>().ToArray(), options);
                    return;
                default:
                    JsonSerializer.Serialize<Object?>(writer, value, options);
                    return;
            }
        }
    }
}