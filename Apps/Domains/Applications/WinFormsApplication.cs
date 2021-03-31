// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using NetExtender.GUI;
using NetExtender.GUI.WinForms.Forms;

namespace NetExtender.Apps.Domains.Applications
{
    public class WinFormsApplication : Application
    {
        public override GUIType GUIType
        {
            get
            {
                return GUIType.WinForms;
            }
        }

        public override Dispatcher Dispatcher
        {
            get
            {
                return Dispatcher.CurrentDispatcher;
            }
        }

        public override ShutdownMode ShutdownMode
        {
            get
            {
                return ShutdownMode.OnLastWindowClose;
            }
            set
            {
            }
        }

        protected internal WinFormsApplication()
        {
        }
        
        [STAThread]
        public override void Run()
        {
            System.Windows.Forms.Application.Run();
        }

        [STAThread]
        public void Run(Form form)
        {
            if (form is not null)
            {
                System.Windows.Forms.Application.Run(form);
                return;
            }
            
            Run();
        }
        
        public override void Run<T>(T window)
        {
            Run(window as Form ?? window as FormWrapper);
        }
    }
}