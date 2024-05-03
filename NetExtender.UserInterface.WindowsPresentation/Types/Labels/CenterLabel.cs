using System.Windows;
using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Labels
{
    public class CenterLabel : Label
    {
        public CenterLabel()
        {
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
}