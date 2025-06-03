using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.History.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.History
{
    public abstract partial class TimeHistoryCollection<T> : TimeHistoryCollection<T, TimeHistoryCollection<T>.Node>, ITimeHistoryCollection<T>, IReadOnlyTimeHistoryCollection<T>
    {
        protected abstract override ICollection<Node> Internal { get; }
        protected override NodeComparer Comparer { get; }

        protected TimeHistoryCollection(IComparer<T>? comparer)
            : this(NodeComparer.Create(comparer))
        {
        }

        protected TimeHistoryCollection(Comparison<T>? comparison)
            : this(comparison?.ToComparer())
        {
        }
        
        protected TimeHistoryCollection(NodeComparer? comparer)
        {
            Comparer = comparer ?? NodeComparer.Default;
        }

        protected override Node Create(T value)
        {
            return new Node(this, value);
        }

        protected override Node Universal(T value)
        {
            return Node.Universal(this, value);
        }
    }

    public abstract class TimeHistoryCollection<T, TNode> : ITimeHistoryCollection<T, TNode>, IReadOnlyTimeHistoryCollection<T, TNode> where TNode : struct, TimeHistoryCollection<T, TNode>.INode
    {
        protected abstract ICollection<TNode> Internal { get; }
        protected abstract IComparer<TNode> Comparer { get; }

        IComparer<TNode> ITimeHistoryCollection<T, TNode>.Comparer
        {
            get
            {
                return Comparer;
            }
        }
        
        IComparer<TNode> IReadOnlyTimeHistoryCollection<T, TNode>.Comparer
        {
            get
            {
                return Comparer;
            }
        }

        public virtual Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public abstract DateTimeOffset? Min { get; }
        public abstract DateTimeOffset? Max { get; }

        private Int32 _limit = Int32.MaxValue;
        public Int32 Limit
        {
            get
            {
                return _limit < 0 ? _limit = Int32.MaxValue : _limit;
            }
            set
            {
                _limit = value < 0 ? Int32.MaxValue : value;
                Trim();
            }
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        protected abstract TNode Create(T value);
        protected abstract TNode Universal(T value);
        
        public abstract Boolean Contains(T item);
        public abstract Int32 Add(T item);
        
        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public abstract Boolean Remove(T item);

        public virtual void Trim()
        {
            Trim(Limit);
        }

        public abstract void Trim(Int32 size);

        public virtual void Clear()
        {
            Internal.Clear();
        }
        
        public virtual void CopyTo(T[] array)
        {
           CopyTo(array, 0);
        }

        public virtual void CopyTo(T[] array, Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            using IEnumerator<TNode> enumerator = Internal.GetEnumerator();

            for (Int32 i = index; i < array.Length && enumerator.MoveNext(); i++)
            {
                array[i] = enumerator.Current.Value;
            }
        }

        public virtual void CopyTo(TNode[] array)
        {
            CopyTo(array, 0);
        }

        public virtual void CopyTo(TNode[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (TNode node in Internal)
            {
                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }
        
        public interface INode : ITimeHistoryCollectionNode<T>, IEquality<TNode>
        {
        }
    }
}