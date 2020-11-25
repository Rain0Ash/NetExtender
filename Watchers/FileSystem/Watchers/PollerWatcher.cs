﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

 using System;
 using System.Collections.Generic;
 using System.Collections.ObjectModel;
 using System.ComponentModel;
 using System.Diagnostics;
 using System.IO;
 using System.Text.RegularExpressions;
 using System.Threading;
 using System.Threading.Tasks;
 using NetExtender.Utils.IO;
 using NetExtender.Watchers.FileSystem.Interfaces;

 namespace NetExtender.Watchers.FileSystem
{
    /// <summary>
    /// Defines how PollerWatcher reports back to the listeners
    /// </summary>
    public enum PollingType
    {
        /// <summary>
        /// Watcher-like behivour. Reports NEWLY created/deleted files.
        /// </summary>
        Watch,
        /// <summary>
        /// PollerWatcher-like behivour. Reports ALL existing files in the directory in EVERY poll.
        /// </summary>
        Poll
    }
    
    [DebuggerDisplay("Path = {_path}, Filter = {_filter}, Polling = {_pollingTask is not null}, EnableRaisingEvents = {_enableRaisingEvents}")]
    public class PollerWatcher : IWatcher, IPoller
    {
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Deleted;
        public event ErrorEventHandler Error;

        public event FileSystemEventHandler Changed
        {
            // ReSharper disable once ValueParameterNotUsed
            add
            {
                if (_supressNotSuppotedErrors)
                {
                    return;
                }

                throw new NotSupportedException(
                    "Changed events are not supported by PollerWatcher. If you are trying to wrap the poller use poller.SupressNotSupportedErrors = true to stop this exception from being thrown.");
            }
            remove
            {
            }
        }

        public event RenamedEventHandler Renamed
        {
            // ReSharper disable once ValueParameterNotUsed
            add
            {
                if (_supressNotSuppotedErrors)
                {
                    return;
                }

                throw new NotSupportedException(
                    "Renamed events are not supported by PollerWatcher. If you are trying to wrap the poller use poller.SupressNotSupportedErrors = true to stop this exception from being thrown.");
            }
            remove
            {
            }
        }

        // classic watcher properties' backing fields
        /// <summary>
        /// Indicates whether the poller should raise it's events when a new notification is found
        /// </summary>
        private Boolean _enableRaisingEvents;

        /// <summary>
        /// Indicates what path of the directory the poller should poll from
        /// </summary>
        private String _path;

        /// <summary>
        /// An uppercase version of _path. used for string comparisons (prevents multiple ToUpper calls)
        /// </summary>
        private String _uppercasePath;

        /// <summary>
        /// Indicates whether the poller should supress it's NotSupported exceptions when subscribing to Changed/Renamed events.
        /// </summary>
        private Boolean _supressNotSuppotedErrors;

        /// <summary>
        /// Indicates whether the poller should poll subdirectories or not.
        /// </summary>
        private Boolean _includeSubdirectories;


        // basic file watching fields

        /// <summary>
        /// A FileSystemWatcher-like filter for the poller to use
        /// </summary>
        private String _filter;

        /// <summary>
        /// A regex expression created according to the _filter and used to check polled files.
        /// </summary>
        private Regex _regexFilter;

        /// <summary>
        /// Used by the polling thread to signal it has finished the initial polling.
        /// </summary>
        private readonly ManualResetEvent _initialFilesSeen;

        /// <summary>
        /// Collection of files seen in the last poll
        /// </summary>
        private IEnumerable<String> _lastSeenFiles;

        /// <summary>
        /// Collection of directories seen in the last poll
        /// </summary>
        private IEnumerable<String> _lastSeenDirs;


        // Polling related fields

        /// <summary>
        /// The task responsible for polling.
        /// </summary>
        private Task _pollingTask;

        /// <summary>
        /// Makes sure only a single thread starts/stops the polling task
        /// </summary>
        private readonly Object _pollingTaskLock;

        /// <summary>
        /// Used by the polling thread to wait the timeout between polls. If set the thread stops waiting and continues.
        /// </summary>
        private readonly AutoResetEvent _pollingTimeoutEvent;

        /// <summary>
        /// Used to signal to the polling thread that it should stop execution
        /// </summary>
        private readonly ManualResetEventSlim _pollingEnabledEvent;

        /// <summary>
        /// Used by the polling thread to signal a poll was done sucessfully. The event is set afte EVERY poll.
        /// </summary>
        private readonly ManualResetEventSlim _pollDone;


        // WaitForChange fields

        /// <summary>
        /// Contains the number of threads waiting for notifications using the WaitForChanged methods
        /// </summary>
        private Int32 _waiters;

        /// <summary>
        /// Used by the polling thread to signal to the waiters that a new notification is available.
        /// </summary>
        private readonly AutoResetEvent _changesWatchingEvent;

        /// <summary>
        /// Latest notification available for the WaitForChanged waiters
        /// </summary>
        private WaitForChangedResult _latestChange;

        /// <summary>
        /// Used to assert only a single thread (waiter/poller) access the _latestChange field at a time.
        /// </summary>
        private readonly Object _latestChangeLocker;


        /// <summary>
        /// Defines whether the poller acts as a 'Watcher' or as a clasic 'PollerWatcher'.
        /// </summary>
        public PollingType PollingType { get; set; }

        /// <summary>
        /// Path of the directory to monitor
        /// </summary>
        public String Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (value.Length == 0)
                {
                    throw new ArgumentException(@"Path cannot be an empty string.", nameof(value));
                }

                StopPollingTask();

                // Add the directory seperator character to the value if it's missing
                if (value[^1] != System.IO.Path.DirectorySeparatorChar)
                {
                    value += System.IO.Path.DirectorySeparatorChar;
                }

                _path = value;
                _uppercasePath = _path.ToUpperInvariant();

                StartPollingTask();
            }
        }

        /// <summary>
        /// Whether the poller should raise Created/Deleted/Error events
        /// </summary>
        public Boolean EnableRaisingEvents
        {
            get
            {
                return _enableRaisingEvents;
            }
            set
            {
                if (String.IsNullOrEmpty(_path))
                {
                    throw new InvalidOperationException("No directory path was provided to the poller. Can not poll.");
                }

                if (!Directory.Exists(_path))
                {
                    throw new InvalidOperationException("Directory path to poll does not exist. Path: " + _path);
                }

                if (value) // settings raising to true
                {
                    _initialFilesSeen.WaitOne(); // waiting for intialization to end
                }

                _enableRaisingEvents = value;
            }
        }

        /// <summary>
        /// A file name filter to monitor the directory with. Files/Directories which does not pass the filter won't be reported.
        /// </summary>
        public String Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;

                _regexFilter = value == String.Empty ? new Regex(".*") : new Regex(Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*"));
            }
        }

        /// <summary>
        /// Whether the poller should also poll in subdirectories of the directory at Path
        /// </summary>
        public Boolean IncludeSubdirectories
        {
            get
            {
                return _includeSubdirectories;
            }
            set
            {
                StopPollingTask();

                _includeSubdirectories = value;

                StartPollingTask();
            }
        }

        public Int32 InternalBufferSize { get; set; }
        public NotifyFilters NotifyFilter { get; set; }

        /// <summary>
        /// The interval to poll at.
        /// </summary>
        public TimeSpan Polling { get; set; }

        /// <summary>
        /// Prevents the "NotImplementedException" from being thrown when subscribing to the Renamed/Changed events.
        /// Set this to true when trying to wrap the PollerWatcher. The events will still not invoke but will allow subscription.
        /// </summary>
        [Browsable(false)]
        public Boolean SupressNotSupportedErrors
        {
            get
            {
                return _supressNotSuppotedErrors;
            }
            set
            {
                _supressNotSuppotedErrors = value;
            }
        }

        /// <summary>
        /// Whether any threads are currently waiting in one of the WaitForChanged overloads
        /// </summary>
        private Boolean ReportsExpected
        {
            get
            {
                return Volatile.Read(ref _waiters) != 0 || EnableRaisingEvents;
            }
        }


        public PollerWatcher(TimeSpan polling)
        {
            _path = String.Empty;

            _initialFilesSeen = new ManualResetEvent(false);
            _lastSeenFiles = new HashSet<String>();
            _lastSeenDirs = new HashSet<String>();

            Filter = String.Empty;

            _waiters = 0;
            _changesWatchingEvent = new AutoResetEvent(false);
            _latestChangeLocker = new Object();
            _latestChange = new WaitForChangedResult();

            _pollingTaskLock = new Object();
            Polling = polling;
            _pollDone = new ManualResetEventSlim(false);
            _pollingTimeoutEvent = new AutoResetEvent(false);
            _pollingEnabledEvent = new ManualResetEventSlim(true);

            PollingType = PollingType.Watch;
        }

        public PollerWatcher(TimeSpan polling, String path)
            : this(polling)
        {
            Path = path;
            StartPollingTask();
        }

        public PollerWatcher(TimeSpan polling, String path, String filter)
            : this(polling, path)
        {
            Filter = filter;
            StartPollingTask();
        }

        private void StopPollingTask()
        {
            // Check if a polling task even exist
            if (_pollingTask is null)
            {
                return;
            }

            lock (_pollingTaskLock)
            {
                _initialFilesSeen.Set();
                _pollingEnabledEvent.Reset(); // Signaling for the task to quit
                _pollingTimeoutEvent.Set(); // Trying to speed up the task exit by interupting the 'sleep' period
                if (_pollingTask.Status == TaskStatus.Running)
                {
                    // ReSharper disable once AsyncConverter.AsyncWait
                    _pollingTask.Wait();
                }

                _pollingTask = null;
            }
        }

        private void StartPollingTask()
        {
            // Check if a no other polling task exists
            if (_pollingTask is not null)
            {
                return;
            }

            lock (_pollingTaskLock)
            {
                if (_pollingTask is not null)
                {
                    return;
                }

                _initialFilesSeen.Reset();
                _pollingTimeoutEvent.Reset();
                _pollingEnabledEvent.Set();
                _lastSeenFiles = new Collection<String>();
                _lastSeenDirs = new Collection<String>();
                _pollingTask = Task.Factory.StartNew(Poll, TaskCreationOptions.LongRunning);
            }
        }

        public void StartWatch()
        {
            EnableRaisingEvents = true;
        }
        
        public void StopWatch()
        {
            EnableRaisingEvents = false;
        }

        /// <summary>
        /// Polls the files currently in the directory
        /// </summary>
        private void PollInitialDirContent()
        {
            // Get initial content
            while (!_initialFilesSeen.WaitOne(1))
            {
                // Check if polling was disabled
                if (!_pollingEnabledEvent.Wait(1))
                {
                    return;
                }

                // Query files in folder
                if (PollCurrentFiles(out IEnumerable<String> currentFiles) && PollCurrentSubDirs(out IEnumerable<String> currentFolders))
                {
                    _lastSeenFiles = currentFiles;
                    _lastSeenDirs = currentFolders;
                    _initialFilesSeen.Set();
                    return;
                }

                // Check if polling was disabled
                if (!_pollingEnabledEvent.Wait(1))
                {
                    return;
                }

                // Sleep
                _pollingTimeoutEvent.WaitOne(Polling);
            }
        }

        /// <summary>
        /// Constantly polls the files in the path given. 
        /// </summary>
        private void Poll()
        {
            // Firstly, get an idea of what the folder currently looks like.
            PollInitialDirContent();


            while (true)
            {
                // Check if polling was disabled
                if (!_pollingEnabledEvent.Wait(1))
                {
                    break;
                }

                // Sleep
                _pollingTimeoutEvent.WaitOne(Polling);

                // Check if polling was disabled while waiting the timeout (which might be long)
                if (!_pollingEnabledEvent.Wait(1))
                {
                    break;
                }

                // Poll both files and directories in watched folder
                if (!PollCurrentFiles(out IEnumerable<String> currentFiles) || !PollCurrentSubDirs(out IEnumerable<String> currentFolders))
                {
                    // Polling files or folders failed, continuing to next sleep
                    continue;
                }

                ProcessPolledItems(currentFiles, currentFolders);

                // Inform any 'ForcePoll' threads that the poll finished
                _pollDone.Set();
            }
        }

        /// <summary>
        /// Proccess collections of files and folders currently polled and runs checks on them according to the polling type
        /// </summary>
        /// <param name="currentFiles">Files that currently exist under the polled folder</param>
        /// <param name="currentFolders">Folders that currently exist under the polled folder</param>
        private void ProcessPolledItems(IEnumerable<String> currentFiles, IEnumerable<String> currentFolders)
        {
            // Orginazing possible check to run each poll
            List<Action> actionsOnItems;
            if (PollingType == PollingType.Watch)
            {
                actionsOnItems = new List<Action>
                {
                    () => ReportCreatedItems(_lastSeenFiles, currentFiles), // Check for new files
                    () => ReportCreatedItems(_lastSeenDirs, currentFolders), // Check for new folders
                    () => ReportDeletedItems(_lastSeenFiles, currentFiles), // Check for deleted files
                    () => ReportDeletedItems(_lastSeenDirs, currentFolders) // Check for deleted folders
                };
            }
            else // PollingType == PollingType.Poll
            {
                actionsOnItems = new List<Action>
                {
                    () => ReportItems(currentFiles, WatcherChangeTypes.Created), // Report current files that match the filter
                    () => ReportItems(currentFolders, WatcherChangeTypes.Created), // Report current directories that match the filter
                };
            }

            // For each one of the checks above, see if there is a point even running this check (EnableRaisingEvents is true or threads are WaitingForChange-s).
            foreach (Action itemsCheck in actionsOnItems)
            {
                if (ReportsExpected)
                {
                    itemsCheck();
                }
            }

            // Update "last seen files" and "last seen folders"
            _lastSeenFiles = currentFiles;
            _lastSeenDirs = currentFolders;
        }

        /// <summary>
        /// Forces the PollerWatcher to poll for files immediatly.
        /// </summary>
        public Task ForcePollAsync(Boolean returnWhenPolled = false)
        {
            return Task.Factory.StartNew(() => ForcePoll(returnWhenPolled));
        }

        /// <summary>
        /// Forces the PollerWatcher to poll for files immediatly.
        /// </summary>
        public void ForcePoll()
        {
            ForcePoll(true);
        }

        /// <summary>
        /// Forces the PollerWatcher to poll for files immediatly.
        /// </summary>
        public void ForcePoll(Boolean returnWhenPolled)
        {
            _pollingTimeoutEvent.Set();

            if (!returnWhenPolled)
            {
                return;
            }

            _pollDone.Reset();
            _pollDone.Wait();
        }

        /// <summary>
        /// Polls a collection of file names in the watched folder
        /// </summary>
        /// <param name="currentFiles">Output variable for the files' names</param>
        /// <returns>True if polling succeeded, false otherwise</returns>
        private Boolean PollCurrentFiles(out IEnumerable<String> currentFiles)
        {
            currentFiles = null;
            try
            {
                currentFiles = DirectoryUtils.GetFiles(Path, IncludeSubdirectories);

                return true;
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
                return false;
            }
        }

        /// <summary>
        /// Polls a collection of directories names in the watched folder
        /// </summary>
        /// <param name="currentFolders">Output variable for the directories' names</param>
        /// <returns>True if polling succeeded, false otherwise</returns>
        private Boolean PollCurrentSubDirs(out IEnumerable<String> currentFolders)
        {
            currentFolders = null;
            try
            {
                currentFolders = DirectoryUtils.GetDirectories(Path, IncludeSubdirectories);

                return true;
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
                return false;
            }
        }


        /// <summary>
        /// Compares an old and a new collection of files to see if any old items were removed. Pops the "Deleted" event for each of those items.
        /// </summary>
        /// <param name="originalItems">Set of old items</param>
        /// <param name="currentItems">Set of new items</param>
        private void ReportDeletedItems(IEnumerable<String> originalItems, IEnumerable<String> currentItems)
        {
            // Copy last known items to a new set
            ISet<String> deletedFiles = new HashSet<String>(originalItems);
            // Substract current items
            deletedFiles.ExceptWith(currentItems);

            // Runs the items through the filter and reports matching ones
            ReportItems(deletedFiles, WatcherChangeTypes.Deleted);
        }

        /// <summary>
        /// Compares an old and a new collection of files to see if any new items were added. Pops the "Created" event for each of those items.
        /// </summary>
        /// <param name="originalItems">Set of old items</param>
        /// <param name="currentItems">Set of new items</param>
        private void ReportCreatedItems(IEnumerable<String> originalItems, IEnumerable<String> currentItems)
        {
            // Copy current found items to a new set
            ISet<String> addedItems = new HashSet<String>(currentItems);
            // Substract last seen items
            addedItems.ExceptWith(originalItems);

            // Runs the items through the filter and reports matching ones
            ReportItems(addedItems, WatcherChangeTypes.Created);
        }

        /// <summary>
        /// Checks an enumerable of items with the current filter and reports those who fit.
        /// </summary>
        /// <param name="items">The collection of items (files/folders) to check</param>
        /// <param name="reportType">The type of report to create for those items</param>
        private void ReportItems(IEnumerable<String> items, WatcherChangeTypes reportType)
        {
            foreach (String item in items)
            {
                String itemName = System.IO.Path.GetFileName(item);
                if (!PassesFilter(itemName))
                {
                    continue;
                }

                String folder = System.IO.Path.GetDirectoryName(item) ?? String.Empty;

                SignalFileChangeForWaiters(reportType, item);

                if (EnableRaisingEvents)
                {
                    FileSystemEventArgs reportArgs = new FileSystemEventArgs(reportType, folder, itemName);
                    switch (reportType)
                    {
                        case WatcherChangeTypes.Created:
                            OnCreated(reportArgs);
                            break;
                        case WatcherChangeTypes.Deleted:
                            OnDeleted(reportArgs);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a single file name/folder name matches the currently set filter
        /// </summary>
        /// <param name="item">File/Folder name</param>
        /// <returns>True if the name matches the filter, false otherwise.</returns>
        private Boolean PassesFilter(String item)
        {
            // returns whether *the string is not empty* && *the string matches filter*
            return !String.IsNullOrEmpty(item) && _regexFilter.IsMatch(item);
        }


        private void OnCreated(FileSystemEventArgs fileSystemEventArgs)
        {
            Created?.Invoke(this, fileSystemEventArgs);
        }

        private void OnDeleted(FileSystemEventArgs fileSystemEventArgs)
        {
            Deleted?.Invoke(this, fileSystemEventArgs);
        }

        private void OnError(ErrorEventArgs errorEventArgs)
        {
            Error?.Invoke(this, errorEventArgs);
        }


        private void SignalFileChangeForWaiters(WatcherChangeTypes type, String filePath)
        {
            if (_waiters == 0)
            {
                return; // No point signaling if no one is waiting
            }

            // Getting the 'relative path' of the filePath compared to the currently monitored folder path
            String uppercaseFilePath = filePath.ToUpperInvariant();
            String fileNameToReport = uppercaseFilePath.Replace(_uppercasePath, String.Empty);

            lock (_latestChangeLocker)
            {
                _latestChange = new WaitForChangedResult {ChangeType = type, Name = fileNameToReport};
            }

            _changesWatchingEvent.Set();
        }

        public WaitForChangedResult WaitForChanged(WatcherChangeTypes type)
        {
            if (type == WatcherChangeTypes.Renamed ||
                type == WatcherChangeTypes.Changed ||
                type == (WatcherChangeTypes.Changed | WatcherChangeTypes.Renamed)) // Polling cannot monitor these changes
            {
                throw new NotSupportedException("File System PollerWatcher can not monitor \"Rename\" or \"Changed\" file changes.");
            }

            while (true)
            {
                Interlocked.Increment(ref _waiters);
                _changesWatchingEvent.WaitOne();
                Interlocked.Decrement(ref _waiters);

                WaitForChangedResult results;
                lock (_latestChangeLocker)
                {
                    results = _latestChange;
                }

                // Check if the report fits the one the current thread is looking for
                if (type.HasFlag(results.ChangeType))
                {
                    // It does, returning the report.
                    return results;
                }

                // It doesn't.
                // allowing a signle other thread to examine it this report:
                _changesWatchingEvent.Set();
                // making sure the event is reset when the current thread returns to it. (If a thread is waiting it will exit after the .Set and before the .Reset)
                _changesWatchingEvent.Reset();
            }
        }

        public WaitForChangedResult WaitForChanged(WatcherChangeTypes type, Int32 timeout)
        {
            if (type == WatcherChangeTypes.Renamed ||
                type == WatcherChangeTypes.Changed ||
                type == (WatcherChangeTypes.Changed | WatcherChangeTypes.Renamed)) // Polling cannot monitor these changes
            {
                throw new NotSupportedException("File System PollerWatcher can not monitor \"Rename\" or \"Changed\" item changes.");
            }

            // Using this stopwatch to check I'm staying in the method longer then the timeout set
            Stopwatch timeInMethodStopwatch = Stopwatch.StartNew();

            Interlocked.Increment(ref _waiters);
            while (true)
            {
                Int32 remainingTimeToWait = timeout - (Int32) timeInMethodStopwatch.ElapsedMilliseconds;
                Boolean timedOut = !_changesWatchingEvent.WaitOne(remainingTimeToWait);

                if (timedOut) // wait timed out, exit method.
                {
                    Interlocked.Decrement(ref _waiters);
                    return new WaitForChangedResult {ChangeType = type, TimedOut = true};
                }

                // wait didn't time out - check results
                WaitForChangedResult results;
                lock (_latestChangeLocker)
                {
                    results = _latestChange;
                }

                // Check if the reported results match the requestsed result type.
                // Otherwise - continue waiting for more changes
                if (!type.HasFlag(results.ChangeType))
                {
                    continue;
                }

                Interlocked.Decrement(ref _waiters);
                return results;
            }
        }


        public void Dispose()
        {
            // Canceling polling task
            StopPollingTask();

            // Empty files/folders collections - those might get quite large
            _lastSeenFiles = new Collection<String>();
            _lastSeenDirs = new Collection<String>();
        }


        public Object Clone()
        {
            PollerWatcher clonedPollerWatcher = new PollerWatcher(Polling, Path, Filter)
            {
                IncludeSubdirectories = IncludeSubdirectories,
                EnableRaisingEvents = EnableRaisingEvents,
                PollingType = PollingType
            };

            return clonedPollerWatcher;
        }
    }
}