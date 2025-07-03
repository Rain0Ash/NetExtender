using System;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Newtonsoft.Types.Objects
{
    internal sealed class NullObjectJsonConverter : NewtonsoftJsonConverter<NullObject>
    {
        protected internal override NullObject Read(in JsonReader reader, Type type, Maybe<NullObject> exist, ref SerializerOptions options)
        {
            return NullObject.Instance;
        }

        protected internal override Boolean Write(in JsonWriter writer, NullObject? value, ref SerializerOptions options)
        {
            writer.WriteNull();
            return true;
        }
    }
}

namespace NetExtender.Serialization.Json.Objects
{
    using System.Text.Json;

    internal sealed class NullObjectJsonConverter : TextJsonConverter<NullObject>
    {
        protected internal override NullObject Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            return NullObject.Instance;
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, NullObject value, ref SerializerOptions options)
        {
            writer.WriteNullValue();
            return true;
        }
    }
}