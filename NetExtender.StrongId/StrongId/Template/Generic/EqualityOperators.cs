
        public static System.Boolean operator ==(STRONGID first, STRONGID second)
        {
            return first.Value == second.Value;
        }

        public static System.Boolean operator !=(STRONGID first, STRONGID second)
        {
            return !(first == second);
        }