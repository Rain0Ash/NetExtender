using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using NetExtender.Types.Nodes;
using NetExtender.Types.Nodes.Interfaces;
using NetExtender.Types.Storages;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public sealed class CommandHistoryLink<TNode> : CommandHistoryLink, ICommandHistoryLinkedEntry where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode>
    {
        private TNode Node { get; }
        
        public override ICommand Command
        {
            get
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                return Node switch
                {
                    ICommandHistoryEntry history => history.Command,
                    ICommand command => command,
                    _ => Commands.Command.Empty
                };
            }
        }
        
        public TNode? First
        {
            get
            {
                return Node.First;
            }
        }
        
        ILinkedNode? ILinkedNode.First
        {
            get
            {
                return ((ILinkedNode) Node).First;
            }
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.First
        {
            get
            {
                return Convert(First);
            }
        }
        
        public TNode? Last
        {
            get
            {
                return Node.Last;
            }
        }
        
        ILinkedNode? ILinkedNode.Last
        {
            get
            {
                return ((ILinkedNode) Node).Last;
            }
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.Last
        {
            get
            {
                return Convert(Last);
            }
        }
        
        public TNode? Next
        {
            get
            {
                return Node.Next;
            }
        }
        
        ILinkedNode? ILinkedNode.Next
        {
            get
            {
                return ((ILinkedNode) Node).Next;
            }
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.Next
        {
            get
            {
                return Convert(Next);
            }
        }
        
        public TNode? Previous
        {
            get
            {
                return Node.Previous;
            }
        }
        
        ILinkedNode? ILinkedNode.Previous
        {
            get
            {
                return ((ILinkedNode) Node).Previous;
            }
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.Previous
        {
            get
            {
                return Convert(Previous);
            }
        }
        
        public override CommandHistoryEntryState State
        {
            get
            {
                return Node.State;
            }
        }
        
        public override CommandHistoryEntryOptions Options
        {
            get
            {
                return Node.Options;
            }
        }
        
        public override Boolean CanExecute
        {
            get
            {
                return Node.CanExecute;
            }
        }
        
        public override Boolean CanRevert
        {
            get
            {
                return Node.CanRevert;
            }
        }
        
        public CommandHistoryLink(TNode node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
        }
        
        public override Boolean Execute()
        {
            return Node switch
            {
                ICommandHistoryEntry history => history.Execute(),
                _ => false
            };
        }
        
        public override Boolean Revert()
        {
            return Node switch
            {
                ICommandHistoryEntry history => history.Revert(),
                _ => false
            };
        }
        
        public TNode? Find(Predicate<TNode> predicate)
        {
            return Node.Find(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.Find(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Convert(Find(node => predicate(Convert(node))));
        }
        
        public TNode? FindLast(Predicate<TNode> predicate)
        {
            return Node.FindLast(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindLast(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Convert(FindLast(node => predicate(Convert(node))));
        }
        
        public TNode? FindPrevious(Predicate<TNode> predicate)
        {
            return Node.FindPrevious(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindPrevious(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Convert(FindPrevious(node => predicate(Convert(node))));
        }
        
        public TNode? FindLastPrevious(Predicate<TNode> predicate)
        {
            return Node.FindLastPrevious(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindLastPrevious(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Convert(FindLastPrevious(node => predicate(Convert(node))));
        }
        
        public TNode? FindNext(Predicate<TNode> predicate)
        {
            return Node.FindNext(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindNext(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Convert(FindNext(node => predicate(Convert(node))));
        }
        
        public TNode? FindLastNext(Predicate<TNode> predicate)
        {
            return Node.FindLastNext(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindLastNext(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Convert(FindLastNext(node => predicate(Convert(node))));
        }
        
        public override Int32 GetHashCode()
        {
            return Node.GetHashCode();
        }
        
        public override Boolean Equals(Object? other)
        {
            return Node.Equals(other);
        }
        
        public override Boolean Equals(ICommandHistoryEntry? other)
        {
            return Node.Equals(other);
        }
        
        public IEnumerator<TNode> GetEnumerator()
        {
            return Node.GetEnumerator();
        }
        
        IEnumerator<ICommandHistoryLinkedEntry> IEnumerable<ICommandHistoryLinkedEntry>.GetEnumerator()
        {
            foreach (TNode node in this)
            {
                yield return Convert(node);
            }
        }
        
        IEnumerator<ILinkedNode<ICommandHistoryLinkedEntry>> IEnumerable<ILinkedNode<ICommandHistoryLinkedEntry>>.GetEnumerator()
        {
            foreach (TNode node in this)
            {
                yield return Convert(node);
            }
        }
        
        IEnumerator<ICommandHistoryLinkedEntry> ILinkedNode<ICommandHistoryLinkedEntry>.GetEnumerator()
        {
            foreach (TNode node in this)
            {
                yield return Convert(node);
            }
        }
        
        IEnumerator<ILinkedNode> ILinkedNode.GetEnumerator()
        {
            return ((ILinkedNode) Node).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Node).GetEnumerator();
        }
        
        public override String? ToString()
        {
            return Node.ToString();
        }
    }
    
    public abstract class CommandHistoryLink : ICommandHistoryEntry
    {
        private static WeakStorage<ICommandHistoryInfo, ICommandHistoryLinkedEntry> NodeStorage { get; } = new WeakStorage<ICommandHistoryInfo, ICommandHistoryLinkedEntry>();
        private static WeakStorage<IReadOnlyLinkedContainer, ICommandHistoryLinkedContainer> ContainerStorage { get; } = new WeakStorage<IReadOnlyLinkedContainer, ICommandHistoryLinkedContainer>();
        
        public abstract ICommand Command { get; }
        public abstract CommandHistoryEntryState State { get; }
        public abstract CommandHistoryEntryOptions Options { get; }
        public abstract Boolean CanExecute { get; }
        public abstract Boolean CanRevert { get; }
        
        [return: NotNullIfNotNull("node")]
        public static ICommandHistoryLinkedEntry? Convert<TNode>(TNode? node) where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode>
        {
            return node switch
            {
                null => null,
                ICommandHistoryLinkedEntry value => value,
                _ => NodeStorage.GetOrAdd(node, static node => new CommandHistoryLink<TNode>((TNode) node))
            };
        }
        
        [return: NotNullIfNotNull("container")]
        public static ICommandHistoryLinkedContainer? Convert<TNode, TContainer>(TContainer? container) where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode> where TContainer : CommandHistoryLinkedContainer<TNode>, new()
        {
            return container switch
            {
                null => null,
                ICommandHistoryLinkedContainer convert => convert,
                _ => ContainerStorage.GetOrAdd(container, static container => new CommandHistoryLinkedContainerWrapper<TNode>((TContainer) container))
            };
        }
        
        public abstract Boolean Execute();
        public abstract Boolean Revert();
        public abstract Boolean Equals(ICommandHistoryEntry? other);
    }
}