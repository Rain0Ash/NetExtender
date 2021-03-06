// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using NetExtender.Utils.IO;
using NetExtender.Watchers.FileSystem.Interfaces;
using NetExtender.Watchers.FileSystem.Other;

 namespace NetExtender.Watchers.FileSystem
{
    [DebuggerDisplay("Path = {Path}, Filter = {Filter}, EnableRaisingEvents = {_enableRaisingEvents}")]
    public class OverseerWatcher : AutoRefreshingWatcher
    {
        private readonly IPoller _poller;

        private Boolean _enableRaisingEvents;

        private readonly Object _reportedFilesLock;
        private readonly HashSet<String> _reportedItems;

        /// <summary>
        /// Defines a delay (in milliseconds) between processing poller reports.
        /// The main reason to delay such reports is to allow the more descriptive reports of the watcher to be processed.
        /// </summary>
        [Browsable(false)]
        public Int32 PollerReportsDelay { get; set; } = 100;

        public override Boolean EnableRaisingEvents
        {
            get
            {
                return _enableRaisingEvents;
            }
            set
            {
                _enableRaisingEvents = value;
                InternalWatcher.EnableRaisingEvents = value;
                _poller.EnableRaisingEvents = value;
            }
        }

        public override String Filter
        {
            get
            {
                return InternalWatcher.Filter;
            }
            set
            {
                InternalWatcher.Filter = value;
                _poller.Filter = value;
            }
        }

        public override Boolean IncludeSubdirectories
        {
            get
            {
                return InternalWatcher.IncludeSubdirectories;
            }
            set
            {
                Boolean lastEREvalue = _enableRaisingEvents;
                _enableRaisingEvents = false;

                InternalWatcher.IncludeSubdirectories = value;
                _poller.IncludeSubdirectories = value;
                _poller.ForcePoll();

                _enableRaisingEvents = lastEREvalue;
            }
        }

        public override Int32 InternalBufferSize
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

        public override NotifyFilters NotifyFilter
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

        public override String Path
        {
            get
            {
                return InternalWatcher.Path;
            }
            set
            {
                Boolean lastEREvalue = _enableRaisingEvents;
                _enableRaisingEvents = false;

                InternalWatcher.Path = value;
                _poller.Path = value;

                _enableRaisingEvents = lastEREvalue;
            }
        }


        public OverseerWatcher(IPoller poller, IWatcher watcher)
            : base(watcher)
        {
            _reportedItems = new HashSet<String>();
            _reportedFilesLock = new Object();

            InitPollerErrorPolicies();

            // Initiating poller
            _poller = poller;
            if (_poller.Path != watcher.Path)
            {
                _poller.Path = watcher.Path;
            }

            EnableRaisingEvents = false;

            _poller.Created += OnCreatedPolled;
            _poller.Deleted += OnDeletedPolled;
            _poller.Error += OnPollerError;

            // Getting initial directory content by forcing a poll
            _poller.PollingType = PollingType.Poll;
            _poller.ForcePoll();

            // For the rest of the OverseerWatcher's lifespan, keep the poller as a 'watcher'
            _poller.PollingType = PollingType.Watch;
        }

        public OverseerWatcher(IPoller poller, FileSystemWatcher watcher)
            : this(poller, new WatcherAdapter(watcher))
        {
        }

        public OverseerWatcher(IPoller poller)
            : this(poller, new WatcherAdapter(poller.Path, poller.Filter))
        {
        }

        public OverseerWatcher(TimeSpan polling)
            : this(new PollerWatcher(polling), new FileSystemWatcher())
        {
        }

        public OverseerWatcher(TimeSpan polling, String path)
            : this(new PollerWatcher(polling, path), new FileSystemWatcher(path))
        {
        }

        public OverseerWatcher(TimeSpan polling, String path, String filter)
            : this(new PollerWatcher(polling, path, filter), new FileSystemWatcher(path, filter))
        {
        }

        private void InitPollerErrorPolicies()
        {
            WatcherErrorHandlingPolicy dirNotFoundPolicy = new WatcherErrorHandlingPolicy(typeof(DirectoryNotFoundException),
                "When the poller indicates a 'directory not found' exception check if it's the main watched directory or sub-dir." +
                "If it's the main directory - refresh the watcher.",
                exception => (exception as DirectoryNotFoundException)?.GetPath() == Path
                    ? WatcherErrorHandlingType.Refresh | WatcherErrorHandlingType.Swallow
                    : WatcherErrorHandlingType.Forward);

            WatcherErrorHandlingPolicy unAuthPolicy = new WatcherErrorHandlingPolicy(typeof(UnauthorizedAccessException),
                "When the poller indicates an 'unauthorized access' exception check if it's access was denied to the main watched directory or file/sub-dir." +
                "If it's the main directory - refresh the watcher.",
                exception => (exception as UnauthorizedAccessException)?.GetPath() == Path
                    ? WatcherErrorHandlingType.Refresh | WatcherErrorHandlingType.Swallow
                    : WatcherErrorHandlingType.Forward);

            AddPolicy(dirNotFoundPolicy);
            AddPolicy(unAuthPolicy);
        }


        // Event handlers for the wrapped watcher and the poller (a delay)
        protected override void OnCreated(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            lock (_reportedFilesLock)
            {
                // If the files was already reported - return 
                if (_reportedItems.Contains(fileSystemEventArgs.FullPath))
                {
                    return;
                }

                // Other wise:
                // 1. Add to reported files set
                _reportedItems.Add(fileSystemEventArgs.FullPath);
            }

            // 2. report to subscribers
            if (!_enableRaisingEvents)
            {
                return;
            }

            base.OnCreated(sender, fileSystemEventArgs);
        }

        protected override void OnDeleted(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            lock (_reportedFilesLock)
            {
                // If the files was already reported - return 
                if (!_reportedItems.Contains(fileSystemEventArgs.FullPath))
                {
                    return;
                }


                // Other wise:
                // 1. Try to remove said file. If the removal fails - return
                if (!_reportedItems.Remove(fileSystemEventArgs.FullPath))
                {
                    return;
                }
            }

            // 2. report to subscribers
            if (!_enableRaisingEvents)
            {
                return;
            }

            base.OnDeleted(sender, fileSystemEventArgs);
        }

        protected override void OnChanged(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            if (!_enableRaisingEvents)
            {
                return;
            }

            base.OnChanged(sender, fileSystemEventArgs);
        }

        protected override void OnRenamed(Object sender, RenamedEventArgs fileSystemEventArgs)
        {
            lock (_reportedFilesLock)
            {
                // If a file with the new name was already reported - return 
                if (_reportedItems.Contains(fileSystemEventArgs.FullPath))
                {
                    return;
                }

                // 1. If the file's old name existed in the storage - remove it
                if (_reportedItems.Contains(fileSystemEventArgs.OldFullPath))
                {
                    _reportedItems.Remove(fileSystemEventArgs.OldFullPath);
                }

                // 2. Add new path to the reportedFiles list
                _reportedItems.Add(fileSystemEventArgs.FullPath);
            }

            // 3. report to subscribers
            if (!_enableRaisingEvents)
            {
                return;
            }

            base.OnRenamed(sender, fileSystemEventArgs);
        }

        protected override void OnError(Object sender, ErrorEventArgs e)
        {
            Exception ex = e.GetException();
            if (ex is InternalBufferOverflowException)
            {
                _poller.ForcePoll();
            }

            base.OnError(sender, e);
        }

        // Events raised by the poller will invoke these methods first:
        private void OnCreatedPolled(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Task.Delay(PollerReportsDelay).ContinueWith(task => OnCreated(sender, fileSystemEventArgs));
        }

        private void OnDeletedPolled(Object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Task.Delay(PollerReportsDelay).ContinueWith(task => OnDeleted(sender, fileSystemEventArgs));
        }

        private void OnPollerError(Object sender, ErrorEventArgs e)
        {
            base.OnError(sender, e);
        }


        ~OverseerWatcher()
        {
            Dispose(false);
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            if (_poller is not null)
            {
                _poller.Created -= OnCreatedPolled;
                _poller.Deleted -= OnDeletedPolled;
                _poller.Error -= OnPollerError;
                _poller.Dispose();
            }
            
            if (disposing)
            {
                base.Dispose();
            }
        }


        public override Object Clone()
        {
            IPoller clonedPoller = (IPoller) _poller.Clone();

            IWatcher clonedEncapsWatcher = (IWatcher) InternalWatcher.Clone();

            OverseerWatcher clonedOverseerWatcher = new OverseerWatcher(clonedPoller, clonedEncapsWatcher)
                {PollerReportsDelay = PollerReportsDelay};

            clonedOverseerWatcher.ClearPolicies();
            foreach (WatcherErrorHandlingPolicy policy in ErrorHandlingPolicies)
            {
                clonedOverseerWatcher.AddPolicy(policy);
            }

            return clonedOverseerWatcher;
        }
    }
}