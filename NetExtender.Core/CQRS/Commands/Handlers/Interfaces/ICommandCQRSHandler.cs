// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.CQRS.Handlers.Interfaces;

namespace NetExtender.CQRS.Commands.Handlers.Interfaces
{
    public interface ICommandCQRSHandler<in TCommand, TResult> : ICommandCQRSHandler<TCommand>, IEntityCQRSHandler<TCommand, TResult> where TCommand : ICommandCQRS<TResult>
    {
        public new Task<TResult> HandleAsync(TCommand command);
        public new Task<TResult> HandleAsync(TCommand command, CancellationToken token);
    }
    
    public interface ICommandCQRSHandler<in TCommand> : IEntityCQRSHandler<TCommand> where TCommand : ICommandCQRS
    {
        public new Task HandleAsync(TCommand command);
        public new Task HandleAsync(TCommand command, CancellationToken token);
    }
}