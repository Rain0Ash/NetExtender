using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        private sealed class AssemblyLoadGcDebouncer
        {
            public static AssemblyLoadGcDebouncer Instance { get; } = new AssemblyLoadGcDebouncer();

            public ManualResetEventSlim LoadGate { get; } = new ManualResetEventSlim(false);

            private SyncRoot SyncRoot { get; } = SyncRoot.Create();
            private MutableDebounce<Boolean> RateLimiter { get; } = new MutableDebounce<Boolean>(false, Time.Minute.One);
            private CancellationTokenSource? Source { get; set; }
            private TimeSpan Delay { get; set; } = Time.Second.One;

            private AssemblyLoadGcDebouncer()
            {
                AppDomain.CurrentDomain.AssemblyLoad += Assembly;
            }

            private void Assembly(Object? sender, AssemblyLoadEventArgs args)
            {
                if (LoadGate.IsSet)
                {
                    TriggerGC();
                }
            }

            public void Release()
            {
                LoadGate.Set();
                TriggerGC();
            }

            private void TriggerGC()
            {
                lock (SyncRoot)
                {
                    Source?.Cancel();
                    Source?.Dispose();

                    Source = new CancellationTokenSource();

                    CancellationToken token = Source.Token;

                    Task.Delay(Delay, token).ContinueWith(async task =>
                    {
                        if (!task.IsCanceled && !token.IsCancellationRequested)
                        {
                            await PerformSafe();
                        }
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
                }
            }

            private async Task PerformSafe()
            {
                TimeSpan wait;

                lock (SyncRoot)
                {
                    wait = RateLimiter.IsDebounce ? RateLimiter.Delay - RateLimiter.Time : TimeSpan.Zero;
                }

                if (wait > TimeSpan.Zero)
                {
                    await Task.Delay(wait);
                }

                lock (SyncRoot)
                {
                    if (RateLimiter.IsDebounce)
                    {
                        return;
                    }

                    RateLimiter.Set(true);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
    }
}