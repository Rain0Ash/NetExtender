using System.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelCheckMark : CheckMark
    {
        static ExcelCheckMark()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExcelCheckMark), new FrameworkPropertyMetadata(typeof(ExcelCheckMark)));
        }
    }
}