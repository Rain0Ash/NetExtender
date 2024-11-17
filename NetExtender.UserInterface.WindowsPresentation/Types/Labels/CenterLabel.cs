using System.Windows;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class CenterLabel : Label
    {
        static CenterLabel()
        {
            HorizontalAlignmentProperty.OverrideMetadataDefaultValue<CenterLabel, HorizontalAlignment>(HorizontalAlignment.Stretch);
            VerticalAlignmentProperty.OverrideMetadataDefaultValue<CenterLabel, VerticalAlignment>(VerticalAlignment.Stretch);
            HorizontalContentAlignmentProperty.OverrideMetadataDefaultValue<CenterLabel, HorizontalAlignment>(HorizontalAlignment.Center);
            VerticalContentAlignmentProperty.OverrideMetadataDefaultValue<CenterLabel, VerticalAlignment>(VerticalAlignment.Center);
        }
        
        public CenterLabel()
        {
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
}