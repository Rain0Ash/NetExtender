using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NetExtender.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    /*[JsonConverter(typeof(FlowResultJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.FlowResultJsonConverter<>))]*/
    public readonly struct FlowResult<T> : IEqualityStruct<FlowResult<T>>, IFlowResult<T, Exception>, IResultEquality<T, FlowResult<T>>, IResultEquality<T, FlowResult<T, Exception>>, ICloneable<FlowResult<T>>, ISerializable
    {
        public static implicit operator FlowResult<T, Exception>(FlowResult<T> value)
        {
            return new FlowResult<T, Exception>(value._next, value.Result);
        }

        public static implicit operator Task<FlowResult<T, Exception>>(FlowResult<T> value)
        {
            return !value.IsEmpty ? Task.FromResult<FlowResult<T, Exception>>(value) : TaskUtilities<FlowResult<T, Exception>>.Default;
        }

        public static implicit operator ValueTask<FlowResult<T, Exception>>(FlowResult<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<FlowResult<T, Exception>>(value) : ValueTaskUtilities<FlowResult<T, Exception>>.Default;
        }

        public static implicit operator Boolean(FlowResult<T> value)
        {
            return value.Exception is null;
        }

        public static implicit operator T(FlowResult<T> value)
        {
            return value.Value;
        }

        public static implicit operator Maybe<T>(FlowResult<T> value)
        {
            return value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator Result<T>(FlowResult<T> value)
        {
            return value.Exception is { } exception ? new Result<T>(exception) : value.Unwrap(out T? result) ? new Result<T>(result) : default;
        }

        public static implicit operator Result<Maybe<T>>(FlowResult<T> value)
        {
            return value.Exception is { } exception ? new Result<Maybe<T>>(exception) : value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator FlowResult<T>(T value)
        {
            return new FlowResult<T>(value);
        }

        public static implicit operator FlowResult<T>(Maybe<T> value)
        {
            return new FlowResult<T>(value);
        }

        public static implicit operator FlowResult<T>(Result<T> value)
        {
            return new FlowResult<T>(value);
        }

        public static implicit operator Exception?(FlowResult<T> value)
        {
            return value.Exception;
        }

        [StackTraceHidden]
        public static implicit operator FlowResult<T>(Exception? value)
        {
            return value is not null ? new FlowResult<T>(value.Throwable()) : default;
        }

        [StackTraceHidden]
        public static implicit operator FlowResult<T>(ExceptionWrapper? value)
        {
            return value is not null ? new FlowResult<T>(value.Exception.Throwable()) : default;
        }

        public static implicit operator Task<FlowResult<T>>(FlowResult<T> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<FlowResult<T>>.Default;
        }

        public static implicit operator ValueTask<FlowResult<T>>(FlowResult<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<FlowResult<T>>.Default;
        }

        public static Boolean operator ==(T? first, FlowResult<T> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, FlowResult<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(FlowResult<T> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(FlowResult<T> first, T? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(FlowResult<T> first, FlowResult<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(FlowResult<T> first, FlowResult<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator <(T? first, FlowResult<T> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, FlowResult<T> second)
        {
            return second >= first;
        }

        public static Boolean operator >(T? first, FlowResult<T> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, FlowResult<T> second)
        {
            return second <= first;
        }

        public static Boolean operator <(FlowResult<T> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(FlowResult<T> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(FlowResult<T> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(FlowResult<T> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(FlowResult<T> first, FlowResult<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(FlowResult<T> first, FlowResult<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(FlowResult<T> first, FlowResult<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(FlowResult<T> first, FlowResult<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        private readonly Maybe<T> _next;

        public Boolean HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _next.HasValue;
            }
        }

        public T Next
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _next.Internal;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? IFlowResult.Next
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Next;
            }
        }

        public Result<T> Result { get; }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? IFlowResult.Result
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Result.Unwrap(out T? result) ? result : null;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Result<T, Exception> IFlowResult<T, Exception>.Result
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Result;
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _next.Unwrap(out T? result) ? result : Result.Value;
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
                return Result.Exception;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _next.IsEmpty && Result.IsEmpty;
            }
        }

        public FlowResult(T next)
        {
            _next = next;
            Result = default;
        }

        public FlowResult(Maybe<T> next)
        {
            _next = next;
            Result = default;
        }

        public FlowResult(Result<T> result)
        {
            _next = default;
            Result = result;
        }

        public FlowResult(Exception exception)
        {
            _next = default;
            Result = exception;
        }

        internal FlowResult(Maybe<T> next, Result<T> result)
        {
            _next = next;
            Result = result;
        }

        internal FlowResult(SerializationInfo info, StreamingContext context)
        {
            if (info.GetBoolean(nameof(HasNext)))
            {
                _next = new Maybe<T>(info, context);
                Result = default;
            }
            else
            {
                _next = default;
                Result = new Result<T>(info, context);
            }
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FlowResult<T> Create(T value)
        {
            return new FlowResult<T>(value);
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FlowResult<T> Create(Exception exception)
        {
            return new FlowResult<T>(exception.Throwable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Throw()
        {
            if (!HasNext)
            {
                Result.Throw();
            }
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (HasNext && _next.Unwrap(out T? result) || Result.Unwrap(out result))
            {
                value = result;
                return true;
            }

            value = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            return HasNext ? _next.Unwrap(out value) : Result.Unwrap(out value);
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
            info.AddValue(nameof(HasNext), HasNext);

            if (HasNext)
            {
                _next.GetObjectData(info, context);
            }
            else
            {
                Result.GetObjectData(info, context);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return HasNext ? _next.CompareTo(other, comparer) : Result.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(FlowResult<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(FlowResult<T> other, IComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }

            if (HasNext && other.HasNext)
            {
                return comparer.SafeCompare(Next, other.Next) ?? 0;
            }

            if (!HasNext && !other.HasNext)
            {
                return comparer.SafeCompare(Result.Value, other.Result.Value) ?? 0;
            }

            Int32 comparison = comparer.SafeCompare(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value) ?? 0;

            if (comparison != 0)
            {
                return comparison;
            }

            return HasNext ? -1 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(FlowResult<T, Exception> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(FlowResult<T, Exception> other, IComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }

            if (HasNext && other.HasNext)
            {
                return comparer.SafeCompare(Next, other.Next) ?? 0;
            }

            if (!HasNext && !other.HasNext)
            {
                return comparer.SafeCompare(Result.Value, other.Result.Value) ?? 0;
            }

            Int32 comparison = comparer.SafeCompare(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value) ?? 0;

            if (comparison != 0)
            {
                return comparison;
            }

            return HasNext ? -1 : 1;
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

            if (other is IFlowResult<T> convert)
            {
                return CompareTo(convert, comparer);
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }

            return comparer.SafeCompare(Value, other.Value) ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IFlowResult<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IFlowResult<T>? other, IComparer<T>? comparer)
        {
            if (other is null)
            {
                return 1;
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }

            if (HasNext && other.HasNext)
            {
                return comparer.SafeCompare(Next, other.Next) ?? 0;
            }

            if (!HasNext && !other.HasNext)
            {
                return comparer.SafeCompare(Result.Value, other.Result.Value) ?? 0;
            }

            Int32 comparison = comparer.SafeCompare(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value) ?? 0;

            if (comparison != 0)
            {
                return comparison;
            }

            return HasNext ? -1 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return HasNext ? _next.GetHashCode() : Result.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return HasNext ? _next.ReferenceEquals(other) : Result.ReferenceEquals(other);
        }

        public Boolean ReferenceEquals(FlowResult<T> other)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.ReferenceEquals(other.Next) : Result.ReferenceEquals(other.Result);
            }

            return ReferenceEquals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
        }

        public Boolean ReferenceEquals(FlowResult<T, Exception> other)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.ReferenceEquals(other.Next) : Result.ReferenceEquals(other.Result);
            }

            return ReferenceEquals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
        }

        public Boolean ReferenceEquals(IResult<T>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (other is IFlowResult<T> convert)
            {
                return ReferenceEquals(convert);
            }

            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            return ReferenceEquals(Value, other.Value);
        }

        public Boolean ReferenceEquals(IFlowResult<T>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.ReferenceEquals(other.Next) : Result.ReferenceEquals(other.Result);
            }

            return ReferenceEquals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
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
                null => HasNext ? _next.Equals(other, comparer) : Result.Equals(other, comparer),
                T value => Equals(value, comparer),
                Exception value => Equals(value),
                FlowResult<T> value => Equals(value, comparer),
                FlowResult<T, Exception> value => Equals(value, comparer),
                IFlowResult<T> value => Equals(value, comparer),
                IResult<T> value => Equals(value, comparer),
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
            return HasNext ? _next.Equals(other, comparer) : Result.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Exception? other)
        {
            return Result.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(FlowResult<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(FlowResult<T> other, IEqualityComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.Equals(other.Next, comparer) : Result.Equals(other.Result, comparer);
            }

            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(FlowResult<T, Exception> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(FlowResult<T, Exception> other, IEqualityComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.Equals(other.Next, comparer) : Result.Equals(other.Result, comparer);
            }

            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
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

            if (other is IFlowResult<T> convert)
            {
                return Equals(convert, comparer);
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }

            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IFlowResult<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IFlowResult<T>? other, IEqualityComparer<T>? comparer)
        {
            if (other is null)
            {
                return false;
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.Equals(other.Next, comparer) : Result.Equals(other.Result, comparer);
            }

            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FlowResult<T> Clone()
        {
            return new FlowResult<T>(_next.Clone(), Result.Clone());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IFlowResult<T, Exception> IFlowResult<T, Exception>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IFlowResult<T, Exception> ICloneable<IFlowResult<T, Exception>>.Clone()
        {
            return Clone();
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
        IFlowResult<T> IFlowResult<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IFlowResult<T> ICloneable<IFlowResult<T>>.Clone()
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
        IFlowResult IFlowResult.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IFlowResult ICloneable<IFlowResult>.Clone()
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
            return HasNext ? _next.ToString() : Result.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format)
        {
            return HasNext ? _next.ToString(format) : Result.ToString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(IFormatProvider? provider)
        {
            return HasNext ? _next.ToString(provider) : Result.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return HasNext ? _next.ToString(format, provider) : Result.ToString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return HasNext ? _next.GetString() : Result.GetString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return HasNext ? _next.GetString(escape) : Result.GetString(escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return HasNext ? _next.GetString(format) : Result.GetString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return HasNext ? _next.GetString(escape, format) : Result.GetString(escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return HasNext ? _next.GetString(provider) : Result.GetString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return HasNext ? _next.GetString(escape, provider) : Result.GetString(escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return HasNext ? _next.GetString(format, provider) : Result.GetString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return HasNext ? _next.GetString(escape, format, provider) : Result.GetString(escape, format, provider);
        }
    }

    [Serializable]
    /*[JsonConverter(typeof(FlowResultJsonConverter<,>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.FlowResultJsonConverter<,>))]*/
    public readonly struct FlowResult<T, TException> : IEqualityStruct<FlowResult<T, TException>>, IFlowResult<T, TException>, IResultEquality<T, FlowResult<T>>, IResultEquality<T, FlowResult<T, TException>>, ICloneable<FlowResult<T, TException>>, ISerializable where TException : Exception
    {
        public static implicit operator FlowResult<T>(FlowResult<T, TException> value)
        {
            return new FlowResult<T>(value._next, value.Result);
        }

        public static implicit operator Task<FlowResult<T>>(FlowResult<T, TException> value)
        {
            return !value.IsEmpty ? Task.FromResult<FlowResult<T>>(value) : TaskUtilities<FlowResult<T>>.Default;
        }

        public static implicit operator ValueTask<FlowResult<T>>(FlowResult<T, TException> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<FlowResult<T>>(value) : ValueTaskUtilities<FlowResult<T>>.Default;
        }

        public static implicit operator Boolean(FlowResult<T, TException> value)
        {
            return value.Exception is null;
        }

        public static implicit operator T(FlowResult<T, TException> value)
        {
            return value.Value;
        }

        public static implicit operator Maybe<T>(FlowResult<T, TException> value)
        {
            return value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator Result<T>(FlowResult<T, TException> value)
        {
            return value.Exception is { } exception ? new Result<T>((Exception) exception) : value.Unwrap(out T? result) ? new Result<T>(result) : default;
        }

        public static implicit operator Result<T, TException>(FlowResult<T, TException> value)
        {
            return value.Exception is { } exception ? new Result<T, TException>(exception) : value.Unwrap(out T? result) ? new Result<T, TException>(result) : default;
        }

        public static implicit operator Result<Maybe<T>>(FlowResult<T, TException> value)
        {
            return value.Exception is { } exception ? new Result<Maybe<T>, TException>(exception) : value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator Result<Maybe<T>, TException>(FlowResult<T, TException> value)
        {
            return value.Exception is { } exception ? new Result<Maybe<T>, TException>(exception) : value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator FlowResult<T, TException>(T value)
        {
            return new FlowResult<T, TException>(value);
        }

        public static implicit operator FlowResult<T, TException>(Maybe<T> value)
        {
            return new FlowResult<T, TException>(value);
        }

        public static implicit operator FlowResult<T, TException>(Result<T> value)
        {
            return new FlowResult<T, TException>(value);
        }

        public static implicit operator TException?(FlowResult<T, TException> value)
        {
            return value.Exception;
        }

        [StackTraceHidden]
        public static implicit operator FlowResult<T, TException>(TException? value)
        {
            return value is not null ? new FlowResult<T, TException>(value.Throwable()) : default;
        }

        [StackTraceHidden]
        public static implicit operator FlowResult<T, TException>(ExceptionWrapper<TException>? value)
        {
            return value is not null ? new FlowResult<T, TException>(value.Exception.Throwable()) : default;
        }

        public static implicit operator Task<FlowResult<T, TException>>(FlowResult<T, TException> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<FlowResult<T, TException>>.Default;
        }

        public static implicit operator ValueTask<FlowResult<T, TException>>(FlowResult<T, TException> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<FlowResult<T, TException>>.Default;
        }

        public static Boolean operator ==(T? first, FlowResult<T, TException> second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, FlowResult<T, TException> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(FlowResult<T, TException> first, T? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(FlowResult<T, TException> first, T? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(FlowResult<T, TException> first, FlowResult<T, TException> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(FlowResult<T, TException> first, FlowResult<T, TException> second)
        {
            return !(first == second);
        }

        public static Boolean operator <(T? first, FlowResult<T, TException> second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, FlowResult<T, TException> second)
        {
            return second >= first;
        }

        public static Boolean operator >(T? first, FlowResult<T, TException> second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, FlowResult<T, TException> second)
        {
            return second <= first;
        }

        public static Boolean operator <(FlowResult<T, TException> first, T? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(FlowResult<T, TException> first, T? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(FlowResult<T, TException> first, T? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(FlowResult<T, TException> first, T? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(FlowResult<T, TException> first, FlowResult<T, TException> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(FlowResult<T, TException> first, FlowResult<T, TException> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(FlowResult<T, TException> first, FlowResult<T, TException> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(FlowResult<T, TException> first, FlowResult<T, TException> second)
        {
            return first.CompareTo(second) >= 0;
        }

        private readonly Maybe<T> _next;

        public Boolean HasNext
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _next.HasValue;
            }
        }

        public T Next
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _next.Internal;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? IFlowResult.Next
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Next;
            }
        }

        public Result<T, TException> Result { get; }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Result<T> IFlowResult<T>.Result
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Result;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Object? IFlowResult.Result
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Result.Unwrap(out T? result) ? result : null;
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _next.Unwrap(out T? result) ? result : Result.Value;
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

        public TException? Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Result.Exception;
            }
        }

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
                return _next.IsEmpty && Result.IsEmpty;
            }
        }

        public FlowResult(T next)
        {
            _next = next;
            Result = default;
        }

        public FlowResult(Maybe<T> next)
        {
            _next = next;
            Result = default;
        }

        public FlowResult(Result<T, TException> result)
        {
            _next = default;
            Result = result;
        }

        public FlowResult(TException exception)
        {
            _next = default;
            Result = exception;
        }

        internal FlowResult(Maybe<T> next, Result<T, TException> result)
        {
            _next = next;
            Result = result;
        }

        internal FlowResult(SerializationInfo info, StreamingContext context)
        {
            if (info.GetBoolean(nameof(HasNext)))
            {
                _next = new Maybe<T>(info, context);
                Result = default;
            }
            else
            {
                _next = default;
                Result = new Result<T, TException>(info, context);
            }
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FlowResult<T, TException> Create(T value)
        {
            return new FlowResult<T, TException>(value);
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FlowResult<T, TException> Create(TException exception)
        {
            return new FlowResult<T, TException>(exception.Throwable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Throw()
        {
            if (!HasNext)
            {
                Result.Throw();
            }
        }

        Boolean IMonad.Unwrap(out Object? value)
        {
            if (HasNext && _next.Unwrap(out T? result) || Result.Unwrap(out result))
            {
                value = result;
                return true;
            }

            value = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Unwrap([MaybeNullWhen(false)] out T value)
        {
            return HasNext ? _next.Unwrap(out value) : Result.Unwrap(out value);
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
            info.AddValue(nameof(HasNext), HasNext);

            if (HasNext)
            {
                _next.GetObjectData(info, context);
            }
            else
            {
                Result.GetObjectData(info, context);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return HasNext ? _next.CompareTo(other, comparer) : Result.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(FlowResult<T> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(FlowResult<T> other, IComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }

            if (HasNext && other.HasNext)
            {
                return comparer.SafeCompare(Next, other.Next) ?? 0;
            }

            if (!HasNext && !other.HasNext)
            {
                return comparer.SafeCompare(Result.Value, other.Result.Value) ?? 0;
            }

            Int32 comparison = comparer.SafeCompare(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value) ?? 0;

            if (comparison != 0)
            {
                return comparison;
            }

            return HasNext ? -1 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(FlowResult<T, TException> other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(FlowResult<T, TException> other, IComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }

            if (HasNext && other.HasNext)
            {
                return comparer.SafeCompare(Next, other.Next) ?? 0;
            }

            if (!HasNext && !other.HasNext)
            {
                return comparer.SafeCompare(Result.Value, other.Result.Value) ?? 0;
            }

            Int32 comparison = comparer.SafeCompare(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value) ?? 0;

            if (comparison != 0)
            {
                return comparison;
            }

            return HasNext ? -1 : 1;
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

            if (other is IFlowResult<T> convert)
            {
                return CompareTo(convert, comparer);
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }

            return comparer.SafeCompare(Value, other.Value) ?? 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IFlowResult<T>? other)
        {
            return CompareTo(other, null);
        }

        public Int32 CompareTo(IFlowResult<T>? other, IComparer<T>? comparer)
        {
            if (other is null)
            {
                return 1;
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Exception is not null ? other.Exception is not null ? 0 : 1 : -1;
            }

            if (HasNext && other.HasNext)
            {
                return comparer.SafeCompare(Next, other.Next) ?? 0;
            }

            if (!HasNext && !other.HasNext)
            {
                return comparer.SafeCompare(Result.Value, other.Result.Value) ?? 0;
            }

            Int32 comparison = comparer.SafeCompare(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value) ?? 0;

            if (comparison != 0)
            {
                return comparison;
            }

            return HasNext ? -1 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return HasNext ? _next.GetHashCode() : Result.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(T? other)
        {
            return HasNext ? _next.ReferenceEquals(other) : Result.ReferenceEquals(other);
        }

        public Boolean ReferenceEquals(FlowResult<T> other)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.ReferenceEquals(other.Next) : Result.ReferenceEquals(other.Result);
            }

            return ReferenceEquals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
        }

        public Boolean ReferenceEquals(FlowResult<T, TException> other)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.ReferenceEquals(other.Next) : Result.ReferenceEquals(other.Result);
            }

            return ReferenceEquals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
        }

        public Boolean ReferenceEquals(IResult<T>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (other is IFlowResult<T> convert)
            {
                return ReferenceEquals(convert);
            }

            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            return ReferenceEquals(Value, other.Value);
        }

        public Boolean ReferenceEquals(IFlowResult<T>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (Exception is not null || other.Exception is not null)
            {
                return ReferenceEquals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.ReferenceEquals(other.Next) : Result.ReferenceEquals(other.Result);
            }

            return ReferenceEquals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
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
                null => HasNext ? _next.Equals(other, comparer) : Result.Equals(other, comparer),
                T value => Equals(value, comparer),
                Exception value => Equals(value),
                FlowResult<T> value => Equals(value, comparer),
                FlowResult<T, TException> value => Equals(value, comparer),
                IFlowResult<T> value => Equals(value, comparer),
                IResult<T> value => Equals(value, comparer),
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
            return HasNext ? _next.Equals(other, comparer) : Result.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Exception? other)
        {
            return Result.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(FlowResult<T> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(FlowResult<T> other, IEqualityComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.Equals(other.Next, comparer) : Result.Equals(other.Result, comparer);
            }

            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(FlowResult<T, TException> other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(FlowResult<T, TException> other, IEqualityComparer<T>? comparer)
        {
            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.Equals(other.Next, comparer) : Result.Equals(other.Result, comparer);
            }

            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
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

            if (other is IFlowResult<T> convert)
            {
                return Equals(convert, comparer);
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }

            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IFlowResult<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IFlowResult<T>? other, IEqualityComparer<T>? comparer)
        {
            if (other is null)
            {
                return false;
            }

            if (Exception is not null || other.Exception is not null)
            {
                return Equals(Exception, other.Exception);
            }

            if (HasNext == other.HasNext)
            {
                return HasNext ? _next.Equals(other.Next, comparer) : Result.Equals(other.Result, comparer);
            }

            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(HasNext ? Next : Result.Value, other.HasNext ? other.Next : other.Result.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FlowResult<T, TException> Clone()
        {
            return new FlowResult<T, TException>(_next.Clone(), Result.Clone());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IFlowResult<T, TException> IFlowResult<T, TException>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IFlowResult<T, TException> ICloneable<IFlowResult<T, TException>>.Clone()
        {
            return Clone();
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
        IFlowResult<T> IFlowResult<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IFlowResult<T> ICloneable<IFlowResult<T>>.Clone()
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
        IFlowResult IFlowResult.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IFlowResult ICloneable<IFlowResult>.Clone()
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
            return HasNext ? _next.ToString() : Result.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format)
        {
            return HasNext ? _next.ToString(format) : Result.ToString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(IFormatProvider? provider)
        {
            return HasNext ? _next.ToString(provider) : Result.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return HasNext ? _next.ToString(format, provider) : Result.ToString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString()
        {
            return HasNext ? _next.GetString() : Result.GetString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape)
        {
            return HasNext ? _next.GetString(escape) : Result.GetString(escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format)
        {
            return HasNext ? _next.GetString(format) : Result.GetString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format)
        {
            return HasNext ? _next.GetString(escape, format) : Result.GetString(escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(IFormatProvider? provider)
        {
            return HasNext ? _next.GetString(provider) : Result.GetString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return HasNext ? _next.GetString(escape, provider) : Result.GetString(escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(String? format, IFormatProvider? provider)
        {
            return HasNext ? _next.GetString(format, provider) : Result.GetString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return HasNext ? _next.GetString(escape, format, provider) : Result.GetString(escape, format, provider);
        }
    }
}