using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Delegates
{
    public sealed class ThrottleAction<T> : IDisposable
    {
        public String? Name { get; }
        private Action<T> Action { get; }
        private TimeSpan Period { get; }
        private Int32 _counter;
        private Task _task = Task.CompletedTask;

        public ThrottleAction(Action<T> action, TimeSpan period)
            : this(null, action, period)
        {
        }

        public ThrottleAction(String? name, Action<T> action, TimeSpan period)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Name = name;
            Period = period;
        }

        public Boolean Invoke(T item)
        {
            return Invoke(Action, item);
        }

        public Boolean Invoke(Action<T>? action, T item)
        {
            if (_counter > 0)
            {
                return false;
            }

            Interlocked.Increment(ref _counter);
            Interlocked.Exchange(ref _task, _task.ContinueWith(_ => Exchange(ref action, item)));
            return true;
        }

        private void Exchange(ref Action<T>? action, T item)
        {
            Interlocked.Decrement(ref _counter);

            try
            {
                action?.Invoke(item);
            }
            finally
            {
                action = null;
                using ManualResetEvent @event = new ManualResetEvent(false);
                @event.WaitOne(Period);
            }
        }

        public void Dispose()
        {
            _task = Task.CompletedTask;
        }

        public override String? ToString()
        {
            return Name;
        }
    }

    public sealed class ThrottleAction : IDisposable
    {
        public String? Name { get; }
        private Action Action { get; }
        private TimeSpan Period { get; }
        private Int32 _counter;
        private Task _task = Task.CompletedTask;

        public ThrottleAction(Action action, TimeSpan period)
            : this(null, action, period)
        {
        }

        public ThrottleAction(String? name, Action action, TimeSpan period)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Name = name;
            Period = period;
        }

        public Boolean Invoke()
        {
            return Invoke(Action);
        }

        public Boolean Invoke(Action? action)
        {
            if (_counter > 0)
            {
                return false;
            }

            Interlocked.Increment(ref _counter);
            Interlocked.Exchange(ref _task, _task.ContinueWith(_ => Exchange(ref action)));
            return true;
        }

        private void Exchange(ref Action? action)
        {
            Interlocked.Decrement(ref _counter);

            try
            {
                action?.Invoke();
            }
            finally
            {
                action = null;
                using ManualResetEvent @event = new ManualResetEvent(false);
                @event.WaitOne(Period);
            }
        }

        public void Dispose()
        {
            _task = Task.CompletedTask;
        }

        public override String? ToString()
        {
            return Name;
        }
    }
}