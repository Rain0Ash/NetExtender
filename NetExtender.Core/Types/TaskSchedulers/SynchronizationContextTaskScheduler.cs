// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.TaskSchedulers
{
    public sealed class SynchronizationContextTaskScheduler : TaskScheduler
    {
        private SynchronizationContext? Context { get; }
        private ConcurrentQueue<Task> Tasks { get; }

        public override Int32 MaximumConcurrencyLevel
        {
            get
            {
                return 1;
            }
        }

        public SynchronizationContextTaskScheduler()
            : this(SynchronizationContext.Current)
        {
        }

        public SynchronizationContextTaskScheduler(SynchronizationContext? context)
        {
            Context = context;
            Tasks = new ConcurrentQueue<Task>();
        }

        protected override void QueueTask(Task task)
        {
            Tasks.Enqueue(task);

            void SendOrPostCallback(Object? _)
            {
                if (Tasks.TryDequeue(out Task? nextTask))
                {
                    TryExecuteTask(nextTask);
                }
            }

            Context?.Post(SendOrPostCallback, null);
        }

        protected override Boolean TryExecuteTaskInline(Task task, Boolean previously)
        {
            return Context == SynchronizationContext.Current && TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Tasks.ToArray();
        }
    }
}