using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Nodes;
using NetExtender.Types.Lists.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Lists
{
    /// <inheritdoc cref="LinkedList{T}"/>
    public class LinkedList<T, TNode> : LinkedList<T, TNode, LinkedList<T, TNode>> where TNode : LinkedListNode<T, TNode>
    {
    }
    
    /// <inheritdoc cref="LinkedList{T}"/>
    public class LinkedList<T, TNode, TList> : LinkedContainer<TNode>, ILinkedList<T, TNode, TList>, IReadOnlyLinkedList<T, TNode, TList>, ISerializable, IDeserializationCallback where TNode : LinkedListNode<T, TNode, TList> where TList : LinkedList<T, TNode, TList>
    {
        private static Func<TList?, T, TNode>? _factory;
        private static Func<TList?, T, TNode> Factory
        {
            get
            {
                return _factory ??= ReflectionUtilities.New<TNode, TList?, T>();
            }
        }
        
        protected TList This
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (TList) this;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.First"/>
        ILinkedListNode<T>? ILinkedList<T>.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.First"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.First"/>
        ILinkedListNode? ILinkedList.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.First"/>
        ILinkedListNode? IReadOnlyLinkedList.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Last"/>
        ILinkedListNode<T>? ILinkedList<T>.Last
        {
            get
            {
                return Last;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Last"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.Last
        {
            get
            {
                return Last;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Last"/>
        ILinkedListNode? ILinkedList.Last
        {
            get
            {
                return Last;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Last"/>
        ILinkedListNode? IReadOnlyLinkedList.Last
        {
            get
            {
                return Last;
            }
        }

        private SerializationInfo? _serialization;
        
        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}()"/>
        public LinkedList()
        {
        }
        
        /// <inheritdoc cref="LinkedList{T}(System.Collections.Generic.IEnumerable{T})"/>
        public LinkedList(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            foreach (T item in collection)
            {
                AddLast(item);
            }
        }
        
        // ReSharper disable once UnusedParameter.Local
        /// <inheritdoc cref="LinkedList{T}(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)"/>
        protected LinkedList(SerializationInfo info, StreamingContext context)
        {
            _serialization = info;
        }
        
        /// <inheritdoc cref="LinkedList{T}.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)"/>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            info.AddValue(nameof(Version), Version);
            info.AddValue(nameof(Count), Count);
            
            if (Count <= 0)
            {
                return;
            }
            
            T[] array = new T[Count];
            CopyTo(array, 0);
            info.AddValue(nameof(Data), array, typeof(T[]));
        }
        
        /// <inheritdoc cref="LinkedList{T}.OnDeserialization(System.Object)"/>
        public virtual void OnDeserialization(Object? sender)
        {
            if (_serialization is null)
            {
                return;
            }
            
            Head = null;
            Int32 version = _serialization.GetInt32(nameof(Version));
            
            if (_serialization.GetInt32(nameof(Count)) > 0)
            {
                if (_serialization.GetValue(nameof(Data), typeof(T[])) is not T[] array)
                {
                    throw new SerializationException(SR.Serialization_MissingValues);
                }

                foreach (T item in array)
                {
                    AddLast(item);
                }
            }
            
            Version = version;
            _serialization = null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected TNode Create(in T value)
        {
            return Create(This, in value);
        }

        protected virtual TNode Create(TList? list, in T value)
        {
            return Factory(list, value);
        }
        
        protected override void ValidateNewNode(TNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            
            if (node.List is not null)
            {
                throw new InvalidOperationException(SR.LinkedListNodeIsAttached);
            }
        }
        
        protected override void ValidateNode(TNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            
            if (node.List != this)
            {
                throw new InvalidOperationException(SR.ExternalLinkedListNode);
            }
        }
        
        protected override void MakeLink(TNode? node)
        {
            if (node is not null)
            {
                node.List = This;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Contains(T)"/>
        public Boolean Contains(T value)
        {
            return Find(value) is not null;
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        public TNode? Find(T value)
        {
            return Find(value, null);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        ILinkedListNode<T>? ILinkedList<T>.Find(T value)
        {
            return Find(value);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.Find(T value)
        {
            return Find(value);
        }
        
        // ReSharper disable once CognitiveComplexity
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        public virtual TNode? Find(T value, IEqualityComparer<T>? comparer)
        {
            if (Head is not { } node)
            {
                return null;
            }
            
            if (value is not null)
            {
                comparer ??= EqualityComparer<T>.Default;
                while (node is not null && !comparer.Equals(node.ValueRef, value))
                {
                    node = node.Next;
                    
                    if (ReferenceEquals(Head, node))
                    {
                        return null;
                    }
                }
                
                return node;
            }
            
            while (node is not null && node.ValueRef is not null)
            {
                node = node.Next;
                
                if (ReferenceEquals(Head, node))
                {
                    return null;
                }
            }
            
            return node;
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        ILinkedListNode<T>? ILinkedList<T>.Find(T value, IEqualityComparer<T>? comparer)
        {
            return Find(value, comparer);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.Find(T value, IEqualityComparer<T>? comparer)
        {
            return Find(value, comparer);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        public virtual TNode? Find(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (Head is not { } node)
            {
                return null;
            }

            while (node is not null && !predicate(node.ValueRef))
            {
                node = node.Next;
                
                if (ReferenceEquals(Head, node))
                {
                    return null;
                }
            }

            return node;
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        ILinkedListNode<T>? ILinkedList<T>.Find(Predicate<T> predicate)
        {
            return Find(predicate);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.Find(Predicate<T> predicate)
        {
            return Find(predicate);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        ILinkedListNode<T>? ILinkedList<T>.Find(Predicate<ILinkedListNode<T>> predicate)
        {
            return Find(predicate);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.Find(Predicate<ILinkedListNode<T>> predicate)
        {
            return Find(predicate);
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        public TNode? FindLast(T value)
        {
            return FindLast(value, null);
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        ILinkedListNode<T>? ILinkedList<T>.FindLast(T value)
        {
            return FindLast(value);
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.FindLast(T value)
        {
            return FindLast(value);
        }
        
        // ReSharper disable once CognitiveComplexity
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        public virtual TNode? FindLast(T value, IEqualityComparer<T>? comparer)
        {
            if (Head?.Link is not { } link)
            {
                return null;
            }
            
            TNode? node = link;
            if (value is not null)
            {
                comparer ??= EqualityComparer<T>.Default;
                while (node is not null && !comparer.Equals(node.ValueRef, value))
                {
                    node = node.Link;
                    
                    if (ReferenceEquals(node, link))
                    {
                        return null;
                    }
                }

                return node;
            }
            
            while (node is not null && node.ValueRef is not null)
            {
                node = node.Link;
                
                if (ReferenceEquals(node, link))
                {
                    return null;
                }
            }

            return node;
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        ILinkedListNode<T>? ILinkedList<T>.FindLast(T value, IEqualityComparer<T>? comparer)
        {
            return FindLast(value, comparer);
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.FindLast(T value, IEqualityComparer<T>? comparer)
        {
            return FindLast(value, comparer);
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        public virtual TNode? FindLast(Predicate<T> predicate)
        {
            if (Head?.Link is not { } link)
            {
                return null;
            }
            
            TNode? node = link;
            while (node is not null && !predicate(node.ValueRef))
            {
                node = node.Link;
                
                if (ReferenceEquals(node, link))
                {
                    return null;
                }
            }

            return node;
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        ILinkedListNode<T>? ILinkedList<T>.FindLast(Predicate<T> predicate)
        {
            return FindLast(predicate);
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.FindLast(Predicate<T> predicate)
        {
            return FindLast(predicate);
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        ILinkedListNode<T>? ILinkedList<T>.FindLast(Predicate<ILinkedListNode<T>> predicate)
        {
            return FindLast(predicate);
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        ILinkedListNode<T>? IReadOnlyLinkedList<T>.FindLast(Predicate<ILinkedListNode<T>> predicate)
        {
            return FindLast(predicate);
        }
        
        void ICollection<T>.Add(T value)
        {
            AddLast(value);
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddFirst(T)"/>
        public TNode AddFirst(T value)
        {
            TNode @new = Create(in value);
            
            if (Head is null)
            {
                InternalInsertNodeToEmptyList(@new);
                return @new;
            }

            InternalInsertNodeBefore(Head, @new);
            return Head = @new;
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddFirst(T)"/>
        ILinkedListNode<T> ILinkedList<T>.AddFirst(T value)
        {
            return AddFirst(value);
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddLast(T)"/>
        public TNode AddLast(T value)
        {
            TNode @new = Create(in value);
            
            if (Head is null)
            {
                InternalInsertNodeToEmptyList(@new);
                return @new;
            }
            
            InternalInsertNodeBefore(Head, @new);
            return @new;
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddLast(T)"/>
        ILinkedListNode<T> ILinkedList<T>.AddLast(T value)
        {
            return AddLast(value);
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddBefore(LinkedListNode{T},T)"/>
        public TNode AddBefore(TNode node, T value)
        {
            ValidateNode(node);

            TNode @new = Create(node.List, value);
            InternalInsertNodeBefore(node, @new);
            return ReferenceEquals(Head, node) ? Head = @new : @new;
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddAfter(LinkedListNode{T},T)"/>
        public TNode AddAfter(TNode node, T value)
        {
            ValidateNode(node);
            TNode @new = Create(node.List, value);
            InternalInsertNodeBefore(node.Next, @new);
            return @new;
        }
        
        /// <inheritdoc cref="LinkedList{T}.Remove(T)"/>
        public Boolean Remove(T value)
        {
            if (Find(value) is not { } node)
            {
                return false;
            }
            
            InternalRemoveNode(node);
            return true;
        }
        
        /// <inheritdoc cref="LinkedContainer{TNode}.Remove(TNode)"/>
        Boolean ILinkedList<T>.Remove(ILinkedListNode<T> node)
        {
            return base.Remove(node as TNode);
        }
        
        Boolean ILinkedList.Remove(ILinkedListNode node)
        {
            return base.Remove(node as TNode);
        }
        
        // ReSharper disable once CognitiveComplexity
        protected override void CopyTo(Array array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof (array));
            }
            
            if (array.Rank != 1)
            {
                throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, nameof(array));
            }
            
            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException(SR.Arg_NonZeroLowerBound, nameof(array));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, SR.ArgumentOutOfRange_NeedNonNegNum);
            }
            
            if (array.Length - index < Count)
            {
                throw new ArgumentException(SR.Arg_InsufficientSpace);
            }
            
            switch (array)
            {
                case TNode[] nodes:
                    base.CopyTo(nodes, index);
                    return;
                case T[] generic:
                    CopyTo(generic, index);
                    return;
                case Object?[] objects:
                    CopyTo(objects, index);
                    return;
                default:
                    throw new ArgumentException(SR.Argument_InvalidArrayType, nameof(array));
            }
        }
        
        protected override void CopyTo(Object?[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, SR.ArgumentOutOfRange_NeedNonNegNum);
            }
            
            if (index > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, SR.ArgumentOutOfRange_BiggerThanCollection);
            }
            
            if (array.Length - index < Count)
            {
                throw new ArgumentException(SR.Arg_InsufficientSpace);
            }
            
            if (Head is not { } node)
            {
                return;
            }
            
            try
            {
                do
                {
                    array[index++] = node.ValueRef;
                    node = node.Next;
                } while (node is not null && !ReferenceEquals(Head, node));
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException(SR.Argument_InvalidArrayType, nameof(array));
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.CopyTo(T[],System.Int32)"/>
        public virtual void CopyTo(T[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, SR.ArgumentOutOfRange_NeedNonNegNum);
            }
            
            if (index > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, SR.ArgumentOutOfRange_BiggerThanCollection);
            }
            
            if (array.Length - index < Count)
            {
                throw new ArgumentException(SR.Arg_InsufficientSpace);
            }
            
            if (Head is not { } node)
            {
                return;
            }
            
            do
            {
                array[index++] = node.ValueRef;
                node = node.Next;
            } while (node is not null && !ReferenceEquals(Head, node));
        }
        
        /// <inheritdoc cref="LinkedList{T}.GetEnumerator"/>
        public new Enumerator GetEnumerator()
        {
            return new Enumerator(This);
        }
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        /// <inheritdoc cref="LinkedList{T}.Enumerator"/>
        public new struct Enumerator : IEnumerator<T>, ISerializable, IDeserializationCallback
        {
            private readonly TList _list;
            private TNode? _node;
            private readonly Int32 _version;
            private T? _current;
            
            /// <inheritdoc cref="LinkedList{T}.Enumerator.Current"/>
            public readonly T Current
            {
                get
                {
                    return _current!;
                }
            }
            
            /// <inheritdoc cref="LinkedList{T}.Enumerator.Current"/>
            readonly Object? IEnumerator.Current
            {
                get
                {
                    if (_index <= 0 || _index == _list.Count + 1)
                    {
                        throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
                    }
                    
                    return Current;
                }
            }
            
            private Int32 _index;

            internal Enumerator(TList list)
            {
                _list = list;
                _version = list.Version;
                _node = list.Head;
                _current = default;
                _index = 0;
            }
            
            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
            {
                throw new PlatformNotSupportedException();
            }
            
            void IDeserializationCallback.OnDeserialization(Object? sender)
            {
                throw new PlatformNotSupportedException();
            }
            
            /// <inheritdoc cref="LinkedList{T}.Enumerator.MoveNext"/>
            public Boolean MoveNext()
            {
                if (_version != _list.Version)
                {
                    throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
                }

                if (_node is null)
                {
                    _index = _list.Count + 1;
                    return false;
                }

                ++_index;
                _current = _node.ValueRef;
                _node = _node.Next;

                if (ReferenceEquals(_list.Head, _node))
                {
                    _node = null;
                }
                
                return true;
            }
            
            void IEnumerator.Reset()
            {
                if (_version != _list.Version)
                {
                    throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
                }

                _current = default;
                _node = _list.Head;
                _index = 0;
            }

            public void Dispose()
            {
            }
        }
    }
}