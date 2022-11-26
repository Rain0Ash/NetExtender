// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Events
{
    public class SuccessfulEventArgs<T> : SuccessfulEventArgs
    {
        public T Value { get; }

        public SuccessfulEventArgs(T value)
            : this(value, true)
        {
        }

        public SuccessfulEventArgs(Boolean successful)
            : this(default!, successful)
        {
        }

        public SuccessfulEventArgs(T value, Boolean successful)
            : base(successful)
        {
            Value = value;
        }
    }

    public class SuccessfulEventArgs : EventArgs
    {
        public Boolean IsSuccessful { get; }

        public SuccessfulEventArgs()
        {
        }

        public SuccessfulEventArgs(Boolean successful)
        {
            IsSuccessful = successful;
        }
    }
}