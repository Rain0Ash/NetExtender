// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface ISuppressObservableChainHandlerCollection<T, THandler> : IObservableChainHandlerCollection<T, THandler>, ISuppressObservableCollection<THandler> where THandler : IChainHandler<T>
    {
    }
}