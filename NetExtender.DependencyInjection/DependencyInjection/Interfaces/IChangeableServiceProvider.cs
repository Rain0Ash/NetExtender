// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.DependencyInjection.Events;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IChangeableServiceProvider : IServiceProvider
    {
        public event ServiceProviderChangedEventHandler? Changed;
        public Boolean IsStable { get; }
        public Boolean IsFinal { get; }
        
        public IServiceProvider? Rebuild();
        public void Final();
    }
}