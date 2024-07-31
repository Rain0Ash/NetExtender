// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.DependencyInjection.Interfaces
{
    internal interface IUnscanServiceDependency : IServiceDependency
    {
    }
    
    public interface ISpecialServiceDependency<T> : ISpecialServiceDependency, IServiceDependency<T> where T : class
    {
    }
    
    public interface ISpecialServiceDependency
    {
    }
}