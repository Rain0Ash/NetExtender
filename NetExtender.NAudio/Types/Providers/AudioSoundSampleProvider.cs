// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NetExtender.NAudio.Types.Sound.Interfaces;

namespace NetExtender.NAudio.Types.Providers
{
    public sealed class AudioSoundSampleProvider : ISampleProvider
    {
        public IAudioSound Sound { get; }
        private ISampleProvider Provider { get; }

        private OffsetSampleProvider Offset { get; }
        private FadeOutSampleProvider FadeOut { get; }

        public WaveFormat WaveFormat
        {
            get
            {
                return FadeOut.WaveFormat;
            }
        }

        public AudioSoundSampleProvider(IAudioSound sound, ISampleProvider provider)
        {
            Sound = sound ?? throw new ArgumentNullException(nameof(sound));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Offset = new OffsetSampleProvider(Provider) { SkipOver = sound.Start, Take = sound.Length };
            FadeOut = new FadeOutSampleProvider(Offset, sound.Active.Start, sound.Length - sound.Active.Stop, sound.Active.Stop);
        }

        public Int32 Read(Single[] buffer, Int32 offset, Int32 count)
        {
            return FadeOut.Read(buffer, offset, count);
        }
    }
}