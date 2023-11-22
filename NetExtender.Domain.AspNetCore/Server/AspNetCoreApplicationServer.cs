// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Server.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.AspNetCore.Server
{
    public abstract class AspNetCoreApplicationServerAbstraction<T> : IAspNetCoreApplicationServer<T> where T : class
    {
        public static AspNetCoreApplicationServerAbstraction<T> Create(T value)
        {
            switch (value)
            {
                case null:
                    throw new ArgumentNullException(nameof(value));
                case IHost host:
                {
                    Type type = typeof(AspNetCoreApplicationServer<>).MakeGenericType(typeof(T));
                    return Activator.CreateInstance(type, host) as AspNetCoreApplicationServerAbstraction<T> ?? throw new InvalidOperationException();
                }
                case IWebHost host:
                {
                    Type type = typeof(AspNetCoreApplicationWebServer<>).MakeGenericType(typeof(T));
                    return Activator.CreateInstance(type, host) as AspNetCoreApplicationServerAbstraction<T> ?? throw new InvalidOperationException();
                }
                default:
                    throw new NotSupportedException();
            }
        }
        
        public abstract T Context { get; }
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
            Server = Start(Context, Source.Token);
        }

        protected abstract Task Start(T context, CancellationToken token);

        public virtual void Stop()
        {
            Stop(Time.Minute.Half);
        }

        [SuppressMessage("ReSharper", "AsyncConverter.AsyncWait")]
        public virtual void Stop(TimeSpan timeout)
        {
            if (!IsStarted || Server is null || Source is null)
            {
                return;
            }

            Source.Cancel();

            try
            {
                Server.Wait(timeout);
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

    public class AspNetCoreApplicationServer<T> : AspNetCoreApplicationServerAbstraction<T>, IAspNetCoreApplicationServer<T> where T : class, IHost
    {
        public sealed override T Context { get; }

        public AspNetCoreApplicationServer(T context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected override Task Start(T context, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.RunAsync(token);
        }
    }
    
    public class AspNetCoreApplicationWebServer<T> : AspNetCoreApplicationServerAbstraction<T>, IAspNetCoreApplicationServer<T> where T : class, IWebHost
    {
        public sealed override T Context { get; }

        public AspNetCoreApplicationWebServer(T context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected override Task Start(T context, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.RunAsync(token);
        }
    }
}