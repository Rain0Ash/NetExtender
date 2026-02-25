// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NetExtender.Interfaces;
using NetExtender.Newtonsoft.Types.Monads.Results;
using NetExtender.Exceptions;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Network;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Unit = System.Reactive.Unit;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(BusinessResultJsonConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.BusinessResultJsonConverter))]
    public readonly struct BusinessResult : IEqualityStruct<BusinessResult>, IBusinessResult<Unit>, IResultEquality<Unit, Result<Unit>>, IResultEquality<Unit, Result<Unit, BusinessException>>, IResultEquality<BusinessResult>, IResultEquality<Unit, BusinessResult<Unit>>, ICloneable<BusinessResult>, ISerializable
    {
        public static implicit operator BusinessResult(Result<Unit, BusinessException> value)
        {
            return new BusinessResult(value);
        }

        public static implicit operator Result<Unit>(BusinessResult value)
        {
            return value.Internal;
        }

        public static implicit operator Task<Result<Unit>>(BusinessResult value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<Unit>>(value) : TaskUtilities<Result<Unit>>.Default;
        }

        public static implicit operator ValueTask<Result<Unit>>(BusinessResult value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<Unit>>(value) : ValueTaskUtilities<Result<Unit>>.Default;
        }

        public static implicit operator Result<Unit, BusinessException>(BusinessResult value)
        {
            return value.Internal;
        }

        public static implicit operator Task<Result<Unit, BusinessException>>(BusinessResult value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<Unit, BusinessException>>(value) : TaskUtilities<Result<Unit, BusinessException>>.Default;
        }

        public static implicit operator ValueTask<Result<Unit, BusinessException>>(BusinessResult value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<Unit, BusinessException>>(value) : ValueTaskUtilities<Result<Unit, BusinessException>>.Default;
        }

        public static implicit operator BusinessResult<Unit>(BusinessResult value)
        {
            return value.Internal;
        }

        public static implicit operator Task<BusinessResult<Unit>>(BusinessResult value)
        {
            return !value.IsEmpty ? Task.FromResult<BusinessResult<Unit>>(value) : TaskUtilities<BusinessResult<Unit>>.Default;
        }

        public static implicit operator ValueTask<BusinessResult<Unit>>(BusinessResult value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<BusinessResult<Unit>>(value) : ValueTaskUtilities<BusinessResult<Unit>>.Default;
        }

        public static implicit operator Boolean(BusinessResult value)
        {
            return value.Internal;
        }

        public static implicit operator HttpStatusCode(BusinessResult value)
        {
            return value.Status;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult(HttpStatusCode value)
        {
            if (!value.IsError())
            {
                return new BusinessResult<Unit> { Status = value };
            }

            BusinessException exception = value;
            return new BusinessResult<Unit>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult((HttpStatusCode Status, String? Message) value)
        {
            BusinessException exception = value;
            return new BusinessResult(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult((HttpStatusCode Status, String? Message, String? Description) value)
        {
            BusinessException exception = value;
            return new BusinessResult(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult((HttpStatusCode Status, Exception? Exception) value)
        {
            BusinessException exception = value;
            return new BusinessResult(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult((HttpStatusCode Status, String? Message, Exception? Exception) value)
        {
            BusinessException exception = value;
            return new BusinessResult(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult((HttpStatusCode Status, String? Message, String? Description, Exception? Exception) value)
        {
            BusinessException exception = value;
            return new BusinessResult(exception.Throwable());
        }

        public static implicit operator Unit(BusinessResult value)
        {
            return value.Internal;
        }

        public static implicit operator Maybe<Unit>(BusinessResult value)
        {
            return value.Internal.Unwrap(out Unit result) ? new Maybe<Unit>(result) : default;
        }

        public static implicit operator Task<Maybe<Unit>>(BusinessResult value)
        {
            return value.Internal.Unwrap(out Unit result) ? Task.FromResult<Maybe<Unit>>(result) : TaskUtilities<Maybe<Unit>>.Default;
        }

        public static implicit operator ValueTask<Maybe<Unit>>(BusinessResult value)
        {
            return value.Internal.Unwrap(out Unit result) ? ValueTask.FromResult<Maybe<Unit>>(result) : ValueTaskUtilities<Maybe<Unit>>.Default;
        }

        public static implicit operator BusinessResult(Unit value)
        {
            return new BusinessResult(value);
        }

        public static implicit operator BusinessException?(BusinessResult value)
        {
            return value.Internal;
        }

        public static implicit operator Task<Exception?>(BusinessResult value)
        {
            return value.Exception is not null ? Task.FromResult<Exception?>(value.Exception) : TaskUtilities<Exception?>.Default;
        }

        public static implicit operator ValueTask<Exception?>(BusinessResult value)
        {
            return value.Exception is not null ? ValueTask.FromResult<Exception?>(value.Exception) : ValueTaskUtilities<Exception?>.Default;
        }

        public static implicit operator Task<BusinessException?>(BusinessResult value)
        {
            return value.Exception is not null ? Task.FromResult<BusinessException?>(value.Exception) : TaskUtilities<BusinessException?>.Default;
        }

        public static implicit operator ValueTask<BusinessException?>(BusinessResult value)
        {
            return value.Exception is not null ? ValueTask.FromResult<BusinessException?>(value.Exception) : ValueTaskUtilities<BusinessException?>.Default;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult(BusinessException? value)
        {
            return value is not null ? new BusinessResult(value.Throwable()) : default;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult(ExceptionWrapper<BusinessException>? value)
        {
            return value is not null ? new BusinessResult(value.Exception.Throwable()) : default;
        }

        public static implicit operator Task<BusinessResult>(BusinessResult value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<BusinessResult>.Default;
        }

        public static implicit operator ValueTask<BusinessResult>(BusinessResult value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<BusinessResult>.Default;
        }

        public static Boolean operator ==(BusinessResult first, BusinessResult second)
        {
            return first.Internal == second.Internal;
        }

        public static Boolean operator !=(BusinessResult first, BusinessResult second)
        {
            return first.Internal != second.Internal;
        }

        public static BusinessResult OK
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return default;
            }
        }

        private readonly BusinessResult<Unit> Internal;

        public Object? Code
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Code;
            }
        }

        public HttpStatusCode Status
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Status;
            }
            init
            {
                Internal = new BusinessResult<Unit>(Internal) { Status = value };
            }
        }

        public Unit Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Value;
            }
        }

        Object IResult.Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Value;
            }
        }

        public BusinessException? Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Exception;
            }
        }

        Exception? IResult.Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception;
            }
        }

        public BusinessException.BusinessInfo Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Info;
            }
        }

        public BusinessException.BusinessInfo Business
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Business;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsEmpty;
            }
        }

        internal BusinessResult(Unit value)
        {
            Internal = new BusinessResult<Unit>(value);
        }

        public BusinessResult(BusinessException exception)
        {
            Internal = new BusinessResult<Unit>(exception);
        }

        internal BusinessResult(Unit value, BusinessException? exception)
        {
            Internal = new BusinessResult<Unit>(value, exception);
        }

        internal BusinessResult(Result<Unit, BusinessException> value)
        {
            Internal = value;
        }

        internal BusinessResult(BusinessResult<Unit> value)
        {
            Internal = value;
        }

        internal BusinessResult(SerializationInfo info, StreamingContext context)
        {
            Internal = new BusinessResult<Unit>(info, context);
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BusinessResult Create(Unit value)
        {
            return new BusinessResult(value);
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult Create(BusinessException exception)
        {
            return new BusinessResult(exception.Throwable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Throw()
        {
            Internal.Throw();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IMonad.Unwrap(out Object? value)
        {
            value = Exception;
            return !IsEmpty && value is null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IMonad<Unit>.Unwrap(out Unit value)
        {
            value = default;
            return !IsEmpty && Exception is null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IResult.Unwrap([MaybeNullWhen(false)] out Exception value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IResult<Unit, BusinessException>.Unwrap([MaybeNullWhen(false)] out BusinessException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IBusinessResult.Unwrap([MaybeNullWhen(false)] out BusinessException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IBusinessResult<Unit>.Unwrap([MaybeNullWhen(false)] out BusinessException value)
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
        public Int32 CompareTo(Unit other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Unit other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<Unit> other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<Unit> other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<Unit, BusinessException> other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<Unit, BusinessException> other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BusinessResult other)
        {
            return Internal.CompareTo(other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BusinessResult other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other.Internal, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BusinessResult<Unit> other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BusinessResult<Unit> other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IResult<Unit>? other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(IResult<Unit>? other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Unit other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Result<Unit> other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Result<Unit, BusinessException> other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(BusinessResult other)
        {
            return Internal.ReferenceEquals(other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(BusinessResult<Unit> other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IResult<Unit>? other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Object? other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Unit other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Unit other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Exception? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<Unit> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<Unit> other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<Unit, BusinessException> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<Unit, BusinessException> other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BusinessResult other)
        {
            return Internal.Equals(other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BusinessResult other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other.Internal, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BusinessResult<Unit> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BusinessResult<Unit> other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IResult<Unit>? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IResult<Unit>? other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BusinessResult Clone()
        {
            return new BusinessResult(Internal.Clone());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult<Unit> IBusinessResult<Unit>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult<Unit> ICloneable<IBusinessResult<Unit>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult IBusinessResult.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult ICloneable<IBusinessResult>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<Unit, BusinessException> IResult<Unit, BusinessException>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<Unit, BusinessException> ICloneable<IResult<Unit, BusinessException>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<Unit> IResult<Unit>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<Unit> ICloneable<IResult<Unit>>.Clone()
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
        IMonad<Unit> IMonad<Unit>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IMonad<Unit> ICloneable<IMonad<Unit>>.Clone()
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
    [JsonConverter(typeof(BusinessResultJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.BusinessResultJsonConverter<>))]
    public readonly struct BusinessResult<T> : IEqualityStruct<BusinessResult<T>>, IBusinessResult<T>, IResultEquality<T, Result<T>>, IResultEquality<T, Result<T, BusinessException>>, IResultEquality<T, BusinessResult<T>>, ICloneable<BusinessResult<T>>, ISerializable
    {
        public static implicit operator BusinessResult<T>(Result<T, BusinessException> value)
        {
            return new BusinessResult<T>(value);
        }

        public static implicit operator Result<T>(BusinessResult<T> value)
        {
            return new Result<T>(new Result<T, Exception>(value.Internal.Internal, value.Exception));
        }

        public static implicit operator Task<Result<T>>(BusinessResult<T> value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<T>>(value) : TaskUtilities<Result<T>>.Default;
        }

        public static implicit operator ValueTask<Result<T>>(BusinessResult<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<T>>(value) : ValueTaskUtilities<Result<T>>.Default;
        }

        public static implicit operator Result<T, BusinessException>(BusinessResult<T> value)
        {
            return new Result<T, BusinessException>(value.Internal.Internal, value.Exception);
        }

        public static implicit operator Task<Result<T, BusinessException>>(BusinessResult<T> value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<T, BusinessException>>(value) : TaskUtilities<Result<T, BusinessException>>.Default;
        }

        public static implicit operator ValueTask<Result<T, BusinessException>>(BusinessResult<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<T, BusinessException>>(value) : ValueTaskUtilities<Result<T, BusinessException>>.Default;
        }

        public static implicit operator BusinessResult(BusinessResult<T> value)
        {
            return new BusinessResult(Unit.Default, value.Exception);
        }

        public static implicit operator Task<BusinessResult>(BusinessResult<T> value)
        {
            return !value.IsEmpty ? Task.FromResult<BusinessResult>(value) : TaskUtilities<BusinessResult>.Default;
        }

        public static implicit operator ValueTask<BusinessResult>(BusinessResult<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<BusinessResult>(value) : ValueTaskUtilities<BusinessResult>.Default;
        }

        public static implicit operator Boolean(BusinessResult<T> value)
        {
            return value.Internal;
        }

        public static implicit operator HttpStatusCode(BusinessResult<T> value)
        {
            return value.Status;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T>(HttpStatusCode value)
        {
            BusinessException exception = value;
            return new BusinessResult<T>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, String? Message) value)
        {
            BusinessException exception = value;
            return new BusinessResult<T>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, String? Message, String? Description) value)
        {
            BusinessException exception = value;
            return new BusinessResult<T>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, Exception? Exception) value)
        {
            BusinessException exception = value;
            return new BusinessResult<T>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, String? Message, Exception? Exception) value)
        {
            BusinessException exception = value;
            return new BusinessResult<T>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, String? Message, String? Description, Exception? Exception) value)
        {
            BusinessException exception = value;
            return new BusinessResult<T>(exception.Throwable());
        }

        public static implicit operator T(BusinessResult<T> value)
        {
            return value.Internal;
        }

        public static implicit operator Maybe<T>(BusinessResult<T> value)
        {
            return value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator Task<Maybe<T>>(BusinessResult<T> value)
        {
            return value.Unwrap(out T? result) ? Task.FromResult<Maybe<T>>(result) : TaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator ValueTask<Maybe<T>>(BusinessResult<T> value)
        {
            return value.Unwrap(out T? result) ? ValueTask.FromResult<Maybe<T>>(result) : ValueTaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator BusinessResult<T>(T value)
        {
            return new BusinessResult<T>(value);
        }

        public static implicit operator BusinessException?(BusinessResult<T> value)
        {
            return value.Internal;
        }

        public static implicit operator Task<Exception?>(BusinessResult<T> value)
        {
            return value.Exception is not null ? Task.FromResult<Exception?>(value.Exception) : TaskUtilities<Exception?>.Default;
        }

        public static implicit operator ValueTask<Exception?>(BusinessResult<T> value)
        {
            return value.Exception is not null ? ValueTask.FromResult<Exception?>(value.Exception) : ValueTaskUtilities<Exception?>.Default;
        }

        public static implicit operator Task<BusinessException?>(BusinessResult<T> value)
        {
            return value.Exception is not null ? Task.FromResult<BusinessException?>(value.Exception) : TaskUtilities<BusinessException?>.Default;
        }

        public static implicit operator ValueTask<BusinessException?>(BusinessResult<T> value)
        {
            return value.Exception is not null ? ValueTask.FromResult<BusinessException?>(value.Exception) : ValueTaskUtilities<BusinessException?>.Default;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T>(BusinessException? value)
        {
            return value is not null ? new BusinessResult<T>(value.Throwable()) : default;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T>(ExceptionWrapper<BusinessException>? value)
        {
            return value is not null ? new BusinessResult<T>(value.Exception.Throwable()) : default;
        }

        public static implicit operator Task<BusinessResult<T>>(BusinessResult<T> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<BusinessResult<T>>.Default;
        }

        public static implicit operator ValueTask<BusinessResult<T>>(BusinessResult<T> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<BusinessResult<T>>.Default;
        }

        public static Boolean operator ==(T? first, BusinessResult<T> second)
        {
            return first == second.Internal;
        }

        public static Boolean operator !=(T? first, BusinessResult<T> second)
        {
            return first != second.Internal;
        }

        public static Boolean operator ==(BusinessResult<T> first, T? second)
        {
            return first.Internal == second;
        }

        public static Boolean operator !=(BusinessResult<T> first, T? second)
        {
            return first.Internal != second;
        }

        public static Boolean operator ==(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal == second.Internal;
        }

        public static Boolean operator !=(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal != second.Internal;
        }

        public static Boolean operator <(T? first, BusinessResult<T> second)
        {
            return first < second.Internal;
        }

        public static Boolean operator <=(T? first, BusinessResult<T> second)
        {
            return first <= second.Internal;
        }

        public static Boolean operator >(T? first, BusinessResult<T> second)
        {
            return first > second.Internal;
        }

        public static Boolean operator >=(T? first, BusinessResult<T> second)
        {
            return first >= second.Internal;
        }

        public static Boolean operator <(BusinessResult<T> first, T? second)
        {
            return first.Internal < second;
        }

        public static Boolean operator <=(BusinessResult<T> first, T? second)
        {
            return first.Internal <= second;
        }

        public static Boolean operator >(BusinessResult<T> first, T? second)
        {
            return first.Internal > second;
        }

        public static Boolean operator >=(BusinessResult<T> first, T? second)
        {
            return first.Internal >= second;
        }

        public static Boolean operator <(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal < second.Internal;
        }

        public static Boolean operator <=(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal <= second.Internal;
        }

        public static Boolean operator >(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal > second.Internal;
        }

        public static Boolean operator >=(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal >= second.Internal;
        }

        private readonly Result<T, BusinessException> Internal;

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Object? Code
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception is not null ? Exception.GetBusinessCode() : throw new InvalidOperationException();
            }
        }

        private readonly HttpStatusCode _status = default;
        public HttpStatusCode Status
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception?.Status ?? (_status is not default(HttpStatusCode) ? _status : HttpStatusCode.OK);
            }
            init
            {
                _status = value;
            }
        }

        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Value;
            }
        }

        Object? IResult.Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Value;
            }
        }

        public BusinessException? Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Exception;
            }
        }

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
        public BusinessException.BusinessInfo Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception is not null ? Exception.Info : throw new InvalidOperationException();
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BusinessException.BusinessInfo Business
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception is not null ? Exception.Business : throw new InvalidOperationException();
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

        public BusinessResult(T value)
        {
            Internal = new Result<T, BusinessException>(value);
        }

        public BusinessResult(BusinessException exception)
        {
            Internal = new Result<T, BusinessException>(exception);
        }

        internal BusinessResult(T value, BusinessException? exception)
        {
            Internal = new Result<T, BusinessException>(value, exception);
        }

        internal BusinessResult(Result<T, BusinessException> value)
        {
            Internal = value;
        }

        internal BusinessResult(BusinessResult<T> value)
        {
            Internal = value.Internal;
        }

        internal BusinessResult(SerializationInfo info, StreamingContext context)
        {
            Internal = new Result<T, BusinessException>(info, context);
            Status = (HttpStatusCode) info.GetInt32(nameof(Status));
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Create(T value)
        {
            return new BusinessResult<T>(value);
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T> Create(BusinessException exception)
        {
            return new BusinessResult<T>(exception.Throwable());
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
        Boolean IResult<T, BusinessException>.Unwrap([MaybeNullWhen(false)] out BusinessException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IBusinessResult.Unwrap([MaybeNullWhen(false)] out BusinessException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IBusinessResult<T>.Unwrap([MaybeNullWhen(false)] out BusinessException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Internal.GetObjectData(info, context);
            info.AddValue(nameof(Status), (Int32) Status);
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
        public Int32 CompareTo(Result<T, BusinessException> other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<T, BusinessException> other, IComparer<T>? comparer)
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
        public Boolean ReferenceEquals(Result<T, BusinessException> other)
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
        public Boolean Equals(Result<T, BusinessException> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<T, BusinessException> other, IEqualityComparer<T>? comparer)
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
        public BusinessResult<T> Clone()
        {
            return new BusinessResult<T>(Internal.Clone());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult<T> IBusinessResult<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult<T> ICloneable<IBusinessResult<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult IBusinessResult.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult ICloneable<IBusinessResult>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, BusinessException> IResult<T, BusinessException>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, BusinessException> ICloneable<IResult<T, BusinessException>>.Clone()
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
    [JsonConverter(typeof(BusinessResultJsonConverter<,>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Monads.BusinessResultJsonConverter<,>))]
    public readonly struct BusinessResult<T, TBusiness> : IEqualityStruct<BusinessResult<T, TBusiness>>, IBusinessResult<T, TBusiness>, IResultEquality<T, Result<T>>, IResultEquality<T, Result<T, BusinessException>>, IResultEquality<T, Result<T, BusinessException<TBusiness>>>, IResultEquality<T, BusinessResult<T>>, IResultEquality<T, BusinessResult<T, TBusiness>>, ICloneable<BusinessResult<T, TBusiness>>, ISerializable
    {
        public static implicit operator BusinessResult<T, TBusiness>(Result<T, BusinessException<TBusiness>> value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }

        public static implicit operator Result<T>(BusinessResult<T, TBusiness> value)
        {
            return new Result<T>(new Result<T, Exception>(value.Internal.Internal, value.Exception));
        }

        public static implicit operator Task<Result<T>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<T>>(value) : TaskUtilities<Result<T>>.Default;
        }

        public static implicit operator ValueTask<Result<T>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<T>>(value) : ValueTaskUtilities<Result<T>>.Default;
        }

        public static implicit operator Result<T, BusinessException>(BusinessResult<T, TBusiness> value)
        {
            return new Result<T, BusinessException>(value.Internal.Internal, value.Exception);
        }

        public static implicit operator Task<Result<T, BusinessException>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<T, BusinessException>>(value) : TaskUtilities<Result<T, BusinessException>>.Default;
        }

        public static implicit operator ValueTask<Result<T, BusinessException>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<T, BusinessException>>(value) : ValueTaskUtilities<Result<T, BusinessException>>.Default;
        }

        public static implicit operator Result<T, BusinessException<TBusiness>>(BusinessResult<T, TBusiness> value)
        {
            return new Result<T, BusinessException<TBusiness>>(value.Internal);
        }

        public static implicit operator Task<Result<T, BusinessException<TBusiness>>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? Task.FromResult<Result<T, BusinessException<TBusiness>>>(value) : TaskUtilities<Result<T, BusinessException<TBusiness>>>.Default;
        }

        public static implicit operator ValueTask<Result<T, BusinessException<TBusiness>>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<Result<T, BusinessException<TBusiness>>>(value) : ValueTaskUtilities<Result<T, BusinessException<TBusiness>>>.Default;
        }

        public static implicit operator BusinessResult(BusinessResult<T, TBusiness> value)
        {
            return new BusinessResult(Unit.Default, value.Exception);
        }

        public static implicit operator Task<BusinessResult>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? Task.FromResult<BusinessResult>(value) : TaskUtilities<BusinessResult>.Default;
        }

        public static implicit operator ValueTask<BusinessResult>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<BusinessResult>(value) : ValueTaskUtilities<BusinessResult>.Default;
        }

        public static implicit operator BusinessResult<T>(BusinessResult<T, TBusiness> value)
        {
            return new BusinessResult<T>(value.Internal.Internal, value.Exception);
        }

        public static implicit operator Task<BusinessResult<T>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? Task.FromResult<BusinessResult<T>>(value) : TaskUtilities<BusinessResult<T>>.Default;
        }

        public static implicit operator ValueTask<BusinessResult<T>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult<BusinessResult<T>>(value) : ValueTaskUtilities<BusinessResult<T>>.Default;
        }

        public static implicit operator Boolean(BusinessResult<T, TBusiness> value)
        {
            return value.Internal;
        }

        public static implicit operator HttpStatusCode(BusinessResult<T, TBusiness> value)
        {
            return value.Status;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code) value)
        {
            BusinessException<TBusiness> exception = value;
            return new BusinessResult<T, TBusiness>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, String? Message) value)
        {
            BusinessException<TBusiness> exception = value;
            return new BusinessResult<T, TBusiness>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, String? Message, String? Description) value)
        {
            BusinessException<TBusiness> exception = value;
            return new BusinessResult<T, TBusiness>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, Exception? Exception) value)
        {
            BusinessException<TBusiness> exception = value;
            return new BusinessResult<T, TBusiness>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, String? Message, Exception? Exception) value)
        {
            BusinessException<TBusiness> exception = value;
            return new BusinessResult<T, TBusiness>(exception.Throwable());
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, String? Message, String? Description, Exception? Exception) value)
        {
            BusinessException<TBusiness> exception = value;
            return new BusinessResult<T, TBusiness>(exception.Throwable());
        }

        public static implicit operator T(BusinessResult<T, TBusiness> value)
        {
            return value.Internal;
        }

        public static implicit operator Maybe<T>(BusinessResult<T, TBusiness> value)
        {
            return value.Unwrap(out T? result) ? new Maybe<T>(result) : default;
        }

        public static implicit operator Task<Maybe<T>>(BusinessResult<T, TBusiness> value)
        {
            return value.Unwrap(out T? result) ? Task.FromResult<Maybe<T>>(result) : TaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator ValueTask<Maybe<T>>(BusinessResult<T, TBusiness> value)
        {
            return value.Unwrap(out T? result) ? ValueTask.FromResult<Maybe<T>>(result) : ValueTaskUtilities<Maybe<T>>.Default;
        }

        public static implicit operator BusinessResult<T, TBusiness>(T value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }

        public static implicit operator BusinessException?(BusinessResult<T, TBusiness> value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessException<TBusiness>?(BusinessResult<T, TBusiness> value)
        {
            return value.Internal;
        }

        public static implicit operator Task<Exception?>(BusinessResult<T, TBusiness> value)
        {
            return value.Exception is not null ? Task.FromResult<Exception?>(value.Exception) : TaskUtilities<Exception?>.Default;
        }

        public static implicit operator ValueTask<Exception?>(BusinessResult<T, TBusiness> value)
        {
            return value.Exception is not null ? ValueTask.FromResult<Exception?>(value.Exception) : ValueTaskUtilities<Exception?>.Default;
        }

        public static implicit operator Task<BusinessException?>(BusinessResult<T, TBusiness> value)
        {
            return value.Exception is not null ? Task.FromResult<BusinessException?>(value.Exception) : TaskUtilities<BusinessException?>.Default;
        }

        public static implicit operator ValueTask<BusinessException?>(BusinessResult<T, TBusiness> value)
        {
            return value.Exception is not null ? ValueTask.FromResult<BusinessException?>(value.Exception) : ValueTaskUtilities<BusinessException?>.Default;
        }

        public static implicit operator Task<BusinessException<TBusiness>?>(BusinessResult<T, TBusiness> value)
        {
            return value.Exception is not null ? Task.FromResult<BusinessException<TBusiness>?>(value.Exception) : TaskUtilities<BusinessException<TBusiness>?>.Default;
        }

        public static implicit operator ValueTask<BusinessException<TBusiness>?>(BusinessResult<T, TBusiness> value)
        {
            return value.Exception is not null ? ValueTask.FromResult<BusinessException<TBusiness>?>(value.Exception) : ValueTaskUtilities<BusinessException<TBusiness>?>.Default;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T, TBusiness>(BusinessException<TBusiness>? value)
        {
            return value is not null ? new BusinessResult<T, TBusiness>(value.Throwable()) : default;
        }

        [StackTraceHidden]
        public static implicit operator BusinessResult<T, TBusiness>(ExceptionWrapper<BusinessException<TBusiness>>? value)
        {
            return value is not null ? new BusinessResult<T, TBusiness>(value.Exception.Throwable()) : default;
        }

        public static implicit operator Task<BusinessResult<T, TBusiness>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? Task.FromResult(value) : TaskUtilities<BusinessResult<T, TBusiness>>.Default;
        }

        public static implicit operator ValueTask<BusinessResult<T, TBusiness>>(BusinessResult<T, TBusiness> value)
        {
            return !value.IsEmpty ? ValueTask.FromResult(value) : ValueTaskUtilities<BusinessResult<T, TBusiness>>.Default;
        }

        public static Boolean operator ==(T? first, BusinessResult<T, TBusiness> second)
        {
            return first == second.Internal;
        }

        public static Boolean operator !=(T? first, BusinessResult<T, TBusiness> second)
        {
            return first != second.Internal;
        }

        public static Boolean operator ==(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal == second;
        }

        public static Boolean operator !=(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal != second;
        }

        public static Boolean operator ==(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal == second.Internal;
        }

        public static Boolean operator !=(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal != second.Internal;
        }

        public static Boolean operator <(T? first, BusinessResult<T, TBusiness> second)
        {
            return first < second.Internal;
        }

        public static Boolean operator <=(T? first, BusinessResult<T, TBusiness> second)
        {
            return first <= second.Internal;
        }

        public static Boolean operator >(T? first, BusinessResult<T, TBusiness> second)
        {
            return first > second.Internal;
        }

        public static Boolean operator >=(T? first, BusinessResult<T, TBusiness> second)
        {
            return first >= second.Internal;
        }

        public static Boolean operator <(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal < second;
        }

        public static Boolean operator <=(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal <= second;
        }

        public static Boolean operator >(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal > second;
        }

        public static Boolean operator >=(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal >= second;
        }

        public static Boolean operator <(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal < second.Internal;
        }

        public static Boolean operator <=(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal <= second.Internal;
        }

        public static Boolean operator >(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal > second.Internal;
        }

        public static Boolean operator >=(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal >= second.Internal;
        }

        private readonly Result<T, BusinessException<TBusiness>> Internal;

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public TBusiness Code
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception is not null ? Exception.Code : throw new InvalidOperationException();
            }
        }

        private readonly HttpStatusCode _status = default;
        public HttpStatusCode Status
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception?.Status ?? (_status is not default(HttpStatusCode) ? _status : HttpStatusCode.OK);
            }
            init
            {
                _status = value;
            }
        }

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

        public BusinessException<TBusiness>? Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Exception;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        BusinessException? IResult<T, BusinessException>.Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        BusinessException? IBusinessResult<T>.Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        BusinessException? IBusinessResult.Exception
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception;
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
        public BusinessException<TBusiness>.BusinessInfo Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception is not null ? Exception.Info : throw new InvalidOperationException();
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        BusinessException.BusinessInfo IBusinessResult.Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Info;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BusinessException<TBusiness>.BusinessInfo Business
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Exception is not null ? Exception.Business : throw new InvalidOperationException();
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        BusinessException.BusinessInfo IBusinessResult.Business
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Business;
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

        public BusinessResult(T value)
        {
            Internal = new Result<T, BusinessException<TBusiness>>(value);
        }

        public BusinessResult(BusinessException<TBusiness> exception)
        {
            Internal = new Result<T, BusinessException<TBusiness>>(exception);
        }

        internal BusinessResult(T value, BusinessException<TBusiness>? exception)
        {
            Internal = new Result<T, BusinessException<TBusiness>>(value, exception);
        }

        internal BusinessResult(Result<T, BusinessException<TBusiness>> value)
        {
            Internal = value;
        }

        internal BusinessResult(SerializationInfo info, StreamingContext context)
        {
            Internal = new Result<T, BusinessException<TBusiness>>(info, context);
            Status = (HttpStatusCode) info.GetInt32(nameof(Status));
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Create(T value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }

        [StackTraceHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessResult<T, TBusiness> Create(BusinessException<TBusiness> exception)
        {
            return new BusinessResult<T, TBusiness>(exception.Throwable());
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
        Boolean IResult<T, BusinessException>.Unwrap([MaybeNullWhen(false)] out BusinessException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IResult<T, BusinessException<TBusiness>>.Unwrap([MaybeNullWhen(false)] out BusinessException<TBusiness> value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IBusinessResult.Unwrap([MaybeNullWhen(false)] out BusinessException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IBusinessResult<T>.Unwrap([MaybeNullWhen(false)] out BusinessException value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IBusinessResult<T, TBusiness>.Unwrap([MaybeNullWhen(false)] out BusinessException<TBusiness> value)
        {
            value = Exception;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Internal.GetObjectData(info, context);
            info.AddValue(nameof(Status), (Int32) Status);
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
        public Int32 CompareTo(Result<T, BusinessException> other)
        {
            return Internal.CompareTo((Result<T>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<T, BusinessException> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo((Result<T>) other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<T, BusinessException<TBusiness>> other)
        {
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Result<T, BusinessException<TBusiness>> other, IComparer<T>? comparer)
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
        public Int32 CompareTo(BusinessResult<T, TBusiness> other)
        {
            return Internal.CompareTo((BusinessResult<T>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BusinessResult<T, TBusiness> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo((BusinessResult<T>) other, comparer);
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
        public Boolean ReferenceEquals(Result<T, BusinessException> other)
        {
            return Internal.ReferenceEquals((Result<T>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(Result<T, BusinessException<TBusiness>> other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(BusinessResult<T> other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(BusinessResult<T, TBusiness> other)
        {
            return Internal.ReferenceEquals((BusinessResult<T>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean ReferenceEquals(IResult<T>? other)
        {
            return Internal.ReferenceEquals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals(Object? other)
        {
            return other is BusinessResult<T, TBusiness> result ? Equals(result) : Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other is BusinessResult<T, TBusiness> result ? Equals(result, comparer) : Internal.Equals(other, comparer);
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
        public Boolean Equals(Result<T, BusinessException> other)
        {
            return Internal.Equals((Result<T>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<T, BusinessException> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals((Result<T>) other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<T, BusinessException<TBusiness>> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Result<T, BusinessException<TBusiness>> other, IEqualityComparer<T>? comparer)
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
        public Boolean Equals(BusinessResult<T, TBusiness> other)
        {
            return Internal.Equals((BusinessResult<T>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BusinessResult<T, TBusiness> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals((BusinessResult<T>) other, comparer);
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
        public BusinessResult<T, TBusiness> Clone()
        {
            return new BusinessResult<T, TBusiness>(Internal.Clone());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult<T, TBusiness> IBusinessResult<T, TBusiness>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult<T, TBusiness> ICloneable<IBusinessResult<T, TBusiness>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult<T> IBusinessResult<T>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult<T> ICloneable<IBusinessResult<T>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult IBusinessResult.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IBusinessResult ICloneable<IBusinessResult>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, BusinessException<TBusiness>> IResult<T, BusinessException<TBusiness>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, BusinessException<TBusiness>> ICloneable<IResult<T, BusinessException<TBusiness>>>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, BusinessException> IResult<T, BusinessException>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IResult<T, BusinessException> ICloneable<IResult<T, BusinessException>>.Clone()
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
}