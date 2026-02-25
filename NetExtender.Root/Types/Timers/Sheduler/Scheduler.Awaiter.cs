using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Timers
{
    public partial class Scheduler
    {
        public sealed class Awaiter : INotifyCompletion
        {
            private volatile Boolean _complete;

            public Boolean IsCompleted
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _complete;
                }
            }

            private volatile Action? _handler;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Awaiter GetAwaiter()
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Object? GetResult()
            {
                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [SuppressMessage("ReSharper", "UnusedParameter.Global")]
            public void Run(Timeout? timeout)
            {
                _complete = true;
                Interlocked.Exchange(ref _handler, null)?.Invoke();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void OnCompleted(Action handler)
            {
                if (handler is null)
                {
                    throw new ArgumentNullException(nameof(handler));
                }

                if (_complete)
                {
                    handler.Invoke();
                    return;
                }

                if (Interlocked.CompareExchange(ref _handler, handler, null) is not null)
                {
                    return;
                }

                if (IsCompleted)
                {
                    Interlocked.Exchange(ref _handler, null)?.Invoke();
                }
            }
        }
    }
}
