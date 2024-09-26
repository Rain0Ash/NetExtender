// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Domains.WindowsPresentation.Builder;
using NetExtender.UserInterface.WindowsPresentation.Windows;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.WindowsPresentation.View
{
    public class DependencyInjectionWindowsPresentationView<T> : WindowsPresentationView<T, WindowsPresentationBuilder<T>> where T : Window, IDependencyWindow
    {
        public virtual IServiceProvider Provider
        {
            get
            {
                return ServiceProviderUtilities.Provider;
            }
        }

        public DependencyInjectionWindowsPresentationView()
            : base(ServiceProviderUtilities.Provider.GetRequiredService<T>())
        {
        }

        public DependencyInjectionWindowsPresentationView(T? window)
            : base(window)
        {
        }
    }
}