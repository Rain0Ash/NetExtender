// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NAudio.Wave;
using NetExtender.NAudio.Types.Playlist.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.NAudio.Types.Playlist
{
    public class WavePlaylist : WavePlaylist<IWaveProvider>, IReadOnlyWavePlaylist, IWavePlaylist
    {
        public WavePlaylist()
        {
        }

        public WavePlaylist(params IWaveProvider[] items)
            : base(items)
        {
        }

        public WavePlaylist(IEnumerable<IWaveProvider> source)
            : base(source)
        {
        }
    }

    public class WavePlaylist<T> : IReadOnlyWavePlaylist<T>, IWavePlaylist<T> where T : class, IWaveProvider
    {
        protected IList<T> Queue { get; }

        public Boolean IsReadOnly
        {
            get
            {
                return Queue.IsReadOnly;
            }
        }

        public Int32 Count
        {
            get
            {
                return Queue.Count;
            }
        }

        public virtual WaveFormat WaveFormat
        {
            get
            {
                return Current?.WaveFormat ?? throw new InvalidOperationException();
            }
        }

        protected virtual T? Current
        {
            get
            {
                lock (Queue)
                {
                    return Queue.Count > 0 ? Queue[0] : null;
                }
            }
        }

        public WavePlaylist()
        {
            Queue = new List<T>();
        }

        public WavePlaylist(params T[] items)
            : this((IEnumerable<T>) items)
        {
        }

        public WavePlaylist(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Queue = new List<T>(source.WhereNotNull());
        }

        public virtual Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Int32 read;
            do
            {
                T? current = Current;

                if (current is null)
                {
                    return 0;
                }

                read = current.Read(buffer, offset, count);
                if (read <= 0)
                {
                    Remove(current);
                }

            } while (read <= 0);

            return read;
        }

        public Boolean Contains(T? item)
        {
            if (item is null)
            {
                return false;
            }

            lock (Queue)
            {
                return Queue.Contains(item);
            }
        }

        public Int32 IndexOf(T? item)
        {
            if (item is null)
            {
                return -1;
            }

            lock (Queue)
            {
                return Queue.IndexOf(item);
            }
        }

        public void Add(T? item)
        {
            if (item is null)
            {
                return;
            }

            lock (Queue)
            {
                Queue.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                Add(item);
            }
        }

        public void Insert(Int32 index, T? item)
        {
            if (item is null)
            {
                return;
            }

            lock (Queue)
            {
                Queue.Insert(index, item);
            }
        }

        public Boolean Remove(T? item)
        {
            if (item is null)
            {
                return false;
            }

            lock (Queue)
            {
                return Queue.Remove(item);
            }
        }

        public void RemoveAt(Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            lock (Queue)
            {
                if (Queue.Count <= index)
                {
                    return;
                }

                Queue.RemoveAt(index);
            }
        }

        public void Clear()
        {
            lock (Queue)
            {
                Queue.Clear();
            }
        }

        public void CopyTo(T[] array, Int32 index)
        {
            lock (Queue)
            {
                Queue.CopyTo(array, index);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (Queue)
            {
                return Queue.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[Int32 index]
        {
            get
            {
                lock (Queue)
                {
                    return Queue[index];
                }
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                lock (Queue)
                {
                    Queue[index] = value;
                }
            }
        }
    }
}