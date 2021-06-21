// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utils.Types
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "AsyncConverter.AsyncMethodNamingHighlighting")]
    public static class TaskUtils
    {
        /// <summary>
        /// Cached true task
        /// </summary>
        public static Task<Boolean> True { get; } = Task.FromResult(true);
        
        /// <summary>
        /// Cached false task
        /// </summary>
        public static Task<Boolean> False { get; } = Task.FromResult(false);

        private static class TaskCache<T>
        {
            public static Task<T?> Default { get; }

            static TaskCache()
            {
                Default = Task.FromResult(default(T));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> DefaultTask<T>()
        {
            return TaskCache<T>.Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ToTask(this Boolean value)
        {
            return value ? True : False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ToTask<T>(this T value)
        {
            return Task.FromResult(value);
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
        
        public static IAsyncEnumerable<Task> ToTasksCompletionQueue(this Task task, CancellationToken token, params Task[] tasks)
        {
            return ToTasksCompletionQueue(tasks.Prepend(task), token);
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
        
        public static IAsyncEnumerable<Task<T>> ToTasksCompletionQueue<T>(this Task<T> task, CancellationToken token, params Task<T>[] tasks)
        {
            return ToTasksCompletionQueue(tasks.Prepend(task), token);
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
        
        public static Task Delay(CancellationToken token)
        {
            return Delay(-1, token);
        }

        public static Task Delay(TimeSpan delay)
        {
            return Delay(delay, CancellationToken.None);
        }

        public static async Task Delay(TimeSpan delay, CancellationToken token)
        {
            try
            {
                await Task.Delay(delay, token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                //ignored
            }
        }
        
        public static Task Delay(Int32 milli)
        {
            return Delay(milli, CancellationToken.None);
        }
        
        public static async Task Delay(Int32 milli, CancellationToken token)
        {
            try
            {
                await Task.Delay(milli, token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                //ignored
            }
        }

        public static Task WaitAsync(CancellationToken token)
        {
            return WaitAsync(null, token);
        }

        public static Task WaitAsync(Func<Boolean>? condition, CancellationToken token)
        {
            return WaitAsync(condition, 25, token);
        }

        public static async Task WaitAsync(Func<Boolean>? condition, Int32 delay, CancellationToken token)
        {
            while (condition?.Invoke() != false && !token.IsCancellationRequested)
            {
                await Task.Delay(delay, token).ConfigureAwait(false);
            }
        }

        public static async Task WaitAsync(Func<Boolean>? condition, Int32 delay, Int32 timeout, CancellationToken token)
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
            return TryWaitAsync(null, token);
        }

        public static Task<Boolean> TryWaitAsync(Func<Boolean>? condition, CancellationToken token)
        {
            return TryWaitAsync(condition, 25, token);
        }

        public static async Task<Boolean> TryWaitAsync(Func<Boolean>? condition, Int32 delay, CancellationToken token)
        {
            while (condition?.Invoke() != false && !token.IsCancellationRequested)
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

        public static async Task<Boolean> TryWaitAsync(Func<Boolean>? condition, Int32 delay, Int32 timeout, CancellationToken token)
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
        
        public static Task WaitHandleAsync(WaitHandle handle, Int32 timeout = Timeout.Infinite)
        {
            TaskCompletionSource<Object?> task = new TaskCompletionSource<Object?>();

            void Callback(Object? state, Boolean timedOut)
            {
                if (timedOut)
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

        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task})"/>
        public static Task ContinueWith(this Task source, Action action)
        {
            return source.ContinueWith(_ => action());
        }
        
        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, CancellationToken)"/>
        public static Task ContinueWith(this Task source, Action action, CancellationToken token)
        {
            return source.ContinueWith(_ => action(), token);
        }
        
        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, CancellationToken, TaskContinuationOptions, TaskScheduler)"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "CA1068")]
        public static Task ContinueWith(this Task source, Action action, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            return source.ContinueWith(_ => action(), token, options, scheduler);
        }
        
        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, TaskContinuationOptions)"/>
        public static Task ContinueWith(this Task source, Action action, TaskContinuationOptions options)
        {
            return source.ContinueWith(_ => action(), options);
        }
        
        /// <inheritdoc cref="Task.ContinueWith(System.Action{System.Threading.Tasks.Task}, TaskScheduler)"/>
        public static Task ContinueWith(this Task source, Action action, TaskScheduler scheduler)
        {
            return source.ContinueWith(_ => action(), scheduler);
        }
        
        public static Task<T> ContinueWith<T>(this Task source, Func<T> function)
        {
            return source.ContinueWith(_ => function());
        }
        
        public static Task<T> ContinueWith<T>(this Task source, Func<T> function, CancellationToken token)
        {
            return source.ContinueWith(_ => function(), token);
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "CA1068")]
        public static Task<T> ContinueWith<T>(this Task source, Func<T> function, CancellationToken token, TaskContinuationOptions options, TaskScheduler scheduler)
        {
            return source.ContinueWith(_ => function(), token, options, scheduler);
        }
        
        public static Task<T> ContinueWith<T>(this Task source, Func<T> function, TaskContinuationOptions options)
        {
            return source.ContinueWith(_ => function(), options);
        }
        
        public static Task<T> ContinueWith<T>(this Task source, Func<T> function, TaskScheduler scheduler)
        {
            return source.ContinueWith(_ => function(), scheduler);
        }
        
        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
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
        public static Boolean WaitAll(this IEnumerable<Task> tasks, Int32 timeout, CancellationToken token)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WaitAll(tasks.ToArray(), timeout, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
        /// <see cref="TimeSpan"/> or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
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
        public static Boolean WaitAll(this IEnumerable<Task> tasks, TimeSpan timeout, CancellationToken token)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WaitAll(tasks.ToArray(), (Int32) timeout.TotalMilliseconds, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="token">
        /// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static void WaitAll(this IEnumerable<Task> tasks, CancellationToken token)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            Task.WaitAll(tasks.ToArray(), token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this IEnumerable<Task> tasks, Int32 timeout)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WaitAll(tasks.ToArray(), timeout);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
        /// <see cref="TimeSpan"/> or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> to wait, or <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this IEnumerable<Task> tasks, TimeSpan timeout)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WaitAll(tasks.ToArray(), timeout);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static void WaitAll(this IEnumerable<Task> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task"/> objects in an enumerable collection
        /// have completed.
        /// </summary>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task WhenAll(this IEnumerable<Task> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{T}"/> objects in an enumerable collection
        /// have completed.
        /// </summary>
        /// <typeparam name="T">The type of the completed Task.</typeparam>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        /// <typeparam name="T">The type of the completed Task.</typeparam>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>
        /// A task that represents the completion of one of the supplied tasks. The return task's Result is the task that
        /// completed.
        /// </returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task<Task<T>> WhenAny<T>(this IEnumerable<Task<T>> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>
        /// A task that represents the completion of one of the supplied tasks. The return task's Result is the task that
        /// completed.
        /// </returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task<Task> WhenAny(this IEnumerable<Task> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WhenAny(tasks);
        }
        
        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
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
        public static Boolean WaitAll(this Task[] tasks, Int32 timeout, CancellationToken token)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WaitAll(tasks, timeout, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
        /// <see cref="TimeSpan"/> or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
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
        public static Boolean WaitAll(this Task[] tasks, TimeSpan timeout, CancellationToken token)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WaitAll(tasks, (Int32) timeout.TotalMilliseconds, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="token">
        /// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static void WaitAll(this Task[] tasks, CancellationToken token)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            Task.WaitAll(tasks, token);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
        /// milliseconds or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this Task[] tasks, Int32 timeout)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WaitAll(tasks, timeout);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
        /// <see cref="TimeSpan"/> or until the wait is cancelled.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> to wait, or <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static Boolean WaitAll(this Task[] tasks, TimeSpan timeout)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WaitAll(tasks, timeout);
        }

        /// <summary>
        /// Waits for all of the provided <see cref="Task"/> objects to complete execution.
        /// </summary>
        /// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
        /// <returns>
        /// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static void WaitAll(this Task[] tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            Task.WaitAll(tasks);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task"/> objects in an enumerable collection
        /// have completed.
        /// </summary>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static Task WhenAll(this Task[] tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{T}"/> objects in an enumerable collection
        /// have completed.
        /// </summary>
        /// <typeparam name="T">The type of the completed Task.</typeparam>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static Task<T[]> WhenAll<T>(this Task<T>[] tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        /// <typeparam name="T">The type of the completed Task.</typeparam>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>
        /// A task that represents the completion of one of the supplied tasks. The return task's Result is the task that
        /// completed.
        /// </returns>
        public static Task<Task<T>> WhenAny<T>(this Task<T>[] tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>
        /// A task that represents the completion of one of the supplied tasks. The return task's Result is the task that
        /// completed.
        /// </returns>
        public static Task<Task> WhenAny(this Task[] tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task.WhenAny(tasks);
        }
        
        public static async void ErrorHandle(this Task<Boolean> task, Action<Exception?>? action = null)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            try
            {
                if (!await task.ConfigureAwait(false))
                {
                    action?.Invoke(null);
                }
            }
            catch (Exception e)
            {
                action?.Invoke(e);
            }
        }
        
        public static async void ErrorHandle(this Task task, Action<Exception?>? action = null)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                action?.Invoke(e);
            }
        }

        public static Task<T>[] InitializeTasks<T>(this IEnumerable<Task<T>> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return tasks.ToArray();
        }
    }
}