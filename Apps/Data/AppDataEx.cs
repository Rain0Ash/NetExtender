// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Protocols;

namespace NetExtender.Apps.Data
{
    public class AppDataEx : AppData, IAppDataEx
    {
        private readonly URLSchemeProtocol _protocol;

        public Boolean UseProtocol
        {
            get
            {
                return _protocol.IsRegister;
            }
            set
            {
                if (value)
                {
                    _protocol.Register();
                    return;
                }

                _protocol.Unregister();
            }
        }

        public String ProtocolName
        {
            get
            {
                return _protocol.Name;
            }
        }
        
        public AppDataEx(AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : base(version, status, branch)
        {
            _protocol = new URLSchemeProtocol(this);
        }

        public AppDataEx(String name, AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : base(name, version, status, branch)
        {
            _protocol = new URLSchemeProtocol(this);
        }

        public AppDataEx(String name, String sname, AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : base(name, sname, version, status, branch)
        {
            _protocol = new URLSchemeProtocol(this);
        }
    }
}