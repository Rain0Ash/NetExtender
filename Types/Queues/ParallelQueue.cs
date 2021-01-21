// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;

namespace NetExtender.Types.Queues
{
    [PublicAPI]
    internal sealed class ParallelQueue : IDisposable
    {
        [NotNull, ItemNotNull]
        private readonly BlockingCollection<Action> _queue = new BlockingCollection<Action>();
        [NotNull, ItemNotNull]
        private readonly List<Exception> _exceptions = new List<Exception>();
        [NotNull, ItemNotNull]
        private readonly Thread[] _workers;

        public ParallelQueue(Int32 workerCount, String name = null)
        {
            _workers = new Thread[System.Math.Max(1, workerCount)];

            for (Int32 i = 0; i < workerCount; i++)
            {
                (_workers[i] = new Thread(Work) { Name = name + i }).Start();
            }
        }

        private Int32 _isFinished;

        public void WaitAll()
        {
            if (Interlocked.Exchange(ref _isFinished, 1) != 0)
            {
                return;
            }

            foreach (Thread _ in _workers)
            {
                _queue.Add(null);
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

        public void EnqueueItem([NotNull] Action item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _queue.Add(item);
        }

        private void Work()
        {
            foreach (Action action in _queue.GetConsumingEnumerable())
            {
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