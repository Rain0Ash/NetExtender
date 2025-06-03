// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IPropertyInterceptEventArgsFactory<out TArgument, in TInfo>
    {
        public TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, TInfo? info);
        public TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, T value, TInfo? info);
        public TArgument Create<T>(PropertyInfo property, PropertyInterceptAccessor accessor, Exception exception, TInfo? info);
    }
}