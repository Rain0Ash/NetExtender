using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface ICommandHistoryMultiEntry<in T> : ICommandHistoryMultiEntry
    {
        public new IMultiCommand<T> Command { get; }
    }
    
    public interface ICommandHistoryMultiEntry : ICommandHistoryEntry
    {
        public new IMultiCommand Command { get; }
    }
}