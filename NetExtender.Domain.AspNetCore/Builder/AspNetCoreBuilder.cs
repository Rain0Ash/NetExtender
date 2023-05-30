// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Builder;

namespace NetExtender.Domains.AspNetCore.Builder
{
    public class AspNetCoreBuilder : ApplicationBuilder<IHost>
    {
        public Boolean UseDefaultHostBuilder { get; init; } = true;
        
        public override IHost Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }
            
            IHostBuilder builder = new HostBuilder();
            
            if (UseDefaultHostBuilder)
            {
                builder.ConfigureDefaults(arguments);
            }

            return builder.ConfigureWebHostDefaults(Build).Build();
        }

        protected virtual void Build(IWebHostBuilder builder)
        {
        }
    }
    
    public class AspNetCoreBuilder<T> : ApplicationBuilder<T> where T : class, IHost
    {
        public override T Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return New();
        }
    }
}