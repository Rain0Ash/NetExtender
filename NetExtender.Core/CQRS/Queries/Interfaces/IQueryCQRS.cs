// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Enumerables.Interfaces;
using NetExtender.Types.Monads.Result;

namespace NetExtender.CQRS.Queries.Interfaces
{
    public interface IBusinessPaginationQueryCQRS<TResult> : IBusinessPaginationQueryCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IBusinessPaginationQueryCQRS<TResult, TCollection> : IBusinessQueryCQRS<TCollection> where TCollection : class, IPaginationEnumerable<TResult>
    {
    }
    
    public interface IPaginationQueryCQRS<TResult> : IPaginationQueryCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IPaginationQueryCQRS<TResult, TCollection> : IQueryCQRS<TCollection> where TCollection : class, IPaginationEnumerable<TResult>
    {
    }
    
    public interface IBusinessQueryCQRS<TResult> : IQueryCQRS<BusinessResult<TResult>>
    {
    }

    public interface IQueryCQRS<TResult> : IEntityCQRS<TResult>
    {
    }
}