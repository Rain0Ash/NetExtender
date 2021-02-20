// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Drawing.Colors
{
    public class HSL : IColor
    {
        public int H { get; set; }
        public byte S { get; set; }
        public byte L { get; set; }

        public HSL(int h, byte s, byte l)
        {
            H = h;
            S = s;
            L = l;
        }

        public override bool Equals(object obj)
        {
            HSL result = (HSL)obj;

            return (
                result != null &&
                H == result.H &&
                S == result.S &&
                L == result.L);
        }

        public override string ToString()
        {
            return $"{H}° {S}% {L}%";
        }
    }
}