// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Playlist.Interfaces
{
    public interface IWavePlaylist : IWavePlaylist<IWaveProvider>
    {
    }
    
    public interface IWavePlaylist<T> : IWaveProvider, IList<T> where T : class, IWaveProvider
    {
        public void AddRange(IEnumerable<T> source);
    }
}