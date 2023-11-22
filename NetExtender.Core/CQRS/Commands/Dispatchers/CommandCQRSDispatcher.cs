// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Commands.Dispatchers.Interfaces;
using NetExtender.CQRS.Dispatchers;

namespace NetExtender.CQRS.Commands.Dispatchers
{
    public abstract class CommandCQRSDispatcher : EntityCQRSDispatcher, ICommandCQRSDispatcher
    {
        Task ICommandCQRSDispatcher.DispatchAsync<TCommand>(TCommand command)
        {
            return DispatchAsync(command);
        }
        
        Task ICommandCQRSDispatcher.DispatchAsync<TCommand>(TCommand command, CancellationToken token)
        {
            return DispatchAsync(command, token);
        }

        Task<TResult> ICommandCQRSDispatcher.DispatchAsync<TCommand, TResult>(TCommand command)
        {
            return DispatchAsync<TCommand, TResult>(command);
        }

        Task<TResult> ICommandCQRSDispatcher.DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken token)
        {
            return DispatchAsync<TCommand, TResult>(command, token);
        }
    }
}