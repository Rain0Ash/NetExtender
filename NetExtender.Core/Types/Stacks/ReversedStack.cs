// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Stacks
{
    public class ReversedStack<T> : IReadOnlyCollection<T>, ICollection
    {
        private List<T> Stack { get; }

        public Int32 Count
        {
            get
            {
                return Stack.Count;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Stack).IsSynchronized;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Stack).SyncRoot;
            }
        }

        public ReversedStack()
        {
            Stack = new List<T>();
        }

        public ReversedStack(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Stack = collection.ToList();
        }

        public ReversedStack(Int32 capacity)
        {
            Stack = new List<T>(capacity);
        }

        public Boolean Contains(T item)
        {
            return Stack.Contains(item);
        }

        public T Peek()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException("Stack empty.");
            }

            return Stack[^1];
        }

        public Boolean TryPeek(out T? result)
        {
            if (Count <= 0)
            {
                result = default;
                return false;
            }

            result = Stack[^1];
            return true;
        }

        public T Pop()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException("Stack empty.");
            }

            T item = Stack[^1];
            Stack.RemoveAt(Stack.Count - 1);

            return item;
        }

        public Boolean TryPop(out T? result)
        {
            if (Count <= 0)
            {
                result = default;
                return false;
            }

            result = Stack[^1];
            Stack.RemoveAt(Stack.Count - 1);

            return true;
        }

        public void Push(T item)
        {
            Stack.Add(item);
        }

        public void Clear()
        {
            Stack.Clear();
        }

        public void CopyTo(Array array, Int32 index)
        {
            ((ICollection) Stack).CopyTo(array, index);
        }

        public void CopyTo(T[] array, Int32 index)
        {
            Stack.CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Stack).GetEnumerator();
        }
    }
}