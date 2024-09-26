// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Controls;
using NetExtender.WindowsPresentation.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Controls
{
    public class DependencyUserControl : UserControl, IDependencyUserControl
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