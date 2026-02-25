// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Enumerables.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Queries.Interfaces
{
    public interface IBusinessPaginationIdQueryCQRS<T, TResult> : IBusinessPaginationQueryCQRS<TResult>, IBusinessPaginationIdQueryCQRS<T, TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IBusinessPaginationQueryCQRS<TResult> : IBusinessPaginationQueryCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IBusinessPaginationIdQueryCQRS<T, TResult, TCollection> : IBusinessPaginationQueryCQRS<TResult, TCollection>, IBusinessIdQueryCQRS<T, TCollection>, IIdPaginationCQRS<T> where TCollection : IPaginationEnumerable<TResult>
    {
    }

    public interface IBusinessPaginationQueryCQRS<TResult, TCollection> : IBusinessQueryCQRS<TCollection>, IPaginationCQRS where TCollection : IPaginationEnumerable<TResult>
    {
    }

    public interface IPaginationIdQueryCQRS<T, TResult> : IPaginationQueryCQRS<TResult>, IPaginationIdQueryCQRS<T, TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IPaginationQueryCQRS<TResult> : IPaginationQueryCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IPaginationIdQueryCQRS<T, TResult, TCollection> : IPaginationQueryCQRS<TResult, TCollection>, IIdQueryCQRS<T, TCollection>, IIdPaginationCQRS<T> where TCollection : IPaginationEnumerable<TResult>
    {
    }

    public interface IPaginationQueryCQRS<TResult, TCollection> : IQueryCQRS<TCollection>, IPaginationCQRS where TCollection : IPaginationEnumerable<TResult>
    {
    }

    public interface IBusinessIdQueryCQRS<T, TResult> : IBusinessQueryCQRS<TResult>, IIdQueryCQRS<T, BusinessResult<TResult>>
    {
    }

    public interface IBusinessValueQueryCQRS<T, TResult> : IBusinessQueryCQRS<TResult>, IValueQueryCQRS<T, BusinessResult<TResult>>
    {
    }

    public interface IBusinessQueryCQRS<TResult> : IQueryCQRS<BusinessResult<TResult>>
    {
    }

    public interface IIdQueryCQRS<T, TResult> : IQueryCQRS<TResult>, IIdEntityCQRS<T, TResult>
    {
    }

    public interface IValueQueryCQRS<T, TResult> : IQueryCQRS<TResult>, IValueEntityCQRS<T, TResult>
    {
    }

    public interface IQueryCQRS<TResult> : IEntityCQRS<TResult>
    {
    }
}