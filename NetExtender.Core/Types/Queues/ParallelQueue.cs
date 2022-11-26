// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace NetExtender.Types.Queues
{
    internal sealed class ParallelQueue : IDisposable
    {
        private BlockingCollection<Action?> Queue { get; } = new BlockingCollection<Action?>();

        private List<Exception> Exceptions { get; } = new List<Exception>();

        private Thread[] Workers { get; }

        private Int32 _finished;

        public ParallelQueue(Int32 workers, String? name = null)
        {
            Workers = new Thread[Math.Max(1, workers)];

            for (Int32 i = 0; i < workers; i++)
            {
                (Workers[i] = new Thread(Work) { Name = name + i }).Start();
            }
        }

        public void Enqueue(Action item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Queue.Add(item);
        }

        public void WaitAll()
        {
            if (Interlocked.Exchange(ref _finished, 1) != 0)
            {
                return;
            }

            foreach (Thread _ in Workers)
            {
                Queue.Add(null);
            }

            foreach (Thread worker in Workers)
            {
                worker.Join();
            }

            Queue.CompleteAdding();

            if (Exceptions.Count > 0)
            {
                throw new AggregateException(Exceptions[0].Message, Exceptions);
            }
        }

        private void Work()
        {
            foreach (Action? action in Queue.GetConsumingEnumerable())
            {
                if (action is null || Exceptions.Count != 0)
                {
                    return;
                }

                try
                {
                    action();
                }
                catch (Exception exception)
                {
                    Exceptions.Add(exception);
                }
            }
        }

        public void Dispose()
        {
            WaitAll();
            Queue.Dispose();
        }
    }
}