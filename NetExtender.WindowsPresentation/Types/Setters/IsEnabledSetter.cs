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