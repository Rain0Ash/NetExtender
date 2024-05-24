// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.NAudio.Types.Sound.Interfaces
{
    public interface IAudioSound : IAudioSoundInformation
    {
        public Boolean TryRead(Span<Byte> destination, out Int32 written);
        public Byte[] Read();
        public Task<Byte[]> ReadAsync();
        public Task<Byte[]> ReadAsync(CancellationToken token);
    }
}