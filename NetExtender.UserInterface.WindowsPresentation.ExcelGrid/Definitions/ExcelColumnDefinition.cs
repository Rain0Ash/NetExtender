// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelColumnDefinition : ExcelPropertyDefinition
    {
        private GridLength _width = GridLength.Auto;
        public GridLength Width
        {
            get
            {
                return _width;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _width, value);
            }
        }
    }
}