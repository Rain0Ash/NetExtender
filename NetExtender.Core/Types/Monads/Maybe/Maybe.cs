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
    public readonly struct Maybe<T> : IMaybe<T>, IEquatable<Maybe<T>>, IEquatable<NullMaybe<T>>, IComparable<Maybe<T>>, IComparable<NullMaybe<T>>
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

        public static Boolean operator ==(T? first, Maybe<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(Maybe<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Maybe<T> first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(Maybe<T> first, NullMaybe<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Maybe<T> first, NullMaybe<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, Maybe<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, Maybe<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, Maybe<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, Maybe<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Maybe<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Maybe<T> first, Maybe<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Maybe<T> first, NullMaybe<T> second)
        {
            return first.CompareTo(second) <= 0;
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
        
        private Maybe(SerializationInfo info, StreamingContext context)
        {
            HasValue = info.GetValue<Boolean>(nameof(HasValue));
            Internal = info.GetValue<T>(nameof(Value));
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(HasValue), HasValue);
            info.AddValue(nameof(Value), Internal);
        }

        public void Deconstruct(out T value, out Boolean notnull)
        {
            value = Internal;
            notnull = HasValue;
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
                
                if (other is null)
                {
                    return !HasValue ? 0 : comparer.Compare(Internal, other);
                }

                return HasValue ? comparer.Compare(Internal, other) : -1;
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
                return other.HasValue ? HasValue ? comparer.Compare(Internal, other.Value) : -1 : HasValue ? 1 : 0;
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
                return HasValue ? comparer.Compare(Internal, other.Value) : -1;
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
                return other.HasValue ? HasValue ? comparer.Compare(Internal, other.Value) : -1 : HasValue ? 1 : 0;
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
                return HasValue ? comparer.Compare(Internal, other.Value) : -1;
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        public override Int32 GetHashCode()
        {
            return HasValue ? Value?.GetHashCode() ?? 0 : 0;
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                T value => Equals(value, comparer),
                Maybe<T> value => Equals(value, comparer),
                NullMaybe<T> value => Equals(value, comparer),
                IMaybe<T> value => Equals(value, comparer),
                INullMaybe<T> value => Equals(value, comparer),
                _ => !HasValue ? other is null : other is not null && Equals(Value, other)
            };
        }

        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return HasValue && comparer.Equals(Value, other);
        }

        public Boolean Equals(Maybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Maybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return !HasValue && !other.HasValue || HasValue && other.HasValue && comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(NullMaybe<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(NullMaybe<T> other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return HasValue && comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(IMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            if (other is null)
            {
                return false;
            }
            
            comparer ??= EqualityComparer<T>.Default;
            return !HasValue && !other.HasValue || HasValue && other.HasValue && comparer.Equals(Value, other.Value);
        }

        public Boolean Equals(INullMaybe<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(INullMaybe<T>? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return other is not null && HasValue && comparer.Equals(Value, other.Value);
        }

        public override String? ToString()
        {
            return HasValue ? Value?.ToString() : null;
        }
    }
}