
        public System.Boolean Equals(STRONGID other)
        {
            return this == other;
        }
                
        public override System.Boolean Equals(System.Object? obj)
        {
            return obj is STRONGID other && Equals(other);
        }
        
        public System.Boolean Equals(STRONGID other, TYPE epsilon)
        {
            if (Value is null || other.Value is null)
            {
                return Value.HasValue == other.Value.HasValue;
            }
            
            if (TYPE.IsNaN(Value.Value) || TYPE.IsNaN(other.Value.Value))
            {
                return false;
            }

            if (TYPE.IsPositiveInfinity(Value.Value) && TYPE.IsPositiveInfinity(other.Value.Value) || TYPE.IsNegativeInfinity(Value.Value) && TYPE.IsNegativeInfinity(other.Value.Value))
            {
                return true;
            }

            return System.Math.Abs(Value.Value - other.Value.Value) < epsilon;
        }

        public override System.Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override System.String? ToString()
        {
            return Value.ToString();
        }