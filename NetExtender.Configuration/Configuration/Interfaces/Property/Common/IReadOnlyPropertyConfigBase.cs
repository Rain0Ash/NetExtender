// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Interfaces.Property.Common
{
    public interface IReadOnlyPropertyConfigBase
    {
        public T? GetValue<T>(IReadOnlyConfigProperty<T> property);

        public Boolean KeyExist(IReadOnlyConfigPropertyBase property);
    }
}