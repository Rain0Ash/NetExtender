using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.Timers
{
    public partial class Scheduler
    {
        public sealed class Timeout
        {
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            internal enum State
            {
                Start,
                Expire,
                Cancel
            }

            private static readonly WaitCallback Handler = Execute;
            public Scheduler Scheduler { get; }
            public Awaiter Awaiter { get; }
            internal Bucket? Bucket { get; set; }
            internal Int64 Deadline { get; }
            internal Int64 Rounds;
            internal Timeout? Next;
            internal Timeout? Previous;
            private volatile Int32 _state = (Int32) State.Start;

            public Boolean IsActive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return (State) _state is State.Start;
                }
            }

            public Boolean IsExpired
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return (State) _state is State.Expire;
                }
            }

            public Boolean IsCancelled
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return (State) _state is State.Cancel;
                }
            }

            internal Timeout(Scheduler scheduler, Awaiter awaiter, Int64 deadline)
            {
                Scheduler = scheduler;
                Awaiter = awaiter;
                Deadline = deadline;
            }

            private static void Execute(Object? state)
            {
                if (state is Timeout timeout)
                {
                    timeout.Awaiter.Run(timeout);
                }
            }

            public void Expire()
            {
                if (Interlocked.CompareExchange(ref _state, (Int32) State.Expire, (Int32) State.Start) is not (Int32) State.Start)
                {
                    return;
                }

                try
                {
                    ThreadPool.UnsafeQueueUserWorkItem(Handler, this);
                }
                catch (Exception)
                {
                    Task.Run(() => Awaiter.Run(this));
                }
            }

            public Boolean Cancel()
            {
                if (Interlocked.CompareExchange(ref _state, (Int32) State.Cancel, (Int32) State.Start) is not (Int32) State.Start)
                {
                    return false;
                }

                Scheduler.Cancel.Enqueue(this);
                return true;
            }

            internal void Remove()
            {
                if (Bucket is { } bucket)
                {
                    bucket.Remove(this);
                    return;
                }

                Scheduler.DecreasePendingTimeouts();
            }
        }
    }
}
