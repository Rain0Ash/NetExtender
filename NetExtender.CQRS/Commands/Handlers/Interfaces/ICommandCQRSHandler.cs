// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Commands.Handlers.Interfaces
{
    public interface ICommandCQRSHandler<TContext, TCommand, TResult> : ICommandCQRSHandler<TContext, TCommand>, IEntityCQRSHandler<TContext, TCommand, TResult> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS<TResult>
    {
        public new BusinessAsync<TResult> Async(TContext context, TCommand command);
        public new BusinessAsync<TResult> Async(TContext context, TCommand command, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, in TCommand command);
        public new BusinessAsync<TResult> Async(TContext context, in TCommand command, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, TCommand command, ICQRS.Transaction transaction);
        public new BusinessAsync<TResult> Async(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, in TCommand command, ICQRS.Transaction transaction);
        public new BusinessAsync<TResult> Async(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, TCommand command);
        public new Async<TResult> SafeAsync(TContext context, TCommand command, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, in TCommand command);
        public new Async<TResult> SafeAsync(TContext context, in TCommand command, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, TCommand command, ICQRS.Transaction transaction);
        public new Async<TResult> SafeAsync(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, in TCommand command, ICQRS.Transaction transaction);
        public new Async<TResult> SafeAsync(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token);
    }

    public interface ICommandCQRSHandler<TContext, TCommand> : IEntityCQRSHandler<TContext, TCommand> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS
    {
        public new BusinessAsync Async(TContext context, TCommand command);
        public new BusinessAsync Async(TContext context, TCommand command, CancellationToken token);
        public new BusinessAsync Async(TContext context, in TCommand command);
        public new BusinessAsync Async(TContext context, in TCommand command, CancellationToken token);
        public new BusinessAsync Async(TContext context, TCommand command, ICQRS.Transaction transaction);
        public new BusinessAsync Async(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token);
        public new BusinessAsync Async(TContext context, in TCommand command, ICQRS.Transaction transaction);
        public new BusinessAsync Async(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token);
        public new Async SafeAsync(TContext context, TCommand command);
        public new Async SafeAsync(TContext context, TCommand command, CancellationToken token);
        public new Async SafeAsync(TContext context, in TCommand command);
        public new Async SafeAsync(TContext context, in TCommand command, CancellationToken token);
        public new Async SafeAsync(TContext context, TCommand command, ICQRS.Transaction transaction);
        public new Async SafeAsync(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token);
        public new Async SafeAsync(TContext context, in TCommand command, ICQRS.Transaction transaction);
        public new Async SafeAsync(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token);
    }
}