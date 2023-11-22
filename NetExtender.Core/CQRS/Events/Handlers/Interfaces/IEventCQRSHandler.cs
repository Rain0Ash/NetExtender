// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Handlers.Interfaces;

namespace NetExtender.CQRS.Events.Handlers.Interfaces
{
    public interface IEventCQRSHandler<in TEvent> : IEntityCQRSHandler<TEvent> where TEvent : IEventCQRS
    {
        public new Task HandleAsync(TEvent @event);
        public new Task HandleAsync(TEvent @event, CancellationToken token);
    }
}