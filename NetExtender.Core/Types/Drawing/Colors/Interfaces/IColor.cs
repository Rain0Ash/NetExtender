// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Drawing.Colors.Interfaces
{
    public interface IColor<T> : IColor, IEquatable<T>
    {
    }
    
    public interface IColor : IFormattable
    {
        public ColorType Type { get; }

        public Color ToColor();
        public Boolean ToColor(out Color color);
        public Boolean Equals(IColor color);
        public String ToString(String? format);
    }
}
