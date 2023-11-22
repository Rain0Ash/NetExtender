// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS.Handlers.Interfaces
{
    public interface IEntityCQRSHandler<in TEntity, TResult> : IEntityCQRSHandler<TEntity> where TEntity : IEntityCQRS<TResult>
    {
        public new Task<TResult> HandleAsync(TEntity entity);
        public new Task<TResult> HandleAsync(TEntity entity, CancellationToken token);
    }
    
    public interface IEntityCQRSHandler<in TEntity> where TEntity : IEntityCQRS
    {
        public Task HandleAsync(TEntity entity);
        public Task HandleAsync(TEntity entity, CancellationToken token);
    }
}