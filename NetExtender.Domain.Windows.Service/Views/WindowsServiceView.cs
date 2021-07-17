// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ServiceProcess;
using NetExtender.Domains.Service.Applications;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;

namespace NetExtender.Domains.Service.Views
{
    public class WindowsServiceView : ApplicationView
    {
        protected ServiceBase Context { get; set; }

        public WindowsServiceView(ServiceBase service)
        {
            Context = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ApplicationShutdownMode.OnExplicitShutdown;
        }

        protected override IApplicationView Run()
        {
            return Run(Context);
        }

        protected virtual IApplicationView Run(ServiceBase? service)
        {
            if (service is null)
            {
                return Run();
            }
            
            Context ??= service;
            if (service != Context)
            {
                throw new ArgumentException($"{nameof(service)} not reference equals with {nameof(Context)}");
            }
            
            WindowsServiceApplication application = Domain.Current.Application as WindowsServiceApplication ?? throw new InitializeException($"{nameof(Domain.Current.Application)} is not {nameof(WindowsServiceApplication)}");
            application.Run(Context);
            return this;
        }
    }
}