// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Monads
{
    [Serializable]
    public readonly struct Maybe<T>
    {
        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public static explicit operator T(Maybe<T> value)
        {
            return value.Value;
        }
        
        public static Boolean operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !(left == right);
        }
        
        private readonly T _value;

        public T Value
        {
            get
            {
                return HasValue ? _value : throw new InvalidOperationException();
            }
        }

        public Boolean HasValue { get; }

        public Maybe(T value)
        {
            _value = value;
            HasValue = true;
        }

        public void Deconstruct(out Boolean has, out T value)
        {
            has = HasValue;
            value = _value;
        }

        public override Boolean Equals(Object? other)
        {
            if (other is Maybe<T> maybe)
            {
                return Equals(maybe);
            }
            
            if (!HasValue)
            {
                return other is null;
            }

            return other is not null && Equals(Value, other);
        }
        
        public Boolean Equals(Maybe<T> other)
        {
            return !HasValue && !other.HasValue || HasValue && other.HasValue && Equals(Value, other.Value);
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