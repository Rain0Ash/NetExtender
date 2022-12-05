// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using NetExtender.NAudio.Types.Playlist.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.NAudio.Types.Playlist
{
    public class RepeatWaveStreamPlaylist : RepeatWaveStreamPlaylist<WaveStream>, IReadOnlyWaveStreamPlaylist, IWaveStreamPlaylist
    {
        public RepeatWaveStreamPlaylist()
        {
        }

        public RepeatWaveStreamPlaylist(params WaveStream[] items)
            : base(items)
        {
        }

        public RepeatWaveStreamPlaylist(IEnumerable<WaveStream> source)
            : base(source)
        {
        }
    }

    public class RepeatWaveStreamPlaylist<T> : WaveStreamPlaylist<T> where T : WaveStream
    {
        private Int32 _index;

        public override Int32 Index
        {
            get
            {
                lock (Queue)
                {
                    return _index.Clamp(0, Queue.Count);
                }
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                lock (Queue)
                {
                    if (Queue.Count <= 0)
                    {
                        return;
                    }

                    _index = value % Queue.Count;
                }
            }
        }

        public override Int64 Length
        {
            get
            {
                lock (Queue)
                {
                    return Queue.Sum(stream => stream.Length);
                }
            }
        }

        public override Int64 Position
        {
            get
            {
                lock (Queue)
                {
                    return Queue.Take(Index).Sum(stream => stream.Length) + StreamPosition;
                }
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                lock (Queue)
                {
                    for (Int32 i = 0; i < Queue.Count; i++)
                    {
                        T stream = Queue[i];
                        Int64 length = stream.Length;
                        if (value < length)
                        {
                            Index = i;
                            StreamPosition = value;
                            return;
                        }

                        value -= length;
                    }
                }
            }
        }

        public virtual Int64 StreamLength
        {
            get
            {
                return base.Length;
            }
        }

        public virtual Int64 StreamPosition
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
            }
        }

        public override TimeSpan TotalTime
        {
            get
            {
                lock (Queue)
                {
                    return Queue.Sum(stream => stream.TotalTime);
                }
            }
        }

        public override TimeSpan CurrentTime
        {
            get
            {
                lock (Queue)
                {
                    return Queue.Take(Index).Sum(stream => stream.TotalTime) + StreamCurrentTime;
                }
            }
            set
            {
                lock (Queue)
                {
                    for (Int32 i = 0; i < Queue.Count; i++)
                    {
                        T stream = Queue[i];
                        TimeSpan length = stream.TotalTime;
                        if (value < length)
                        {
                            Index = i;
                            StreamCurrentTime = value;
                            return;
                        }

                        value -= length;
                    }
                }
            }
        }

        public virtual TimeSpan StreamTotalTime
        {
            get
            {
                return base.TotalTime;
            }
        }

        public virtual TimeSpan StreamCurrentTime
        {
            get
            {
                return base.CurrentTime;
            }
            set
            {
                base.CurrentTime = value;
            }
        }

        protected override T? Current
        {
            get
            {
                lock (Queue)
                {
                    return Index < Queue.Count ? Queue[Index] : Queue.Count > 0 ? Queue[0] : null;
                }
            }
        }

        public RepeatWaveStreamPlaylist()
        {
        }

        public RepeatWaveStreamPlaylist(params T[] items)
            : base(items)
        {
        }

        public RepeatWaveStreamPlaylist(IEnumerable<T> source)
            : base(source)
        {
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
                Next();
            }

            return read;
        }

        public override void Next(Int32 skip)
        {
            lock (Queue)
            {
                NextWithoutReset(skip);
                StreamPosition = 0;
            }
        }

        public void NextWithoutReset()
        {
            NextWithoutReset(1);
        }

        public virtual void NextWithoutReset(Int32 skip)
        {
            lock (Queue)
            {
                Index = (Index + skip).ToRange(0, Queue.Count, true);
            }
        }
    }
}