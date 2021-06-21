// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Events
{
    public class TypeCancellationEventArgs<T> : CancellationEventArgs
    {
        public static implicit operator TypeCancellationEventArgs<T?>(TypeCancelEventArgs<T?> args)
        {
            return new TypeCancellationEventArgs<T?>(args.Value, args.Cancel);
        }
        
        public static implicit operator TypeCancellationEventArgs<T?>(TypeHandledEventArgs<T?> args)
        {
            return new TypeCancellationEventArgs<T?>(args.Value, args.Handled);
        }
        
        public T? Value { get; }
        
        public TypeCancellationEventArgs(T? value)
        {
            Value = value;
        }

        public TypeCancellationEventArgs(Boolean cancel)
            : base(cancel)
        {
        }

        public TypeCancellationEventArgs(T? value, Boolean cancel)
            : base(cancel)
        {
            Value = value;
        }
    }
}