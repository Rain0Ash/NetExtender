// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS.Handlers
{
    public abstract class EntityCQRSHandler<TEntity, TResult> : EntityCQRSHandler<TEntity>, IEntityCQRSHandler<TEntity, TResult> where TEntity : IEntityCQRS<TResult>
    {
        public override Task<TResult> HandleAsync(TEntity entity)
        {
            return HandleAsync(entity, CancellationToken.None);
        }

        public abstract override Task<TResult> HandleAsync(TEntity entity, CancellationToken token);
    }
    
    public abstract class EntityCQRSHandler<TEntity> : IEntityCQRSHandler<TEntity> where TEntity : IEntityCQRS
    {
        public virtual Task HandleAsync(TEntity entity)
        {
            return HandleAsync(entity, CancellationToken.None);
        }
        
        public abstract Task HandleAsync(TEntity entity, CancellationToken token);
    }
}