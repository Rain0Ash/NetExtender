using System.Windows;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Types.ViewBoxes
{
    public class CenterViewBox : Viewbox
    {
        static CenterViewBox()
        {
            HorizontalAlignmentProperty.OverrideMetadataDefaultValue<CenterViewBox>(HorizontalAlignment.Center);
            VerticalAlignmentProperty.OverrideMetadataDefaultValue<CenterViewBox>(VerticalAlignment.Center);
        }
    }
}