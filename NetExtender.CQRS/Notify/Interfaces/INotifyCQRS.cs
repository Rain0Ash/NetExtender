// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS.Notify.Interfaces
{
    public interface INotifyCQRS<out TEvent> : INotifyCQRS where TEvent : IEventCQRS
    {
        public TEvent Event { get; }
    }

    public interface INotifyCQRS : IEntityCQRS
    {
    }
}