// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AspNet.Core.Types.Initializers.Interfaces
{
    public interface IStartupRegistrationProvider : IStartupProvider
    {
        public IStartupRegistrationProvider Register(Action<IServiceCollection> collection);
        public IStartupRegistrationProvider Register(Action<IApplicationBuilder> application);
        public IStartupRegistrationProvider Register(Action<IServiceProvider> provider);
        public IStartupRegistrationProvider Register(Action<IApplicationBuilder> application, Action<IServiceProvider> provider);
        public IStartupRegistrationProvider Register(Action<IApplicationBuilder, IServiceProvider> configuration);
        public IStartupRegistrationProvider RegisterCallback(Action callback);
    }
}