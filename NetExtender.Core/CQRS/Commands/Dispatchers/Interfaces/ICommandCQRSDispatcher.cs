// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.CQRS.Dispatchers.Interfaces;

namespace NetExtender.CQRS.Commands.Dispatchers.Interfaces
{
    public interface ICommandCQRSDispatcher : IEntityCQRSDispatcher
    {
        public new Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommandCQRS;
        public new Task DispatchAsync<TCommand>(TCommand command, CancellationToken token) where TCommand : ICommandCQRS;
        public new Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : ICommandCQRS<TResult>;
        public new Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken token) where TCommand : ICommandCQRS<TResult>;
    }
}