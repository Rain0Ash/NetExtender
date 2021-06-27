// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Playlist.Interfaces
{
    public interface IWaveStreamPlaylist : IWaveStreamPlaylist<WaveStream>
    {
    }
    
    public interface IWaveStreamPlaylist<T> : IWavePlaylist<T> where T : WaveStream
    {
        public Int32 Index { get; set; }

        public void Next();
        public void Next(Int32 skip);
        public void RemoveRange(Int32 index, Int32 count);
        public Int32 RemoveAll(Predicate<T> match);
    }
}