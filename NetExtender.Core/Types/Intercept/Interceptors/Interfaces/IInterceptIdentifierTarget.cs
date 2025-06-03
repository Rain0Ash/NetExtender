// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IInterceptIdentifierTarget<TSender> : IInterceptIdentifierTarget<TSender, String>
    {
    }
    
    public interface IInterceptIdentifierTarget<TSender, TIdentifier>
    {
        public TIdentifier Identifier { get; init; }
    }
}