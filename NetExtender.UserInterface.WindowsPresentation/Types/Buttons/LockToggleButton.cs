using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Buttons
{
    public class LockToggleButton : ToggleButton
    {
        public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register(nameof(IsLocked), typeof(Boolean), typeof(LockToggleButton), new UIPropertyMetadata(true));

        public Boolean IsLocked
        {
            get
            {
                return (Boolean) GetValue(IsLockedProperty);
            }
            set
            {
                SetValue(IsLockedProperty, value);
            }
        }

        protected override void OnToggle()
        {
            if (!IsLocked)
            {
                base.OnToggle();
            }
        }
    }
}