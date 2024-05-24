// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NetExtender.Utilities.Types;

namespace NetExtender.NAudio.Types.Providers
{
    public sealed class FadeOutSampleProvider : ISampleProvider
    {
        private enum FadeState
        {
            Standard,
            FadingIn,
            FadingOut,
            Silence
        }

        private const Boolean Silence = false;

        private Object SyncRoot { get; } = ConcurrentUtilities.SyncRoot;
        
        private ISampleProvider Provider { get; }
        private Int32 FadePosition { get; set; }
        private Int32 FadeCount { get; set; }
        private Int32 FadeDelaySamples { get; set; }
        private Int32 FadeDelayPosition { get; set; }
        private FadeState State { get; set; }

        public TimeSpan FadeIn { get; }
        public TimeSpan FadeOutDelay { get; }
        public TimeSpan FadeOut { get; }

        public WaveFormat WaveFormat
        {
            get
            {
                return Provider.WaveFormat;
            }
        }

        public FadeOutSampleProvider(ISampleProvider provider, TimeSpan fadeout)
            : this(provider, fadeout, Silence)
        {
        }

        public FadeOutSampleProvider(ISampleProvider provider, TimeSpan fadeout, Boolean silent)
            : this(provider, default, fadeout, silent)
        {
        }

        public FadeOutSampleProvider(ISampleProvider provider, TimeSpan delay, TimeSpan fadeout)
            : this(provider, delay, fadeout, Silence)
        {
        }

        public FadeOutSampleProvider(ISampleProvider provider, TimeSpan delay, TimeSpan fadeout, Boolean silent)
            : this(provider, default, delay, fadeout, silent)
        {
        }

        public FadeOutSampleProvider(ISampleProvider provider, TimeSpan fadein, TimeSpan delay, TimeSpan fadeout)
            : this(provider, fadein, delay, fadeout, Silence)
        {
        }

        public FadeOutSampleProvider(ISampleProvider provider, TimeSpan fadein, TimeSpan delay, TimeSpan fadeout, Boolean silent)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            State = silent ? FadeState.Silence : FadeState.Standard;

            FadeIn = fadein >= default(TimeSpan) ? fadein : default;
            FadeOut = fadeout >= default(TimeSpan) ? fadeout : default;
            FadeOutDelay = FadeOut > default(TimeSpan) && delay - FadeIn >= default(TimeSpan) ? delay - FadeIn : default;

            if (FadeIn > default(TimeSpan))
            {
                BeginFadeIn(FadeIn);
            }
            else if (FadeOut > default(TimeSpan))
            {
                BeginFadeOut(FadeOutDelay, FadeOut);
            }
        }

        private void BeginFadeIn(TimeSpan duration)
        {
            lock (SyncRoot)
            {
                FadePosition = 0;
                FadeCount = (Int32) (duration.TotalSeconds * Provider.WaveFormat.SampleRate);
                State = FadeState.FadingIn;
            }
        }

        private void BeginFadeOut(TimeSpan duration)
        {
            BeginFadeOut(default, duration);
        }

        private void BeginFadeOut(TimeSpan delay, TimeSpan duration)
        {
            lock (SyncRoot)
            {
                FadePosition = 0;
                FadeCount = (Int32) (duration.TotalSeconds * Provider.WaveFormat.SampleRate);
                FadeDelaySamples = (Int32) (delay.TotalSeconds * Provider.WaveFormat.SampleRate);
                FadeDelayPosition = 0;
            }
        }

        private void Action(Single[] buffer, Int32 offset, Int32 count, Int32 read)
        {
            switch (State)
            {
                case FadeState.Standard:
                    break;
                case FadeState.FadingIn:
                    FadeInHandler(buffer, offset, read);
                    break;
                case FadeState.FadingOut:
                    FadeOutHandler(buffer, offset, read);
                    break;
                case FadeState.Silence:
                    ClearBuffer(buffer, offset, count);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(State), State, null);
            }
        }

        public Int32 Read(Single[] buffer, Int32 offset, Int32 count)
        {
            Int32 read = Provider.Read(buffer, offset, count);

            lock (SyncRoot)
            {
                if (FadeDelaySamples <= 0)
                {
                    Action(buffer, offset, count, read);
                    return read;
                }

                Int32 old = FadeDelayPosition;
                FadeDelayPosition += read / WaveFormat.Channels;

                if (FadeDelayPosition <= FadeDelaySamples)
                {
                    Action(buffer, offset, count, read);
                    return read;
                }

                Int32 normal = (FadeDelaySamples - old) * WaveFormat.Channels;
                Int32 fadeout = (FadeDelayPosition - FadeDelaySamples) * WaveFormat.Channels;
                FadeOutHandler(buffer, offset + normal, fadeout);

                FadeDelaySamples = 0;
                State = FadeState.FadingOut;
                return read;
            }
        }

        private void FadeInHandler(Span<Single> buffer, Int32 offset, Int32 read)
        {
            Int32 sample = 0;
            while (sample < read)
            {
                Single multiplier = FadePosition / (Single) FadeCount;
                for (Int32 channel = 0; channel < Provider.WaveFormat.Channels; channel++)
                {
                    buffer[offset + sample++] *= multiplier;
                }

                if (++FadePosition <= FadeCount)
                {
                    continue;
                }

                State = FadeState.Standard;

                if (FadeOut > default(TimeSpan))
                {
                    BeginFadeOut(FadeOutDelay, FadeOut);
                }

                break;
            }
        }

        private void FadeOutHandler(Span<Single> buffer, Int32 offset, Int32 read)
        {
            Int32 sample = 0;

            while (sample < read)
            {
                Single multiplier = 1F - FadePosition / (Single) FadeCount;
                for (Int32 channel = 0; channel < Provider.WaveFormat.Channels; channel++)
                {
                    buffer[offset + sample++] *= multiplier;
                }

                if (++FadePosition <= FadeCount)
                {
                    continue;
                }

                State = FadeState.Silence;
                ClearBuffer(buffer, sample + offset, read - sample);
                break;
            }
        }

        private static void ClearBuffer(Span<Single> buffer, Int32 offset, Int32 count)
        {
            for (Int32 i = 0; i < count; i++)
            {
                buffer[i + offset] = 0;
            }
        }
    }
}