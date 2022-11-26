// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NAudio.Wave;

namespace NetExtender.NAudio.Types.Streams
{
    public class WaveProviderStreamReader : Stream
    {
        protected IWaveProvider Provider { get; }

        public override Boolean CanRead
        {
            get
            {
                return true;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                return false;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return false;
            }
        }

        public override Int64 Length
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override Int64 Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public WaveProviderStreamReader(IWaveProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Provider.Read(buffer, offset, count);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(Int64 value)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }
    }
}