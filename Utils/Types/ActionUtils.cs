// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utils.Types
{
    public static class ActionUtils
    {
        public static async void InBackground(Action action, Boolean longRunning = false)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            TaskCreationOptions options = TaskCreationOptions.DenyChildAttach;

            if (longRunning) {
                options |= TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness;
            }

            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await Task.Factory.StartNew(action, CancellationToken.None, options, TaskScheduler.Default).ConfigureAwait(false);
        }
    }
}