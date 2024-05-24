// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.NAudio.Types.Sound.Interfaces;

namespace NetExtender.Utilities.NAudio
{
    public enum AudioSoundType
    {
        Unknown,
        Wave,
        Mp3,
        Aiff,
        Flac
    }

    //TODO:
    public static class AudioSoundUtilities
    {
        public static IEnumerable<IAudioSound> Split(this IAudioSound sound)
        {
            if (sound is null)
            {
                throw new ArgumentNullException(nameof(sound));
            }

            yield break;
        }
    }
}