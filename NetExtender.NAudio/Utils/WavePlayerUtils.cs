// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace NetExtender.Utils.NAudio
{
    public static class WavePlayerUtils
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
            
            SemaphoreSlim signal = new SemaphoreSlim(0, 1);

            void Release(Object? sender, StoppedEventArgs args)
            {
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
            
            player.Play(provider);
            await player.WaitAsync(token);
        }
    }
}