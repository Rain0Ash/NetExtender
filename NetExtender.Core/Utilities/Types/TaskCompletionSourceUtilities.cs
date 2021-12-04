// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class TaskCompletionSourceUtilities
    {
        public static Boolean SetNotNullException(this TaskCompletionSource source, Exception? exception)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (exception is null)
            {
                return false;
            }
            
            source.SetException(exception);
            return true;
        }
        
        public static Boolean SetNotNullException(this TaskCompletionSource source, IEnumerable<Exception?>? exception)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (exception is null)
            {
                return false;
            }
            
            source.SetException(exception.WhereNotNull());
            return true;
        }
        
        public static Boolean TrySetNotNullException(this TaskCompletionSource source, Exception? exception)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return exception is not null && source.TrySetException(exception);
        }
        
        public static Boolean TrySetNotNullException(this TaskCompletionSource source, IEnumerable<Exception?>? exception)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return exception is not null && source.TrySetException(exception.WhereNotNull());
        }
        
        public static Boolean SetNotNullException<T>(this TaskCompletionSource<T> source, Exception? exception)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (exception is null)
            {
                return false;
            }
            
            source.SetException(exception);
            return true;
        }
        
        public static Boolean SetNotNullException<T>(this TaskCompletionSource<T> source, IEnumerable<Exception?>? exception)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (exception is null)
            {
                return false;
            }
            
            source.SetException(exception.WhereNotNull());
            return true;
        }
        
        public static Boolean TrySetNotNullException<T>(this TaskCompletionSource<T> source, Exception? exception)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return exception is not null && source.TrySetException(exception);
        }
        
        public static Boolean TrySetNotNullException<T>(this TaskCompletionSource<T> source, IEnumerable<Exception?>? exception)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return exception is not null && source.TrySetException(exception.WhereNotNull());
        }
        
        public static TaskCompletionSource SetFromTask(this TaskCompletionSource source, Task task)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    source.SetResult();
                    return source;
                case TaskStatus.Faulted:
                    source.SetNotNullException(task.Exception?.InnerExceptions);
                    return source;
                case TaskStatus.Canceled:
                    source.SetCanceled();
                    return source;
                default:
                    throw new InvalidOperationException("The task was not completed.");
            }
        }

        public static TaskCompletionSource<T?> SetFromTask<T>(this TaskCompletionSource<T?> source, Task task)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (task is Task<T> result)
            {
                return SetFromTask(source!, result)!;
            }
            
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    source.SetResult(default);
                    return source;
                case TaskStatus.Faulted:
                    source.SetNotNullException(task.Exception?.InnerExceptions);
                    return source;
                case TaskStatus.Canceled:
                    source.SetCanceled();
                    return source;
                default:
                    throw new InvalidOperationException("The task was not completed.");
            }
        }

        public static TaskCompletionSource<T> SetFromTask<T>(this TaskCompletionSource<T> source, Task<T> task)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    source.SetResult(task.Result);
                    return source;
                case TaskStatus.Faulted:
                    source.SetNotNullException(task.Exception?.InnerExceptions);
                    return source;
                case TaskStatus.Canceled:
                    source.SetCanceled();
                    return source;
                default:
                    throw new InvalidOperationException("The task was not completed.");
            }
        }
        
        public static Boolean TrySetFromTask(this TaskCompletionSource source, Task task)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            return task.Status switch
            {
                TaskStatus.RanToCompletion => source.TrySetResult(),
                TaskStatus.Faulted => source.TrySetNotNullException(task.Exception?.InnerExceptions),
                TaskStatus.Canceled => source.TrySetCanceled(),
                _ => false
            };
        }

        public static Boolean TrySetFromTask<T>(this TaskCompletionSource<T?> source, Task task)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (task is Task<T> result)
            {
                return TrySetFromTask(source!, result);
            }

            return task.Status switch
            {
                TaskStatus.RanToCompletion => source.TrySetResult(default),
                TaskStatus.Faulted => source.TrySetNotNullException(task.Exception?.InnerExceptions),
                TaskStatus.Canceled => source.TrySetCanceled(),
                _ => false
            };
        }

        public static Boolean TrySetFromTask<T>(this TaskCompletionSource<T> source, Task<T> task)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            return task.Status switch
            {
                TaskStatus.RanToCompletion => source.TrySetResult(task.Result),
                TaskStatus.Faulted => source.TrySetNotNullException(task.Exception?.InnerExceptions),
                TaskStatus.Canceled => source.TrySetCanceled(),
                _ => false
            };
        }
    }
}