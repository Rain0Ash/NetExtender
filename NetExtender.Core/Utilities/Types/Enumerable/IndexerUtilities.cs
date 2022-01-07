// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Initializer.Types.Indexers;
using NetExtender.Initializer.Types.Indexers.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class IndexerUtilities
    {
        public static Indexer<T> ToIndexer<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Indexer<T>(source);
        }
        
        public static Indexer<T> ToIndexer<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Indexer<T>(source, comparer);
        }
        
        public static MapIndexer<T> ToMapIndexer<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new MapIndexer<T>(source);
        }
        
        public static MapIndexer<T> ToMapIndexer<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new MapIndexer<T>(source, comparer);
        }

        public static IReadOnlyIndexer<T> AsIReadOnlyIndexer<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IReadOnlyIndexer<T> ?? new Indexer<T>(source) : new Indexer<T>();
        }
        
        public static IIndexer<T> AsIIndexer<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IIndexer<T> ?? new Indexer<T>(source) : new Indexer<T>();
        }
        
        public static Indexer<T> AsIndexer<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as Indexer<T> ?? new Indexer<T>(source) : new Indexer<T>();
        }
        
        public static IReadOnlyMapIndexer<T> AsIReadOnlyMapIndexer<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IReadOnlyMapIndexer<T> ?? new MapIndexer<T>(source) : new MapIndexer<T>();
        }
        
        public static IMapIndexer<T> AsIMapIndexer<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IMapIndexer<T> ?? new MapIndexer<T>(source) : new MapIndexer<T>();
        }
        
        public static MapIndexer<T> AsMapIndexer<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as MapIndexer<T> ?? new MapIndexer<T>(source) : new MapIndexer<T>();
        }
    }
}