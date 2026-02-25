// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;

namespace NetExtender.Exceptions.Interfaces
{
    public interface IUnsafeBusinessException<T> : IBusinessException<T>, IUnsafeBusinessException
    {
        public new BusinessException<T>.BusinessInfo Info { get; }
        public new BusinessException<T>.BusinessInfo Business { get; }

        public new BusinessException<T> Exception
        {
            get
            {
                return this as BusinessException<T> ?? throw new InvalidOperationException($"Type '{GetType()}' must inherit '{typeof(BusinessException<T>)}'.");
            }
        }
    }

    public interface IBusinessException<out T> : IBusinessException
    {
        public T Code { get; }

        public new IBusinessExceptionInfo<T> Info { get; }
        public new IBusinessExceptionInfo<T> Business { get; }
    }

    public interface IUnsafeBusinessException : IBusinessException
    {
        public new BusinessException.BusinessInfo Info { get; }
        public new BusinessException.BusinessInfo Business { get; }

        public new BusinessException Exception
        {
            get
            {
                return this as BusinessException ?? throw new InvalidOperationException($"Type '{GetType()}' must inherit '{typeof(BusinessException)}'.");
            }
        }
    }

    public interface IBusinessException : ITraceException
    {
        public Type? Type { get; }
        public String? Identity { get; }
        public String? Description { get; }
        public HttpStatusCode? Status { get; }
        public IBusinessExceptionInfo Info { get; }
        public IBusinessExceptionInfo Business { get; }

        public new Exception Exception
        {
            get
            {
                return this as Exception ?? throw new InvalidOperationException($"Type '{GetType()}' must inherit '{typeof(Exception)}'.");
            }
        }
    }
}