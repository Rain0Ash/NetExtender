// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Monads
{
    public sealed class Box<T> : IEquatable<T>, IEquatable<Box<T>>
    {
        [return: NotNullIfNotNull("box")]
        public static implicit operator T?(Box<T>? box)
        {
            return box is not null ? box.Value : default;
        }

        public static implicit operator Box<T>(T value)
        {
            return new Box<T>(value);
        }

        public static Boolean operator ==(Box<T>? first, Box<T>? second)
        {
            return EqualityComparer<Box<T>>.Default.Equals(first, second);
        }

        public static Boolean operator !=(Box<T>? first, Box<T>? second)
        {
            return !(first == second);
        }

        public T Value { get; }

        public Box(T value)
        {
            Value = value;
        }

        public override Int32 GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other is T value && Equals(value) || other is Box<T> box && Equals(box);
        }

        public Boolean Equals(T? other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        public Boolean Equals(Box<T>? other)
        {
            return other is not null && Equals(other.Value);
        }

        public override String? ToString()
        {
            return Value?.ToString();
        }
    }
}