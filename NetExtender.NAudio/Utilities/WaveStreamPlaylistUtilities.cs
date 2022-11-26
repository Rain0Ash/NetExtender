// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NAudio.Wave;
using NetExtender.NAudio.Types.Playlist;
using NetExtender.NAudio.Types.Playlist.Interfaces;

namespace NetExtender.Utilities.NAudio
{
    public static class WaveStreamPlaylistUtilities
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

        public static IWaveStreamPlaylist RepeatPlaylist(this WaveStream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new RepeatWaveStreamPlaylist(stream);
        }

        public static IWaveStreamPlaylist RepeatPlaylist(this WaveStream stream, params WaveStream[] items)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IWaveStreamPlaylist playlist = new RepeatWaveStreamPlaylist(stream);
            playlist.AddRange(items);
            return playlist;
        }

        public static IWaveStreamPlaylist RepeatPlaylist(this IEnumerable<WaveStream> playlist)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new RepeatWaveStreamPlaylist(playlist);
        }

        public static RepeatWaveStreamPlaylist AsRepeatPlaylistStream(this WaveStream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new RepeatWaveStreamPlaylist(stream);
        }

        public static RepeatWaveStreamPlaylist AsRepeatPlaylistStream(this WaveStream stream, params WaveStream[] items)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            RepeatWaveStreamPlaylist playlist = new RepeatWaveStreamPlaylist(stream);
            playlist.AddRange(items);
            return playlist;
        }

        public static RepeatWaveStreamPlaylist AsRepeatPlaylistStream(this IEnumerable<WaveStream> playlist)
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new RepeatWaveStreamPlaylist(playlist);
        }

        public static RepeatWaveStreamPlaylist<T> AsRepeatPlaylistStream<T>(this T stream) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new RepeatWaveStreamPlaylist<T>(stream);
        }

        public static RepeatWaveStreamPlaylist<T> AsRepeatPlaylistStream<T>(this T stream, params T[] items) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            RepeatWaveStreamPlaylist<T> playlist = new RepeatWaveStreamPlaylist<T>(stream);
            playlist.AddRange(items);
            return playlist;
        }

        public static RepeatWaveStreamPlaylist<T> AsRepeatPlaylistStream<T>(this IEnumerable<T> playlist) where T : WaveStream
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new RepeatWaveStreamPlaylist<T>(playlist);
        }

        public static IWaveStreamPlaylist<T> WithRepeatPlaylist<T>(this T stream) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return new RepeatWaveStreamPlaylist<T>(stream);
        }

        public static IWaveStreamPlaylist<T> WithRepeatPlaylist<T>(this T stream, params T[] items) where T : WaveStream
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IWaveStreamPlaylist<T> playlist = new RepeatWaveStreamPlaylist<T>(stream);
            playlist.AddRange(items);
            return playlist;
        }

        public static IWaveStreamPlaylist<T> WithRepeatPlaylist<T>(this IEnumerable<T> playlist) where T : WaveStream
        {
            if (playlist is null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            return new RepeatWaveStreamPlaylist<T>(playlist);
        }
    }
}