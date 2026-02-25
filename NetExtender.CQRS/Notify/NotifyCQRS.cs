using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Interfaces;

namespace NetExtender.CQRS.Notify
{
    public readonly struct NotifyCQRS<TEvent> : INotifyCQRS<TEvent> where TEvent : IEventCQRS
    {
        public static implicit operator NotifyCQRS<TEvent>(TEvent @event)
        {
            return new NotifyCQRS<TEvent>(@event);
        }

        public readonly TEvent Event;

        TEvent INotifyCQRS<TEvent>.Event
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Event;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Event.IsEmpty;
            }
        }

        public NotifyCQRS(TEvent @event)
        {
            Event = @event;
        }
    }

    public record CustomNotifyCQRS<TEvent> : EntityCQRS, INotifyCQRS<TEvent> where TEvent : IEventCQRS
    {
        [return: NotNullIfNotNull("event")]
        public static implicit operator CustomNotifyCQRS<TEvent>?(TEvent? @event)
        {
            return @event is { IsEmpty: false } ? new CustomNotifyCQRS<TEvent>(@event) : null;
        }

        public TEvent Event { get; }

        public override Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Event.IsEmpty;
            }
        }

        protected CustomNotifyCQRS(TEvent @event)
        {
            Event = @event ?? throw new ArgumentNullException(nameof(@event));
        }
    }
}