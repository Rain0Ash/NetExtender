// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface ICommandHistoryIdManager<T, in TId, TNode, out TContainer> : ICommandHistoryManager<T, TNode, TContainer>, ICommandHistoryIdManager<T, TId, TNode> where T : class? where TNode : class, ICommandHistoryLink<TNode> where TContainer : class, ICommandHistoryLinkedContainer<TNode>
    {
        public new TContainer? this[TId id, T parameter] { get; }
    }
    
    public interface ICommandHistoryManager<T, TNode, out TContainer> : ICommandHistoryManager<T, TNode> where T : class? where TNode : class, ICommandHistoryLink<TNode> where TContainer : class, ICommandHistoryLinkedContainer<TNode>
    {
        public new TContainer? this[TNode node] { get; }
        public new TContainer? this[T parameter] { get; }
    }

    public interface ICommandHistoryIdManager<T, in TId, TNode> : ICommandHistoryManager<T, TNode>, ICommandHistoryIdManager<T, TId> where T : class? where TNode : class, ICommandHistoryLink<TNode>
    {
        public new TNode Create(ICommand<T> command, TId id, T parameter);
        public new TNode Create(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options);
        public new TNode Execute(ICommand<T> command, TId id, T parameter);
        public new TNode Execute(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options);
        public new Boolean Clear(TId id, T parameter);
        public new ICommandHistoryLinkedContainer<TNode>? this[TId id, T parameter] { get; }
    }
    
    public interface ICommandHistoryManager<T, TNode> : ICommandHistoryManager<T> where T : class? where TNode : class, ICommandHistoryLink<TNode>
    {
        public new TNode Create(ICommand<T> command, T parameter);
        public new TNode Create(ICommand<T> command, T parameter, CommandHistoryEntryOptions options);
        public new TNode Execute(ICommand<T> command, T parameter);
        public new TNode Execute(ICommand<T> command, T parameter, CommandHistoryEntryOptions options);
        public new Boolean Clear(T parameter);
        public ICommandHistoryLinkedContainer<TNode>? this[TNode node] { get; }
        public new ICommandHistoryLinkedContainer<TNode>? this[T parameter] { get; }
    }

    public interface ICommandHistoryIdManager<T, in TId> : ICommandHistoryManager<T> where T : class?
    {
        public ICommandHistoryLinkedEntry Create(ICommand<T> command, TId id, T parameter);
        public ICommandHistoryLinkedEntry Create(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options);
        public ICommandHistoryLinkedEntry Execute(ICommand<T> command, TId id, T parameter);
        public ICommandHistoryLinkedEntry Execute(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options);
        public Boolean Clear(TId id, T parameter);
        public ICommandHistoryLinkedContainer? this[TId id, T parameter] { get; }
    }
    
    public interface ICommandHistoryManager<T> : ICommandHistoryManagerInfo where T : class?
    {
        public ICommandHistoryLinkedEntry Create(ICommand<T> command, T parameter);
        public ICommandHistoryLinkedEntry Create(ICommand<T> command, T parameter, CommandHistoryEntryOptions options);
        public ICommandHistoryLinkedEntry Execute(ICommand<T> command, T parameter);
        public ICommandHistoryLinkedEntry Execute(ICommand<T> command, T parameter, CommandHistoryEntryOptions options);
        public Boolean Clear(T parameter);
        public ICommandHistoryLinkedContainer? this[ICommandHistoryInfo node] { get; }
        public ICommandHistoryLinkedContainer? this[T parameter] { get; }
    }

    public interface ICommandHistoryManagerInfo
    {
        public CommandHistoryEntryOptions Options { get; }
    }
}