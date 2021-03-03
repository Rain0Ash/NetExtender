// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using NetExtender.Watchers.FileSystem.Interfaces;
using NetExtender.Watchers.FileSystem.Other;

 namespace NetExtender.Watchers.FileSystem
{
    /// <summary>
    /// An IWatcher wrapper which automaticly refreshes it when specific errors occurre.
    /// </summary>
    public class AutoRefreshingWatcher : RefreshableWatcher
    {
        private List<WatcherErrorHandlingPolicy> _errorHandlingPolicies;


        public IReadOnlyCollection<WatcherErrorHandlingPolicy> ErrorHandlingPolicies
        {
            get
            {
                return _errorHandlingPolicies;
            }
        }


        public AutoRefreshingWatcher()
        {
            InitBasicPolicies();
        }

        public AutoRefreshingWatcher(FileSystemWatcher watcher)
            : base(watcher)
        {
            InitBasicPolicies();
        }

        public AutoRefreshingWatcher(IWatcher watcher)
            : base(watcher)
        {
            InitBasicPolicies();
        }

        public AutoRefreshingWatcher(String path)
            : base(path)
        {
            InitBasicPolicies();
        }

        public AutoRefreshingWatcher(String path, String filter)
            : base(path, filter)
        {
            InitBasicPolicies();
        }

        public void InitBasicPolicies()
        {
            _errorHandlingPolicies = new List<WatcherErrorHandlingPolicy>();

            WatcherErrorHandlingPolicy accessDeniedPolicy = new WatcherErrorHandlingPolicy(
                typeof(Win32Exception),
                "When an 'access denied' win32 exception occures, refresh the wrapped watcher.",
                exception =>
                    (exception as Win32Exception)?.NativeErrorCode == 5 ? WatcherErrorHandlingType.RefreshAndSwallow : WatcherErrorHandlingType.Forward);

            WatcherErrorHandlingPolicy netNameDeletedPolicy = new WatcherErrorHandlingPolicy(
                typeof(Win32Exception),
                "When a 'net name deleted' win32 exception occures, refresh the wrapped watcher.",
                exception =>
                    (exception as Win32Exception)?.NativeErrorCode == 64 ? WatcherErrorHandlingType.RefreshAndSwallow : WatcherErrorHandlingType.Forward);

            _errorHandlingPolicies.Add(accessDeniedPolicy);
            _errorHandlingPolicies.Add(netNameDeletedPolicy);
        }


        /// <summary>
        /// Removes all currently set error handling policies
        /// </summary>
        public void ClearPolicies()
        {
            _errorHandlingPolicies.Clear();
        }

        /// <summary>
        /// Tries to remove a specific error handling policy
        /// </summary>
        /// <param name="pol">The policy to remove</param>
        /// <returns>
        ///     true if policy is successfully removed; otherwise, false. This method also returns
        ///     false if policy was not found in the policies collection.
        /// </returns>
        public Boolean RemovePolicy(WatcherErrorHandlingPolicy pol)
        {
            return _errorHandlingPolicies.Remove(pol);
        }

        /// <summary>
        /// Adds an error handling policy
        /// </summary>
        /// <param name="pol"></param>
        public void AddPolicy(WatcherErrorHandlingPolicy pol)
        {
            _errorHandlingPolicies.Add(pol);
        }

        /// <summary>
        /// Inoked when the wrapped watcher throws an exception. The exception is tested with the existing policies
        /// and handled according to the tests results.
        /// </summary>
        /// <param name="sender">Raiser of the event</param>
        /// <param name="e">Error event args</param>
        protected override void OnError(Object sender, ErrorEventArgs e)
        {
            Exception ex = e.GetException();
            Type exType = ex.GetType();
            WatcherErrorHandlingType exHandling = ErrorHandlingPolicies.Where(policy => policy.ExceptionType == exType)
                .Aggregate(WatcherErrorHandlingType.Forward, (current, relevantPolicy) => current | relevantPolicy.Test(ex));

            // Testing all relevant policies according to the exception type

            // Check the policies test results.

            //  If ANY of the policies requested a refresh - a refresh will be invoked
            if (exHandling.HasFlag(WatcherErrorHandlingType.Refresh))
            {
                // Tries to refresh. If a refresh is already in progress, the thread returns.
                _ = RefreshAsync(false);
            }

            //  If NONE of the policies requested a swallow - the error will be forwarded
            //  (if any of them DID request a swallow, the error will be swallowed)
            if (!exHandling.HasFlag(WatcherErrorHandlingType.Swallow))
            {
                base.OnError(sender, e);
            }
        }


        public override Object Clone()
        {
            IWatcher clonedEncapsWatcher = InternalWatcher.Clone() as IWatcher;
            AutoRefreshingWatcher clonedAutoRefreshingWatcher = new AutoRefreshingWatcher(clonedEncapsWatcher);
            // Add current refresher's policies to the cloned one
            clonedAutoRefreshingWatcher.ClearPolicies();
            foreach (WatcherErrorHandlingPolicy policy in _errorHandlingPolicies)
            {
                clonedAutoRefreshingWatcher.AddPolicy(policy);
            }

            return clonedAutoRefreshingWatcher;
        }
    }
}