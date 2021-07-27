// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ServiceProcess;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Utils.Application;
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

        public override IApplication Run()
        {
            return Run(null);
        }

        public virtual IApplication Run(ServiceBase? service)
        {
            if (service is null)
            {
                return this;
            }

            String? path = ApplicationUtils.Path;
            if (path is not null)
            {
                WindowsServiceUtils.InstallServiceIfNotExists(path, Domain.ApplicationShortName);
            }

            ServiceBase.Run(service);
            return this;
        }
    }
}