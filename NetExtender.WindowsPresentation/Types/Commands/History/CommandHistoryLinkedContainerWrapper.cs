using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Nodes;
using NetExtender.Types.Nodes.Interfaces;
using NetExtender.Types.Storages;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public sealed class CommandHistoryLinkedContainerWrapper<TNode> : ICommandHistoryLinkedContainer where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode>
    {
        private ICommandHistoryLinkedContainer<TNode> Container { get; }
        private WeakStorage<TNode, ICommandHistoryLinkedEntry> Storage { get; } = new WeakStorage<TNode, ICommandHistoryLinkedEntry>();
        
        public Int32 Count
        {
            get
            {
                return Container.Count;
            }
        }
        
        public TNode? First
        {
            get
            {
                return Container.First;
            }
        }
        
        ICommandHistoryLinkedEntry? IReadOnlyLinkedContainer<ICommandHistoryLinkedEntry>.First
        {
            get
            {
                return Convert(First);
            }
        }
        
        ILinkedNode? IReadOnlyLinkedContainer.First
        {
            get
            {
                return ((IReadOnlyLinkedContainer) Container).First;
            }
        }
        
        public TNode? Last
        {
            get
            {
                return Container.Last;
            }
        }
        
        ICommandHistoryLinkedEntry? IReadOnlyLinkedContainer<ICommandHistoryLinkedEntry>.Last
        {
            get
            {
                return Convert(Last);
            }
        }
        
        ILinkedNode? IReadOnlyLinkedContainer.Last
        {
            get
            {
                return ((IReadOnlyLinkedContainer) Container).Last;
            }
        }
        
        Boolean ICollection.IsSynchronized
        {
            get
            {
                return Container.IsSynchronized;
            }
        }
        
        Object ICollection.SyncRoot
        {
            get
            {
                return Container.SyncRoot;
            }
        }
        
        public CommandHistoryLinkedContainerWrapper(ICommandHistoryLinkedContainer<TNode> container)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }
        
        [return: NotNullIfNotNull("node")]
        private ICommandHistoryLinkedEntry? Convert(TNode? node)
        {
            return node switch
            {
                null => null,
                ICommandHistoryLinkedEntry result => result,
                _ => Storage.GetOrAdd(node, static node => new CommandHistoryLink<TNode>(node))
            };
        }

        public TNode? Find(Predicate<TNode> predicate)
        {
            return Container.Find(predicate);
        }
        
        ICommandHistoryLinkedEntry? IReadOnlyLinkedContainer<ICommandHistoryLinkedEntry>.Find(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Convert(Find(node => predicate(Convert(node))));
        }

        public TNode? FindLast(Predicate<TNode> predicate)
        {
            return Container.FindLast(predicate);
        }
        
        ICommandHistoryLinkedEntry? IReadOnlyLinkedContainer<ICommandHistoryLinkedEntry>.FindLast(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Convert(FindLast(node => predicate(Convert(node))));
        }
        
        void ICollection.CopyTo(Array array, Int32 index)
        {
            Container.CopyTo(array, index);
        }
        
        public IEnumerator<TNode> GetEnumerator()
        {
            return Container.GetEnumerator();
        }
        
        IEnumerator<ICommandHistoryLinkedEntry> IEnumerable<ICommandHistoryLinkedEntry>.GetEnumerator()
        {
            foreach (TNode node in this)
            {
                yield return Convert(node);
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Container).GetEnumerator();
        }
    }
}