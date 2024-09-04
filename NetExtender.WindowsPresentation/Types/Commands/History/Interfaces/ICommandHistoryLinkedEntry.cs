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