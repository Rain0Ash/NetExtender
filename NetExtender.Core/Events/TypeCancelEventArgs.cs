// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Events
{
    public class TypeCancelEventArgs<T> : CancelEventArgs
    {
        [return: NotNullIfNotNull("args")]
        public static implicit operator TypeCancelEventArgs<T>?(TypeCancellationEventArgs<T>? args)
        {
            return args is not null ? new TypeCancelEventArgs<T>(args.Value, args.IsCancelled) : null;
        }
        
        [return: NotNullIfNotNull("args")]
        public static implicit operator TypeCancelEventArgs<T>?(TypeHandledEventArgs<T>? args)
        {
            return args is not null ? new TypeCancelEventArgs<T>(args.Value, args.Handled) : null;
        }
        
        public T Value { get; }
        
        public TypeCancelEventArgs(T value)
        {
            Value = value;
        }

        public TypeCancelEventArgs(Boolean cancel)
            : base(cancel)
        {
            Value = default!;
        }

        public TypeCancelEventArgs(T value, Boolean cancel)
            : base(cancel)
        {
            Value = value;
        }
    }
}