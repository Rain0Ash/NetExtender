using System;
using System.Runtime.CompilerServices;
using NetExtender.Newtonsoft.Types.Objects;
using Newtonsoft.Json;

namespace NetExtender.Utilities.Types
{
    public static class ObjectUtilities
    {
        public static Object Null
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return NullObject.Instance;
            }
        }
    }

    [JsonConverter(typeof(NullObjectJsonConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Objects.NullObjectJsonConverter))]
    internal sealed class NullObject
    {
        public static NullObject Instance { get; } = new NullObject();
        
        private NullObject()
        {
        }

        public override Int32 GetHashCode()
        {
            return 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other is null || other is NullObject;
        }

        public override String? ToString()
        {
            return null;
        }
    }
}