// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Events.Handlers.Interfaces;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Handlers;

namespace NetExtender.CQRS.Events.Handlers
{
    public abstract class EventCQRSHandler<TEvent> : EntityCQRSHandler<TEvent>, IEventCQRSHandler<TEvent> where TEvent : IEventCQRS
    {
        public override Task HandleAsync(TEvent @event)
        {
            return HandleAsync(@event, CancellationToken.None);
        }
        
        public abstract override Task HandleAsync(TEvent @event, CancellationToken token);
    }
}