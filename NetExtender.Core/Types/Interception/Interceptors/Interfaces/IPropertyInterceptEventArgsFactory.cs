using System;
using System.Reflection;

namespace NetExtender.Types.Interception.Interfaces
{
    public interface IPropertyInterceptEventArgsFactory<out TArgument, in TInfo>
    {
        public TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, TInfo? info);
        public TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, T value, TInfo? info);
        public TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, Exception exception, TInfo? info);
    }
}