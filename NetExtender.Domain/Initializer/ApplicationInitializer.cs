// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Initializer.Interfaces;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Initializer;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Threading;

namespace NetExtender.Domains.Initializer
{
    public abstract class ApplicationInitializer : NetExtender.Initializer.Initializer
    {
        private static ApplicationInitializer? instance;
        protected static ApplicationInitializer Instance
        {
            get
            {
                return instance ?? throw new InvalidOperationException();
            }
            set
            {
                if (instance is not null)
                {
                    throw new InvalidOperationException();
                }
                
                instance = value;
            }
        }

        static ApplicationInitializer()
        {
            Assembly? assembly = Assembly.GetEntryAssembly();
            
            if (assembly is null)
            {
                throw new EntryPointNotFoundException();
            }

            Type[] derived = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(ApplicationInitializer))).ToArray();

            Instance = derived.Length switch
            {
                0 => throw new EntryPointNotFoundException(),
                1 => (ApplicationInitializer?) Activator.CreateInstance(derived[0]),
                _ => throw new AmbiguousMatchException()
            } ?? throw new InvalidOperationException();
        }

        protected ApplicationInitializer()
            : base(instance is not null)
        {
        }

        protected virtual void Initialize()
        {
        }

        private Int32 Internal(String[]? args)
        {
            try
            {
                Initialize();
                Domain.AutoStart(args);
                return 0;
            }
            catch (Exception exception)
            {
                UnhandledException(this, exception, InitializerUnhandledExceptionState.Terminate);
                return exception.HResult;
            }
        }

        private async Task<Int32> InternalAsync(String[]? args, CancellationToken token)
        {
            try
            {
                Initialize();
                await Domain.AutoStartAsync(token);
                return 0;
            }
            catch (Exception exception)
            {
                UnhandledException(this, exception, InitializerUnhandledExceptionState.Terminate);
                return exception.HResult;
            }
        }

        protected static Int32 Sync(String[]? args)
        {
            return Instance.Start(args);
        }
        
        protected static Task<Int32> Async(String[]? args)
        {
            return Instance.StartAsync(args);
        }
        
        protected static Task<Int32> Async(String[]? args, CancellationToken token)
        {
            return Instance.StartAsync(args, token);
        }

        protected Int32 Start(String[]? args)
        {
            return ThreadUtilities.STA(Internal, args);
        }

        protected Task<Int32> StartAsync(String[]? args)
        {
            return StartAsync(args, CancellationToken.None);
        }

        protected async Task<Int32> StartAsync(String[]? args, CancellationToken token)
        {
            return await ThreadUtilities.STA(InternalAsync, args, token);
        }

        protected override void Shutdown(Object? sender, Boolean exit)
        {
            if (exit)
            {
                return;
            }

            if (Domain.IsInitialized)
            {
                Domain.Shutdown();
                return;
            }

            ApplicationUtilities.Shutdown();
        }

        protected override void Terminate(Object? sender, Exception? exception)
        {
            Int32 code = exception?.HResult ?? 1;
            
            if (Domain.IsInitialized)
            {
                Domain.Shutdown(code, true);
                return;
            }
            
            ApplicationUtilities.Shutdown(code);
        }
    }
    
    [SuppressMessage("ReSharper", "MethodSupportsCancellation")]
    public abstract class ApplicationInitializer<TApplication, TView> : IApplicationInitializer where TApplication : class, IApplication, new() where TView : class, IApplicationView, new()
    {
        [return: NotNullIfNotNull("application")]
        public static implicit operator TApplication?(ApplicationInitializer<TApplication, TView>? application)
        {
            return application?.Application;
        }
        
        [return: NotNullIfNotNull("application")]
        public static implicit operator TView?(ApplicationInitializer<TApplication, TView>? application)
        {
            return application?.View;
        }

        protected TApplication Application { get; }
        IApplication IApplicationInitializer.Application
        {
            get
            {
                return Application;
            }
        }

        protected TView View { get; }
        IApplicationView IApplicationInitializer.View
        {
            get
            {
                return View;
            }
        }

        public Boolean? Elevate
        {
            get
            {
                return Application.Elevate;
            }
        }

        public Boolean? IsElevate
        {
            get
            {
                return Application.IsElevate;
            }
        }

        public IDispatcher? Dispatcher
        {
            get
            {
                return Application.Dispatcher;
            }
        }

        public ApplicationShutdownMode ShutdownMode
        {
            get
            {
                return Application.ShutdownMode;
            }
            set
            {
                Application.ShutdownMode = value;
            }
        }

        public CancellationToken ShutdownToken
        {
            get
            {
                return Application.ShutdownToken;
            }
        }

        protected ApplicationInitializer()
        {
            Application = new TApplication();
            View = new TView();
        }

        public IApplicationView Start()
        {
            return View.Start();
        }

        public IApplicationView Start(IEnumerable<String>? args)
        {
            return View.Start(args);
        }

        public IApplicationView Start(params String[]? args)
        {
            return View.Start(args);
        }

        public Task<IApplicationView> StartAsync()
        {
            return View.StartAsync();
        }

        public Task<IApplicationView> StartAsync(CancellationToken token)
        {
            return View.StartAsync(token);
        }

        public Task<IApplicationView> StartAsync(IEnumerable<String>? args)
        {
            return View.StartAsync(args);
        }

        public Task<IApplicationView> StartAsync(params String[]? args)
        {
            return View.StartAsync(args);
        }

        public Task<IApplicationView> StartAsync(IEnumerable<String>? args, CancellationToken token)
        {
            return View.StartAsync(args, token);
        }

        public Task<IApplicationView> StartAsync(CancellationToken token, params String[]? args)
        {
            return View.StartAsync(token, args);
        }

        public IApplication Run()
        {
            return Application.Run();
        }

        public Task<IApplication> RunAsync()
        {
            return Application.RunAsync();
        }

        public Task<IApplication> RunAsync(CancellationToken token)
        {
            return Application.RunAsync(token);
        }

        public void Restart()
        {
            Application.Restart();
        }

        public Task<Boolean> RestartAsync()
        {
            return Application.RestartAsync();
        }

        public Task<Boolean> RestartAsync(Int32 milli)
        {
            return Application.RestartAsync(milli);
        }

        public Task<Boolean> RestartAsync(CancellationToken token)
        {
            return Application.RestartAsync(token);
        }

        public Task<Boolean> RestartAsync(Int32 milli, CancellationToken token)
        {
            return Application.RestartAsync(milli, token);
        }

        public void Shutdown()
        {
            Application.Shutdown();
        }

        public void Shutdown(Int32 code)
        {
            Application.Shutdown(code);
        }

        public void Shutdown(Boolean force)
        {
            Application.Shutdown(force);
        }

        public void Shutdown(Int32 code, Boolean force)
        {
            Application.Shutdown(code, force);
        }

        public Task<Boolean> ShutdownAsync()
        {
            return Application.ShutdownAsync();
        }

        public Task<Boolean> ShutdownAsync(CancellationToken token)
        {
            return Application.ShutdownAsync(token);
        }

        public Task<Boolean> ShutdownAsync(Int32 code)
        {
            return Application.ShutdownAsync(code);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, CancellationToken token)
        {
            return Application.ShutdownAsync(code, token);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli)
        {
            return Application.ShutdownAsync(code, milli);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, CancellationToken token)
        {
            return Application.ShutdownAsync(code, milli, token);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force)
        {
            return Application.ShutdownAsync(code, milli, force);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force, CancellationToken token)
        {
            return Application.ShutdownAsync(code, milli, force, token);
        }

        public void Dispose()
        {
            View.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public abstract class Application<TApplication, TView> : ApplicationInitializer<TApplication, TView> where TApplication : class, IApplication, new() where TView : class, IApplicationView, new()
    {
    }
}