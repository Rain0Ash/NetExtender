// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;
using NetExtender.Domains.AspNetCore.Builder.Interfaces;
using NetExtender.Domains.Builder;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.AspNetCore.Builder
{
    public class AspNetCoreBuilder : ApplicationBuilder<IHost>, IAspNetCoreBuilder<IHost>
    {
        public virtual Boolean UseDefaultHostBuilder { get; init; } = true;
        
        public override IHost Build(ImmutableArray<String> arguments)
        {
            IHostBuilder builder = new HostBuilder();
            
            if (UseDefaultHostBuilder)
            {
                builder.ConfigureDefaults(arguments.ToArray());
            }
            
            // ReSharper disable once VariableHidesOuterVariable
            void Handler(IWebHostBuilder builder)
            {
                Build(builder);
                Manager?.Invoke(this, builder);
            }

            return builder.ConfigureWebHostDefaults(Handler).Build();
        }

        protected virtual void Build(IWebHostBuilder builder)
        {
        }
    }
    
    public class AspNetCoreBuilder<T> : ApplicationBuilder<T>, IAspNetCoreBuilder<T> where T : class, IHost
    {
        protected virtual Boolean DotNetWatch
        {
            get
            {
                return false;
            }
        }
        
        protected virtual String? DotNetWatchArgument
        {
            get
            {
                return DotNetWatch ? "DOTNET_WATCH" : null;
            }
        }

        protected virtual String? EnvironmentName
        {
            get
            {
                return null;
            }
        }

        protected virtual String? ApplicationName
        {
            get
            {
                return null;
            }
        }

        protected virtual String? ContentRootPath
        {
            get
            {
                return null;
            }
        }

        protected virtual String? WebRootPath
        {
            get
            {
                return null;
            }
        }

        protected virtual IEnumerable<KeyValuePair<String, String?>>? WebHostSettings
        {
            get
            {
                return null;
            }
        }
        
        protected virtual WebApplicationOptions WebApplicationOptions(ImmutableArray<String> arguments)
        {
            return new WebApplicationOptions
            {
                Args = Arguments?.ToArray(),
                EnvironmentName = EnvironmentName,
                ApplicationName = ApplicationName,
                ContentRootPath = ContentRootPath,
                WebRootPath = WebRootPath
            };
        }

        protected virtual void ApplySettings(WebApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (WebHostSettings is not { } settings)
            {
                return;
            }

            foreach ((String key, String? value) in settings!.WhereKeyNotNull())
            {
                builder.WebHost.UseSetting(key, value);
            }
        }

        protected override void Setup(ImmutableArray<String> arguments)
        {
            if (!DotNetWatch)
            {
                base.Setup(arguments);
                return;
            }
            
            String? argument = DotNetWatchArgument;
            if (String.IsNullOrEmpty(argument) || arguments.Contains(argument))
            {
                base.Setup(arguments);
                return;
            }

            InitializeDotNetWatch(argument, arguments);
        }

        //TODO:
        protected virtual void InitializeDotNetWatch(String argument, ImmutableArray<String> arguments)
        {
            if (String.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullOrEmptyStringException(argument, nameof(argument));
            }

            Process process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo("cmd", $"/c dotnet watch -- {argument}")
                {
                    StandardInputEncoding = Console.InputEncoding,
                    StandardOutputEncoding = Console.OutputEncoding,
                    StandardErrorEncoding = Console.OutputEncoding,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.OutputDataReceived += (_, args) => Console.Out.Write(args.Data);
            process.ErrorDataReceived += (_, args) => Console.Error.Write(args.Data);

            process.Start();
            process.WaitForExitAsync().ContinueWith(_ => throw new ShutdownException(process.ExitCode)).Wait();
        }

        protected override TType New<TType>(ImmutableArray<String> arguments)
        {
            Setup(arguments);
            
            try
            {
                WebApplicationOptions options = WebApplicationOptions(arguments);

                if (typeof(TType) == typeof(WebApplication))
                {
                    WebApplication builder = WebApplication.Create(options.Args);
                    return builder as TType ?? throw new InvalidOperationException();
                }
                
                if (typeof(TType) == typeof(WebApplicationBuilder))
                {
                    WebApplicationBuilder builder = WebApplication.CreateBuilder(options);
                    ApplySettings(builder);
                    return builder as TType ?? throw new InvalidOperationException();
                }

                if (typeof(TType) == typeof(WebApplicationBuilderWrapper))
                {
                    WebApplicationBuilderWrapper builder = new WebApplicationBuilderWrapper(options);
                    ApplySettings(builder);
                    return builder as TType ?? throw new InvalidOperationException();
                }
                
                return base.New<TType>(Arguments ?? ImmutableArray<String>.Empty);
            }
            finally
            {
                Finish();
            }
        }

        public override T Build(ImmutableArray<String> arguments)
        {
            Manager?.Invoke(this, this);
            return New(arguments);
        }

        protected virtual T Build(T application)
        {
            Manager?.Invoke(this, this);
            Manager?.Invoke(this, application);
            return application ?? throw new InvalidOperationException();
        }
    }
    
    public class AspNetCoreBuilder<T, TBuilder> : AspNetCoreBuilder<T> where T : class, IHost where TBuilder : class, IHostBuilder
    {
        public override T Build(ImmutableArray<String> arguments)
        {
            return Build(New<TBuilder>(arguments));
        }

        protected virtual T Build(TBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            Manager?.Invoke(this, builder);
            
            try
            {
                return builder.Build() is T application ? Build(application) : throw new InvalidOperationException();
            }
            catch (HostAbortedException exception)
            {
                throw IsDesignMode ? new SuccessfulApplicationAbortException(exception) : new ApplicationAbortException(exception);
            }
        }
    }
    
    public class AspNetCoreWebBuilder : ApplicationBuilder<IWebHost>, IAspNetCoreBuilder<IWebHost>
    {
        public override IWebHost Build(ImmutableArray<String> arguments)
        {
            Manager?.Invoke(this, this);
            return Build(new WebHostBuilder());
        }

        protected virtual IWebHost Build(IWebHostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            Manager?.Invoke(this, builder);

            try
            {
                return builder.Build() is { } application ? Build(application) : throw new InvalidOperationException();
            }
            catch (HostAbortedException exception)
            {
                throw IsDesignMode ? new SuccessfulApplicationAbortException(exception) : new ApplicationAbortException(exception);
            }
        }

        protected virtual IWebHost Build(IWebHost application)
        {
            Manager?.Invoke(this, application);
            return application ?? throw new ArgumentNullException(nameof(application));
        }
    }

    public class AspNetCoreWebBuilder<T> : ApplicationBuilder<T>, IAspNetCoreBuilder<T> where T : class, IWebHost
    {
        protected virtual String? EnvironmentName
        {
            get
            {
                return null;
            }
        }

        protected virtual String? ApplicationName
        {
            get
            {
                return null;
            }
        }

        protected virtual String? ContentRootPath
        {
            get
            {
                return null;
            }
        }

        protected virtual String? WebRootPath
        {
            get
            {
                return null;
            }
        }

        protected virtual IEnumerable<KeyValuePair<String, String?>>? WebHostSettings
        {
            get
            {
                return null;
            }
        }

        protected virtual WebApplicationOptions WebApplicationOptions(ImmutableArray<String> arguments)
        {
            return new WebApplicationOptions
            {
                Args = Arguments?.ToArray(),
                EnvironmentName = EnvironmentName,
                ApplicationName = ApplicationName,
                ContentRootPath = ContentRootPath,
                WebRootPath = WebRootPath
            };
        }

        protected virtual void ApplySettings(WebApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (WebHostSettings is not { } settings)
            {
                return;
            }

            foreach ((String key, String? value) in settings!.WhereKeyNotNull())
            {
                builder.WebHost.UseSetting(key, value);
            }
        }
        
        protected override TType New<TType>(ImmutableArray<String> arguments)
        {
            Setup(arguments);
            
            try
            {
                WebApplicationOptions options = WebApplicationOptions(arguments);
                
                if (typeof(TType) == typeof(WebApplication))
                {
                    WebApplication builder = WebApplication.Create(options.Args);
                    return builder as TType ?? throw new InvalidOperationException();
                }
                
                if (typeof(TType) == typeof(WebApplicationBuilder))
                {
                    WebApplicationBuilder builder = WebApplication.CreateBuilder(options);
                    ApplySettings(builder);
                    return builder as TType ?? throw new InvalidOperationException();
                }
                
                if (typeof(TType) == typeof(WebApplicationBuilderWrapper))
                {
                    WebApplicationBuilderWrapper builder = new WebApplicationBuilderWrapper(options);
                    ApplySettings(builder);
                    return builder as TType ?? throw new InvalidOperationException();
                }
                
                return base.New<TType>(Arguments ?? ImmutableArray<String>.Empty);
            }
            finally
            {
                Finish();
            }
        }
        
        public override T Build(ImmutableArray<String> arguments)
        {
            return New(arguments);
        }

        protected virtual T Build(T application)
        {
            Manager?.Invoke(this, this);
            return application ?? throw new ArgumentNullException(nameof(application));
        }
    }
    
    public class AspNetCoreWebBuilder<T, TBuilder> : AspNetCoreWebBuilder<T> where T : class, IWebHost where TBuilder : class, IWebHostBuilder
    {
        public override T Build(ImmutableArray<String> arguments)
        {
            return Build(New<TBuilder>(arguments));
        }

        protected virtual T Build(TBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            Manager?.Invoke(this, this);
            Manager?.Invoke(this, builder);

            try
            {
                return builder.Build() is T application ? Build(application) : throw new InvalidOperationException();
            }
            catch (HostAbortedException exception)
            {
                throw IsDesignMode ? new SuccessfulApplicationAbortException(exception) : new ApplicationAbortException(exception);
            }
        }
    }
    
    public class AspNetCoreWebApplicationBuilder : AspNetCoreBuilder<WebApplication, WebApplicationBuilderWrapper>
    {
        protected sealed override WebApplication Build(WebApplicationBuilderWrapper wrapper)
        {
            if (wrapper is null)
            {
                throw new ArgumentNullException(nameof(wrapper));
            }

            WebApplicationBuilder builder = Build(wrapper);
            return base.Build(builder);
        }

        protected virtual WebApplicationBuilder Build(WebApplicationBuilder builder)
        {
            Manager?.Invoke(this, this);
            return builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}