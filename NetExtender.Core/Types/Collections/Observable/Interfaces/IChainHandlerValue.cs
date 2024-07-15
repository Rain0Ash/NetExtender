using System;
using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IChainHandlerValue<T> : IChainHandlerValue<T, IChainHandler<T>>
    {
    }
    
    public interface IChainHandlerValue<T, THandler> : IObservableChainHandlerCollection<T, THandler> where THandler : IChainHandler<T>
    {
        public T Initial { get; }
        public T Value { get; }
        
        public Boolean Update();
    }
}