// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Protocols;
using NetExtender.Types.Protocols.Interfaces;

namespace NetExtender.Windows.Protocols
{
    public abstract class ProtocolRegistration : IProtocol
    {
        public String Name { get; }

        public abstract ProtocolStatus Status { get; }

        public virtual Boolean IsRegister
        {
            get
            {
                return Status == ProtocolStatus.Register;
            }
            set
            {
                if (value)
                {
                    if (Status == ProtocolStatus.Register)
                    {
                        return;
                    }

                    Register();
                    return;
                }

                if (Status == ProtocolStatus.Unregister)
                {
                    return;
                }

                Unregister(true);
            }
        }

        public Boolean IsUnknown
        {
            get
            {
                return Status == ProtocolStatus.Unknown;
            }
        }

        public Boolean IsAnother
        {
            get
            {
                return Status == ProtocolStatus.Another;
            }
        }

        public Boolean IsError
        {
            get
            {
                return Status == ProtocolStatus.Error;
            }
        }

        protected ProtocolRegistration(String name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        
        public abstract Boolean Register();

        public Boolean Unregister()
        {
            return Unregister(false);
        }
        
        public abstract Boolean Unregister(Boolean force);
    }
}