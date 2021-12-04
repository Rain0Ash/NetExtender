// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetExtender.Core.Types.TaskSchedulers
{
    public class CurrentThreadTaskScheduler : TaskScheduler
    {
        public override Int32 MaximumConcurrencyLevel
        {
            get
            {
                return 1;
            }
        }
        
        protected override void QueueTask(Task task)
        {
            TryExecuteTask(task);
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