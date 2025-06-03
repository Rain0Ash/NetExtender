// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Applications.Interfaces
{
    public interface IDependencyApplication
    {
        public WindowsPresentationServiceProvider Provider { get; }
        public IDispatcher? Dispatcher { get; }
        public ShutdownMode ShutdownMode { get; set; }
        
        public void Shutdown();
        public void Shutdown(Int32 code);
    }
}