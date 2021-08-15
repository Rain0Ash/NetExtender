// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NAudio.Wave;
using NetExtender.NAudio.Types.Playlist;
using NetExtender.NAudio.Types.Playlist.Interfaces;

namespace NetExtender.Utilities.NAudio
{
    public static class WavePlaylistUtilities
    {
        public static IWavePlaylist Playlist(this IWaveProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (provider is IWavePlaylist playlist)
            {
                return playlist;
            }

            return new WavePlaylist(provider);
        }
        
        public static IWavePlaylist Playlist(this IWaveProvider provider, params IWaveProvider[] items)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            IWavePlaylist playlist = new WavePlaylist(provider);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static IWavePlaylist Playlist(this IEnumerable<IWaveProvider> playlist)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new WavePlaylist(playlist);
        }
        
        public static IWavePlaylist<T> WithPlaylist<T>(this T provider) where T : class, IWaveProvider
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (provider is IWavePlaylist<T> playlist)
            {
                return playlist;
            }

            return new WavePlaylist<T>(provider);
        }
        
        public static IWavePlaylist<T> WithPlaylist<T>(this T provider, params T[] items) where T : class, IWaveProvider
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            IWavePlaylist<T> playlist = new WavePlaylist<T>(provider);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static IWavePlaylist<T> WithPlaylist<T>(this IEnumerable<T> playlist) where T : class, IWaveProvider
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new WavePlaylist<T>(playlist);
        }
    }
}