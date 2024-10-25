using System.Windows;
using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelTextBlock : TextBlock
    {
        static ExcelTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExcelTextBlock), new FrameworkPropertyMetadata(typeof(ExcelTextBlock)));
        }
    }
}