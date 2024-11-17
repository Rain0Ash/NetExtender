using System.Windows;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class CenterTextBox : ScaleTextBox
    {
        static CenterTextBox()
        {
            TextAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox, TextAlignment>(TextAlignment.Center);
            HorizontalAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox, HorizontalAlignment>(HorizontalAlignment.Stretch);
            VerticalAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox, VerticalAlignment>(VerticalAlignment.Stretch);
            HorizontalContentAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox, HorizontalAlignment>(HorizontalAlignment.Center);
            VerticalContentAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox, VerticalAlignment>(VerticalAlignment.Center);
        }
        
        public CenterTextBox()
        {
            TextAlignment = TextAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
}