using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.CQRS.Commands.Dispatchers;
using NetExtender.CQRS.Commands.Handlers;
using NetExtender.CQRS.Commands.Handlers.Interfaces;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.CQRS
{
    public partial class CQRS<TContext>
    {
        public abstract class CommandCQRSDispatcher : CommandCQRSDispatcher<TContext>
        {
        }

        public abstract class CommandCQRSHandler<TCommand> : NetExtender.CQRS.Commands.Handlers.CommandCQRSHandler<TContext, TCommand> where TCommand : ICommandCQRS
        {
        }

        public abstract class CommandCQRSHandler<TCommand, TResult> : CommandCQRSHandler<TContext, TCommand, TResult> where TCommand : ICommandCQRS<TResult>
        {
        }
    }

    public partial class CQRS<TContext>
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static partial class Dispatcher
        {
            private static class Command<TCommand> where TCommand : ICommandCQRS
            {
                public static Type? Handler { get; }

                static Command()
                {
                    Handler = ReflectionUtilities.Inherit[typeof(ICommandCQRSHandler<TContext, TCommand>)].Types.Search(static type => type is { IsAbstract: false }, null);
                }
            }

            private static class Command<TCommand, TResult> where TCommand : ICommandCQRS<TResult>
            {
                public static Type? Handler { get; }

                static Command()
                {
                    Handler = ReflectionUtilities.Inherit[typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)].Types.Search(static type => type is { IsAbstract: false }, null);
                }
            }

            public readonly struct Command
            {
                private CQRS<TContext> CQRS { get; }

                public Command(CQRS<TContext> dispatcher)
                {
                    CQRS = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TCommand>(TContext context, TCommand command) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.Async(Next(ref context, command), command) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TCommand>(TContext context, TCommand command, CancellationToken token) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.Async(Next(ref context, command), command, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TCommand>(TContext context, in TCommand command) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.Async(Next(ref context, in command), in command) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TCommand>(TContext context, in TCommand command, CancellationToken token) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.Async(Next(ref context, in command), in command, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.Async(Next(ref context, command), command, transaction) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.Async(Next(ref context, command), command, transaction, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.Async(Next(ref context, in command), in command, transaction) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.Async(Next(ref context, in command), in command, transaction, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TCommand>(TContext context, TCommand command) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.SafeAsync(Next(ref context, command), command) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TCommand>(TContext context, TCommand command, CancellationToken token) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.SafeAsync(Next(ref context, command), command, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TCommand>(TContext context, in TCommand command) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.SafeAsync(Next(ref context, in command), in command) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TCommand>(TContext context, in TCommand command, CancellationToken token) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.SafeAsync(Next(ref context, in command), in command, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.SafeAsync(Next(ref context, command), command, transaction) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.SafeAsync(Next(ref context, command), command, transaction, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.SafeAsync(Next(ref context, in command), in command, transaction) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS
                {
                    return CQRS.Provider.GetService(Command<TCommand>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand>)) is ICommandCQRSHandler<TContext, TCommand> handler ? handler.SafeAsync(Next(ref context, in command), in command, transaction, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, TCommand command) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.Async(Next(ref context, command), command) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.Async(Next(ref context, command), command, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, in TCommand command) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.Async(Next(ref context, in command), in command) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, in TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.Async(Next(ref context, in command), in command, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.Async(Next(ref context, command), command, transaction) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.Async(Next(ref context, command), command, transaction, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.Async(Next(ref context, in command), in command, transaction) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.Async(Next(ref context, in command), in command, transaction, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TCommand, TResult>(TContext context, TCommand command) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.SafeAsync(Next(ref context, command), command) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TCommand, TResult>(TContext context, TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.SafeAsync(Next(ref context, command), command, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TCommand, TResult>(TContext context, in TCommand command) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.SafeAsync(Next(ref context, in command), in command) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TCommand, TResult>(TContext context, in TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.SafeAsync(Next(ref context, in command), in command, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.SafeAsync(Next(ref context, command), command, transaction) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.SafeAsync(Next(ref context, command), command, transaction, token) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.SafeAsync(Next(ref context, in command), in command, transaction) : throw CQRS.Throw<TCommand>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token) where TCommand : ICommandCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Command<TCommand, TResult>.Handler ?? typeof(ICommandCQRSHandler<TContext, TCommand, TResult>)) is ICommandCQRSHandler<TContext, TCommand, TResult> handler ? handler.SafeAsync(Next(ref context, in command), in command, transaction, token) : throw CQRS.Throw<TCommand>();
                }
            }
        }
    }
}