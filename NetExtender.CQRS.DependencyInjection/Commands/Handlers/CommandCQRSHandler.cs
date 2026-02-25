// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Commands.Handlers.Interfaces;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Commands.Handlers
{
    public abstract class CommandTransientCQRSHandler<TContext, TCommand, TResult> : CommandCQRSHandler<TContext, TCommand, TResult>, ITransient<ICommandCQRSHandler<TContext, TCommand, TResult>> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS<TResult>
    {
    }

    public abstract class CommandScopedCQRSHandler<TContext, TCommand, TResult> : CommandCQRSHandler<TContext, TCommand, TResult>, IScoped<ICommandCQRSHandler<TContext, TCommand, TResult>> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS<TResult>
    {
    }

    public abstract class CommandSingletonCQRSHandler<TContext, TCommand, TResult> : CommandCQRSHandler<TContext, TCommand, TResult>, ISingleton<ICommandCQRSHandler<TContext, TCommand, TResult>> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS<TResult>
    {
    }

    public abstract class CommandTransientCQRSHandler<TContext, TCommand> : CommandCQRSHandler<TContext, TCommand>, ITransient<ICommandCQRSHandler<TContext, TCommand>> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS
    {
    }

    public abstract class CommandScopedCQRSHandler<TContext, TCommand> : CommandCQRSHandler<TContext, TCommand>, IScoped<ICommandCQRSHandler<TContext, TCommand>> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS
    {
    }

    public abstract class CommandSingletonCQRSHandler<TContext, TCommand> : CommandCQRSHandler<TContext, TCommand>, ISingleton<ICommandCQRSHandler<TContext, TCommand>> where TContext : CQRS<TContext>.IContext where TCommand : ICommandCQRS
    {
    }
}