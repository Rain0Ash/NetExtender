// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Threading;
using NetExtender.Utilities.Types;

namespace NetExtender.Core.Types.TaskSchedulers
{
    public class STATaskScheduler : TaskScheduler, IDisposable
    {
        public BlockingCollection<Task>? Tasks { get; private set; }
        private List<Thread> Threads { get; }
        
        public override Int32 MaximumConcurrencyLevel
        {
            get
            {
                return Threads.Count;
            }
        }

        public STATaskScheduler(Int32 threads)
        {
            if (threads < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(threads), threads, "Can't be less than 1");
            }

            Tasks = new BlockingCollection<Task>();
            
            void Start()
            {
                foreach (var task in Tasks.GetConsumingEnumerable())
                {
                    TryExecuteTask(task);
                }
            }

            Thread Selector(Int32 _)
            {
                Thread thread = new Thread(Start).SetBackground();
                thread.SetApartmentState(ApartmentState.STA);
                return thread;
            }

            Threads = Enumerable.Range(0, threads).Select(Selector).ForEach(thread => thread.Start()).ToList();
        }

        protected override void QueueTask(Task task)
        {
            if (Tasks is null)
            {
                throw new ObjectDisposedException(nameof(STATaskScheduler));
            }
            
            Tasks.Add(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            if (Tasks is null)
            {
                throw new ObjectDisposedException(nameof(STATaskScheduler));
            }
            
            return Tasks.ToArray();
        }

        protected override Boolean TryExecuteTaskInline(Task task, Boolean previously)
        {
            return Thread.CurrentThread.GetApartmentState() == ApartmentState.STA && TryExecuteTask(task);
        }

        public void Dispose()
        {
            if (Tasks is null)
            {
                return;
            }
            
            Tasks.CompleteAdding();

            foreach (Thread thread in Threads)
            {
                thread.Join();
            }

            // Cleanup
            Tasks.Dispose();
            Tasks = null;
            
            GC.SuppressFinalize(this);
        }
    }
}