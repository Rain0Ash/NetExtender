// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net;
using NetExtender.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IBusinessResult<T, TBusiness> : IBusinessResult<T>, IResult<T, BusinessException<TBusiness>>, ICloneable<IBusinessResult<T, TBusiness>>
    {
        public new BusinessException<TBusiness>? Exception { get; }
        
        public TBusiness Code { get; }
        public new BusinessException<TBusiness>.BusinessInfo Info { get; }
        public new BusinessException<TBusiness>.BusinessInfo Business { get; }
        
        public new IBusinessResult<T, TBusiness> Clone();
    }
    
    public interface IBusinessResult<T> : IBusinessResult, IResult<T, BusinessException>, ICloneable<IBusinessResult<T>>
    {
        public new BusinessException? Exception { get; }
        
        public new IBusinessResult<T> Clone();
    }
    
    public interface IBusinessResult : IResult, ICloneable<IBusinessResult>
    {
        public new BusinessException? Exception { get; }
        
        public HttpStatusCode Status { get; }
        public BusinessException.BusinessInfo Info { get; }
        public BusinessException.BusinessInfo Business { get; }
        
        public new IBusinessResult Clone();
    }
    
    public interface IResult<T, out TException> : IResult<T>, ICloneable<IResult<T, TException>> where TException : Exception
    {
        public new TException? Exception { get; }
        
        public new IResult<T, TException> Clone();
    }
    
    public interface IResult<T> : IResult, IMonad<T>, IResultEquality<T, T>, IResultEquality<T, IResult<T>>, ICloneable<IResult<T>>
    {
        public new T Value { get; }
        
        public new IResult<T> Clone();
    }
    
    public interface IResult : IMonad, ICloneable<IResult>
    {
        public Object? Value { get; }
        public Exception? Exception { get; }

        public new IResult Clone();
    }
    
    public interface IResultEquality<out T, TResult> : IResultEquality<TResult>, IResultComparable<T, TResult>, IResultEquatable<T, TResult>, IMonadEquality<T, TResult>
    {
    }
    
    public interface IResultEquality<T> : IResultComparable<T>, IResultEquatable<T>, IMonadEquality<T>
    {
    }
    
    public interface IResultEquatable<out T, TResult> : IResultEquatable<TResult>, IMonadEquatable<T, TResult>
    {
    }
    
    public interface IResultEquatable<T> : IMonadEquatable<T>
    {
    }
    
    public interface IResultComparable<out T, in TResult> : IResultComparable<TResult>, IMonadComparable<T, TResult>
    {
    }
    
    public interface IResultComparable<in T> : IMonadComparable<T>
    {
    }
}