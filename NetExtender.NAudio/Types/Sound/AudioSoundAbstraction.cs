// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.NAudio.Types.Sound.Interfaces;

namespace NetExtender.NAudio.Types.Sound
{
    public abstract class AudioSoundAbstraction : IAudioSound
    {
        public abstract Int64 Size { get; }
        public abstract TimeSpan Start { get; }
        public abstract TimeSpan Stop { get; }

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

        public abstract Boolean TryRead(Span<Byte> destination, out Int32 written);
        public abstract Byte[] Read();

        public Task<Byte[]> ReadAsync()
        {
            return ReadAsync(CancellationToken.None);
        }

        public virtual async Task<Byte[]> ReadAsync(CancellationToken token)
        {
            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            return await Task.Run(Read, token).ConfigureAwait(false);
        }
    }
}