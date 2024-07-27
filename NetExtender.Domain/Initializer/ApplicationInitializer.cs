// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Initializer.Interfaces;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Initializer;
using NetExtender.Types.Attributes;
using NetExtender.Types.Tasks;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Threading;

namespace NetExtender.Domains.Initializer
{
    [NetExtenderCritical, NetExtenderException]
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
        
        private static Type? _type;
        public static Type Type
        {
            get
            {
                return _type ??= Instance.GetType();
            }
        }
        
        [NetExtenderException]
        static ApplicationInitializer()
        {
            if (Assembly.GetEntryAssembly() is not { } assembly)
            {
                throw new EntryPointNotFoundException();
            }
            
            static ApplicationInitializer Initialize(Type initializer)
            {
                if (initializer is null)
                {
                    throw new ArgumentNullException(nameof(initializer));
                }
                
                try
                {
                    return (ApplicationInitializer?) Activator.CreateInstance(Seal(initializer)) ?? throw new InvalidOperationException();
                }
                catch (Exception)
                {
                    return (ApplicationInitializer?) Activator.CreateInstance(initializer) ?? throw new InvalidOperationException();
                }
            }
            
            Instance = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(ApplicationInitializer))).ToArray() switch
            {
                { Length: 0 } => throw new EntryPointNotFoundException($"Application initializer for assembly '{assembly}' not found."),
                { Length: 1 } initializer => Initialize(initializer[0]),
                { } result => throw new AmbiguousMatchException($"Multiple application initializer was found: {String.Join(", ", (IEnumerable<Type>) result)}."),
                _ => throw new InvalidOperationException()
            };
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
                await Domain.AutoStartAsync(token).ConfigureAwait(false);
                return 0;
            }
            catch (Exception exception)
            {
                UnhandledException(this, exception, InitializerUnhandledExceptionState.Terminate);
                return exception.HResult;
            }
        }

        protected static Awaiter<Int32> Sync(String[]? args)
        {
            return Instance.Start(args);
        }

        protected static Awaiter<Int32> Async(String[]? args)
        {
            return Instance.StartAsync(args);
        }

        protected static Awaiter<Int32> Async(String[]? args, CancellationToken token)
        {
            return Instance.StartAsync(args, token);
        }

        protected Awaiter<Int32> Start(String[]? args)
        {
            return new Awaiter<Int32>(ThreadUtilities.STA(Internal, args));
        }

        protected Awaiter<Int32> StartAsync(String[]? args)
        {
            return StartAsync(args, CancellationToken.None);
        }
        
        protected Awaiter<Int32> StartAsync(String[]? args, CancellationToken token)
        {
            static async Task<Int32> Execute(ApplicationInitializer initializer, String[]? args, CancellationToken token)
            {
                if (initializer is null)
                {
                    throw new ArgumentNullException(nameof(initializer));
                }

                return await ThreadUtilities.STA(initializer.InternalAsync, args, token).ConfigureAwait(false);
            }
            
            return new Awaiter<Int32>(Execute(this, args, token));
        }

        protected override void Shutdown(Object? sender, Int32 code, Boolean exit)
        {
            if (exit)
            {
                return;
            }

            if (!Domain.IsReady)
            {
                ApplicationUtilities.Shutdown(code);
                return;
            }

            Domain.Shutdown(code);
        }

        protected override void Terminate(Object? sender, Exception? exception)
        {
            Int32 code = exception?.HResult ?? 1;

            if (!Domain.IsReady)
            {
                ApplicationUtilities.Shutdown(code);
                return;
            }

            Domain.Shutdown(code, true);
        }

        protected new readonly struct Awaiter<T> : IEquatable<Awaiter<T>>
        {
            public static implicit operator Awaiter<T>(NetExtender.Initializer.Initializer.Awaiter<T> value)
            {
                return value != default ? new Awaiter<T>(value) : default;
            }
            
            public static implicit operator T(Awaiter<T> value)
            {
                return value.Internal;
            }

            public static implicit operator Task<T>(Awaiter<T> value)
            {
                return value.Internal;
            }
            
            public static implicit operator ValueTask<T>(Awaiter<T> value)
            {
                return value.Internal;
            }
            
            public static Boolean operator true(Awaiter<T> value)
            {
                return value != default;
            }

            public static Boolean operator false(Awaiter<T> value)
            {
                return value == default;
            }
            
            public static Boolean operator ==(Awaiter<T> first, Awaiter<T> second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Awaiter<T> first, Awaiter<T> second)
            {
                return !(first == second);
            }

            public static Awaiter<T> operator |(Awaiter<T> first, Awaiter<T> second)
            {
                return first != default ? first : second;
            }

            private AsyncResult<T> Internal { get; }

            public Awaiter(T value)
            {
                Internal = new AsyncResult<T>(value);
            }

            public Awaiter(Task<T> value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                Internal = new AsyncResult<T>(value);
            }

            public Awaiter(ValueTask<T> value)
            {
                Internal = new AsyncResult<T>(value);
            }

            public T AsValue()
            {
                return this;
            }

            public Task<T> AsTask()
            {
                return this;
            }

            public ValueTask<T> AsValueTask()
            {
                return this;
            }

            public ValueTaskAwaiter<T> GetAwaiter()
            {
                return Internal.GetAwaiter();
            }
            
            public ConfiguredValueTaskAwaitable<T> ConfigureAwait(Boolean continueOnCapturedContext)
            {
                return Internal.ConfigureAwait(continueOnCapturedContext);
            }
            
            public override Int32 GetHashCode()
            {
                return Internal.GetHashCode();
            }

            public override Boolean Equals(Object? other)
            {
                return other is Awaiter<T> result && Equals(result);
            }

            public Boolean Equals(Awaiter<T> other)
            {
                return Internal.Equals(other.Internal);
            }
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

        protected TApplication Application { get; } = new TApplication();

        IApplication IApplicationInitializer.Application
        {
            get
            {
                return Application;
            }
        }

        protected TView View { get; } = new TView();

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