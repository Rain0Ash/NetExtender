// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Domains.View
{
    public abstract class ApplicationView : IApplicationView
    {
        public static IApplicationView? Current { get; private set; }

        protected static Object SyncObject { get; } = new Object();

        protected Boolean Started { get; private set; }

        public ImmutableArray<String> Arguments { get; private set; }

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
                    StartInitialize();
                    Current = this;
                    Started = true;
                    HandleArgs(args ?? Array.Empty<String>());
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

        protected void HandleArgs(String[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Arguments = args.WhereNotNullOrEmpty().ToImmutableArray();
            HandleArgs(this, args);
        }

        protected virtual void HandleArgs(Object sender, String[] args)
        {
        }

        protected virtual IApplicationView Run()
        {
            Domain.Run();
            return this;
        }

        protected virtual IApplicationView Run<T>(T window) where T : IWindow
        {
            Domain.Run(window);
            return this;
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