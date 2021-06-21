using System;
using System.Collections.Immutable;
using Microsoft.Win32;
using NetExtender.Utils.Serialization;
using NetExtender.Utils.Registry;
using NetExtender.Utils.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetExtender.Registry
{
    [Serializable]
    public readonly struct RegistryEntry
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public RegistryKeys Key { get; init; }
        public IImmutableList<String> Sections { get; init; }
        
        [JsonIgnore]
        public String Path
        {
            get
            {
                return Sections.Join(Registry.Separator);
            }
        }

        [JsonIgnore]
        public String FullPath
        {
            get
            {
                return $"{Key.ToRegistryName()}{Registry.Separator}{Path}";
            }
        }
        
        public String Name { get; init; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public RegistryValueKind Kind { get; init; }
        
        public Object? Value { get; init; }

        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    };
}