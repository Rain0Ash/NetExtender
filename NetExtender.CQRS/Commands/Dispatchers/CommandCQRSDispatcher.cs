// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Commands.Dispatchers.Interfaces;
using NetExtender.CQRS.Dispatchers;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Commands.Dispatchers
{
    public abstract class CommandCQRSDispatcher<TContext> : EntityCQRSDispatcher<TContext>, ICommandCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, TCommand command)
        {
            return Async(context, command);
        }

        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, TCommand command, CancellationToken token)
        {
            return Async(context, command, token);
        }

        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, in TCommand command)
        {
            return Async(context, in command);
        }

        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, in TCommand command, CancellationToken token)
        {
            return Async(context, in command, token);
        }

        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction)
        {
            return Async(context, command, transaction);
        }

        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, command, transaction, token);
        }

        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction)
        {
            return Async(context, in command, transaction);
        }

        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in command, transaction, token);
        }

        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, TCommand command)
        {
            return SafeAsync(context, command);
        }

        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, TCommand command, CancellationToken token)
        {
            return SafeAsync(context, command, token);
        }

        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, in TCommand command)
        {
            return SafeAsync(context, in command);
        }

        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, in TCommand command, CancellationToken token)
        {
            return SafeAsync(context, in command, token);
        }

        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, command, transaction);
        }

        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync(context, command, transaction, token);
        }

        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, in command, transaction);
        }

        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync(context, in command, transaction, token);
        }

        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, TCommand command)
        {
            return Async<TCommand, TResult>(context, command);
        }

        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, TCommand command, CancellationToken token)
        {
            return Async<TCommand, TResult>(context, command, token);
        }

        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, in TCommand command)
        {
            return Async<TCommand, TResult>(context, in command);
        }

        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, in TCommand command, CancellationToken token)
        {
            return Async<TCommand, TResult>(context, in command, token);
        }

        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction)
        {
            return Async<TCommand, TResult>(context, command, transaction);
        }

        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async<TCommand, TResult>(context, command, transaction, token);
        }

        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction)
        {
            return Async<TCommand, TResult>(context, in command, transaction);
        }

        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async<TCommand, TResult>(context, in command, transaction, token);
        }

        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, TCommand command)
        {
            return SafeAsync<TCommand, TResult>(context, command);
        }

        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, TCommand command, CancellationToken token)
        {
            return SafeAsync<TCommand, TResult>(context, command, token);
        }

        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, in TCommand command)
        {
            return SafeAsync<TCommand, TResult>(context, in command);
        }

        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, in TCommand command, CancellationToken token)
        {
            return SafeAsync<TCommand, TResult>(context, in command, token);
        }

        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction)
        {
            return SafeAsync<TCommand, TResult>(context, command, transaction);
        }

        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync<TCommand, TResult>(context, command, transaction, token);
        }

        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction)
        {
            return SafeAsync<TCommand, TResult>(context, in command, transaction);
        }

        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync<TCommand, TResult>(context, in command, transaction, token);
        }
    }
}