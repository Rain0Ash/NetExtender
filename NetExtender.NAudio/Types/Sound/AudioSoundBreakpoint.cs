// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.NAudio.Types.Sound.Interfaces;

namespace NetExtender.NAudio.Types.Sound
{
    //TODO:
    public class AudioSoundBreakpoint : AudioSoundInformation
    {
        public IAudioSoundInformation Audio { get; }

        public override Int64? Size
        {
            get
            {
                return null;
            }
        }

        public override Boolean IsVirtual
        {
            get
            {
                return true;
            }
        }

        protected override Info Information { get; }

        public AudioSoundBreakpoint(IAudioSoundInformation audio)
        {
            Audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }
    }
}