// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Reactive.Concurrency;
using System.Threading;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Streams
{
    public class BandwidthStream : BandwidthStream<Stream>
    {
        public BandwidthStream(Stream stream, Int32 speed, InformationSize size)
            : base(stream, speed, size)
        {
        }

        public BandwidthStream(Stream stream, Int32 speed, InformationSize size, IScheduler scheduler)
            : base(stream, speed, size, scheduler)
        {
        }

        public BandwidthStream(Stream stream, UInt64 speed, InformationSize size)
            : base(stream, speed, size)
        {
        }

        public BandwidthStream(Stream stream, UInt64 speed = UInt64.MaxValue)
            : base(stream, speed)
        {
        }

        public BandwidthStream(Stream stream, UInt64 speed, IScheduler scheduler)
            : base(stream, speed, scheduler)
        {
        }
    }
    
    public class BandwidthStream<T> : Stream where T : Stream
    {
        private static IScheduler DefaultScheduler
        {
            get
            {
                return System.Reactive.Concurrency.Scheduler.Immediate;
            }
        }
        
        public T BaseStream { get; }

        private IScheduler Scheduler { get; }
        private IStopwatch Stopwatch { get; }
        public UInt64 MaximumSpeed { get; set; }
        private UInt64 Processed { get; set; }
        
        private AutoResetEvent Wait { get; }
        
        public override Boolean CanRead
        {
            get
            {
                return BaseStream.CanRead;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                return BaseStream.CanSeek;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return BaseStream.CanWrite;
            }
        }

        public override Boolean CanTimeout
        {
            get
            {
                return BaseStream.CanTimeout;
            }
        }

        public override Int64 Length
        {
            get
            {
                return BaseStream.Length;
            }
        }

        public override Int64 Position
        {
            get
            {
                return BaseStream.Position;
            }
            set
            {
                BaseStream.Position = value;
            }
        }
        
        public BandwidthStream(T stream, Int32 speed, InformationSize size)
            : this(stream, speed, size, DefaultScheduler)
        {
        }
        
        public BandwidthStream(T stream, Int32 speed, InformationSize size, IScheduler scheduler)
            : this(stream, size.ConvertToBytes(speed.IsPositive() ? speed : throw new ArgumentOutOfRangeException(nameof(speed))), scheduler)
        {
        }
        
        public BandwidthStream(T stream, UInt64 speed, InformationSize size)
            : this(stream, size.ConvertToBytes(speed))
        {
        }

        public BandwidthStream(T stream, UInt64 speed = UInt64.MaxValue)
            : this(stream, speed, DefaultScheduler)
        {
        }
        
        public BandwidthStream(T stream, UInt64 speed, IScheduler scheduler)
        {
            BaseStream = stream ?? throw new ArgumentNullException(nameof(stream));
            MaximumSpeed = speed;
            Scheduler = scheduler;
            Wait = new AutoResetEvent(false);
            Stopwatch = scheduler.StartStopwatch();
        }

        protected void Throttle(Int32 bytes)
        {
            Throttle((UInt64) bytes);
        }

        protected virtual void Throttle(UInt64 bytes)
        {
            Processed += bytes;
            
            TimeSpan target = TimeSpan.FromSeconds((Double) Processed / MaximumSpeed);
            TimeSpan actual = Stopwatch.Elapsed;
            TimeSpan sleep = target - actual;
            
            if (sleep <= TimeSpan.Zero)
            {
                return;
            }

            Scheduler.Sleep(sleep).GetAwaiter().OnCompleted(() => Wait.Set());
            Wait.WaitOne();
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public override void SetLength(Int64 value)
        {
            BaseStream.SetLength(value);
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            Int32 read = BaseStream.Read(buffer);
            Throttle(read);
            return read;
        }
        
        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Int32 read = BaseStream.Read(buffer, offset, count);
            Throttle(read);
            return read;
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            Throttle(buffer.Length);
            BaseStream.Write(buffer);
        }
        
        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            Throttle(count);
            BaseStream.Write(buffer, offset, count);
        }

        public override void Flush()
        {
            BaseStream.Flush();
        }

        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                Wait.Dispose();
            }
        }
    }
}