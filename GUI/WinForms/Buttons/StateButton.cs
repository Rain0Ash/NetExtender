// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Utils.Numerics;

namespace NetExtender.GUI.WinForms.Buttons
{
    public class StateButton : Button
    {
        private Int32 _state;
        public event TypeHandler<Int32> StateChanged;

        public Int32 State
        {
            get
            {
                return _state;
            }
            set
            {
                Int32 val = value.ToRange(0, MaximumStates);
                if (_state == val)
                {
                    return;
                }

                _state = val;
                StateChanged?.Invoke(val);
            }
        }

        private Int32 _maximumStates;
        public event TypeHandler<Int32> MaximumStatesChanged;

        public Int32 MaximumStates
        {
            get
            {
                return _maximumStates;
            }
            set
            {
                if (_maximumStates == value)
                {
                    return;
                }

                _maximumStates = value;
                MaximumStatesChanged?.Invoke(value);
            }
        }

        public StateButton()
        {
            MaximumStatesChanged += OnMaximumStatesChanged;
            Click += OnButtonClick;
        }

        private void OnButtonClick(Object sender, EventArgs e)
        {
            if (State >= MaximumStates)
            {
                State = 0;
                return;
            }

            State++;
        }

        private void OnMaximumStatesChanged(Int32 number)
        {
            State = number;
        }

        protected override void Dispose(Boolean disposing)
        {
            Click -= OnButtonClick;
            MaximumStatesChanged = null;
            StateChanged = null;
            base.Dispose(disposing);
        }
    }
}