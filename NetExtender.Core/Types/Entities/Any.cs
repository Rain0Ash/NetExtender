// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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