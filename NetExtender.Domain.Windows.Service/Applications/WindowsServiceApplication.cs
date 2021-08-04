// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Utils.Application;
using NetExtender.Windows.Services.Types.Installers;
using NetExtender.Windows.Services.Types.Services.Interfaces;
using NetExtender.Windows.Services.Utils;

namespace NetExtender.Domains.Service.Applications
{
    public class WindowsServiceApplication : Application
    {
        public override IDispatcher? Dispatcher
        {
            get
            {
                return null;
            }
        }

        public override ApplicationShutdownMode ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
            set
            {
            }
        }
        
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

        public WindowsServiceApplication()
            : this(null)
        {
        }
        
        public WindowsServiceApplication(WindowsServiceInstaller? installer)
        {
            Installer = installer;
        }

        public override IApplication Run()
        {
            return Run(null);
        }

        public virtual IApplication Run(IWindowsService? service)
        {
            if (service is null)
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

            service.RunQuiet();
            return this;
        }
    }
}