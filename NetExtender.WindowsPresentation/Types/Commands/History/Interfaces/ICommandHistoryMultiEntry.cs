// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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