// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NetExtender.Watchers.FileSystem.Interfaces;

 namespace NetExtender.Watchers.FileSystem.Other
{
    /// <summary>
    /// An abstract wrapper for an IFilesystemWatcher
    /// </summary>
    public abstract class WatcherWrapper : IWatcher
    {
        private IWatcher _internalWatcher;

        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Deleted;
        public event ErrorEventHandler Error;
        public event RenamedEventHandler Renamed;


        protected IWatcher InternalWatcher
        {
            get
            {
                return _internalWatcher;
            }
            set
            {
                UnsubscribeFromInternalWatcherEvents();
                _internalWatcher = value;
                SubscribeToPrivateWatcherEvents();
            }
        }

        public virtual Boolean EnableRaisingEvents
        {
            get
            {
                return InternalWatcher.EnableRaisingEvents;
            }
            set
            {
                InternalWatcher.EnableRaisingEvents = value;
            }
        }

        public virtual String Filter
        {
            get
            {
                return InternalWatcher.Filter;
            }
            set
            {
                InternalWatcher.Filter = value;
            }
        }

        public virtual Boolean IncludeSubdirectories
        {
            get
            {
                return InternalWatcher.IncludeSubdirectories;
            }
            set
            {
                InternalWatcher.IncludeSubdirectories = value;
            }
        }

        public virtual Int32 InternalBufferSize
        {
            get
            {
                return InternalWatcher.InternalBufferSize;
            }
            set
            {
                InternalWatcher.InternalBufferSize = value;
            }
        }

        public virtual NotifyFilters NotifyFilter
        {
            get
            {
                return InternalWatcher.NotifyFilter;
            }
            set
            {
                InternalWatcher.NotifyFilter = value;
            }
        }

        public virtual String Path
        {
            get
            {
                return InternalWatcher.Path;
            }
            set
            {
                InternalWatcher.Path = value;
            }
        }


        protected WatcherWrapper(IWatcher watcher)
        {
            InternalWatcher = watcher;
        }

        protected WatcherWrapper(FileSystemWatcher watcher)
            : this(new WatcherAdapter(watcher))
        {
        }

        protected WatcherWrapper()
            : this(new WatcherAdapter())
        {
        }

        protected WatcherWrapper(String path)
            : this(new WatcherAdapter(path))
        {
        }

        protected WatcherWrapper(String path, String filter)
            : this(new WatcherAdapter(path, filter))
        {
        }


        // Subscribe/Unsubscribe from wrapped watcher's events
        protected virtual void SubscribeToPrivateWatcherEvents()
        {
            if (InternalWatcher is null)
            {
                return;
            }

            InternalWatcher.Created += OnCreated;
            InternalWatcher.Changed += OnChanged;
            InternalWatcher.Deleted += OnDeleted;
            InternalWatcher.Error += OnError;
            InternalWatcher.Renamed += OnRenamed;
        }

        protected virtual void UnsubscribeFromInternalWatcherEvents()
        {
            if (InternalWatcher is null)
            {
                return;
            }

            InternalWatcher.Created -= OnCreated;
            InternalWatcher.Changed -= OnChanged;
            InternalWatcher.Deleted -= OnDeleted;
            InternalWatcher.Error -= OnError;
            InternalWatcher.Renamed -= OnRenamed;
        }

        // Events Invokers
        protected virtual void OnChanged(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Changed?.Invoke(sender, fileSystemEventArgs);
        }

        protected virtual void OnCreated(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Created?.Invoke(sender, fileSystemEventArgs);
        }

        protected virtual void OnDeleted(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Deleted?.Invoke(sender, fileSystemEventArgs);
        }

        protected virtual void OnError(Object sender, ErrorEventArgs fileSystemErrorArgs)
        {
            Error?.Invoke(sender, fileSystemErrorArgs);
        }

        protected virtual void OnRenamed(Object sender, RenamedEventArgs fileSystemEventArgs)
        {
            Renamed?.Invoke(sender, fileSystemEventArgs);
        }

        public void StartWatch()
        {
            EnableRaisingEvents = true;
        }
        
        public void StopWatch()
        {
            EnableRaisingEvents = false;
        }

        public virtual WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
        {
            return InternalWatcher.WaitForChanged(changeType);
        }

        public virtual WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, Int32 timeout)
        {
            return InternalWatcher.WaitForChanged(changeType, timeout);
        }
        
        ~WatcherWrapper()
        {
            Dispose(false);
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            UnsubscribeFromInternalWatcherEvents();

            if (disposing)
            {
                InternalWatcher.Dispose();
            }
        }
        
        public abstract Object Clone();
    }
}