// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Server.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.AspNetCore.Server
{
    public sealed class AspNetCoreApplicationServer<THost> : AspNetCoreApplicationServer, IAspNetCoreApplicationServer<THost> where THost : class, IHost
    {
        public override THost Context { get; }

        public AspNetCoreApplicationServer(THost context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
    
    public abstract class AspNetCoreApplicationServer : IAspNetCoreApplicationServer
    {
        public abstract IHost Context { get; }
        protected Task? Server { get; set; }
        protected CancellationTokenSource? Source { get; set; }

        public Boolean IsStarted
        {
            get
            {
                return Server is not null && !Server.IsCompleted;
            }
        }

        public virtual void Start()
        {
            if (Context is null)
            {
                throw new InvalidOperationException();
            }
            
            if (IsStarted)
            {
                return;
            }

            Source = new CancellationTokenSource();
            Server = Context.RunAsync(Source.Token);
        }

        public virtual void Stop()
        {
            if (!IsStarted || Server is null || Source is null)
            {
                return;
            }

            Source.Cancel();

            try
            {
                Server.Wait(Time.Minute.Half);
            }
            catch (Exception)
            {
                //ignored
            }
            finally
            {
                Source.Dispose();
                Source = null;
                Server = null;
            }
        }
    }
}