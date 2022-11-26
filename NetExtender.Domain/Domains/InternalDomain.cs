// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Interfaces;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Domains
{
    public static partial class Domain
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Local")]
        [SuppressMessage("ReSharper", "MethodSupportsCancellation")]
        private class InternalDomain : IDomain
        {
            private IApplication? _application;
            public IApplication Application
            {
                get
                {
                    return _application ?? throw new NotInitializedException("Application is not initialized", nameof(Application));
                }
                private set
                {
                    if (_application is not null)
                    {
                        throw new AlreadyInitializedException("Application already initialized", nameof(Application));
                    }

                    _application = value;
                }
            }

            public Boolean IsReady
            {
                get
                {
                    return _application is not null;
                }
            }

            public IApplicationData Data { get; }

            public Boolean? Elevate { get; init; }

            public Boolean? IsElevate { get; init; }

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

            public Guid Guid
            {
                get
                {
                    return Data.Guid;
                }
            }

            public ApplicationVersion Version
            {
                get
                {
                    return Data.Version;
                }
            }

            public ApplicationInfo Information
            {
                get
                {
                    return Data.Information;
                }
            }

            public DateTime StartedAt
            {
                get
                {
                    return Data.StartedAt;
                }
            }

            public ApplicationStatus Status
            {
                get
                {
                    return Data.Status;
                }
            }

            public String StatusData
            {
                get
                {
                    return Data.StatusData;
                }
            }

            public ApplicationBranch Branch
            {
                get
                {
                    return Data.Branch;
                }
            }

            public String BranchData
            {
                get
                {
                    return Data.BranchData;
                }
            }

            public String ApplicationName
            {
                get
                {
                    return Data.ApplicationName;
                }
            }

            public String ApplicationIdentifier
            {
                get
                {
                    return Data.ApplicationIdentifier;
                }
            }

            public CultureInfo Culture
            {
                get
                {
                    return Thread.CurrentThread.CurrentCulture;
                }
                set
                {
                    Thread.CurrentThread.CurrentCulture = value;
                }
            }

            public Boolean AlreadyStarted
            {
                get
                {
                    return Data.HasAnotherInstance;
                }
            }

            public InternalDomain(IApplicationData data)
            {
                Data = data ?? throw new ArgumentNullException(nameof(data));
                Culture = CultureInfo.InvariantCulture;
            }

            public IDomain Initialize<T>() where T : IApplication, new()
            {
                return Initialize(new T());
            }

            public IDomain Initialize(IApplication application)
            {
                Application = application ?? throw new ArgumentNullException(nameof(application));
                return this;
            }

            public IDomain View<T>() where T : IApplicationView, new()
            {
                return View(new T());
            }

            public IDomain View(IApplicationView view)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                view.Start();
                return this;
            }

            public IDomain View<T>(IEnumerable<String>? args) where T : IApplicationView, new()
            {
                return View(new T(), args);
            }

            public IDomain View(IApplicationView view, IEnumerable<String>? args)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                view.Start(args);
                return this;
            }

            public IDomain View<T>(params String[]? args) where T : IApplicationView, new()
            {
                return View(new T(), args);
            }

            public IDomain View(IApplicationView view, params String[]? args)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                view.Start(args);
                return this;
            }

            public Task<IDomain> ViewAsync<T>() where T : IApplicationView, new()
            {
                return ViewAsync(new T());
            }

            public async Task<IDomain> ViewAsync(IApplicationView view)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                await view.StartAsync();
                return this;
            }

            public Task<IDomain> ViewAsync<T>(CancellationToken token) where T : IApplicationView, new()
            {
                return ViewAsync(new T(), token);
            }

            public async Task<IDomain> ViewAsync(IApplicationView view, CancellationToken token)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                await view.StartAsync(token);
                return this;
            }

            public Task<IDomain> ViewAsync<T>(IEnumerable<String>? args) where T : IApplicationView, new()
            {
                return ViewAsync(new T(), args);
            }

            public async Task<IDomain> ViewAsync(IApplicationView view, IEnumerable<String>? args)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                await view.StartAsync(args);
                return this;
            }

            public Task<IDomain> ViewAsync<T>(params String[]? args) where T : IApplicationView, new()
            {
                return ViewAsync(new T(), args);
            }

            public async Task<IDomain> ViewAsync(IApplicationView view, params String[]? args)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                await view.StartAsync(args);
                return this;
            }

            public Task<IDomain> ViewAsync<T>(IEnumerable<String>? args, CancellationToken token) where T : IApplicationView, new()
            {
                return ViewAsync(new T(), args, token);
            }

            public async Task<IDomain> ViewAsync(IApplicationView view, IEnumerable<String>? args, CancellationToken token)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                await view.StartAsync(args, token);
                return this;
            }

            public Task<IDomain> ViewAsync<T>(CancellationToken token, params String[] args) where T : IApplicationView, new()
            {
                return ViewAsync(new T(), token, args);
            }

            public async Task<IDomain> ViewAsync(IApplicationView view, CancellationToken token, params String[]? args)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                await view.StartAsync(args, token);
                return this;
            }

            public IDomain Run()
            {
                Application.Run();
                return this;
            }

            public async Task<IDomain> RunAsync()
            {
                await Application.RunAsync();
                return this;
            }

            public async Task<IDomain> RunAsync(CancellationToken token)
            {
                await Application.RunAsync(token);
                return this;
            }

            IApplication IApplication.Run()
            {
                return Application.Run();
            }

            Task<IApplication> IApplication.RunAsync()
            {
                return Application.RunAsync();
            }

            Task<IApplication> IApplication.RunAsync(CancellationToken token)
            {
                return Application.RunAsync(token);
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

            private Boolean _disposed;

            public void Dispose()
            {
                DisposeInternal(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(Boolean disposing)
            {
                Shutdown();
            }

            private void DisposeInternal(Boolean disposing)
            {
                if (_disposed)
                {
                    return;
                }

                Dispose(disposing);
                _disposed = true;
            }
        }
    }
}