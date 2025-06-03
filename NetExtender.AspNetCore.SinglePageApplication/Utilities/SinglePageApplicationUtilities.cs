// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class SinglePageApplicationUtilities
    {
        public static IServiceCollection AddSpaStaticFiles(this IServiceCollection services, DirectoryInfo directory)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            void Configuration(SpaStaticFilesOptions configuration)
            {
                configuration.RootPath = directory.FullName;
            }

            services.AddSpaStaticFiles(Configuration);
            return services;
        }

        public static IServiceCollection AddSpaStaticFiles(this IServiceCollection services, String path)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (String.IsNullOrWhiteSpace(path))
            {
                path = ".";
            }

            void Configuration(SpaStaticFilesOptions configuration)
            {
                configuration.RootPath = path;
            }

            services.AddSpaStaticFiles(Configuration);
            return services;
        }
    }
}