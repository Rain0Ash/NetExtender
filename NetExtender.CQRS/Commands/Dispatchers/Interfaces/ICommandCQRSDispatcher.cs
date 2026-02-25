// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Commands.Dispatchers.Interfaces
{
    public interface ICommandCQRSDispatcher<TContext> : IEntityCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        public new BusinessAsync Async<TCommand>(TContext context, TCommand command) where TCommand : ICommandCQRS;
        public new BusinessAsync Async<TCommand>(TContext context, TCommand command, CancellationToken token) where TCommand : ICommandCQRS;
        public new BusinessAsync Async<TCommand>(TContext context, in TCommand command) where TCommand : ICommandCQRS;
        public new BusinessAsync Async<TCommand>(TContext context, in TCommand command, CancellationToken token) where TCommand : ICommandCQRS;
        public new BusinessAsync Async<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS;
        public new BusinessAsync Async<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS;
        public new BusinessAsync Async<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS;
        public new BusinessAsync Async<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS;
        public new Async SafeAsync<TCommand>(TContext context, TCommand command) where TCommand : ICommandCQRS;
        public new Async SafeAsync<TCommand>(TContext context, TCommand command, CancellationToken token) where TCommand : ICommandCQRS;
        public new Async SafeAsync<TCommand>(TContext context, in TCommand command) where TCommand : ICommandCQRS;
        public new Async SafeAsync<TCommand>(TContext context, in TCommand command, CancellationToken token) where TCommand : ICommandCQRS;
        public new Async SafeAsync<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS;
        public new Async SafeAsync<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS;
        public new Async SafeAsync<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS;
        public new Async SafeAsync<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS;
        public new BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, TCommand command) where TCommand : ICommandCQRS<TResult>, ICommandCQRS;
        public new BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, in TCommand command) where TCommand : ICommandCQRS<TResult>, ICommandCQRS;
        public new BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, in TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS<TResult>, ICommandCQRS;
        public new BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS<TResult>, ICommandCQRS;
        public new BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
        public new Async<TResult> SafeAsync<TCommand, TResult>(TContext context, TCommand command) where TCommand : ICommandCQRS<TResult>;
        public new Async<TResult> SafeAsync<TCommand, TResult>(TContext context, TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
        public new Async<TResult> SafeAsync<TCommand, TResult>(TContext context, in TCommand command) where TCommand : ICommandCQRS<TResult>;
        public new Async<TResult> SafeAsync<TCommand, TResult>(TContext context, in TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
        public new Async<TResult> SafeAsync<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS<TResult>;
        public new Async<TResult> SafeAsync<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
        public new Async<TResult> SafeAsync<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS<TResult>;
        public new Async<TResult> SafeAsync<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
    }
}