// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Utilities;
using NetExtender.Domains.View.Interfaces;

namespace NetExtender.Domains.View
{
    public abstract class ContextApplicationView<TContext, TApplication> : ApplicationView where TContext : class where TApplication : class, IApplication<TContext>
    {
        protected abstract TContext? Context { get; set; }

        protected abstract override Task<IApplicationView> RunAsync(CancellationToken token);

        protected virtual async Task<IApplicationView> RunAsync(TContext? context, CancellationToken token)
        {
            if (context is null)
            {
                return await RunAsync(token).ConfigureAwait(false);
            }

            Context ??= context;
            if (context != Context)
            {
                throw new ArgumentException($"{nameof(context)} not reference equals with {nameof(Context)}");
            }

            TApplication application = Domain.Current.Application.As<TApplication>();
            await application.RunAsync(Context, token).ConfigureAwait(false);
            return this;
        }
    }
}