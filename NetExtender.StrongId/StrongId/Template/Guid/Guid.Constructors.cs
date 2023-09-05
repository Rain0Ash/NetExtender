
        public STRONGID(UNDERLYING value)
        {
            Value = value;
        }

        public static STRONGID New()
        {
            return new STRONGID(TYPE.NewGuid());
        }