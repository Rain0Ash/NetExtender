// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Utils;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Exceptions;
using NetExtender.Utils.Application;
using NetExtender.Windows.Services.Types.Installers;
using NetExtender.Windows.Services.Utils;

namespace NetExtender.Domains.AspNetCore.Windows.Service.Applications
{
    public class AspNetCoreWindowsServiceApplication : AspNetCoreApplication
    {
        private WindowsServiceInstaller? _installer;
        public WindowsServiceInstaller? Installer
        {
            get
            {
                if (_installer is not null)
                {
                    return _installer;
                }
                
                String? path = ApplicationUtils.Path;
                if (path is null)
                {
                    return null;
                }

                return _installer ??= new WindowsServiceInstaller(path, Domain.ApplicationIdentifier, Domain.ApplicationName);
            }
            init
            {
                _installer = value;
            }
        }

        public AspNetCoreWindowsServiceApplication()
            : this(null)
        {
        }
        
        public AspNetCoreWindowsServiceApplication(WindowsServiceInstaller? installer)
        {
            Installer = installer;
        }
        
        public override IApplication Run(IHost? host)
        {
            if (host is null)
            {
                return this;
            }
            
            if (Installer is not null && !Installer.TryInstallServiceIfNotExist())
            {
                throw new InitializeException("Can't initialize service. Maybe need elevate execute for install service.");
            }
            
            if (WindowsServiceUtils.IsServiceExist(Domain.ApplicationIdentifier))
            {
                if (!WindowsServiceUtils.TryStartService(Domain.ApplicationIdentifier))
                {
                    throw new InitializeException("Can't start service. Maybe need elevate execute for starting service.");
                }
            }

            Context = host;
            Context.RunAsServiceQuiet();
            return this;
        }
    }
}