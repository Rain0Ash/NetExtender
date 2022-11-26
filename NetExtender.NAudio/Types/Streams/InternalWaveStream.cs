// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Streams
{
    public abstract class InternalWaveStream : WaveStream
    {
        protected WaveStream Stream { get; }

        public override WaveFormat WaveFormat
        {
            get
            {
                return Stream.WaveFormat;
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

        public override TimeSpan TotalTime
        {
            get
            {
                return Stream.TotalTime;
            }
        }

        public override TimeSpan CurrentTime
        {
            get
            {
                return Stream.CurrentTime;
            }
            set
            {
                Stream.CurrentTime = value;
            }
        }

        protected InternalWaveStream(WaveStream stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Stream.Read(buffer, offset, count);
        }
    }
}