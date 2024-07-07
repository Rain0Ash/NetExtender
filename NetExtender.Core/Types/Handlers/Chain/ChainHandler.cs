using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Initializer.Types.Handlers.Chain
{
    public class DynamicChainHandler<T> : ChainHandler<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator DynamicChainHandler<T>?(Func<T, T>? value)
        {
            return value is not null ? new DynamicChainHandler<T>(value) : null;
        }
        
        protected Func<T, T> Handler { get; }
        
        public DynamicChainHandler(Func<T, T> handler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }
        
        public override T Handle(T value)
        {
            return Handler.Invoke(value);
        }
    }
    
    public abstract class ChainHandler<T> : IChainHandler<T>
    {
        public abstract T Handle(T value);
    }
}