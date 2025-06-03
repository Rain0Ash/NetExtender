// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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