namespace NetExtender.WindowsPresentation.Types.Commands.History.Interfaces
{
    public interface ICommandHistoryLinkedMultiEntry<out TNode> : ICommandHistoryMultiEntry, ICommandHistoryLink<TNode> where TNode : class, ICommandHistoryLinkedMultiEntry<TNode>
    {
    }
}