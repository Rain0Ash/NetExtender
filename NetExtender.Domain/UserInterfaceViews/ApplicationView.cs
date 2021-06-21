// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;
using NetExtender.UserInterface.Interfaces;

namespace NetExtender.Domains.View
{
    public abstract class ApplicationView : IApplicationView
    {
        public static IApplicationView? Current { get; private set; }

        protected static Object SyncObject { get; } = new Object();
        
        protected Boolean Started { get; private set; }

        public void Start(String[] args)
        {
            lock (SyncObject)
            {
                if (Started)
                {
                    if (Current == this)
                    {
                        return;
                    }
                
                    throw new AlreadyInitializedException("View already initialized", nameof(Current));
                }

                args ??= Array.Empty<String>();

                try
                {
                    StartInitialize();
                    Current = this;
                    Started = true;
                    HandleArgs(args);
                    Run();
                }
                catch(Exception)
                {
                    Started = false;
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
            HandleArgs(this, args);
        }

        protected virtual void HandleArgs(Object sender, String[] args)
        {
        }

        protected virtual void Run()
        {
            Domain.Run();
        }
        
        protected virtual void Run<T>(T window) where T : IWindow
        {
            Domain.Run(window);
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