// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Handlers;
using NetExtender.CQRS.Queries.Handlers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;

namespace NetExtender.CQRS.Queries.Handlers
{
    public abstract class QueryCQRSHandler<TQuery, TResult> : EntityCQRSHandler<TQuery, TResult>, IQueryCQRSHandler<TQuery, TResult> where TQuery : IQueryCQRS<TResult>
    {
        public override Task<TResult> HandleAsync(TQuery query)
        {
            return HandleAsync(query, CancellationToken.None);
        }
        
        public abstract override Task<TResult> HandleAsync(TQuery query, CancellationToken token);
    }
}