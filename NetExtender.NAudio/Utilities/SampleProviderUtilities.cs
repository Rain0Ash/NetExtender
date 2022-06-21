// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NetExtender.NAudio.Types.Common.Interfaces;
using NetExtender.NAudio.Types.Providers;

namespace NetExtender.Utilities.NAudio
{
    public static class SampleProviderUtilities
    {
        public static CacheSampleProvider Caching(this ISampleProvider source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new CacheSampleProvider(source);
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