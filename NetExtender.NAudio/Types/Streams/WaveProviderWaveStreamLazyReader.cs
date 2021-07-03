// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Streams
{
    public class WaveProviderWaveStreamLazyReader : WaveProviderWaveStreamReader
    {
        protected Int64 MaxLength { get; set; }
        
        public override Int64 Length
        {
            get
            {
                return MaxLength;
            }
        }

        public virtual Int64 FullLength
        {
            get
            {
                return Stream.Length;
            }
        }

        public WaveProviderWaveStreamLazyReader(IWaveProvider provider)
            : base(provider)
        {
        }
        
        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Int32 read = Stream.Read(buffer, offset, count);
            MaxLength = Math.Max(Position + 1, MaxLength);

            return read;
        }
    }
}