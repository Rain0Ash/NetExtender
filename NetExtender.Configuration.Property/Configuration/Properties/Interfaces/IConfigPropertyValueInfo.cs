// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Properties.Interfaces
{
    public interface IConfigPropertyValueInfo : IConfigPropertyInfo
    {
        public event ConfigurationChangedEventHandler Changed;
        public String? Value { get; }
        public String? Alternate { get; }
    }

    public interface IConfigPropertyValueInfo<T> : IConfigPropertyInfo
    {
        public event ConfigurationChangedEventHandler<T> Changed;
        public T Value { get; }
        public T Alternate { get; }
    }
}