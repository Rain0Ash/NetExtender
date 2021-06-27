// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Events
{
    public class CancellationEventArgs : EventArgs
    {
        [return: NotNullIfNotNull("args")]
        public static implicit operator CancellationEventArgs?(CancelEventArgs? args)
        {
            return args is not null ? new CancellationEventArgs(args.Cancel) : null;
        }
        
        [return: NotNullIfNotNull("args")]
        public static implicit operator CancelEventArgs?(CancellationEventArgs? args)
        {
            return args is not null ? new CancelEventArgs(args.IsCancelled) : null;
        }
        
        [return: NotNullIfNotNull("args")]
        public static implicit operator CancellationEventArgs?(HandledEventArgs? args)
        {
            return args is not null ? new CancellationEventArgs(args.Handled) : null;
        }
        
        [return: NotNullIfNotNull("args")]
        public static implicit operator HandledEventArgs?(CancellationEventArgs? args)
        {
            return args is not null ? new HandledEventArgs(args.IsCancelled) : null;
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