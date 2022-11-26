// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NetExtender.NAudio.Types.Providers;
using NetExtender.NAudio.Types.Sound.Interfaces;
using NetExtender.NAudio.Types.Streams;

namespace NetExtender.Utilities.NAudio
{
    public static class SampleProviderUtilities
    {
        public static RepeatWaveStream Repeat(this ISampleProvider source)
        {
            return Repeat(source, false);
        }

        public static RepeatWaveStream Repeat(this ISampleProvider source, Boolean bit16)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new RepeatWaveStream(new WaveProviderWaveStreamLazyReader(bit16 ? new SampleToWaveProvider16(source) : new SampleToWaveProvider(source)));
        }

        public static AudioSoundSampleProvider Sound(this ISampleProvider source, IAudioSound sound)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (sound is null)
            {
                throw new ArgumentNullException(nameof(sound));
            }

            return new AudioSoundSampleProvider(sound, source);
        }
    }
}