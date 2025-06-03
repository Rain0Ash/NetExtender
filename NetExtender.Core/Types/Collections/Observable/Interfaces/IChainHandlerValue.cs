// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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