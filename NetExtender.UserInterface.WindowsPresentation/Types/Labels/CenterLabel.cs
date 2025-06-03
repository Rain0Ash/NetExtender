// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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