// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls;

namespace NetExtender.GUI.WPF.ComboBoxes
{
    public class StartedComboBox : ComboBox
    {
        public event RoutedEventHandler Started;
        
        private Boolean _isStarted;

        protected StartedComboBox()
        {
            Loaded += OnStarted;
        }
        
        private void OnStarted(Object sender, RoutedEventArgs e)
        {
            if (_isStarted)
            {
                return;
            }

            Loaded -= OnStarted;
            Started?.Invoke(sender, e);
            _isStarted = true;
        }
    }
}