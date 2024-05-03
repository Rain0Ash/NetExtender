// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Types.Monads
{
    [Serializable]
    public readonly struct Maybe<T> : IMaybe<T>, IEquatable<Maybe<T>>, IEquatable<NullMaybe<T>>
    {
        public static implicit operator Boolean(Maybe<T> value)
        {
            return value.HasValue;
        }

        public static explicit operator T(Maybe<T> value)
        {
            return value.Value;
        }

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public static Boolean operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public Boolean HasValue { get; }
        private T Internal { get; }

        public T Value
        {
            get
            {
                return HasValue ? Internal : throw new InvalidOperationException();
            }
        }
        
        Object? IMaybe.Value
        {
            get
            {
                return Value;
            }
        }

        public Maybe(T value)
        {
            HasValue = true;
            Internal = value;
        }

        public void Deconstruct(out T value, out Boolean notnull)
        {
            value = Internal;
            notnull = HasValue;
        }

        public override Boolean Equals(Object? other)
        {
            switch (other)
            {
                case T item:
                    return Equals(item);
                case Maybe<T> maybe:
                    return Equals(maybe);
            }

            if (!HasValue)
            {
                return other is null;
            }

            return other is not null && Equals(Value, other);
        }

        public Boolean Equals(T? other)
        {
            return HasValue && EqualityComparer<T>.Default.Equals(Value, other);
        }

        public Boolean Equals(Maybe<T> other)
        {
            return !HasValue && !other.HasValue || HasValue && other.HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public Boolean Equals(IMaybe<T>? other)
        {
            if (other is null)
            {
                return false;
            }
            
            return !HasValue && !other.HasValue || HasValue && other.HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public Boolean Equals(NullMaybe<T> other)
        {
            return HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public Boolean Equals(INullMaybe<T>? other)
        {
            return other is not null && HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override Int32 GetHashCode()
        {
            return HasValue ? Value?.GetHashCode() ?? 0 : 0;
        }

        public override String? ToString()
        {
            return HasValue ? Value?.ToString() : String.Empty;
        }
    }
}