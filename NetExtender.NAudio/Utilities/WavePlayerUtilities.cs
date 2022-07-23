// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using NetExtender.NAudio.Types.Sound.Interfaces;
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

        public static void Play(this IWavePlayer player, ISampleProvider provider)
        {
            Play(player, provider, false);
        }

        public static void Play(this IWavePlayer player, ISampleProvider provider, Boolean bit16)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            player.Init(provider, bit16);
            player.Play();
        }
        
        public static void Play(this IWavePlayer player, ISampleProvider provider, IAudioSound sound)
        {
            Play(player, provider, sound, false);
        }

        public static void Play(this IWavePlayer player, ISampleProvider provider, IAudioSound sound, Boolean bit16)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (sound is null)
            {
                throw new ArgumentNullException(nameof(sound));
            }
            
            Play(player, provider.Sound(sound), bit16);
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
        
        public static void Repeat(this IWavePlayer player, ISampleProvider provider)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            Play(player, provider.Repeat());
        }
        
        public static void Repeat(this IWavePlayer player, ISampleProvider provider, Boolean bit16)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            Play(player, provider.Repeat(bit16));
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
        
        public static Task PlayAsync(this IWavePlayer player, ISampleProvider provider)
        {
            return PlayAsync(player, provider, CancellationToken.None);
        }

        public static async Task PlayAsync(this IWavePlayer player, ISampleProvider provider, CancellationToken token)
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
        
        public static Task PlayAsync(this IWavePlayer player, ISampleProvider provider, Boolean bit16)
        {
            return PlayAsync(player, provider, bit16, CancellationToken.None);
        }

        public static async Task PlayAsync(this IWavePlayer player, ISampleProvider provider, Boolean bit16, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            Play(player, provider, bit16);
            await player.WaitAsync(token);
        }

        public static Task PlayAsync(this IWavePlayer player, ISampleProvider provider, IAudioSound sound)
        {
            return PlayAsync(player, provider, sound, CancellationToken.None);
        }

        public static Task PlayAsync(this IWavePlayer player, ISampleProvider provider, IAudioSound sound, CancellationToken token)
        {
            return PlayAsync(player, provider, sound, false, token);
        }

        public static Task PlayAsync(this IWavePlayer player, ISampleProvider provider, IAudioSound sound, Boolean bit16)
        {
            return PlayAsync(player, provider, sound, bit16, CancellationToken.None);
        }

        public static Task PlayAsync(this IWavePlayer player, ISampleProvider provider, IAudioSound sound, Boolean bit16, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (sound is null)
            {
                throw new ArgumentNullException(nameof(sound));
            }
            
            return PlayAsync(player, provider.Sound(sound), bit16, token);
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
        
        public static Task RepeatAsync(this IWavePlayer player, ISampleProvider provider)
        {
            return RepeatAsync(player, provider, CancellationToken.None);
        }

        public static Task RepeatAsync(this IWavePlayer player, ISampleProvider provider, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return PlayAsync(player, provider.Repeat(), token);
        }
        
        public static Task RepeatAsync(this IWavePlayer player, ISampleProvider provider, Boolean bit16)
        {
            return RepeatAsync(player, provider, bit16, CancellationToken.None);
        }

        public static Task RepeatAsync(this IWavePlayer player, ISampleProvider provider, Boolean bit16, CancellationToken token)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return PlayAsync(player, provider.Repeat(bit16), token);
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