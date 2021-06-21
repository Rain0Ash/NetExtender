// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.Queues
{
    public class TaskQueue
    {
        private readonly ConcurrentQueue<Func<Task>> _processingQueue = new ConcurrentQueue<Func<Task>>();
        private readonly ConcurrentDictionary<Int32, Task> _runningTasks = new ConcurrentDictionary<Int32, Task>();
        private readonly Int32 _maxParallelizationCount;
        private readonly Int32 _maxQueueLength;
        private TaskCompletionSource<Boolean> _queue = new TaskCompletionSource<Boolean>();

        public TaskQueue(Int32 maxParallelizationCount = Int32.MaxValue, Int32 maxQueueLength = Int32.MaxValue)
        {
            _maxParallelizationCount = maxParallelizationCount;
            _maxQueueLength = maxQueueLength;
        }

        public Boolean Enqueue(Func<Task> futureTask)
        {
            if (_processingQueue.Count >= _maxQueueLength)
            {
                return false;
            }

            _processingQueue.Enqueue(futureTask);
            return true;
        }

        public Int32 GetQueueCount()
        {
            return _processingQueue.Count;
        }

        public Int32 GetRunningCount()
        {
            return _runningTasks.Count;
        }

        public Task ProcessAsync()
        {
            Task<Boolean> task = _queue.Task;
            StartTasks();
            return task;
        }

        public void ProcessBackground(Action<Exception>? exception = null)
        {
            Task.Run(ProcessAsync).ContinueWith(task => { exception?.Invoke(task.Exception); }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void StartTasks()
        {
            Int32 startMaxCount = _maxParallelizationCount - _runningTasks.Count;
            for (Int32 i = 0; i < startMaxCount; i++)
            {
                if (!_processingQueue.TryDequeue(out Func<Task>? future))
                {
                    // Queue is most likely empty
                    break;
                }

                Task t = Task.Run(future);
                if (!_runningTasks.TryAdd(t.GetHashCode(), t))
                {
                    throw new Exception("Should not happen, hash codes are unique");
                }

                t.ContinueWith(t2 =>
                {
                    if (!_runningTasks.TryRemove(t2.GetHashCode(), out Task _))
                    {
                        throw new Exception("Should not happen, hash codes are unique");
                    }

                    // Continue the queue processing
                    StartTasks();
                });
            }

            if (!_processingQueue.IsEmpty || !_runningTasks.IsEmpty)
            {
                return;
            }

            // Interlocked.Exchange might not be necessary
            TaskCompletionSource<Boolean> oldQueue = Interlocked.Exchange(
                ref _queue, new TaskCompletionSource<Boolean>());
            oldQueue.TrySetResult(true);
        }
    }
}