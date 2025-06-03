// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Nodes.Interfaces
{
    public interface IReadOnlyLinkedContainer<out TNode> : IReadOnlyLinkedContainer, IReadOnlyCollection<TNode> where TNode : class, ILinkedNode<TNode>
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
    }
    
    public interface IReadOnlyLinkedContainer : ICollection
    {
        /// <inheritdoc cref="LinkedContainer{TNode}.First"/>
        public ILinkedNode? First { get; }

        /// <inheritdoc cref="LinkedContainer{TNode}.Last"/>
        public ILinkedNode? Last { get; }
    }
}