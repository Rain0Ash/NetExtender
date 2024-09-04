// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;

namespace NetExtender.Types.Nodes.Interfaces
{
    public interface ILinkedNode<T, out TNode> : ILinkedNode<TNode>, IEnumerable<ILinkedNode<T, TNode>> where TNode : class, ILinkedNode<T, TNode>
    {
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Find(T)"/>
        public TNode? Find(T value);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Find(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public TNode? Find(T value, IEqualityComparer<T>? comparer);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Find(System.Predicate{T})"/>
        public TNode? Find(Predicate<T> predicate);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLast(T)"/>
        public TNode? FindLast(T value);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLast(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public TNode? FindLast(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLast(System.Predicate{T})"/>
        public TNode? FindLast(Predicate<T> predicate);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindPrevious(T)"/>
        public TNode? FindPrevious(T value);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindPrevious(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public TNode? FindPrevious(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindPrevious(System.Predicate{T})"/>
        public TNode? FindPrevious(Predicate<T> predicate);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLastPrevious(T)"/>
        public TNode? FindLastPrevious(T value);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLastPrevious(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public TNode? FindLastPrevious(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLastPrevious(System.Predicate{T})"/>
        public TNode? FindLastPrevious(Predicate<T> predicate);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindNext(T)"/>
        public TNode? FindNext(T value);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindNext(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public TNode? FindNext(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindNext(System.Predicate{T})"/>
        public TNode? FindNext(Predicate<T> predicate);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLastNext(T)"/>
        public TNode? FindLastNext(T value);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLastNext(T,System.Collections.Generic.IEqualityComparer{T})"/>
        public TNode? FindLastNext(T value, IEqualityComparer<T>? comparer);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLastNext(System.Predicate{T})"/>
        public TNode? FindLastNext(Predicate<T> predicate);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.GetEnumerator()"/>
        public new IEnumerator<TNode> GetEnumerator();
    }

    public interface ILinkedNode<out TNode> : ILinkedNode, IEnumerable<TNode>, IEnumerable<ILinkedNode<TNode>> where TNode : class, ILinkedNode<TNode>
    {
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.First"/>
        public new TNode? First { get; }

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Last"/>
        public new TNode? Last { get; }

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Next"/>
        public new TNode? Next { get; }

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Previous"/>
        public new TNode? Previous { get; }
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Find(System.Predicate{TNode})"/>
        public TNode? Find(Predicate<TNode> predicate);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLast(System.Predicate{TNode})"/>
        public TNode? FindLast(Predicate<TNode> predicate);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindPrevious(System.Predicate{TNode})"/>
        public TNode? FindPrevious(Predicate<TNode> predicate);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLastPrevious(System.Predicate{TNode})"/>
        public TNode? FindLastPrevious(Predicate<TNode> predicate);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindNext(System.Predicate{TNode})"/>
        public TNode? FindNext(Predicate<TNode> predicate);
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.FindLastNext(System.Predicate{TNode})"/>
        public TNode? FindLastNext(Predicate<TNode> predicate);

        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.GetEnumerator()"/>
        public new IEnumerator<TNode> GetEnumerator();
    }
    
    public interface ILinkedNode
    {
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.First"/>
        public ILinkedNode? First { get; }
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Last"/>
        public ILinkedNode? Last { get; }
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Next"/>
        public ILinkedNode? Next { get; }
        
        /// <inheritdoc cref="NetExtender.Types.Lists.Interfaces.ILinkedListNode{T,TNode}.Previous"/>
        public ILinkedNode? Previous { get; }
        
        public IEnumerator<ILinkedNode> GetEnumerator();
    }
}