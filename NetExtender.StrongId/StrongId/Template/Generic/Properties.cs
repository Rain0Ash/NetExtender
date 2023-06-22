
        STRONGID NetExtender.StrongId.IStrongId<STRONGID, UNDERLYING>.Id
        {
            get
            {
                return this;
            }
        }

        UNDERLYING NetExtender.StrongId.IStrongId<UNDERLYING>.Underlying
        {
            get
            {
                return Value;
            }
        }

        public System.Boolean IsDefault
        {
            get
            {
                return Value == default;
            }
        }

        public System.Boolean IsZero
        {
            get
            {
                return Value == 0;
            }
        }

        public System.Boolean IsPositive
        {
            get
            {
                return Value > 0;
            }
        }

        public System.Boolean IsNegative
        {
            get
            {
                return Value < 0;
            }
        }