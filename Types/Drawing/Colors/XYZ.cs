// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Drawing.Colors
{
    public class XYZ : IColor
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public XYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            XYZ result = (XYZ)obj;

            return (
                result != null &&
                X == result.X &&
                Y == result.Y &&
                Z == result.Z);
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }
    }
}