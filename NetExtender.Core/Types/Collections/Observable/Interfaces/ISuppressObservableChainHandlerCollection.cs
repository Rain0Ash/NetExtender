using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface ISuppressObservableChainHandlerCollection<T, THandler> : IObservableChainHandlerCollection<T, THandler>, ISuppressObservableCollection<THandler> where THandler : IChainHandler<T>
    {
    }
}