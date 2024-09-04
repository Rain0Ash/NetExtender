// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using NetExtender.Types.LinkedLists;
using NetExtender.Types.Nodes.Interfaces;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface ILinkedListNode<T, out TNode, out TList> : ILinkedListNode<T, TNode> where TNode : class, ILinkedListNode<T, TNode, TList> where TList : class, ILinkedList<T, TNode, TList>
    {
        public TList? List { get; }
    }
    
    public interface IReadOnlyLinkedListNode<T, out TNode, out TList> : ILinkedListNode<T, TNode> where TNode : class, IReadOnlyLinkedListNode<T, TNode, TList> where TList : class, IReadOnlyLinkedList<T, TNode, TList>
    {
        public TList? List { get; }
    }
    
    public interface ILinkedListNode<T, out TNode> : ILinkedNode<T, TNode>, ILinkedListNode<T> where TNode : class, ILinkedListNode<T, TNode>
    {
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.First"/>
        public new TNode? First { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Last"/>
        public new TNode? Last { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Next"/>
        public new TNode? Next { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Previous"/>
        public new TNode? Previous { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(T)"/>
        public new TNode? Find(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public new TNode? Find(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(System.Predicate{T})"/>
        public new TNode? Find(Predicate<T> predicate);
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(System.Predicate{TNode})"/>
        public new TNode? Find(Predicate<TNode> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T)"/>
        public new TNode? FindLast(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public new TNode? FindLast(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(System.Predicate{T})"/>
        public new TNode? FindLast(Predicate<T> predicate);
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(System.Predicate{TNode})"/>
        public new TNode? FindLast(Predicate<TNode> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindPrevious(T)"/>
        public new TNode? FindPrevious(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindPrevious(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public new TNode? FindPrevious(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindPrevious(System.Predicate{T})"/>
        public new TNode? FindPrevious(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindPrevious(TNode)"/>
        public new TNode? FindPrevious(Predicate<TNode> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastPrevious(T)"/>
        public new TNode? FindLastPrevious(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastPrevious(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public new TNode? FindLastPrevious(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastPrevious(System.Predicate{T})"/>
        public new TNode? FindLastPrevious(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastPrevious(TNode)"/>
        public new TNode? FindLastPrevious(Predicate<TNode> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindNext(T)"/>
        public new TNode? FindNext(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindNext(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public new TNode? FindNext(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindNext(System.Predicate{T})"/>
        public new TNode? FindNext(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindNext(TNode)"/>
        public new TNode? FindNext(Predicate<TNode> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastNext(T)"/>
        public new TNode? FindLastNext(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastNext(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public new TNode? FindLastNext(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastNext(System.Predicate{T})"/>
        public new TNode? FindLastNext(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastNext(TNode)"/>
        public new TNode? FindLastNext(Predicate<TNode> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.GetEnumerator()"/>
        public new IEnumerator<T> GetEnumerator();
    }

    public interface ILinkedListNode<T> : ILinkedListNode, IEquatable<T>, IEquatable<ILinkedListNode<T>>, IEquatable<LinkedListNode<T>>, IEquatable<OneWayLinkedListNode<T>>, IEquatable<TwoWayLinkedListNode<T>>, IEquatable<OneWayReadOnlyLinkedListNode<T>>, IEquatable<TwoWayReadOnlyLinkedListNode<T>>
    {
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Value"/>
        public T Value { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.First"/>
        public new ILinkedListNode<T>? First { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Last"/>
        public new ILinkedListNode<T>? Last { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Next"/>
        public new ILinkedListNode<T>? Next { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Previous"/>
        public new ILinkedListNode<T>? Previous { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(T)"/>
        public ILinkedListNode<T>? Find(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ILinkedListNode<T>? Find(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(System.Predicate{T})"/>
        public ILinkedListNode<T>? Find(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Find(System.Predicate{ILinkedListNode{T}})"/>
        public ILinkedListNode<T>? Find(Predicate<ILinkedListNode<T>> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T)"/>
        public ILinkedListNode<T>? FindLast(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ILinkedListNode<T>? FindLast(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T,System.Predicate{T})"/>
        public ILinkedListNode<T>? FindLast(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLast(T,System.Predicate{ILinkedListNode{T}})"/>
        public ILinkedListNode<T>? FindLast(Predicate<ILinkedListNode<T>> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindPrevious(T)"/>
        public ILinkedListNode<T>? FindPrevious(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindPrevious(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ILinkedListNode<T>? FindPrevious(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindPrevious(System.Predicate{T})"/>
        public ILinkedListNode<T>? FindPrevious(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindPrevious(ILinkedListNode{T})"/>
        public ILinkedListNode<T>? FindPrevious(Predicate<ILinkedListNode<T>> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastPrevious(T)"/>
        public ILinkedListNode<T>? FindLastPrevious(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastPrevious(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ILinkedListNode<T>? FindLastPrevious(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastPrevious(System.Predicate{T})"/>
        public ILinkedListNode<T>? FindLastPrevious(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastPrevious(ILinkedListNode{T})"/>
        public ILinkedListNode<T>? FindLastPrevious(Predicate<ILinkedListNode<T>> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindNext(T)"/>
        public ILinkedListNode<T>? FindNext(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindNext(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ILinkedListNode<T>? FindNext(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindNext(System.Predicate{T})"/>
        public ILinkedListNode<T>? FindNext(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindNext(ILinkedListNode{T})"/>
        public ILinkedListNode<T>? FindNext(Predicate<ILinkedListNode<T>> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastNext(T)"/>
        public ILinkedListNode<T>? FindLastNext(T value);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastNext(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ILinkedListNode<T>? FindLastNext(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastNext(System.Predicate{T})"/>
        public ILinkedListNode<T>? FindLastNext(Predicate<T> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.FindLastNext(ILinkedListNode{T})"/>
        public ILinkedListNode<T>? FindLastNext(Predicate<ILinkedListNode<T>> predicate);

        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.GetEnumerator()"/>
        public new IEnumerator<T> GetEnumerator();
    }
    
    public interface ILinkedListNode : ILinkedNode
    {
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.First"/>
        public new ILinkedListNode? First { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Last"/>
        public new ILinkedListNode? Last { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Next"/>
        public new ILinkedListNode? Next { get; }
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.Previous"/>
        public new ILinkedListNode? Previous { get; }
        
        /// <inheritdoc cref="IEquatable{ILinkedListNode}.Equals(ILinkedListNode)"/>
        public Boolean Equals(ILinkedListNode? other);
        
        /// <inheritdoc cref="LinkedListNode{T,TNode,TList}.GetEnumerator()"/>
        public new IEnumerator<ILinkedListNode> GetEnumerator();
    }
}