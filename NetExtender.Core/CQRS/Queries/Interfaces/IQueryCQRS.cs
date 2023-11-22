// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Enumerables.Interfaces;

namespace NetExtender.CQRS.Queries.Interfaces
{
    public interface IPaginationQueryCQRS<TResult> : IPaginationQueryCQRS<TResult, IPaginationEnumerable<TResult>>
    {
    }

    public interface IPaginationQueryCQRS<TResult, TCollection> : IQueryCQRS<TCollection> where TCollection : class, IPaginationEnumerable<TResult>
    {
    }

    public interface IQueryCQRS<TResult> : IEntityCQRS<TResult>
    {
    }
}