// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Sound.Interfaces
{
    public interface IActiveSound : IDisposable
    {
        public IAudioSoundFile Sound { get; }
        public ISoundPlayer Player { get; }
        public PlaybackState State { get; }
        public Single? Volume { get; }

        public IActiveSound Play();
        public void Stop();
    }
}