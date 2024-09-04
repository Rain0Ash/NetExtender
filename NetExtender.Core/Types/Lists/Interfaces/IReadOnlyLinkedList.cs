// ReSharper disable RedundantUsingDirective
using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Nodes.Interfaces;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface IReadOnlyLinkedList<T, out TNode, out TList> : IReadOnlyLinkedList<T, TNode> where TNode : class, IReadOnlyLinkedListNode<T, TNode, TList> where TList : class, IReadOnlyLinkedList<T, TNode, TList>
    {
    }
    
    public interface IReadOnlyLinkedList<T, out TNode> : IReadOnlyLinkedList<T> where TNode : class, ILinkedListNode<T, TNode>
    {
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.First"/>
        public new TNode? First { get; }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Last"/>
        public new TNode? Last { get; }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Find(T)"/>
        public new TNode? Find(T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Find(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public new TNode? Find(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Find(System.Predicate{T})"/>
        public new TNode? Find(Predicate<T> predicate);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.FindLast(T)"/>
        public new TNode? FindLast(T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.FindLast(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public new TNode? FindLast(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.FindLast(System.Predicate{T})"/>
        public new TNode? FindLast(Predicate<T> predicate);
    }
    
    public interface IReadOnlyLinkedList<T> : IReadOnlyLinkedList, IReadOnlyCollection<T>
    {
        /// <inheritdoc cref="ICollection{T}.Count"/>
        public new Int32 Count { get; }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.First"/>
        public new ILinkedListNode<T>? First { get; }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Last"/>
        public new ILinkedListNode<T>? Last { get; }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Find(T)"/>
        public ILinkedListNode<T>? Find(T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Find(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ILinkedListNode<T>? Find(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Find(System.Predicate{T})"/>
        public ILinkedListNode<T>? Find(Predicate<T> predicate);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Find(System.Predicate{T})"/>
        public ILinkedListNode<T>? Find(Predicate<ILinkedListNode<T>> predicate);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.FindLast(T)"/>
        public ILinkedListNode<T>? FindLast(T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.FindLast(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public ILinkedListNode<T>? FindLast(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.FindLast(System.Predicate{T})"/>
        public ILinkedListNode<T>? FindLast(Predicate<T> predicate);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.FindLast(System.Predicate{T})"/>
        public ILinkedListNode<T>? FindLast(Predicate<ILinkedListNode<T>> predicate);
    }
    
    public interface IReadOnlyLinkedList : IReadOnlyLinkedContainer
    {
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.First"/>
        public new ILinkedListNode? First { get; }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Last"/>
        public new ILinkedListNode? Last { get; }
    }
}