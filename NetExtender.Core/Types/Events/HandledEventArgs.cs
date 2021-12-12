// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Events
{
    public class HandledEventArgs<T> : HandledEventArgs
    {
        [return: NotNullIfNotNull("args")]
        public static implicit operator HandledEventArgs<T>?(CancelEventArgs<T>? args)
        {
            return args is not null ? new HandledEventArgs<T>(args.Value, args.Cancel) : null;
        }
        
        [return: NotNullIfNotNull("args")]
        public static implicit operator HandledEventArgs<T>?(CancellationEventArgs<T>? args)
        {
            return args is not null ? new HandledEventArgs<T>(args.Value, args.IsCancelled) : null;
        }
        
        public T Value { get; }
        
        public HandledEventArgs(T value)
        {
            Value = value;
        }

        public HandledEventArgs(T value, Boolean handled)
            : base(handled)
        {
            Value = value;
        }
    }
}