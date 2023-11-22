// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS.Dispatchers
{
    public abstract class EntityCQRSDispatcher : IEntityCQRSDispatcher
    {
        public virtual Task DispatchAsync<TEntity>(TEntity entity) where TEntity : IEntityCQRS
        {
            return DispatchAsync(entity, CancellationToken.None);
        }
        
        public abstract Task DispatchAsync<TEntity>(TEntity entity, CancellationToken token) where TEntity : IEntityCQRS;

        public virtual Task<TResult> DispatchAsync<TEntity, TResult>(TEntity entity) where TEntity : IEntityCQRS<TResult>
        {
            return DispatchAsync<TEntity, TResult>(entity, CancellationToken.None);
        }
        
        public abstract Task<TResult> DispatchAsync<TEntity, TResult>(TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
    }
}