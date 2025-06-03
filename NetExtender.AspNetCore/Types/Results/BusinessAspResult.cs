// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetExtender.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Monads.Result;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Unit = System.Reactive.Unit;

#pragma warning disable CA1061

#if NET7_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http.HttpResults;
#endif

namespace NetExtender.AspNetCore.Types.Results
{
    public interface IAspResult<T> : IEquatable<T>, IEquatable<IAspResult<T>>, IBusinessResult, ICloneable<IAspResult<T>> where T : struct, IBusinessResult
    {
        public T Result { get; }
        public new IAspResult<T> Clone();
    }
    
    public class AspResult : Microsoft.AspNetCore.Http.IResult, IAspResult<BusinessResult>, ICloneable<AspResult>
    {
#if NET7_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
        public static implicit operator AspResult?(EmptyHttpResult? value)
        {
            return value is not null ? None : null;
        }
#endif
        
        public static implicit operator AspResult(BusinessException? value)
        {
            return new AspResult(value);
        }
        
        public static implicit operator AspResult(BusinessResult value)
        {
            return new AspResult(value);
        }
        
        public static implicit operator AspResult(BusinessResult<Unit> value)
        {
            return new AspResult(value);
        }
        
        public static implicit operator Boolean(AspResult value)
        {
            return value.Result;
        }
        
        public static implicit operator HttpStatusCode(AspResult value)
        {
            return value.Result;
        }
        
        public static implicit operator AspResult(HttpStatusCode value)
        {
            return new AspResult(value);
        }
        
        public static implicit operator AspResult((HttpStatusCode Status, String? Message) value)
        {
            return new AspResult(value);
        }
        
        public static implicit operator AspResult((HttpStatusCode Status, String? Message, String? Description) value)
        {
            return new AspResult(value);
        }
        
        public static implicit operator AspResult((HttpStatusCode Status, Exception? Exception) value)
        {
            return new AspResult(value);
        }
        
        public static implicit operator AspResult((HttpStatusCode Status, String? Message, Exception? Exception) value)
        {
            return new AspResult(value);
        }
        
        public static implicit operator AspResult((HttpStatusCode Status, String? Message, String? Description, Exception? Exception) value)
        {
            return new AspResult(value);
        }

        public static AspResult Default { get; } = new AspResult(HttpStatusCode.OK);
        public static AspResult None { get; } = new NoneAspResult();
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public BusinessResult Result { get; }

        Object NetExtender.Types.Monads.Interfaces.IResult.Value
        {
            get
            {
                return Result.Value;
            }
        }
        
        public Int32? StatusCode { get; init; }

        HttpStatusCode IBusinessResult.Status
        {
            get
            {
                return StatusCode.HasValue ? (HttpStatusCode) StatusCode.Value : default;
            }
        }

        public BusinessException.BusinessInfo Info
        {
            get
            {
                return Result.Info;
            }
        }

        public BusinessException.BusinessInfo Business
        {
            get
            {
                return Result.Info;
            }
        }

        public BusinessException? Exception
        {
            get
            {
                return Result.Exception;
            }
        }

        Exception? NetExtender.Types.Monads.Interfaces.IResult.Exception
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
                return Result.IsEmpty;
            }
        }

        protected AspResult()
        {
        }
        
        public AspResult(BusinessResult result)
        {
            Result = result;
            StatusCode = (Int32) Result.Status;
            _ = result.Value;
        }

        public virtual Task ExecuteAsync(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (StatusCode is { } code)
            {
                context.Response.StatusCode = code;
            }
            
            return Task.CompletedTask;
        }

        public override Int32 GetHashCode()
        {
            return Result.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Equals(default(BusinessResult)),
                BusinessResult result => Equals(result),
                IAspResult<BusinessResult> result => Equals(result),
                _ => Result.Equals(other)
            };
        }

        public Boolean Equals(BusinessResult other)
        {
            return Result.Equals(other);
        }

        public Boolean Equals(IAspResult<BusinessResult>? other)
        {
            return Equals(other?.Result ?? default);
        }

        public virtual AspResult Clone()
        {
            return new AspResult(Result.Clone());
        }

        IAspResult<BusinessResult> IAspResult<BusinessResult>.Clone()
        {
            return Clone();
        }

        IAspResult<BusinessResult> ICloneable<IAspResult<BusinessResult>>.Clone()
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

        NetExtender.Types.Monads.Interfaces.IResult NetExtender.Types.Monads.Interfaces.IResult.Clone()
        {
            return Clone();
        }

        NetExtender.Types.Monads.Interfaces.IResult ICloneable<NetExtender.Types.Monads.Interfaces.IResult>.Clone()
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
            return Result.ToString();
        }

        public String ToString(String? format)
        {
            return Result.ToString(format);
        }

        public String ToString(IFormatProvider? provider)
        {
            return Result.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Result.ToString(format, provider);
        }

        public String? GetString()
        {
            return Result.GetString();
        }

        public String? GetString(EscapeType escape)
        {
            return Result.GetString(escape);
        }

        public String? GetString(String? format)
        {
            return Result.GetString(format);
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return Result.GetString(escape, format);
        }

        public String? GetString(IFormatProvider? provider)
        {
            return Result.GetString(provider);
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return Result.GetString(escape, provider);
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return Result.GetString(format, provider);
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return Result.GetString(escape, format, provider);
        }

        private sealed class NoneAspResult : AspResult
        {
            public override Task ExecuteAsync(HttpContext context)
            {
                return Task.CompletedTask;
            }
        }
    }

    public class AspResult<T> : BusinessAspResult<BusinessResult<T>>, IBusinessResult<T>, ICloneable<AspResult<T>>
    {
        public static implicit operator AspResult<T>(T value)
        {
            return new AspResult<T>(value);
        }

        public static implicit operator AspResult<T>(BusinessException value)
        {
            return new AspResult<T>(value);
        }

        public static implicit operator AspResult<T>(BusinessResult<T> value)
        {
            return new AspResult<T>(value);
        }
        
        public static implicit operator Boolean(AspResult<T> value)
        {
            return value.Result;
        }
        
        public static implicit operator HttpStatusCode(AspResult<T> value)
        {
            return value.Result;
        }
        
        public static implicit operator AspResult<T>(HttpStatusCode value)
        {
            return new AspResult<T>(value);
        }
        
        public static implicit operator AspResult<T>((HttpStatusCode Status, String? Message) value)
        {
            return new AspResult<T>(value);
        }
        
        public static implicit operator AspResult<T>((HttpStatusCode Status, String? Message, String? Description) value)
        {
            return new AspResult<T>(value);
        }
        
        public static implicit operator AspResult<T>((HttpStatusCode Status, Exception? Exception) value)
        {
            return new AspResult<T>(value);
        }
        
        public static implicit operator AspResult<T>((HttpStatusCode Status, String? Message, Exception? Exception) value)
        {
            return new AspResult<T>(value);
        }
        
        public static implicit operator AspResult<T>((HttpStatusCode Status, String? Message, String? Description, Exception? Exception) value)
        {
            return new AspResult<T>(value);
        }

        public new T Value
        {
            get
            {
                return Result.Value;
            }
        }
        
        public AspResult(T result)
            : base(result)
        {
        }
        
        public AspResult(BusinessResult<T> result)
            : base(result)
        {
        }

        public Int32 CompareTo(T? other)
        {
            return Result.CompareTo(other);
        }

        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return Result.CompareTo(other, comparer);
        }

        public Int32 CompareTo(IResult<T>? other)
        {
            return Result.CompareTo(other);
        }

        public Int32 CompareTo(IResult<T>? other, IComparer<T>? comparer)
        {
            return Result.CompareTo(other, comparer);
        }

        public override Int32 GetHashCode()
        {
            return Result.GetHashCode();
        }

        public sealed override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public virtual Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                null => Equals(default(T), comparer),
                BusinessResult<T> result => Equals(result, comparer),
                Result<T> result => Equals(result, comparer),
                T result => Equals(result, comparer),
                IResult<T> result => Equals(result, comparer),
                _ => Result.Equals(other)
            };
        }

        public Boolean Equals(T? other)
        {
            return Result.Equals(other);
        }

        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            return Result.Equals(other, comparer);
        }

        public Boolean Equals(Result<T> other)
        {
            return Result.Equals(other);
        }

        public Boolean Equals(Result<T> other, IEqualityComparer<T>? comparer)
        {
            return Result.Equals(other, comparer);
        }

        public sealed override Boolean Equals(BusinessResult<T> other)
        {
            return Result.Equals(other);
        }

        public Boolean Equals(BusinessResult<T> other, IEqualityComparer<T>? comparer)
        {
            return Result.Equals(other, comparer);
        }

        public Boolean Equals(IResult<T>? other)
        {
            return Result.Equals(other);
        }

        public Boolean Equals(IResult<T>? other, IEqualityComparer<T>? comparer)
        {
            return Result.Equals(other, comparer);
        }

        public override AspResult<T> Clone()
        {
            return new AspResult<T>(Result.Clone());
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

        NetExtender.Types.Monads.Interfaces.IResult NetExtender.Types.Monads.Interfaces.IResult.Clone()
        {
            return Clone();
        }

        NetExtender.Types.Monads.Interfaces.IResult ICloneable<NetExtender.Types.Monads.Interfaces.IResult>.Clone()
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
    }

    public abstract class BusinessAspResult<T> : ObjectResult, IAspResult<T>, ICloneable<BusinessAspResult<T>> where T : struct, IBusinessResult, IEquatable<T>
    {
        public static implicit operator T(BusinessAspResult<T>? value)
        {
            return value?.Result ?? default;
        }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public T Result { get; }

        HttpStatusCode IBusinessResult.Status
        {
            get
            {
                return StatusCode.HasValue ? (HttpStatusCode) StatusCode.Value : default;
            }
        }

        public BusinessException.BusinessInfo Info
        {
            get
            {
                return Result.Info;
            }
        }

        public BusinessException.BusinessInfo Business
        {
            get
            {
                return Result.Info;
            }
        }

        public BusinessException? Exception
        {
            get
            {
                return Result.Exception;
            }
        }

        Exception? NetExtender.Types.Monads.Interfaces.IResult.Exception
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
                return Result.IsEmpty;
            }
        }

        protected BusinessAspResult(T result)
            : base(null)
        {
            Result = result;
            StatusCode = (Int32) Result.Status;
            Value = Result.Value;
        }

        public abstract override Int32 GetHashCode();
        public abstract override Boolean Equals(Object? other);
        public abstract Boolean Equals(T other);

        public virtual Boolean Equals(IAspResult<T>? other)
        {
            return Equals(other?.Result ?? default(T));
        }

        public abstract BusinessAspResult<T> Clone();

        IAspResult<T> IAspResult<T>.Clone()
        {
            return Clone();
        }

        IAspResult<T> ICloneable<IAspResult<T>>.Clone()
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

        NetExtender.Types.Monads.Interfaces.IResult NetExtender.Types.Monads.Interfaces.IResult.Clone()
        {
            return Clone();
        }

        NetExtender.Types.Monads.Interfaces.IResult ICloneable<NetExtender.Types.Monads.Interfaces.IResult>.Clone()
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
            return Result.ToString();
        }

        public String ToString(String? format)
        {
            return Result.ToString(format);
        }

        public String ToString(IFormatProvider? provider)
        {
            return Result.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Result.ToString(format, provider);
        }

        public String? GetString()
        {
            return Result.GetString();
        }

        public String? GetString(EscapeType escape)
        {
            return Result.GetString(escape);
        }

        public String? GetString(String? format)
        {
            return Result.GetString(format);
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return Result.GetString(escape, format);
        }

        public String? GetString(IFormatProvider? provider)
        {
            return Result.GetString(provider);
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return Result.GetString(escape, provider);
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return Result.GetString(format, provider);
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return Result.GetString(escape, format, provider);
        }
    }
}