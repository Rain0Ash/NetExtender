// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


using NetExtender.GUI;
using WPFApp = System.Windows.Application;

namespace NetExtender.Apps.Domains.Applications
{
    public class ConsoleGUIApplication<TApp> : ConsoleApplication<TApp> where TApp : WPFApp, new()
    {
        public override GUIType GUIType
        {
            get
            {
                return GUIType.ConsoleGUI;
            }
        }
        
        protected internal ConsoleGUIApplication(TApp application)
            : base(application)
        {
        }
    }
}