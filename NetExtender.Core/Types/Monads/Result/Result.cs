// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads.Result
{
    [Serializable]
    public readonly struct Result<T> : IEqualityStruct<Result<T>>, IResult<T, Exception>, IResultEquality<T, Result<T>>, IResultEquality<T, Result<T, Exception>>, IResultEquality<T, BusinessResult<T>>, ICloneable<Result<T>>, ISerializable
    {
        public static implicit operator Result<T, Exception>(Result<T> value)
        {
            return new Result<T, Exception>(value.Internal);
        }
        
        public static implicit operator Boolean(Result<T> value)
        {
            return value.Internal;
        }

        public static implicit operator T(Result<T> value)
        {
            return value.Internal;
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }

        public static implicit operator Exception?(Result<T> value)
        {
            return value.Internal;
        }

        public static implicit operator Result<T>(Exception? value)
        {
            return value is not null ? new Result<T>(value) : default;
        }

        public static implicit operator Result<T>(ExceptionWrapper? value)
        {
            return value is not null ? new Result<T>(value.Exception) : default;
        }

        public static Boolean operator ==(T? first, Result<T> second)
        {
            return first == second.Internal;
        }

        public static Boolean operator !=(T? first, Result<T> second)
        {
            return first != second.Internal;
        }

        public static Boolean operator ==(Result<T> first, T? second)
        {
            return first.Internal == second;
        }

        public static Boolean operator !=(Result<T> first, T? second)
        {
            return first.Internal != second;
        }
        
        public static Boolean operator ==(Result<T> first, Result<T> second)
        {
            return first.Internal == second.Internal;
        }

        public static Boolean operator !=(Result<T> first, Result<T> second)
        {
            return first.Internal != second.Internal;
        }

        public static Boolean operator >(T? first, Result<T> second)
        {
            return first > second.Internal;
        }

        public static Boolean operator >=(T? first, Result<T> second)
        {
            return first >= second.Internal;
        }

        public static Boolean operator <(T? first, Result<T> second)
        {
            return first < second.Internal;
        }

        public static Boolean operator <=(T? first, Result<T> second)
        {
            return first <= second.Internal;
        }

        public static Boolean operator >(Result<T> first, T? second)
        {
            return first.Internal > second;
        }

        public static Boolean operator >=(Result<T> first, T? second)
        {
            return first.Internal >= second;
        }

        public static Boolean operator <(Result<T> first, T? second)
        {
            return first.Internal < second;
        }

        public static Boolean operator <=(Result<T> first, T? second)
        {
            return first.Internal <= second;
        }

        public static Boolean operator >(Result<T> first, Result<T> second)
        {
            return first.Internal > second.Internal;
        }

        public static Boolean operator >=(Result<T> first, Result<T> second)
        {
            return first.Internal >= second.Internal;
        }

        public static Boolean operator <(Result<T> first, Result<T> second)
        {
            return first.Internal < second.Internal;
        }

        public static Boolean operator <=(Result<T> first, Result<T> second)
        {
            return first.Internal <= second.Internal;
        }

        private readonly Result<T, Exception> Internal;
        
        public T Value
        {
            get
            {
                return Internal.Value;
            }
        }

        Object? IResult.Value
        {
            get
            {
                return Value;
            }
        }

        public Exception? Exception
        {
            get
            {
                return Internal.Exception;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Internal.IsEmpty;
            }
        }

        public Result(T value)
        {
            Internal = new Result<T, Exception>(value);
        }

        public Result(Exception exception)
        {
            Internal = new Result<T, Exception>(exception);
        }

        internal Result(T value, Exception? exception)
        {
            Internal = new Result<T, Exception>(value, exception);
        }

        internal Result(Result<T> value)
        {
            Internal = value.Internal;
        }

        internal Result(Result<T, Exception> value)
        {
            Internal = value;
        }

        internal Result(SerializationInfo info, StreamingContext context)
        {
            Internal = new Result<T, Exception>(info, context);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Internal.GetObjectData(info, context);
        }

        public void Deconstruct(out T value, out Exception? exception)
        {
            (value, exception) = Internal;
        }

        public Int32 CompareTo(T? other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(Result<T> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(Result<T> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(Result<T, Exception> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(Result<T, Exception> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(BusinessResult<T> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(BusinessResult<T> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(IResult<T>? other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(IResult<T>? other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(T? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(Exception? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Result<T> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Result<T> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(Result<T, Exception> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Result<T, Exception> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(BusinessResult<T> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(BusinessResult<T> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(IResult<T>? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(IResult<T>? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Result<T> Clone()
        {
            return new Result<T>(Internal.Clone());
        }

        IResult<T, Exception> IResult<T, Exception>.Clone()
        {
            return Clone();
        }

        IResult<T, Exception> ICloneable<IResult<T, Exception>>.Clone()
        {
            return Clone();
        }

        IResult<T> IResult<T>.Clone()
        {
            return Clone();
        }

        IResult<T> ICloneable<IResult<T>>.Clone()
        {
            return Clone();
        }

        IResult IResult.Clone()
        {
            return Clone();
        }

        IResult ICloneable<IResult>.Clone()
        {
            return Clone();
        }

        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        IMonad IMonad.Clone()
        {
            return Clone();
        }

        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }

        public String ToString(String? format)
        {
            return Internal.ToString(format);
        }

        public String ToString(IFormatProvider? provider)
        {
            return Internal.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Internal.ToString(format, provider);
        }

        public String? GetString()
        {
            return Internal.GetString();
        }

        public String? GetString(EscapeType escape)
        {
            return Internal.GetString(escape);
        }

        public String? GetString(String? format)
        {
            return Internal.GetString(format);
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return Internal.GetString(escape, format);
        }

        public String? GetString(IFormatProvider? provider)
        {
            return Internal.GetString(provider);
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return Internal.GetString(escape, provider);
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return Internal.GetString(format, provider);
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return Internal.GetString(escape, format, provider);
        }
    }
    
    [Serializable]
    public readonly struct Result<T, TException> : IEqualityStruct<Result<T, TException>>, IResult<T, TException>, IResultEquality<T, Result<T>>, IResultEquality<T, Result<T, TException>>, IResultEquality<T, BusinessResult<T>>, ICloneable<Result<T, TException>>, ISerializable where TException : Exception
    {
        public static implicit operator Result<T>(Result<T, TException> value)
        {
            return new Result<T>(new Result<T, Exception>(value.Internal, value.Exception));
        }
        
        public static implicit operator Boolean(Result<T, TException> value)
        {
            return value.Exception is null;
        }

        public static implicit operator T(Result<T, TException> value)
        {
            return value.Value;
        }

        public static implicit operator Result<T, TException>(T value)
        {
            return new Result<T, TException>(value);
        }

        public static implicit operator TException?(Result<T, TException> value)
        {
            return value.Exception;
        }

        public static implicit operator Result<T, TException>(TException? value)
        {
            return value is not null ? new Result<T, TException>(value) : default;
        }

        public static implicit operator Result<T, TException>(ExceptionWrapper<TException>? value)
        {
            return value is not null ? new Result<T, TException>(value.Exception) : default;
        }

        public static Boolean operator ==(T? first, Result<T, TException> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, Result<T, TException> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(Result<T, TException> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Result<T, TException> first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(Result<T, TException> first, Result<T, TException> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Result<T, TException> first, Result<T, TException> second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, Result<T, TException> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, Result<T, TException> second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, Result<T, TException> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, Result<T, TException> second)
        {
            return second >= first;
        }

        public static Boolean operator >(Result<T, TException> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Result<T, TException> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Result<T, TException> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Result<T, TException> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Result<T, TException> first, Result<T, TException> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Result<T, TException> first, Result<T, TException> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(Result<T, TException> first, Result<T, TException> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Result<T, TException> first, Result<T, TException> second)
        {
            return first.CompareTo(second) <= 0;
        }

        private readonly Maybe<T> _value;
        internal T Internal
        {
            get
            {
                return _value.Internal;
            }
        }

        public T Value
        {
            get
            {
                return Exception is null ? _value.Internal : throw Exception;
            }
        }

        Object? IResult.Value
        {
            get
            {
                return Value;
            }
        }

        public TException? Exception { get; }

        Exception? IResult.Exception
        {
            get
            {
                return Exception;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Exception is null && !_value.HasValue;
            }
        }
        
        public Result(T value)
            : this(value, null)
        {
        }

        public Result(TException exception)
            : this(default!, exception)
        {
        }

        internal Result(T value, TException? exception)
        {
            _value = value;
            Exception = exception;
        }

        internal Result(Result<T, TException> value)
        {
            _value = value._value;
            Exception = value.Exception;
        }

        internal Result(SerializationInfo info, StreamingContext context)
        {
            _value = info.GetValue<T>(nameof(Value));
            Exception = info.GetValue<TException>(nameof(Exception));
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), Internal);
            info.AddValue(nameof(Exception), Exception);
        }

        public void Deconstruct(out T value, out TException? exception)
        {
            value = _value.Internal;
            exception = Exception;
        }

        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Exception is not null ? 1 : comparer.SafeCompare(Internal, other) ?? 0;
        }

        public Int32 CompareTo(Result<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(Result<T> other, IComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }
            
            return comparer.SafeCompare(Internal, other.Value) ?? 0;
        }

        public Int32 CompareTo(Result<T, TException> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(Result<T, TException> other, IComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }
            
            return comparer.SafeCompare(Internal, other.Internal) ?? 0;
        }

        public Int32 CompareTo(BusinessResult<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(BusinessResult<T> other, IComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }
            
            return comparer.SafeCompare(Internal, other.Value) ?? 0;
        }

        public Int32 CompareTo(IResult<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IResult<T>? other, IComparer<T>? comparer)
        {
            if (other is null)
            {
                return 1;
            }
            
            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }
            
            return comparer.SafeCompare(Internal, other.Value) ?? 0;
        }

        public override Int32 GetHashCode()
        {
            return Exception is not null ? Exception.GetHashCode() : _value.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                null => _value.Equals(other),
                T value => Equals(value, comparer),
                Exception value => Equals(value),
                Result<T, TException> value => Equals(value, comparer),
                IResult<T> value => Equals(value, comparer),
                _ when Internal is not null => Exception is null && Internal.Equals(other),
                _ => false
            };
        }

        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return Exception is null && comparer.Equals(Value, other);
        }

        public Boolean Equals(Exception? other)
        {
            return Exception is not null && Equals(Exception, other);
        }

        public Boolean Equals(Result<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Result<T> other, IEqualityComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }
            
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Internal, other.Value);
        }

        public Boolean Equals(Result<T, TException> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Result<T, TException> other, IEqualityComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }
            
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Internal, other.Internal);
        }

        public Boolean Equals(BusinessResult<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(BusinessResult<T> other, IEqualityComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }
            
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Internal, other.Value);
        }

        public Boolean Equals(IResult<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IResult<T>? other, IEqualityComparer<T>? comparer)
        {
            if (other is null)
            {
                return false;
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }
            
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Internal, other.Value);
        }

        public Result<T, TException> Clone()
        {
            return this;
        }

        IResult<T, TException> IResult<T, TException>.Clone()
        {
            return Clone();
        }

        IResult<T, TException> ICloneable<IResult<T, TException>>.Clone()
        {
            return Clone();
        }

        IResult<T> IResult<T>.Clone()
        {
            return Clone();
        }

        IResult<T> ICloneable<IResult<T>>.Clone()
        {
            return Clone();
        }

        IResult IResult.Clone()
        {
            return Clone();
        }

        IResult ICloneable<IResult>.Clone()
        {
            return Clone();
        }

        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        IMonad IMonad.Clone()
        {
            return Clone();
        }

        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public override String? ToString()
        {
            return Exception is null ? _value.ToString() : Exception.ToString();
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Exception is null ? _value.ToString(format, provider) : Exception.ToString();
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public String? GetString()
        {
            return Exception is null ? _value.GetString() : Exception.ToString();
        }

        public String? GetString(EscapeType escape)
        {
            return Exception is null ? _value.GetString(escape) : Exception.ToString();
        }

        public String? GetString(String? format)
        {
            return Exception is null ? _value.GetString(format) : Exception.ToString();
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return Exception is null ? _value.GetString(escape, format) : Exception.ToString();
        }

        public String? GetString(IFormatProvider? provider)
        {
            return Exception is null ? _value.GetString(provider) : Exception.ToString();
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return Exception is null ? _value.GetString(escape, provider) : Exception.ToString();
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return Exception is null ? _value.GetString(format, provider) : Exception.ToString();
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return Exception is null ? _value.GetString(escape, format, provider) : Exception.ToString();
        }
    }
}