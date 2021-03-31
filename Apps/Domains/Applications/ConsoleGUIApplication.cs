// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


using NetExtender.GUI;
using WPFApp = System.Windows.Application;

namespace NetExtender.Apps.Domains.Applications
{
    public class ConsoleGUIApplication : ConsoleApplication
    {
        public override GUIType GUIType
        {
            get
            {
                return GUIType.ConsoleGUI;
            }
        }
        
        protected internal ConsoleGUIApplication(WPFApp application)
            : base(application)
        {
        }
    }
}