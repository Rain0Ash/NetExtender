// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Events
{
    public class TypeCancellationEventArgs<T> : CancellationEventArgs
    {
        [return: NotNullIfNotNull("args")]
        public static implicit operator TypeCancellationEventArgs<T>?(TypeCancelEventArgs<T>? args)
        {
            return args is not null ? new TypeCancellationEventArgs<T>(args.Value, args.Cancel) : null;
        }
        
        [return: NotNullIfNotNull("args")]
        public static implicit operator TypeCancellationEventArgs<T>?(TypeHandledEventArgs<T>? args)
        {
            return args is not null ? new TypeCancellationEventArgs<T>(args.Value, args.Handled) : null;
        }
        
        public T Value { get; }
        
        public TypeCancellationEventArgs(T value)
        {
            Value = value;
        }

        public TypeCancellationEventArgs(Boolean cancel)
            : base(cancel)
        {
            Value = default!;
        }

        public TypeCancellationEventArgs(T value, Boolean cancel)
            : base(cancel)
        {
            Value = value;
        }
    }
}