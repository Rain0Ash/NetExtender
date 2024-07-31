using System;

namespace NetExtender.Types.Entities
{
    public sealed record Any
    {
        public enum Value : Byte
        {
        }

        public override String ToString()
        {
            return nameof(Any);
        }
    }
}