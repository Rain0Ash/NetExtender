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