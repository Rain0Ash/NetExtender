using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface ICommandHistoryEntry : IEquatable<ICommandHistoryEntry>
    {
        public ICommand Command { get; }
        public CommandHistoryEntryState State { get; }
        public Boolean CanExecute { get; }
        public Boolean CanRevert { get; }
        
        public Boolean Execute();
        public Boolean Revert();
    }
}