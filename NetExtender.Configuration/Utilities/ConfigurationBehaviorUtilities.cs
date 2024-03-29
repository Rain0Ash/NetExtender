// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Wrappers;

namespace NetExtender.Utilities.Configuration
{
    public static class ConfigurationBehaviorUtilities
    {
        public static IConfig Create(this IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new Config(behavior);
        }

        public static IConfigBehavior Temporary(this IConfigBehavior behavior)
        {
            return Temporary(behavior, true);
        }

        public static IConfigBehavior Temporary(this IConfigBehavior behavior, Boolean reload)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            IConfigBehavior temporary = new TemporaryConfigurationBehaviorWrapper(behavior);

            if (reload)
            {
                temporary.Reload();
            }

            return temporary;
        }

        public static Task<IConfigBehavior> TemporaryAsync(this IConfigBehavior behavior)
        {
            return TemporaryAsync(behavior, true);
        }

        public static async Task<IConfigBehavior> TemporaryAsync(this IConfigBehavior behavior, Boolean reload)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            IConfigBehavior temporary = new TemporaryConfigurationBehaviorWrapper(behavior);

            if (reload)
            {
                await temporary.ReloadAsync(CancellationToken.None).ConfigureAwait(false);
            }

            return temporary;
        }

        public static IConfigBehavior Concurrent(this IConfigBehavior behavior)
        {
            return Concurrent(behavior, false);
        }

        public static IConfigBehavior Concurrent(this IConfigBehavior behavior, Boolean duplicate)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            if (behavior.IsThreadSafe && !duplicate)
            {
                return behavior;
            }

            return new ConcurrentConfigBehavior(behavior);
        }

        public static IConfigBehavior Concurrent(this IConfigBehavior behavior, Object? synchronization)
        {
            return Concurrent(behavior, synchronization, false);
        }

        public static IConfigBehavior Concurrent(this IConfigBehavior behavior, Object? synchronization, Boolean duplicate)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            if (behavior.IsThreadSafe && !duplicate)
            {
                return behavior;
            }

            return new ConcurrentConfigBehavior(behavior, synchronization);
        }
    }
}