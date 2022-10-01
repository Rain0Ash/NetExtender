// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Protocols.Interfaces
{
    public interface IProtocol : IProtocolInfo
    {
        public new Boolean IsRegister { get; set; }

        public Boolean Register();
        public Boolean Unregister();
    }
}