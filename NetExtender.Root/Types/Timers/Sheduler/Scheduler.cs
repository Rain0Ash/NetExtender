using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.Exceptions;

namespace NetExtender.Types.Timers
{
    public partial class Scheduler
    {
        private enum State
        {
            Initialize,
            Start,
            Shutdown
        }

        private volatile Int32 _state;
        private ConcurrentQueue<Timeout> Timeouts { get; } = new ConcurrentQueue<Timeout>();
        internal ConcurrentQueue<Timeout> Cancel { get; } = new ConcurrentQueue<Timeout>();
        private ISet<Timeout> Unprocess { get; } = new HashSet<Timeout>();
        private Bucket[] Wheel { get; }
        private Int32 Mask { get; }
        private Int64 Tick { get; set; }
        private Int64 Interval { get; }

        private Int64 StartTime { get; set; }
        private ManualResetEvent StartTimeEvent { get; } = new ManualResetEvent(false);
        private Int64 _last;

        private Int64 _pending;
        private Int64 PendingTimeouts
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _pending;
            }
        }

        private Int64 MaximumPendingTimeouts { get; }
        private Thread Worker { get; }
        private Int64 Base { get; } = DateTime.UtcNow.Ticks / 10000;

        private Int64 Time
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return unchecked(DateTime.UtcNow.Ticks / 10000 - Base);
            }
        }

        public Scheduler()
            : this(TimeSpan.FromMilliseconds(100), 512, 0)
        {
        }

        public Scheduler(TimeSpan interval, Int32 wheel, Int64 pending)
        {
            if (interval <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(interval), interval, "Interval must be positive.");
            }

            Bucket[] bucket = wheel switch
            {
                <= 0 => throw new ArgumentOutOfRangeException(nameof(wheel), wheel, "Wheel must be positive."),
                > 1 << 30 => throw new ArgumentOutOfRangeException(nameof(wheel), wheel, "Wheel must be less than 2^30."),
                _ => new Bucket[(Int32) BitOperations.RoundUpToPowerOf2(unchecked((UInt32) wheel))]
            };

            for (Int32 i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new Bucket();
            }

            Mask = bucket.Length - 1;
            Wheel = bucket;

            Interval = (Int64) interval.TotalMilliseconds;

            Int64 maximum = Int64.MaxValue / Wheel.Length;
            if (Interval >= maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(interval), interval, $"Interval must be less than {maximum} milliseconds for wheel length {Wheel.Length}.");
            }

            Worker = new Thread(Run);
            MaximumPendingTimeouts = pending;
        }

        internal Int64 DecreasePendingTimeouts()
        {
            return Interlocked.Decrement(ref _pending);
        }

        public Awaiter Delay(Int64 milliseconds)
        {
            Awaiter awaiter = new Awaiter();
            Timeout? timeout = NewTimeout(awaiter, TimeSpan.FromMilliseconds(milliseconds));

            if (timeout is null)
            {
                awaiter.Run(null);
            }

            return awaiter;
        }

        public Timeout? NewTimeout(Awaiter awaiter, TimeSpan span)
        {
            if (awaiter is null)
            {
                throw new ArgumentNullException(nameof(awaiter));
            }

            if (_state is (Int32) State.Shutdown)
            {
                return null;
            }

            Int64 pending = Interlocked.Increment(ref _pending);

            if (MaximumPendingTimeouts > 0 && pending > MaximumPendingTimeouts)
            {
                Interlocked.Decrement(ref _pending);
                throw new InvalidOperationException($"Number of pending timeouts '{pending}' is greater than or equal to maximum allowed pending timeouts '{MaximumPendingTimeouts}'.");
            }

            Start();
            Int64 deadline = Time + (Int64) span.TotalMilliseconds - StartTime;

            if (span.TotalMilliseconds > 0 && deadline < 0)
            {
                deadline = Int64.MaxValue;
            }

            Timeout timeout = new Timeout(this, awaiter, deadline);
            Timeouts.Enqueue(timeout);
            return timeout;
        }

        private Int64 WaitForNextTick()
        {
            Int64 deadline = Interval * (Tick + 1);

            start:
            Int64 current = Time - StartTime;

            if (current < Volatile.Read(ref _last))
            {
                throw new InvalidOperationException("System time moved backwards.");
            }

            Volatile.Write(ref _last, current);

            Decimal difference = deadline - current + 1M;
            Int32 sleep = difference <= Int32.MaxValue ? (Int32) Math.Truncate(difference) : throw new InvalidOperationException("Sleep interval overflow. Interval too large or time jump.");

            if (sleep <= 0)
            {
                return current is not Int64.MaxValue ? current : -Int64.MaxValue;
            }

            Thread.Sleep(sleep);
            goto start;
        }

        private void TransferTimeoutsToBuckets()
        {
            for (Int32 i = 0; i < 100000; i++)
            {
                if (!Timeouts.TryDequeue(out Timeout? timeout))
                {
                    break;
                }

                if (timeout.IsCancelled)
                {
                    continue;
                }

                Int64 evaluate = timeout.Deadline / Interval;
                timeout.Rounds = (evaluate - Tick) / Wheel.Length;

                Bucket bucket = Wheel[(Int32) (Math.Max(evaluate, Tick) & Mask)];
                bucket.Add(timeout);
            }
        }

        private void Start()
        {
            State state;
            switch (state = (State) _state)
            {
                case State.Initialize:
                {
                    if (Interlocked.CompareExchange(ref _state, (Int32) State.Start, (Int32) State.Initialize) is (Int32) State.Initialize)
                    {
                        Worker.Start();
                    }

                    break;
                }
                case State.Start:
                {
                    break;
                }
                case State.Shutdown:
                {
                    return;
                }
                default:
                {
                    throw new EnumUndefinedOrNotSupportedException<State>(state, nameof(State), null);
                }
            }

            while (StartTime == 0)
            {
                try
                {
                    StartTimeEvent.WaitOne(5000);
                }
                catch
                {
                }
            }
        }

        private void Run()
        {
            StartTime = Time;
            if (StartTime == 0)
            {
                StartTime = 1;
            }

            StartTimeEvent.Set();

            do
            {
                Int64 deadline = WaitForNextTick();
                if (deadline <= 0)
                {
                    continue;
                }

                Int32 index = (Int32) (Tick & Mask);
                ProcessCancelledTasks();
                Bucket bucket = Wheel[index];
                TransferTimeoutsToBuckets();
                bucket.Expire(deadline);
                Tick++;

            } while (_state is (Int32) State.Start);

            foreach (Bucket bucket in Wheel)
            {
                bucket.Clear(Unprocess);
            }

            while (Timeouts.TryDequeue(out Timeout? timeout))
            {
                if (!timeout.IsCancelled)
                {
                    Unprocess.Add(timeout);
                }
            }

            ProcessCancelledTasks();
        }

        public ISet<Timeout> Stop()
        {
            Int32 state = Interlocked.CompareExchange(ref _state, (Int32) State.Shutdown, (Int32) State.Start);

            if (state is not (Int32) State.Start)
            {
                return new HashSet<Timeout>();
            }

            try
            {
                while (Worker.IsAlive)
                {
                    Worker.Join(1000);
                }

                return Unprocess;
            }
            catch (Exception)
            {
                return Unprocess;
            }
        }

        private void ProcessCancelledTasks()
        {
            while (Cancel.TryDequeue(out Timeout? timeout))
            {
                try
                {
                    timeout.Remove();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}