// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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