using System.Windows;
using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation.Types.TextBlocks
{
    public class CenterTextBlock : TextBlock
    {
        public CenterTextBlock()
        {
            TextAlignment = TextAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;
        }
    }
}