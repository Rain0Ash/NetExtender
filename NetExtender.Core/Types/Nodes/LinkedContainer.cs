// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Nodes.Interfaces;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Nodes
{
    public abstract class LinkedContainer<TNode> : LinkedContainer, ILinkedContainer<TNode>, IReadOnlyLinkedContainer<TNode> where TNode : LinkedNode<TNode>
    {
        protected internal TNode? Head { get; protected set; }
        
        /// <inheritdoc cref="LinkedList{T}.First"/>
        public TNode? First
        {
            get
            {
                return Head;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.First"/>
        ILinkedNode? ILinkedContainer.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.First"/>
        ILinkedNode? IReadOnlyLinkedContainer.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Last"/>
        public TNode? Last
        {
            get
            {
                return Head?.Link;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Last"/>
        ILinkedNode? ILinkedContainer.Last
        {
            get
            {
                return Last;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Last"/>
        ILinkedNode? IReadOnlyLinkedContainer.Last
        {
            get
            {
                return Last;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.Count"/>
        public Int32 Count { get; protected set; }
        protected Int32 Version { get; set; }
        
        Object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }
        
        Boolean ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }
        
        Boolean ICollection<TNode>.IsReadOnly
        {
            get
            {
                return false;
            }
        }
        
        protected virtual void InsertNodeToEmptyListCore(TNode @new)
        {
            @new.Next = @new;
            @new.Link = @new;
            Head = @new;
            ++Version;
            ++Count;
        }
        
        protected virtual void InsertNodeBeforeCore(TNode? old, TNode @new)
        {
            @new.Next = old;
            @new.Link = old?.Link;
            
            if (old is not null)
            {
                old.Link!.Next = @new;
                old.Link = @new;
            }
            
            ++Version;
            ++Count;
        }
        
        protected virtual Boolean RemoveNodeCore(TNode? node)
        {
            if (node is null)
            {
                return false;
            }
            
            if (ReferenceEquals(node.Next, node))
            {
                Head = null;
            }
            else
            {
                node.Next!.Link = node.Link;
                node.Link!.Next = node.Next;
                if (ReferenceEquals(Head, node))
                {
                    Head = node.Next;
                }
            }
            
            node.Invalidate();
            --Count;
            ++Version;
            
            return true;
        }
        
        protected virtual void ValidateNewNode(TNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }
        }
        
        protected virtual void ValidateNode(TNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }
        }
        
        protected virtual void MakeLink(TNode? node)
        {
        }
        
        public Boolean Contains(TNode? node)
        {
            if (node is null || Head is not { } current)
            {
                return false;
            }
            
            while (current is not null && !ReferenceEquals(current, node))
            {
                current = current.Next;
            }
            
            return current is not null;
        }
        
        /// <inheritdoc cref="LinkedList{T}.Find(T)"/>
        public virtual TNode? Find(Predicate<TNode> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Head is not { } node)
            {
                return null;
            }
            
            while (node is not null && !predicate(node))
            {
                node = node.Next;
                
                if (ReferenceEquals(Head, node))
                {
                    return null;
                }
            }
            
            return node;
        }
        
        /// <inheritdoc cref="LinkedList{T}.FindLast(T)"/>
        public virtual TNode? FindLast(Predicate<TNode> predicate)
        {
            if (Head?.Link is not { } link)
            {
                return null;
            }
            
            TNode? node = link;
            while (node is not null && !predicate(node))
            {
                node = node.Link;
                
                if (ReferenceEquals(node, link))
                {
                    return null;
                }
            }
            
            return node;
        }
        
        void ICollection<TNode>.Add(TNode node)
        {
            AddLast(node);
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddFirst(LinkedListNode{T})"/>
        public void AddFirst(TNode node)
        {
            ValidateNewNode(node);
            
            if (Head is null)
            {
                InsertNodeToEmptyListCore(node);
            }
            else
            {
                InsertNodeBeforeCore(Head, node);
                Head = node;
            }
            
            MakeLink(node);
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddLast(LinkedListNode{T})"/>
        public void AddLast(TNode node)
        {
            ValidateNewNode(node);
            
            if (Head is null)
            {
                InsertNodeToEmptyListCore(node);
                MakeLink(node);
                return;
            }
            
            InsertNodeBeforeCore(Head, node);
            MakeLink(node);
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddBefore(LinkedListNode{T},LinkedListNode{T})"/>
        public void AddBefore(TNode node, TNode @new)
        {
            ValidateNode(node);
            ValidateNewNode(@new);
            InsertNodeBeforeCore(node, @new);
            
            MakeLink(@new);
            if (ReferenceEquals(Head, node))
            {
                Head = @new;
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.AddAfter(LinkedListNode{T},LinkedListNode{T})"/>
        public void AddAfter(TNode node, TNode @new)
        {
            ValidateNode(node);
            ValidateNewNode(@new);
            InsertNodeBeforeCore(node.Next, @new);
            MakeLink(@new);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Remove(LinkedListNode{T})"/>
        public Boolean Remove(TNode? node)
        {
            if (node is null)
            {
                return false;
            }

            ValidateNode(node);
            return RemoveNodeCore(node);
        }
        
        /// <inheritdoc cref="LinkedList{T}.Remove(LinkedListNode{T})"/>
        Boolean ILinkedContainer.Remove(ILinkedNode node)
        {
            return Remove(node as TNode);
        }

        /// <inheritdoc cref="LinkedList{T}.RemoveFirst"/>
        public Boolean RemoveFirst()
        {
            return Head is not null && RemoveNodeCore(Head);
        }

        /// <inheritdoc cref="LinkedList{T}.RemoveLast"/>
        public Boolean RemoveLast()
        {
            return Head is not null && RemoveNodeCore(Head.Link);
        }

        /// <inheritdoc cref="LinkedList{T}.Clear"/>
        public void Clear()
        {
            TNode? node = Head;
            while (node is not null)
            {
                TNode current = node;
                node = node.Next;
                current.Invalidate();
            }
            
            Head = null;
            Count = 0;
            ++Version;
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual void CopyTo(Array array, Int32 index)
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
                    CopyTo(nodes, index);
                    return;
                case Object?[] objects:
                    CopyTo(objects, index);
                    return;
                default:
                    throw new ArgumentException(SR.Argument_InvalidArrayType, nameof(array));
            }
        }
        
        void ICollection.CopyTo(Array array, Int32 index)
        {
            CopyTo(array, index);
        }
        
        protected virtual void CopyTo(Object?[] array, Int32 index)
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
                    array[index++] = node;
                    node = node.Next;
                } while (node is not null && !ReferenceEquals(Head, node));
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException(SR.Argument_InvalidArrayType, nameof(array));
            }
        }
        
        /// <inheritdoc cref="LinkedList{T}.CopyTo(T[],System.Int32)"/>
        public virtual void CopyTo(TNode[] array, Int32 index)
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
                array[index++] = node;
                node = node.Next;
            } while (node is not null && !ReferenceEquals(Head, node));
        }
        
        /// <inheritdoc cref="LinkedList{T}.GetEnumerator"/>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<TNode> IEnumerable<TNode>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc cref="LinkedList{T}.Enumerator"/>
        public struct Enumerator : IEnumerator<TNode>
        {
            private readonly LinkedContainer<TNode> _container;
            private readonly Int32 _version;
            
            private TNode? _current;

            /// <inheritdoc cref="LinkedList{T}.Enumerator.Current"/>
            public readonly TNode Current
            {
                get
                {
                    return _current!;
                }
            }

            /// <inheritdoc cref="LinkedList{T}.Enumerator.Current"/>
            readonly Object IEnumerator.Current
            {
                get
                {
                    if (_index <= 0 || _index == _container.Count + 1)
                    {
                        throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
                    }
                    
                    return Current;
                }
            }
            
            private Int32 _index;

            internal Enumerator(LinkedContainer<TNode> container)
            {
                _container = container;
                _version = container.Version;
                _current = container.Head;
                _index = 0;
            }
            
            /// <inheritdoc cref="LinkedList{T}.Enumerator.MoveNext"/>
            public Boolean MoveNext()
            {
                if (_version != _container.Version)
                {
                    throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
                }

                if (_current is null)
                {
                    _index = _container.Count + 1;
                    return false;
                }

                ++_index;
                _current = _current.Next;

                if (ReferenceEquals(_container.Head, _current))
                {
                    _current = null;
                }
                
                return true;
            }
            
            void IEnumerator.Reset()
            {
                if (_version != _container.Version)
                {
                    throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
                }

                _current = _container.Head;
                _index = 0;
            }

            public void Dispose()
            {
            }
        }
    }
    
    public abstract class LinkedContainer
    {
        [ReflectionSystemResource(typeof(LinkedList<>))]
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        protected static class SR
        {
            private static Type SRType { get; } = SRUtilities.SRType(typeof(LinkedList<>).Assembly);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo Arg_NonZeroLowerBound { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo ArgumentOutOfRange_NeedNonNegNum { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo ArgumentOutOfRange_BiggerThanCollection { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo Arg_RankMultiDimNotSupported { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo Argument_InvalidArrayType { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo Arg_InsufficientSpace { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo LinkedListEmpty { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo LinkedListNodeIsAttached { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo ExternalLinkedListNode { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo Serialization_MissingValues { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo InvalidOperation_EnumOpCantHappen { get; } = new SRInfo(SRType);
            
            [ReflectionSystemResource(typeof(LinkedList<>))]
            public static SRInfo InvalidOperation_EnumFailedVersion { get; } = new SRInfo(SRType);
        }
        
        [ReflectionNaming(typeof(LinkedList<>))]
        protected static class Data
        {
        }
    }
}