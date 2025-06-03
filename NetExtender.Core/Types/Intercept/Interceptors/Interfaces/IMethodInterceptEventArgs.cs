// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Reflection;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IMethodInterceptEventArgs<T> : IMethodInterceptEventArgs, IMemberInterceptEventArgs<MethodInfo, T>
    {
    }
    
    public interface IMethodInterceptEventArgs : IMemberInterceptEventArgs<MethodInfo>
    {
        public MethodInfo Method { get; }
        public ImmutableArray<Object?> Arguments { get; }
    }
}