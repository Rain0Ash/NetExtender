// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
    public static class TaskFactoryUtilities
    {
        public static TaskFactory<T> ToGeneric<T>(this TaskFactory factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new TaskFactory<T>(factory.CancellationToken, factory.CreationOptions, factory.ContinuationOptions, factory.Scheduler);
        }

        public static TaskFactory ToNonGeneric<T>(this TaskFactory<T> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new TaskFactory(factory.CancellationToken, factory.CreationOptions, factory.ContinuationOptions, factory.Scheduler);
        }

        public static TaskScheduler GetTaskScheduler(this TaskFactory factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return factory.Scheduler ?? TaskScheduler.Current;
        }

        public static TaskScheduler GetTaskScheduler<T>(this TaskFactory<T> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return factory.Scheduler ?? TaskScheduler.Current;
        }

        public static Task<Task[]> WhenAll(this TaskFactory factory, params Task[] tasks)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return factory.ContinueWhenAll(tasks, complete => complete);
        }

        public static Task<Task<T>[]> WhenAll<T>(this TaskFactory factory, params Task<T>[] tasks)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return factory.ContinueWhenAll(tasks, complete => complete);
        }

        public static Task<Task> WhenAny(this TaskFactory factory, params Task[] tasks)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return factory.ContinueWhenAny(tasks, complete => complete);
        }

        public static Task<Task<T>> WhenAny<T>(this TaskFactory factory, params Task<T>[] tasks)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return factory.ContinueWhenAny(tasks, complete => complete);
        }

        public static Task Create(this TaskFactory factory, Action action)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return new Task(action, factory.CancellationToken, factory.CreationOptions);
        }

        public static Task Create(this TaskFactory factory, Action action, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return new Task(action, factory.CancellationToken, options);
        }

        public static Task Create(this TaskFactory factory, Action<Object?> action, Object? state)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return new Task(action, state, factory.CancellationToken, factory.CreationOptions);
        }

        public static Task Create(this TaskFactory factory, Action<Object?> action, Object? state, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return new Task(action, state, factory.CancellationToken, options);
        }

        public static Task<T> Create<T>(this TaskFactory factory, Func<T> function)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new Task<T>(function, factory.CancellationToken, factory.CreationOptions);
        }

        public static Task<T> Create<T>(this TaskFactory factory, Func<T> function, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new Task<T>(function, factory.CancellationToken, options);
        }

        public static Task<T> Create<T>(this TaskFactory factory, Func<Object?, T> function, Object? state)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new Task<T>(function, state, factory.CancellationToken, factory.CreationOptions);
        }

        public static Task<T> Create<T>(this TaskFactory factory, Func<Object?, T> function, Object? state, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new Task<T>(function, state, factory.CancellationToken, options);
        }

        public static Task<T> Create<T>(this TaskFactory<T> factory, Func<T> function)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new Task<T>(function, factory.CancellationToken, factory.CreationOptions);
        }

        public static Task<T> Create<T>(this TaskFactory<T> factory, Func<T> function, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new Task<T>(function, factory.CancellationToken, options);
        }

        public static Task<T> Create<T>(this TaskFactory<T> factory, Func<Object?, T> function, Object? state)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new Task<T>(function, state, factory.CancellationToken, factory.CreationOptions);
        }

        public static Task<T> Create<T>(this TaskFactory<T> factory, Func<Object?, T> function, Object? state, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return new Task<T>(function, state, factory.CancellationToken, options);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout)
        {
            return StartNewDelayed(factory, timeout, CancellationToken.None);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            if (token.IsCancellationRequested)
            {
                return factory.FromCancellation(token);
            }

            TaskCompletionSource<Object?> source = new TaskCompletionSource<Object?>(factory.CreationOptions);
            CancellationTokenRegistration registration = default;

            Timer timer = new Timer(self =>
            {
                // ReSharper disable once AccessToModifiedClosure
                registration.Dispose();

                Timer? timer = self as Timer;
                timer?.Dispose();
                source.TrySetResult(null);
            });

            if (token.CanBeCanceled)
            {
                registration = token.Register(() =>
                {
                    timer.Dispose();
                    source.TrySetCanceled();
                });
            }

            try
            {
                timer.Change(timeout, Timeout.Infinite);
            }
            catch (ObjectDisposedException)
            {
            }

            return source.Task;
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, Action action)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, action, factory.GetTaskScheduler(), factory.CancellationToken);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, Action action, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, action, factory.GetTaskScheduler(), factory.CancellationToken);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, Action action, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, action, factory.GetTaskScheduler(), token);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, Action action, TaskScheduler scheduler, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }
            
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            return factory.StartNewDelayed(timeout, token).ContinueWith(action, token, TaskContinuationOptions.OnlyOnRanToCompletion, scheduler);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, Action<Object?> action, Object? state)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, action, state, factory.GetTaskScheduler(), factory.CancellationToken);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, Action<Object?> action, Object? state, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, action, state, factory.GetTaskScheduler(), factory.CancellationToken);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, Action<Object?> action, Object? state, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, action, state, factory.GetTaskScheduler(), token);
        }

        public static Task StartNewDelayed(this TaskFactory factory, Int32 timeout, Action<Object?> action, Object? state, TaskScheduler scheduler, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }
            
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            TaskCompletionSource<Object?> result = new TaskCompletionSource<Object?>(state);

            void Continuation(Task task)
            {
                if (task.IsCanceled)
                {
                    result.TrySetCanceled(CancellationToken.None);
                    return;
                }

                try
                {
                    action(state);
                    result.TrySetResult(null);
                }
                catch (Exception exception)
                {
                    result.TrySetException(exception);
                }
            }

            factory.StartNewDelayed(timeout, token).ContinueWith(Continuation, scheduler);

            return result.Task;
        }

        public static Task<T> StartNewDelayed<T>(this TaskFactory<T> factory, Int32 timeout, Func<T> function)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, function, factory.CreationOptions, factory.GetTaskScheduler(), factory.CancellationToken);
        }

        public static Task<T> StartNewDelayed<T>(this TaskFactory<T> factory, Int32 timeout, Func<T> function, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, function, options, factory.GetTaskScheduler(), factory.CancellationToken);
        }

        public static Task<T> StartNewDelayed<T>(this TaskFactory<T> factory, Int32 timeout, Func<T> function, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, function, factory.CreationOptions, factory.GetTaskScheduler(), token);
        }

        public static Task<T> StartNewDelayed<T>(this TaskFactory<T> factory, Int32 timeout, Func<T> function, TaskCreationOptions options, TaskScheduler scheduler, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }
            
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            TaskCompletionSource<Object?> source = new TaskCompletionSource<Object?>();
            Timer timer = new Timer(callback => (callback as TaskCompletionSource<Object?>)?.SetResult(null), source, timeout, Timeout.Infinite);

            return source.Task.ContinueWith(_ =>
            {
                timer.Dispose();
                return function();
                
            }, token, options.ToContinuationOptions(), scheduler);
        }

        public static Task<T> StartNewDelayed<T>(this TaskFactory<T> factory, Int32 timeout, Func<Object?, T> function, Object? state)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, function, state, factory.CreationOptions, factory.GetTaskScheduler(), factory.CancellationToken);
        }

        public static Task<T> StartNewDelayed<T>(this TaskFactory<T> factory, Int32 timeout, Func<Object?, T> function, Object? state, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, function, state, factory.CreationOptions, factory.GetTaskScheduler(), token);
        }

        public static Task<T> StartNewDelayed<T>(this TaskFactory<T> factory, Int32 timeout, Func<Object?, T> function, Object? state, TaskCreationOptions options)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return StartNewDelayed(factory, timeout, function, state, options, factory.GetTaskScheduler(), factory.CancellationToken);
        }

        public static Task<T> StartNewDelayed<T>(this TaskFactory<T> factory, Int32 timeout, Func<Object?, T> function, Object? state,
            TaskCreationOptions options, TaskScheduler scheduler, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (scheduler is null)
            {
                throw new ArgumentNullException(nameof(scheduler));
            }

            Timer? timer = null;
            TaskCompletionSource<T> result = new TaskCompletionSource<T>(state);

            Task<T> task = new Task<T>(function, state, options);

            task.ContinueWith(continuation =>
            {
                // ReSharper disable once AccessToModifiedClosure
                timer?.Dispose();
                result.SetFromTask(continuation);
            }, token, options.ToContinuationOptions() | TaskContinuationOptions.ExecuteSynchronously, scheduler);

            timer = new Timer(obj => (obj as Task)?.Start(scheduler), task, timeout, Timeout.Infinite);
            return result.Task;
        }

        public static Task FromException(this TaskFactory factory, Exception exception)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            TaskCompletionSource source = new TaskCompletionSource(factory.CreationOptions);
            source.SetException(exception);
            return source.Task;
        }

        public static Task<T> FromException<T>(this TaskFactory factory, Exception exception)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            TaskCompletionSource<T> source = new TaskCompletionSource<T>(factory.CreationOptions);
            source.SetException(exception);
            return source.Task;
        }

        public static Task<T> FromResult<T>(this TaskFactory factory, T result)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            TaskCompletionSource<T> source = new TaskCompletionSource<T>(factory.CreationOptions);
            source.SetResult(result);
            return source.Task;
        }

        public static Task FromCancellation(this TaskFactory factory, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new Task(ActionUtilities.Default, token);
        }

        public static Task<T?> FromCancellation<T>(this TaskFactory factory, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new Task<T?>(FuncUtilities.Default<T>, token);
        }

        public static Task<T> FromException<T>(this TaskFactory<T> factory, Exception exception)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            TaskCompletionSource<T> source = new TaskCompletionSource<T>(factory.CreationOptions);
            source.SetException(exception);
            return source.Task;
        }

        public static Task<T> FromResult<T>(this TaskFactory<T> factory, T result)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            TaskCompletionSource<T> source = new TaskCompletionSource<T>(factory.CreationOptions);
            source.SetResult(result);
            return source.Task;
        }

        public static Task<T?> FromCancellation<T>(this TaskFactory<T> factory, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return new Task<T?>(FuncUtilities.Default<T>, token);
        }

        public static Task FromAsync(this TaskFactory factory, WaitHandle handle)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            TaskCompletionSource<Object?> source = new TaskCompletionSource<Object?>();
            RegisteredWaitHandle register = ThreadPool.RegisterWaitForSingleObject(handle, (_, _) => source.TrySetResult(null), null, Timeout.Infinite, true);
            Task<Object?> task = source.Task;
            task.ContinueWith(_ => register.Unregister(null), TaskContinuationOptions.ExecuteSynchronously);
            return task;
        }

        public static TaskContinuationOptions ToContinuationOptions(this TaskCreationOptions options)
        {
            return (TaskContinuationOptions) ((options & TaskCreationOptions.AttachedToParent) | (options & TaskCreationOptions.PreferFairness) |
                                              (options & TaskCreationOptions.LongRunning));
        }
    }
}