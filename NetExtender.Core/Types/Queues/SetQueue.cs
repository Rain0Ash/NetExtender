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
    public class SetQueue<T> : IReadOnlySet<T>, ICollection<T>, ICollection
    {
        private LinkedList<T> Queue { get; }
        private HashSet<T> Set { get; }
        
        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return ((ICollection<T>) Set).IsReadOnly;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Queue).SyncRoot;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Queue).IsSynchronized;
            }
        }

        public Int32 Count
        {
            get
            {
                return Queue.Count;
            }
        }

        public SetQueue()
            : this((IEqualityComparer<T>?) null)
        {
        }

        public SetQueue(IEqualityComparer<T>? comparer)
        {
            Queue = new LinkedList<T>();
            Set = new HashSet<T>(comparer);
        }
        
        public SetQueue(Int32 capacity)
            : this(capacity, null)
        {
        }
        
        public SetQueue(Int32 capacity, IEqualityComparer<T>? comparer)
        {
            Queue = new LinkedList<T>();
            Set = new HashSet<T>(capacity, comparer);
        }
        
        public SetQueue(IEnumerable<T> source)
            : this(source, null)
        {
        }
        
        public SetQueue(IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 capacity = source.CountIfMaterialized() ?? 16;
            Queue = new LinkedList<T>();
            Set = new HashSet<T>(capacity, comparer);
        }

        public void EnsureCapacity(Int32 capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            Set.EnsureCapacity(capacity);
        }

        public void TrimExcess()
        {
            Set.TrimExcess();
        }

        public Boolean Contains(T item)
        {
            return Set.Contains(item);
        }

        public T Peek()
        {
            return Queue.Peek();
        }
        
        public Boolean TryPeek([MaybeNullWhen(false)] out T result)
        {
            return Queue.TryPeek(out result);
        }

        public Boolean Enqueue(T item)
        {
            if (!Set.Add(item))
            {
                return false;
            }

            Queue.Enqueue(item);
            return true;
        }

        public Boolean Rotate(T item)
        {
            if (!Set.Contains(item))
            {
                return false;
            }
            
            Queue.Remove(item);
            Queue.AddLast(item);
            return true;
        }

        public Boolean RotateEnqueue(T item)
        {
            if (!Set.Add(item))
            {
                Queue.Remove(item);
            }
            
            Queue.AddLast(item);
            return true;
        }

        public T Dequeue()
        {
            T item = Queue.Dequeue();
            Set.Remove(item);
            return item;
        }
        
        public Boolean TryDequeue([MaybeNullWhen(false)] out T result)
        {
            if (!Queue.TryDequeue(out result))
            {
                return false;
            }

            Set.Remove(result);
            return true;
        }

        public Boolean Add(T item)
        {
            return Enqueue(item);
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }
        
        public Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsProperSubsetOf(other);
        }

        public Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsProperSupersetOf(other);
        }

        public Boolean IsSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsSubsetOf(other);
        }

        public Boolean IsSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsSupersetOf(other);
        }

        public Boolean Overlaps(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.Overlaps(other);
        }

        public Boolean SetEquals(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.SetEquals(other);
        }

        public Boolean Remove(T item)
        {
            if (!Set.Remove(item))
            {
                return false;
            }

            Queue.Remove(item);
            return true;
        }
        
        public void Clear()
        {
            Queue.Clear();
            Set.Clear();
        }
        
        public T[] ToArray()
        {
            return Queue.ToArray();
        }
        
        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            Queue.CopyTo(array, arrayIndex);
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((ICollection) Queue).CopyTo(array, index);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Queue.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Queue.GetEnumerator();
        }
    }
}