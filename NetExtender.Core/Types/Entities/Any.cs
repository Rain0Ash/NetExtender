using System;

namespace NetExtender.Types.Entities
{
    public record Any
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