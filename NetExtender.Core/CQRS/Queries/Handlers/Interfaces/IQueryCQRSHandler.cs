// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;

namespace NetExtender.CQRS.Queries.Handlers.Interfaces
{
    public interface IQueryCQRSHandler<in TQuery, TResult> : IEntityCQRSHandler<TQuery, TResult> where TQuery : IQueryCQRS<TResult>
    {
        public new Task<TResult> HandleAsync(TQuery query);
        public new Task<TResult> HandleAsync(TQuery query, CancellationToken token);
    }
}