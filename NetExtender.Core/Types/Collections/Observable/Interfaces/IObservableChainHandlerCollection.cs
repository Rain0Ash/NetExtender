using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IObservableChainHandlerCollection<T, THandler> : IObservableCollection<THandler>, IChainHandler<T> where THandler : IChainHandler<T>
    {
    }
}