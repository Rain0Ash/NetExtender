// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NetExtender.NAudio.Types.Sound.Interfaces;

namespace NetExtender.NAudio.Types.Sound
{
    public abstract class AudioSoundSampleProviderAbstraction : AudioSoundAbstraction, IAudioSoundSampleProvider
    {
        public abstract WaveFormat WaveFormat { get; }
        public abstract Int32 Read(Single[] buffer, Int32 offset, Int32 count);
    }
}