using System;
using System.Collections.Immutable;
using System.Reflection;

namespace NetExtender.Types.Interception.Interfaces
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