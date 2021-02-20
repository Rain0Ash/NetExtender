// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Drawing.Colors
{
    public class HSV : IColor
    {
        public int H { get; set; }
        public byte S { get; set; }
        public byte V { get; set; }

        public HSV(int h, byte s, byte v)
        {
            H = h;
            S = s;
            V = v;
        }

        public override bool Equals(object obj)
        {
            HSV result = (HSV)obj;

            return (
                result != null &&
                H == result.H &&
                S == result.S &&
                V == result.V);
        }

        public override string ToString()
        {
            return $"{H}° {S}% {V}%";
        }
    }
}