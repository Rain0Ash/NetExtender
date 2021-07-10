// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Utils;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.AspNetCore.Applications;

namespace NetExtender.Domain.AspNetCore.Windows.Service.Applications
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

            Context = host;
            Context.RunAsService();
            return this;
        }
    }
}