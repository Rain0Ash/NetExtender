// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Apps.Domains.GUIViews.Common.Interfaces;
using NetExtender.Exceptions;

namespace NetExtender.Apps.Domains.GUIViews.Common
{
    public abstract class AppGUIView : IGUIView
    {
        public static IGUIView CurrentView { get; private set; }
        protected static Object SyncObject { get; } = new Object();
        
        protected Boolean Started { get; private set; }

        public void Start(String[] args)
        {
            lock (SyncObject)
            {
                if (Started)
                {
                    if (CurrentView == this)
                    {
                        return;
                    }
                
                    throw new AlreadyInitializedException();
                }

                args ??= Array.Empty<String>();

                try
                {
                    StartInitialize();
                    CurrentView = this;
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

        private protected abstract void InitializeInternal();
        
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

        public virtual void Dispose()
        {
        }
    }
}