// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utils.Types
{
    public static class ActionUtils
    {
        public static Task InBackground(this Action action)
        {
            return InBackground(action, false);
        }
        
        public static Task InBackground(this Action action, Boolean fairness)
        {
            return InBackground(action, fairness, CancellationToken.None);
        }
        
        public static Task InBackground(this Action action, CancellationToken token)
        {
            return InBackground(action, false, token);
        }
        
        public static Task InBackground(this Action action, Boolean fairness, CancellationToken token)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            TaskCreationOptions options = TaskCreationOptions.DenyChildAttach;

            if (fairness)
            {
                options |= TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness;
            }

            return Task.Factory.StartNew(action, token, options, TaskScheduler.Default);
        }
    }
}