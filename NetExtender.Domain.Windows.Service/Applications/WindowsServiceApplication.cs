// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Utils.Application;
using NetExtender.Windows.Services.Types.Installers;
using NetExtender.Windows.Services.Types.Services;
using NetExtender.Windows.Services.Types.Services.Interfaces;
using NetExtender.Windows.Services.Utils;
using NetExtender.Workstation;

namespace NetExtender.Domains.Service.Applications
{
    public class WindowsServiceApplication : Application, IWindowsService
    {
        public override Boolean? Elevate
        {
            get
            {
                return true;
            }
            init
            {
            }
        }
        
        protected override Boolean? IsElevate
        {
            get
            {
                return WorkStation.IsAdministrator;
            }
        }

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

        protected LazyWindowsServiceInitializer Initializer { get; }
        
        public ServiceBase Service
        {
            get
            {
                return Initializer.Service;
            }
        }

        public IContainer? Container
        {
            get
            {
                return Initializer.Container;
            }
        }

        public ISite? Site
        {
            get
            {
                return Initializer.Site;
            }
            set
            {
                Initializer.Site = value;
            }
        }

        public String ServiceName
        {
            get
            {
                return Initializer.ServiceName;
            }
            set
            {
                Initializer.ServiceName = value;
            }
        }

        public EventLog EventLog
        {
            get
            {
                return Initializer.EventLog;
            }
        }

        public Boolean AutoLog
        {
            get
            {
                return Initializer.AutoLog;
            }
            set
            {
                Initializer.AutoLog = value;
            }
        }

        public Boolean CanHandlePowerEvent
        {
            get
            {
                return Initializer.CanHandlePowerEvent;
            }
            set
            {
                Initializer.CanHandlePowerEvent = value;
            }
        }

        public Boolean CanHandleSessionChangeEvent
        {
            get
            {
                return Initializer.CanHandleSessionChangeEvent;
            }
            set
            {
                Initializer.CanHandleSessionChangeEvent = value;
            }
        }

        public Boolean CanPauseAndContinue
        {
            get
            {
                return Initializer.CanPauseAndContinue;
            }
            set
            {
                Initializer.CanPauseAndContinue = value;
            }
        }

        public Boolean CanShutdown
        {
            get
            {
                return Initializer.CanShutdown;
            }
            set
            {
                Initializer.CanShutdown = value;
            }
        }

        public Boolean CanStop
        {
            get
            {
                return Initializer.CanStop;
            }
            set
            {
                Initializer.CanStop = value;
            }
        }

        public Int32 ExitCode
        {
            get
            {
                return Initializer.ExitCode;
            }
            set
            {
                Initializer.ExitCode = value;
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
        
        public Boolean Quiet { get; init; } = true;
        
        public WindowsServiceApplication()
            : this(null, null)
        {
        }
        
        public WindowsServiceApplication(LazyWindowsServiceInitializer? initializer)
            : this(initializer, null)
        {
        }
        
        public WindowsServiceApplication(WindowsServiceInstaller? installer)
            : this(null, installer)
        {
        }

        public WindowsServiceApplication(LazyWindowsServiceInitializer? initializer, WindowsServiceInstaller? installer)
        {
            Initializer = initializer ?? new LazyWindowsServiceInitializer();
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
            
            Initializer.Initialize(service).Run(Quiet);
            return this;
        }

        public void Dispose()
        {
            Initializer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}