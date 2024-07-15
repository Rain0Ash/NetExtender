using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface ISuppressChainHandlerValue<T, THandler> : IChainHandlerValue<T, THandler>, ISuppressObservableChainHandlerCollection<T, THandler> where THandler : IChainHandler<T>
    {
    }
}