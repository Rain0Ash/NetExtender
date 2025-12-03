// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NET8_0_OR_GREATER
using System;
using System.Threading;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.AspNetCore
{
    public interface ITimeServiceTimeProvider : ITimeProvider
    {
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
}
#endif