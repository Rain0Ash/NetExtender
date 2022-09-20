// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Tasks.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers.Interfaces
{
    public interface ITimer : IStartable, IDisposable, IAsyncDisposable
    {
        public event TickHandler Tick;
        public TimeSpan Interval { get; set; }
    }
}