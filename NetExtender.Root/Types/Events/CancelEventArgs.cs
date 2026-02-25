// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Events
{
    public class CancelEventArgs<T> : CancelEventArgs
    {
        [return: NotNullIfNotNull("args")]
        public static implicit operator CancelEventArgs<T>?(CancellationEventArgs<T>? args)
        {
            return args is not null ? new CancelEventArgs<T>(args.Value, args.IsCancelled) : null;
        }

        [return: NotNullIfNotNull("args")]
        public static implicit operator CancelEventArgs<T>?(HandledEventArgs<T>? args)
        {
            return args is not null ? new CancelEventArgs<T>(args.Value, args.Handled) : null;
        }

        public T Value { get; protected set; }

        public CancelEventArgs(T value)
        {
            Value = value;
        }

        public CancelEventArgs(Boolean cancel)
            : base(cancel)
        {
            Value = default!;
        }

        public CancelEventArgs(T value, Boolean cancel)
            : base(cancel)
        {
            Value = value;
        }
    }
}