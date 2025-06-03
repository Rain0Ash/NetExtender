// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Enumerables.Interfaces;
using NetExtender.Types.Monads.Result;

namespace NetExtender.CQRS.Requests.Interfaces
{
    public interface IBusinessPaginationRequestCQRS<TResult> : IBusinessPaginationRequestCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IBusinessPaginationRequestCQRS<TResult, TCollection> : IBusinessRequestCQRS<TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
    }
    
    public interface IPaginationRequestCQRS<TResult> : IPaginationRequestCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IPaginationRequestCQRS<TResult, TCollection> : IRequestCQRS<TCollection> where TCollection : IPaginationEnumerable<TResult>
    {
    }
    
    public interface IBusinessRequestCQRS<TResult> : IRequestCQRS<BusinessResult<TResult>>
    {
    }
    
    public interface IBusinessRequestCQRS : IRequestCQRS<BusinessResult>
    {
    }
    
    public interface IRequestCQRS<TResult> : IRequestCQRS, IEntityCQRS<TResult>
    {
    }
    
    public interface IRequestCQRS : IEntityCQRS
    {
    }
}