// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Commands.Handlers.Interfaces;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.CQRS.Handlers;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Commands.Handlers
{
    public abstract class CommandCQRSHandler<TContext, TCommand, TResult> : EntityCQRSHandler<TContext, TCommand, TResult>, ICommandCQRSHandler<TContext, TCommand, TResult> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS<TResult>
    {
        public override BusinessAsync<TResult> Async(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in command, transaction, token);
        }

        public override BusinessAsync<TResult> Async(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, command, transaction, token);
        }
    }

    public abstract class CommandCQRSHandler<TContext, TCommand> : EntityCQRSHandler<TContext, TCommand>, ICommandCQRSHandler<TContext, TCommand> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS
    {
        public override BusinessAsync Async(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in command, transaction, token);
        }

        public override BusinessAsync Async(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, command, transaction, token);
        }
    }
}