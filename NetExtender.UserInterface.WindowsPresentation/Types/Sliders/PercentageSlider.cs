using System;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class PercentageSlider : Slider
    {
        static PercentageSlider()
        {
            MinimumProperty.OverrideMetadataDefaultValue<PercentageSlider, Double>(0);
            MaximumProperty.OverrideMetadataDefaultValue<PercentageSlider, Double>(100);
            DelayProperty.OverrideMetadataDefaultValue<PercentageSlider, Int32>(1);
            IntervalProperty.OverrideMetadataDefaultValue<PercentageSlider, Int32>(1);
            TickFrequencyProperty.OverrideMetadataDefaultValue<PercentageSlider, Double>(1);
            IsSnapToTickEnabledProperty.OverrideMetadataDefaultValue<PercentageSlider, Boolean>(true);
        }
    }
}