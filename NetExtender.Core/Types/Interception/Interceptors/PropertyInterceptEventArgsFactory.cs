using System;
using System.Reflection;
using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Types.Interception
{
    public abstract class PropertyInterceptEventArgsFactory<TArgument, TInfo> : IPropertyInterceptEventArgsFactory<TArgument, TInfo>
    {
        public abstract TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, TInfo? info);
        public abstract TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, T value, TInfo? info);
        public abstract TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, Exception exception, TInfo? info);
    }
}