using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Entities.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class EntityUtilities
    {
        public static IEnumerable<IEntity> Events(this IEnumerable<IEntity> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return EnumerableBaseUtilities.WhereNotNull(entities.Select(static entity => entity.Events)).SelectMany(static entity => entity);
        }

        public static IEnumerable<TEvent> Events<TEvent, TEvents>(this IEnumerable<IEventsEntity<TEvent, TEvents>> entities) where TEvents : class, IReadOnlyCollection<TEvent>
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return EnumerableBaseUtilities.WhereNotNull(entities.Select(static entity => entity.Events)).SelectMany(static entity => entity);
        }

        public static IQueryable<IEntity> Events(this IQueryable<IEntity> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return QueryableBaseUtilities.WhereNotNull(entities.Select(static entity => entity.Events)).SelectMany(static entity => entity);
        }

        public static IQueryable<TEvent> Events<TEvent, TEvents>(this IQueryable<IEventsEntity<TEvent, TEvents>> entities) where TEvents : class, IReadOnlyCollection<TEvent>
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return QueryableBaseUtilities.WhereNotNull(entities.Select(static entity => entity.Events)).SelectMany(static entity => entity);
        }
    }
}