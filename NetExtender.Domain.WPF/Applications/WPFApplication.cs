// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Windows;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Types.Dispatchers;
using NetExtender.Types.Dispatchers.Interfaces;
using WPFApp = System.Windows.Application;

namespace NetExtender.Domains.Applications
{
    public class WPFApplication : Application
    {
        public WPFApp Application { get; }

        public override IDispatcher Dispatcher { get; } = new DispatcherWrapper(WPFApp.Current.Dispatcher);

        public override ApplicationShutdownMode ShutdownMode
        {
            get
            {
                return Dispatcher.Invoke(() => (ApplicationShutdownMode) Application.ShutdownMode);
            }
            set
            {
                if (ShutdownMode == value)
                {
                    return;
                }

                try
                {
                    Application.ShutdownMode = (ShutdownMode) value;
                }
                catch(InvalidOperationException)
                {
                    //ignore
                }
            }
        }
        
        protected WPFApplication(WPFApp application)
        {
            Application = application ?? throw new ArgumentNullException();
        }
        
        public override IApplication Run()
        {
            InitializeComponent();
            Application.Run();
            return this;
        }

        public IApplication Run(Window? window)
        {
            if (window is null)
            {
                return Run();
            }

            Application.Run(window);
            return this;
        }
        
        public override IApplication Run<T>(T window)
        {
            return Run(window as Window);
        }

        public override void Shutdown(Int32 code = 0)
        {
            Application.Dispatcher.Invoke(() => Application.Shutdown(code));
        }

        private void InitializeComponent()
        {
            Application.GetType()
                .GetMethod("InitializeComponent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod)?
                .Invoke(Application, null);
        }
    }
}