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
        private readonly BlockingCollection<Action> _queue = new BlockingCollection<Action>();
        
        private readonly List<Exception> _exceptions = new List<Exception>();
        
        private readonly Thread[] _workers;
        
        private Int32 _finished;

        public ParallelQueue(Int32 workers, String? name = null)
        {
            _workers = new Thread[Math.Max(1, workers)];

            for (Int32 i = 0; i < workers; i++)
            {
                (_workers[i] = new Thread(Work) { Name = name + i }).Start();
            }
        }

        public void WaitAll()
        {
            if (Interlocked.Exchange(ref _finished, 1) != 0)
            {
                return;
            }

            foreach (Thread _ in _workers)
            {
                _queue.Add(null!);
            }

            foreach (Thread worker in _workers)
            {
                worker.Join();
            }

            _queue.CompleteAdding();

            if (_exceptions.Count > 0)
            {
                throw new AggregateException(_exceptions[0].Message, _exceptions);
            }
        }

        public void EnqueueItem(Action item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _queue.Add(item);
        }

        private void Work()
        {
            foreach (Action? action in _queue.GetConsumingEnumerable())
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (action is null || _exceptions.Count != 0)
                {
                    return;
                }

                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    _exceptions.Add(ex);
                }
            }
        }

        public void Dispose()
        {
            WaitAll();
            _queue.Dispose();
        }
    }
}