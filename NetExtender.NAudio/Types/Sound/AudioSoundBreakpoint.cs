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