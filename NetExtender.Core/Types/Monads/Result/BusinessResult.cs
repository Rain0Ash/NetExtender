// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Network;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Unit = System.Reactive.Unit;

namespace NetExtender.Types.Monads.Result
{
    [Serializable]
    public readonly struct BusinessResult : IEqualityStruct<BusinessResult>, IBusinessResult<Unit>, IResultEquality<Unit, Result<Unit>>, IResultEquality<Unit, Result<Unit, BusinessException>>, IResultEquality<BusinessResult>, IResultEquality<Unit, BusinessResult<Unit>>, ICloneable<BusinessResult>, ISerializable
    {
        public static implicit operator Result<Unit>(BusinessResult value)
        {
            return value.Internal;
        }
        
        public static implicit operator Result<Unit, BusinessException>(BusinessResult value)
        {
            return value.Internal;
        }
        
        public static implicit operator BusinessResult<Unit>(BusinessResult value)
        {
            return value.Internal;
        }
        
        public static implicit operator Boolean(BusinessResult value)
        {
            return value.Internal;
        }
        
        public static implicit operator HttpStatusCode(BusinessResult value)
        {
            return value.Status;
        }
        
        public static implicit operator BusinessResult(HttpStatusCode value)
        {
            return value.IsError() ? new BusinessResult<Unit>((BusinessException) value) : new BusinessResult<Unit> { Status = value };
        }

        public static implicit operator BusinessResult((HttpStatusCode Status, String? Message) value)
        {
            return new BusinessResult((BusinessException) value);
        }
        
        public static implicit operator BusinessResult((HttpStatusCode Status, String? Message, String? Description) value)
        {
            return new BusinessResult((BusinessException) value);
        }
        
        public static implicit operator BusinessResult((HttpStatusCode Status, Exception? Exception) value)
        {
            return new BusinessResult((BusinessException) value);
        }
        
        public static implicit operator BusinessResult((HttpStatusCode Status, String? Message, Exception? Exception) value)
        {
            return new BusinessResult((BusinessException) value);
        }
        
        public static implicit operator BusinessResult((HttpStatusCode Status, String? Message, String? Description, Exception? Exception) value)
        {
            return new BusinessResult((BusinessException) value);
        }

        public static implicit operator Unit(BusinessResult value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessResult(Unit value)
        {
            return new BusinessResult(value);
        }

        public static implicit operator BusinessException?(BusinessResult value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessResult(BusinessException? value)
        {
            return value is not null ? new BusinessResult(value) : default;
        }

        public static implicit operator BusinessResult(ExceptionWrapper<BusinessException>? value)
        {
            return value is not null ? new BusinessResult(value.Exception) : default;
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
            get
            {
                return default;
            }
        }

        private readonly BusinessResult<Unit> Internal;

        public Object? Code
        {
            get
            {
                return Internal.Code;
            }
        }

        public HttpStatusCode Status
        {
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
            get
            {
                return Internal.Value;
            }
        }

        Object IResult.Value
        {
            get
            {
                return Value;
            }
        }

        public BusinessException? Exception
        {
            get
            {
                return Internal.Exception;
            }
        }

        Exception? IResult.Exception
        {
            get
            {
                return Exception;
            }
        }

        public BusinessException.BusinessInfo Info
        {
            get
            {
                return Internal.Info;
            }
        }

        public BusinessException.BusinessInfo Business
        {
            get
            {
                return Internal.Business;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Internal.IsEmpty;
            }
        }
        
        public BusinessResult()
        {
            Internal = new BusinessResult<Unit>();
        }
        
        public BusinessResult(Unit value)
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

        internal BusinessResult(BusinessResult<Unit> value)
        {
            Internal = value;
        }

        internal BusinessResult(SerializationInfo info, StreamingContext context)
        {
            Internal = new BusinessResult<Unit>(info, context);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Internal.GetObjectData(info, context);
        }

        public Int32 CompareTo(Unit other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(Unit other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(Result<Unit> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(Result<Unit> other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(Result<Unit, BusinessException> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(Result<Unit, BusinessException> other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(BusinessResult other)
        {
            return Internal.CompareTo(other.Internal);
        }

        public Int32 CompareTo(BusinessResult other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other.Internal, comparer);
        }

        public Int32 CompareTo(BusinessResult<Unit> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(BusinessResult<Unit> other, IComparer<Unit>? comparer)
        {
            return Internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(IResult<Unit>? other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(IResult<Unit>? other, IComparer<Unit>? comparer)
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

        public Boolean Equals(Object? other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(Unit other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Unit other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(Exception? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Result<Unit> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Result<Unit> other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(Result<Unit, BusinessException> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Result<Unit, BusinessException> other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(BusinessResult other)
        {
            return Internal.Equals(other.Internal);
        }

        public Boolean Equals(BusinessResult other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other.Internal, comparer);
        }

        public Boolean Equals(BusinessResult<Unit> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(BusinessResult<Unit> other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public Boolean Equals(IResult<Unit>? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(IResult<Unit>? other, IEqualityComparer<Unit>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public BusinessResult Clone()
        {
            return new BusinessResult(Internal.Clone());
        }

        IBusinessResult<Unit> IBusinessResult<Unit>.Clone()
        {
            return Clone();
        }

        IBusinessResult<Unit> ICloneable<IBusinessResult<Unit>>.Clone()
        {
            return Clone();
        }

        IBusinessResult IBusinessResult.Clone()
        {
            return Clone();
        }

        IBusinessResult ICloneable<IBusinessResult>.Clone()
        {
            return Clone();
        }

        IResult<Unit, BusinessException> IResult<Unit, BusinessException>.Clone()
        {
            return Clone();
        }

        IResult<Unit, BusinessException> ICloneable<IResult<Unit, BusinessException>>.Clone()
        {
            return Clone();
        }

        IResult<Unit> IResult<Unit>.Clone()
        {
            return Clone();
        }

        IResult<Unit> ICloneable<IResult<Unit>>.Clone()
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

        IMonad<Unit> IMonad<Unit>.Clone()
        {
            return Clone();
        }

        IMonad<Unit> ICloneable<IMonad<Unit>>.Clone()
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
    public readonly struct BusinessResult<T> : IEqualityStruct<BusinessResult<T>>, IBusinessResult<T>, IResultEquality<T, Result<T>>, IResultEquality<T, Result<T, BusinessException>>, IResultEquality<T, BusinessResult<T>>, ICloneable<BusinessResult<T>>, ISerializable
    {
        public static implicit operator Result<T>(BusinessResult<T> value)
        {
            return new Result<T>(new Result<T, Exception>(value.Internal.Internal, value.Exception));
        }
        
        public static implicit operator Result<T, BusinessException>(BusinessResult<T> value)
        {
            return new Result<T, BusinessException>(value.Internal.Internal, value.Exception);
        }

        public static implicit operator BusinessResult(BusinessResult<T> value)
        {
            return new BusinessResult(Unit.Default, value.Exception);
        }
        
        public static implicit operator Boolean(BusinessResult<T> value)
        {
            return value.Internal;
        }
        
        public static implicit operator HttpStatusCode(BusinessResult<T> value)
        {
            return value.Status;
        }
        
        public static implicit operator BusinessResult<T>(HttpStatusCode value)
        {
            return new BusinessResult<T>((BusinessException) value);
        }
        
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, String? Message) value)
        {
            return new BusinessResult<T>((BusinessException) value);
        }
        
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, String? Message, String? Description) value)
        {
            return new BusinessResult<T>((BusinessException) value);
        }
        
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, Exception? Exception) value)
        {
            return new BusinessResult<T>((BusinessException) value);
        }
        
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, String? Message, Exception? Exception) value)
        {
            return new BusinessResult<T>((BusinessException) value);
        }
        
        public static implicit operator BusinessResult<T>((HttpStatusCode Status, String? Message, String? Description, Exception? Exception) value)
        {
            return new BusinessResult<T>((BusinessException) value);
        }

        public static implicit operator T(BusinessResult<T> value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessResult<T>(T value)
        {
            return new BusinessResult<T>(value);
        }

        public static implicit operator BusinessException?(BusinessResult<T> value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessResult<T>(BusinessException? value)
        {
            return value is not null ? new BusinessResult<T>(value) : default;
        }

        public static implicit operator BusinessResult<T>(ExceptionWrapper<BusinessException>? value)
        {
            return value is not null ? new BusinessResult<T>(value.Exception) : default;
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

        public static Boolean operator >(T? first, BusinessResult<T> second)
        {
            return first > second.Internal;
        }

        public static Boolean operator >=(T? first, BusinessResult<T> second)
        {
            return first >= second.Internal;
        }

        public static Boolean operator <(T? first, BusinessResult<T> second)
        {
            return first < second.Internal;
        }

        public static Boolean operator <=(T? first, BusinessResult<T> second)
        {
            return first <= second.Internal;
        }

        public static Boolean operator >(BusinessResult<T> first, T? second)
        {
            return first.Internal > second;
        }

        public static Boolean operator >=(BusinessResult<T> first, T? second)
        {
            return first.Internal >= second;
        }

        public static Boolean operator <(BusinessResult<T> first, T? second)
        {
            return first.Internal < second;
        }

        public static Boolean operator <=(BusinessResult<T> first, T? second)
        {
            return first.Internal <= second;
        }

        public static Boolean operator >(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal > second.Internal;
        }

        public static Boolean operator >=(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal >= second.Internal;
        }

        public static Boolean operator <(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal < second.Internal;
        }

        public static Boolean operator <=(BusinessResult<T> first, BusinessResult<T> second)
        {
            return first.Internal <= second.Internal;
        }

        private readonly Result<T, BusinessException> Internal;

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Object? Code
        {
            get
            {
                return Exception is not null ? Exception.GetBusinessCode() : throw new InvalidOperationException();
            }
        }

        private readonly HttpStatusCode _status = default;
        public HttpStatusCode Status
        {
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

        public BusinessException? Exception
        {
            get
            {
                return Internal.Exception;
            }
        }

        Exception? IResult.Exception
        {
            get
            {
                return Exception;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BusinessException.BusinessInfo Info
        {
            get
            {
                return Exception is not null ? Exception.Info : throw new InvalidOperationException();
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BusinessException.BusinessInfo Business
        {
            get
            {
                return Exception is not null ? Exception.Business : throw new InvalidOperationException();
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Internal.GetObjectData(info, context);
            info.AddValue(nameof(Status), (Int32) Status);
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

        public Int32 CompareTo(Result<T, BusinessException> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(Result<T, BusinessException> other, IComparer<T>? comparer)
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

        public Boolean Equals(Result<T, BusinessException> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Result<T, BusinessException> other, IEqualityComparer<T>? comparer)
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

        public BusinessResult<T> Clone()
        {
            return new BusinessResult<T>(Internal.Clone());
        }

        IBusinessResult<T> IBusinessResult<T>.Clone()
        {
            return Clone();
        }

        IBusinessResult<T> ICloneable<IBusinessResult<T>>.Clone()
        {
            return Clone();
        }

        IBusinessResult IBusinessResult.Clone()
        {
            return Clone();
        }

        IBusinessResult ICloneable<IBusinessResult>.Clone()
        {
            return Clone();
        }

        IResult<T, BusinessException> IResult<T, BusinessException>.Clone()
        {
            return Clone();
        }

        IResult<T, BusinessException> ICloneable<IResult<T, BusinessException>>.Clone()
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
    public readonly struct BusinessResult<T, TBusiness> : IEqualityStruct<BusinessResult<T, TBusiness>>, IBusinessResult<T, TBusiness>, IResultEquality<T, Result<T>>, IResultEquality<T, Result<T, BusinessException>>, IResultEquality<T, Result<T, BusinessException<TBusiness>>>, IResultEquality<T, BusinessResult<T>>, IResultEquality<T, BusinessResult<T, TBusiness>>, ICloneable<BusinessResult<T, TBusiness>>, ISerializable
    {
        public static implicit operator Result<T>(BusinessResult<T, TBusiness> value)
        {
            return new Result<T>(new Result<T, Exception>(value.Internal.Internal, value.Exception));
        }
        
        public static implicit operator Result<T, BusinessException>(BusinessResult<T, TBusiness> value)
        {
            return new Result<T, BusinessException>(value.Internal.Internal, value.Exception);
        }
        
        public static implicit operator Result<T, BusinessException<TBusiness>>(BusinessResult<T, TBusiness> value)
        {
            return new Result<T, BusinessException<TBusiness>>(value.Internal);
        }

        public static implicit operator BusinessResult(BusinessResult<T, TBusiness> value)
        {
            return new BusinessResult(Unit.Default, value.Exception);
        }
        
        public static implicit operator BusinessResult<T>(BusinessResult<T, TBusiness> value)
        {
            return new BusinessResult<T>(value.Internal.Internal, value.Exception);
        }
        
        public static implicit operator Boolean(BusinessResult<T, TBusiness> value)
        {
            return value.Internal;
        }
        
        public static implicit operator HttpStatusCode(BusinessResult<T, TBusiness> value)
        {
            return value.Status;
        }
        
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code) value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }
        
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, String? Message) value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }
        
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, String? Message, String? Description) value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }
        
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, Exception? Exception) value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }
        
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, String? Message, Exception? Exception) value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }
        
        public static implicit operator BusinessResult<T, TBusiness>((HttpStatusCode Status, TBusiness Code, String? Message, String? Description, Exception? Exception) value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }

        public static implicit operator T(BusinessResult<T, TBusiness> value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessResult<T, TBusiness>(T value)
        {
            return new BusinessResult<T, TBusiness>(value);
        }

        public static implicit operator BusinessException<TBusiness>?(BusinessResult<T, TBusiness> value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessResult<T, TBusiness>(BusinessException<TBusiness>? value)
        {
            return value is not null ? new BusinessResult<T, TBusiness>(value) : default;
        }

        public static implicit operator BusinessResult<T, TBusiness>(ExceptionWrapper<BusinessException<TBusiness>>? value)
        {
            return value is not null ? new BusinessResult<T, TBusiness>(value.Exception) : default;
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

        public static Boolean operator >(T? first, BusinessResult<T, TBusiness> second)
        {
            return first > second.Internal;
        }

        public static Boolean operator >=(T? first, BusinessResult<T, TBusiness> second)
        {
            return first >= second.Internal;
        }

        public static Boolean operator <(T? first, BusinessResult<T, TBusiness> second)
        {
            return first < second.Internal;
        }

        public static Boolean operator <=(T? first, BusinessResult<T, TBusiness> second)
        {
            return first <= second.Internal;
        }

        public static Boolean operator >(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal > second;
        }

        public static Boolean operator >=(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal >= second;
        }

        public static Boolean operator <(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal < second;
        }

        public static Boolean operator <=(BusinessResult<T, TBusiness> first, T? second)
        {
            return first.Internal <= second;
        }

        public static Boolean operator >(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal > second.Internal;
        }

        public static Boolean operator >=(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal >= second.Internal;
        }

        public static Boolean operator <(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal < second.Internal;
        }

        public static Boolean operator <=(BusinessResult<T, TBusiness> first, BusinessResult<T, TBusiness> second)
        {
            return first.Internal <= second.Internal;
        }

        private readonly Result<T, BusinessException<TBusiness>> Internal;

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public TBusiness Code
        {
            get
            {
                return Exception is not null ? Exception.Code : throw new InvalidOperationException();
            }
        }

        private readonly HttpStatusCode _status = default;
        public HttpStatusCode Status
        {
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

        public BusinessException<TBusiness>? Exception
        {
            get
            {
                return Internal.Exception;
            }
        }

        BusinessException? IResult<T, BusinessException>.Exception
        {
            get
            {
                return Exception;
            }
        }

        BusinessException? IBusinessResult<T>.Exception
        {
            get
            {
                return Exception;
            }
        }

        BusinessException? IBusinessResult.Exception
        {
            get
            {
                return Exception;
            }
        }

        Exception? IResult.Exception
        {
            get
            {
                return Exception;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BusinessException<TBusiness>.BusinessInfo Info
        {
            get
            {
                return Exception is not null ? Exception.Info : throw new InvalidOperationException();
            }
        }

        BusinessException.BusinessInfo IBusinessResult.Info
        {
            get
            {
                return Info;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BusinessException<TBusiness>.BusinessInfo Business
        {
            get
            {
                return Exception is not null ? Exception.Business : throw new InvalidOperationException();
            }
        }

        BusinessException.BusinessInfo IBusinessResult.Business
        {
            get
            {
                return Business;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Internal.GetObjectData(info, context);
            info.AddValue(nameof(Status), (Int32) Status);
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

        public Int32 CompareTo(Result<T, BusinessException> other)
        {
            return Internal.CompareTo((Result<T>) other);
        }

        public Int32 CompareTo(Result<T, BusinessException> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo((Result<T>) other, comparer);
        }

        public Int32 CompareTo(Result<T, BusinessException<TBusiness>> other)
        {
            return Internal.CompareTo(other);
        }

        public Int32 CompareTo(Result<T, BusinessException<TBusiness>> other, IComparer<T>? comparer)
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

        public Int32 CompareTo(BusinessResult<T, TBusiness> other)
        {
            return Internal.CompareTo((BusinessResult<T>) other);
        }

        public Int32 CompareTo(BusinessResult<T, TBusiness> other, IComparer<T>? comparer)
        {
            return Internal.CompareTo((BusinessResult<T>) other, comparer);
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
            return other is BusinessResult<T, TBusiness> result ? Equals(result) : Internal.Equals(other);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other is BusinessResult<T, TBusiness> result ? Equals(result, comparer) : Internal.Equals(other, comparer);
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

        public Boolean Equals(Result<T, BusinessException> other)
        {
            return Internal.Equals((Result<T>) other);
        }

        public Boolean Equals(Result<T, BusinessException> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals((Result<T>) other, comparer);
        }

        public Boolean Equals(Result<T, BusinessException<TBusiness>> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(Result<T, BusinessException<TBusiness>> other, IEqualityComparer<T>? comparer)
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

        public Boolean Equals(BusinessResult<T, TBusiness> other)
        {
            return Internal.Equals((BusinessResult<T>) other);
        }

        public Boolean Equals(BusinessResult<T, TBusiness> other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals((BusinessResult<T>) other, comparer);
        }

        public Boolean Equals(IResult<T>? other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(IResult<T>? other, IEqualityComparer<T>? comparer)
        {
            return Internal.Equals(other, comparer);
        }

        public BusinessResult<T, TBusiness> Clone()
        {
            return new BusinessResult<T, TBusiness>(Internal.Clone());
        }

        IBusinessResult<T, TBusiness> IBusinessResult<T, TBusiness>.Clone()
        {
            return Clone();
        }

        IBusinessResult<T, TBusiness> ICloneable<IBusinessResult<T, TBusiness>>.Clone()
        {
            return Clone();
        }

        IBusinessResult<T> IBusinessResult<T>.Clone()
        {
            return Clone();
        }

        IBusinessResult<T> ICloneable<IBusinessResult<T>>.Clone()
        {
            return Clone();
        }

        IBusinessResult IBusinessResult.Clone()
        {
            return Clone();
        }

        IBusinessResult ICloneable<IBusinessResult>.Clone()
        {
            return Clone();
        }

        IResult<T, BusinessException<TBusiness>> IResult<T, BusinessException<TBusiness>>.Clone()
        {
            return Clone();
        }

        IResult<T, BusinessException<TBusiness>> ICloneable<IResult<T, BusinessException<TBusiness>>>.Clone()
        {
            return Clone();
        }

        IResult<T, BusinessException> IResult<T, BusinessException>.Clone()
        {
            return Clone();
        }

        IResult<T, BusinessException> ICloneable<IResult<T, BusinessException>>.Clone()
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
}