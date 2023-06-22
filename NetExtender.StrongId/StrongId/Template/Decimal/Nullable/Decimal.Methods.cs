
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