// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public class ActiveBindingPathTokenId : IEquatable<ActiveBindingPathTokenId>
    {
        public ActiveBindingPathTokenType Type { get; }
        public String Value { get; }
        public String? RelativeSource { get; set; }

        public ActiveBindingPathTokenId(ActiveBindingPathTokenType type, String value)
        {
            Type = type;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is ActiveBindingPathTokenId other && Equals(other);
        }
        
        public Boolean Equals(ActiveBindingPathTokenId? other)
        {
            return other is not null && other.Type == Type && other.Value == Value;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }
    }
}