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
        public static IServiceCollection AddSpaStaticFiles(this IServiceCollection collection, DirectoryInfo directory)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            void Configuration(SpaStaticFilesOptions configuration)
            {
                configuration.RootPath = directory.FullName;
            }

            collection.AddSpaStaticFiles(Configuration);
            return collection;
        }

        public static IServiceCollection AddSpaStaticFiles(this IServiceCollection collection, String path)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
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

            collection.AddSpaStaticFiles(Configuration);
            return collection;
        }
    }
}