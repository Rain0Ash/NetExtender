// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;

namespace NetExtender.Events
{
    public class TypeCancelEventArgs<T> : CancelEventArgs
    {
        public static implicit operator TypeCancelEventArgs<T?>(TypeCancellationEventArgs<T?> args)
        {
            return new TypeCancelEventArgs<T?>(args.Value, args.IsCancelled);
        }
        
        public static implicit operator TypeCancelEventArgs<T?>(TypeHandledEventArgs<T?> args)
        {
            return new TypeCancelEventArgs<T?>(args.Value, args.Handled);
        }
        
        public T? Value { get; }
        
        public TypeCancelEventArgs(T? value)
        {
            Value = value;
        }

        public TypeCancelEventArgs(Boolean cancel)
            : base(cancel)
        {
        }

        public TypeCancelEventArgs(T? value, Boolean cancel)
            : base(cancel)
        {
            Value = value;
        }
    }
}