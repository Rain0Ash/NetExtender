// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Collections
{
    [Flags]
    public enum ProducerConsumerCollectionType : Byte
    {
        None = 0,
        Produce = 1,
        Consume = 2,
        All = 3
    }

    public sealed class ProducerOrConsumerCollection<T> : IProducerConsumerCollection<T>
    {
        public IProducerConsumerCollection<T> Internal { get; }
        private ProducerConsumerCollectionType Type { get; }
        
        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsSynchronized
        {
            get
            {
                return Internal.IsSynchronized;
            }
        }

        public Object SyncRoot
        {
            get
            {
                return Internal.SyncRoot;
            }
        }

        public ProducerOrConsumerCollection(IProducerConsumerCollection<T> collection, ProducerConsumerCollectionType type)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Type = type;
        }

        public Boolean TryAdd(T item)
        {
            return Type.HasFlag(ProducerConsumerCollectionType.Produce) && Internal.TryAdd(item);
        }

        public Boolean TryTake([MaybeNullWhen(false)] out T item)
        {
            if (Type.HasFlag(ProducerConsumerCollectionType.Consume))
            {
                return Internal.TryTake(out item);
            }

            item = default;
            return false;
        }

        public void CopyTo(Array array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }
        
        public void CopyTo(T[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public T[] ToArray()
        {
            return Internal.ToArray();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Internal).GetEnumerator();
        }
    }
}