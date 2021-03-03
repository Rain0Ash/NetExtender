// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Watchers.FileSystem.Interfaces;
using NetExtender.Watchers.FileSystem.Other;
using Polly;

namespace NetExtender.Watchers.FileSystem
{
    /// <summary>
    /// An IWatcher wrapper which allows refreshing the watcher for when it ceases to work due to a problem.
    /// </summary>
    public class RefreshableWatcher : WatcherWrapper
    {
        /// <summary>
        /// A collection of ManualResetEvents of the threads currently waiting for a refresh to finish
        /// </summary>
        private readonly ConcurrentDictionary<Thread, ManualResetEventSlim> _waitingThreadsEvents;

        /// <summary>
        /// Used to synchronize between different threads that try to refresh the watcher at the same time 
        /// Only the one who successfully entered this object is allowed to refresh.
        /// </summary>
        private readonly Object _refreshLock;

        private readonly CancellationTokenSource _refreshTokenSource;


        /// <summary>
        /// The amount of time in milliseconds to wait between refresh attemps on the watcher.
        /// </summary>
        [Browsable(false)]
        public Int32 RefreshAttempInterval { get; set; } = 500;

        /// <summary>
        /// Wether the watcher is currently refreshing or not.
        /// </summary>
        public Boolean IsRefreshing { get; private set; }


        public event EventHandler Refreshed;


        public RefreshableWatcher(IWatcher watcher)
            : base(watcher)
        {
            _refreshTokenSource = new CancellationTokenSource();
            _waitingThreadsEvents = new ConcurrentDictionary<Thread, ManualResetEventSlim>();
            IsRefreshing = false;
            _refreshLock = new Object();
        }

        public RefreshableWatcher(FileSystemWatcher watcher)
            : this(new WatcherAdapter(watcher))
        {
        }

        public RefreshableWatcher()
            : this(new FileSystemWatcher())
        {
        }

        public RefreshableWatcher(String path)
            : this(new FileSystemWatcher(path))
        {
        }

        public RefreshableWatcher(String path, String filter)
            : this(new FileSystemWatcher(path, filter))
        {
        }


        /// <summary>
        /// Refreshes the internal FileSystemWatcher asynchronously
        /// </summary>
        /// <returns></returns>
        public Task RefreshAsync(Boolean returnWhenRefreshed = true)
        {
            return Task.Factory.StartNew(() => Refresh(returnWhenRefreshed));
        }

        /// <summary>
        /// Refreshes the internal FileSystemWatcher
        /// </summary>
        public void Refresh()
        {
            // when using this synchronous method, the call should make sure to return only when the watcher has been refreshed.
            Refresh(true);
        }

        /// <summary>
        /// Refreshes the internal FileSystemWatcher
        /// </summary>
        /// <param name="returnWhenRefreshed">In case another thread is alreayd refreshing, determines wether the thread should return before the refreshing thread finishes or not.</param>
        private void Refresh(Boolean returnWhenRefreshed)
        {
            // Making sure another thread isn't already refreshing:
            if (!Monitor.TryEnter(_refreshLock))
            {
                // if another thread IS already refreshing - wait for it to finish then return
                if (returnWhenRefreshed)
                {
                    WaitForRefresh();
                }

                return;
            }

            IsRefreshing = true;

            // 1. unsubscribe from old watcher's events.
            UnsubscribeFromInternalWatcherEvents();

            // 2a. Keeping the current internal "EnableRaisingEvents" value
            Boolean currentEnableRaisingEvents = InternalWatcher.EnableRaisingEvents;
            // 2b. Turning off EnableRaisingEvents to avoid "locking" the watched folder
            InternalWatcher.EnableRaisingEvents = false;

            // 3. Get a new watcher
            IWatcher newInternalWatcher = GetReplacementWatcher();
            newInternalWatcher.EnableRaisingEvents = currentEnableRaisingEvents;

            // 4. Disposing of the old watcher
            InternalWatcher.Dispose();

            // 5. Place new watcher in the Internal watcher property
            //    This also registers to the watcher's events
            InternalWatcher = newInternalWatcher;

            // Change state back to "not refreshing"
            IsRefreshing = false;
            // Notify any waiting threads that the refresh is done
            foreach (ManualResetEventSlim waitingThreadEvent in _waitingThreadsEvents.Values)
            {
                waitingThreadEvent.Set();
            }

            _waitingThreadsEvents.Clear();
            Monitor.Exit(_refreshLock);

            // Notify listeners about the refresh.
            Refreshed?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Gets a replacement for the InternalWatcher
        /// </summary>
        /// <returns>A new IWatcher of the same type as the InternalWatcher</returns>
        private IWatcher GetReplacementWatcher()
        {
            IWatcher newInternalWatcher = null;
            // Swallowing any exceptions that might occure when trying to get a clone of the current watcher
            CancellationToken cToken = _refreshTokenSource.Token;

            Policy.Handle<Exception>()
                .RetryForever((Exception ex, Int32 con) => Thread.Sleep(RefreshAttempInterval))
                .Execute(() =>
                {
                    // If the refreshment is cancelled, place a fake as the new watcher and return.
                    if (cToken.IsCancellationRequested)
                    {
                        newInternalWatcher = new FakeWatcher();
                        return; //Exits polly's 'Execute' method.
                    }

                    newInternalWatcher = (IWatcher) InternalWatcher.Clone();
                    // setting EnableRaisingEvents to true is where exceptions may raise so 
                    // I'm giving this clone a "test drive" before returning it to the Refresh method
                    newInternalWatcher.EnableRaisingEvents = true;
                    newInternalWatcher.EnableRaisingEvents = false;
                });

            return newInternalWatcher;
        }

        /// <summary>
        /// Blocks the thread while a refresh is in progress
        /// </summary>
        public void WaitForRefresh()
        {
            // Create a reset event and adds it to the waiting threads events list
            ManualResetEventSlim refreshEvent = new ManualResetEventSlim(false);
            _waitingThreadsEvents[Thread.CurrentThread] = refreshEvent;
            refreshEvent.Wait();
        }

        /// <summary>
        /// Blocks the thread while a refresh is in progress
        /// </summary>
        /// <param name="timeout">Maximum amount of time, in ms, to wait for the refresh to finish.</param>
        /// <returns>True if the refresh finished in time, false if the wait timed out.</returns>
        public Boolean WaitForRefresh(Int32 timeout)
        {
            // Create a reset event and adds it to the waiting threads events list
            ManualResetEventSlim refreshEvent = new ManualResetEventSlim(false);
            _waitingThreadsEvents[Thread.CurrentThread] = refreshEvent;

            Boolean refreshed = refreshEvent.Wait(timeout); // waiting for the refresh

            if (!refreshed) // = wait timed out
            {
                // remove the event from the list
                _waitingThreadsEvents.TryRemove(Thread.CurrentThread, out refreshEvent);
            }

            return refreshed;
        }


        ~RefreshableWatcher()
        {
            Dispose(false);
        }

        public override void Dispose()
        {
            base.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            // If the refresher is currently refreshing, cancel the refresh and wait for the refreshing thread to exit
            if (IsRefreshing)
            {
                _refreshTokenSource.Cancel();
                WaitForRefresh();
            }

            if (disposing)
            {
                base.Dispose();
            }
        }


        public override Object Clone()
        {
            IWatcher clonedInternalWatcher = (IWatcher) InternalWatcher.Clone();
            return new RefreshableWatcher(clonedInternalWatcher) {RefreshAttempInterval = RefreshAttempInterval};
        }
    }
}