// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Reflection.Interfaces;

namespace NetExtender.Types.Anonymous.Interfaces
{
    public interface IAnonymousActivatorInfo : IReflectionActivator
    {
    }
    
    public interface IAnonymousActivator : IReflectionActivator<IAnonymousObject>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T> : IReflectionActivator<IAnonymousObject, T>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T1, in T2> : IReflectionActivator<IAnonymousObject, T1, T2>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T1, in T2, in T3> : IReflectionActivator<IAnonymousObject, T1, T2, T3>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T1, in T2, in T3, in T4> : IReflectionActivator<IAnonymousObject, T1, T2, T3, T4>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T1, in T2, in T3, in T4, in T5> : IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T1, in T2, in T3, in T4, in T5, in T6> : IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5, T6>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T1, in T2, in T3, in T4, in T5, in T6, in T7> : IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5, T6, T7>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8> : IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5, T6, T7, T8>, IAnonymousActivatorInfo
    {
    }
    
    public interface IAnonymousActivator<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9> : IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5, T6, T7, T8, T9>, IAnonymousActivatorInfo
    {
    }
}