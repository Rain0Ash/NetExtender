// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Readers
{
    public class WaveStreamReaderProvider : IWaveProvider
    {
        protected Stream Stream { get; }

        public WaveFormat WaveFormat { get; }

        public WaveStreamReaderProvider(WaveStream stream)
            : this(stream ?? throw new ArgumentNullException(nameof(stream)), stream.WaveFormat)
        {
        }

        public WaveStreamReaderProvider(Stream stream, WaveFormat format)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            WaveFormat = format ?? throw new ArgumentNullException(nameof(format));
        }

        public Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Stream.Read(buffer, offset, count);
        }
    }
}