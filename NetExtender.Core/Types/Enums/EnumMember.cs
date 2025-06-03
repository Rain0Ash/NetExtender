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
    /// <summary>
    /// Represents the member information of the constant in the specified enumeration.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    public sealed record EnumMember<T> where T : unmanaged, Enum
    {
        /// <summary>
        /// Gets the name of specified enumration member.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets the value of specified enumration member.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the <see cref="System.Reflection.FieldInfo"/> of specified enumration member.
        /// </summary>
        public FieldInfo? FieldInfo { get; }

        /// <summary>
        /// Gets the <see cref="System.Runtime.Serialization.EnumMemberAttribute"/> of specified enumration member.
        /// </summary>
        public EnumMemberAttribute? EnumMemberAttribute { get; }

        /// <summary>
        /// Gets the labels of specified enumration member.
        /// </summary>
        public IImmutableDictionary<Int32, String>? Labels { get; }

        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="name"></param>
        public EnumMember(String name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = Enum.TryParse(name, out T value) ? value : throw new ArgumentException($"Can't parse enum name '{name}' of type '{typeof(T).Name}'.", nameof(name));
            FieldInfo = typeof(T).GetField(name);
            EnumMemberAttribute = FieldInfo?.GetCustomAttribute<EnumMemberAttribute>();
            Labels = FieldInfo?.GetCustomAttributes<EnumLabelAttribute>().ToImmutableDictionary(static attribute => attribute.Index, static attribute => attribute.Value);
        }

        /// <summary>
        /// Gets the Attributes of specified enumration member.
        /// </summary>
        /// <typeparam name="TAttribute">Attribute Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            return EnumUtilities.AttributesStorage<T, TAttribute>.Get(Value);
        }

        /// <summary>
        /// Deconstruct into name and value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Deconstruct(out String name, out T value)
        {
            name = Name;
            value = Value;
        }

        /// <summary>
        /// Provides <see cref="IEqualityComparer{T}"/> by <see cref="Value"/>.
        /// </summary>
        internal sealed class ValueComparer : IEqualityComparer<EnumMember<T>?>
        {
            public Boolean Equals(EnumMember<T>? x, EnumMember<T>? y)
            {
                return EqualityComparer<T?>.Default.Equals(x?.Value, y?.Value);
            }

            public Int32 GetHashCode(EnumMember<T> value)
            {
                return EqualityComparer<T>.Default.GetHashCode(value.Value);
            }
        }
    }
}