// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class TaskSchedulerUtilities
    {
        public static SynchronizationContext ToSynchronizationContext(this TaskScheduler scheduler)
        {
            return new TaskSchedulerSynchronizationContext(scheduler);
        }

        private sealed class TaskSchedulerSynchronizationContext : SynchronizationContext
        {
            private TaskScheduler Scheduler { get; }

            public TaskSchedulerSynchronizationContext(TaskScheduler scheduler)
            {
                Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            }

            public override void Post(SendOrPostCallback callback, Object? state)
            {
                Task.Factory.StartNew(() => callback(state), CancellationToken.None, TaskCreationOptions.None, Scheduler);
            }

            [SuppressMessage("ReSharper", "AsyncConverter.AsyncWait")]
            public override void Send(SendOrPostCallback callback, Object? state)
            {
                Task task = new Task(() => callback(state));
                task.RunSynchronously(Scheduler);
                task.Wait();
            }
        }
    }
}