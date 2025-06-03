// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Nodes.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface ICommandHistoryLinkedContainer : ICommandHistoryLinkedContainer<ICommandHistoryLinkedEntry>
    {
    }
    
    public interface ICommandHistoryLinkedContainer<out TNode> : IReadOnlyLinkedContainer<TNode> where TNode : class, ICommandHistoryLink<TNode>
    {
    }
}