// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Interfaces;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.UserInterface.Interfaces;

namespace NetExtender.Domains
{
    public static partial class Domain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
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

            public DateTime StartedAt
            {
                get
                {
                    return Data.StartedAt;
                }
            }

            public IApplicationData Data { get; }

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
            
            public String ApplicationShortName
            {
                get
                {
                    return Data.ApplicationShortName;
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
                Culture = CultureInfo.InvariantCulture;
                Data = data;
            }
            
            public IDomain Initialize(IApplication application)
            {
                Application = application ?? throw new ArgumentNullException(nameof(application));
                return this;
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
            
            public IDomain View(IApplicationView view, String[]? args)
            {
                if (view is null)
                {
                    throw new ArgumentNullException(nameof(view));
                }

                view.Start(args);
                return this;
            }
            
            IApplication IApplication.Run()
            {
                return Run();
            }

            public IDomain Run()
            {
                Application.Run();
                return this;
            }
            
            IApplication IApplication.Run<T>(T window)
            {
                return Run(window);
            }
            
            public IDomain Run<T>(T window) where T : IWindow
            {
                Application.Run(window);
                return this;
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

            public void Restart(Int32 milli)
            {
                Application.Restart(milli);
            }

            public void Restart(CancellationToken token)
            {
                Application.Restart(token);
            }

            public void Restart(Int32 milli, CancellationToken token)
            {
                Application.Restart(milli, token);
            }

            private Boolean _disposed;

            public void Dispose()
            {
                DisposeInternal(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(Boolean disposing)
            {
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