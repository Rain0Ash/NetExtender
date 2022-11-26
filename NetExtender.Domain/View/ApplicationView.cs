// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.View
{
    public abstract class ApplicationView : IApplicationView
    {
        public static IApplicationView? Current { get; private set; }

        protected Boolean Started { get; private set; }

        public ImmutableArray<String> Arguments { get; private set; }

        protected virtual ProcessStartInfo? DefaultProcessStartInfo
        {
            get
            {
                String? path = ApplicationUtilities.Path;

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

        protected virtual ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return null;
            }
        }

        public IApplicationView Start()
        {
            return Start(null);
        }

        public IApplicationView Start(IEnumerable<String>? args)
        {
            return StartAsync(args).GetAwaiter().GetResult();
        }

        public IApplicationView Start(params String[]? args)
        {
            return StartAsync(args).GetAwaiter().GetResult();
        }

        public Task<IApplicationView> StartAsync()
        {
            return StartAsync(CancellationToken.None);
        }

        public Task<IApplicationView> StartAsync(CancellationToken token)
        {
            return StartAsync(null, token);
        }

        public Task<IApplicationView> StartAsync(IEnumerable<String>? args)
        {
            return StartAsync(args, CancellationToken.None);
        }

        public Task<IApplicationView> StartAsync(params String[]? args)
        {
            return StartAsync(args, CancellationToken.None);
        }

        public async Task<IApplicationView> StartAsync(IEnumerable<String>? args, CancellationToken token)
        {
            if (Started)
            {
                if (Current == this)
                {
                    return this;
                }

                throw new AlreadyInitializedException("View already initialized", nameof(Current));
            }

            token.ThrowIfCancellationRequested();

            try
            {
                SaveArguments(args);

                IApplication application = Domain.Current.Application;
                if (application.Elevate == true && application.IsElevate == false)
                {
                    await ElevateAsync(application, token).ConfigureAwait(false);
                }

                Initialize(ShutdownMode);
                Current = this;
                Started = true;
                HandleArguments(Arguments);
                return await RunAsync(token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new InitializeException($"Exception when starting {GetType()} instance!", exception);
            }
        }

        public Task<IApplicationView> StartAsync(CancellationToken token, params String[]? args)
        {
            return StartAsync(args, token);
        }

        protected virtual void Initialize()
        {
        }

        private void Initialize(ApplicationShutdownMode? mode)
        {
            if (mode is not null)
            {
                Domain.ShutdownMode = mode.Value;
            }

            Initialize();
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
            return RunAsync().GetAwaiter().GetResult();
        }

        protected Task<IApplicationView> RunAsync()
        {
            return RunAsync(CancellationToken.None);
        }

        protected virtual async Task<IApplicationView> RunAsync(CancellationToken token)
        {
            await Domain.RunAsync(token).ConfigureAwait(false);
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

        protected Task<Boolean> ElevateAsync(IApplication application)
        {
            return ElevateAsync(application, CancellationToken.None);
        }

        protected virtual Task<Boolean> ElevateAsync(IApplication application, CancellationToken token)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (application.IsElevate != false)
            {
                return TaskUtilities.True;
            }

            ProcessStartInfo? info = GetProcessElevateInfo(application);

            if (info is null)
            {
                return TaskUtilities.False;
            }

            using Process? process = Process.Start(info);
            return process is null ? TaskUtilities.False : application.ShutdownAsync(token);
        }

        protected virtual ProcessStartInfo? GetProcessElevateInfo(IApplication application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            String? path = ApplicationUtilities.Path;

            if (path is null)
            {
                return null;
            }

            ProcessStartInfo? info = DefaultProcessStartInfo;

            if (info is null)
            {
                return null;
            }

            info.Verb = RuntimeInformationUtilities.ElevateVerbose ?? throw new PlatformNotSupportedException();

            info.ArgumentList.AddRange(Arguments);

            String? directory = ApplicationUtilities.Directory;

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