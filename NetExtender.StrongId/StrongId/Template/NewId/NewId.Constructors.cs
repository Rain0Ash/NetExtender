
        public STRONGID(System.Guid value)
            : this(TYPE.FromGuid(value))
        {
        }

        public STRONGID(UNDERLYING value)
        {
            Value = value;
        }

        public static STRONGID New()
        {
            return new STRONGID(TYPE.Next());
        }