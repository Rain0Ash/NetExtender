// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Tasks.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers.Interfaces
{
    public interface ITimer : IStartable, IDisposable, IAsyncDisposable
#if NET8_0_OR_GREATER
    , System.Threading.ITimer    
#endif
    {
        public event TickHandler Tick;
        public DateTime Now { get; }
        public DateTimeKind Kind { get; set; }
        public TimeSpan Interval { get; set; }

        public Boolean TrySetKind(DateTimeKind kind);
        
#if !NET8_0_OR_GREATER
        public Boolean Change(TimeSpan dueTime, TimeSpan period);
#endif
    }
}