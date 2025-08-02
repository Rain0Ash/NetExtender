using System;
using System.Net.Http;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using NetExtender.Utilities.Network;

namespace NetExtender.AspNetCore.Filters
{
    public class TracingHttpMessageHandlerBuilderFilter : TracingHttpMessageHandlerBuilderFilter<TracingHttpMessageHandlerBuilderFilter.Configuration>
    {
        public TracingHttpMessageHandlerBuilderFilter(IServiceProvider provider, ILoggerFactory logger)
            : this(provider, logger, null)
        {
        }

        public TracingHttpMessageHandlerBuilderFilter(IServiceProvider provider, ILoggerFactory logger, Func<IServiceProvider, HttpMessageHandlerBuilder, Configuration>? configurator)
            : base(provider, logger, configurator)
        {
        }

        public record Configuration
        {
            public Boolean IsEnabled { get; init; } = true;
            public String? Category { get; init; }
            public Predicate<HttpResponseMessage>? Validator { get; init; }
            public Boolean Buffering { get; init; }
        }
    }

    public abstract class TracingHttpMessageHandlerBuilderFilter<TConfiguration> : IHttpMessageHandlerBuilderFilter where TConfiguration : TracingHttpMessageHandlerBuilderFilter.Configuration
    {
        protected IServiceProvider Provider { get; }
        protected ILoggerFactory Logger { get; }
        protected Func<IServiceProvider, HttpMessageHandlerBuilder, TConfiguration>? Configurator { get; }

        protected TracingHttpMessageHandlerBuilderFilter(IServiceProvider provider, ILoggerFactory logger)
            : this(provider, logger, null)
        {
        }

        protected TracingHttpMessageHandlerBuilderFilter(IServiceProvider provider, ILoggerFactory logger, Func<IServiceProvider, HttpMessageHandlerBuilder, TConfiguration>? configurator)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Configurator = configurator;
        }

        public virtual Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            return builder =>
            {
                next(builder);

                DelegatingHandler? handler = Configurator?.Invoke(Provider, builder) switch
                {
                    null => new HttpTracingHandler(Logger.CreateLogger(HttpTracingHandler.LoggerCategory(builder.Name)), null, false),
                    { IsEnabled: true } configuration => new HttpTracingHandler(Logger.CreateLogger(configuration.Category ?? HttpTracingHandler.LoggerCategory(builder.Name)), configuration.Validator, configuration.Buffering),
                    _ => null
                };

                if (handler is not null)
                {
                    builder.AdditionalHandlers.Add(handler);
                }
            };
        }
    }
}