// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using NetExtender.NAudio.Types.Streams;
using NetExtender.Types.Multithreading;

namespace NetExtender.Utilities.NAudio
{
    public static class WavePlayerUtilities
    {
        public static Task WaitAsync(this IWavePlayer player)
        {
            return WaitAsync(player, CancellationToken.None);
        }

        public static async Task WaitAsync(this IWavePlayer player, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }
            
            using MutexSlim signal = new MutexSlim();

            void Release(Object? sender, StoppedEventArgs args)
            {
                // ReSharper disable once AccessToDisposedClosure
                signal.Release();
            }

            player.PlaybackStopped += Release;

            try
            {
                await signal.WaitAsync(token);
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                player.PlaybackStopped -= Release;
            }
        }
        
        public static void Play(this IWavePlayer player, IWaveProvider provider)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            player.Init(provider);
            player.Play();
        }
        
        public static void Repeat(this IWavePlayer player, IWaveProvider provider)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            switch (provider)
            {
                case null:
                    throw new ArgumentNullException(nameof(provider));
                case WaveStream stream:
                    Repeat(player, stream);
                    break;
                default:
                    Repeat(player, new WaveProviderWaveStreamLazyReader(provider));
                    break;
            }
        }

        public static void Repeat(this IWavePlayer player, WaveStream stream)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            stream = stream as RepeatWaveStream ?? new RepeatWaveStream(stream);
            player.Play(stream);
        }

        public static Task PlayAsync(this IWavePlayer player)
        {
            return PlayAsync(player, CancellationToken.None);
        }
        
        public static async Task PlayAsync(this IWavePlayer player, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            player.Play();
            await player.WaitAsync(token);
        }

        public static Task PlayAsync(this IWavePlayer player, IWaveProvider provider)
        {
            return PlayAsync(player, provider, CancellationToken.None);
        }

        public static async Task PlayAsync(this IWavePlayer player, IWaveProvider provider, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            Play(player, provider);
            await player.WaitAsync(token);
        }
        
        public static Task RepeatAsync(this IWavePlayer player, IWaveProvider provider)
        {
            return RepeatAsync(player, provider, CancellationToken.None);
        }

        public static Task RepeatAsync(this IWavePlayer player, IWaveProvider provider, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            return provider switch
            {
                null => throw new ArgumentNullException(nameof(provider)),
                WaveStream stream => RepeatAsync(player, stream, token),
                _ => RepeatAsync(player, new WaveProviderWaveStreamLazyReader(provider), token)
            };
        }
        
        public static Task RepeatAsync(this IWavePlayer player, WaveStream stream)
        {
            return RepeatAsync(player, stream, CancellationToken.None);
        }

        public static Task RepeatAsync(this IWavePlayer player, WaveStream stream, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            stream = stream as RepeatWaveStream ?? new RepeatWaveStream(stream);
            return player.PlayAsync(stream, token);
        }
    }
}