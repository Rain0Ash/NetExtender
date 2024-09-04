// ReSharper disable RedundantUsingDirective
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Nodes;
using NetExtender.Types.LinkedLists;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Lists
{
    public class LinkedListNode<T, TNode> : LinkedListNode<T, TNode, LinkedList<T, TNode>> where TNode : LinkedListNode<T, TNode>
    {
        /// <inheritdoc cref="LinkedListNode{T}(T)"/>
        public LinkedListNode(T value)
            : base(value)
        {
        }
        
        protected internal LinkedListNode(LinkedList<T, TNode> list, T value)
            : base(list, value)
        {
        }
    }
    
    public class LinkedListNode<T, TNode, TList> : LinkedNode<T, TNode>, ILinkedListNode<T, TNode, TList>, IReadOnlyLinkedListNode<T, TNode, TList>, IEquatable<LinkedListNode<T, TNode, TList>> where TNode : LinkedListNode<T, TNode, TList> where TList : LinkedList<T, TNode, TList>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(LinkedListNode<T, TNode, TList>? value)
        {
            return value is not null ? value.ValueRef : default;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator TNode?(LinkedListNode<T, TNode, TList>? value)
        {
            return (TNode?) value;
        }
        
        /// <inheritdoc cref="LinkedListNode{T}.List"/>
        public TList? List { get; protected internal set; }
        
        protected sealed override TNode? Head
        {
            get
            {
                return List?.Head;
            }
        }
        
        private T _value;
        
        /// <inheritdoc cref="LinkedListNode{T}.Value"/>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        
        /// <inheritdoc cref="LinkedListNode{T}.ValueRef"/>
        public ref T ValueRef
        {
            get
            {
                return ref _value;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.First"/>
        public sealed override TNode? First
        {
            get
            {
                return List?.First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.First"/>
        ILinkedListNode<T>? ILinkedListNode<T>.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.First"/>
        ILinkedListNode? ILinkedListNode.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Last"/>
        public sealed override TNode? Last
        {
            get
            {
                return List?.Last;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Last"/>
        ILinkedListNode<T>? ILinkedListNode<T>.Last
        {
            get
            {
                return Last;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Last"/>
        ILinkedListNode? ILinkedListNode.Last
        {
            get
            {
                return Last;
            }
        }

        /// <inheritdoc cref="LinkedListNode{T}.Previous"/>
        public sealed override TNode? Previous
        {
            get
            {
                return base.Previous;
            }
        }
        
        /// <inheritdoc cref="LinkedListNode{T}.Previous"/>
        ILinkedListNode<T>? ILinkedListNode<T>.Previous
        {
            get
            {
                return Previous;
            }
        }
        
        /// <inheritdoc cref="LinkedListNode{T}.Previous"/>
        ILinkedListNode? ILinkedListNode.Previous
        {
            get
            {
                return Previous;
            }
        }
        
        /// <inheritdoc cref="LinkedListNode{T}.Next"/>
        public sealed override TNode? Next
        {
            get
            {
                return base.Next;
            }
            protected internal set
            {
                base.Next = value;
            }
        }
        
        /// <inheritdoc cref="LinkedListNode{T}.Next"/>
        ILinkedListNode<T>? ILinkedListNode<T>.Next
        {
            get
            {
                return Next;
            }
        }
        
        /// <inheritdoc cref="LinkedListNode{T}.Next"/>
        ILinkedListNode? ILinkedListNode.Next
        {
            get
            {
                return Next;
            }
        }
        
        /// <inheritdoc cref="LinkedListNode{T}(T)"/>
        public LinkedListNode(T value)
        {
            _value = value;
        }
        
        protected internal LinkedListNode(TList list, T value)
        {
            List = list ?? throw new ArgumentNullException(nameof(list));
            _value = value;
        }
        
        protected internal override void Invalidate()
        {
            List = null;
            base.Invalidate();
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(T)"/>
        public sealed override TNode? Find(T value)
        {
            return List?.Find(value);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(T)"/>
        ILinkedListNode<T>? ILinkedListNode<T>.Find(T value)
        {
            return Find(value);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public sealed override TNode? Find(T value, IEqualityComparer<T>? comparer)
        {
            return List?.Find(value, comparer);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(T,System.Collections.Generic.IEqualityComparer{T})"/>
        ILinkedListNode<T>? ILinkedListNode<T>.Find(T value, IEqualityComparer<T>? comparer)
        {
            return Find(value, comparer);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(System.Predicate{T})"/>
        public sealed override TNode? Find(Predicate<T> predicate)
        {
            return List?.Find(predicate);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(System.Predicate{T})"/>
        ILinkedListNode<T>? ILinkedListNode<T>.Find(Predicate<T> predicate)
        {
            return Find(predicate);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(System.Predicate{TNode})"/>
        public sealed override TNode? Find(Predicate<TNode> predicate)
        {
            return List?.Find(predicate);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(System.Predicate{ILinkedListNode{T}})"/>
        ILinkedListNode<T>? ILinkedListNode<T>.Find(Predicate<ILinkedListNode<T>> predicate)
        {
            return Find(predicate);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T)"/>
        public sealed override TNode? FindLast(T value)
        {
            return List?.FindLast(value);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T)"/>
        ILinkedListNode<T>? ILinkedListNode<T>.FindLast(T value)
        {
            return FindLast(value);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public sealed override TNode? FindLast(T value, IEqualityComparer<T>? comparer)
        {
            return List?.FindLast(value, comparer);
        }

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T,System.Collections.Generic.IEqualityComparer{T})"/>
        ILinkedListNode<T>? ILinkedListNode<T>.FindLast(T value, IEqualityComparer<T>? comparer)
        {
            return FindLast(value, comparer);
        }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(System.Predicate{T})"/>
        public sealed override TNode? FindLast(Predicate<T> predicate)
        {
            return List?.FindLast(predicate);
        }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(System.Predicate{T})"/>
        ILinkedListNode<T>? ILinkedListNode<T>.FindLast(Predicate<T> predicate)
        {
            return FindLast(predicate);
        }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(System.Predicate{TNode})"/>
        public sealed override TNode? FindLast(Predicate<TNode> predicate)
        {
            return List?.FindLast(predicate);
        }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(System.Predicate{ILinkedListNode{T}})"/>
        ILinkedListNode<T>? ILinkedListNode<T>.FindLast(Predicate<ILinkedListNode<T>> predicate)
        {
            return FindLast(predicate);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindPrevious(T value)
        {
            return FindPrevious(value);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindPrevious(T value, IEqualityComparer<T>? comparer)
        {
            return FindPrevious(value, comparer);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindPrevious(Predicate<T> predicate)
        {
            return FindPrevious(predicate);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindPrevious(Predicate<ILinkedListNode<T>> predicate)
        {
            return FindPrevious(predicate);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastPrevious(T value)
        {
            return FindLastPrevious(value);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastPrevious(T value, IEqualityComparer<T>? comparer)
        {
            return FindLastPrevious(value, comparer);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastPrevious(Predicate<T> predicate)
        {
            return FindLastPrevious(predicate);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastPrevious(Predicate<ILinkedListNode<T>> predicate)
        {
            return FindLastPrevious(predicate);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindNext(T value)
        {
            return FindNext(value);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindNext(T value, IEqualityComparer<T>? comparer)
        {
            return FindNext(value, comparer);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindNext(Predicate<T> predicate)
        {
            return FindNext(predicate);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindNext(Predicate<ILinkedListNode<T>> predicate)
        {
            return FindNext(predicate);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastNext(T value)
        {
            return FindLastNext(value);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastNext(T value, IEqualityComparer<T>? comparer)
        {
            return FindLastNext(value, comparer);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastNext(Predicate<T> predicate)
        {
            return FindLastNext(predicate);
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastNext(Predicate<ILinkedListNode<T>> predicate)
        {
            return FindLastNext(predicate);
        }
        
        [SuppressMessage("ReSharper", "BaseObjectGetHashCodeCallInGetHashCode")]
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                LinkedListNode<T, TNode, TList> value => Equals(value),
                T value => Equals(value),
                LinkedListNode<T> value => Equals(value),
                OneWayLinkedListNode<T> value => Equals(value),
                TwoWayLinkedListNode<T> value => Equals(value),
                OneWayReadOnlyLinkedListNode<T> value => Equals(value),
                TwoWayReadOnlyLinkedListNode<T> value => Equals(value),
                ILinkedListNode<T> value => Equals(value),
                _ => false
            };
        }
        
        public Boolean Equals(LinkedListNode<T, TNode, TList>? other)
        {
            return ReferenceEquals(this, other);
        }
        
        public Boolean Equals(T? other)
        {
            return EqualityComparer<T>.Default.Equals(ValueRef, other);
        }
        
        public Boolean Equals(LinkedListNode<T>? other)
        {
            return other is not null && Equals(other.ValueRef);
        }

        public Boolean Equals(ILinkedListNode<T>? other)
        {
            return other is not null && (other is LinkedListNode<T, TNode, TList> ? Equals(other) : Equals(other.Value));
        }
        
        Boolean ILinkedListNode.Equals(ILinkedListNode? other)
        {
            return Equals(other as ILinkedListNode<T>);
        }
        
        public Boolean Equals(OneWayLinkedListNode<T> other)
        {
            return Equals(other.Internal);
        }
        
        public Boolean Equals(TwoWayLinkedListNode<T> other)
        {
            return Equals(other.Internal);
        }
        
        public Boolean Equals(OneWayReadOnlyLinkedListNode<T> other)
        {
            return Equals(other.Internal);
        }
        
        public Boolean Equals(TwoWayReadOnlyLinkedListNode<T> other)
        {
            return Equals(other.Internal);
        }

        public new virtual IEnumerator<T> GetEnumerator()
        {
            TNode? source = this;
            TNode? head = List?.First;

            do
            {
                yield return Value;
                source = source.Next;
            } while (source is not null && !ReferenceEquals(source, head));
        }
        
        IEnumerator<TNode> IEnumerable<TNode>.GetEnumerator()
        {
            return base.GetEnumerator();
        }
        
        IEnumerator<ILinkedListNode> ILinkedListNode.GetEnumerator()
        {
            return base.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}