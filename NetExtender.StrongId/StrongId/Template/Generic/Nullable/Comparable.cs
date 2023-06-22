
        public System.Int32 CompareTo(STRONGID other)
        {
            return Value.HasValue && other.Value.HasValue ? Value.Value.CompareTo(other.Value.Value) : Value.HasValue ? 1 : other.Value.HasValue ? -1 : 0;
        }