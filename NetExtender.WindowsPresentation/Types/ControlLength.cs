// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types
{
    public readonly struct ControlLength : IEquatable<ControlLength>, IEquatable<GridLength>, IEquatable<DataGridLength>
    {
        public static implicit operator ControlLength(Double value)
        {
            return new ControlLength(value);
        }
        
        public static implicit operator GridLength(ControlLength value)
        {
            return value.Internal.ToGridLength();
        }
        
        public static implicit operator DataGridLength(ControlLength value)
        {
            return value.Internal;
        }
        
        public static implicit operator ControlLength(GridLength value)
        {
            return new ControlLength(value);
        }
        
        public static implicit operator ControlLength(DataGridLength value)
        {
            return new ControlLength(value);
        }

        public static Boolean operator ==(ControlLength first, ControlLength second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ControlLength first, ControlLength second)
        {
            return !(first == second);
        }

        public static ControlLength Auto
        {
            get
            {
                return DataGridLength.Auto;
            }
        }

        public static ControlLength SizeToCells
        {
            get
            {
                return DataGridLength.SizeToCells;
            }
        }

        public static ControlLength SizeToHeader
        {
            get
            {
                return DataGridLength.SizeToHeader;
            }
        }
        
        private DataGridLength Internal { get; }

        public Double DesiredValue
        {
            get
            {
                return Internal.DesiredValue;
            }
        }

        public Double DisplayValue
        {
            get
            {
                return Internal.DisplayValue;
            }
        }

        public Boolean IsAbsolute
        {
            get
            {
                return Internal.IsAbsolute;
            }
        }

        public Boolean IsAuto
        {
            get
            {
                return Internal.IsAuto;
            }
        }

        public Boolean IsSizeToCells
        {
            get
            {
                return Internal.IsSizeToCells;
            }
        }

        public Boolean IsSizeToHeader
        {
            get
            {
                return Internal.IsSizeToHeader;
            }
        }

        public Boolean IsStar
        {
            get
            {
                return Internal.IsStar;
            }
        }

        public DataGridLengthUnitType UnitType
        {
            get
            {
                return Internal.UnitType;
            }
        }

        public Double Value
        {
            get
            {
                return Internal.Value;
            }
        }

        public ControlLength(Double pixels)
        {
            Internal = new DataGridLength(pixels);
        }

        public ControlLength(Double value, DataGridLengthUnitType type)
        {
            Internal = new DataGridLength(value, type);
        }

        public ControlLength(Double value, DataGridLengthUnitType type, Double desiredValue, Double displayValue)
        {
            Internal = new DataGridLength(value, type, desiredValue, displayValue);
        }

        public ControlLength(GridLength length)
        {
            Internal = length.ToDataGridLength();
        }

        public ControlLength(DataGridLength length)
        {
            Internal = length;
        }

        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                ControlLength length => Equals(length),
                GridLength length => Equals(length),
                DataGridLength length => Equals(length),
                _ => false
            };
        }

        public Boolean Equals(GridLength other)
        {
            return Internal.Equals(other.ToDataGridLength());
        }

        public Boolean Equals(DataGridLength other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(ControlLength other)
        {
            return Internal.Equals(other.Internal);
        }

        public override String ToString()
        {
            return Internal.ToString();
        }
    }
}