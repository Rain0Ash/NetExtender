// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Win32;
using NetExtender.Utilities.Registry;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetExtender.Registry
{
    [Serializable]
    public readonly struct RegistryEntry : IEquatable<RegistryEntry>
    {
        public static Boolean operator ==(RegistryEntry first, RegistryEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(RegistryEntry first, RegistryEntry second)
        {
            return !(first == second);
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public RegistryKeys Key { get; init; }
        public ImmutableArray<String> Sections { get; init; }

        [JsonIgnore]
        public Int32 Length
        {
            get
            {
                return Sections.Length;
            }
        }

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

        public Boolean Equals(RegistryEntry other)
        {
            return Key == other.Key && Name == other.Name && Kind == other.Kind && Equals(Value, other.Value) && Sections.SequenceEqual(other.Sections);
        }

        public override Boolean Equals(Object? other)
        {
            return other is RegistryEntry entry && Equals(entry);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine((Int32) Key, Sections, Name, (Int32) Kind, Value);
        }

        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }
}