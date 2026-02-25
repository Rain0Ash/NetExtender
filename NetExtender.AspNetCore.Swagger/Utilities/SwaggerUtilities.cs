using System;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.AspNetCore.Swagger.Schema.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetExtender.AspNetCore.Utilities
{
    public static class SwaggerUtilities
    {
        public static IServiceCollection Swagger(this IServiceCollection services, Action<SwaggerGenOptions>? configure = null)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSwaggerGen(options =>
            {
                configure?.Invoke(options);
                /*options.SchemaFilter<EnumSchemaFilter>();*/
            }).AddSwaggerGenNewtonsoftSupport();
        }

        public static void AddJsonIgnoreFilter(this SwaggerGenOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.SchemaFilter<JsonIgnoreSchemaFilter>();
        }

        public static IServiceCollection AddSwaggerJsonIgnoreFilter(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.Configure<SwaggerGenOptions>(static options => options.AddJsonIgnoreFilter());
        }
    }
}