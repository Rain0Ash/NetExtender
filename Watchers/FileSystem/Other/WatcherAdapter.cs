// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NetExtender.Watchers.FileSystem.Interfaces;

 namespace NetExtender.Watchers.FileSystem.Other
{
    /// <summary>
    /// Adapts a FileSystemWatcher to make it fit the IWatcher interface
    /// </summary>
    public class WatcherAdapter : IWatcher
    {
        private FileSystemWatcher _watcher;


        public WatcherAdapter(FileSystemWatcher watcherToWrap)
        {
            _watcher = watcherToWrap;
            SubscribeToPrivateWatcherEvents();
        }

        public WatcherAdapter()
            : this(new FileSystemWatcher())
        {
        }

        public WatcherAdapter(String path)
            : this(new FileSystemWatcher(path))
        {
        }

        public WatcherAdapter(String path, String filter)
            : this(new FileSystemWatcher(path, filter))
        {
        }


        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Deleted;
        public event ErrorEventHandler Error;
        public event RenamedEventHandler Renamed;


        protected FileSystemWatcher InternalWatcher
        {
            get
            {
                return _watcher;
            }
            set
            {
                UnsubscribeFromPrivateWatcherEvents();
                _watcher = value;
                SubscribeToPrivateWatcherEvents();
            }
        }

        public Boolean EnableRaisingEvents
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

        public String Filter
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

        public Boolean IncludeSubdirectories
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

        public Int32 InternalBufferSize
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

        public NotifyFilters NotifyFilter
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

        public String Path
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


        protected void SubscribeToPrivateWatcherEvents()
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

        protected void UnsubscribeFromPrivateWatcherEvents()
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

        protected void OnChanged(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Changed?.Invoke(sender, fileSystemEventArgs);
        }

        protected void OnCreated(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Created?.Invoke(sender, fileSystemEventArgs);
        }

        protected void OnDeleted(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Deleted?.Invoke(sender, fileSystemEventArgs);
        }

        protected void OnError(Object sender, ErrorEventArgs fileSystemErrorArgs)
        {
            Error?.Invoke(sender, fileSystemErrorArgs);
        }

        protected void OnRenamed(Object sender, RenamedEventArgs fileSystemEventArgs)
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

        public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
        {
            return InternalWatcher.WaitForChanged(changeType);
        }

        public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, Int32 timeout)
        {
            return InternalWatcher.WaitForChanged(changeType, timeout);
        }


        ~WatcherAdapter()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            UnsubscribeFromPrivateWatcherEvents();

            if (disposing)
            {
                _watcher.Dispose();
            }
        }


        public Object Clone()
        {
            FileSystemWatcher clonedEncapsWatcher = new FileSystemWatcher
            {
                NotifyFilter = InternalWatcher.NotifyFilter,
                Path = InternalWatcher.Path,
                IncludeSubdirectories = InternalWatcher.IncludeSubdirectories,
                InternalBufferSize = InternalWatcher.InternalBufferSize,
                Filter = InternalWatcher.Filter,
                EnableRaisingEvents = InternalWatcher.EnableRaisingEvents
            };
            return new WatcherAdapter(clonedEncapsWatcher);
        }
    }
}