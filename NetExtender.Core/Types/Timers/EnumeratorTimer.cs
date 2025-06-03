// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Types.Events;
using NetExtender.Types.Timers.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers
{
    public class EnumeratorTimer<T> : IEnumeratorTimer<T>
    {
        private IEnumerator<T> Source { get; }
        private ITimer Timer { get; }

        public event TickHandler Tick
        {
            add
            {
                Timer.Tick += value;
            }
            remove
            {
                Timer.Tick -= value;
            }
        }

        public event EventHandler? Finished;
        public event ItemTickHandler<T>? ItemTick;

        public T Current
        {
            get
            {
                lock (Source)
                {
                    return Source.Current;
                }
            }
        }

        Object? IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Boolean IsStarted
        {
            get
            {
                return Timer.IsStarted;
            }
        }
        
        public DateTime Now
        {
            get
            {
                return Timer.Now;
            }
        }

        public DateTimeKind Kind
        {
            get
            {
                return Timer.Kind;
            }
            set
            {
                Timer.Kind = value;
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return Timer.Interval;
            }
            set
            {
                Timer.Interval = value;
            }
        }

        public Boolean IsReset { get; init; }

        public EnumeratorTimer(Int32 interval, IEnumerable<T> source)
            : this(source ?? throw new ArgumentNullException(nameof(source)), new TimerWrapper(interval))
        {
        }

        public EnumeratorTimer(Double interval, IEnumerable<T> source)
            : this(source ?? throw new ArgumentNullException(nameof(source)), new TimerWrapper(interval))
        {
        }

        public EnumeratorTimer(TimeSpan interval, IEnumerable<T> source)
            : this(source ?? throw new ArgumentNullException(nameof(source)), new TimerWrapper(interval))
        {
        }

        private EnumeratorTimer(IEnumerable<T> source, ITimer timer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Source = source.GetThreadSafeEnumerator();
            Timer = timer ?? throw new ArgumentNullException(nameof(timer));
            Tick += OnTick;
        }

        protected void OnItemTick(Object? sender, TimeEventArgs args, T item)
        {
            ItemTick?.Invoke(sender, args, item);
        }

        protected virtual void OnTick(Object? sender, TimeEventArgs args)
        {
            if (Source.MoveNext())
            {
                OnItemTick(this, args, Source.Current);
                return;
            }

            if (IsReset && Reset())
            {
                return;
            }

            Finished?.Invoke(this, EventArgs.Empty);
            Dispose();
        }

        public Boolean TrySetKind(DateTimeKind kind)
        {
            return Timer.TrySetKind(kind);
        }

        public Boolean Change(TimeSpan dueTime, TimeSpan period)
        {
            return false;
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        public Boolean MoveNext()
        {
            lock (Source)
            {
                return Source.MoveNext();
            }
        }

        void IEnumerator.Reset()
        {
            Source.Reset();
        }

        public Boolean Reset()
        {
            return Source.TryReset();
        }

        public void Dispose()
        {
            Finished = null;
            ItemTick = null;
            Timer.Dispose();
            Source.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            Finished = null;
            ItemTick = null;
            await Source.DisposeAsync().ConfigureAwait(false);
            await Timer.DisposeAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }
    }
}