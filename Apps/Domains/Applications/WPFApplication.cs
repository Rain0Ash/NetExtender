// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using NetExtender.GUI;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.GUI.WPF.Windows;
using WPFApp = System.Windows.Application;

namespace NetExtender.Apps.Domains.Applications
{
    public class WPFApplication<TApp> : Application where TApp : WPFApp, new()
    {
        public TApp Application { get; }

        public override GUIType GUIType
        {
            get
            {
                return GUIType.WPF;
            }
        }

        public override Dispatcher Dispatcher
        {
            get
            {
                return Application.Dispatcher;
            }
        }

        public override ShutdownMode ShutdownMode
        {
            get
            {
                return Dispatcher.Invoke(() => Application.ShutdownMode);
            }
            set
            {
                if (ShutdownMode == value)
                {
                    return;
                }

                try
                {
                    Application.ShutdownMode = value;
                }
                catch(InvalidOperationException)
                {
                    //ignore
                }
            }
        }
        
        protected internal WPFApplication(TApp application)
        {
            Application = application ?? throw new ArgumentNullException();
        }
        
        public override void Run()
        {
            InitializeComponent();
            Application.Run();
        }

        public void Run(Window window)
        {
            if (window is not null)
            {
                Application.Run(window);
                return;
            }
            
            Run();
        }
        
        public override void Run(IWindow window)
        {
            Run(window as Window ?? window as WindowWrapper);
        }

        public override void Shutdown(Int32 code = 0)
        {
            Application.Dispatcher.Invoke(() => Application.Shutdown(code));
        }

        private void InitializeComponent()
        {
            const String initialize = "InitializeComponent";
            Application.GetType()
                .GetMethod(initialize, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod)?
                .Invoke(Application, null);
        }
    }
}