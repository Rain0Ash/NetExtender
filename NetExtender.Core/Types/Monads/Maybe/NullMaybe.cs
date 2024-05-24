// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Monads
{
    [Serializable]
    public readonly struct NullMaybe<T> : INullMaybe<T>, IEquatable<Maybe<T>>, IEquatable<NullMaybe<T>>, IComparable<Maybe<T>>, IComparable<NullMaybe<T>>
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
        
        public static Boolean operator ==(T? first, NullMaybe<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, NullMaybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(NullMaybe<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(NullMaybe<T> first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(NullMaybe<T> first, NullMaybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(NullMaybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(NullMaybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, NullMaybe<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, NullMaybe<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, NullMaybe<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, NullMaybe<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(NullMaybe<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(NullMaybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(NullMaybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public T Value { get; }
        
        Object? INullMaybe.Value
        {
            get
            {
                return Value;
            }
        }

        public NullMaybe(T value)
        {
            Value = value;
        }
        
        private NullMaybe(SerializationInfo info, StreamingContext context)
        {
            Value = info.GetValue<T>(nameof(Value));
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), Value);
        }

        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            try
            {
                comparer ??= Comparer<T>.Default;
                return comparer.Compare(Value, other);
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        public Int32 CompareTo(Maybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(Maybe<T> other, IComparer<T>? comparer)
        {
            try
            {
                comparer ??= Comparer<T>.Default;
                return other.HasValue ? comparer.Compare(Value, other.Value) : 1;
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        public Int32 CompareTo(NullMaybe<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(NullMaybe<T> other, IComparer<T>? comparer)
        {
            try
            {
                comparer ??= Comparer<T>.Default;
                return comparer.Compare(Value, other.Value);
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        public Int32 CompareTo(IMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IMaybe<T>? other, IComparer<T>? comparer)
        {
            if (other is null)
            {
                return 1;
            }
            
            try
            {
                comparer ??= Comparer<T>.Default;
                return other.HasValue ? comparer.Compare(Value, other.Value) : 1;
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        public Int32 CompareTo(INullMaybe<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(INullMaybe<T>? other, IComparer<T>? comparer)
        {
            if (other is null)
            {
                return 1;
            }
            
            try
            {
                comparer ??= Comparer<T>.Default;
                return comparer.Compare(Value, other.Value);
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        public override Int32 GetHashCode()
        {
            return Value is not null ? Value.GetHashCode() : 0;
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                null => Value is null,
                T value => Equals(value, comparer),
                Maybe<T> value => Equals(value, comparer),
                NullMaybe<T> value => Equals(value),
                IMaybe<T> value => Equals(value),
                INullMaybe<T> value => Equals(value),
                _ => Equals(Value, other)
            };
        }

        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other);
        }

        public Boolean Equals(Maybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Maybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other.HasValue && comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(NullMaybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(NullMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(IMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && other.HasValue && comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(INullMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(INullMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && comparer.Equals(Value, other.Value);
        }

        public override String? ToString()
        {
            return Value?.ToString();
        }
    }
}