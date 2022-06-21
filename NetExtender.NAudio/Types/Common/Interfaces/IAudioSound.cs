// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.NAudio.Types.Common.Interfaces
{
    public interface IAudioSound
    {
        public String Path { get; }
        public String Extension { get; }
        public Boolean Exists { get; }
        public Int64 Size { get; }
        public TimeSpan Start { get; }
        public TimeSpan Stop { get; }
        public TimeSpan StartActive { get; }
        public TimeSpan StopActive { get; }
        public TimeSpan TotalStartActive { get; }
        public TimeSpan TotalStopActive { get; }
        public TimeSpan Length { get; }
        public TimeSpan TotalTime { get; }
        public Single Volume { get; }
    }
}