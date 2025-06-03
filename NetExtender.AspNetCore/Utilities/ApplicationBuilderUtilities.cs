// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.AspNetCore.Types.Context.Interfaces;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class ApplicationBuilderUtilities
    {
        public static IApplicationBuilder UseIdentity(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseAuthentication().UseAuthorization();
        }
        
        public static IApplicationBuilder UseRoutingIdentity(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseRouting().UseIdentity();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IApplicationBuilder MapControllers(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseEndpoints(static route => route.MapControllers());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IApplicationBuilder RouteControllers(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseRouting().MapControllers();
        }

        public static void Migrate<TContext>(this IApplicationBuilder builder) where TContext : class, IDbMigrationContext, IDisposable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            using IServiceScope scope = builder.ApplicationServices.CreateScope();
            using TContext context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Migrate();
        }
    }
}