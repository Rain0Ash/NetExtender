// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Utils;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Utils.Application;
using NetExtender.Windows.Services.Utils;

namespace NetExtender.Domains.AspNetCore.Windows.Service.Applications
{
    public class AspNetCoreWindowsServiceApplication : AspNetCoreApplication
    {
        public virtual IApplication RunAsService()
        {
            return RunAsService(null);
        }
        
        public virtual IApplication RunAsService(IHost? host)
        {
            if (host is null)
            {
                return this;
            }
            
            String name = Domain.ApplicationShortName;
            if (WindowsServiceUtils.IsServiceExist(name))
            {
                WindowsServiceUtils.UninstallService(name);
            }
                
            String? path = ApplicationUtils.Path;
            if (path is not null)
            {
                WindowsServiceUtils.InstallService(path, name);
            }

            Context = host;
            Context.RunAsService();
            return this;
        }
    }
}