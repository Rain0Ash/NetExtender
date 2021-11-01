// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Events
{
    public class TypeHandledEventArgs<T> : HandledEventArgs
    {
        [return: NotNullIfNotNull("args")]
        public static implicit operator TypeHandledEventArgs<T>?(TypeCancelEventArgs<T>? args)
        {
            return args is not null ? new TypeHandledEventArgs<T>(args.Value, args.Cancel) : null;
        }
        
        [return: NotNullIfNotNull("args")]
        public static implicit operator TypeHandledEventArgs<T>?(TypeCancellationEventArgs<T>? args)
        {
            return args is not null ? new TypeHandledEventArgs<T>(args.Value, args.IsCancelled) : null;
        }
        
        public T Value { get; }
        
        public TypeHandledEventArgs(T value)
        {
            Value = value;
        }

        public TypeHandledEventArgs(T value, Boolean handled)
            : base(handled)
        {
            Value = value;
        }
    }
}