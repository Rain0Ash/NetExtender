// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS.Dispatchers.Interfaces
{
    public interface IEntityCQRSDispatcher
    {
        public Task DispatchAsync<TEntity>(TEntity entity) where TEntity : IEntityCQRS;
        public Task DispatchAsync<TEntity>(TEntity entity, CancellationToken token) where TEntity : IEntityCQRS;
        public Task<TResult> DispatchAsync<TEntity, TResult>(TEntity entity) where TEntity : IEntityCQRS<TResult>;
        public Task<TResult> DispatchAsync<TEntity, TResult>(TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
    }
}