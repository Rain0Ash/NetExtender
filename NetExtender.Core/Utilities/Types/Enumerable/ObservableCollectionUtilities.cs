// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using NetExtender.Random.Interfaces;
using NetExtender.Types.Collections;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class ObservableCollectionUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable? Suppress<T>(this ObservableCollection<T> collection)
        {
            return collection switch
            {
                null => throw new ArgumentNullException(nameof(collection)),
                SuppressObservableCollection<T> suppress => suppress.Suppress(),
                ISuppressObservableCollection<T> suppress => suppress.Suppress(),
                IReadOnlySuppressObservableCollection<T> suppress => suppress.Suppress(),
                _ => null
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SuppressObservableCollection<T> AsSuppressObservableCollection<T>(this ObservableCollection<T> collection)
        {
            return collection switch
            {
                null => throw new ArgumentNullException(nameof(collection)),
                SuppressObservableCollection<T> suppress => suppress,
                _ => new SuppressObservableCollection<T>(collection)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISuppressObservableCollection<T> AsSuppress<T>(this ObservableCollection<T> collection)
        {
            return collection switch
            {
                null => throw new ArgumentNullException(nameof(collection)),
                ISuppressObservableCollection<T> suppress => suppress,
                _ => new SuppressObservableCollectionWrapper<T>(collection)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlySuppressObservableCollection<T> AsReadOnlySuppress<T>(this ObservableCollection<T> collection)
        {
            return collection switch
            {
                null => throw new ArgumentNullException(nameof(collection)),
                IReadOnlySuppressObservableCollection<T> suppress => suppress,
                _ => new SuppressObservableCollectionWrapper<T>(collection)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this ObservableCollection<T> collection, params T[] source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            CollectionUtilities.AddRange(collection, source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            CollectionUtilities.AddRange(collection, source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange<T>(this ObservableCollection<T> collection, params T[] source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            CollectionUtilities.RemoveRange(collection, source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange<T>(this ObservableCollection<T> collection, IEnumerable<T> source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            CollectionUtilities.RemoveRange(collection, source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RemoveAll<T>(this ObservableCollection<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            return CollectionUtilities.RemoveAll(collection, item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RemoveAll<T>(this ObservableCollection<T> collection, params T[] source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            return CollectionUtilities.RemoveAll(collection, source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RemoveAll<T>(this ObservableCollection<T> collection, IEnumerable<T> source)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            return CollectionUtilities.RemoveAll(collection, source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this ObservableCollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            using IDisposable? suppress = collection.Suppress();
            ListUtilities.Shuffle<T>(collection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this ObservableCollection<T> collection, System.Random random)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            using IDisposable? suppress = collection.Suppress();
            ListUtilities.Shuffle<T>(collection, random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this ObservableCollection<T> collection, IRandom random)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            using IDisposable? suppress = collection.Suppress();
            ListUtilities.Shuffle<T>(collection, random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Rotate<T>(this ObservableCollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            ListUtilities.Rotate<T>(collection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Rotate<T>(this ObservableCollection<T> collection, Int32 offset)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            using IDisposable? suppress = collection.Suppress();
            ListUtilities.Rotate<T>(collection, offset);
        }
    }
}