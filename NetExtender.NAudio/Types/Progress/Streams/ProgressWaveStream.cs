// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NetExtender.NAudio.Types.Streams;

namespace NetExtender.NAudio.Types.Progress
{
    public class ProgressWaveStream : InternalWaveStream
    {
        protected class ProgressWaveStreamWithoutLength : InternalWaveStream
        {
            protected Action<Int64> Callback { get; }

            public ProgressWaveStreamWithoutLength(WaveStream stream, Action<Int64> callback)
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

        protected class ProgressWaveStreamWithLength : InternalWaveStream
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

        protected class TimeProgressWaveStream : InternalWaveStream
        {
            protected Action<TimeSpan> Callback { get; }

            public TimeProgressWaveStream(WaveStream stream, Action<TimeSpan> callback)
                : base(stream)
            {
                Callback = callback ?? throw new ArgumentNullException(nameof(callback));
            }

            public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
            {
                Callback.Invoke(CurrentTime);
                return Stream.Read(buffer, offset, count);
            }
        }

        protected class TimeProgressWaveStreamWithLength : InternalWaveStream
        {
            protected Action<TimeSpan, TimeSpan> Callback { get; }

            public TimeProgressWaveStreamWithLength(WaveStream stream, Action<TimeSpan, TimeSpan> callback)
                : base(stream)
            {
                Callback = callback ?? throw new ArgumentNullException(nameof(callback));
            }

            public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
            {
                Callback.Invoke(CurrentTime, TotalTime);
                return Stream.Read(buffer, offset, count);
            }
        }

        public ProgressWaveStream(WaveStream stream, Action<Int64> callback)
            : base(new ProgressWaveStreamWithoutLength(stream ?? throw new ArgumentNullException(nameof(stream)), callback ?? throw new ArgumentNullException(nameof(callback))))
        {
        }

        public ProgressWaveStream(WaveStream stream, Action<Int64, Int64> callback)
            : base(new ProgressWaveStreamWithLength(stream ?? throw new ArgumentNullException(nameof(stream)), callback ?? throw new ArgumentNullException(nameof(callback))))
        {
        }

        public ProgressWaveStream(WaveStream stream, Action<TimeSpan> callback)
            : base(new TimeProgressWaveStream(stream ?? throw new ArgumentNullException(nameof(stream)), callback ?? throw new ArgumentNullException(nameof(callback))))
        {
        }

        public ProgressWaveStream(WaveStream stream, Action<TimeSpan, TimeSpan> callback)
            : base(new TimeProgressWaveStreamWithLength(stream ?? throw new ArgumentNullException(nameof(stream)), callback ?? throw new ArgumentNullException(nameof(callback))))
        {
        }
    }
}