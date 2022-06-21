// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NetExtender.NAudio.Types.Common.Interfaces;

namespace NetExtender.NAudio.Types.Common
{
    public abstract class AudioSoundAbstraction : IAudioSound
    {
        protected FileInfo File { get; }
        
        public String Path
        {
            get
            {
                return File.FullName;
            }
        }
        
        public String Extension
        {
            get
            {
                return File.Extension;
            }
        }

        public Boolean Exists
        {
            get
            {
                return File.Exists;
            }
        }
        
        public Int64 Size
        {
            get
            {
                return File.Length;
            }
        }

        public TimeSpan Start { get; }
        public TimeSpan Stop { get; }

        public TimeSpan StartActive { get; init; }
        public TimeSpan StopActive { get; init; }

        public TimeSpan TotalStartActive
        {
            get
            {
                return Start + StartActive;
            }
        }
        
        public TimeSpan TotalStopActive
        {
            get
            {
                return Stop - StopActive;
            }
        }

        public TimeSpan Length
        {
            get
            {
                return Stop - Start;
            }
        }
        
        public abstract TimeSpan TotalTime { get; }

        public Single Volume { get; init; } = 1F;

        protected AudioSoundAbstraction(FileInfo file, TimeSpan start, TimeSpan stop)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
            
            if (start < default(TimeSpan))
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, null);
            }
            
            if (stop < default(TimeSpan))
            {
                throw new ArgumentOutOfRangeException(nameof(stop), stop, null);
            }
            
            if (stop != default && stop < start)
            {
                throw new ArgumentOutOfRangeException(nameof(stop), stop, "Stop must be greater than start");
            }

            // ReSharper disable once VirtualMemberCallInConstructor
            TimeSpan total = TotalTime;
            
            if (start > total)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "Start must be less than total time");
            }

            Start = start;
            Stop = TimeSpan.FromTicks(Math.Clamp(stop == default ? total.Ticks : stop.Ticks, start.Ticks, total.Ticks));
        }
    }
}