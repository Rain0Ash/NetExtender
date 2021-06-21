// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Windows.Protocols.Interfaces
{
    public interface IProtocolInfo
    {
        public String Name { get; }
        public Boolean IsRegister { get; }
        public ProtocolStatus Status { get; }
    }
}