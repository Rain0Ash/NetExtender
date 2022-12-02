// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Protocols;
using NetExtender.Workstation;

namespace NetExtender.Windows.Protocols
{
    public class FileTypeAssociationProtocol : TypeAssociationProtocol
    {
        public String Extension
        {
            get
            {
                return Name;
            }
        }

        public override (String? ProgId, String? Hash)? Info
        {
            get
            {
                (String? progid, String? hash) = GetFileTypeAssociation(Extension);

                if (progid is null && hash is null)
                {
                    return null;
                }
                
                return (progid, hash);
            }
        }

        public FileTypeAssociationProtocol(String extension)
            : base(extension)
        {
        }
        
        public override Boolean Register()
        {
            switch (Status)
            {
                case ProtocolStatus.Register:
                    return true;
                case ProtocolStatus.Unknown:
                case ProtocolStatus.Another:
                case ProtocolStatus.Error:
                    String? progid = ProgId;

                    if (progid is not null)
                    {
                        return SetFileTypeAssociation(progid, Extension, Icon, Sid);
                    }
                    
                    return Path is not null && RegisterFileTypeAssociation(Path, Extension, null, Icon, Sid);
                case ProtocolStatus.Unregister:
                    return Path is not null && RegisterFileTypeAssociation(Path, Extension, null, Icon, Sid);
                default:
                    throw new NotSupportedException();
            }
        }

        public override Boolean Unregister(Boolean force)
        {
            if (Status == ProtocolStatus.Unregister)
            {
                return true;
            }
            
            if (!force && IsAnother)
            {
                return false;
            }

            String? progid = RegisterProgId;
            return progid is null || RemoveFileTypeAssociation(progid, Extension);
        }
    }

    public class UrlTypeAssociationProtocol : TypeAssociationProtocol
    {
        public String Protocol
        {
            get
            {
                return Name;
            }
        }

        public override (String? ProgId, String? Hash)? Info
        {
            get
            {
                (String? progid, String? hash) = GetProtocolTypeAssociation(Protocol);
                
                if (progid is null && hash is null)
                {
                    return null;
                }
                
                return (progid, hash);
            }
        }

        public UrlTypeAssociationProtocol(String protocol)
            : base(protocol)
        {
        }

        public UrlTypeAssociationProtocol(String? path, String protocol)
            : base(path, protocol)
        {
        }
        
        public override Boolean Register()
        {
            switch (Status)
            {
                case ProtocolStatus.Register:
                    return true;
                case ProtocolStatus.Unknown:
                case ProtocolStatus.Another:
                case ProtocolStatus.Error:
                    String? progid = ProgId;

                    if (progid is not null)
                    {
                        return SetProtocolTypeAssociation(progid, Protocol, Icon, Sid);
                    }
                    
                    return Path is not null && RegisterProtocolTypeAssociation(Path, Protocol, null, Icon, Sid);
                case ProtocolStatus.Unregister:
                    return Path is not null && RegisterProtocolTypeAssociation(Path, Protocol, null, Icon, Sid);
                default:
                    throw new NotSupportedException();
            }
        }

        public override Boolean Unregister(Boolean force)
        {
            if (Status == ProtocolStatus.Unregister)
            {
                return true;
            }
            
            if (!force && IsAnother)
            {
                return false;
            }

            String? progid = RegisterProgId;
            return progid is null || RemoveProtocolTypeAssociation(progid, Protocol);
        }
    }

    public abstract partial class TypeAssociationProtocol : ProtocolRegistration
    {
        public abstract (String? ProgId, String? Hash)? Info { get; }

        public String? RegisterProgId
        {
            get
            {
                return Info?.ProgId;
            }
        }

        public String? RegisterHash
        {
            get
            {
                return Info?.Hash;
            }
        }

        private readonly String? _progid;
        public String? ProgId
        {
            get
            {
                return !String.IsNullOrEmpty(_progid) ? _progid : RegisterProgId;
            }
            init
            {
                _progid = value;
            }
        }

        protected Boolean IsEqualsProgId
        {
            get
            {
                return String.Equals(ProgId, RegisterProgId, StringComparison.Ordinal);
            }
        }

        public String? Path { get; }
        public String? Icon { get; init; }
        public String? Sid { get; init; } = WorkStation.CurrentUserSID;
        
        public override ProtocolStatus Status
        {
            get
            {
                if (Info is not { } info)
                {
                    return ProtocolStatus.Unregister;
                }

                if (info.ProgId is null || info.Hash is null)
                {
                    return ProtocolStatus.Error;
                }

                return IsEqualsProgId ? ProtocolStatus.Register : ProtocolStatus.Another;
            }
        }

        protected TypeAssociationProtocol(String name)
            : this(null, name)
        {
        }

        protected TypeAssociationProtocol(String? path, String name)
            : base(name)
        {
            Path = path;
        }
    }
}