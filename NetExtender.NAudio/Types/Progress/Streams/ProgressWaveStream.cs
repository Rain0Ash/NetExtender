// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NetExtender.NAudio.Types.Streams;

namespace NetExtender.NAudio.Types.Progress
{
    public class ProgressWaveStream : InternalWaveStream
    {
        protected Action<Int64> Callback { get; }
        
        public ProgressWaveStream(WaveStream stream, Action<Int64> callback)
            : base(stream)
        {
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Int64 position = Position;
            Int64 length = Length;

            Callback.Invoke(position <= length ? position : length);
            
            return Stream.Read(buffer, offset, count);
        }
    }
    
    public class ProgressWaveStreamWithLength : InternalWaveStream
    {
        protected Action<Int64, Int64> Callback { get; }
        
        public ProgressWaveStreamWithLength(WaveStream stream, Action<Int64, Int64> callback)
            : base(stream)
        {
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Int64 position = Position;
            Int64 length = Length;

            Callback.Invoke(position <= length ? position : length, length);
            
            return Stream.Read(buffer, offset, count);
        }
    }
}