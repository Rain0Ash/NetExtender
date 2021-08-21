// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Windows.Protocols.Interfaces;

namespace NetExtender.Windows.Protocols
{
    //TODO: registration
    public abstract class ProtocolRegistration : IProtocol
    {
        public String Name { get; }

        public Boolean IsRegister { get; set; }
        
        public ProtocolStatus Status { get; }

        public abstract Boolean Register();

        public abstract Boolean Unregister();

        protected ProtocolRegistration(String name, ProtocolStatus status)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Status = status;
        }
    }
}