using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Sliders
{
    public class PercentageSlider : Slider
    {
        public PercentageSlider()
        {
            Minimum = 0;
            Maximum = 100;
            Delay = 1;
            Interval = 1;
            TickFrequency = 1;
            IsSnapToTickEnabled = true;
        }
    }
}