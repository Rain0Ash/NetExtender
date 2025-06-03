// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Types.ViewBoxes
{
    public class CenterViewBox : Viewbox
    {
        static CenterViewBox()
        {
            HorizontalAlignmentProperty.OverrideMetadataDefaultValue<CenterViewBox, HorizontalAlignment>(HorizontalAlignment.Center);
            VerticalAlignmentProperty.OverrideMetadataDefaultValue<CenterViewBox, VerticalAlignment>(VerticalAlignment.Center);
        }
    }
}