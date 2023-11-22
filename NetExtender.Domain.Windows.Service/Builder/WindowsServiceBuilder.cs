// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using NetExtender.Domains.Builder;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Domains.Service.Builder
{
    public abstract class WindowsServiceBuilder : ApplicationBuilder<IWindowsService>
    {
    }
    
    public class WindowsServiceBuilder<T> : ApplicationBuilder<T> where T : class, IWindowsService
    {
        public override T Build(ImmutableArray<String> arguments)
        {
            return New(arguments);
        }
    }
}