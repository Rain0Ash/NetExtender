using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class PercentageSlider : Slider
    {
        static PercentageSlider()
        {
            MinimumProperty.OverrideMetadataDefaultValue<PercentageSlider>(0);
            MaximumProperty.OverrideMetadataDefaultValue<PercentageSlider>(100);
            DelayProperty.OverrideMetadataDefaultValue<PercentageSlider>(1);
            IntervalProperty.OverrideMetadataDefaultValue<PercentageSlider>(1);
            TickFrequencyProperty.OverrideMetadataDefaultValue<PercentageSlider>(1);
            IsSnapToTickEnabledProperty.OverrideMetadataDefaultValue<PercentageSlider>(true);
        }
    }
}