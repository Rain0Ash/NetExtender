using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface ISuppressChainHandlerValue<T, THandler> : IChainHandlerValue<T, THandler>, ISuppressObservableCollection<THandler> where THandler : IChainHandler<T>
    {
    }
}