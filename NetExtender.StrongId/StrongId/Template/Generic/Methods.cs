
        public System.Boolean Equals(STRONGID other)
        {
            return this == other;
        }
                
        public override System.Boolean Equals(System.Object? obj)
        {
            return obj is STRONGID other && Equals(other);
        }

        public override System.Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override System.String? ToString()
        {
            return Value.ToString();
        }