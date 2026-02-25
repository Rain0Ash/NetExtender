using System;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.CQRS.Commands.Dispatchers.Interfaces;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Events.Dispatchers.Interfaces;
using NetExtender.CQRS.Querys.Dispatchers.Interfaces;
using NetExtender.CQRS.Requests.Dispatchers.Interfaces;
using NetExtender.Initializer.CQRS.Exceptions;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS
{
    public partial interface ICQRS
    {
    }

    public abstract partial class CQRS<TContext> : CQRS, IQueryCQRSDispatcher<TContext>, IRequestCQRSDispatcher<TContext>, ICommandCQRSDispatcher<TContext>, IEventCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        protected abstract IServiceProvider Provider { get; }
        public readonly Dispatcher.Query Query;
        public readonly Dispatcher.Request Request;
        public readonly Dispatcher.Command Command;
        public readonly Dispatcher.Event Event;
        public readonly Dispatcher.Entity Entity;

        protected CQRS()
        {
            Query = new Dispatcher.Query(this);
            Request = new Dispatcher.Request(this);
            Command = new Dispatcher.Command(this);
            Event = new Dispatcher.Event(this);
            Entity = new Dispatcher.Entity(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ref TContext Next<T>(ref TContext context, T value) where T : notnull
        {
            if (context.IsCQRS)
            {
                context = context.Continue(value);
            }

            return ref context;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ref TContext Next<T>(ref TContext context, in T value) where T : notnull
        {
            if (context.IsCQRS)
            {
                context = context.Continue(in value);
            }

            return ref context;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Exception Throw<T>()
        {
            return new DispatchNotSupportedException<T>(GetType());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, TQuery query)
        {
            return Query.Async<TQuery, TResult>(context, query);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, TQuery query, CancellationToken token)
        {
            return Query.Async<TQuery, TResult>(context, query, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, in TQuery query)
        {
            return Query.Async<TQuery, TResult>(context, in query);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, in TQuery query, CancellationToken token)
        {
            return Query.Async<TQuery, TResult>(context, in query, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction)
        {
            return Query.Async<TQuery, TResult>(context, query, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Query.Async<TQuery, TResult>(context, query, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction)
        {
            return Query.Async<TQuery, TResult>(context, in query, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Query.Async<TQuery, TResult>(context, in query, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, TQuery query)
        {
            return Query.SafeAsync<TQuery, TResult>(context, query);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, TQuery query, CancellationToken token)
        {
            return Query.SafeAsync<TQuery, TResult>(context, query, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, in TQuery query)
        {
            return Query.SafeAsync<TQuery, TResult>(context, in query);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, in TQuery query, CancellationToken token)
        {
            return Query.SafeAsync<TQuery, TResult>(context, in query, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction)
        {
            return Query.SafeAsync<TQuery, TResult>(context, query, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Query.SafeAsync<TQuery, TResult>(context, query, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction)
        {
            return Query.SafeAsync<TQuery, TResult>(context, in query, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Query.SafeAsync<TQuery, TResult>(context, in query, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, TRequest request)
        {
            return Request.Async(context, request);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, TRequest request, CancellationToken token)
        {
            return Request.Async(context, request, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, in TRequest request)
        {
            return Request.Async(context, in request);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, in TRequest request, CancellationToken token)
        {
            return Request.Async(context, in request, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction)
        {
            return Request.Async(context, request, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Request.Async(context, request, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction)
        {
            return Request.Async(context, in request, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Request.Async(context, in request, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, TRequest request)
        {
            return Request.SafeAsync(context, request);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, TRequest request, CancellationToken token)
        {
            return Request.SafeAsync(context, request, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, in TRequest request)
        {
            return Request.SafeAsync(context, in request);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, in TRequest request, CancellationToken token)
        {
            return Request.SafeAsync(context, in request, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction)
        {
            return Request.SafeAsync(context, request, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Request.SafeAsync(context, request, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction)
        {
            return Request.SafeAsync(context, in request, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Request.SafeAsync(context, in request, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, TRequest request)
        {
            return Request.Async<TRequest, TResult>(context, request);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, TRequest request, CancellationToken token)
        {
            return Request.Async<TRequest, TResult>(context, request, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, in TRequest request)
        {
            return Request.Async<TRequest, TResult>(context, in request);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, in TRequest request, CancellationToken token)
        {
            return Request.Async<TRequest, TResult>(context, in request, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction)
        {
            return Request.Async<TRequest, TResult>(context, request, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Request.Async<TRequest, TResult>(context, request, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction)
        {
            return Request.Async<TRequest, TResult>(context, in request, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Request.Async<TRequest, TResult>(context, in request, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, TRequest request)
        {
            return Request.SafeAsync<TRequest, TResult>(context, request);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, TRequest request, CancellationToken token)
        {
            return Request.SafeAsync<TRequest, TResult>(context, request, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, in TRequest request)
        {
            return Request.SafeAsync<TRequest, TResult>(context, in request);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, in TRequest request, CancellationToken token)
        {
            return Request.SafeAsync<TRequest, TResult>(context, in request, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction)
        {
            return Request.SafeAsync<TRequest, TResult>(context, request, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Request.SafeAsync<TRequest, TResult>(context, request, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction)
        {
            return Request.SafeAsync<TRequest, TResult>(context, in request, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Request.SafeAsync<TRequest, TResult>(context, in request, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, TCommand command)
        {
            return Command.Async(context, command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, TCommand command, CancellationToken token)
        {
            return Command.Async(context, command, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, in TCommand command)
        {
            return Command.Async(context, in command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, in TCommand command, CancellationToken token)
        {
            return Command.Async(context, in command, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction)
        {
            return Command.Async(context, command, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Command.Async(context, command, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction)
        {
            return Command.Async(context, in command, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync ICommandCQRSDispatcher<TContext>.Async<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Command.Async(context, in command, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, TCommand command)
        {
            return Command.SafeAsync(context, command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, TCommand command, CancellationToken token)
        {
            return Command.SafeAsync(context, command, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, in TCommand command)
        {
            return Command.SafeAsync(context, in command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, in TCommand command, CancellationToken token)
        {
            return Command.SafeAsync(context, in command, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction)
        {
            return Command.SafeAsync(context, command, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Command.SafeAsync(context, command, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction)
        {
            return Command.SafeAsync(context, in command, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Command.SafeAsync(context, in command, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, TCommand command)
        {
            return Command.Async<TCommand, TResult>(context, command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, TCommand command, CancellationToken token)
        {
            return Command.Async<TCommand, TResult>(context, command, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, in TCommand command)
        {
            return Command.Async<TCommand, TResult>(context, in command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, in TCommand command, CancellationToken token)
        {
            return Command.Async<TCommand, TResult>(context, in command, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction)
        {
            return Command.Async<TCommand, TResult>(context, command, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Command.Async<TCommand, TResult>(context, command, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction)
        {
            return Command.Async<TCommand, TResult>(context, in command, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> ICommandCQRSDispatcher<TContext>.Async<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Command.Async<TCommand, TResult>(context, in command, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, TCommand command)
        {
            return Command.SafeAsync<TCommand, TResult>(context, command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, TCommand command, CancellationToken token)
        {
            return Command.SafeAsync<TCommand, TResult>(context, command, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, in TCommand command)
        {
            return Command.SafeAsync<TCommand, TResult>(context, in command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, in TCommand command, CancellationToken token)
        {
            return Command.SafeAsync<TCommand, TResult>(context, in command, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction)
        {
            return Command.SafeAsync<TCommand, TResult>(context, command, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Command.SafeAsync<TCommand, TResult>(context, command, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction)
        {
            return Command.SafeAsync<TCommand, TResult>(context, in command, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> ICommandCQRSDispatcher<TContext>.SafeAsync<TCommand, TResult>(TContext context, in TCommand command, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Command.SafeAsync<TCommand, TResult>(context, in command, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, TEvent @event)
        {
            return Event.Async(context, @event);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, TEvent @event, CancellationToken token)
        {
            return Event.Async(context, @event, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, in TEvent @event)
        {
            return Event.Async(context, in @event);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, in TEvent @event, CancellationToken token)
        {
            return Event.Async(context, in @event, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction)
        {
            return Event.Async(context, @event, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Event.Async(context, @event, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction)
        {
            return Event.Async(context, in @event, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Event.Async(context, in @event, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, TEvent @event)
        {
            return Event.SafeAsync(context, @event);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, TEvent @event, CancellationToken token)
        {
            return Event.SafeAsync(context, @event, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, in TEvent @event)
        {
            return Event.SafeAsync(context, in @event);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, in TEvent @event, CancellationToken token)
        {
            return Event.SafeAsync(context, in @event, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction)
        {
            return Event.SafeAsync(context, @event, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Event.SafeAsync(context, @event, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction)
        {
            return Event.SafeAsync(context, in @event, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Event.SafeAsync(context, in @event, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEntityCQRSDispatcher<TContext>.Async<TEntity>(TContext context, TEntity entity)
        {
            return Entity.Async(context, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEntityCQRSDispatcher<TContext>.Async<TEntity>(TContext context, TEntity entity, CancellationToken token)
        {
            return Entity.Async(context, entity, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEntityCQRSDispatcher<TContext>.Async<TEntity>(TContext context, in TEntity entity)
        {
            return Entity.Async(context, in entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEntityCQRSDispatcher<TContext>.Async<TEntity>(TContext context, in TEntity entity, CancellationToken token)
        {
            return Entity.Async(context, in entity, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEntityCQRSDispatcher<TContext>.Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction)
        {
            return Entity.Async(context, entity, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEntityCQRSDispatcher<TContext>.Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Entity.Async(context, entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEntityCQRSDispatcher<TContext>.Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction)
        {
            return Entity.Async(context, in entity, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync IEntityCQRSDispatcher<TContext>.Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Entity.Async(context, in entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity>(TContext context, TEntity entity)
        {
            return Entity.SafeAsync(context, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity>(TContext context, TEntity entity, CancellationToken token)
        {
            return Entity.SafeAsync(context, entity, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity>(TContext context, in TEntity entity)
        {
            return Entity.SafeAsync(context, in entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity>(TContext context, in TEntity entity, CancellationToken token)
        {
            return Entity.SafeAsync(context, in entity, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction)
        {
            return Entity.SafeAsync(context, entity, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Entity.SafeAsync(context, entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction)
        {
            return Entity.SafeAsync(context, in entity, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Entity.SafeAsync(context, in entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IEntityCQRSDispatcher<TContext>.Async<TEntity, TResult>(TContext context, TEntity entity)
        {
            return Entity.Async<TEntity, TResult>(context, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IEntityCQRSDispatcher<TContext>.Async<TEntity, TResult>(TContext context, TEntity entity, CancellationToken token)
        {
            return Entity.Async<TEntity, TResult>(context, entity, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IEntityCQRSDispatcher<TContext>.Async<TEntity, TResult>(TContext context, in TEntity entity)
        {
            return Entity.Async<TEntity, TResult>(context, in entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IEntityCQRSDispatcher<TContext>.Async<TEntity, TResult>(TContext context, in TEntity entity, CancellationToken token)
        {
            return Entity.Async<TEntity, TResult>(context, in entity, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IEntityCQRSDispatcher<TContext>.Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction)
        {
            return Entity.Async<TEntity, TResult>(context, entity, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IEntityCQRSDispatcher<TContext>.Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Entity.Async<TEntity, TResult>(context, entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IEntityCQRSDispatcher<TContext>.Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction)
        {
            return Entity.Async<TEntity, TResult>(context, in entity, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BusinessAsync<TResult> IEntityCQRSDispatcher<TContext>.Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Entity.Async<TEntity, TResult>(context, in entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity, TResult>(TContext context, TEntity entity)
        {
            return Entity.SafeAsync<TEntity, TResult>(context, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity, TResult>(TContext context, TEntity entity, CancellationToken token)
        {
            return Entity.SafeAsync<TEntity, TResult>(context, entity, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity, TResult>(TContext context, in TEntity entity)
        {
            return Entity.SafeAsync<TEntity, TResult>(context, in entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, CancellationToken token)
        {
            return Entity.SafeAsync<TEntity, TResult>(context, in entity, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction)
        {
            return Entity.SafeAsync<TEntity, TResult>(context, entity, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Entity.SafeAsync<TEntity, TResult>(context, entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction)
        {
            return Entity.SafeAsync<TEntity, TResult>(context, in entity, transaction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Async<TResult> IEntityCQRSDispatcher<TContext>.SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Entity.SafeAsync<TEntity, TResult>(context, in entity, transaction, token);
        }
    }

    public abstract class CQRS : ICQRS
    {
        internal CQRS()
        {
        }
    }
}