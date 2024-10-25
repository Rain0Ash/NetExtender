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