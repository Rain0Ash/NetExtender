using System;
using System.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static class EventArgsUtilities
    {
        private static class ElapsedEventArgs
        {
            public static Action<System.Timers.ElapsedEventArgs, DateTime>? SignalTime { get; }
        
            static ElapsedEventArgs()
            {
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                if (typeof(System.Timers.ElapsedEventArgs).GetField($"<{nameof(System.Timers.ElapsedEventArgs.SignalTime)}>k__BackingField", binding) is { } field)
                {
                    SignalTime = field.CreateSetExpression<System.Timers.ElapsedEventArgs, DateTime>().Compile();
                }
            }
        }
        
        public static Boolean SetSignalTime(this System.Timers.ElapsedEventArgs args, DateTime value)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (ElapsedEventArgs.SignalTime is not { } setter)
            {
                return false;
            }

            setter.Invoke(args, value);
            return true;
        }
    }
}