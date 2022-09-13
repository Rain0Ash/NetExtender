// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.View.Interfaces;

namespace NetExtender.Domains.Initializer.Interfaces
{
    public interface IApplicationInitializer : IApplication, IApplicationView
    {
        public IApplication Application { get; }
        public IApplicationView View { get; }
    }
}