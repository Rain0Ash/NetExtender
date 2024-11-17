using System.Windows;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class CenterTextBlock : TextBlock
    {
        static CenterTextBlock()
        {
            TextAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBlock, TextAlignment>(TextAlignment.Center);
            HorizontalAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBlock, HorizontalAlignment>(HorizontalAlignment.Center);
            VerticalAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBlock, VerticalAlignment>(VerticalAlignment.Center);
        }
    }
}