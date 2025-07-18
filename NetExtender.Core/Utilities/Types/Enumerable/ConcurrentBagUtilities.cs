// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NetExtender.Utilities.Types
{
    public static class ConcurrentBagUtilities
    {
        public static void AddRange<T>(this ConcurrentBag<T> collection, params T[]? source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (source is not null)
            {
                AddRange(collection, (IEnumerable<T>) source);
            }
        }
        
        public static void AddRange<T>(this ConcurrentBag<T> collection, IEnumerable<T> source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            foreach (T item in source)
            {
                collection.Add(item);
            }
        }
    }
}