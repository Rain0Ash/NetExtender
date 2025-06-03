// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;

namespace NetExtender.WindowsPresentation.Types.Setters
{
    public class IsEnabledSetter : Setter
    {
        public IsEnabledSetter()
        {
            Property = UIElement.IsEnabledProperty;
        }
    }
}