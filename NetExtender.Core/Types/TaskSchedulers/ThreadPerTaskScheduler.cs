// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Threading;

namespace NetExtender.Core.Types.TaskSchedulers
{
    public class ThreadPerTaskScheduler : TaskScheduler
    {
        protected override void QueueTask(Task task)
        {
            void Start()
            {
                TryExecuteTask(task);
            }

            Thread thread = new Thread(Start).SetBackground();
            thread.Start();
        }

        protected override Boolean TryExecuteTaskInline(Task task, Boolean previously)
        {
            return TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Enumerable.Empty<Task>();
        }
    }
}