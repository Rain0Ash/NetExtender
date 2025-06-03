// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;
using NetExtender.Domains.AspNetCore.Builder.Interfaces;
using NetExtender.Domains.Builder;
using NetExtender.Types.Middlewares;
using NetExtender.Types.Middlewares.Exceptions;
using NetExtender.Types.Middlewares.Interfaces;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.AspNetCore.Builder.Middlewares
{
    [ApplicationBuilderMiddleware]
    internal sealed class BuilderServiceScanMiddleware : Middleware<IServiceCollection>, IMiddlewareConverter<IHostBuilder, IServiceCollection>, IMiddlewareConverter<IWebHostBuilder, IServiceCollection>, IMiddlewareConverter<WebApplicationBuilder, IServiceCollection>
    {
        public BuilderServiceScanMiddleware()
        {
            Idempotency = MiddlewareIdempotencyMode.Argument;
        }
        
        public override void Invoke(Object? sender, IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (!Memorize(sender, services))
            {
                return;
            }

            switch (sender)
            {
                case IScannable { IsScan: false }:
                    return;
                case IAspNetCoreBuilder builder:
                    services.InjectFrom(builder.ServiceHandler);
                    return;
                default:
                    services.InjectFrom();
                    return;
            }
        }

        public IServiceCollection Convert(Object? sender, IHostBuilder builder)
        {
            switch (builder)
            {
                case null:
                    throw new ArgumentNullException(nameof(builder));
                case WebApplicationBuilderWrapper wrapper:
                    return Convert(sender, wrapper.Builder);
                default:
                    builder.ConfigureServices(collection => Invoke(sender, collection));
                    throw new MiddlewareConvertNoInvokeException();
            }
        }
        
        public IServiceCollection Convert(Object? sender, IWebHostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            builder.ConfigureServices(collection => Invoke(sender, collection));
            throw new MiddlewareConvertNoInvokeException();
        }

        public IServiceCollection Convert(Object? sender, WebApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return builder.Services;
        }
    }
}