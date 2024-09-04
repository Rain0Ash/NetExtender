using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Nodes.Interfaces
{
    public interface ILinkedContainer<TNode> : ILinkedContainer, ICollection<TNode> where TNode : class, ILinkedNode<TNode>
    {
        /// <inheritdoc cref="ICollection{T}.Count"/>
        public new Int32 Count { get; }
        
        /// <inheritdoc cref="LinkedContainer{TNode}.First"/>
        public new TNode? First { get; }
        
        /// <inheritdoc cref="LinkedContainer{TNode}.First"/>
        public new TNode? Last { get; }
        
        /// <inheritdoc cref="LinkedContainer{TNode}.Find(System.Predicate{TNode})"/>
        public TNode? Find(Predicate<TNode> predicate);
        
        /// <inheritdoc cref="LinkedContainer{TNode}.FindLast(System.Predicate{TNode})"/>
        public TNode? FindLast(Predicate<TNode> predicate);
        
        /// <inheritdoc cref="LinkedContainer{TNode}.AddFirst(TNode)"/>
        public void AddFirst(TNode node);
        
        /// <inheritdoc cref="LinkedContainer{TNode}.AddLast(TNode)"/>
        public void AddLast(TNode node);
        
        /// <inheritdoc cref="LinkedContainer{TNode}.AddBefore(TNode,TNode)"/>
        public void AddBefore(TNode node, TNode @new);
        
        /// <inheritdoc cref="LinkedContainer{TNode}.AddBefore(TNode,TNode)"/>
        public void AddAfter(TNode node, TNode @new);
        
        /// <inheritdoc cref="LinkedContainer{TNode}.Remove(TNode)"/>
        public new Boolean Remove(TNode? node);
    }
    
    public interface ILinkedContainer : ICollection
    {
        /// <inheritdoc cref="LinkedContainer{TNode}.First"/>
        public ILinkedNode? First { get; }

        /// <inheritdoc cref="LinkedContainer{TNode}.Last"/>
        public ILinkedNode? Last { get; }

        /// <inheritdoc cref="LinkedContainer{TNode}.Remove(TNode)"/>
        public Boolean Remove(ILinkedNode node);

        /// <inheritdoc cref="LinkedContainer{TNode}.RemoveFirst"/>
        public Boolean RemoveFirst();

        /// <inheritdoc cref="LinkedContainer{TNode}.RemoveLast"/>
        public Boolean RemoveLast();
    }
}