// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Playlist.Interfaces
{
    public interface IReadOnlyWaveStreamPlaylist : IReadOnlyWaveStreamPlaylist<WaveStream>
    {
    }

    public interface IReadOnlyWaveStreamPlaylist<out T> : IReadOnlyWavePlaylist<T> where T : WaveStream
    {
        public Int32 Index { get; }
    }
}