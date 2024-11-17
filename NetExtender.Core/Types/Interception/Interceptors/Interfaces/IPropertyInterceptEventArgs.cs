using System.Reflection;

namespace NetExtender.Types.Interception.Interfaces
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