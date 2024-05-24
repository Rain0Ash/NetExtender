// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.NAudio.Types.Sound.Interfaces;

namespace NetExtender.NAudio.Types.Sound
{
    public abstract class AudioSoundAbstraction : AudioSoundInformation, IAudioSound
    {
        public abstract Boolean TryRead(Span<Byte> destination, out Int32 written);
        public abstract Byte[] Read();

        public Task<Byte[]> ReadAsync()
        {
            return ReadAsync(CancellationToken.None);
        }

        public virtual async Task<Byte[]> ReadAsync(CancellationToken token)
        {
            return await Task.Run(Read, token).ConfigureAwait(false);
        }
    }
}