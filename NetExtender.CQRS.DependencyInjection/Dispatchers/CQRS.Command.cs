using NetExtender.CQRS.Commands.Handlers.Interfaces;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Dispatchers
{
    public partial class ServiceCQRS<TContext>
    {
        public abstract class CommandTransientCQRSHandler<TCommand, TResult> : CommandCQRSHandler<TCommand, TResult>, ISingleTransient<ICommandCQRSHandler<TContext, TCommand, TResult>> where TCommand : ICommandCQRS<TResult>
        {
        }

        public abstract class CommandScopedCQRSHandler<TCommand, TResult> : CommandCQRSHandler<TCommand, TResult>, ISingleScoped<ICommandCQRSHandler<TContext, TCommand, TResult>> where TCommand : ICommandCQRS<TResult>
        {
        }

        public abstract class CommandSingletonCQRSHandler<TCommand, TResult> : CommandCQRSHandler<TCommand, TResult>, ISingleSingleton<ICommandCQRSHandler<TContext, TCommand, TResult>> where TCommand : ICommandCQRS<TResult>
        {
        }

        public abstract class CommandTransientCQRSHandler<TCommand> : CommandCQRSHandler<TCommand>, ISingleTransient<ICommandCQRSHandler<TContext, TCommand>> where TCommand : ICommandCQRS
        {
        }

        public abstract class CommandScopedCQRSHandler<TCommand> : CommandCQRSHandler<TCommand>, ISingleScoped<ICommandCQRSHandler<TContext, TCommand>> where TCommand : ICommandCQRS
        {
        }

        public abstract class CommandSingletonCQRSHandler<TCommand> : CommandCQRSHandler<TCommand>, ISingleSingleton<ICommandCQRSHandler<TContext, TCommand>> where TCommand : ICommandCQRS
        {
        }
    }
}