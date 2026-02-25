// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Enums.Attributes;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Enums
{
    public sealed record EnumMember<T> where T : unmanaged, Enum
    {
        public static IEqualityComparer<EnumMember<T>?> ValueComparer { get; } = new Comparer();

        public String Name { get; }
        public T Value { get; }
        public FieldInfo? FieldInfo { get; }
        public EnumMemberAttribute? EnumMemberAttribute { get; }

        public ImmutableDictionary<Int32, String>? Labels { get; }

        public EnumMember(String name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = Enum.TryParse(name, out T value) ? value : throw new ArgumentException($"Can't parse enum name '{name}' of type '{typeof(T).Name}'.", nameof(name));
            FieldInfo = typeof(T).GetField(name);
            EnumMemberAttribute = FieldInfo?.GetCustomAttribute<EnumMemberAttribute>();
            Labels = FieldInfo?.GetCustomAttributes<EnumLabelAttribute>().ToImmutableDictionary(static attribute => attribute.Index, static attribute => attribute.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            return EnumUtilities.AttributesStorage<T, TAttribute>.Get(Value);
        }

        public void Deconstruct(out String name, out T value)
        {
            name = Name;
            value = Value;
        }

        private sealed class Comparer : IEqualityComparer<EnumMember<T>?>
        {
            public Int32 GetHashCode(EnumMember<T> value)
            {
                return EqualityComparer<T>.Default.GetHashCode(value.Value);
            }

            public Boolean Equals(EnumMember<T>? x, EnumMember<T>? y)
            {
                return EqualityComparer<T?>.Default.Equals(x?.Value, y?.Value);
            }
        }
    }
}