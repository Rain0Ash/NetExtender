// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NAudio.Wave;
using NetExtender.Types.Streams;

namespace NetExtender.NAudio.Types.Streams
{
    public class WaveProviderWaveStreamReader : WaveStream
    {
        protected IWaveProvider Provider { get; }
        protected Stream Stream { get; }

        public override WaveFormat WaveFormat
        {
            get
            {
                return Provider.WaveFormat;
            }
        }

        public override Int64 Length
        {
            get
            {
                return Stream.Length;
            }
        }

        public override Int64 Position
        {
            get
            {
                return Stream.Position;
            }
            set
            {
                Stream.Position = value;
            }
        }

        public WaveProviderWaveStreamReader(IWaveProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Stream = (Stream?) (provider as WaveStream) ?? new SeekableStream(new WaveProviderStreamReader(provider));
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Stream.Read(buffer, offset, count);
        }
    }
}