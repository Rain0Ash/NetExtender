// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Property.Interfaces
{
    public interface IConfigProperty<T> : IReadOnlyConfigProperty<T>, IConfigPropertyBase
    {
        public new T DefaultValue { get; set; }
        public new Boolean ThrowOnInvalid { get; set; }
        public new Boolean ThrowOnReadOnly { get; set; }
        public new TryConverter<String, T> Converter { get; set; }
        public void SetValue(T value);
        public T? GetOrSetValue();
        public void ResetValue();
        public void RemoveValue();
    }
}