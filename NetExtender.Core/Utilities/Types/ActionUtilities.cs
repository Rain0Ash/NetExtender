// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Application;

namespace NetExtender.Utilities.Types
{
    public static class ActionUtilities
    {
        public static void Default()
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T>(T first)
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T1, T2>(T1 first, T2 second)
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T1, T2, T3>(T1 first, T2 second, T3 third)
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T1, T2, T3, T4>(T1 first, T2 second, T3 third, T4 fourth)
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T1, T2, T3, T4, T5>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T1, T2, T3, T4, T5, T6>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T1, T2, T3, T4, T5, T6, T7>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T1, T2, T3, T4, T5, T6, T7, T8>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
        }
        
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        public static void Default<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
        }
        
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

        public static Action WithFailFast(this Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            void Action()
            {
                try
                {
                    action();
                }
                catch (Exception exception)
                {
                    if (!Debugger.IsAttached)
                    {
                        throw EnvironmentUtilities.FailFast(exception);
                    }

                    Debugger.Break();
                    throw new NeverOperationException(exception);
                }
            }

            return Action;
        }

        public static void Invoke(this IEnumerable<Action?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Action? action in source)
            {
                action?.Invoke();
            }
        }
    }
}