// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Domains.Applications.Interfaces;
using WPFApp = System.Windows.Application;

namespace NetExtender.Domains.Applications
{
    public class WPFConsoleApplication : WPFApplication
    {
        protected WPFConsoleApplication(WPFApp application)
            : base(application)
        {
        }
        
        public override IApplication Run()
        {
            Application.Run();
            return this;
        }
    }
}