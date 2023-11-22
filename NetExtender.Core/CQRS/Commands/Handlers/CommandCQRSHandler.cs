// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Commands.Handlers.Interfaces;
using NetExtender.CQRS.Commands.Interfaces;
using NetExtender.CQRS.Handlers;

namespace NetExtender.CQRS.Commands.Handlers
{
    public abstract class CommandCQRSHandler<TCommand, TResult> : EntityCQRSHandler<TCommand, TResult>, ICommandCQRSHandler<TCommand, TResult> where TCommand : ICommandCQRS<TResult>
    {
        public override Task<TResult> HandleAsync(TCommand command)
        {
            return HandleAsync(command, CancellationToken.None);
        }
        
        public abstract override Task<TResult> HandleAsync(TCommand command, CancellationToken token);
    }
    
    public abstract class CommandCQRSHandler<TCommand> : EntityCQRSHandler<TCommand>, ICommandCQRSHandler<TCommand> where TCommand : ICommandCQRS
    {
        public override Task HandleAsync(TCommand command)
        {
            return HandleAsync(command, CancellationToken.None);
        }
        
        public abstract override Task HandleAsync(TCommand command, CancellationToken token);
    }
}