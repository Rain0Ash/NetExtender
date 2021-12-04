// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Core.Types.TaskSchedulers
{
    public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        [field: ThreadStatic]
        private static Boolean CurrentThreadIsProcessingItems { get; set; }

        private LinkedList<Task> Tasks { get; }

        private Int32 DelegatesQueuedOrRunning { get; set; }

        public sealed override Int32 MaximumConcurrencyLevel { get; }

        public LimitedConcurrencyLevelTaskScheduler(Int32 concurrency)
        {
            if (concurrency < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(concurrency), concurrency, "Can't be less than 1");
            }

            Tasks = new LinkedList<Task>();
            MaximumConcurrencyLevel = concurrency;
        }

        protected sealed override void QueueTask(Task task)
        {
            lock (Tasks)
            {
                Tasks.AddLast(task);
                if (DelegatesQueuedOrRunning >= MaximumConcurrencyLevel)
                {
                    return;
                }

                ++DelegatesQueuedOrRunning;
                NotifyThreadPoolOfPendingWork();
            }
        }

        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
            {
                CurrentThreadIsProcessingItems = true;
                try
                {
                    while (true)
                    {
                        Task? item;
                        lock (Tasks)
                        {
                            if (Tasks.Count <= 0)
                            {
                                --DelegatesQueuedOrRunning;
                                break;
                            }

                            if (!Tasks.TryDequeue(out item))
                            {
                                break;
                            }
                        }
                        
                        TryExecuteTask(item);
                    }
                }
                finally
                {
                    CurrentThreadIsProcessingItems = false;
                }
            }, null);
        }

        protected sealed override Boolean TryExecuteTaskInline(Task task, Boolean previously)
        {
            if (!CurrentThreadIsProcessingItems)
            {
                return false;
            }

            if (previously)
            {
                TryDequeue(task);
            }

            return TryExecuteTask(task);
        }
        
        protected sealed override Boolean TryDequeue(Task task)
        {
            lock (Tasks)
            {
                return Tasks.Remove(task);
            }
        }

        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            Boolean locked = false;
            try
            {
                Monitor.TryEnter(Tasks, ref locked);
                
                if (locked)
                {
                    return Tasks.ToArray();
                }
                
                throw new NotSupportedException();
            }
            finally
            {
                if (locked)
                {
                    Monitor.Exit(Tasks);
                }
            }
        }
    }
}