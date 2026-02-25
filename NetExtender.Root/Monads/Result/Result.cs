// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NetExtender.Interfaces;
using NetExtender.Newtonsoft.Types.Monads.Results;
using NetExtender.Exceptions;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(ResultJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.ResultJsonConverter<>))]
    public readonly struct Result<T> : IEqualityStruct<Result<T>>, IResult<T, Exception>, IResultEquality<T, Result<T>>, IResultEquality<T, Result<T, Exception>>, IResultEquality<T, BusinessResult<T>>, ICloneable<Result<T>>, ISerializable
    {
        public static implicit operator Result<T, Exception>(Result<T> value)
        {
            return new Result<T, Exception>(value.Internal);
        }

        public static implicit operator Task<Result<T, Exception>>(Result<T> value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<T, Exception>>(value) : TaskUtilities<Result<T, Exception>>.Default;
        }

        public static implicit operator ValueTask<Result<T, Exception>>(Result<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<T, Exception>>(value) : ValueTaskUtilities<Result<T, Exception>>.Default;
        }

        public static implicit operator Boolean(Result<T> value)
        {
            return value.Internal;
        }

        public static implicit operator T(Result<T> value)
        {
            return value.Internal;
        }

        public static implicit operator Maybe<T>(Result<T> value)
        {
            return value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator Task<Maybe<T>>(Result<T> value)
        {
            return value.Unwrap(out T? result) ? Task.FromResult<Maybe<T>>(result) : TaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator ValueTask<Maybe<T>>(Result<T> value)
        {
            return value.Unwrap(out T? result) ? ValueTask.FromResult<Maybe<T>>(result) : ValueTaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }

        public static implicit operator Exception?(Result<T> value)
        {
            return value.Internal;
        }

        public static implicit operator Task<Exception?>(Result<T> value)
        {
            return value.Exception is not null ? Task.FromResult<Exception?>(value.Exception) : TaskUtilities<Exception?>.Default;
        }

        public static implicit operator ValueTask<Exception?>(Result<T> value)
        {
            return value.Exception is not null ? ValueTask.FromResult<Exception?>(value.Exception) : ValueTaskUtilities<Exception?>.Default;
        }

        [StackTraceHidden]
        public static implicit operator Result<T>(Exception? value)
        {
            return value is not null ? new Result<T>(value.Throwable()) : default;
        }

        [StackTraceHidden]
        public static implicit operator Result<T>(ExceptionWrapper? value)
        {
            return value is not null ? new Result<T>(value.Exception.Throwable()) : default;
        }

        public static implicit operator Task<Result<T>>(Result<T> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<Result<T>>.Default;
        }

        public static implicit operator ValueTask<Result<T>>(Result<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<Result<T>>.Default;
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

        public static Boolean operator <(T? first, Result<T> second)
        {
            return first < second.Internal;
        }

        public static Boolean operator <=(T? first, Result<T> second)
        {
            return first <= second.Internal;
        }

        public static Boolean operator >(T? first, Result<T> second)
        {
            return first > second.Internal;
        }

        public static Boolean operator >=(T? first, Result<T> second)
        {
            return first >= second.Internal;
        }

        public static Boolean operator <(Result<T> first, T? second)
        {
            return first.Internal < second;
        }

        public static Boolean operator <=(Result<T> first, T? second)
        {
            return first.Internal <= second;
        }

        public static Boolean operator >(Result<T> first, T? second)
        {
            return first.Internal > second;
        }

        public static Boolean operator >=(Result<T> first, T? second)
        {
            return first.Internal >= second;
        }

        public static Boolean operator <(Result<T> first, Result<T> second)
        {
            return first.Internal < second.Internal;
        }

        public static Boolean operator <=(Result<T> first, Result<T> second)
        {
            return first.Internal <= second.Internal;
        }

        public static Boolean operator >(Result<T> first, Result<T> second)
        {
            return first.Internal > second.Internal;
        }

        public static Boolean operator >=(Result<T> first, Result<T> second)
        {
            return first.Internal >= second.Internal;
        }

        private readonly Result<T, Exception> Internal;

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Value;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? IResult.Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Value;
            }
        }

        public Exception? Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Exception;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Create(T value)
        {
            return new Result<T>(value);
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Create(Exception exception)
        {
            return new Result<T>(exception.Throwable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Throw()
        {
            Internal.Throw();
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (IsEmpty || Exception is not null)
            {
                value = Exception;
                return false;
            }

            value = Internal.Internal;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            if (IsEmpty || Exception is not null)
            {
                value = default;
                return false;
            }

            value = Internal.Internal;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IResult.Unwrap([MaybeNullWhen(false)] out Exception value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IResult<T, Exception>.Unwrap([MaybeNullWhen(false)] out Exception value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Internal.GetObjectData(info, context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out T value, out Exception? exception)
        {
            (value, exception) = Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<T> other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<T> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<T, Exception> other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<T, Exception> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BusinessResult<T> other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BusinessResult<T> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IResult<T>? other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IResult<T>? other, IComparer<T>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Result<T> other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Result<T, Exception> other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(BusinessResult<T> other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IResult<T>? other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Exception? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<T> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<T> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<T, Exception> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<T, Exception> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BusinessResult<T> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BusinessResult<T> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IResult<T>? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IResult<T>? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T> Clone()
        {
            return new Result<T>(Internal.Clone());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, Exception> IResult<T, Exception>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, Exception> ICloneable<IResult<T, Exception>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T> IResult<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T> ICloneable<IResult<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult IResult.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult ICloneable<IResult>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad IMonad.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Object ICloneable.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String? ToString()
        {
            return Internal.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format)
        {
            return Internal.ToString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(IFormatProvider? provider)
        {
            return Internal.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return Internal.ToString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return Internal.GetString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return Internal.GetString(escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return Internal.GetString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return Internal.GetString(escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return Internal.GetString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return Internal.GetString(escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return Internal.GetString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return Internal.GetString(escape, format, provider);
        }
    }

    [Serializable]
    [JsonConverter(typeof(ResultJsonConverter<,>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.ResultJsonConverter<,>))]
    public readonly struct Result<T, TException> : IEqualityStruct<Result<T, TException>>, IResult<T, TException>, IResultEquality<T, Result<T>>, IResultEquality<T, Result<T, TException>>, IResultEquality<T, BusinessResult<T>>, ICloneable<Result<T, TException>>, ISerializable where TException : Exception
    {
        public static implicit operator Result<T>(Result<T, TException> value)
        {
            return new Result<T>(new Result<T, Exception>(value.Internal, value.Exception));
        }

        public static implicit operator Task<Result<T>>(Result<T, TException> value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<T>>(value) : TaskUtilities<Result<T>>.Default;
        }

        public static implicit operator ValueTask<Result<T>>(Result<T, TException> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<T>>(value) : ValueTaskUtilities<Result<T>>.Default;
        }

        public static implicit operator Boolean(Result<T, TException> value)
        {
            return value.Exception is null;
        }

        public static implicit operator T(Result<T, TException> value)
        {
            return value.Value;
        }

        public static implicit operator Maybe<T>(Result<T, TException> value)
        {
            return value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator Task<Maybe<T>>(Result<T, TException> value)
        {
            return value.Unwrap(out T? result) ? Task.FromResult<Maybe<T>>(result) : TaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator ValueTask<Maybe<T>>(Result<T, TException> value)
        {
            return value.Unwrap(out T? result) ? ValueTask.FromResult<Maybe<T>>(result) : ValueTaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator Result<T, TException>(T value)
        {
            return new Result<T, TException>(value);
        }

        public static implicit operator TException?(Result<T, TException> value)
        {
            return value.Exception;
        }

        public static implicit operator Task<Exception?>(Result<T, TException> value)
        {
            return value.Exception is not null ? Task.FromResult<Exception?>(value.Exception) : TaskUtilities<Exception?>.Default;
        }

        public static implicit operator ValueTask<Exception?>(Result<T, TException> value)
        {
            return value.Exception is not null ? ValueTask.FromResult<Exception?>(value.Exception) : ValueTaskUtilities<Exception?>.Default;
        }

        public static implicit operator Task<TException?>(Result<T, TException> value)
        {
            return value.Exception is not null ? Task.FromResult<TException?>(value.Exception) : TaskUtilities<TException?>.Default;
        }

        public static implicit operator ValueTask<TException?>(Result<T, TException> value)
        {
            return value.Exception is not null ? ValueTask.FromResult<TException?>(value.Exception) : ValueTaskUtilities<TException?>.Default;
        }

        [StackTraceHidden]
        public static implicit operator Result<T, TException>(TException? value)
        {
            return value is not null ? new Result<T, TException>(value.Throwable()) : default;
        }

        [StackTraceHidden]
        public static implicit operator Result<T, TException>(ExceptionWrapper<TException>? value)
        {
            return value is not null ? new Result<T, TException>(value.Exception.Throwable()) : default;
        }

        public static implicit operator Task<Result<T, TException>>(Result<T, TException> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<Result<T, TException>>.Default;
        }

        public static implicit operator ValueTask<Result<T, TException>>(Result<T, TException> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<Result<T, TException>>.Default;
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

        public static Boolean operator <(T? first, Result<T, TException> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, Result<T, TException> second)
        {
            return second >= first;
        }

        public static Boolean operator >(T? first, Result<T, TException> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, Result<T, TException> second)
        {
            return second <= first;
        }

        public static Boolean operator <(Result<T, TException> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(Result<T, TException> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(Result<T, TException> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Result<T, TException> first, T? second)
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

        public static Boolean operator >(Result<T, TException> first, Result<T, TException> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(Result<T, TException> first, Result<T, TException> second)
        {
            return first.CompareTo(second) >= 0;
        }

        private readonly Maybe<T> _value;
        internal T Internal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _value.Internal;
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (Exception is { } exception)
                {
                    ExceptionDispatchInfo.Capture(exception).Throw();
                }

                return _value.Internal;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? IResult.Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Value;
            }
        }

        public TException? Exception { get; }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Exception? IResult.Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Create(T value)
        {
            return new Result<T, TException>(value);
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, TException> Create(TException exception)
        {
            return new Result<T, TException>(exception.Throwable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Throw()
        {
            if (Exception is { } exception)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
            }
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (IsEmpty || Exception is not null)
            {
                value = Exception;
                return false;
            }

            value = Internal;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            if (IsEmpty || Exception is not null)
            {
                value = default;
                return false;
            }

            value = Internal;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IResult.Unwrap([MaybeNullWhen(false)] out Exception value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IResult<T, TException>.Unwrap([MaybeNullWhen(false)] out TException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), Internal);
            info.AddValue(nameof(Exception), Exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out T value, out TException? exception)
        {
            value = _value.Internal;
            exception = Exception;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Exception is not null ? 1 : comparer.SafeCompare(Internal, other) ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return Exception is not null ? Exception.GetHashCode() : _value.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return Exception is null && _value.ReferenceEquals(other);
        }

        public Boolean ReferenceEquals(Result<T> other)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            return ReferenceEquals(other.Value);
        }

        public Boolean ReferenceEquals(Result<T, TException> other)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            return ReferenceEquals(other.Value);
        }

        public Boolean ReferenceEquals(BusinessResult<T> other)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            return ReferenceEquals(other.Value);
        }

        public Boolean ReferenceEquals(IResult<T>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            return ReferenceEquals(other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                Result<T> value => Equals(value, comparer),
                Result<T, TException> value => Equals(value, comparer),
                BusinessResult<T> value => Equals(value, comparer),
                IResult<T> value => Equals(value, comparer),
                _ when Internal is not null => Exception is null && Internal.Equals(other),
                _ => false
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return Exception is null && comparer.Equals(Value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Exception? other)
        {
            return Exception is not null && Equals(Exception, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, TException> Clone()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, TException> IResult<T, TException>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, TException> ICloneable<IResult<T, TException>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T> IResult<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T> ICloneable<IResult<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult IResult.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult ICloneable<IResult>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad IMonad.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Object ICloneable.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String? ToString()
        {
            return Exception is null ? _value.ToString() : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return Exception is null ? _value.ToString(format, provider) : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return Exception is null ? _value.GetString() : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return Exception is null ? _value.GetString(escape) : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return Exception is null ? _value.GetString(format) : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return Exception is null ? _value.GetString(escape, format) : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return Exception is null ? _value.GetString(provider) : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return Exception is null ? _value.GetString(escape, provider) : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return Exception is null ? _value.GetString(format, provider) : Exception.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return Exception is null ? _value.GetString(escape, format, provider) : Exception.ToString();
        }
    }
}