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
        public override IApplication Run(IHost? host)
        {
            if (host is null)
            {
                return this;
            }
            
            String? path = ApplicationUtils.Path;
            if (path is not null)
            {
                WindowsServiceUtils.InstallServiceIfNotExists(path, Domain.ApplicationIdentifier, Domain.ApplicationName);
            }

            Context = host;
            Context.RunAsServiceQuiet();
            return this;
        }
    }
}