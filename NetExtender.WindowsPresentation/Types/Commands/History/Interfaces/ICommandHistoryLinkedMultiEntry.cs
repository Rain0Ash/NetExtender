// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface ICommandHistoryLinkedMultiEntry<out TNode> : ICommandHistoryMultiEntry, ICommandHistoryLink<TNode> where TNode : class, ICommandHistoryLinkedMultiEntry<TNode>
    {
    }
}