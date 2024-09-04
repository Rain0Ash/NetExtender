using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface ICommandHistoryEntry<in T> : ICommandHistoryEntry
    {
        public new ICommand<T> Command { get; }
    }
    
    public interface ICommandHistoryEntry : ICommandHistoryInfo, IEquatable<ICommandHistoryEntry>
    {
        public ICommand Command { get; }
        public Boolean Execute();
        public Boolean Revert();
    }
    
    public interface ICommandHistoryInfo
    {
        public CommandHistoryEntryState State { get; }
        public CommandHistoryEntryOptions Options { get; }
        public Boolean CanExecute { get; }
        public Boolean CanRevert { get; }
    }
}