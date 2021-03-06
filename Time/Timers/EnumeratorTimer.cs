// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NetExtender.Events.Args;
using NetExtender.Times.Timers.Interfaces;
using NetExtender.Times.Timers.Wrappers;
using NetExtender.Utils.Types;

namespace NetExtender.Times.Timers
{
    public class EnumeratorTimer<T> : IEnumeratorTimer<T>
    {
        private ITimer Timer { get; }
        private IEnumerator<T> Enumerator { get; }
        
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

        public event EmptyHandler Finished;
        public event ItemTickHandler<T> ItemTick;

        public T Current
        {
            get
            {
                lock (Enumerator)
                {
                    return Enumerator.Current;
                }
            }
        }

        Object IEnumerator.Current
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
        
        public EnumeratorTimer(Int32 interval, [NotNull] IEnumerable<T> source)
            : this(source ?? throw new ArgumentNullException(nameof(source)), new TimerWrapper(interval))
        {
        }
        
        public EnumeratorTimer(Double interval, [NotNull] IEnumerable<T> source)
            : this(source ?? throw new ArgumentNullException(nameof(source)), new TimerWrapper(interval))
        {
        }

        public EnumeratorTimer(TimeSpan interval, [NotNull] IEnumerable<T> source)
            : this(source ?? throw new ArgumentNullException(nameof(source)), new TimerWrapper(interval))
        {
        }
        
        private EnumeratorTimer([NotNull] IEnumerable<T> enumerable, [NotNull] ITimer timer)
        {
            Enumerator = enumerable?.GetThreadSafeEnumerator() ?? throw new ArgumentNullException(nameof(enumerable));
            Timer = timer ?? throw new ArgumentNullException(nameof(timer));
            Tick += OnTick;
        }

        protected void OnItemTick(Object sender, TimeEventArgs args, T item)
        {
            ItemTick?.Invoke(sender, args, item);
        }

        protected virtual void OnTick(Object sender, TimeEventArgs args)
        {
            if (Enumerator.MoveNext())
            {
                OnItemTick(this, args, Enumerator.Current);
                return;
            }

            if (!IsReset || !Reset())
            {
                Finished?.Invoke();
                Dispose();
            }
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
            lock (Enumerator)
            {
                return Enumerator.MoveNext();
            }
        }

        void IEnumerator.Reset()
        {
            Enumerator.Reset();
        }

        public Boolean Reset()
        {
            return Enumerator.TryReset();
        }

        public void Dispose()
        {
            Finished = null;
            ItemTick = null;
            Timer.Dispose();
            Enumerator.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            Finished = null;
            ItemTick = null;
            await Enumerator.DisposeAsync().ConfigureAwait(false);
            await Timer.DisposeAsync().ConfigureAwait(false);
        }
    }
}