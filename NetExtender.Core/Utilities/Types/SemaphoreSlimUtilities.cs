using System;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Disposable;

namespace NetExtender.Utilities.Types
{
    public static class SemaphoreSlimUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Lock(this SemaphoreSlim value)
        {
            return Lock(value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Lock(this SemaphoreSlim value, CancellationToken token)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.Wait(token);
            return Disposable.Create(value, static value => value.Release());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AwaitableDisposable<IDisposable> LockAsync(this SemaphoreSlim value)
        {
            return value.LockAsync(CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AwaitableDisposable<IDisposable> LockAsync(this SemaphoreSlim value, CancellationToken cancellationToken)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            static async Task<IDisposable> Internal(SemaphoreSlim value, CancellationToken cancellationToken)
            {
                await value.WaitAsync(cancellationToken).ConfigureAwait(false);
                return Disposable.Create(value, static value => value.Release());
            }

            return new AwaitableDisposable<IDisposable>(Internal(value, cancellationToken));
        }
    }
}