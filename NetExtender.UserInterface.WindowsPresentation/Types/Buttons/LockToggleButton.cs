// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class LockToggleButton : ToggleButton
    {
        public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register(nameof(IsLocked), typeof(Boolean), typeof(LockToggleButton), new UIPropertyMetadata(true));

        public Boolean IsLocked
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(IsLockedProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
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