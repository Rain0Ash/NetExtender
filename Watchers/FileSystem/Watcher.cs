// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Watchers.FileSystem.Interfaces;

namespace NetExtender.Watchers.FileSystem
{
    /// <summary>
    /// Different approachs to handle a FileSystemWatcher error.
    /// </summary>
    [Flags]
    public enum WatcherErrorHandlingType
    {
        /// <summary>
        /// Forward the error using the Error event
        /// </summary>
        Forward = 0,
        /// <summary>
        /// Do not forward the error using the Error event
        /// </summary>
        Swallow = 1,
        /// <summary>
        /// Refresh the internal watcher
        /// </summary>
        Refresh = 2,
        /// <summary>
        /// Refresh the internal watcher and do not forward the error using the Error event
        /// </summary>
        RefreshAndSwallow = 3,
    }

    public enum WatcherType
    {
        /// <summary>
        /// <returns>null</returns>
        /// </summary>
        None,
        /// <summary>
        /// <returns>FakeWatcher</returns>
        /// </summary>
        Fake,
        /// <summary>
        /// Events: all supported
        /// Remote Host Disconnected: Breaks
        /// Remote Folder Deleted: Breaks
        /// Local Folder Deleted: Breaks
        /// Internal Buffer Overflow: Misses Files
        /// <returns>System.IO.FileSystemWatcher</returns>
        /// </summary>
        IOWatcher,
        /// <summary>
        /// Events: all supported
        /// Remote Host Disconnected: User can refresh
        /// Remote Folder Deleted: User can refresh
        /// Local Folder Deleted: User can refresh
        /// Internal Buffer Overflow: Misses Files
        /// <returns>RefresableWatcher</returns>
        /// </summary>
        Refresh,
        /// <summary>
        /// Events: All supported
        /// Remote Host Disconnected: Triggers watcher refresh
        /// Remote Folder Deleted: Triggers watcher refresh
        /// Local Folder Deleted: User can refresh
        /// Internal Buffer Overflow: Misses Files
        /// <returns>AutoRefreshingWatcher</returns>
        /// </summary>
        AutoRefresh,
        /// <summary>
        /// Events: changed and renamed not supported
        /// Remote Host Disconnected: Continues when available
        /// Remote Folder Deleted: Continues when available
        /// Local Folder Deleted: Continues when available
        /// Internal Buffer Overflow: No effect
        /// <returns>PollerWatcher</returns>
        /// </summary>
        Poller,
        /// <summary>
        /// Events: changed and renamed supported while the internal buffer doesn't overflow. Falls back to Created/Deleted events instead of renames if it does.
        /// Remote Host Disconnected: Triggers watcher refresh
        /// Remote Folder Deleted: Triggers watcher refresh
        /// Local Folder Deleted: Triggers watcher refresh
        /// Internal Buffer Overflow: Triggers polling
        /// <returns>OverseerWatcher</returns>
        /// </summary>
        Overseer
    }

    public static class Watcher
    {
        public static IWatcher Create(WatcherType type, String path, String filter = "")
        {
            return Create(type, path, TimeSpan.FromMinutes(1), filter);
        }
        
        public static IWatcher Create(WatcherType type, String path, TimeSpan polling, String filter = "")
        {
            return type switch
            {
                WatcherType.None => null,
                WatcherType.Fake => new FakeWatcher(path, filter),
                WatcherType.IOWatcher => new FileSystemWatcherExtended(path, filter),
                WatcherType.Refresh => new RefreshableWatcher(path, filter),
                WatcherType.AutoRefresh => new AutoRefreshingWatcher(path, filter),
                WatcherType.Poller => new PollerWatcher(polling, path, filter),
                WatcherType.Overseer => new OverseerWatcher(polling, path, filter),
                _ => throw new NotSupportedException($"{type} is not supported")
            };
        }
    }
}