using System.Windows;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class CenterTextBox : ScaleTextBox
    {
        static CenterTextBox()
        {
            TextAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox>(TextAlignment.Center);
            HorizontalAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox>(HorizontalAlignment.Stretch);
            VerticalAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox>(VerticalAlignment.Stretch);
            HorizontalContentAlignmentProperty.OverrideMetadata(typeof(CenterTextBox), new FrameworkPropertyMetadata(HorizontalAlignment.Center));
            VerticalContentAlignmentProperty.OverrideMetadataDefaultValue<CenterTextBox>(VerticalAlignment.Center);
        }
        
        public CenterTextBox()
        {
            TextAlignment = TextAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
}