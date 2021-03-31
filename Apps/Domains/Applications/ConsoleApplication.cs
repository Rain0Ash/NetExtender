// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.GUI;
using WPFApp = System.Windows.Application;

namespace NetExtender.Apps.Domains.Applications
{
    public class ConsoleApplication : WPFApplication
    {
        public override GUIType GUIType
        {
            get
            {
                return GUIType.Console;
            }
        }

        public override void Run()
        {
            Application.Run();
        }

        protected internal ConsoleApplication(WPFApp application)
            : base(application)
        {
        }
    }
}