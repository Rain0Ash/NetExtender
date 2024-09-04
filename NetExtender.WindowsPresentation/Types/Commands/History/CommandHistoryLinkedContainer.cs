using System;
using System.Collections.Generic;
using NetExtender.Types.Nodes;
using NetExtender.Types.Nodes.Interfaces;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public class CommandHistoryLinkedContainer : CommandHistoryLinkedContainer<CommandHistoryLinkedEntry>, ICommandHistoryLinkedContainer
    {
        ICommandHistoryLinkedEntry? IReadOnlyLinkedContainer<ICommandHistoryLinkedEntry>.First
        {
            get
            {
                return First;
            }
        }
        
        ICommandHistoryLinkedEntry? IReadOnlyLinkedContainer<ICommandHistoryLinkedEntry>.Last
        {
            get
            {
                return Last;
            }
        }
        
        ICommandHistoryLinkedEntry? IReadOnlyLinkedContainer<ICommandHistoryLinkedEntry>.Find(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            return Find(predicate);
        }
        
        ICommandHistoryLinkedEntry? IReadOnlyLinkedContainer<ICommandHistoryLinkedEntry>.FindLast(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            return FindLast(predicate);
        }
        
        IEnumerator<ICommandHistoryLinkedEntry> IEnumerable<ICommandHistoryLinkedEntry>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public class CommandHistoryLinkedContainer<TNode> : LinkedContainer<TNode>, ICommandHistoryLinkedContainer<TNode> where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode>
    {
    }
}