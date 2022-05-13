// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Queues;

namespace NetExtender.Utilities.Types
{
    public static class QueueUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Queue<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this Queue<T> queue, T item)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            queue.Enqueue(item);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this Queue<T> queue, params T[] items)
        {
            EnqueueRange(queue, items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            EnqueueRange(queue, items);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this SetQueue<T> queue, params T[] items)
        {
            EnqueueRange(queue, items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this SetQueue<T> queue, IEnumerable<T> items)
        {
            EnqueueRange(queue, items);
        }

        public static T Rotate<T>(this Queue<T> queue)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (queue.Count <= 1)
            {
                return queue.Peek();
            }

            T item = queue.Dequeue();
            queue.Enqueue(item);
            return item;
        }

        public static T Rotate<T>(this Queue<T> queue, Int32 offset)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }
            
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (queue.Count <= 1 || (offset %= queue.Count) == 0)
            {
                return queue.Peek();
            }

            T item = default!;
            for (Int32 i = 0; i < offset % queue.Count; i++)
            {
                item = queue.Dequeue();
                queue.Enqueue(item);
            }

            return item;
        }
        
        public static Boolean TryRotate<T>(this Queue<T> queue, [MaybeNullWhen(false)] out T result)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (queue.Count <= 1)
            {
                return queue.TryPeek(out result);
            }

            if (!queue.TryDequeue(out result))
            {
                return false;
            }
            
            queue.Enqueue(result);
            return true;
        }
        
        public static Boolean TryRotate<T>(this Queue<T> queue, Int32 offset, [MaybeNullWhen(false)] out T result)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (queue.Count <= 0)
            {
                result = default;
                return false;
            }
            
            if (offset < 0)
            {
                result = default;
                return false;
            }
            
            if (queue.Count <= 1 || (offset %= queue.Count) == 0)
            {
                return queue.TryPeek(out result);
            }

            result = default!;
            for (Int32 i = 0; i < offset; i++)
            {
                if (!queue.TryDequeue(out result))
                {
                    return false;
                }
                        
                queue.Enqueue(result);
            }

            return true;
        }
        
        public static T Rotate<T>(this SetQueue<T> queue)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            T item = queue.Dequeue();
            queue.Enqueue(item);
            return item;
        }
        
        public static T Rotate<T>(this SetQueue<T> queue, Int32 offset)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }
            
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (queue.Count <= 1 || (offset %= queue.Count) == 0)
            {
                return queue.Peek();
            }

            T item = default!;
            for (Int32 i = 0; i < offset; i++)
            {
                item = queue.Dequeue();
                queue.Enqueue(item);
            }

            return item;
        }
        
        public static Boolean TryRotate<T>(this SetQueue<T> queue, [MaybeNullWhen(false)] out T result)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (!queue.TryDequeue(out result))
            {
                return false;
            }
            
            queue.Enqueue(result);
            return true;
        }
        
        public static Boolean TryRotate<T>(this SetQueue<T> queue, Int32 offset, [MaybeNullWhen(false)] out T result)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (queue.Count <= 0)
            {
                result = default;
                return false;
            }
            
            if (offset < 0)
            {
                result = default;
                return false;
            }

            if (queue.Count <= 1 || (offset %= queue.Count) == 0)
            {
                return queue.TryPeek(out result);
            }

            result = default!;
            for (Int32 i = 0; i < offset; i++)
            {
                if (!queue.TryDequeue(out result))
                {
                    return false;
                }
                        
                queue.Enqueue(result);
            }

            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnqueueRange<T>(this Queue<T> queue, params T[] items)
        {
            EnqueueRange(queue, (IEnumerable<T>) items);
        }

        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            
            foreach (T item in items)
            {
                queue.Enqueue(item);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnqueueRange<T>(this SetQueue<T> queue, params T[] items)
        {
            EnqueueRange(queue, (IEnumerable<T>) items);
        }

        public static void EnqueueRange<T>(this SetQueue<T> queue, IEnumerable<T> items)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            
            foreach (T item in items)
            {
                queue.Enqueue(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DequeueMultiple<T>(this Queue<T> queue)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            while (queue.TryDequeue(out T? value))
            {
                yield return value;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DequeueMultiple<T>(this SetQueue<T> queue)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            while (queue.TryDequeue(out T? value))
            {
                yield return value;
            }
        }
    }
}