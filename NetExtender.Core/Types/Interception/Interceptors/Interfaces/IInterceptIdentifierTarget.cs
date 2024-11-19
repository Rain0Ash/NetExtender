using System;

namespace NetExtender.Types.Interception.Interfaces
{
    public interface IInterceptIdentifierTarget<TSender> : IInterceptIdentifierTarget<TSender, String>
    {
    }
    
    public interface IInterceptIdentifierTarget<TSender, TIdentifier>
    {
        public TIdentifier Identifier { get; init; }
    }
}