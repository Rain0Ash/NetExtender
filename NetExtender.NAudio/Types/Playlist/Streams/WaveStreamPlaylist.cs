// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NAudio.Wave;
using NetExtender.NAudio.Types.Playlist.Interfaces;
using NetExtender.Utilities.NAudio;
using NetExtender.Utilities.Types;

namespace NetExtender.NAudio.Types.Playlist
{
    public class WaveStreamPlaylist : WaveStreamPlaylist<WaveStream>, IReadOnlyWaveStreamPlaylist, IWaveStreamPlaylist
    {
        public WaveStreamPlaylist()
        {
        }

        public WaveStreamPlaylist(params WaveStream[] items)
            : base(items)
        {
        }

        public WaveStreamPlaylist(IEnumerable<WaveStream> source)
            : base(source)
        {
        }
    }

    public class WaveStreamPlaylist<T> : WaveStream, IReadOnlyWaveStreamPlaylist<T>, IWaveStreamPlaylist<T> where T : WaveStream
    {
        protected List<T> Queue { get; }

        public virtual Int32 Index
        {
            get
            {
                return 0;
            }
            set
            {
                Skip(value);
            }
        }

        public virtual Boolean IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual Int32 Count
        {
            get
            {
                return Queue.Count;
            }
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return Current?.WaveFormat ?? WaveFormatUtilities.Empty;
            }
        }

        public override Int64 Length
        {
            get
            {
                return Current?.Length ?? 0;
            }
        }

        public override Int64 Position
        {
            get
            {
                return Current?.Position ?? 0;
            }
            set
            {
                if (Current is null)
                {
                    return;
                }

                Current.Position = value;
            }
        }

        public override Int32 BlockAlign
        {
            get
            {
                return Current?.BlockAlign ?? 0;
            }
        }

        public override TimeSpan TotalTime
        {
            get
            {
                return Current?.TotalTime ?? TimeSpan.Zero;
            }
        }

        public override TimeSpan CurrentTime
        {
            get
            {
                return Current?.CurrentTime ?? TimeSpan.Zero;
            }
            set
            {
                lock (Queue)
                {
                    T? current = Current;
                    if (current is not null)
                    {
                        current.CurrentTime = value;
                    }
                }
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

        public WaveStreamPlaylist()
        {
            Queue = new List<T>();
        }

        public WaveStreamPlaylist(params T[] items)
            : this((IEnumerable<T>) items)
        {
        }

        public WaveStreamPlaylist(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Queue = new List<T>(source.WhereNotNull());
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            T? current = Current;

            if (current is null)
            {
                return 0;
            }

            Int32 read = current.Read(buffer, offset, count);
            if (read <= 0)
            {
                Remove(current);
            }

            return read;
        }

        public virtual Boolean Contains(T? item)
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

        public virtual Int32 IndexOf(T? item)
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

        public virtual void Add(T? item)
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

        public virtual void AddRange(IEnumerable<T> source)
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

        public virtual void Insert(Int32 index, T? item)
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

        public void Next()
        {
            Next(1);
        }

        public virtual void Next(Int32 skip)
        {
            if (skip <= 0)
            {
                return;
            }

            lock (Queue)
            {
                Queue.RemoveRange(Index, skip);
            }
        }

        public virtual Boolean Remove(T? item)
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

        public virtual void RemoveAt(Int32 index)
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

        public virtual void RemoveRange(Int32 index, Int32 count)
        {
            lock (Queue)
            {
                Queue.RemoveRange(index, count);
            }
        }

        public virtual Int32 RemoveAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            lock (Queue)
            {
                return Queue.RemoveAll(match);
            }
        }

        public virtual void Clear()
        {
            lock (Queue)
            {
                Queue.Clear();
            }
        }

        public virtual void CopyTo(T[] array, Int32 index)
        {
            lock (Queue)
            {
                Queue.CopyTo(array, index);
            }
        }

        public virtual IEnumerator<T> GetEnumerator()
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

        public virtual T this[Int32 index]
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