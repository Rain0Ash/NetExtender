
        public System.Boolean Equals(STRONGID other)
        {
            return this == other;
        }
                
        public override System.Boolean Equals(System.Object? other)
        {
            return other is STRONGID value && Equals(value);
        }
        
        public System.Boolean Equals(STRONGID other, TYPE epsilon)
        {
            return System.Math.Abs(Value - other.Value) < epsilon;
        }

        public override System.Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override System.String? ToString()
        {
            return Value.ToString();
        }