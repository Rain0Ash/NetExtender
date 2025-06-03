// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Reflection;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IPropertyInterceptEventArgs<T> : IPropertyInterceptEventArgs, IMemberInterceptEventArgs<PropertyInfo, T>
    {
    }
    
    public interface IPropertyInterceptEventArgs : IMemberInterceptEventArgs<PropertyInfo>
    {
        public PropertyInfo Property { get; }
        public PropertyInterceptAccessor Accessor { get; }
    }
}