// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Environments
{
    [Serializable]
    public readonly struct EnvironmentValueEntry : IEquatable<EnvironmentValueEntry>
    {
        public static Boolean operator ==(EnvironmentValueEntry first, EnvironmentValueEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(EnvironmentValueEntry first, EnvironmentValueEntry second)
        {
            return !(first == second);
        }

        public String Key { get; }
        public String? Value { get; }

        public EnvironmentValueEntry(String key)
            : this(key, null)
        {
        }

        public EnvironmentValueEntry(String key, String? value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value;
        }

        public override Boolean Equals(Object? other)
        {
            return other is EnvironmentValueEntry entry && Equals(entry);
        }

        public Boolean Equals(EnvironmentValueEntry other)
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