// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Timers;

namespace NetExtender.Events
{
    public class TimeEventArgs : EventArgs
    {
        public static implicit operator TimeEventArgs(ElapsedEventArgs args)
        {
            return new TimeEventArgs(args.SignalTime);
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