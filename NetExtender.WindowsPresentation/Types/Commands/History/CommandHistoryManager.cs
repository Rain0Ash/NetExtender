using System;
using System.Windows.Input;
using NetExtender.Types.Concurrent.Dictionaries;
using NetExtender.Types.Nodes;
using NetExtender.Types.Storages;
using NetExtender.Utilities.Core;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public class CommandHistoryManager<T, TNode> : CommandHistoryManager<T, UInt64, TNode> where T : class? where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode>
    {
    }

    public class CommandHistoryManager<T, TId, TNode> : CommandHistoryManager<T, TId, TNode, CommandHistoryLinkedContainer<TNode>> where T : class? where TId : struct where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode>
    {
    }

    public class CommandHistoryManager<T, TId, TNode, TContainer> : ICommandHistoryIdManager<T, TId, TNode, TContainer> where T : class? where TId : struct where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode> where TContainer : CommandHistoryLinkedContainer<TNode>, new()
    {
        private static Func<ICommand<T>, T, CommandHistoryEntryOptions, TNode>? _factory;
        protected static Func<ICommand<T>, T, CommandHistoryEntryOptions, TNode> Factory
        {
            get
            {
                return _factory ??= ReflectionUtilities.New<TNode, ICommand<T>, T, CommandHistoryEntryOptions>();
            }
        }

        protected NullableConcurrentDictionary<T, NullableConcurrentDictionary<TId?, TContainer>> Entries { get; } = new NullableConcurrentDictionary<T, NullableConcurrentDictionary<TId?, TContainer>>();
        protected WeakStorage<TNode, TContainer> Storage { get; } = new WeakStorage<TNode, TContainer>();
        public CommandHistoryEntryOptions Options { get; init; }

        protected virtual TNode Node(ICommand<T> command, TId? id, T parameter, CommandHistoryEntryOptions options)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            
            return Factory(command, parameter, options);
        }
        
        protected TNode Create(ICommand<T> command, TId? id, T parameter)
        {
            return Create(command, id, parameter, Options);
        }
        
        protected virtual TNode Create(ICommand<T> command, TId? id, T parameter, CommandHistoryEntryOptions options)
        {
            NullableConcurrentDictionary<TId?, TContainer> entries = Entries.GetOrAdd(parameter, static _ => new NullableConcurrentDictionary<TId?, TContainer>());
            TContainer container = entries.GetOrAdd(id, static _ => new TContainer());
            
            lock (container)
            {
                TNode node = Node(command, id, parameter, options);
                Storage.GetOrAdd(node, container);
                container.AddLast(node);
                return node;
            }
        }
        
        public TNode Create(ICommand<T> command, T parameter)
        {
            return Create(command, default(TId?), parameter);
        }
        
        ICommandHistoryLinkedEntry ICommandHistoryManager<T>.Create(ICommand<T> command, T parameter)
        {
            return CommandHistoryLink.Convert(Create(command, parameter));
        }
        
        public TNode Create(ICommand<T> command, T parameter, CommandHistoryEntryOptions options)
        {
            return Create(command, default(TId?), parameter, options);
        }
        
        ICommandHistoryLinkedEntry ICommandHistoryManager<T>.Create(ICommand<T> command, T parameter, CommandHistoryEntryOptions options)
        {
            return CommandHistoryLink.Convert(Create(command, parameter, options));
        }
        
        public TNode Create(ICommand<T> command, TId id, T parameter)
        {
            return Create(command, (TId?) id, parameter);
        }
        
        ICommandHistoryLinkedEntry ICommandHistoryIdManager<T, TId>.Create(ICommand<T> command, TId id, T parameter)
        {
            return CommandHistoryLink.Convert(Create(command, id, parameter));
        }
        
        public TNode Create(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options)
        {
            return Create(command, (TId?) id, parameter, options);
        }
        
        ICommandHistoryLinkedEntry ICommandHistoryIdManager<T, TId>.Create(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options)
        {
            return CommandHistoryLink.Convert(Create(command, id, parameter, options));
        }
        
        protected TNode Execute(ICommand<T> command, TId? id, T parameter)
        {
            return Execute(command, id, parameter, Options);
        }
        
        protected virtual TNode Execute(ICommand<T> command, TId? id, T parameter, CommandHistoryEntryOptions options)
        {
            TNode node = Create(command, id, parameter, options);
            
            if (node is ICommandHistoryEntry history)
            {
                history.Execute();
            }
            
            return node;
        }
        
        public TNode Execute(ICommand<T> command, T parameter)
        {
            return Execute(command, default(TId?), parameter);
        }
        
        ICommandHistoryLinkedEntry ICommandHistoryManager<T>.Execute(ICommand<T> command, T parameter)
        {
            return CommandHistoryLink.Convert(Execute(command, parameter));
        }
        
        public TNode Execute(ICommand<T> command, T parameter, CommandHistoryEntryOptions options)
        {
            return Execute(command, default(TId?), parameter, options);
        }
        
        ICommandHistoryLinkedEntry ICommandHistoryManager<T>.Execute(ICommand<T> command, T parameter, CommandHistoryEntryOptions options)
        {
            return CommandHistoryLink.Convert(Execute(command, parameter, options));
        }
        
        public TNode Execute(ICommand<T> command, TId id, T parameter)
        {
            return Execute(command, (TId?) id, parameter);
        }
        
        ICommandHistoryLinkedEntry ICommandHistoryIdManager<T, TId>.Execute(ICommand<T> command, TId id, T parameter)
        {
            return CommandHistoryLink.Convert(Execute(command, id, parameter));
        }
        
        public TNode Execute(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options)
        {
            return Execute(command, (TId?) id, parameter, options);
        }
        
        ICommandHistoryLinkedEntry ICommandHistoryIdManager<T, TId>.Execute(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options)
        {
            return CommandHistoryLink.Convert(Execute(command, id, parameter, options));
        }
        
        protected virtual Boolean Clear(TId? id, T parameter)
        {
            if (!Entries.TryGetValue(parameter, out NullableConcurrentDictionary<TId?, TContainer>? entries))
            {
                return false;
            }
            
            if (!entries.TryRemove(id, out TContainer? container))
            {
                return false;
            }
            
            lock (container)
            {
                foreach (TNode node in container)
                {
                    Storage.Remove(node);
                }
            }

            return true;
        }
        
        public Boolean Clear(T parameter)
        {
            return Clear(default(TId?), parameter);
        }
        
        public Boolean Clear(TId id, T parameter)
        {
            return Clear((TId?) id, parameter);
        }
        
        public virtual TContainer? this[TNode node]
        {
            get
            {
                if (node is null)
                {
                    throw new ArgumentNullException(nameof(node));
                }
                
                return Storage.TryGetValue(node, out TContainer? container) ? container : null;
            }
        }

        ICommandHistoryLinkedContainer<TNode>? ICommandHistoryManager<T, TNode>.this[TNode node]
        {
            get
            {
                return this[node];
            }
        }
        
        ICommandHistoryLinkedContainer? ICommandHistoryManager<T>.this[ICommandHistoryInfo node]
        {
            get
            {
                return CommandHistoryLink.Convert<TNode, TContainer>(node is TNode value ? this[value] : null);
            }
        }

        protected virtual TContainer? this[TId? id, T parameter]
        {
            get
            {
                return Entries.TryGetValue(parameter, out NullableConcurrentDictionary<TId?, TContainer>? storage) && storage.TryGetValue(id, out TContainer? container) ? container : null;
            }
        }

        public TContainer? this[T parameter]
        {
            get
            {
                return this[default(TId?), parameter];
            }
        }
        
        ICommandHistoryLinkedContainer<TNode>? ICommandHistoryManager<T, TNode>.this[T parameter]
        {
            get
            {
                return this[parameter];
            }
        }
        
        ICommandHistoryLinkedContainer? ICommandHistoryManager<T>.this[T parameter]
        {
            get
            {
                return CommandHistoryLink.Convert<TNode, TContainer>(this[parameter]);
            }
        }

        public TContainer? this[TId id, T parameter]
        {
            get
            {
                return this[(TId?) id, parameter];
            }
        }
        
        ICommandHistoryLinkedContainer<TNode>? ICommandHistoryIdManager<T, TId, TNode>.this[TId id, T parameter]
        {
            get
            {
                return this[(TId?) id, parameter];
            }
        }
        
        ICommandHistoryLinkedContainer? ICommandHistoryIdManager<T, TId>.this[TId id, T parameter]
        {
            get
            {
                TContainer? result = this[id, parameter];
                return CommandHistoryLink.Convert<TNode, TContainer>(result);
            }
        }
    }
}