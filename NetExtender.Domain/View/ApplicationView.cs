// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Utils.Application;
using NetExtender.Utils.Types;

namespace NetExtender.Domains.View
{
    public abstract class ApplicationView : IApplicationView
    {
        public static IApplicationView? Current { get; private set; }

        protected static Object SyncObject { get; } = new Object();

        protected Boolean Started { get; private set; }

        public ImmutableArray<String> Arguments { get; private set; }

        protected virtual ProcessStartInfo? DefaultProcessStartInfo
        {
            get
            {
                String? path = ApplicationUtils.Path;

                if (path is null)
                {
                    return null;
                }
                
                return new ProcessStartInfo(path)
                {
                    UseShellExecute = true,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
        }

        public IApplicationView Start()
        {
            return Start(null);
        }

        public IApplicationView Start(String[]? args)
        {
            lock (SyncObject)
            {
                if (Started)
                {
                    if (Current == this)
                    {
                        return this;
                    }

                    throw new AlreadyInitializedException("View already initialized", nameof(Current));
                }

                try
                {
                    SaveArguments(args);

                    IApplication application = Domain.Current.Application;
                    if (application.Elevate == true && application.IsElevate == false)
                    {
                        Elevate(application);
                    }
                    
                    StartInitialize();
                    Current = this;
                    Started = true;
                    HandleArguments(Arguments);
                    return Run();
                }
                catch (Exception exception)
                {
                    throw new InitializeException($"Exception when starting {GetType()} instance!", exception);
                }
            }
        }

        private void StartInitialize()
        {
            InitializeInternal();

            Initialize();
        }

        protected abstract void InitializeInternal();

        protected virtual void Initialize()
        {
        }

        protected virtual void SaveArguments(IEnumerable<String>? args)
        {
            Arguments = args?.WhereNotNullOrEmpty().ToImmutableArray() ?? ImmutableArray<String>.Empty;
        }
        
        private void HandleArguments(ImmutableArray<String> args)
        {
            HandleArguments(this, args);
        }

        protected virtual void HandleArguments(Object sender, ImmutableArray<String> args)
        {
        }

        protected virtual IApplicationView Run()
        {
            Domain.Run();
            return this;
        }

        protected Boolean Elevate(IApplication application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return ElevateAsync(application).GetAwaiter().GetResult();
        }
        
        protected virtual Task<Boolean> ElevateAsync(IApplication application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (application.IsElevate != false)
            {
                return TaskUtils.True;
            }

            ProcessStartInfo? info = GetProcessElevateInfo(application);

            if (info is null)
            {
                return TaskUtils.False;
            }

            Process? process = Process.Start(info);
            return process is null ? TaskUtils.False : application.ShutdownAsync();
        }

        protected virtual ProcessStartInfo? GetProcessElevateInfo(IApplication application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            String? path = ApplicationUtils.Path;

            if (path is null)
            {
                return null;
            }

            ProcessStartInfo? info = DefaultProcessStartInfo;

            if (info is null)
            {
                return null;
            }
            
            info.Verb = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "runas" : "sudo";
            info.ArgumentList.AddRange(Arguments);

            String? directory = ApplicationUtils.Directory;

            if (directory is not null)
            {
                info.WorkingDirectory = directory;
            }

            return info;
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