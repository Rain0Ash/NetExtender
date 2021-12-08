// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class SinglePageApplicationBuilderUtilities
    {
        public static ISpaBuilder SetDefaultPage(this ISpaBuilder builder, PathString page)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Options.DefaultPage = page;
            return builder;
        }
        
        public static ISpaBuilder SetSourcePath(this ISpaBuilder builder, String? path)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Options.SourcePath = path;
            return builder;
        }

        public static ISpaBuilder SetStartupTimeout(this ISpaBuilder builder, Int32 timeout)
        {
            return SetStartupTimeout(builder, TimeSpan.FromMilliseconds(timeout));
        }

        public static ISpaBuilder SetStartupTimeout(this ISpaBuilder builder, TimeSpan timeout)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Options.StartupTimeout = timeout;
            return builder;
        }
        
        public static ISpaBuilder SetPackageManagerCommand(this ISpaBuilder builder, String command)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Options.PackageManagerCommand = command;
            return builder;
        }
        
        public static ISpaBuilder SetDefaultPageStaticFileOptions(this ISpaBuilder builder, StaticFileOptions? command)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Options.DefaultPageStaticFileOptions = command;
            return builder;
        }
    }
}