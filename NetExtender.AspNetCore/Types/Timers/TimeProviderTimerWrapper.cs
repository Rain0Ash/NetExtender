// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NET8_0_OR_GREATER
using System;
using System.Threading;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.AspNetCore
{
    public interface ITimeServiceTimer : NetExtender.Types.Timers.Interfaces.ITimer
    {
        public new Boolean Change(TimeSpan dueTime, TimeSpan period);
    }

    public sealed class TimeProviderTimerWrapper : NetExtender.Types.Timers.TimeProviderTimerWrapper, ITimeServiceTimer
    {
        public TimeProviderTimerWrapper(TimeProvider provider, TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
            : base(provider, callback, state, dueTime, period)
        {
        }

        public TimeProviderTimerWrapper(ITimeProvider provider, TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
            : base(provider, callback, state, dueTime, period)
        {
        }
    }
}
#endif