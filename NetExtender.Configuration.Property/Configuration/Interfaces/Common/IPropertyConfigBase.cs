// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Property.Interfaces.Common
{
    public interface IPropertyConfigBase : IReadOnlyPropertyConfigBase
    {
        public Boolean SetValue<T>(IReadOnlyConfigProperty<T> property, T value);

        public T? GetOrSetValue<T>(IReadOnlyConfigProperty<T> property);

        public T? GetOrSetValue<T>(IReadOnlyConfigProperty<T> property, T value);

        public Boolean RemoveValue(IReadOnlyConfigPropertyBase property);
    }
}