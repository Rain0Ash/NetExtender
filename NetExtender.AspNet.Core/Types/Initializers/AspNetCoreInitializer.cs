// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using AspNet.Core.Types.Initializers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace NetExtender.AspNet.Core.Types.Initializers
{
    public sealed class AspNetCoreInitializer : IAspNetCoreInitializer
    {
        private IApplicationBuilder Builder { get; }
        
        IServiceProvider IApplicationBuilder.ApplicationServices
        {
            get
            {
                return Builder.ApplicationServices;
            }
            set
            {
                Builder.ApplicationServices = value;
            }
        }

        IFeatureCollection IApplicationBuilder.ServerFeatures
        {
            get
            {
                return Builder.ServerFeatures;
            }
        }

        IDictionary<String, Object?> IApplicationBuilder.Properties
        {
            get
            {
                return Builder.Properties;
            }
        }

        private IWebHostEnvironment Environment { get; }
        
        String IHostEnvironment.ApplicationName
        {
            get
            {
                return Environment.ApplicationName;
            }
            set
            {
                Environment.ApplicationName = value;
            }
        }

        IFileProvider IHostEnvironment.ContentRootFileProvider
        {
            get
            {
                return Environment.ContentRootFileProvider;
            }
            set
            {
                Environment.ContentRootFileProvider = value;
            }
        }

        String IHostEnvironment.ContentRootPath
        {
            get
            {
                return Environment.ContentRootPath;
            }
            set
            {
                Environment.ContentRootPath = value;
            }
        }

        String IHostEnvironment.EnvironmentName
        {
            get
            {
                return Environment.EnvironmentName;
            }
            set
            {
                Environment.EnvironmentName = value;
            }
        }

        String IWebHostEnvironment.WebRootPath
        {
            get
            {
                return Environment.WebRootPath;
            }
            set
            {
                Environment.WebRootPath = value;
            }
        }

        IFileProvider IWebHostEnvironment.WebRootFileProvider
        {
            get
            {
                return Environment.WebRootFileProvider;
            }
            set
            {
                Environment.WebRootFileProvider = value;
            }
        }

        private IServiceCollection Services { get; }
        
        Int32 ICollection<ServiceDescriptor>.Count
        {
            get
            {
                return Services.Count;
            }
        }

        Boolean ICollection<ServiceDescriptor>.IsReadOnly
        {
            get
            {
                return Services.IsReadOnly;
            }
        }

        private IConfiguration Configuration { get; }

        public AspNetCoreInitializer(IApplicationBuilder builder, IWebHostEnvironment environment, IServiceCollection services, IConfiguration configuration)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            Services = services ?? throw new ArgumentNullException(nameof(services));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        IApplicationBuilder IApplicationBuilder.Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            return Builder.Use(middleware);
        }

        IApplicationBuilder IApplicationBuilder.New()
        {
            return Builder.New();
        }

        RequestDelegate IApplicationBuilder.Build()
        {
            return Builder.Build();
        }
        
        Boolean ICollection<ServiceDescriptor>.Contains(ServiceDescriptor item)
        {
            return Services.Contains(item);
        }
        
        Int32 IList<ServiceDescriptor>.IndexOf(ServiceDescriptor item)
        {
            return Services.IndexOf(item);
        }
        
        void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
        {
            Services.Add(item);
        }
        
        void IList<ServiceDescriptor>.Insert(Int32 index, ServiceDescriptor item)
        {
            Services.Insert(index, item);
        }
        
        Boolean ICollection<ServiceDescriptor>.Remove(ServiceDescriptor item)
        {
            return Services.Remove(item);
        }
        
        void IList<ServiceDescriptor>.RemoveAt(Int32 index)
        {
            Services.RemoveAt(index);
        }
        
        void ICollection<ServiceDescriptor>.Clear()
        {
            Services.Clear();
        }
        
        void ICollection<ServiceDescriptor>.CopyTo(ServiceDescriptor[] array, Int32 arrayIndex)
        {
            Services.CopyTo(array, arrayIndex);
        }

        IEnumerator<ServiceDescriptor> IEnumerable<ServiceDescriptor>.GetEnumerator()
        {
            return Services.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Services).GetEnumerator();
        }

        ServiceDescriptor IList<ServiceDescriptor>.this[Int32 index]
        {
            get
            {
                return Services[index];
            }
            set
            {
                Services[index] = value;
            }
        }

        IEnumerable<IConfigurationSection> IConfiguration.GetChildren()
        {
            return Configuration.GetChildren();
        }

        IChangeToken IConfiguration.GetReloadToken()
        {
            return Configuration.GetReloadToken();
        }

        IConfigurationSection IConfiguration.GetSection(String key)
        {
            return Configuration.GetSection(key);
        }

        String IConfiguration.this[String key]
        {
            get
            {
                return Configuration[key];
            }
            set
            {
                Configuration[key] = value;
            }
        }
    }
}