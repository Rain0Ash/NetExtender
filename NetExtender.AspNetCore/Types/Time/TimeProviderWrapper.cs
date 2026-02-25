// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NET8_0_OR_GREATER
using System;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.AspNetCore
{
#pragma warning disable CS0618
    public interface ITimeServiceTimeProvider : ITimeProvider, ISystemClock
    {
        DateTimeOffset ISystemClock.UtcNow
        {
            get
            {
                return GetUtcNow();
            }
        }

        public new ITimeServiceTimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period);
    }

    public class TimeProviderWrapper : NetExtender.Types.Times.TimeProviderWrapper, ITimeServiceTimeProvider
    {
        public TimeProviderWrapper(TimeProvider provider)
            : base(provider)
        {
        }

        public override ITimeServiceTimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
        {
            return new TimeProviderTimerWrapper(Provider, callback, state, dueTime, period);
        }
    }
#pragma warning restore CS0618
}
#endif