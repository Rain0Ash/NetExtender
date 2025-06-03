// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.NAudio.Types.Sound.Interfaces
{
    public interface IAudioSoundInformation
    {
        public Int64? Size { get; }
        public Boolean IsVirtual { get; }
        public TimeSpan Start { get; }
        public TimeSpan Stop { get; }
        public AudioSoundInformation.Info Active { get; init; }
        public AudioSoundInformation.TotalInfo Total { get; }
        public TimeSpan Length { get; }
        public TimeSpan TotalTime { get; }
        public Single? Volume { get; }
    }
}