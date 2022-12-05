// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Queues
{
    public sealed class PriorityQueue<T> : IReadOnlyCollection<T>
    {
        private SortedDictionary<Int32, Queue<T>> Queue { get; }

        public Int32 Size { get; }

        public Int32 Lower { get; private set; }
        public Int32 Upper { get; private set; }

        public Int32 PriorityCount
        {
            get
            {
                return Queue.Count;
            }
        }

        public Int32 Count { get; private set; }

        public PriorityQueue()
            : this(16)
        {
        }

        public PriorityQueue(Int32 size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }

            Size = size;
            Queue = new SortedDictionary<Int32, Queue<T>>();
        }

        private Int32 LowerKey
        {
            get
            {
                return Queue.Keys.FirstOrDefault();
            }
        }

        private Int32 UpperKey
        {
            get
            {
                return Queue.Keys.LastOrDefault();
            }
        }

        public void TrimExcess()
        {
            foreach ((Int32 key, Queue<T> queue) in Queue)
            {
                if (queue.Count <= 0)
                {
                    Queue.Remove(key);
                    continue;
                }

                queue.TrimExcess();
            }

            Lower = LowerKey;
            Upper = UpperKey;
        }

        public Boolean Contains(T item)
        {
            return Queue.Values.Any(queue => queue.Contains(item));
        }

        public Boolean Contains(Int32 priority, T item)
        {
            return Queue.TryGetValue(priority, out Queue<T>? queue) && queue.Contains(item);
        }

        public T Peek()
        {
            if (TryPeek(out T? value))
            {
                return value;
            }

            throw new InvalidOperationException("Queue is empty.");
        }

        public T Peek(Int32 priority)
        {
            if (TryPeek(priority, out T? value))
            {
                return value;
            }

            throw new InvalidOperationException("Queue is empty.");
        }

        public Boolean TryPeek([MaybeNullWhen(false)] out T value)
        {
            foreach (Queue<T> queue in Queue.Values)
            {
                if (queue.TryPeek(out value))
                {
                    return true;
                }
            }

            value = default;
            return false;
        }

        public Boolean TryPeek(Int32 priority, [MaybeNullWhen(false)] out T value)
        {
            if (Queue.TryGetValue(priority, out Queue<T>? queue))
            {
                return queue.TryPeek(out value);
            }

            value = default;
            return false;
        }

        public void Enqueue(T item)
        {
            Enqueue(Upper, item);
        }

        public void Enqueue(KeyValuePair<Int32, T> item)
        {
            Enqueue(item.Key, item.Value);
        }

        public void Enqueue(Int32 priority, T item)
        {
            if (!Queue.TryGetValue(priority, out Queue<T>? queue))
            {
                queue = new Queue<T>(Size);
                Queue.Add(priority, queue);
            }

            queue.Enqueue(item);

            if (Count++ <= 0)
            {
                Lower = priority;
                Upper = priority;
                return;
            }

            if (priority < Lower)
            {
                Lower = priority;
            }

            if (priority > Upper)
            {
                Upper = priority;
            }
        }

        public void EnqueueRange(IEnumerable<T> source)
        {
            EnqueueRange(Upper, source);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void EnqueueRange(Int32 priority, IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return;
            }

            if (!Queue.TryGetValue(priority, out Queue<T>? queue))
            {
                queue = new Queue<T>(source.CountIfMaterialized() ?? Size);
                Queue.Add(priority, queue);
            }

            if (Count <= 0)
            {
                Lower = priority;
                Upper = priority;
            }

            do
            {
                queue.Enqueue(enumerator.Current);
                Count++;
            } while (enumerator.MoveNext());

            if (priority < Lower)
            {
                Lower = priority;
            }

            if (priority > Upper)
            {
                Upper = priority;
            }
        }

        public void EnqueueRange(IEnumerable<KeyValuePair<Int32, T>> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (IGrouping<Int32, T> grouping in items.GroupManyByKey())
            {
                EnqueueRange(grouping.Key, grouping);
            }
        }

        public T Dequeue()
        {
            if (TryDequeue(out T? value))
            {
                return value;
            }

            throw new InvalidOperationException("Queue is empty.");
        }

        public T Dequeue(Int32 priority)
        {
            if (TryDequeue(priority, out T? value))
            {
                return value;
            }

            throw new InvalidOperationException("Queue is empty.");
        }

        // ReSharper disable once CognitiveComplexity
        public Boolean TryDequeue([MaybeNullWhen(false)] out T result)
        {
            foreach ((Int32 priority, Queue<T> queue) in Queue)
            {
                if (queue.Count <= 0)
                {
                    if (Queue.Remove(priority))
                    {
                        Lower = LowerKey;
                        Upper = UpperKey;
                    }

                    continue;
                }

                if (!queue.TryDequeue(out result))
                {
                    continue;
                }

                Count--;

                if (queue.Count > 0)
                {
                    return true;
                }

                if (!Queue.Remove(priority))
                {
                    return true;
                }

                Lower = LowerKey;
                Upper = UpperKey;

                return true;
            }

            result = default;
            return false;
        }

        // ReSharper disable once CognitiveComplexity
        public Boolean TryDequeue(Int32 priority, [MaybeNullWhen(false)] out T result)
        {
            if (Queue.TryGetValue(priority, out Queue<T>? queue))
            {
                if (queue.Count <= 0)
                {
                    if (Queue.Remove(priority))
                    {
                        Lower = LowerKey;
                        Upper = UpperKey;
                    }

                    result = default;
                    return false;
                }

                if (!queue.TryDequeue(out result))
                {
                    return false;
                }

                Count--;

                if (queue.Count > 0)
                {
                    return true;
                }

                if (!Queue.Remove(priority))
                {
                    return true;
                }

                Lower = LowerKey;
                Upper = UpperKey;

                return true;
            }

            result = default;
            return false;
        }

        public void Clear()
        {
            foreach (Queue<T> queue in Queue.Values)
            {
                queue.Clear();
            }

            Queue.Clear();
            Count = Lower = Upper = 0;
        }

        public void Clear(Int32 priority)
        {
            if (!Queue.Remove(priority, out Queue<T>? queue))
            {
                return;
            }

            Count -= queue.Count;
            queue.Clear();

            if (priority <= Lower)
            {
                Lower = LowerKey;
            }

            if (priority >= Upper)
            {
                Upper = UpperKey;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Queue.Values.SelectMany().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex > array.Length || array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex, null);
            }

            foreach (Queue<T> queue in Queue.Values)
            {
                queue.CopyTo(array, arrayIndex);
                arrayIndex += queue.Count;
            }
        }

        public IReadOnlyCollection<T> this[Int32 priority]
        {
            get
            {
                return Queue.TryGetValue(priority, out Queue<T>? queue) ? queue : Array.Empty<T>();
            }
        }
    }
}