using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Handlers.Chain;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class ChainHandlerUtilities
    {
        public static DynamicChainHandler<T> Add<T>(this IObservableChainHandlerCollection<T, IChainHandler<T>> collection, Func<T, T> handler)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            
            DynamicChainHandler<T> dynamic = new DynamicChainHandler<T>(handler);
            collection.Add(dynamic);
            return dynamic;
        }
        
        public static DynamicChainHandler<T>[] AddRange<T>(this IObservableChainHandlerCollection<T, IChainHandler<T>> collection, params Func<T, T>?[]? handlers)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (handlers is null || handlers.Length <= 0)
            {
                return Array.Empty<DynamicChainHandler<T>>();
            }
            
            List<DynamicChainHandler<T>> dynamics = new List<DynamicChainHandler<T>>(handlers.Length);
            dynamics.AddRange(handlers.WhereNotNull().Select(static handler => new DynamicChainHandler<T>(handler)));
            
            collection.AddRange(dynamics.Cast<IChainHandler<T>>());
            return dynamics.ToArray();
        }
    }
}