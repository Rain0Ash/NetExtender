// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Nodes.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface ICommandHistoryLinkedEntry : ICommandHistoryLinkedEntry<ICommandHistoryLinkedEntry>
    {
    }
    
    public interface ICommandHistoryLinkedEntry<out TNode> : ICommandHistoryEntry, ICommandHistoryLink<TNode> where TNode : class, ICommandHistoryLink<TNode>
    {
    }
    
    public interface ICommandHistoryLink<out TNode> : ICommandHistoryInfo, ILinkedNode<TNode> where TNode : class, ICommandHistoryLink<TNode>
    {
    }
}