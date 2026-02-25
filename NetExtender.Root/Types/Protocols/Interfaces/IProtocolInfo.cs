// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Protocols.Interfaces
{
    public interface IProtocolInfo
    {
        public String Name { get; }
        public ProtocolStatus Status { get; }
        public Boolean IsRegister { get; }
        public Boolean IsUnknown { get; }
        public Boolean IsAnother { get; }
        public Boolean IsError { get; }
    }
}