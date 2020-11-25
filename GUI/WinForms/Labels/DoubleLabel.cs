// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.GUI.WinForms.Labels
{
    public class DoubleLabel : AdditionalsLabel
    {
        private event EmptyHandler ValueChanged;
        private String _firstString;
        private String _secondString;
        private String _separator = "\\";

        public String FirstString
        {
            get
            {
                return _firstString;
            }
            set
            {
                if (_firstString == value)
                {
                    return;
                }

                _firstString = value;
                ValueChanged?.Invoke();
            }
        }

        public String SecondString
        {
            get
            {
                return _secondString;
            }
            set
            {
                if (_secondString == value)
                {
                    return;
                }

                _secondString = value;
                ValueChanged?.Invoke();
            }
        }

        public String Separator
        {
            get
            {
                return _separator;
            }
            set
            {
                if (_separator == value)
                {
                    return;
                }

                _separator = value;
                ValueChanged?.Invoke();
            }
        }

        public void Reload()
        {
            Text = $@"{FirstString}{Separator}{SecondString}";
        }

        public DoubleLabel()
        {
            ValueChanged += Reload;
        }
    }
}