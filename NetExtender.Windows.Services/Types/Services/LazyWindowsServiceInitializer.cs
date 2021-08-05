// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using NetExtender.Exceptions;
using NetExtender.Windows.Services.Types.Services.Interfaces;
using NetExtender.Windows.Services.Utils;

namespace NetExtender.Windows.Services.Types.Services
{
    public sealed class LazyWindowsServiceInitializer : IWindowsService
    {
        private ServiceBase? _service;
        public ServiceBase Service
        {
            get
            {
                return _service ?? throw new NotInitializedException();
            }
            private set
            {
                if (Initialized)
                {
                    throw new AlreadyInitializedException();
                }

                _service = value;
            }
        }

        public Boolean Initialized
        {
            get
            {
                return _service is not null;
            }
        }

        public IContainer? Container
        {
            get
            {
                return Service.Container;
            }
        }

        private ISite? _site;
        private Boolean _issite;
        public ISite? Site
        {
            get
            {
                return Initialized ? Service.Site : _site;
            }
            set
            {
                if (Initialized)
                {
                    Service.Site = value;
                    return;
                }

                _site = value;
                _issite = true;
            }
        }

        private String? _name;
        public String ServiceName
        {
            get
            {
                return Initialized ? Service.ServiceName : !String.IsNullOrEmpty(_name) ? _name : throw new ArgumentException();
            }
            set
            {
                if (!WindowsServiceUtils.IsValidServiceName(value))
                {
                    throw new ArgumentException("Service name is invalid.", nameof(value));
                }
                
                if (Initialized)
                {
                    Service.ServiceName = value;
                    return;
                }

                _name = value;
            }
        }

        public EventLog EventLog
        {
            get
            {
                return Service.EventLog;
            }
        }

        private Boolean? _autolog;
        public Boolean AutoLog
        {
            get
            {
                return Initialized ? Service.AutoLog : _autolog ?? throw new NotInitializedException();
            }
            set
            {
                if (Initialized)
                {
                    Service.AutoLog = value;
                    return;
                }

                _autolog = value;
            }
        }

        private Boolean? _powerevent;
        public Boolean CanHandlePowerEvent
        {
            get
            {
                return Initialized ? Service.CanHandlePowerEvent : _powerevent ?? throw new NotInitializedException();
            }
            set
            {
                if (Initialized)
                {
                    Service.CanHandlePowerEvent = value;
                    return;
                }

                _powerevent = value;
            }
        }

        private Boolean? _sessionevent;
        public Boolean CanHandleSessionChangeEvent
        {
            get
            {
                return Initialized ? Service.CanHandleSessionChangeEvent : _sessionevent ?? throw new NotInitializedException();
            }
            set
            {
                if (Initialized)
                {
                    Service.CanHandleSessionChangeEvent = value;
                    return;
                }

                _sessionevent = value;
            }
        }

        private Boolean? _pause;
        public Boolean CanPauseAndContinue
        {
            get
            {
                return Initialized ? Service.CanPauseAndContinue : _pause ?? throw new NotInitializedException();
            }
            set
            {
                if (Initialized)
                {
                    Service.CanPauseAndContinue = value;
                    return;
                }

                _pause = value;
            }
        }

        private Boolean? _shutdown;
        public Boolean CanShutdown
        {
            get
            {
                return Initialized ? Service.CanShutdown : _shutdown ?? throw new NotInitializedException();
            }
            set
            {
                if (Initialized)
                {
                    Service.CanShutdown = value;
                    return;
                }

                _shutdown = value;
            }
        }

        private Boolean? _stop;
        public Boolean CanStop
        {
            get
            {
                return Initialized ? Service.CanStop : _stop ?? throw new NotInitializedException();
            }
            set
            {
                if (Initialized)
                {
                    Service.CanStop = value;
                    return;
                }

                _stop = value;
            }
        }

        private Int32? _exitcode;
        public Int32 ExitCode
        {
            get
            {
                return Initialized ? Service.ExitCode : _exitcode ?? throw new NotInitializedException();
            }
            set
            {
                if (Initialized)
                {
                    Service.ExitCode = value;
                    return;
                }

                _exitcode = value;
            }
        }

        public LazyWindowsServiceInitializer()
        {
        }

        public LazyWindowsServiceInitializer(ServiceBase service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public ServiceBase Initialize(ServiceBase service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            service.Site = _issite ? _site : service.Site;
            service.ServiceName = !String.IsNullOrEmpty(_name) ? _name : service.ServiceName;
            service.AutoLog = _autolog ?? service.AutoLog;
            service.CanHandlePowerEvent = _powerevent ?? service.CanHandlePowerEvent;
            service.CanHandleSessionChangeEvent = _sessionevent ?? service.CanHandleSessionChangeEvent;
            service.CanPauseAndContinue = _pause ?? service.CanPauseAndContinue;
            service.CanShutdown = _shutdown ?? service.CanShutdown;
            service.CanStop = _stop ?? service.CanStop;
            service.ExitCode = _exitcode ?? service.ExitCode;

            if (!Initialized)
            {
                Service = service;
            }
            
            return service;
        }
        
        public WindowsService Initialize(WindowsService service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Initialize(service.Service);
            return service;
        }
        
        public IWindowsService Initialize(IWindowsService service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Initialize(service.Service);
            return service;
        }
        
        public void Dispose()
        {
        }
    }
}