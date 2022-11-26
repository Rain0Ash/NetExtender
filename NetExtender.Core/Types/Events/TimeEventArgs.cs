// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Timers;

namespace NetExtender.Types.Events
{
    public class TimeEventArgs : EventArgs
    {
        [return: NotNullIfNotNull("args")]
        public static implicit operator TimeEventArgs?(ElapsedEventArgs? args)
        {
            return args is not null ? new TimeEventArgs(args.SignalTime) : null;
        }

        public DateTime SignalTime { get; }

        public TimeEventArgs()
            : this(DateTime.Now)
        {
        }

        public TimeEventArgs(DateTime time)
        {
            SignalTime = time;
        }
    }
}