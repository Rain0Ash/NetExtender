// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
            
            static async Task<IDisposable> Core(SemaphoreSlim value, CancellationToken cancellationToken)
            {
                await value.WaitAsync(cancellationToken).ConfigureAwait(false);
                return Disposable.Create(value, static value => value.Release());
            }

            return new AwaitableDisposable<IDisposable>(Core(value, cancellationToken));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<Int32> ReleaseAsync(this SemaphoreSlim value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return new ValueTask<Int32>(value.Release());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<Int32> ReleaseAsync(this SemaphoreSlim value, Int32 releaseCount)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return new ValueTask<Int32>(value.Release(releaseCount));
        }
    }
}