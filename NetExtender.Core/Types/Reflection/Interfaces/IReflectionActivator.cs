// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IReflectionActivator
    {
        public Type Type { get; }
        public Type[] Arguments { get; }
    }

    public interface IReflectionActivator<out TSource> : IReflectionActivator
    {
        public TSource Activate();
    }
    
    public interface IReflectionActivator<out TSource, in T> : IReflectionActivator
    {
        public TSource Activate(T argument);
    }
    
    public interface IReflectionActivator<out TSource, in T1, in T2> : IReflectionActivator
    {
        public TSource Activate(T1 first, T2 second);
    }
    
    public interface IReflectionActivator<out TSource, in T1, in T2, in T3> : IReflectionActivator
    {
        public TSource Activate(T1 first, T2 second, T3 third);
    }
    
    public interface IReflectionActivator<out TSource, in T1, in T2, in T3, in T4> : IReflectionActivator
    {
        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth);
    }
    
    public interface IReflectionActivator<out TSource, in T1, in T2, in T3, in T4, in T5> : IReflectionActivator
    {
        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
    }
    
    public interface IReflectionActivator<out TSource, in T1, in T2, in T3, in T4, in T5, in T6> : IReflectionActivator
    {
        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
    }
    
    public interface IReflectionActivator<out TSource, in T1, in T2, in T3, in T4, in T5, in T6, in T7> : IReflectionActivator
    {
        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
    }
    
    public interface IReflectionActivator<out TSource, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8> : IReflectionActivator
    {
        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
    }
    
    public interface IReflectionActivator<out TSource, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9> : IReflectionActivator
    {
        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
    }
}