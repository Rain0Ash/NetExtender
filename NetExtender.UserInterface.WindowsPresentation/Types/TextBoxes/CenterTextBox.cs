using System.Windows;
using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation.Types.TextBoxes
{
    public class CenterTextBox : TextBox
    {
        public CenterTextBox()
        {
            TextAlignment = TextAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
}