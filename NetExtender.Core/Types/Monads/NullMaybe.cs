// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Monads
{
    [Serializable]
    public readonly struct NullMaybe<T> : IEquatable<T>, IEquatable<Maybe<T>>, IEquatable<NullMaybe<T>>
    {
        public static implicit operator NullMaybe<T>(Maybe<T> value)
        {
            return value.HasValue ? new NullMaybe<T>(value.Value) : default;
        }
        
        public static implicit operator Maybe<T>(NullMaybe<T> value)
        {
            return new Maybe<T>(value);
        }
        
        public static implicit operator NullMaybe<T>(T value)
        {
            return new NullMaybe<T>(value);
        }

        public static implicit operator T(NullMaybe<T> value)
        {
            return value.Value;
        }
        
        public static Boolean operator ==(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(NullMaybe<T> first, NullMaybe<T> second)
        {
            return !(first == second);
        }

        public T Value { get; }

        public NullMaybe(T value)
        {
            Value = value;
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Value is null,
                T item => Equals(item),
                Maybe<T> maybe => Equals(maybe),
                NullMaybe<T> maybe => Equals(maybe),
                _ => Equals(Value, other)
            };
        }
        
        public Boolean Equals(T? other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }
        
        public Boolean Equals(Maybe<T> other)
        {
            return other.HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }
        
        public Boolean Equals(NullMaybe<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }
        
        public override Int32 GetHashCode()
        {
            return Value is not null ? Value?.GetHashCode() ?? 0 : 0;
        }

        public override String? ToString()
        {
            return Value?.ToString();
        }
    }
}