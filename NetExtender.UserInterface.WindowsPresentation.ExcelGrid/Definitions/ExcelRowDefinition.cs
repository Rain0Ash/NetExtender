// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelRowDefinition : ExcelPropertyDefinition
    {
        private GridLength _height = GridLength.Auto;
        public GridLength Height
        {
            get
            {
                return _height;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _height, value);
            }
        }
    }
}