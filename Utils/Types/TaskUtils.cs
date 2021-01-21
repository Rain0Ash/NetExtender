// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    public static class TaskUtils
    {
	    /// <inheritdoc cref="BooleanUtils.True"/>
	    public static Task<Boolean> True
	    {
		    get
		    {
			    return BooleanUtils.True;
		    }
	    }
	    
	    /// <inheritdoc cref="BooleanUtils.False"/>
	    public static Task<Boolean> False
	    {
		    get
		    {
			    return BooleanUtils.False;
		    }
	    }

	    private static class TaskCache<T>
        {
	        public static Task<T> Default { get; }

	        static TaskCache()
	        {
		        Default = Task.FromResult(default(T));
	        }
        }

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> DefaultTask<T>()
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
				await Task.Delay(delay, token);
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
		        await Task.Delay(milli, token);
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

        public static Task WaitAsync(Func<Boolean> condition, CancellationToken token)
        {
            return WaitAsync(condition, 25, token);
        }

        public static async Task WaitAsync(Func<Boolean> condition, Int32 delay, CancellationToken token)
        {
            while (condition?.Invoke() != false && !token.IsCancellationRequested)
            {
                await Task.Delay(delay, token).ConfigureAwait(false);
            }
        }

        public static async Task WaitAsync(Func<Boolean> condition, Int32 delay, Int32 timeout, CancellationToken token)
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

        public static Task<Boolean> TryWaitAsync(Func<Boolean> condition, CancellationToken token)
        {
            return TryWaitAsync(condition, 25, token);
        }

        public static async Task<Boolean> TryWaitAsync(Func<Boolean> condition, Int32 delay, CancellationToken token)
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

        public static async Task<Boolean> TryWaitAsync(Func<Boolean> condition, Int32 delay, Int32 timeout, CancellationToken token)
        {
            try
            {
                Task time = Task.Delay(timeout, token);
                Task<Boolean> wait = TryWaitAsync(condition, delay, token);

                return await Task.WhenAny(wait, time).ConfigureAwait(false) == wait && await wait;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }
        
        public static Task WaitHandleAsync(WaitHandle handle, Int32 timeout = Timeout.Infinite)
        {
            TaskCompletionSource<Object> task = new TaskCompletionSource<Object>();

            void Callback(Object state, Boolean timedOut)
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

        public static async Task<T> TimeoutRetryTaskAsync<T>(Func<CancellationToken, Task<T>> task, Byte tries, TimeSpan timeout, Action<Byte> callback, CancellationToken token)
        {
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
		/// <param name="cancellation">
		/// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
		/// </param>
		/// <returns>
		/// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
		/// <c>false</c>.
		/// </returns>
		public static Boolean WaitAll([NotNull] this IEnumerable<Task> tasks, Int32 timeout, CancellationToken cancellation)
		{
			return Task.WaitAll(tasks.ToArray(), timeout, cancellation);
		}

		/// <summary>
		/// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
		/// <see cref="TimeSpan"/> or until the wait is cancelled.
		/// </summary>
		/// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
		/// <param name="timeout">
		/// A <see cref="TimeSpan"/> to wait, or <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
		/// </param>
		/// <param name="cancellation">
		/// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
		/// </param>
		/// <returns>
		/// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
		/// <c>false</c>.
		/// </returns>
		public static Boolean WaitAll([NotNull] this IEnumerable<Task> tasks, TimeSpan timeout, CancellationToken cancellation)
		{
			return Task.WaitAll(tasks.ToArray(), (Int32) timeout.TotalMilliseconds, cancellation);
		}

		/// <summary>
		/// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
		/// milliseconds or until the wait is cancelled.
		/// </summary>
		/// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
		/// <param name="cancellation">
		/// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
		/// </param>
		/// <returns>
		/// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
		/// <c>false</c>.
		/// </returns>
		public static void WaitAll([NotNull] this IEnumerable<Task> tasks, CancellationToken cancellation)
		{
			Task.WaitAll(tasks.ToArray(), cancellation);
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
		public static Boolean WaitAll([NotNull] this IEnumerable<Task> tasks, Int32 timeout)
		{
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
		public static Boolean WaitAll([NotNull] this IEnumerable<Task> tasks, TimeSpan timeout)
		{
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
		public static void WaitAll([NotNull] this IEnumerable<Task> tasks)
		{
			Task.WaitAll(tasks.ToArray());
		}

		/// <summary>
		/// Creates a task that will complete when all of the <see cref="Task"/> objects in an enumerable collection
		/// have completed.
		/// </summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		[NotNull]
		// ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
		public static Task WhenAll([NotNull, ItemNotNull] this IEnumerable<Task> tasks)
		{
			return Task.WhenAll(tasks);
		}

		/// <summary>
		/// Creates a task that will complete when all of the <see cref="Task{T}"/> objects in an enumerable collection
		/// have completed.
		/// </summary>
		/// <typeparam name="T">The type of the completed Task.</typeparam>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		[NotNull]
		[ItemNotNull]
		// ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
		public static Task<T[]> WhenAll<T>([NotNull, ItemNotNull] this IEnumerable<Task<T>> tasks)
		{
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
		[NotNull]
		[ItemNotNull]
		// ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
		public static Task<Task<T>> WhenAny<T>([NotNull, ItemNotNull] this IEnumerable<Task<T>> tasks)
		{
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
		[NotNull]
		[ItemNotNull]
		// ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
		public static Task<Task> WhenAny([NotNull, ItemNotNull] this IEnumerable<Task> tasks)
		{
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
		/// <param name="cancellation">
		/// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
		/// </param>
		/// <returns>
		/// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
		/// <c>false</c>.
		/// </returns>
		public static Boolean WaitAll([NotNull] this Task[] tasks, Int32 timeout, CancellationToken cancellation)
		{
			return Task.WaitAll(tasks, timeout, cancellation);
		}

		/// <summary>
		/// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified
		/// <see cref="TimeSpan"/> or until the wait is cancelled.
		/// </summary>
		/// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
		/// <param name="timeout">
		/// A <see cref="TimeSpan"/> to wait, or <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
		/// </param>
		/// <param name="cancellation">
		/// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
		/// </param>
		/// <returns>
		/// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
		/// <c>false</c>.
		/// </returns>
		public static Boolean WaitAll([NotNull] this Task[] tasks, TimeSpan timeout, CancellationToken cancellation)
		{
			return Task.WaitAll(tasks, (Int32) timeout.TotalMilliseconds, cancellation);
		}

		/// <summary>
		/// Waits for all of the provided <see cref="Task"/> objects to complete execution within a specified number of
		/// milliseconds or until the wait is cancelled.
		/// </summary>
		/// <param name="tasks"><see cref="Task"/> instances on which to wait.</param>
		/// <param name="cancellation">
		/// A <see cref="CancellationToken"/> to observe while waiting for the tasks to complete.
		/// </param>
		/// <returns>
		/// <c>true</c> if all of the <see cref="Task"/> instances completed execution within the allotted time; otherwise,
		/// <c>false</c>.
		/// </returns>
		public static void WaitAll([NotNull] this Task[] tasks, CancellationToken cancellation)
		{
			Task.WaitAll(tasks, cancellation);
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
		public static Boolean WaitAll([NotNull] this Task[] tasks, Int32 timeout)
		{
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
		public static Boolean WaitAll([NotNull] this Task[] tasks, TimeSpan timeout)
		{
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
		public static void WaitAll([NotNull] this Task[] tasks)
		{
			Task.WaitAll(tasks);
		}

		/// <summary>
		/// Creates a task that will complete when all of the <see cref="Task"/> objects in an enumerable collection
		/// have completed.
		/// </summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		[NotNull]
		// ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
		public static Task WhenAll([NotNull, ItemNotNull] this Task[] tasks)
		{
			return Task.WhenAll(tasks);
		}

		/// <summary>
		/// Creates a task that will complete when all of the <see cref="Task{T}"/> objects in an enumerable collection
		/// have completed.
		/// </summary>
		/// <typeparam name="T">The type of the completed Task.</typeparam>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		[NotNull]
		[ItemNotNull]
		// ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
		public static Task<T[]> WhenAll<T>([NotNull, ItemNotNull] this Task<T>[] tasks)
		{
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
		[NotNull]
		[ItemNotNull]
		// ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
		public static Task<Task<T>> WhenAny<T>([NotNull, ItemNotNull] this Task<T>[] tasks)
		{
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
		[NotNull]
		[ItemNotNull]
		// ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
		public static Task<Task> WhenAny([NotNull, ItemNotNull] this Task[] tasks)
		{
			return Task.WhenAny(tasks);
		}
		
		public static async void ErrorHandle(this Task<Boolean> task, Action<Exception> action = null)
		{
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
		
		public static async void ErrorHandle(this Task task, Action<Exception> action = null)
		{
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
			return tasks.ToArray();
		}
    }
}