// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class WaitHandleUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task FromWaitHandle(this WaitHandle handle)
        {
            return FromWaitHandle(handle, Timeout.InfiniteTimeSpan, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> FromWaitHandle(this WaitHandle handle, TimeSpan timeout)
        {
            return FromWaitHandle(handle, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task FromWaitHandle(this WaitHandle handle, CancellationToken token)
        {
            return FromWaitHandle(handle, Timeout.InfiniteTimeSpan, token);
        }

        public static Task<Boolean> FromWaitHandle(this WaitHandle handle, TimeSpan timeout, CancellationToken token)
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            if (handle.WaitOne(0))
            {
                return TaskUtilities.True;
            }

            if (timeout == TimeSpan.Zero)
            {
                return TaskUtilities.False;
            }

            return !token.IsCancellationRequested ? DoFromWaitHandle(handle, timeout, token) : new CancellationToken(true).ToCanceledTask<Boolean>();
        }

        private static async Task<Boolean> DoFromWaitHandle(WaitHandle handle, TimeSpan timeout, CancellationToken token)
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            TaskCompletionSource<Boolean> source = new TaskCompletionSource<Boolean>();
            using (new ThreadPoolRegistration(handle, timeout, source))
            {
                await using (token.Register(state => ((TaskCompletionSource<Boolean>?) state!).TrySetCanceled(), source, false))
                {
                    return await source.Task.ConfigureAwait(false);
                }
            }
        }

        private sealed class ThreadPoolRegistration : IDisposable
        {
            private RegisteredWaitHandle Handle { get; }

            public ThreadPoolRegistration(WaitHandle handle, TimeSpan timeout, TaskCompletionSource<Boolean> source)
            {
                if (handle is null)
                {
                    throw new ArgumentNullException(nameof(handle));
                }

                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                static void Callback(Object? state, Boolean expired)
                {
                    if (state is TaskCompletionSource<Boolean> source)
                    {
                        source.TrySetResult(expired);
                    }
                }

                Handle = ThreadPool.RegisterWaitForSingleObject(handle, Callback, source, timeout, true);
            }

            void IDisposable.Dispose()
            {
                Handle.Unregister(null);
            }
        }
    }
}