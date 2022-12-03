// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Anonymous.Interfaces;

namespace NetExtender.ReactiveUI.Types.Anonymous.Interfaces
{
    public interface IReactiveAnonymousActivatorInfo : IAnonymousActivatorInfo
    {
    }
    
    public interface IReactiveAnonymousActivator : IAnonymousActivator, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate();
    }
    
    public interface IReactiveAnonymousActivator<in T> : IAnonymousActivator<T>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T argument);
    }
    
    public interface IReactiveAnonymousActivator<in T1, in T2> : IAnonymousActivator<T1, T2>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T1 first, T2 second);
    }
    
    public interface IReactiveAnonymousActivator<in T1, in T2, in T3> : IAnonymousActivator<T1, T2, T3>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T1 first, T2 second, T3 third);
    }
    
    public interface IReactiveAnonymousActivator<in T1, in T2, in T3, in T4> : IAnonymousActivator<T1, T2, T3, T4>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T1 first, T2 second, T3 third, T4 fourth);
    }
    
    public interface IReactiveAnonymousActivator<in T1, in T2, in T3, in T4, in T5> : IAnonymousActivator<T1, T2, T3, T4, T5>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
    }
    
    public interface IReactiveAnonymousActivator<in T1, in T2, in T3, in T4, in T5, in T6> : IAnonymousActivator<T1, T2, T3, T4, T5, T6>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
    }
    
    public interface IReactiveAnonymousActivator<in T1, in T2, in T3, in T4, in T5, in T6, in T7> : IAnonymousActivator<T1, T2, T3, T4, T5, T6, T7>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
    }
    
    public interface IReactiveAnonymousActivator<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8> : IAnonymousActivator<T1, T2, T3, T4, T5, T6, T7, T8>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
    }
    
    public interface IReactiveAnonymousActivator<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9> : IAnonymousActivator<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IReactiveAnonymousActivatorInfo
    {
        public new IReactiveAnonymousObject Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
    }
}