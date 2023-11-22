// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;

namespace NetExtender.CQRS.Querys.Dispatchers.Interfaces
{
    public interface IQueryCQRSDispatcher : IEntityCQRSDispatcher
    {
        public new Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQueryCQRS<TResult>;
        public new Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
    }
}