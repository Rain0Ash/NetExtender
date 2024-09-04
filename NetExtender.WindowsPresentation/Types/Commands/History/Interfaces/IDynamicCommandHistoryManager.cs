using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface IDynamicCommandHistoryIdManager<TNode> : IDynamicCommandHistoryManager<TNode>, IDynamicCommandHistoryIdManager where TNode : class, ICommandHistoryLink<TNode>
    {
        public new ICommandHistoryLinkedContainer<TNode>? Get<T, TId>(TId id, T parameter) where T : class? where TId : struct;
        public new TNode Create<T, TId>(ICommand<T> command, TId id, T parameter) where T : class? where TId : struct;
        public new TNode Create<T, TId>(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options) where T : class? where TId : struct;
        public new TNode Execute<T, TId>(ICommand<T> command, TId id, T parameter) where T : class? where TId : struct;
        public new TNode Execute<T, TId>(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options) where T : class? where TId : struct;
    }

    public interface IDynamicCommandHistoryManager<TNode> : IDynamicCommandHistoryManager where TNode : class, ICommandHistoryLink<TNode>
    {
        public ICommandHistoryLinkedContainer<TNode>? Get(TNode node);
        public new ICommandHistoryLinkedContainer<TNode>? Get<T>(T parameter) where T : class?;
        public new TNode Create<T>(ICommand<T> command, T parameter) where T : class?;
        public new TNode Create<T>(ICommand<T> command, T parameter, CommandHistoryEntryOptions options) where T : class?;
        public new TNode Execute<T>(ICommand<T> command, T parameter) where T : class?;
        public new TNode Execute<T>(ICommand<T> command, T parameter, CommandHistoryEntryOptions options) where T : class?;
    }
    
    public interface IDynamicCommandHistoryIdManager : IDynamicCommandHistoryManager
    {
        public ICommandHistoryLinkedContainer? Get<T, TId>(TId id, T parameter) where T : class? where TId : struct;
        public ICommandHistoryLinkedEntry Create<T, TId>(ICommand<T> command, TId id, T parameter) where T : class? where TId : struct;
        public ICommandHistoryLinkedEntry Create<T, TId>(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options) where T : class? where TId : struct;
        public ICommandHistoryLinkedEntry Execute<T, TId>(ICommand<T> command, TId id, T parameter) where T : class? where TId : struct;
        public ICommandHistoryLinkedEntry Execute<T, TId>(ICommand<T> command, TId id, T parameter, CommandHistoryEntryOptions options) where T : class? where TId : struct;
        public Boolean Clear<T, TId>(TId id, T parameter) where T : class? where TId : struct;
    }
    
    public interface IDynamicCommandHistoryManager : ICommandHistoryManagerInfo
    {
        public ICommandHistoryLinkedContainer? Get(ICommandHistoryInfo node);
        public ICommandHistoryLinkedContainer? Get<T>(T parameter) where T : class?;
        public ICommandHistoryLinkedEntry Create<T>(ICommand<T> command, T parameter) where T : class?;
        public ICommandHistoryLinkedEntry Create<T>(ICommand<T> command, T parameter, CommandHistoryEntryOptions options) where T : class?;
        public ICommandHistoryLinkedEntry Execute<T>(ICommand<T> command, T parameter) where T : class?;
        public ICommandHistoryLinkedEntry Execute<T>(ICommand<T> command, T parameter, CommandHistoryEntryOptions options) where T : class?;
        public Boolean Clear<T>(T parameter) where T : class?;
    }
}