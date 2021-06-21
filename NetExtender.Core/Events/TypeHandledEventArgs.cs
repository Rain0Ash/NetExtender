// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;

namespace NetExtender.Events
{
    public class TypeHandledEventArgs<T> : HandledEventArgs
    {
        public static implicit operator TypeHandledEventArgs<T?>(TypeCancelEventArgs<T?> args)
        {
            return new TypeHandledEventArgs<T?>(args.Value, args.Cancel);
        }
        
        public static implicit operator TypeHandledEventArgs<T?>(TypeCancellationEventArgs<T?> args)
        {
            return new TypeHandledEventArgs<T?>(args.Value, args.IsCancelled);
        }
        
        public T? Value { get; }
        
        public TypeHandledEventArgs(T? value)
        {
            Value = value;
        }

        public TypeHandledEventArgs(T? value, Boolean handled)
            : base(handled)
        {
            Value = value;
        }
    }
}