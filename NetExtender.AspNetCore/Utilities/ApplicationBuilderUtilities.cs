// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Builder;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class ApplicationBuilderUtilities
    {
        public static IApplicationBuilder MapControllers(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseEndpoints(route => route.MapControllers());
        }
        
        public static IApplicationBuilder RouteControllers(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseRouting().MapControllers();
        }
    }
}