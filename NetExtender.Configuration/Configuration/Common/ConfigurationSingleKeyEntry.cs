// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Configuration.Common
{
    [Serializable]
    public readonly struct ConfigurationSingleKeyEntry : IEquatable<ConfigurationSingleKeyEntry>
    {
        public static Boolean operator ==(ConfigurationSingleKeyEntry first, ConfigurationSingleKeyEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ConfigurationSingleKeyEntry first, ConfigurationSingleKeyEntry second)
        {
            return !(first == second);
        }

        public String Key { get; }
        public String? Value { get; }

        public ConfigurationSingleKeyEntry(String key)
            : this(key, null)
        {
        }

        public ConfigurationSingleKeyEntry(String key, String? value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value;
        }

        public override Boolean Equals(Object? other)
        {
            return other is ConfigurationSingleKeyEntry entry && Equals(entry);
        }

        public Boolean Equals(ConfigurationSingleKeyEntry other)
        {
            return Key == other.Key && Value == other.Value;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Key, Value);
        }

        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }
}