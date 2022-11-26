// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Windows.Services.Types.Services
{
    public sealed class WindowsServiceWrapper : IWindowsService
    {
        [return: NotNullIfNotNull("service")]
        public static implicit operator ServiceBase?(WindowsServiceWrapper? service)
        {
            return service?.Service;
        }

        [return: NotNullIfNotNull("service")]
        public static implicit operator WindowsServiceWrapper?(ServiceBase? service)
        {
            return service is not null ? new WindowsServiceWrapper(service) : null;
        }

        public ServiceBase Service { get; }

        public IContainer? Container
        {
            get
            {
                return Service.Container;
            }
        }

        public ISite? Site
        {
            get
            {
                return Service.Site;
            }
            set
            {
                Service.Site = value;
            }
        }

        public String ServiceName
        {
            get
            {
                return Service.ServiceName;
            }
            set
            {
                Service.ServiceName = value;
            }
        }

        public EventLog EventLog
        {
            get
            {
                return Service.EventLog;
            }
        }

        public Boolean AutoLog
        {
            get
            {
                return Service.AutoLog;
            }
            set
            {
                Service.AutoLog = value;
            }
        }

        public Boolean CanHandlePowerEvent
        {
            get
            {
                return Service.CanHandlePowerEvent;
            }
            set
            {
                Service.CanHandlePowerEvent = value;
            }
        }

        public Boolean CanHandleSessionChangeEvent
        {
            get
            {
                return Service.CanHandleSessionChangeEvent;
            }
            set
            {
                Service.CanHandleSessionChangeEvent = value;
            }
        }

        public Boolean CanPauseAndContinue
        {
            get
            {
                return Service.CanPauseAndContinue;
            }
            set
            {
                Service.CanPauseAndContinue = value;
            }
        }

        public Boolean CanShutdown
        {
            get
            {
                return Service.CanShutdown;
            }
            set
            {
                Service.CanShutdown = value;
            }
        }

        public Boolean CanStop
        {
            get
            {
                return Service.CanStop;
            }
            set
            {
                Service.CanStop = value;
            }
        }

        public Int32 ExitCode
        {
            get
            {
                return Service.ExitCode;
            }
            set
            {
                Service.ExitCode = value;
            }
        }

        public WindowsServiceWrapper(ServiceBase service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void Dispose()
        {
            Service.Dispose();
        }
    }
}