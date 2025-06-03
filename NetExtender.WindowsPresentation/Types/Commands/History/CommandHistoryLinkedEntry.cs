// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Input;
using NetExtender.Types.Nodes;
using NetExtender.Types.Nodes.Interfaces;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public class CommandHistoryLinkedEntry : CommandHistoryLinkedEntry<CommandHistoryLinkedEntry>, ICommandHistoryLinkedEntry
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CommandHistoryLinkedEntry?(CommandHistoryEntry? value)
        {
            return value is not null ? new CommandHistoryLinkedEntry(value) : null;
        }

        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.First
        {
            get
            {
                return First;
            }
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.Last
        {
            get
            {
                return Last;
            }
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.Next
        {
            get
            {
                return Next;
            }
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.Previous
        {
            get
            {
                return Previous;
            }
        }
        
        public CommandHistoryLinkedEntry(ICommandHistoryEntry history)
            : base(history)
        {
        }
        
        public CommandHistoryLinkedEntry(CommandHistoryLinkedContainer container, ICommandHistoryEntry history)
            : base(container, history)
        {
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.Find(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            return Find(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindLast(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            return FindLast(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindPrevious(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            return FindPrevious(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindLastPrevious(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            return FindLastPrevious(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindNext(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            return FindNext(predicate);
        }
        
        ICommandHistoryLinkedEntry? ILinkedNode<ICommandHistoryLinkedEntry>.FindLastNext(Predicate<ICommandHistoryLinkedEntry> predicate)
        {
            return FindLastNext(predicate);
        }
        
        IEnumerator<ICommandHistoryLinkedEntry> ILinkedNode<ICommandHistoryLinkedEntry>.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        IEnumerator<ILinkedNode<ICommandHistoryLinkedEntry>> IEnumerable<ILinkedNode<ICommandHistoryLinkedEntry>>.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        IEnumerator<ICommandHistoryLinkedEntry> IEnumerable<ICommandHistoryLinkedEntry>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public class CommandHistoryLinkedEntry<TNode> : CommandHistoryLinkedEntry<ICommandHistoryEntry, TNode> where TNode : CommandHistoryLinkedEntry<TNode>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CommandHistoryLinkedEntry<TNode>?(CommandHistoryEntry? value)
        {
            return value is not null ? new CommandHistoryLinkedEntry<TNode>(value) : null;
        }
        
        public CommandHistoryLinkedEntry(ICommandHistoryEntry history)
            : base(history)
        {
        }
        
        public CommandHistoryLinkedEntry(CommandHistoryLinkedContainer<TNode> container, ICommandHistoryEntry history)
            : base(container, history)
        {
        }
    }
    
    public class CommandHistoryLinkedEntry<THistory, TNode> : LinkedNode<THistory, TNode>, ICommandHistoryLinkedEntry<TNode> where THistory : class, ICommandHistoryEntry where TNode : CommandHistoryLinkedEntry<THistory, TNode>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CommandHistoryLinkedEntry<THistory, TNode>?(THistory? value)
        {
            return value is not null ? new CommandHistoryLinkedEntry<THistory, TNode>(value) : null;
        }
        
        protected CommandHistoryLinkedContainer<TNode>? Container { get; }
        
        protected sealed override TNode? Head
        {
            get
            {
                return LinkedContainersMarshal.Head(Container);
            }
        }
        
        protected THistory History { get; }
        
        public ICommand Command
        {
            get
            {
                return History.Command;
            }
        }
        
        public CommandHistoryEntryState State
        {
            get
            {
                return History.State;
            }
        }
        
        public CommandHistoryEntryOptions Options
        {
            get
            {
                return History.Options;
            }
        }
        
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public virtual Boolean CanExecute
        {
            get
            {
                if (!History.CanExecute)
                {
                    return false;
                }
                
                TNode? current = Previous;
                while (current is not null)
                {
                    if (!current.Options.HasFlag(CommandHistoryEntryOptions.Blocking))
                    {
                        current = current.Previous;
                        continue;
                    }
                    
                    if (current.State is not CommandHistoryEntryState.None && current.State is not CommandHistoryEntryState.Executed)
                    {
                        return false;
                    }
                    
                    current = current.Previous;
                }
                
                current = Next;
                while (current is not null)
                {
                    if (!current.Options.HasFlag(CommandHistoryEntryOptions.Blocking))
                    {
                        current = current.Next;
                        continue;
                    }
                    
                    if (current.State is not CommandHistoryEntryState.None && current.State is not CommandHistoryEntryState.Reverted)
                    {
                        return false;
                    }
                    
                    current = current.Next;
                }
                
                return true;
            }
        }
        
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public virtual Boolean CanRevert
        {
            get
            {
                if (!History.CanRevert)
                {
                    return false;
                }
                
                TNode? current = Next;
                while (current is not null)
                {
                    if (!current.Options.HasFlag(CommandHistoryEntryOptions.Blocking))
                    {
                        current = current.Next;
                        continue;
                    }
                    
                    if (current.State is not CommandHistoryEntryState.None && current.State is not CommandHistoryEntryState.Reverted)
                    {
                        return false;
                    }
                    
                    current = current.Next;
                }
                
                current = Previous;
                while (current is not null)
                {
                    if (!current.Options.HasFlag(CommandHistoryEntryOptions.Blocking))
                    {
                        current = current.Previous;
                        continue;
                    }
                    
                    if (current.State is not CommandHistoryEntryState.None && current.State is not CommandHistoryEntryState.Executed)
                    {
                        return false;
                    }
                    
                    current = current.Previous;
                }
                
                return true;
            }
        }

        public CommandHistoryLinkedEntry(THistory history)
            : this(null, history)
        {
        }
        
        public CommandHistoryLinkedEntry(CommandHistoryLinkedContainer<TNode>? container, THistory history)
        {
            History = history ?? throw new ArgumentNullException(nameof(history));
            Container = container;
        }

        public Boolean Execute()
        {
            return CanExecute && History.Execute();
        }
        
        public Boolean Revert()
        {
            return CanRevert && History.Revert();
        }

        public override Int32 GetHashCode()
        {
            return History.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other as ICommandHistoryEntry);
        }

        public Boolean Equals(ICommandHistoryEntry? other)
        {
            return other is CommandHistoryLinkedEntry<THistory, TNode> history ? History.Equals(history.History) : History.Equals(other);
        }

        public override String? ToString()
        {
            return History.ToString();
        }
    }
}