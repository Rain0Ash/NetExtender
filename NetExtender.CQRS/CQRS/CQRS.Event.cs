using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.CQRS.Events.Dispatchers;
using NetExtender.CQRS.Events.Handlers;
using NetExtender.CQRS.Events.Handlers.Interfaces;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Handlers.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Initializer.CQRS.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.CQRS
{
    public partial class CQRS<TContext>
    {
        public abstract class EventCQRSDispatcher : EventCQRSDispatcher<TContext>
        {
        }

        public abstract class EventCQRSHandler<TEvent> : EventCQRSHandler<TContext, TEvent> where TEvent : IEventCQRS
        {
            protected EventCQRSHandler(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? notifier)
                : base(notifier)
            {
            }
        }
    }

    public partial class CQRS<TContext>
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static partial class Dispatcher
        {
            private static partial class Event<TEvent> where TEvent : IEventCQRS
            {
                public static Type Handler { get; }

                static Event()
                {
                    Handler = ReflectionUtilities.Inherit[typeof(IEventCQRSHandler<TContext, TEvent>)].Types.Require(static type => type is { IsAbstract: false }, true);
                }
            }

            public readonly struct Event
            {
                private CQRS<TContext> CQRS { get; }

                public Event(CQRS<TContext> cqrs)
                {
                    CQRS = cqrs ?? throw new ArgumentNullException(nameof(cqrs));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEvent>(TContext context, TEvent @event) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.Async(Next(ref context, @event), @event);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async BusinessAsync Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await (Async) notify.Async(context, @event));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEvent>(TContext context, TEvent @event, CancellationToken token) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.Async(Next(ref context, @event), @event, token);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async BusinessAsync Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, CancellationToken token)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await (Async) notify.Async(context, @event, token));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, token);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEvent>(TContext context, in TEvent @event) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.Async(Next(ref context, in @event), in @event);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async BusinessAsync Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await (Async) notify.Async(context, in @event));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEvent>(TContext context, in TEvent @event, CancellationToken token) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.Async(Next(ref context, in @event), in @event, token);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async BusinessAsync Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, CancellationToken token)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await (Async) notify.Async(context, in @event, token));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, token);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.Async(Next(ref context, @event), @event, transaction);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async BusinessAsync Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, ICQRS.Transaction transaction)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await (Async) notify.Async(context, @event, transaction));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, transaction);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.Async(Next(ref context, @event), @event, transaction, token);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async BusinessAsync Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, ICQRS.Transaction transaction, CancellationToken token)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await (Async) notify.Async(context, @event, transaction, token));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, transaction, token);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.Async(Next(ref context, in @event), in @event, transaction);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async BusinessAsync Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, ICQRS.Transaction transaction)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await (Async) notify.Async(context, in @event, transaction));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, transaction);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.Async(Next(ref context, in @event), in @event, transaction, token);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async BusinessAsync Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, ICQRS.Transaction transaction, CancellationToken token)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await (Async) notify.Async(context, in @event, transaction, token));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, transaction, token);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEvent>(TContext context, TEvent @event) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.SafeAsync(Next(ref context, @event), @event);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async Async Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await notify.SafeAsync(context, @event));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEvent>(TContext context, TEvent @event, CancellationToken token) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.SafeAsync(Next(ref context, @event), @event, token);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async Async Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, CancellationToken token)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await notify.SafeAsync(context, @event, token));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, token);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEvent>(TContext context, in TEvent @event) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.SafeAsync(Next(ref context, in @event), in @event);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async Async Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await notify.SafeAsync(context, in @event));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEvent>(TContext context, in TEvent @event, CancellationToken token) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.SafeAsync(Next(ref context, in @event), in @event, token);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async Async Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, CancellationToken token)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await notify.SafeAsync(context, in @event, token));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, token);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.SafeAsync(Next(ref context, @event), @event, transaction);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async Async Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, ICQRS.Transaction transaction)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await notify.SafeAsync(context, @event, transaction));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, transaction);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.SafeAsync(Next(ref context, @event), @event, transaction, token);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async Async Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, ICQRS.Transaction transaction, CancellationToken token)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await notify.SafeAsync(context, @event, transaction, token));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, transaction, token);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.SafeAsync(Next(ref context, in @event), in @event, transaction);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async Async Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, ICQRS.Transaction transaction)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await notify.SafeAsync(context, in @event, transaction));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, transaction);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token) where TEvent : IEventCQRS
                {
                    if (CQRS.Provider.GetService(Event<TEvent>.Handler) is IEventCQRSHandler<TContext, TEvent> handler)
                    {
                        return handler.SafeAsync(Next(ref context, in @event), in @event, transaction, token);
                    }

                    if (CQRS.Provider.GetService(typeof(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>)) is not IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier)
                    {
                        throw CQRS.Throw<TEvent>();
                    }

                    static async Async Seal(CQRS<TContext> CQRS, TContext context, TEvent @event, IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>> notifier, ICQRS.Transaction transaction, CancellationToken token)
                    {
                        Event<TEvent>.Collector collector = new Event<TEvent>.Collector(CQRS);

                        foreach (INotifyEventCQRSHandler<TContext, TEvent> notify in notifier)
                        {
                            collector.Add(await notify.SafeAsync(context, in @event, transaction, token));
                        }

                        if (collector.Dispose() is { } exception)
                        {
                            throw exception;
                        }
                    }

                    return Seal(CQRS, Next(ref context, @event), @event, notifier, transaction, token);
                }
            }
        }
    }

    public partial class CQRS<TContext>
    {
        public partial class Dispatcher
        {
            private partial class Event<TEvent>
            {
                public struct Collector : IDisposable
                {
                    private static ArrayPool<Exception> Pool { get; } = ArrayPool<Exception>.Create(Byte.MaxValue, 4);

                    private readonly CQRS<TContext> CQRS;
                    private Exception[]? Storage = null;
                    private Byte Count = 0;
                    private Byte Any = 0;
                    private Byte Business = 0;

                    public Collector(CQRS<TContext> cqrs)
                    {
                        CQRS = cqrs;
                    }

                    public void Add(Exception? exception)
                    {
                        if (CQRS is null)
                        {
                            throw new ObjectDisposedException(nameof(CQRS));
                        }

                        Count++;
                        if (exception is null)
                        {
                            return;
                        }

                        Storage ??= Pool.Rent(Byte.MaxValue);
                        Storage[exception is BusinessException ? Any + Business++ : Any++ + Business] = exception;
                    }

                    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
                    public Exception? Dispose()
                    {
                        if (Storage is null)
                        {
                            Count = Any = Business = 0;
                            return null;
                        }

                        Exception? exception;
                        switch (Any + Business)
                        {
                            case 0 when Count <= 0:
                            {
                                exception = CQRS.Throw<TEvent>();
                                break;
                            }
                            case 0:
                            {
                                exception = null;
                                break;
                            }
                            case 1 when Business <= 0:
                            {
                                exception = Storage[0];
                                exception = new CQRSEventNotifyException(exception);
                                break;
                            }
                            case var exceptions when Any <= 0:
                            {
                                BusinessException[] rent = new BusinessException[exceptions];
                                Storage.AsSpan(0, exceptions).CopyTo((Exception[]) rent);
                                exception = new BusinessException(rent);
                                break;
                            }
                            case var exceptions:
                            {
                                Exception[] rent = new Exception[exceptions];
                                Storage.AsSpan(0, exceptions).CopyTo(rent);
                                exception = new CQRSEventNotifyException(rent);
                                break;
                            }
                        }

                        Pool.Return(Storage, true);
                        return exception;
                    }

                    void IDisposable.Dispose()
                    {
                        Dispose();
                    }
                }
            }
        }
    }
}