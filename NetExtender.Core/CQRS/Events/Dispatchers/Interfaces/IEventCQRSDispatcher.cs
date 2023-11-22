// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Events.Interfaces;

namespace NetExtender.CQRS.Events.Dispatchers.Interfaces
{
    public interface IEventCQRSDispatcher : IEntityCQRSDispatcher
    {
        public new Task DispatchAsync<TEvent>(TEvent @event) where TEvent : IEventCQRS;
        public new Task DispatchAsync<TEvent>(TEvent @event, CancellationToken token) where TEvent : IEventCQRS;
    }
}