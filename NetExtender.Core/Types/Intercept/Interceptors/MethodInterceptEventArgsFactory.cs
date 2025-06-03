// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using NetExtender.Types.Intercept.Interfaces;

namespace NetExtender.Types.Intercept
{
    public abstract class MethodInterceptEventArgsFactory<TArgument, TInfo> : IMethodInterceptEventArgsFactory<TArgument, TInfo>
    {
        public virtual TArgument Create(MethodInfo method, IEnumerable<Object?>? parameters, TInfo? info)
        {
            return Create(method, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, info);
        }

        public abstract TArgument Create(MethodInfo method, ImmutableArray<Object?> parameters, TInfo? info);

        public virtual TArgument Create(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, TInfo? info)
        {
            return Create(method, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, exception, info);
        }

        public abstract TArgument Create(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, TInfo? info);

        public virtual TArgument Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, TInfo? info)
        {
            return Create<T>(method, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, info);
        }

        public abstract TArgument Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, TInfo? info);

        public virtual TArgument Create<T>(MethodInfo method, T value, IEnumerable<Object?>? parameters, TInfo? info)
        {
            return Create(method, value, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, info);
        }

        public abstract TArgument Create<T>(MethodInfo method, T value, ImmutableArray<Object?> parameters, TInfo? info);

        public virtual TArgument Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, TInfo? info)
        {
            return Create<T>(method, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, exception, info);
        }

        public abstract TArgument Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, TInfo? info);
    }
}