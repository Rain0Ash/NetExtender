// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Property.Interfaces
{
    public interface IReadOnlyConfigProperty<T> : IReadOnlyConfigPropertyBase
    {
        public event EventHandler<T> ValueChanged;  
        public Boolean ThrowOnInvalid { get; }
        public Boolean ThrowOnReadOnly { get; }
        public T Alternate { get; }
        public T Value { get; }
        public Boolean IsValid { get; }
        public Func<T?, Boolean> Validate { get; }
        public TryConverter<String?, T> Converter { get; }
        public T? GetValue();
        public T? GetValue(Func<T, Boolean> validate);
    }
}