// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NAudio.Wave;
using NetExtender.NAudio.Types.Playlist;
using NetExtender.NAudio.Types.Playlist.Interfaces;

namespace NetExtender.Utils.NAudio
{
    public static class WaveStreamPlaylistUtils
    {
        public static IWaveStreamPlaylist Playlist(this WaveStream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (stream is IWaveStreamPlaylist playlist)
            {
                return playlist;
            }

            return new WaveStreamPlaylist(stream);
        }
        
        public static IWaveStreamPlaylist Playlist(this WaveStream stream, params WaveStream[] items)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IWaveStreamPlaylist playlist = new WaveStreamPlaylist(stream);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static IWaveStreamPlaylist Playlist(this IEnumerable<WaveStream> playlist)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new WaveStreamPlaylist(playlist);
        }
        
        public static WaveStream AsPlaylistStream(this WaveStream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new WaveStreamPlaylist(stream);
        }
        
        public static WaveStream AsPlaylistStream(this WaveStream stream, params WaveStream[] items)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            WaveStreamPlaylist playlist = new WaveStreamPlaylist(stream);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static WaveStream AsPlaylistStream(this IEnumerable<WaveStream> playlist)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new WaveStreamPlaylist(playlist);
        }
        
        public static WaveStreamPlaylist<T> AsPlaylistStream<T>(this T stream) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new WaveStreamPlaylist<T>(stream);
        }
        
        public static WaveStreamPlaylist<T> AsPlaylistStream<T>(this T stream, params T[] items) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            WaveStreamPlaylist<T> playlist = new WaveStreamPlaylist<T>(stream);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static WaveStreamPlaylist<T> AsPlaylistStream<T>(this IEnumerable<T> playlist) where T : WaveStream
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new WaveStreamPlaylist<T>(playlist);
        }
        
        public static IWaveStreamPlaylist<T> WithPlaylist<T>(this T stream) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (stream is IWaveStreamPlaylist<T> playlist)
            {
                return playlist;
            }

            return new WaveStreamPlaylist<T>(stream);
        }
        
        public static IWaveStreamPlaylist<T> WithPlaylist<T>(this T stream, params T[] items) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IWaveStreamPlaylist<T> playlist = new WaveStreamPlaylist<T>(stream);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static IWaveStreamPlaylist<T> WithPlaylist<T>(this IEnumerable<T> playlist) where T : WaveStream
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new WaveStreamPlaylist<T>(playlist);
        }
        
        public static IWaveStreamPlaylist CyclicPlaylist(this WaveStream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new CyclicWaveStreamPlaylist(stream);
        }
        
        public static IWaveStreamPlaylist CyclicPlaylist(this WaveStream stream, params WaveStream[] items)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IWaveStreamPlaylist playlist = new CyclicWaveStreamPlaylist(stream);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static IWaveStreamPlaylist CyclicPlaylist(this IEnumerable<WaveStream> playlist)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new CyclicWaveStreamPlaylist(playlist);
        }
        
        public static CyclicWaveStreamPlaylist AsCyclicPlaylistStream(this WaveStream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new CyclicWaveStreamPlaylist(stream);
        }
        
        public static CyclicWaveStreamPlaylist AsCyclicPlaylistStream(this WaveStream stream, params WaveStream[] items)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            CyclicWaveStreamPlaylist playlist = new CyclicWaveStreamPlaylist(stream);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static CyclicWaveStreamPlaylist AsCyclicPlaylistStream(this IEnumerable<WaveStream> playlist)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new CyclicWaveStreamPlaylist(playlist);
        }
        
        public static CyclicWaveStreamPlaylist<T> AsCyclicPlaylistStream<T>(this T stream) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new CyclicWaveStreamPlaylist<T>(stream);
        }
        
        public static CyclicWaveStreamPlaylist<T> AsCyclicPlaylistStream<T>(this T stream, params T[] items) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            CyclicWaveStreamPlaylist<T> playlist = new CyclicWaveStreamPlaylist<T>(stream);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static CyclicWaveStreamPlaylist<T> AsCyclicPlaylistStream<T>(this IEnumerable<T> playlist) where T : WaveStream
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new CyclicWaveStreamPlaylist<T>(playlist);
        }
        
        public static IWaveStreamPlaylist<T> WithCyclicPlaylist<T>(this T stream) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new CyclicWaveStreamPlaylist<T>(stream);
        }
        
        public static IWaveStreamPlaylist<T> WithCyclicPlaylist<T>(this T stream, params T[] items) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IWaveStreamPlaylist<T> playlist = new CyclicWaveStreamPlaylist<T>(stream);
            playlist.AddRange(items);
            return playlist;
        }
        
        public static IWaveStreamPlaylist<T> WithCyclicPlaylist<T>(this IEnumerable<T> playlist) where T : WaveStream
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new CyclicWaveStreamPlaylist<T>(playlist);
        }
    }
}