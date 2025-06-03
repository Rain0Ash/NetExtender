// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using Newtonsoft.Json;

namespace NetExtender.NewtonSoft
{
    public sealed class PolymorphicJsonConverter : JsonConverter
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

        public override Object ReadJson(JsonReader reader, Type type, Object? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            switch (value)
            {
                case null:
                    serializer.Serialize(writer, default);
                    return;
                case String @string:
                    serializer.Serialize(writer, @string);
                    return;
                case System.Collections.IEnumerable enumerable:
                    serializer.Serialize(writer, enumerable.Cast<Object?>().ToArray());
                    return;
                default:
                    serializer.Serialize(writer, value);
                    return;
            }
        }
    }
}