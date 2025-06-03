// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using NetExtender.Types.Concurrent.Dictionaries;
using NetExtender.Types.Entities;
using NetExtender.Types.Nodes;
using NetExtender.Types.Storages;
using NetExtender.Utilities.Core;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public class DynamicCommandHistoryManager : DynamicCommandHistoryManager<CommandHistoryLinkedEntry>
    {
    }
    
    public class DynamicCommandHistoryManager<TNode> : DynamicCommandHistoryManager<TNode, CommandHistoryLinkedContainer<TNode>> where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode>
    {
    }
    
    public class DynamicCommandHistoryManager<TNode, TContainer> : IDynamicCommandHistoryIdManager<TNode> where TNode : LinkedNode<TNode>, ICommandHistoryLink<TNode> where TContainer : CommandHistoryLinkedContainer<TNode>, new()
    {
        protected NullableConcurrentDictionary<Object?, NullableConcurrentDictionary<Object?, TContainer>> Entries { get; } = new NullableConcurrentDictionary<Object?, NullableConcurrentDictionary<Object?, TContainer>>();
        protected WeakStorage<TNode, TContainer> Storage { get; } = new WeakStorage<TNode, TContainer>();
        
        [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
        protected static class Factory<T>
        {
            private static Func<ICommand<T>, T, CommandHistoryEntryOptions, TNode>? factory;

            public static TNode Create(ICommand<T> command, T parameter, CommandHistoryEntryOptions options)
            {
                factory ??= ReflectionUtilities.New<TNode, ICommand<T>, T, CommandHistoryEntryOptions>();
                return factory(command, parameter, options);
            }
        }
        
        public CommandHistoryEntryOptions Options { get; init; }
        
        protected virtual TNode Node<T, TId>(ICommand<T> command, TId? id, T parameter, CommandHistoryEntryOptions options)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return Factory<T>.Create(command, parameter, options);
        }
        
        public virtual TContainer? Get(TNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            
            return Storage.TryGetValue(node, out TContainer? container) ? container : null;
        }
        
        ICommandHistoryLinkedContainer<TNode>? IDynamicCommandHistoryManager<TNode>.Get(TNode node)
        {
            return Get(node);
        }
        
        ICommandHistoryLinkedContainer? IDynamicCommandHistoryManager.Get(ICommandHistoryInfo node)
        {
            return CommandHistoryLink.Convert<TNode, TContainer>(node is TNode value ? Get(value) : null);
        }
        
        protected virtual TContainer? Get<T, TId>(TId? id, T parameter) where T : class? where TId : struct
        {
            return Entries.TryGetValue(parameter, out NullableConcurrentDictionary<Object?, TContainer>? storage) && storage.TryGetValue(id, out TContainer? container) ? container : null;
        }
        
        public TContainer? Get<T>(T parameter) where T : class?
        {
            return Get(default(Any.Value?), parameter);
        }
        
        ICommandHistoryLinkedContainer<TNode>? IDynamicCommandHistoryManager<TNode>.Get<T>(T parameter)
        {
            return Get(parameter);
        }
        
        ICommandHistoryLinkedContainer? IDynamicCommandHistoryManager.Get<T>(T parameter)
        {
            return CommandHistoryLink.Convert<TNode, TContainer>(Get(parameter));
        }
        
        public TContainer? Get<T, TId>(TId id, T parameter) where T : class? where TId : struct
        {
            return Get((TId?) id, parameter);
        }
        
        ICommandHistoryLinkedContainer<TNode>? IDynamicCommandHistoryIdManager<TNode>.Get<T, TId>(TId id, T parameter)
        {
            return Get(id, parameter);
        }
        
        ICommandHistoryLinkedContainer? IDynamicCommandHistoryIdManager.Get<T, TId>(TId id, T parameter)
        {
            return CommandHistoryLink.Convert<TNode, TContainer>(Get(id, parameter));
        }
        
        protected TNode Create<T, TId>(ICommand<T> command, TId? id, T parameter) where T : class? where TId : struct
        {
            return Create(command, id, parameter, Options);
        }
        
        protected virtual TNode Create<T, TId>(ICommand<T> command, TId? id, T parameter, CommandHistoryEntryOptions options) where T : class? where TId : struct
        {
            NullableConcurrentDictionary<Object?, TContainer> entries = Entries.GetOrAdd(parameter, static _ => new NullableConcurrentDictionary<Object?, TContainer>());
            TContainer container = entries.GetOrAdd(id, static _ => new TContainer());
            
            lock (container)
            {
                TNode node = Node(command, id, parameter, options);
                Storage.GetOrAdd(node, container);
                container.AddLast(node);
                return node;
            }
        }
        
        public TNode Create<T>(ICommand<T> command, T parameter) where T : class?
        {
            return Create(command, default(Any.Value?), parameter);
        }
        
        ICommandHistoryLinkedEntry IDynamicCommandHistoryManager.Create<T>(ICommand<T> command, T parameter)
        {
            return CommandHistoryLink.Convert(Create(command, parameter));
        }
        
        public TNode Create<T>(ICommand<T> command, T parameter, CommandHistoryEntryOptions options) where T : class?
        {
            return Create(command, default(Any.Value?), parameter, options);
        }
        
        ICommandHistoryLinkedEntry IDynamicCommandHistoryManager.Create<T>(ICommand<T> command, T parameter, CommandHistoryEntryOptions options)
        {
            return CommandHistoryLink.Convert(Create(command, parameter, options));
        }
        
        public TNode Create<T, TId>(ICommand<T> command, TId id, T parameter) where T : class? where TId : struct
        {
            return Create(command, (TId?) id, parameter);
        }
        
        ICommandHistoryLinkedEntry IDynamicCommandHistoryIdManager.Create<T, TId>(ICommand<T> command, TId id, T parameter)
        {
            return CommandHistoryLink.Convert(Create(command, id, parameter));
        }
        
        public TNode Create<T, TId>(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options) where T : class? where TId : struct
        {
            return Create(command, (TId?) id, parameter, options);
        }
        
        ICommandHistoryLinkedEntry IDynamicCommandHistoryIdManager.Create<T, TId>(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options)
        {
            return CommandHistoryLink.Convert(Create(command, id, parameter, options));
        }
        
        protected TNode Execute<T, TId>(ICommand<T> command, TId? id, T parameter) where T : class? where TId : struct
        {
            return Execute(command, id, parameter, Options);
        }
        
        protected virtual TNode Execute<T, TId>(ICommand<T> command, TId? id, T parameter, CommandHistoryEntryOptions options) where T : class? where TId : struct
        {
            TNode node = Create(command, id, parameter, options);
            
            if (node is ICommandHistoryEntry history)
            {
                history.Execute();
            }
            
            return node;
        }

        public TNode Execute<T>(ICommand<T> command, T parameter) where T : class?
        {
            return Execute(command, default(Any.Value?), parameter);
        }
        
        ICommandHistoryLinkedEntry IDynamicCommandHistoryManager.Execute<T>(ICommand<T> command, T parameter)
        {
            return CommandHistoryLink.Convert(Execute(command, parameter));
        }
        
        public TNode Execute<T>(ICommand<T> command, T parameter, CommandHistoryEntryOptions options) where T : class?
        {
            return Execute(command, default(Any.Value?), parameter, options);
        }
        
        ICommandHistoryLinkedEntry IDynamicCommandHistoryManager.Execute<T>(ICommand<T> command, T parameter, CommandHistoryEntryOptions options)
        {
            return CommandHistoryLink.Convert(Execute(command, parameter, options));
        }
        
        public TNode Execute<T, TId>(ICommand<T> command, TId id, T parameter) where T : class? where TId : struct
        {
            return Execute(command, (TId?) id, parameter);
        }
        
        ICommandHistoryLinkedEntry IDynamicCommandHistoryIdManager.Execute<T, TId>(ICommand<T> command, TId id, T parameter)
        {
            return CommandHistoryLink.Convert(Execute(command, id, parameter));
        }
        
        public TNode Execute<T, TId>(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options) where T : class? where TId : struct
        {
            return Execute(command, (TId?) id, parameter, options);
        }
        
        ICommandHistoryLinkedEntry IDynamicCommandHistoryIdManager.Execute<T, TId>(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options)
        {
            return CommandHistoryLink.Convert(Execute(command, id, parameter, options));
        }
        
        protected virtual Boolean Clear<T, TId>(TId? id, T parameter) where T : class? where TId : struct
        {
            if (!Entries.TryGetValue(parameter, out NullableConcurrentDictionary<Object?, TContainer>? entries))
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
        
        public Boolean Clear<T>(T parameter) where T : class?
        {
            return Clear(default(Any.Value?), parameter);
        }

        public Boolean Clear<T, TId>(TId id, T parameter) where T : class? where TId : struct
        {
            return Clear((TId?) id, parameter);
        }
    }
}