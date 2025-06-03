// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using NetExtender.Types.Intercept.Interfaces;

namespace NetExtender.Types.Intercept
{
    public abstract class PropertyInterceptEventArgsFactory<TArgument, TInfo> : IPropertyInterceptEventArgsFactory<TArgument, TInfo>
    {
        public abstract TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, TInfo? info);
        public abstract TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, T value, TInfo? info);
        public abstract TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, Exception exception, TInfo? info);
    }
}