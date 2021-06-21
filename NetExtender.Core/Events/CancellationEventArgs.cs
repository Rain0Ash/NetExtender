// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;

namespace NetExtender.Events
{
    public class CancellationEventArgs : EventArgs
    {
        public static implicit operator CancellationEventArgs(CancelEventArgs args)
        {
            return new CancellationEventArgs(args.Cancel);
        }
        
        public static implicit operator CancelEventArgs(CancellationEventArgs args)
        {
            return new CancelEventArgs(args.IsCancelled);
        }
        
        public static implicit operator CancellationEventArgs(HandledEventArgs args)
        {
            return new CancellationEventArgs(args.Handled);
        }
        
        public static implicit operator HandledEventArgs(CancellationEventArgs args)
        {
            return new HandledEventArgs(args.IsCancelled);
        }
        
        public Boolean IsCancelled { get; private set; }

        public CancellationEventArgs()
        {
        }
        
        public CancellationEventArgs(Boolean cancel)
        {
            IsCancelled = cancel;
        }

        public void Cancel()
        {
            if (IsCancelled)
            {
                return;
            }

            IsCancelled = true;
        }
    }
}