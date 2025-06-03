// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// ReSharper disable RedundantUsingDirective
using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Nodes.Interfaces;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface ILinkedList<T, TNode, out TList> : ILinkedList<T, TNode> where TNode : class, ILinkedListNode<T, TNode, TList> where TList : class, ILinkedList<T, TNode, TList>
    {
    }
    
    public interface ILinkedList<T, TNode> : ILinkedList<T> where TNode : class, ILinkedListNode<T, TNode>
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
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddFirst(T)"/>
        public new TNode AddFirst(T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddFirst(TNode)"/>
        public void AddFirst(TNode node);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddLast(T)"/>
        public new TNode AddLast(T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddLast(TNode)"/>
        public void AddLast(TNode node);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddBefore(TNode,T)"/>
        public TNode AddBefore(TNode node, T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddBefore(TNode,TNode)"/>
        public void AddBefore(TNode node, TNode @new);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddBefore(TNode,T)"/>
        public TNode AddAfter(TNode node, T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddBefore(TNode,TNode)"/>
        public void AddAfter(TNode node, TNode @new);
        
        /// <inheritdoc cref="NetExtender.Types.Nodes.LinkedContainer{TNode}.Remove(TNode)"/>
        public Boolean Remove(TNode? node);
    }
    
    public interface ILinkedList<T> : ILinkedList, ICollection<T>
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
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddFirst(T)"/>
        public ILinkedListNode<T> AddFirst(T value);
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.AddLast(T)"/>
        public ILinkedListNode<T> AddLast(T value);
        
        /// <inheritdoc cref="NetExtender.Types.Nodes.LinkedContainer{TNode}.Remove(TNode)"/>
        public Boolean Remove(ILinkedListNode<T> node);
    }
    
    public interface ILinkedList : ILinkedContainer
    {
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.First"/>
        public new ILinkedListNode? First { get; }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Last"/>
        public new ILinkedListNode? Last { get; }
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.Remove(ILinkedListNode)"/>
        public Boolean Remove(ILinkedListNode node);

        /// <inheritdoc cref="LinkedList{T,TNode,TList}.RemoveFirst"/>
        public new Boolean RemoveFirst();
        
        /// <inheritdoc cref="LinkedList{T,TNode,TList}.RemoveLast"/>
        public new Boolean RemoveLast();
    }
}