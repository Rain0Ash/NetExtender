// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.NAudio.Types.Sound.Interfaces
{
    public interface IAudioSoundFile : IAudioSound
    {
        public String Path { get; }
        public String Extension { get; }
        public Boolean Exists { get; }
    }
}