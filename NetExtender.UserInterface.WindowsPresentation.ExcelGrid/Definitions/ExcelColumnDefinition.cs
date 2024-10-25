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