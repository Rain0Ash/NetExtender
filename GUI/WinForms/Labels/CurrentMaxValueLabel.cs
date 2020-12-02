// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Linq;
using NetExtender.Utils.Types;
using NetExtender.Utils.Numerics;

// ReSharper disable MemberCanBePrivate.Global

namespace NetExtender.GUI.WinForms.Labels
{
    public class CurrentMaxValueLabel : AdditionalsLabel
    {
        private event EmptyHandler ValueChanged;

        private Decimal _currentValue;

        public Decimal CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                if (_currentValue == value)
                {
                    return;
                }

                _currentValue = value;
                ValueChanged?.Invoke();
            }
        }

        private Decimal _maximumValue = 100;
        public Decimal MaximumValue
        {
            get
            {
                return _maximumValue;
            }
            set
            {
                if (_maximumValue == value)
                {
                    return;
                }

                _maximumValue = value;
                ValueChanged?.Invoke();
            }
        }

        private String _separator = "\\";
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

        private DisplayType _displayType  = DisplayType.Value;
        public DisplayType DisplayType
        {
            get
            {
                return _displayType;
            }
            set
            {
                if (_displayType == value)
                {
                    return;
                }

                _displayType = value;
                ValueChanged?.Invoke();
            }
        }

        private Byte _fractional = 2;
        public Byte PercentFractionalCount
        {
            get
            {
                return _fractional;
            }
            set
            {
                if (_fractional == value)
                {
                    return;
                }

                _fractional = value;
                ValueChanged?.Invoke();
            }
        }

        private RoundType _round = RoundType.Banking;
        public RoundType Round
        {
            get
            {
                return _round;
            }
            set
            {
                if (_round == value)
                {
                    return;
                }

                _round = value;
                ValueChanged?.Invoke();
            }
        }

        public Decimal Step { get; set; } = 1;

        public Boolean Loop { get; set; } = false;

        public Boolean FixedDecimalNumber { get; set; } = true;

        private CultureInfo _culture = CultureInfo.InvariantCulture;
        public CultureInfo Culture
        {
            get
            {
                return _culture;
            }
            set
            {
                if (Equals(_culture, value))
                {
                    return;
                }

                _culture = value;
                ValueChanged?.Invoke();
            }
        }

        public void PerformStep()
        {
            CurrentValue = (CurrentValue + Step).ToRange(0, MaximumValue, Loop);
        }

        public CurrentMaxValueLabel()
        {
            ValueChanged += Display;
        }

        private String EvaluateAndFill(Int32 multiply)
        {
            Decimal value = (CurrentValue / MaximumValue.ToNonZero() * multiply).Round(PercentFractionalCount, Round);
            
            Int32 digits = value.GetDigitsCountAfterPoint();

            String zeros;
            if (FixedDecimalNumber)
            {
                zeros = String.Concat(Enumerable.Repeat("0", PercentFractionalCount - digits));

                if (digits <= 0)
                {
                    zeros = zeros.AddPrefix(".");
                }
            }
            else
            {
                zeros = String.Empty;
            }

            return $"{value.ToString(Culture)}{zeros}";
        }

        private String Value
        {
            get
            {
                return $"{CurrentValue}{Separator}{MaximumValue}";
            }
        }
        
        private String Percent
        {
            get
            {
                return $"{EvaluateAndFill(100)}%";
            }
        }

        private String Promille
        {
            get
            {
                return $"{EvaluateAndFill(1000)}‰";
            }
        }

        protected virtual void Display()
        {
            Text = DisplayType switch
            {
                DisplayType.ValueAndPercent => $"{Value} {Percent}",
                DisplayType.Percent => Percent,
                DisplayType.ValueAndPromille => $"{Value} {Promille}",
                DisplayType.Promille => Promille,
                _ => Value
            };;
        }

        protected override void Dispose(Boolean disposing)
        {
            ValueChanged = null;
            base.Dispose(disposing);
        }
    }
}