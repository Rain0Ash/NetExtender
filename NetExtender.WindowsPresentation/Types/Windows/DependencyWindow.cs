// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.WindowsPresentation.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public class DependencyWindow : Window, IDependencyWindow
    {
        protected static WindowsPresentationServiceProvider Provider
        {
            get
            {
                return WindowsPresentationServiceProvider.Instance;
            }
        }
    }
}