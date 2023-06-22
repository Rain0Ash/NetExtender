// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class TaskUtilities<T>
    {
        public static Task<T?> Default { get; } = Task.FromResult(default(T));
    }

    [SuppressMessage("ReSharper", "AsyncConverter.AsyncMethodNamingHighlighting")]
    public static class TaskUtilities
    {
        /// <summary>
        /// Cached true task
        /// </summary>
        public static Task<Boolean> True { get; } = Task.FromResult(true);

        /// <summary>
        /// Cached false task
        /// </summary>
        public static Task<Boolean> False { get; } = Task.FromResult(false);

        /// <summary>
        /// Cached zero task
        /// </summary>
        public static Task<Int32> Zero { get; } = Task.FromResult(0);

        /// <summary>
        /// Cached one task
        /// </summary>
        public static Task<Int32> One { get; } = Task.FromResult(1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> Default<T>()
        {
            return TaskUtilities<T>.Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ToTask(this Boolean value)
        {
            return value ? True : False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ToTask<T>(this T value)
        {
            return value is null ? TaskUtilities<T>.Default! : Task.FromResult(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ToCanceledTask(this CancellationToken token)
        {
            return Task.FromCanceled(token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ToCanceledTask<T>(this CancellationToken token)
        {
            return Task.FromCanceled<T>(token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ToExceptionTask(this Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return Task.FromException(exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ToExceptionTask<T>(this Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return Task.FromException<T>(exception);
        }

        public static IAsyncEnumerable<Task> ToTasksCompletionQueue(params Task[] tasks)
        {
            return ToTasksCompletionQueue(tasks, CancellationToken.None);
        }

        public static IAsyncEnumerable<Task> ToTasksCompletionQueue(CancellationToken token, params Task[] tasks)
        {
            return ToTasksCompletionQueue(tasks, token);
        }

        public static IAsyncEnumerable<Task> ToTasksCompletionQueue(this Task source, CancellationToken token, params Task[]? tasks)
        {
            return ToTasksCompletionQueue(tasks.Prepend(source), token);
        }

        public static IAsyncEnumerable<Task> ToTasksCompletionQueue(this IEnumerable<Task> source)
        {
            return ToTasksCompletionQueue(source, CancellationToken.None);
        }

        public static async IAsyncEnumerable<Task> ToTasksCompletionQueue(this IEnumerable<Task> source, [EnumeratorCancellation] CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            HashSet<Task> set = source.WhereNotNull().ToHashSet();

            while (set.Count > 0 && !token.IsCancellationRequested)
            {
                Task task = await Task.WhenAny(set).ConfigureAwait(false);
                set.Remove(task);

                if (token.IsCancellationRequested)
                {
                    yield break;
                }

                yield return task;
            }
        }

        public static IAsyncEnumerable<Task<T>> ToTasksCompletionQueue<T>(params Task<T>[] tasks)
        {
            return ToTasksCompletionQueue(tasks, CancellationToken.None);
        }

        public static IAsyncEnumerable<Task<T>> ToTasksCompletionQueue<T>(CancellationToken token, params Task<T>[] tasks)
        {
            return ToTasksCompletionQueue(tasks, token);
        }

        public static IAsyncEnumerable<Task<T>> ToTasksCompletionQueue<T>(this Task<T> source, CancellationToken token, params Task<T>[]? tasks)
        {
            return ToTasksCompletionQueue(tasks.Prepend(source), token);
        }

        public static IAsyncEnumerable<Task<T>> ToTasksCompletionQueue<T>(this IEnumerable<Task<T>> source)
        {
            return ToTasksCompletionQueue(source, CancellationToken.None);
        }

        public static async IAsyncEnumerable<Task<T>> ToTasksCompletionQueue<T>(this IEnumerable<Task<T>> source, [EnumeratorCancellation] CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            HashSet<Task<T>> set = source.WhereNotNull().ToHashSet();

            while (set.Count > 0 && !token.IsCancellationRequested)
            {
                Task<T> task = await Task.WhenAny(set).ConfigureAwait(false);
                set.Remove(task);

                if (token.IsCancellationRequested)
                {
                    yield break;
                }

                yield return task;
            }
        }

        public static Task Delay(this CancellationToken token)
        {
            return Delay(Timeout.InfiniteTimeSpan, token);
        }

        public static Task Delay(Int32 milliseconds)
        {
            return Delay(milliseconds, CancellationToken.None);
        }

        public static Task Delay(this TimeSpan delay)
        {
            return Delay(delay, CancellationToken.None);
        }

        public static async Task Delay(Int32 milliseconds, CancellationToken token)
        {
            try
            {
                await Task.Delay(milliseconds, token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
            }
        }

        public static async Task Delay(this TimeSpan delay, CancellationToken token)
        {
            try
            {
                await Task.Delay(delay, token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
            }
        }

        private const Int32 DefaultWaitDelay = 25;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(this CancellationToken token)
        {
            return WaitAsync(DefaultWaitDelay, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Int32 milliseconds, CancellationToken token)
        {
            return WaitAsync(TimeSpan.FromMilliseconds(milliseconds), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task WaitAsync(this TimeSpan delay, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(delay, token).ConfigureAwait(false);
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean> condition)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return WaitAsync(condition, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean>? condition, CancellationToken token)
        {
            return WaitAsync(condition, DefaultWaitDelay, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean> condition, Int32 delay)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return WaitAsync(condition, delay, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean>? condition, Int32 delay, CancellationToken token)
        {
            return WaitAsync(condition, TimeSpan.FromMilliseconds(delay), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean> condition, TimeSpan delay)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return WaitAsync(condition, delay, CancellationToken.None);
        }

        public static async Task WaitAsync(Func<Boolean>? condition, TimeSpan delay, CancellationToken token)
        {
            if (condition is null)
            {
                await WaitAsync(token).ConfigureAwait(false);
                return;
            }

            try
            {
                while (condition.Invoke() && !token.IsCancellationRequested)
                {
                    await Task.Delay(delay, token).ConfigureAwait(false);
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean> condition, Int32 delay, Int32 timeout)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return WaitAsync(condition, delay, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean>? condition, Int32 delay, Int32 timeout, CancellationToken token)
        {
            return WaitAsync(condition, TimeSpan.FromMilliseconds(delay), TimeSpan.FromMilliseconds(timeout), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean> condition, TimeSpan delay, Int32 timeout)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return WaitAsync(condition, delay, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean>? condition, TimeSpan delay, Int32 timeout, CancellationToken token)
        {
            return WaitAsync(condition, delay, TimeSpan.FromMilliseconds(timeout), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean> condition, Int32 delay, TimeSpan timeout)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return WaitAsync(condition, delay, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean>? condition, Int32 delay, TimeSpan timeout, CancellationToken token)
        {
            return WaitAsync(condition, TimeSpan.FromMilliseconds(delay), timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitAsync(Func<Boolean> condition, TimeSpan delay, TimeSpan timeout)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return WaitAsync(condition, delay, timeout, CancellationToken.None);
        }

        public static async Task WaitAsync(Func<Boolean>? condition, TimeSpan delay, TimeSpan timeout, CancellationToken token)
        {
            Task time = Task.Delay(timeout, token);
            Task wait = WaitAsync(condition, delay, token);

            if (await Task.WhenAny(wait, time).ConfigureAwait(false) == time)
            {
                throw new TimeoutException();
            }
        }

        public static Task<Boolean> TryWaitAsync(CancellationToken token)
        {
            return TryWaitAsync(DefaultWaitDelay, token);
        }

        public static Task<Boolean> TryWaitAsync(Int32 milliseconds, CancellationToken token)
        {
            return TryWaitAsync(TimeSpan.FromMilliseconds(milliseconds), token);
        }

        public static async Task<Boolean> TryWaitAsync(TimeSpan delay, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(delay, token).ConfigureAwait(false);
                }
            }
            catch (TaskCanceledException)
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean> condition)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return TryWaitAsync(condition, CancellationToken.None);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean>? condition, CancellationToken token)
        {
            return TryWaitAsync(condition, DefaultWaitDelay, token);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean> condition, Int32 delay)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return TryWaitAsync(condition, delay, CancellationToken.None);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean>? condition, Int32 delay, CancellationToken token)
        {
            return TryWaitAsync(condition, TimeSpan.FromMilliseconds(delay), token);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean> condition, TimeSpan delay)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return TryWaitAsync(condition, delay, CancellationToken.None);
        }

        public static async Task<Boolean> TryWaitAsync(Func<Boolean>? condition, TimeSpan delay, CancellationToken token)
        {
            if (condition is null)
            {
                return await TryWaitAsync(delay, token).ConfigureAwait(false);
            }

            while (condition.Invoke() && !token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(delay, token).ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    return false;
                }
            }

            return true;
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean> condition, Int32 delay, Int32 timeout)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return TryWaitAsync(condition, delay, timeout, CancellationToken.None);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean>? condition, Int32 delay, Int32 timeout, CancellationToken token)
        {
            return TryWaitAsync(condition, TimeSpan.FromMilliseconds(delay), TimeSpan.FromMilliseconds(timeout), token);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean> condition, TimeSpan delay, Int32 timeout)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return TryWaitAsync(condition, delay, timeout, CancellationToken.None);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean>? condition, TimeSpan delay, Int32 timeout, CancellationToken token)
        {
            return TryWaitAsync(condition, delay, TimeSpan.FromMilliseconds(timeout), token);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean> condition, Int32 delay, TimeSpan timeout)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return TryWaitAsync(condition, delay, timeout, CancellationToken.None);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean>? condition, Int32 delay, TimeSpan timeout, CancellationToken token)
        {
            return TryWaitAsync(condition, TimeSpan.FromMilliseconds(delay), timeout, token);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean> condition, TimeSpan delay, TimeSpan timeout)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return TryWaitAsync(condition, delay, timeout, CancellationToken.None);
        }

        public static async Task<Boolean> TryWaitAsync(Func<Boolean>? condition, TimeSpan delay, TimeSpan timeout, CancellationToken token)
        {
            try
            {
                Task time = Task.Delay(timeout, token);
                Task<Boolean> wait = TryWaitAsync(condition, delay, token);

                return await Task.WhenAny(wait, time).ConfigureAwait(false) == wait && await wait.ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        public static Task WaitHandleAsync(WaitHandle handle)
        {
            return WaitHandleAsync(handle, Timeout.InfiniteTimeSpan);
        }

        public static Task WaitHandleAsync(WaitHandle handle, Int32 timeout)
        {
            return WaitHandleAsync(handle, TimeSpan.FromMilliseconds(timeout));
        }

        public static Task WaitHandleAsync(WaitHandle handle, TimeSpan timeout)
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            TaskCompletionSource<Object?> task = new TaskCompletionSource<Object?>();

            void Callback(Object? state, Boolean timedout)
            {
                if (timedout)
                {
                    task.TrySetCanceled();
                }
                else
                {
                    task.TrySetResult(default);
                }
            }

            ThreadPool.RegisterWaitForSingleObject(handle, Callback, default, timeout, true);

            return task.Task;
        }

        public static async Task<T?> TimeoutRetryTaskAsync<T>(Func<CancellationToken, Task<T>> task, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            Byte count = 0;

            do
            {
                token.ThrowIfCancellationRequested();

                using CancellationTokenSource source = token.CreateLinkedSource();
                source.CancelAfter(timeout);

                CancellationToken cancel = source.Token;

                try
                {
                    return await task(cancel).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    if (!token.IsCancellationRequested)
                    {
                        callback?.Invoke(count);
                    }
                }

                await Task.Delay(timeout, cancel).ConfigureAwait(false);

            } while (tries <= 0 || ++count < tries);

            return default;
        }

        /// <inheritdoc cref="Task.Run(Action)"/>
        public static Task Run(Action action)
        {
            return Task.Run(action);
        }

        /// <inheritdoc cref="Task.Run(Action,CancellationToken)"/>
        public static Task Run(Action action, CancellationToken token)
        {
            return Task.Run(action, token);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{T})"/>
        public static Task<T> Run<T>(Func<T> function)
        {
            return Task.Run(function);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{T})"/>
        public static Task<T> Run<T>(Func<T> function, CancellationToken token)
        {
            return Task.Run(function, token);
        }

        /// <inheritdoc cref="Task.Run(Func{Task?})"/>
        public static Task Run(Func<Task?> function)
        {
            return Task.Run(function);
        }

        /// <inheritdoc cref="Task.Run(Func{Task?},CancellationToken)"/>
        public static Task Run(Func<Task?> function, CancellationToken token)
        {
            return Task.Run(function, token);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{Task{T}?})"/>
        public static Task<T> Run<T>(Func<Task<T>?> function)
        {
            return Task.Run(function);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{Task{T}?},CancellationToken)"/>
        public static Task<T> Run<T>(Func<Task<T>?> function, CancellationToken token)
        {
            return Task.Run(function, token);
        }

        /// <inheritdoc cref="Task.Run(Action)"/>
        public static Task RunWithDelay(Action action, Int32 milliseconds)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Delay(milliseconds).ContinueWith(action);
        }

        /// <inheritdoc cref="Task.Run(Action)"/>
        public static Task RunWithDelay(Action action, TimeSpan delay)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Delay(delay).ContinueWith(action);
        }

        /// <inheritdoc cref="Task.Run(Action,CancellationToken)"/>
        public static Task RunWithDelay(Action action, Int32 milliseconds, CancellationToken token)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Delay(milliseconds, token).ContinueWith(action, token);
        }

        /// <inheritdoc cref="Task.Run(Action,CancellationToken)"/>
        public static Task RunWithDelay(Action action, TimeSpan delay, CancellationToken token)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Delay(delay, token).ContinueWith(action, token);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{T})"/>
        public static Task<T> RunWithDelay<T>(Func<T> function, Int32 milliseconds)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(milliseconds).ContinueWith(function);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{T})"/>
        public static Task<T> RunWithDelay<T>(Func<T> function, TimeSpan delay)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(delay).ContinueWith(function);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{T})"/>
        public static Task<T> RunWithDelay<T>(Func<T> function, TimeSpan delay, CancellationToken token)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(delay, token).ContinueWith(function, token);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{T})"/>
        public static Task<T> RunWithDelay<T>(Func<T> function, Int32 milliseconds, CancellationToken token)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(milliseconds, token).ContinueWith(function, token);
        }

        /// <inheritdoc cref="Task.Run(Func{Task?})"/>
        public static Task RunWithDelay(Func<Task?> function, Int32 milliseconds)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(milliseconds).ContinueWith(function);
        }

        /// <inheritdoc cref="Task.Run(Func{Task?})"/>
        public static Task RunWithDelay(Func<Task?> function, TimeSpan delay)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(delay).ContinueWith(function);
        }

        /// <inheritdoc cref="Task.Run(Func{Task?},CancellationToken)"/>
        public static Task RunWithDelay(Func<Task?> function, Int32 milliseconds, CancellationToken token)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(milliseconds, token).ContinueWith(function, token);
        }

        /// <inheritdoc cref="Task.Run(Func{Task?},CancellationToken)"/>
        public static Task RunWithDelay(Func<Task?> function, TimeSpan delay, CancellationToken token)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(delay, token).ContinueWith(function, token);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{Task{T}?})"/>
        public static Task<T> RunWithDelay<T>(Func<Task<T>?> function, Int32 milliseconds)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(milliseconds).ContinueWith(function);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{Task{T}?})"/>
        public static Task<T> RunWithDelay<T>(Func<Task<T>?> function, TimeSpan delay)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(delay).ContinueWith(function);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{Task{T}?},CancellationToken)"/>
        public static Task<T> RunWithDelay<T>(Func<Task<T>?> function, Int32 milliseconds, CancellationToken token)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(milliseconds, token).ContinueWith(function, token);
        }

        /// <inheritdoc cref="Task.Run{T}(Func{Task{T}?},CancellationToken)"/>
        public static Task<T> RunWithDelay<T>(Func<Task<T>?> function, TimeSpan delay, CancellationToken token)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Delay(delay, token).ContinueWith(function, token);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task})"/>
        public static Task ContinueWith(this Task source, Action action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWith(_ => action());
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, CancellationToken)"/>
        public static Task ContinueWith(this Task source, Action action, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWith(_ => action(), token);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, CancellationToken, TaskContinuationOptions, TaskScheduler)"/>
        [SuppressMessage("ReSharper", "CA1068")]
        public static Task ContinueWith(this Task source, Action action, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWith(_ => action(), token, options, scheduler);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, TaskContinuationOptions)"/>
        public static Task ContinueWith(this Task source, Action action, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWith(_ => action(), options);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, TaskScheduler)"/>
        public static Task ContinueWith(this Task source, Action action, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWith(_ => action(), scheduler);
        }

        public static Task<T> ContinueWith<T>(this Task source, Func<T> function)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWith(_ => function());
        }

        public static Task<T> ContinueWith<T>(this Task source, Func<T> function, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWith(_ => function(), token);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<T> ContinueWith<T>(this Task source, Func<T> function, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWith(_ => function(), token, options, scheduler);
        }

        public static Task<T> ContinueWith<T>(this Task source, Func<T> function, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWith(_ => function(), options);
        }

        public static Task<T> ContinueWith<T>(this Task source, Func<T> function, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWith(_ => function(), scheduler);
        }

        public static async Task<T> ContinueWith<T>(this Task source, Func<Task<T>?> function)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            Task<Task<T>?> task = source.ContinueWith(_ => function());
            Task<T>? next = await task.ConfigureAwait(false);

            if (next is null)
            {
                throw new TaskCanceledException(task);
            }

            return await next.ConfigureAwait(false);
        }

        public static async Task<T> ContinueWith<T>(this Task source, Func<Task<T>?> function, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            Task<Task<T>?> task = source.ContinueWith(_ => function(), token);
            Task<T>? next = await task.ConfigureAwait(false);

            if (next is null)
            {
                throw new TaskCanceledException(task);
            }

            return await next.ConfigureAwait(false);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static async Task<T> ContinueWith<T>(this Task source, Func<Task<T>?> function, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            Task<Task<T>?> task = source.ContinueWith(_ => function(), token, options, scheduler);
            Task<T>? next = await task.ConfigureAwait(false);

            if (next is null)
            {
                throw new TaskCanceledException(task);
            }

            return await next.ConfigureAwait(false);
        }

        public static async Task<T> ContinueWith<T>(this Task source, Func<Task<T>?> function, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            Task<Task<T>?> task = source.ContinueWith(_ => function(), options);
            Task<T>? next = await task.ConfigureAwait(false);

            if (next is null)
            {
                throw new TaskCanceledException(task);
            }

            return await next.ConfigureAwait(false);
        }

        public static async Task<T> ContinueWith<T>(this Task source, Func<Task<T>?> function, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            Task<Task<T>?> task = source.ContinueWith(_ => function(), scheduler);
            Task<T>? next = await task.ConfigureAwait(false);

            if (next is null)
            {
                throw new TaskCanceledException(task);
            }

            return await next.ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ContinueWithDelay(this Task source, Int32 milliseconds)
        {
            return ContinueWithDelay(source, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ContinueWithDelay(this Task source, Int32 milliseconds, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (milliseconds < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(milliseconds), milliseconds, null);
            }

            return source.ContinueWith(_ => Task.Delay(milliseconds, token), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ContinueWithDelay(this Task source, TimeSpan delay)
        {
            return ContinueWithDelay(source, delay, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task ContinueWithDelay(this Task source, TimeSpan delay, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!delay.IsTimeout())
            {
                throw new ArgumentOutOfRangeException(nameof(delay), delay, null);
            }

            return source.ContinueWith(_ => Task.Delay(delay, token), token);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task> action, Int32 milliseconds)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(action);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task> action, TimeSpan delay)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(delay).ContinueWith(action);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task> action, Int32 milliseconds, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(action, token);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task> action, TimeSpan delay, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(action, token);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task> action, Int32 milliseconds, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(action, scheduler);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task> action, TimeSpan delay, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay).ContinueWith(action, scheduler);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task> action, Int32 milliseconds, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(action, options);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task> action, TimeSpan delay, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(delay).ContinueWith(action, options);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task ContinueWithDelay(this Task source, Action<Task> action, Int32 milliseconds, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(action, token, options, scheduler);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task ContinueWithDelay(this Task source, Action<Task> action, TimeSpan delay, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(action, token, options, scheduler);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, Int32 milliseconds, Object? state)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(action, state);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, TimeSpan delay, Object? state)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(delay).ContinueWith(action, state);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, Int32 milliseconds, Object? state, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(action, state, token);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, TimeSpan delay, Object? state, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(action, state, token);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, Int32 milliseconds, Object? state, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(action, state, scheduler);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, TimeSpan delay, Object? state, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay).ContinueWith(action, state, scheduler);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, Int32 milliseconds, Object? state, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(action, state, options);
        }

        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, TimeSpan delay, Object? state, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(delay).ContinueWith(action, state, options);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, Int32 milliseconds, Object? state, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(action, state, token, options, scheduler);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task ContinueWithDelay(this Task source, Action<Task, Object?> action, TimeSpan delay, Object? state, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(action, state, token, options, scheduler);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, Int32 milliseconds)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, TimeSpan delay)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, Int32 milliseconds, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(function, token);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, TimeSpan delay, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(function, token);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, Int32 milliseconds, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, scheduler);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, TimeSpan delay, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, scheduler);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, Int32 milliseconds, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, options);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, TimeSpan delay, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, options);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, Int32 milliseconds, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(function, token, options, scheduler);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, TResult> function, TimeSpan delay, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(function, token, options, scheduler);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, Int32 milliseconds, Object? state)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, state);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, TimeSpan delay, Object? state)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, state);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, Int32 milliseconds, Object? state, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(function, state, token);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, TimeSpan delay, Object? state, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(function, state, token);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, Int32 milliseconds, Object? state, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, state, scheduler);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, TimeSpan delay, Object? state, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, state, scheduler);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, Int32 milliseconds, Object? state, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, state, options);
        }

        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, TimeSpan delay, Object? state, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, state, options);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, Int32 milliseconds, Object? state, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(function, state, token, options, scheduler);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<TResult> ContinueWithDelay<TResult>(this Task source, Func<Task, Object?, TResult> function, TimeSpan delay, Object? state, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(function, state, token, options, scheduler);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, CancellationToken)"/>
        public static Task ContinueWithDelay(this Task source, Action action, Int32 milliseconds, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(action, token);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, CancellationToken)"/>
        public static Task ContinueWithDelay(this Task source, Action action, TimeSpan delay, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(action, token);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, CancellationToken, TaskContinuationOptions, TaskScheduler)"/>
        [SuppressMessage("ReSharper", "CA1068")]
        public static Task ContinueWithDelay(this Task source, Action action, Int32 milliseconds, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(action, token, options, scheduler);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, CancellationToken, TaskContinuationOptions, TaskScheduler)"/>
        [SuppressMessage("ReSharper", "CA1068")]
        public static Task ContinueWithDelay(this Task source, Action action, TimeSpan delay, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(action, token, options, scheduler);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, TaskContinuationOptions)"/>
        public static Task ContinueWithDelay(this Task source, Action action, Int32 milliseconds, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(action, options);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, TaskContinuationOptions)"/>
        public static Task ContinueWithDelay(this Task source, Action action, TimeSpan delay, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return source.ContinueWithDelay(delay).ContinueWith(action, options);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, TaskScheduler)"/>
        public static Task ContinueWithDelay(this Task source, Action action, Int32 milliseconds, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(action, scheduler);
        }

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, TaskScheduler)"/>
        public static Task ContinueWithDelay(this Task source, Action action, TimeSpan delay, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay).ContinueWith(action, scheduler);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, Int32 milliseconds)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, TimeSpan delay)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, Int32 milliseconds, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(function, token);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, TimeSpan delay, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(function, token);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, Int32 milliseconds, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(function, token, options, scheduler);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, TimeSpan delay, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(function, token, options, scheduler);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, Int32 milliseconds, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, options);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, TimeSpan delay, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, options);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, Int32 milliseconds, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, scheduler);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<T> function, TimeSpan delay, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, scheduler);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, Int32 milliseconds)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, TimeSpan delay)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, Int32 milliseconds, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(function, token);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, TimeSpan delay, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(function, token);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, Int32 milliseconds, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds, token).ContinueWith(function, token, options, scheduler);
        }

        [SuppressMessage("ReSharper", "CA1068")]
        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, TimeSpan delay, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay, token).ContinueWith(function, token, options, scheduler);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, Int32 milliseconds, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, options);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, TimeSpan delay, TaskContinuationOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, options);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, Int32 milliseconds, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(milliseconds).ContinueWith(function, scheduler);
        }

        public static Task<T> ContinueWithDelay<T>(this Task source, Func<Task<T>?> function, TimeSpan delay, TaskScheduler scheduler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            return source.ContinueWithDelay(delay).ContinueWith(function, scheduler);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static void WaitAll(this IEnumerable<Task> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Task.WaitAll(source.AsArray());
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="token">
        /// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static void WaitAll(this IEnumerable<Task> source, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Task.WaitAll(source.AsArray(), token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this IEnumerable<Task> source, Int32 timeout)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WaitAll(source.AsArray(), timeout);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.
        /// </param>
        /// <param name="token">
        /// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this IEnumerable<Task> source, Int32 timeout, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WaitAll(source.AsArray(), timeout, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
        /// <see cref="TimeSpan"/> or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> to wait, or <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this IEnumerable<Task> source, TimeSpan timeout)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WaitAll(source.AsArray(), timeout);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
        /// <see cref="TimeSpan"/> or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> to wait, or <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
        /// </param>
        /// <param name="token">
        /// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this IEnumerable<Task> source, TimeSpan timeout, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WaitAll(source.AsArray(), (Int32) timeout.TotalMilliseconds, token);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task"/> objects in an enumerable collection
        /// have completed.
        /// </summary>
        /// <param name="source">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task WhenAll(this IEnumerable<Task> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WhenAll(source);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{T}"/> objects in an enumerable collection
        /// have completed.
        /// </summary>
        /// <typeparam name="T">The type of the completed Task.</typeparam>
        /// <param name="source">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WhenAll(source);
        }

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        /// <typeparam name="T">The type of the completed Task.</typeparam>
        /// <param name="source">The tasks to wait on for completion.</param>
        /// <returns>
        /// A task that represents the completion of one of the supplied tasks. The return task's Result is the task that
        /// completed.
        /// </returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task<Task<T>> WhenAny<T>(this IEnumerable<Task<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WhenAny(source);
        }

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        /// <param name="source">The tasks to wait on for completion.</param>
        /// <returns>
        /// A task that represents the completion of one of the supplied tasks. The return task's Result is the task that
        /// completed.
        /// </returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task<Task> WhenAny(this IEnumerable<Task> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WhenAny(source);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.
        /// </param>
        /// <param name="token">
        /// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this Task[] source, Int32 timeout, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WaitAll(source, timeout, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
        /// <see cref="TimeSpan"/> or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> to wait, or <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
        /// </param>
        /// <param name="token">
        /// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this Task[] source, TimeSpan timeout, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WaitAll(source, (Int32) timeout.TotalMilliseconds, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="token">
        /// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static void WaitAll(this Task[] source, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Task.WaitAll(source, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this Task[] source, Int32 timeout)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WaitAll(source, timeout);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
        /// <see cref="TimeSpan"/> or until the wait is cancelled.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> to wait, or <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this Task[] source, TimeSpan timeout)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WaitAll(source, timeout);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution.
        /// </summary>
        /// <param name="source"><see cref="Task"/> instances on which to wait.</param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static void WaitAll(this Task[] source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Task.WaitAll(source);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task"/> objects in an enumerable collection
        /// have completed.
        /// </summary>
        /// <param name="source">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static Task WhenAll(this Task[] source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WhenAll(source);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{T}"/> objects in an enumerable collection
        /// have completed.
        /// </summary>
        /// <typeparam name="T">The type of the completed Task.</typeparam>
        /// <param name="source">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static Task<T[]> WhenAll<T>(this Task<T>[] source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WhenAll(source);
        }

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        /// <typeparam name="T">The type of the completed Task.</typeparam>
        /// <param name="source">The tasks to wait on for completion.</param>
        /// <returns>
        /// A task that represents the completion of one of the supplied tasks. The return task's Result is the task that
        /// completed.
        /// </returns>
        public static Task<Task<T>> WhenAny<T>(this Task<T>[] source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WhenAny(source);
        }

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        /// <param name="source">The tasks to wait on for completion.</param>
        /// <returns>
        /// A task that represents the completion of one of the supplied tasks. The return task's Result is the task that
        /// completed.
        /// </returns>
        public static Task<Task> WhenAny(this Task[] source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.WhenAny(source);
        }

        public static Task ErrorHandle(this Task source)
        {
            return ErrorHandle(source, null);
        }

        public static async Task ErrorHandle(this Task source, Action<Exception?>? action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                await source.ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                action?.Invoke(exception);
            }
        }

        public static Task ErrorHandle(this Task<Boolean> source)
        {
            return ErrorHandle(source, null);
        }

        public static async Task ErrorHandle(this Task<Boolean> source, Action<Exception?>? action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                if (!await source.ConfigureAwait(false))
                {
                    action?.Invoke(null);
                }
            }
            catch (Exception exception)
            {
                action?.Invoke(exception);
            }
        }

        public static void FireAndForget(this Task task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            task.ContinueWith(continuation => continuation.Exception, TaskContinuationOptions.OnlyOnFaulted);
        }

        public static void FireAndForget(this Task task, Action<Exception> handler)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            void Handle(Task continuation)
            {
                if (continuation.Exception is not null)
                {
                    handler.Invoke(continuation.Exception);
                }
            }

            task.ContinueWith(Handle, TaskContinuationOptions.OnlyOnFaulted);
        }

        public static Task ToAsync(this Task source, AsyncCallback? callback, Object? state)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TaskCompletionSource<Object?> completion = new TaskCompletionSource<Object?>(state);
            source.ContinueWith(_ =>
            {
                completion.SetFromTask(source);
                callback?.Invoke(completion.Task);
            });

            return completion.Task;
        }

        public static Task<T> ToAsync<T>(this Task<T> source, AsyncCallback? callback, Object? state)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TaskCompletionSource<T> completion = new TaskCompletionSource<T>(state);
            source.ContinueWith(_ =>
            {
                completion.SetFromTask(source);
                callback?.Invoke(completion.Task);
            });

            return completion.Task;
        }

        private const TaskContinuationOptions TaskExceptionOptions = TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted;

        public static Task IgnoreException(this Task source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.ContinueWith(ActionUtilities.Default, CancellationToken.None, TaskExceptionOptions, TaskScheduler.Default);
            return source;
        }

        public static Task<T> IgnoreException<T>(this Task<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.ContinueWith(ActionUtilities.Default, CancellationToken.None, TaskExceptionOptions, TaskScheduler.Default);
            return source;
        }

        private static void TaskFault(Task source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Exception? exception = source.Exception;

            if (exception is not null)
            {
                Environment.FailFast("A task faulted.", exception);
            }
        }

        public static Task FailFastOnException(this Task source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.ContinueWith(TaskFault, CancellationToken.None, TaskExceptionOptions, TaskScheduler.Default);
            return source;
        }

        public static Task<T> FailFastOnException<T>(this Task<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.ContinueWith(TaskFault, CancellationToken.None, TaskExceptionOptions, TaskScheduler.Default);
            return source;
        }

        [SuppressMessage("ReSharper", "AsyncConverter.AsyncWait")]
        public static Task PropagateException(this Task source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.IsFaulted)
            {
                source.Wait();
            }

            return source;
        }

        [SuppressMessage("ReSharper", "AsyncConverter.AsyncWait")]
        public static Task<T> PropagateException<T>(this Task<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.IsFaulted)
            {
                source.Wait();
            }

            return source;
        }

        public static IObservable<T> ToObservable<T>(this Task<T> task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            return new TaskObservable<T>(task);
        }

        private class TaskObservable<T> : IObservable<T>
        {
            private Task<T> Internal { get; }

            public TaskObservable(Task<T> task)
            {
                Internal = task ?? throw new ArgumentNullException(nameof(task));
            }

            [SuppressMessage("ReSharper", "AsyncConverter.AsyncWait")]
            public IDisposable Subscribe(IObserver<T> observer)
            {
                if (observer is null)
                {
                    throw new ArgumentNullException(nameof(observer));
                }

                CancellationTokenSource source = new CancellationTokenSource();

                void ContinuationFunction(Task<T> task)
                {
                    switch (task.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            observer.OnNext(Internal.Result);
                            observer.OnCompleted();
                            break;
                        case TaskStatus.Faulted:
                            Exception? exception = Internal.Exception;
                            if (exception is not null)
                            {
                                observer.OnError(exception);
                            }

                            break;
                        case TaskStatus.Canceled:
                            observer.OnError(new TaskCanceledException(task));
                            break;
                        default:
                            break;
                    }
                }

                Internal.ContinueWith(ContinuationFunction, source.Token);
                return new CancelOnDispose(source);
            }

            private class CancelOnDispose : IDisposable
            {
                private CancellationTokenSource Source { get; }

                public CancelOnDispose(CancellationTokenSource source)
                {
                    Source = source ?? throw new ArgumentNullException(nameof(source));
                }

                public void Dispose()
                {
                    Source.Cancel();
                    Source.Dispose();
                }
            }
        }

        public static Task<T>[] InitializeTasks<T>(this IEnumerable<Task<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToArray();
        }

        public static TaskAwaiter GetAwaiter(this SByte value)
        {
            if (value == Timeout.Infinite)
            {
                return GetAwaiter(Timeout.InfiniteTimeSpan);
            }

            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this Byte value)
        {
            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this Int16 value)
        {
            if (value == Timeout.Infinite)
            {
                return GetAwaiter(Timeout.InfiniteTimeSpan);
            }

            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this UInt16 value)
        {
            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this Int32 value)
        {
            if (value == Timeout.Infinite)
            {
                return GetAwaiter(Timeout.InfiniteTimeSpan);
            }

            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this UInt32 value)
        {
            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this Int64 value)
        {
            if (value == Timeout.Infinite)
            {
                return GetAwaiter(Timeout.InfiniteTimeSpan);
            }

            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this UInt64 value)
        {
            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this Single value)
        {
            if (Math.Abs(value - Timeout.Infinite) < Single.Epsilon)
            {
                return GetAwaiter(Timeout.InfiniteTimeSpan);
            }

            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this Double value)
        {
            if (Math.Abs(value - Timeout.Infinite) < Double.Epsilon)
            {
                return GetAwaiter(Timeout.InfiniteTimeSpan);
            }

            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds(value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this Decimal value)
        {
            if (value == Timeout.Infinite)
            {
                return GetAwaiter(Timeout.InfiniteTimeSpan);
            }

            TimeSpan wait = value > 0 ? TimeSpan.FromMilliseconds((Double) value) : TimeSpan.Zero;
            return GetAwaiter(wait);
        }

        public static TaskAwaiter GetAwaiter(this TimeSpan value)
        {
            return value > TimeSpan.Zero || value == Timeout.InfiniteTimeSpan ? Task.Delay(value).GetAwaiter() : Task.CompletedTask.GetAwaiter();
        }
    }
}