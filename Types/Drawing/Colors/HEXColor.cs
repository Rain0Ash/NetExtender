// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Drawing.Colors
{
    public class HEXColor : IColor
    {
        public Int32 Code { get; }

        public String Value
        {
            get
            {
                return ToString();
            }
        }

        public HEXColor(Byte r, Byte g, Byte b)
        {
        }
        
        public 
        
        public HEXColor(String value)
        {
            if (value.)
        }

        public override bool Equals(object obj)
        {
            return Value == (obj as HEXColor)?.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}