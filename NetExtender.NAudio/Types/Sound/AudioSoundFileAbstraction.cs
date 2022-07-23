// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.NAudio.Types.Sound.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.NAudio.Types.Sound
{
    public abstract class AudioSoundFileAbstraction : AudioSoundSampleProviderAbstraction, IAudioSoundFile
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
        
        public override Int64 Size
        {
            get
            {
                return File.Length;
            }
        }

        public override TimeSpan Start { get; }
        public override TimeSpan Stop { get; }

        protected AudioSoundFileAbstraction(FileInfo file, TimeSpan start, TimeSpan stop)
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

        public override Boolean TryRead(Span<Byte> destination, out Int32 written)
        {
            if (destination.Length == 0)
            {
                written = 0;
                return false;
            }

            try
            {
                using FileStream stream = File.OpenRead();
                written = stream.Read(destination);
                return written > 0;
            }
            catch (Exception)
            {
                written = 0;
                return false;
            }
        }

        public override Byte[] Read()
        {
            using FileStream stream = File.OpenRead();
            return stream.ReadAsByteArray();
        }

        public override async Task<Byte[]> ReadAsync(CancellationToken token)
        {
            await using FileStream stream = File.OpenRead();
            return await stream.ReadAsByteArrayAsync(token);
        }
    }
}