// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;

namespace NetExtender.Types.Exceptions.Interfaces
{
    public interface IBusinessException<T> : IBusinessException
    {
        public T Code { get; }
        
        public new BusinessException<T> Exception
        {
            get
            {
                return this as BusinessException<T> ?? throw new InvalidOperationException($"Type '{GetType()}' must inherit '{typeof(BusinessException<T>)}'.");
            }
        }
    }

    public interface IBusinessException : IException
    {
        public Type? Type { get; }
        public String? Name { get; }
        public String? Description { get; }
        public HttpStatusCode? Status { get; }
        public BusinessException.BusinessInfo Info { get; }
        public BusinessException.BusinessInfo Business { get; }
        
        public new BusinessException Exception
        {
            get
            {
                return this as BusinessException ?? throw new InvalidOperationException($"Type '{GetType()}' must inherit '{typeof(BusinessException)}'.");
            }
        }
    }
}