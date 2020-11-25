// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Events.Args
{
    public class TypeCancellationEventArgs<T> : CancellationEventArgs
    {
        public T Value { get; }
        
        public TypeCancellationEventArgs(T value)
        {
            Value = value;
        }

        public TypeCancellationEventArgs(Boolean cancel)
            : base(cancel)
        {
        }

        public TypeCancellationEventArgs(T value, Boolean cancel)
            : base(cancel)
        {
            Value = value;
        }
    }
}