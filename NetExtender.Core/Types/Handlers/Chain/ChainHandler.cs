// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Handlers.Chain.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Handlers.Chain
{
    public readonly struct DynamicChainHandler<T> : IChainHandler<T>, IEquatable<DynamicChainHandler<T>>, IEquatable<Func<T, T>>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator DynamicChainHandler<T>?(Func<T, T>? value)
        {
            return value is not null ? new DynamicChainHandler<T>(value) : null;
        }
        
        private static Func<T, T> Default { get; } = static value => value;
        private Maybe<Func<T, T>> Handler { get; }
        
        public DynamicChainHandler(Func<T, T> handler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }
        
        public T Handle(T value)
        {
            return Handler.Unwrap(Default).Invoke(value);
        }
        
        public override Int32 GetHashCode()
        {
            return Handler.GetHashCode();
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                DynamicChainHandler<T> handler => Equals(handler),
                Func<T, T> handler => Equals(handler),
                _ => false
            };
        }
        
        public Boolean Equals(DynamicChainHandler<T> other)
        {
            return Handler.Equals(other.Handler);
        }
        
        public Boolean Equals(Func<T, T>? other)
        {
            return Handler.Equals(other);
        }
        
        public override String? ToString()
        {
            return Handler.ToString();
        }
    }
    
    public abstract class ChainHandler<T> : IChainHandler<T>
    {
        public abstract T Handle(T value);
    }
}