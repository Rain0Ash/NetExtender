// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Domains.Builder;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Domains.Service.Builder
{
    public abstract class WindowsServiceBuilder : ApplicationBuilder<IWindowsService>
    {
    }
    
    public class WindowsServiceBuilder<T> : ApplicationBuilder<T> where T : class, IWindowsService, new()
    {
        public override T Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new T();
        }
    }
}