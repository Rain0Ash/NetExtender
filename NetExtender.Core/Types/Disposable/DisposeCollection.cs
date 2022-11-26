// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetExtender.Types.Comparers;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Disposable
{
    public class DisposeCollection : DisposeCollection<IDisposable>
    {
    }

    public class AsyncDisposeCollection : AsyncDisposeCollection<IAsyncDisposable>
    {
    }

    public class DisposeCollection<T> : ICollection<T>, IReadOnlyCollection<T>, IDisposable where T : IDisposable
    {
        private ConcurrentDictionary<T, Boolean> Internal { get; }

        public Boolean Active { get; set; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return ((ICollection<T>) Internal).IsReadOnly;
            }
        }

        public DisposeCollection()
        {
            Internal = new ConcurrentDictionary<T, Boolean>(ReferenceEqualityComparer<T>.Default);
        }

        public DisposeCollection(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Internal = new ConcurrentDictionary<T, Boolean>(source.Select(item => new KeyValuePair<T, Boolean>(item, false)), ReferenceEqualityComparer<T>.Default);
        }

        public Boolean Contains(T item)
        {
            return Internal.ContainsKey(item);
        }

        public void Add(T item)
        {
            Internal.TryAdd(item, false);
        }

        public Boolean Remove(T item)
        {
           Boolean successful = Internal.TryRemove(item, out _);

           // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
           if (Active && successful && item is not null)
           {
               item.Dispose();
           }

           return successful;
        }

        public void Clear()
        {
            if (Active)
            {
                Dispose();
                return;
            }

            Internal.Clear();
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            Internal.Keys.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            this.DisposeAll();
            Internal.Clear();
            GC.SuppressFinalize(this);
        }
    }

    public class AsyncDisposeCollection<T> : ICollection<T>, IReadOnlyCollection<T>, IAsyncDisposable where T : IAsyncDisposable
    {
        private ConcurrentDictionary<T, Boolean> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return ((ICollection<T>) Internal).IsReadOnly;
            }
        }

        public AsyncDisposeCollection()
        {
            Internal = new ConcurrentDictionary<T, Boolean>(ReferenceEqualityComparer<T>.Default);
        }

        public AsyncDisposeCollection(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Internal = new ConcurrentDictionary<T, Boolean>(source.Select(item => new KeyValuePair<T, Boolean>(item, false)), ReferenceEqualityComparer<T>.Default);
        }

        public Boolean Contains(T item)
        {
            return Internal.ContainsKey(item);
        }

        public void Add(T item)
        {
            Internal.TryAdd(item, false);
        }

        public Boolean Remove(T item)
        {
            return Internal.TryRemove(item, out _);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            Internal.Keys.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAllAsync();
            Internal.Clear();
            GC.SuppressFinalize(this);
        }
    }
}