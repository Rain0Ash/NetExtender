// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NET8_0_OR_GREATER
using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public sealed class IdentityTimeServiceWrapper<TId, TUser, TRole> : IIdentityTimeService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private ITimeProvider Provider { get; }

        public TimeZoneInfo LocalTimeZone
        {
            get
            {
                return Provider.LocalTimeZone;
            }
        }

        public Int64 TimestampFrequency
        {
            get
            {
                return Provider.TimestampFrequency;
            }
        }

        public IdentityTimeServiceWrapper(TimeProvider provider)
            : this(provider is not null ? provider as ITimeProvider ?? new TimeProviderWrapper(provider) : throw new ArgumentNullException(nameof(provider)))
        {
        }

        private IdentityTimeServiceWrapper(ITimeProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public NetExtender.Types.Timers.Interfaces.ITimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
        {
            return Provider.CreateTimer(callback, state, dueTime, period);
        }

        public Int64 GetTimestamp()
        {
            return Provider.GetTimestamp();
        }

        public DateTimeOffset GetUtcNow()
        {
            return Provider.GetUtcNow();
        }

        public DateTimeOffset GetLocalNow()
        {
            return Provider.GetLocalNow();
        }

        public TimeSpan GetElapsedTime(Int64 start)
        {
            return Provider.GetElapsedTime(start);
        }

        public TimeSpan GetElapsedTime(Int64 start, Int64 end)
        {
            return Provider.GetElapsedTime(start, end);
        }

        public void Dispose()
        {
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}
#endif