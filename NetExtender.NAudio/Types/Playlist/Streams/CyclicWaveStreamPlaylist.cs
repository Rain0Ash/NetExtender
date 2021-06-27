// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using NetExtender.NAudio.Types.Playlist.Interfaces;
using NetExtender.Utils.Numerics;

namespace NetExtender.NAudio.Types.Playlist
{
    public class CyclicWaveStreamPlaylist : CyclicWaveStreamPlaylist<WaveStream>, IReadOnlyWaveStreamPlaylist, IWaveStreamPlaylist
    {
        public CyclicWaveStreamPlaylist()
        {
        }

        public CyclicWaveStreamPlaylist(params WaveStream[] items)
            : base(items)
        {
        }

        public CyclicWaveStreamPlaylist(IEnumerable<WaveStream> items)
            : base(items)
        {
        }
    }
    
    public class CyclicWaveStreamPlaylist<T> : WaveStreamPlaylist<T> where T : WaveStream
    {
        private Int32 _index;

        public override Int32 Index
        {
            get
            {
                lock (Queue)
                {
                    return _index.ToRange(0, Queue.Count);
                }
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
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
                    throw new ArgumentOutOfRangeException(nameof(value));
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
        
        public CyclicWaveStreamPlaylist()
        {
        }

        public CyclicWaveStreamPlaylist(params T[] items)
            : base(items)
        {
        }

        public CyclicWaveStreamPlaylist(IEnumerable<T> items)
            : base(items)
        {
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
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
                if (read > 0)
                {
                    continue;
                }

                Next();

            } while (read <= 0);

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
                Index += skip;
            }
        }
    }
}