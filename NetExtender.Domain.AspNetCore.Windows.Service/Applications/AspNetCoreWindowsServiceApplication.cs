// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Utils;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Service.Applications;
using NetExtender.Windows.Services.Types.Installers;
using NetExtender.Windows.Services.Types.Services;

namespace NetExtender.Domains.AspNetCore.Windows.Service.Applications
{
    public class AspNetCoreWindowsServiceApplication : WindowsServiceApplication
    {
        protected IHost? Context { get; set; }
        
        public AspNetCoreWindowsServiceApplication()
        {
        }
        
        public AspNetCoreWindowsServiceApplication(LazyWindowsServiceInitializer? initializer)
            : base(initializer)
        {
        }
        
        public AspNetCoreWindowsServiceApplication(WindowsServiceInstaller? installer)
            : base(installer)
        {
        }

        public AspNetCoreWindowsServiceApplication(LazyWindowsServiceInitializer? initializer, WindowsServiceInstaller? installer)
            : base(initializer, installer)
        {
        }
        
        public override IApplication Run()
        {
            return Run(null);
        }

        public virtual IApplication Run(IHost? host)
        {
            if (host is null)
            {
                return this;
            }

            Context = host;
            return Run(Context.AsService());
        }
        
        public override void Shutdown(Int32 code)
        {
            try
            {
                Context?.StopAsync().GetAwaiter().GetResult();
            }
            finally
            {
                base.Shutdown(code);
            }
        }
    }
}