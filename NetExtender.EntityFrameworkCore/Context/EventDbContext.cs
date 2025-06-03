// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetExtender.CQRS.Events.Dispatchers.Interfaces;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.DependencyInjection.Context.Interfaces;
using NetExtender.Types.Entities.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.EntityFrameworkCore;

namespace NetExtender.EntityFrameworkCore.Context
{
    public delegate Task DbContextEventHandler(IEventCQRSDispatcher dispatcher, IEventCQRS @event, CancellationToken token);

    public enum DbContextBehavior
    {
        Default,
        Lazy,
        Eager
    }
    
    public class EventDbContext : DbContext, ISaveDbContext
    {
        public Assembly? Assembly { get; }
        protected SaveChangesInterceptor? SaveInterceptor { get; }
        protected IEventCQRSDispatcher? EventDispatcher { get; }
        protected ConcurrentDictionary<Type, DbContextEventHandler> EventHandler { get; } = new ConcurrentDictionary<Type, DbContextEventHandler>();
        protected DbContextBehavior ContextBehavior { get; private set; }
        private static MethodInfo Dispatch { get; }

        static EventDbContext()
        {
            Dispatch = typeof(IEventCQRSDispatcher).GetMethod(nameof(IEventCQRSDispatcher.DispatchAsync), new []{ Type.MakeGenericMethodParameter(0), typeof(CancellationToken) }) ?? throw new NeverOperationException();
        }

        public EventDbContext(Assembly? assembly, SaveChangesInterceptor? interceptor)
            : this(assembly, interceptor, null)
        {
        }

        public EventDbContext(Assembly? assembly, SaveChangesInterceptor? interceptor, IEventCQRSDispatcher? dispatcher)
        {
            Assembly = assembly;
            SaveInterceptor = interceptor;
            EventDispatcher = dispatcher;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            switch (this)
            {
                case ILazyDbContext and IEagerDbContext:
                    throw new NotSupportedException("DbContext cannot be lazy and eager at same time.");
                case ILazyDbContext:
                    ContextBehavior = DbContextBehavior.Lazy;
                    builder.UseLazyLoading();
                    return;
                case IEagerDbContext:
                    ContextBehavior = DbContextBehavior.Eager;
                    return;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (Assembly is not null)
            {
                builder.ApplyConfigurationsFromAssembly(Assembly);
            }
            
            base.OnModelCreating(builder);
        }

        public override async Task<Int32> SaveChangesAsync(CancellationToken token = default)
        {
            if (ContextBehavior is DbContextBehavior.Eager)
            {
                throw new NotSupportedException($"Сan't save changes at eager {nameof(DbContext)}.");
            }
            
            IReadOnlyCollection<IAfterSaveEventCQRS> events = await ProcessEventsBeforeSaveChanges(token);

            Int32 result = await base.SaveChangesAsync(token).ConfigureAwait(false);
            await DispatchEvents(events, EventDispatcher, token).ConfigureAwait(false);
            return result;
        }

        public async Task<Int32> SaveChangesDeferEventsAsync(CancellationToken token = default)
        {
            Int32 start = await base.SaveChangesAsync(token).ConfigureAwait(false);
            return await SaveChangesAsync(token) + start;
        }

        private async Task<IReadOnlyCollection<IAfterSaveEventCQRS>> ProcessEventsBeforeSaveChanges(CancellationToken token)
        {
            foreach (EntityEntry<IEntity> entity in ChangeTracker.Entries<IEntity>().Where(static entity => entity.State == EntityState.Deleted))
            {
                entity.Entity.Delete();
            }

            IEntity[] entities = ChangeTracker.Entries<IEntity>().Where(static entity => entity.Entity.Events.Count > 0).Select(static entity => entity.Entity).ToArray();
            IEnumerable<IBeforeSaveEventCQRS> events = entities.SelectMany(static entity => entity.Events).OfType<IBeforeSaveEventCQRS>();

            await DispatchEvents(events, EventDispatcher, token).ConfigureAwait(false);
            return entities.SelectMany(static entity => entity.Events).OfType<IAfterSaveEventCQRS>().ToList();
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        protected virtual async Task DispatchEvents(IEnumerable<IEventCQRS> source, IEventCQRSDispatcher? dispatcher, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (dispatcher is null)
            {
                return;
            }
            
            while (true)
            {
                IEventCQRS[] events = source.ToArray();

                foreach (IEventCQRS @event in events.Where(static @event => !@event.Resolved))
                {
                    @event.Resolved = true;
                    DbContextEventHandler handler = EventHandler.GetOrAdd(@event.Self, CreateEventHandler);
                    await handler.Invoke(dispatcher, @event, token).WaitAsync(token).ConfigureAwait(false);
                }

                if (events.Length == source.Count())
                {
                    break;
                }
            }
        }

        protected static DbContextEventHandler CreateEventHandler(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MethodInfo method = Dispatch.MakeGenericMethod(type);
            
            ParameterExpression dispatcher = Expression.Parameter(typeof(IEventCQRSDispatcher), nameof(dispatcher));
            ParameterExpression @event = Expression.Parameter(typeof(IEventCQRS), nameof(@event));
            ParameterExpression token = Expression.Parameter(typeof(CancellationToken), nameof(token));
            UnaryExpression convert = Expression.Convert(@event, type);

            MethodCallExpression call = Expression.Call(dispatcher, method, convert, token);
            return Expression.Lambda<DbContextEventHandler>(call, dispatcher, @event, token).Compile();
        }
    }
}