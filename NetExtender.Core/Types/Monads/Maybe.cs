// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Monads
{
    public readonly struct Maybe<T>
    {
        public static Boolean operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !(left == right);
        }
        
        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public static explicit operator T(Maybe<T> value)
        {
            return value.Value;
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
        
        public T GetValueOrDefault()
        {
            return Value;
        }
        
        public T GetValueOrDefault(T @default)
        {
            return HasValue ? Value : @default;
        }
        
        public override Boolean Equals(Object? other)
        {
            if (!HasValue)
            {
                return other == null;
            }

            return other is not null && Equals(Value, other);
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